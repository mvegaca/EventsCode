//---------------------------------------------------------------------------
//
// <copyright file="TwitterListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>2/22/2016 2:47:25 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.Twitter;
using DotNetSpainConference.Sections;
using DotNetSpainConference.ViewModels;
using AppStudio.Uwp;

namespace DotNetSpainConference.Pages
{
    public sealed partial class TwitterListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public TwitterListPage()
        {
			this.ViewModel = ListViewModel.CreateNew(Singleton<TwitterConfig>.Instance);

            this.InitializeComponent();

            new Microsoft.ApplicationInsights.TelemetryClient().TrackPageView(this.GetType().FullName);
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

    }
}
