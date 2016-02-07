using DotNetSensorApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSensorApp.Services.Base
{
    public abstract class NetworkServiceBase : INetworkService
    {
        public async Task<string> DownloadAsync(Uri service)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(service);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        public async Task<T> DownloadAsync<T>(Uri service)
        {
            string content = await DownloadAsync(service);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
        }
    }
}
