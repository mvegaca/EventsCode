using DotNetSensorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace DotNetSensorApp.Views
{
    public sealed partial class VHome : Page
    {
        VMHome ViewModel { get { return DataContext as VMHome; } }
        public VHome()
        {
            this.InitializeComponent();
            ViewModel.SetPage(this);
        }

        private void PageLoad(object sender, RoutedEventArgs e)
        {
            ViewModel.GetStatusCommand.Execute(null);
        }

        private void Navigate_Tap(object sender, TappedRoutedEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            string parameter = sp.Tag as string;
            ViewModel.NavigateCommand.Execute(parameter);
        }
    }
}
