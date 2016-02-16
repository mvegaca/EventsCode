using AppStudio.DataProviders.BingMaps;
using AppStudio.Uwp.Navigation;
using DotNetSpainConference.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSpainConference.Sections
{
    public class MapSection : SectionConfigBase<BingMapsSchema>
    {
        private readonly BingMapsDataProvider _dataProvider = new BingMapsDataProvider();
        public override Func<Task<IEnumerable<BingMapsSchema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new BingMapsDataConfig
                {
                    APIKey = "0xsDiwvZhehrn5Datto8~vromUP5HdosyrxmivUDHVw~AgtGyqVF3WOQxMIhD2EoRHBmuBnUE1-yJ7ky887bNBUjw6QJlsqCtxCM7AIvUYso",
                    Query = "Kinepolis Madrid"
                };
                return () => _dataProvider.LoadDataAsync(config, MaxRecords);
            }
        }
        public override DetailPageConfig<BingMapsSchema> DetailPage
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override ListPageConfig<BingMapsSchema> ListPage
        {
            get
            {
                return new ListPageConfig<BingMapsSchema>
                {
                    Title = "Mapa",

                    PageTitle = "Resultados en mapa",

                    ListNavigationInfo = NavigationInfo.FromPage("MapPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Name;
                        viewModel.SubTitle = string.Format("{0} {1}", item.Latitude, item.Longitude);
                        viewModel.Description = null;
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavigationInfo.FromPage("MapDetailPage");
                    }
                };
            }
        }
    }
}
