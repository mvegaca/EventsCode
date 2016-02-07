
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LordOfCodes.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private string _hastag;
        public string Hastag
        {
            get { return _hastag; }
            set { SetProperty(ref _hastag, value); }
        }
        private ObservableCollection<InstagramSchema> _items;
        public ObservableCollection<InstagramSchema> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
        public MainPage()
        {
            this.Items = new ObservableCollection<InstagramSchema>();
            this.Hastag = "universityofsalamanca";
            this.InitializeComponent();
        }

        private async void GetInstagramDataClick(object sender, RoutedEventArgs e)
        {
            await GetInstagramData();
        }

        private async Task GetInstagramData()
        {
            var appId = "4e9badaafa4e4436977733f01e05fbd0";
            Uri requestedUri = new Uri(string.Format("https://api.instagram.com/v1/tags/{0}/media/recent?client_id={1}", Hastag, appId), UriKind.Absolute);
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(requestedUri);
            var statusCode = response.StatusCode;
            string jsonData = await response.Content.ReadAsStringAsync();
            var parsedData = JsonConvert.DeserializeObject<InstagramResponse>(jsonData);
            if (parsedData != null)
            {
                Items.Clear();
                foreach (var item in parsedData.ToSchema())
                {
                    Items.Add(item);
                }
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion INotifyPropertyChanged
    }
}
