


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Twitter;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp;
using System.Linq;
using DotNetSpainConference.Config;
using DotNetSpainConference.ViewModels;

namespace DotNetSpainConference.Sections
{
    public class TwitterConfig : SectionConfigBase<TwitterSchema>
    {
		private readonly TwitterDataProvider _dataProvider = new TwitterDataProvider(new TwitterOAuthTokens
        {
			ConsumerKey = "0K2eCSsci2NXHjsx1r48j7adC",
                    ConsumerSecret = "1aZhQwtPeefIPVRV8aJW49RCDmg7LwlAX4JIImJHVRXi45xEA1",
                    AccessToken = "275442106-YlBW4ovgJHta8dzbG3xalg7Mk1hapkJaBwcoeYaY",
                    AccessTokenSecret = "IlCKgTrecAYfxpFtUuO66lQKQLIFLbOY7rgkYhBo3UCPp"
        });

		public override Func<Task<IEnumerable<TwitterSchema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new TwitterDataConfig
                {
                    QueryType = TwitterQueryType.Search,
                    Query = @"#dotNetSpain2016"
                };

                return () => _dataProvider.LoadDataAsync(config, MaxRecords);
            }
        }

        public override ListPageConfig<TwitterSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<TwitterSchema>
                {
                    Title = "Twitter",

					PageTitle = "Twitter",

                    ListNavigationInfo = NavigationInfo.FromPage("TwitterListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.UserName.ToSafeString();
                        viewModel.SubTitle = item.Text.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.UserProfileImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return new NavigationInfo
                        {
                            NavigationType = NavigationType.DeepLink,
                            TargetUri = new Uri(item.Url)
                        };
                    }
                };
            }
        }

        public override DetailPageConfig<TwitterSchema> DetailPage
        {
            get { return null; }
        }
    }
}
