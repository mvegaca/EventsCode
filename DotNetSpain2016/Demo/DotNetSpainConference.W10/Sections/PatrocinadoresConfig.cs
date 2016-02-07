


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.Menu;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp;
using System.Linq;
using DotNetSpainConference.Config;
using DotNetSpainConference.ViewModels;

namespace DotNetSpainConference.Sections
{
    public class PatrocinadoresConfig : SectionConfigBase<MenuSchema>
    {
	    public override Func<Task<IEnumerable<MenuSchema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new LocalStorageDataConfig
                {
                    FilePath = "/Assets/Data/Patrocinadores.json"
                };

                return () => Singleton<LocalStorageDataProvider<MenuSchema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override bool NeedsNetwork
        {
            get
            {
                return false;
            }
        }

        public override ListPageConfig<MenuSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<MenuSchema>
                {
                    Title = "Patrocinadores",

					PageTitle = "Patrocinadores",

                    ListNavigationInfo = NavigationInfo.FromPage("PatrocinadoresListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title;						
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Icon);
                    },
                    DetailNavigation = (item) =>
                    {
                        return item.ToNavigationInfo();
                    }
                };
            }
        }

        public override DetailPageConfig<MenuSchema> DetailPage
        {
            get { return null; }
        }
    }
}
