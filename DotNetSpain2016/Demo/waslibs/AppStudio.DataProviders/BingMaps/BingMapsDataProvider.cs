using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            if (config.Query == null)
            {
                throw new ConfigParameterNullException("Query");
            }
        }

        protected async override Task<IEnumerable<TSchema>> GetDataAsync<TSchema>(BingMapsDataConfig config, int maxRecords, IParser<TSchema> parser)
        {
            Assertions(config);
            var settings = new HttpRequestSettings()
            {
                RequestedUri = new Uri(string.Format("http://dev.virtualearth.net/REST/v1/Locations?query={0}&key={1}", WebUtility.UrlEncode(config.Query), config.APIKey))
            };

            HttpRequestResult result = await HttpRequest.DownloadAsync(settings);
            if (result.Success)
            {
                return parser.Parse(result.Result);
            }

            throw new RequestFailedException(result.StatusCode, result.Result);
        }

        protected override IParser<BingMapsSchema> GetDefaultParserInternal(BingMapsDataConfig config)
        {
            return new BingMapsParser();
        }

        protected override void ValidateConfig(BingMapsDataConfig config)
        {
            throw new NotImplementedException();
        }
    }
}
