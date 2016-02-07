using DotNetSensorApp.Model;
using DotNetSensorApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSensorApp.Services.Base
{
    public class WeatherService : BaseService
    {
        #region Services
        private INetworkService _networkService;
        #endregion

        #region Constructors
        public WeatherService(INetworkService networkService)
            : base()
        {
            this._networkService = networkService;
        }
        #endregion

        #region Methods
        public async Task<MWeather> GetWeatherAsync(double latitude, double longitude)
        {
            string service = string.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}", latitude, longitude);
            Uri apiUri = new Uri(service, UriKind.Absolute);
            return await _networkService.DownloadAsync<MWeather>(apiUri);
        }
        #endregion
    }
}
