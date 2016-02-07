using DotNetSensorApp.Services.Base;
using DotNetSensorApp.Services.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using DotNetSensorApp.Model;

namespace DotNetSensorApp.ViewModel
{
    public class VMHome : VMBase
    {
        #region Services

        #endregion

        #region Properties
        private MWeather _weather;
        public MWeather Weather
        {
            get { return _weather; }
            set { Set("Weather", ref _weather, value); }
        }
        #endregion

        #region Constructors
        public VMHome(INavigationService navigationService)
            : base(navigationService)
        {
        }

        private async void LocationServicePositionChanged(object sender, Windows.Devices.Geolocation.BasicGeoposition position)
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.Status.Model.Latitude = position.Latitude;
                this.Status.Model.Longitude = position.Longitude;
                GetWeatherData(position.Latitude, position.Longitude);
            });
        }

        private async void LocationServiceStatusChanged(object sender, string positionStatus)
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.Status.Model.PositionStatus = positionStatus;
            });
        }
        #endregion

        #region Methods
        private void CreateDesignData()
        {
        }
        private async Task GetData(bool force)
        {
            this.IsProgressIndicatorVisible = true;
            this.GetDeviceData(force);
            this.GetLocationData(force);
            this.GetNetworkStatusData(force);
            this.IsProgressIndicatorVisible = false;
        }

        private void GetNetworkStatusData(bool force)
        {
            try
            {
            }
            catch (Exception ex)
            {
            }

        }
        private void GetDeviceData(bool force)
        {
        }
        private void GetLocationData(bool force)
        {
        }
        private async void GetWeatherData(double latitude, double longitude)
        {
        }
        #endregion

        #region Commands
        #region GetStatusCommand
        private RelayCommand _getStatusCommand;
        public RelayCommand GetStatusCommand
        {
            get
            {
                return _getStatusCommand ?? (_getStatusCommand = new RelayCommand(ExecuteGetStatusCommand));
            }
        }
        private async void ExecuteGetStatusCommand()
        {
            await GetData(false);
        }
        #endregion
        #endregion
    }
}
