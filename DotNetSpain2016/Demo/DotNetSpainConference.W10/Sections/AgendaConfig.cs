


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp;
using Windows.ApplicationModel.Appointments;
using System.Linq;
using DotNetSpainConference.Config;
using DotNetSpainConference.ViewModels;

namespace DotNetSpainConference.Sections
{
    public class AgendaConfig : SectionConfigBase<Agenda1Schema, Ponentes1Schema>
    {
		public override Func<Task<IEnumerable<Agenda1Schema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new LocalStorageDataConfig
                {
                    FilePath = "/Assets/Data/Agenda.json"
                };

                return () => Singleton<LocalStorageDataProvider<Agenda1Schema>>.Instance.LoadDataAsync(config, 41);
            }
        }

        public override bool NeedsNetwork
        {
            get
            {
                return false;
            }
        }

        public override ListPageConfig<Agenda1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Agenda1Schema>
                {
                    Title = "Agenda",

					PageTitle = "Agenda",

                    ListNavigationInfo = NavigationInfo.FromPage("AgendaListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
						viewModel.Header = item.Technology.ToSafeString();
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Description.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Image.ToSafeString());
						viewModel.Aside = item.Time.ToString(DateTimeFormat.CardTime);
						viewModel.Footer = item.Speaker.ToSafeString();

						viewModel.GroupBy = item.Technology.SafeType();

						viewModel.OrderBy = item.Time;
                    },
					OrderType = OrderType.Ascending,
                    DetailNavigation = (item) =>
                    {
                        return NavigationInfo.FromPage("AgendaDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<Agenda1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Agenda1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl("");
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<Agenda1Schema>>
                {
                    ActionConfig<Agenda1Schema>.Link("Web", (item) => item.Url.ToSafeString()),
                };

                return new DetailPageConfig<Agenda1Schema>
                {
                    Title = "Agenda",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
		public override RelatedContentConfig<Ponentes1Schema, Agenda1Schema> RelatedContent
		{
			get
			{
				return new RelatedContentConfig<Ponentes1Schema, Agenda1Schema>()
				{
					NeedsNetwork = false,
					LoadDataAsync = async (selected) =>
					{
						var config = new LocalStorageDataConfig
						{
							FilePath = "/Assets/Data/Ponentes.json"
						};
						var result = await Singleton<LocalStorageDataProvider<Ponentes1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
						return result
								.Where(r => r.Name.ToSafeString() == selected.Speaker.ToSafeString())
								.ToList();
					},
					ListPage = new ListPageConfig<Ponentes1Schema>
					{
						Title = "Ponente",

						LayoutBindings = (viewModel, item) =>
						{
							viewModel.Title = item.Name.ToSafeString();
							viewModel.SubTitle = item.Description.ToSafeString();
							viewModel.Description = null;
							viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Image.ToSafeString());
						},
						DetailNavigation = (item) =>
						{
							return NavigationInfo.FromPage("PonentesDetailPage", true);
						}
					}
				};
			}
		}
    }
}
