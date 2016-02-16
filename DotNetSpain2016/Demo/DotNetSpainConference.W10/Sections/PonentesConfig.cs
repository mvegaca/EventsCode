


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp;
using System.Linq;
using DotNetSpainConference.Config;
using DotNetSpainConference.ViewModels;

namespace DotNetSpainConference.Sections
{
    public class PonentesConfig : SectionConfigBase<Ponentes1Schema, Agenda1Schema>
    {
        public override int MaxRecords
        {
            get
            {
                return 50;
            }
        }
        public override Func<Task<IEnumerable<Ponentes1Schema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new LocalStorageDataConfig
                {
                    FilePath = "/Assets/Data/Ponentes.json"
                };

                return () => Singleton<LocalStorageDataProvider<Ponentes1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override bool NeedsNetwork
        {
            get
            {
                return false;
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
					NeedsNetwork = false,
					LoadDataAsync = async (selected) =>
					{
						var config = new LocalStorageDataConfig
						{
							FilePath = "/Assets/Data/Agenda.json"
						};
						var result = await Singleton<LocalStorageDataProvider<Agenda1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
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
