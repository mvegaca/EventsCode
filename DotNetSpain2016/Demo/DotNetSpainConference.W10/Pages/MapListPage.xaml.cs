//---------------------------------------------------------------------------
//
// <copyright file="PonentesListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>2/16/2016 7:35:39 AM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.LocalStorage;
using DotNetSpainConference.Sections;
using DotNetSpainConference.ViewModels;
using AppStudio.Uwp;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using System;
using Windows.Storage.Streams;
using Windows.Foundation;
using System.Globalization;
using System.Linq;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;

namespace DotNetSpainConference.Pages
{
    public sealed partial class MapListPage : Page
    {
        public MapListPage()
        {
            this.ViewModel = ListViewModel.CreateNew(Singleton<MapConfig>.Instance);

            this.InitializeComponent();
            new Microsoft.ApplicationInsights.TelemetryClient().TrackPageView(this.GetType().FullName);
        }

        public ListViewModel ViewModel { get; set; }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
            AddIconsFromData();
            base.OnNavigatedTo(e);
        }

        private void AddIconsFromData()
        {
            foreach (var element in mapControl.MapElements)
            {
                if (element.GetType() == typeof(MapIcon))
                {
                    MapIcon icon = element as MapIcon;
                    if (icon.Title != "DotNet Spain Conference")
                    {
                        mapControl.MapElements.Remove(element);
                    }
                }
            }
            foreach (var item in ViewModel.Items)
            {
                var coordinates = item.Content.Split('*');
                double latitude = Double.Parse(coordinates[0], new CultureInfo("en-US"));
                double longitude = Double.Parse(coordinates[1], new CultureInfo("en-US"));
                AddIcon(latitude, longitude, item.Title, coordinates[2]);
            }
        }

        private void MapLoaded(object sender, RoutedEventArgs e)
        {
            mapControl.MapServiceToken = "0xsDiwvZhehrn5Datto8~vromUP5HdosyrxmivUDHVw~AgtGyqVF3WOQxMIhD2EoRHBmuBnUE1-yJ7ky887bNBUjw6QJlsqCtxCM7AIvUYso";
            mapControl.Center = new Geopoint(new BasicGeoposition()
            {
                Latitude = 40.3937115,
                Longitude = -3.7964247
            });
            mapControl.ZoomLevel = 17;
            AddIcon(40.3937115, -3.7964247, "DotNet Spain Conference", "poi.png");
        }

        private void AddIcon(double latitude, double longitude, string title, string image, bool navigate = false)
        {
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Location = new Geopoint(new BasicGeoposition() { Latitude = latitude, Longitude = longitude });
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.Title = title;
            mapIcon1.Image = RandomAccessStreamReference.CreateFromUri(new Uri(string.Format("ms-appx:///Assets/DataImages/{0}", image)));
            mapIcon1.ZIndex = 0;
            mapControl.MapElements.Add(mapIcon1);
        }

        private void MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            try
            {
                MapIcon mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
                if (mapIcon != null)
                {
                    ItemViewModel itemViewModel = ViewModel.Items.FirstOrDefault(x => x.Title == mapIcon.Title);
                    if (itemViewModel != null)
                    {
                        var coordinates = itemViewModel.Content.Split('*');
                        ActionCommands.NavigateToUrl.Execute(string.Format("bingmaps:?collection=point.{0}_{1}_{2}&lvl=18", coordinates[0], coordinates[1], itemViewModel.Title));
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
