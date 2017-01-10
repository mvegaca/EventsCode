using System.Threading.Tasks;
using System.Collections.Generic;

using AppStudio.DataProviders.Rss;

namespace ViewModelLocatorPoC
{
    public interface IDataService
    {
        Task<IEnumerable<RssSchema>> GetData();
    }
}
