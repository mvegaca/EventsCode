using DotNetSensorApp.Services.Interface;
using DotNetSensorApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace DotNetSensorApp.Services.Base
{
    public abstract class NavigationServiceBase : INavigationService
    {

        private Page Page { get; set; }
        private Frame Frame { get { return this.Page.Frame; } }
        private Dictionary<string, Type> _routingTable;
        private Dictionary<string, Type> RoutingTable { get { return _routingTable; } set { this._routingTable = value; } }

        private Type GetType(string route)
        {
            Type outValue;
            bool success = RoutingTable.TryGetValue(route, out outValue);
            return (success) ? outValue : typeof(VHome);
        }
        public NavigationServiceBase()
        {
            this.RoutingTable = new Dictionary<string, Type>();
            this.RoutingTable.Add("VHome", typeof(VHome));
        }
        public bool Navigate(string route)
        {
            return this.Frame.Navigate(GetType(route));
        }
        public bool Navigate(string route, string parameter)
        {
            return this.Frame.Navigate(GetType(route), parameter);
        }
        public void GoBack()
        {
            if (CanGoBack()) this.Frame.GoBack();
        }
        public bool CanGoBack()
        {
            return (this.Frame != null && this.Frame.CanGoBack);
        }
        public void SetPage(Page page)
        {
            this.Page = page;
        }

        Dictionary<string, Type> INavigationService.RoutingTable
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
