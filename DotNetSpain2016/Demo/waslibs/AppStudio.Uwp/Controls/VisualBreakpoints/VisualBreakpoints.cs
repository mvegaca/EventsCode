﻿using AppStudio.Uwp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppStudio.Uwp.Controls
{
    public sealed class VisualBreakpoints : Control, INotifyPropertyChanged
    {
        private static Dictionary<string, string> _configCache = new Dictionary<string, string>();
        private static SemaphoreSlim readFileSemaphore = new SemaphoreSlim(1);

        private int _lastActive = -1;
        private int[] _breakpointsIndex = new int[0];
        private BreakpointsTable _fallBackBreakPoint = new BreakpointsTable();
        private BreakpointsDictionary _breakpointsTable = new BreakpointsDictionary();

        private string _configFile;
        public string ConfigFile
        {
            get { return _configFile; }
            set { _configFile = value; }
        }

        public dynamic Active { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public VisualBreakpoints()
        {
            Active = new ExpandoObject();
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Loaded += VisualBreakpoints_Loaded;
                Unloaded += VisualBreakpoints_Unloaded;
            }
        }

        private void VisualBreakpoints_Unloaded(object sender, RoutedEventArgs e)
        {
            var container = Parent as FrameworkElement;
            if (container != null)
            {
                container.SizeChanged -= Container_SizeChanged;
            }
            else
            {
                Window.Current.SizeChanged -= Window_SizeChanged;
            }
        }

        private async void VisualBreakpoints_Loaded(object sender, RoutedEventArgs e)
        {
            await Initialize();

            var container = Parent as FrameworkElement;
            double initialWidth = 0;
            if (container != null)
            {
                container.SizeChanged += Container_SizeChanged;
                initialWidth = container.ActualWidth;
            }
            else
            {
                Window.Current.SizeChanged += Window_SizeChanged;
                initialWidth = Window.Current.Bounds.Width;
            }

            TrySetActive(initialWidth);
        }

        private void Container_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TrySetActive(e.NewSize.Width);
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            TrySetActive(e.Size.Width);
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void TrySetActive(double width)
        {
            if (_breakpointsIndex != null && _breakpointsIndex.Length > 0)
            {
                var currentActive = _breakpointsIndex.FirstOrDefault(b => width < b);

                if (currentActive != _lastActive)
                {
                    var activeBreakpoint = GetActiveBreakpoint(currentActive);
                    SetActiveProperties(activeBreakpoint);

                    _lastActive = currentActive;
                }
            }
        }

        private void SetActiveProperties(BreakpointsTable activeBreakpoint)
        {
            if (activeBreakpoint != null)
            {
                var activeDict = Active as IDictionary<string, object>;

                foreach (var props in activeBreakpoint)
                {
                    activeDict[props.Name] = props.Value;
                }

                //this is because: System.Reflection.MemberInfo.get_MetadataToken()' cannot be used on the current platform. See http://go.microsoft.com/fwlink/?LinkId=248273 for more information.
                OnPropertyChanged("Active");
            }
        }

        private BreakpointsTable GetActiveBreakpoint(int currentActive)
        {
            BreakpointsTable activeBreakpoint;

            if (_breakpointsTable.ContainsKey(currentActive.ToString()))
            {
                activeBreakpoint = _breakpointsTable[currentActive.ToString()];
            }
            else
            {
                activeBreakpoint = _fallBackBreakPoint;
            }

            return activeBreakpoint;
        }

        private async Task Initialize()
        {
            await InitBreakPoints();
        }

        private async Task InitBreakPoints()
        {
            var loadedFiles = new List<string>();

            var rawConfigContents = await GetConfigContents(_configFile, loadedFiles);
            await ReadBreakPoints(rawConfigContents, loadedFiles);
        }

        private async Task<string> GetConfigContents(string configFile, List<string> loadedFiles)
        {
            var cacheKey = configFile.ToLowerInvariant();
            if (loadedFiles.Any(f => f == cacheKey))
            {
                throw new OverflowException($"Config file: '{configFile}' was already loaded.");
            }

            //lock the execution in order to not request the same file several times
            await readFileSemaphore.WaitAsync();
            try
            {
                if (!_configCache.ContainsKey(cacheKey))
                {
                    var fileContents = await GetConfigContentsFromFile(configFile);

                    loadedFiles.Add(configFile);
                    _configCache.Add(cacheKey, fileContents);
                }
            }
            finally
            {
                readFileSemaphore.Release();
            }

            return _configCache[cacheKey];
        }

        private async Task<string> GetConfigContentsFromFile(string configFile)
        {
            var uri = new Uri(string.Format("ms-appx://{0}", configFile));

            try
            {
                var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
                var randomStream = await file.OpenReadAsync();

                using (var r = new StreamReader(randomStream.AsStreamForRead()))
                {
                    return await r.ReadToEndAsync();
                }
            }
            catch (FileNotFoundException)
            {
                throw new ArgumentException($"Config file '{uri}' does not found");
            }
        }

        private async Task ReadBreakPoints(string contents, List<string> loadedFiles)
        {
            var configContents = JsonConvert.DeserializeObject<BreakpointsConfig>(contents);

            if (configContents == null)
            {
                return;
            }

            await ReadImport(configContents, loadedFiles);

            if (configContents.breakpoints == null)
            {
                return;
            }

            ReBuildIndex(configContents);

            foreach (var b in configContents.breakpoints)
            {
                if (b.maxwidth.IsNumeric())
                {
                    _breakpointsTable.Merge(b);
                }
            }

            var fallBack = configContents.breakpoints
                                            .FirstOrDefault(b => !b.maxwidth.IsNumeric());
            if (fallBack != null)
            {
                _fallBackBreakPoint.Merge(fallBack);
            }
        }

        private async Task ReadImport(BreakpointsConfig configContents, List<string> loadedFiles)
        {
            if (!string.IsNullOrWhiteSpace(configContents._import))
            {
                var rawConfigContents = await GetConfigContents(configContents._import, loadedFiles);
                await ReadBreakPoints(rawConfigContents, loadedFiles);
            }
        }

        private void ReBuildIndex(BreakpointsConfig breakpointsConfig)
        {
            var tmpIndex = new List<int>(_breakpointsIndex);

            var newItems = breakpointsConfig.breakpoints
                                                .Where(b => b.maxwidth.IsNumeric())
                                                .Select(b => int.Parse(b.maxwidth));

            foreach (var item in newItems)
            {
                if (!tmpIndex.Any(i => i == item))
                {
                    tmpIndex.Add(item);
                }
            }

            _breakpointsIndex = tmpIndex
                                    .OrderBy(i => i)
                                    .ToArray();
        }
    }
}
