﻿using AppStudio.DataProviders.BingMaps;
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
                return null;
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
                        return new NavigationInfo() { NavigationType = NavigationType.DeepLink, TargetUri = new Uri(string.Format("bingmaps:?collection=point.{0}_{1}_{2}&lvl=18", item.Latitude, item.Longitude, item.Name), UriKind.Absolute) };
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
                return () => Singleton<BingMapsDataProvider>.Instance.LoadDataAsync(config, 100);
            }
        }
    }
}
