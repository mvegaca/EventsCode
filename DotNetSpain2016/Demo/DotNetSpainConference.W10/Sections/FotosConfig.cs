


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using Windows.Storage;
using AppStudio.DataProviders.DynamicStorage;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using System.Linq;
using DotNetSpainConference.Config;
using DotNetSpainConference.ViewModels;

namespace DotNetSpainConference.Sections
{
    public class FotosConfig : SectionConfigBase<Fotos1Schema>
    {
	    public override Func<Task<IEnumerable<Fotos1Schema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new DynamicStorageDataConfig
                {
                    Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=1e3f045e-1561-49dd-88b8-2cc5db737212&appId=225510f9-06a2-4235-8597-6f32a74156d7"),
                    AppId = "225510f9-06a2-4235-8597-6f32a74156d7",
                    StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                    DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string
                };

                return () => Singleton<DynamicStorageDataProvider<Fotos1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override ListPageConfig<Fotos1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Fotos1Schema>
                {
                    Title = "Fotos",

					PageTitle = "Fotos",

                    ListNavigationInfo = NavigationInfo.FromPage("FotosListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Description.ToSafeString();
                        viewModel.Description = "";
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Image.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavigationInfo.FromPage("FotosDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<Fotos1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Fotos1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Image.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<Fotos1Schema>>
                {
                };

                return new DetailPageConfig<Fotos1Schema>
                {
                    Title = "Fotos",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
