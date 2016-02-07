//---------------------------------------------------------------------------
//
// <copyright file="VideosChannel9ListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>2/5/2016 5:57:55 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.Rss;
using DotNetSpainConference.Sections;
using DotNetSpainConference.ViewModels;
using AppStudio.Uwp;

namespace DotNetSpainConference.Pages
{
    public sealed partial class VideosChannel9ListPage : Page
    {
        public VideosChannel9ListPage()
        {
            this.ViewModel = ListViewModel.CreateNew(Singleton<VideosChannel9Config>.Instance);

            this.InitializeComponent();
            new Microsoft.ApplicationInsights.TelemetryClient().TrackPageView(this.GetType().FullName);
        }

        public ListViewModel ViewModel { get; set; }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

    }
}