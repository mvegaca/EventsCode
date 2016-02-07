


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
    public class PonentesConfig : SectionConfigBase<Ponentes1Schema, Agenda1Schema>
    {
	    public override Func<Task<IEnumerable<Ponentes1Schema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new DynamicStorageDataConfig
                {
                    Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=ea627736-97e3-4856-b77e-ac18d4ccc572&appId=225510f9-06a2-4235-8597-6f32a74156d7"),
                    AppId = "225510f9-06a2-4235-8597-6f32a74156d7",
                    StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                    DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string
                };

                return () => Singleton<DynamicStorageDataProvider<Ponentes1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override ListPageConfig<Ponentes1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Ponentes1Schema>
                {
                    Title = "Ponentes",

					PageTitle = "Ponentes",

                    ListNavigationInfo = NavigationInfo.FromPage("PonentesListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Name.ToSafeString();
                        viewModel.SubTitle = item.Description.ToSafeString();
                        viewModel.Description = "";
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Image.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavigationInfo.FromPage("PonentesDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<Ponentes1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Ponentes1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Name.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Image.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<Ponentes1Schema>>
                {
                    ActionConfig<Ponentes1Schema>.Link("Follow", (item) => item.Url.ToSafeString()),
                };

                return new DetailPageConfig<Ponentes1Schema>
                {
                    Title = "Ponentes",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
		public override RelatedContentConfig<Agenda1Schema, Ponentes1Schema> RelatedContent
		{
			get
			{
				return new RelatedContentConfig<Agenda1Schema, Ponentes1Schema>()
				{
					LoadDataAsync = async (selected) =>
					{
						var config = new DynamicStorageDataConfig
						{
							Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=3502d929-886c-4480-98d0-d2e9c73d159a&appId=225510f9-06a2-4235-8597-6f32a74156d7"),
							AppId = "225510f9-06a2-4235-8597-6f32a74156d7",
							StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
							DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string
						};

						var result = await Singleton<DynamicStorageDataProvider<Agenda1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
						return result
								.Where(r => r.Speaker == selected.Name)
								.ToList();
					},
					ListPage = new ListPageConfig<Agenda1Schema>
					{
						Title = "Charlas",

						LayoutBindings = (viewModel, item) =>
						{
							viewModel.Title = item.Title.ToSafeString();
							viewModel.SubTitle = item.Description.ToSafeString();
							viewModel.Description = null;
							viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Image.ToSafeString());
						},
						DetailNavigation = (item) =>
						{
							return NavigationInfo.FromPage("AgendaDetailPage", true);
						}
					}
				};
			}
		}
    }
}
