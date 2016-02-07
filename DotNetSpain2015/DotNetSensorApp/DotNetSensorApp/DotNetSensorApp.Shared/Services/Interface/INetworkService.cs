using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSensorApp.Services.Interface
{
    public interface INetworkService
    {
        Task<string> DownloadAsync(Uri service);
        Task<T> DownloadAsync<T>(Uri service);
    }
}
