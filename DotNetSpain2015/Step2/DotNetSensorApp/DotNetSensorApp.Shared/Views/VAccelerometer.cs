using DotNetSensorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Navigation;

namespace DotNetSensorApp.Views
{
    public sealed partial class VAccelerometer
    {
        VMAccelerometer ViewModel { get { return DataContext as VMAccelerometer; } }
        public VAccelerometer()
        {
            this.InitializeComponent();
            ViewModel.SetPage(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Start();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ViewModel.Stop();
        }
    }
}
