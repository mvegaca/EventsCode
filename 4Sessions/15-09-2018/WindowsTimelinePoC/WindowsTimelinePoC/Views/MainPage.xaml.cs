using System;

using Windows.UI.Xaml.Controls;

using WindowsTimelinePoC.ViewModels;

namespace WindowsTimelinePoC.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
