using AppStudio.DataProviders.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace AppStudio.DataProviders.BingMaps
{
    public class BingMapsDataProvider : DataProviderBase<BingMapsDataConfig, BingMapsSchema>
    {
        private static void Assertions(BingMapsDataConfig config)
        {
            if (config == null)
            {
                throw new ConfigNullException();
            }
            if (string.IsNullOrEmpty(config.DataSourceId))
            {
                throw new ConfigNullException();
            }
            if (string.IsNullOrEmpty(config.Latitude))
            {
                throw new ConfigNullException();
            }
            if (string.IsNullOrEmpty(config.Longitude))
            {
                throw new ConfigNullException();
            }
            if (string.IsNullOrEmpty(config.Radius))
            {
                throw new ConfigNullException();
            }            
            if (string.IsNullOrEmpty(config.APIKey))
            {
                throw new ConfigNullException();
            }
        }

        protected async override Task<IEnumerable<TSchema>> GetDataAsync<TSchema>(BingMapsDataConfig config, int maxRecords, IParser<TSchema> parser)
        {
            Assertions(config);
            var requestedUri = new Uri(string.Format("http://spatial.virtualearth.net/REST/v1/data/{0}?spatialFilter=nearby({1},{2},{3})&$format=json&$top={4}&key={5}", config.DataSourceId, config.Latitude, config.Longitude, config.Radius, maxRecords, config.APIKey));
            string result = await DownloadAsync(requestedUri);
            return parser.Parse(result);
        }

        internal async Task<string> DownloadAsync(Uri requestedUri)
        {
            var filter = new HttpBaseProtocolFilter();
            filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
            var httpClient = new HttpClient(filter);
            HttpResponseMessage response = await httpClient.GetAsync(requestedUri);
            return await response.Content.ReadAsStringAsync();
        }

        protected override IParser<BingMapsSchema> GetDefaultParserInternal(BingMapsDataConfig config)
        {
            return new BingMapsParser();
        }

        protected override void ValidateConfig(BingMapsDataConfig config)
        {
            
        }
    }
}
