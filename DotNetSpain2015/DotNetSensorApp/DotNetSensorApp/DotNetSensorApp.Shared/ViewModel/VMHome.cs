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
        private readonly LocationService _locationService;
        private readonly WeatherService _weatherService;
        private readonly NetworkStatusService _networkStatusService;
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
        public VMHome(INavigationService navigationService, LocationService locationService, WeatherService weatherService, NetworkStatusService networkStatusService)
            : base(navigationService)
        {
            this._locationService = locationService;
            this._weatherService = weatherService;
            this._locationService.StatusChanged += LocationServiceStatusChanged;
            this._locationService.PositionChanged += LocationServicePositionChanged;
            this._networkStatusService = networkStatusService;
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
                this.Status.Model.Roaming = _networkStatusService.Roaming;
                this.Status.Model.OverDataLimit = _networkStatusService.OverDataLimit;
                this.Status.Model.ApproachingDataLimit = _networkStatusService.ApproachingDataLimit;
                this.Status.Model.NetworkCostType = _networkStatusService.NetworkCostType;
                this.Status.Model.NetworkConnectivityLevel = _networkStatusService.NetworkConnectivityLevel;
                this.Status.Model.DomainConnectivityLevel = _networkStatusService.DomainConnectivityLevel;


                this.Status.Model.DataLimitInMegabytes = _networkStatusService.DataLimitInMegabytes;
                this.Status.Model.MegabytesUsed = _networkStatusService.MegabytesUsed;
                this.Status.Model.InboundBitsPerSecond = _networkStatusService.InboundBitsPerSecond;
                this.Status.Model.MaxTransferSizeInMegabytes = _networkStatusService.MaxTransferSizeInMegabytes;
                this.Status.Model.NextBillingCycle = _networkStatusService.NextBillingCycle;
                this.Status.Model.OutboundBitsPerSecond = _networkStatusService.OutboundBitsPerSecond;
                this.Status.Model.NetworkAuthenticationType = _networkStatusService.NetworkAuthenticationType;
                this.Status.Model.NetworkEncryptionType = _networkStatusService.NetworkEncryptionType;
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
            this.Status.Model.PositionStatus = _locationService.PositionStatus;
            this.Status.Model.Latitude = _locationService.Position.Latitude;
            this.Status.Model.Longitude = _locationService.Position.Longitude;
        }
        private async void GetWeatherData(double latitude, double longitude)
        {
            this.Weather = await _weatherService.GetWeatherAsync(latitude, longitude);
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
