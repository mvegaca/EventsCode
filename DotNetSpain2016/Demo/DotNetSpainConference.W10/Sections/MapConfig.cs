using AppStudio.DataProviders.BingMaps;
using AppStudio.DataProviders.Core;
using AppStudio.Uwp;
using AppStudio.Uwp.Navigation;
using DotNetSpainConference.Config;
using DotNetSpainConference.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSpainConference.Sections
{
    public class MapConfig : SectionConfigBase<BingMapsSchema>
    {
        public override DetailPageConfig<BingMapsSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, BingMapsSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.DisplayName.ToSafeString();
                    viewModel.Title = item.Name.ToSafeString();
                    viewModel.Description = string.Format("{0}. {1} {2}. Teléfono {3}", item.AddressLine, item.Locality, item.AdminDistrict, item.Phone);
                    viewModel.Content = null;
                });
                var actions = new List<ActionConfig<BingMapsSchema>>
                {
                    ActionConfig<BingMapsSchema>.Phone("Llamar", (item) => item.Phone),
                    ActionConfig<BingMapsSchema>.Address("Ir", (item) => string.Format("{0}_{1}_{2}", item.Latitude, item.Longitude, item.Name))
                };
                return new DetailPageConfig<BingMapsSchema>()
                {
                    Title = "Cerca de DotNet Spain Conference",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }

        public override ListPageConfig<BingMapsSchema> ListPage
        {
            get
            {
                return new ListPageConfig<BingMapsSchema>
                {
                    Title = "Mapa",

                    PageTitle = "Mapa",

                    ListNavigationInfo = NavigationInfo.FromPage("MapListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        System.Diagnostics.Debug.WriteLine(item.EntityTypeID);
                        viewModel.Title = item.Name;
                        viewModel.SubTitle = string.Format("{0} {1}. {2} {3}. Teléfono {4}", item.EntityTypeID, item.AddressLine, item.Locality, item.AdminDistrict, item.Phone);
                        viewModel.Description = "";
                        viewModel.ImageUrl = "";
                        viewModel.Content = string.Format("{0}*{1}*{2}.png", item.Latitude, item.Longitude, item.EntityTypeID);
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavigationInfo.FromPage("MapDetailPage", true);
                    }
                };
            }
        }

        public override Func<Task<IEnumerable<BingMapsSchema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new BingMapsDataConfig()
                {
                    APIKey = "0xsDiwvZhehrn5Datto8~vromUP5HdosyrxmivUDHVw~AgtGyqVF3WOQxMIhD2EoRHBmuBnUE1-yJ7ky887bNBUjw6QJlsqCtxCM7AIvUYso",
                    DataSourceId = "c2ae584bbccc4916a0acf75d1e6947b4/NavteqEU/NavteqPOIs",
                    Latitude = "40.3937115",
                    Longitude = "-3.7964247",
                    Radius = "5"
                };
                return () => Singleton<BingMapsDataProvider>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }
    }
}
