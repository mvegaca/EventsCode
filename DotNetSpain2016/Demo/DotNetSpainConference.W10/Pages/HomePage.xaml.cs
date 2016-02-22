//---------------------------------------------------------------------------
//
// <copyright file="HomePage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>2/22/2016 2:47:25 PM</createdOn>
//
//---------------------------------------------------------------------------

using AppStudio.Uwp;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DotNetSpainConference.ViewModels;

namespace DotNetSpainConference.Pages
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            ViewModel = new MainViewModel(12);            
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            new Microsoft.ApplicationInsights.TelemetryClient().TrackPageView(this.GetType().FullName);
        }		
        public MainViewModel ViewModel { get; set; }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();			
            searchCtrl.Reset();
			searchCtrl.IsTextVisibleChanged += SearchCtrlIsTextVisibleChanged;
            
        }

		protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            searchCtrl.IsTextVisibleChanged -= SearchCtrlIsTextVisibleChanged;
        }
        private double appBarWidth;
		private void SearchCtrlIsTextVisibleChanged(object sender, bool isTextVisible)
        {
            if (appBar.ActualWidth > 0)
            {
                appBarWidth = appBar.ActualWidth;
            }
            if (isTextVisible)
            {
                appBar.AnimateWidth(0);
            }
            else
            {
                appBar.AnimateWidth(appBarWidth);
            }
        }
    }
}
