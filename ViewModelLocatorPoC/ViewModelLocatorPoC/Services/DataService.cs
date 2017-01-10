using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AppStudio.DataProviders.Rss;

namespace ViewModelLocatorPoC
{
    public class DataService : IDataService
    {
        public async Task<IEnumerable<RssSchema>> GetData()
        {
            RssDataConfig dataConfig = new RssDataConfig()
            {
                Url = new Uri("https://blogs.windows.com/devices/feed/")
            };
            RssDataProvider dataProvider = new RssDataProvider();
            return await dataProvider.LoadDataAsync(dataConfig);
        }
    }
}
