using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace DotNetSensorApp.Services.Interface
{
    public interface INavigationService
    {
        Dictionary<string, Type> RoutingTable { get; set; }
        bool Navigate(string route);
        bool Navigate(string route, string parameter);
        void GoBack();
        bool CanGoBack();
        void SetPage(Page page);
    }
}
