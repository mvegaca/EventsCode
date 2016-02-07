


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
    public class DestacadoEnLaAgendaConfig : SectionConfigBase<DestacadoEnLaAgenda1Schema>
    {
	    public override Func<Task<IEnumerable<DestacadoEnLaAgenda1Schema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new DynamicStorageDataConfig
                {
                    Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=ba4ae74a-371b-4bf9-af01-8984e06b859f&appId=225510f9-06a2-4235-8597-6f32a74156d7"),
                    AppId = "225510f9-06a2-4235-8597-6f32a74156d7",
                    StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                    DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string
                };

                return () => Singleton<DynamicStorageDataProvider<DestacadoEnLaAgenda1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override ListPageConfig<DestacadoEnLaAgenda1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<DestacadoEnLaAgenda1Schema>
                {
                    Title = "Destacado en la Agenda",

					PageTitle = "Destacado en la Agenda",

                    ListNavigationInfo = NavigationInfo.FromPage("DestacadoEnLaAgendaListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Description.ToSafeString();
                        viewModel.Description = "";
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Image.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavigationInfo.FromPage("DestacadoEnLaAgendaDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<DestacadoEnLaAgenda1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, DestacadoEnLaAgenda1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Destacado en Agenda";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Image.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<DestacadoEnLaAgenda1Schema>>
                {
                    ActionConfig<DestacadoEnLaAgenda1Schema>.Link("Web", (item) => item.Url.ToSafeString()),
                };

                return new DetailPageConfig<DestacadoEnLaAgenda1Schema>
                {
                    Title = "Destacado en la Agenda",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
