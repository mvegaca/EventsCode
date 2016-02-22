using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppStudio.Uwp.Navigation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;

namespace DotNetSpainConference.Navigation
{
    public class AppNavigation
    {
        private NavigationNode _active;

        static AppNavigation()
        {

        }

        public NavigationNode Active
        {
            get
            {
                return _active;
            }
            set
            {
                if (_active != null)
                {
                    _active.IsSelected = false;
                }
                _active = value;
                if (_active != null)
                {
                    _active.IsSelected = true;
                }
            }
        }


        public ObservableCollection<NavigationNode> Nodes { get; private set; }

        public void LoadNavigation()
        {
            Nodes = new ObservableCollection<NavigationNode>();
		    var resourceLoader = new ResourceLoader();
			AddNode(Nodes, "Home", "\ue10f", string.Empty, "HomePage", true, @"dotNet Spain Conference");
			AddNode(Nodes, "Agenda", "\ue163", string.Empty, "AgendaListPage", true);			
			AddNode(Nodes, "Ponentes", "\ue125", string.Empty, "PonentesListPage", true);			
			AddNode(Nodes, "Fotos", "\ue114", string.Empty, "FotosListPage", true);			
			AddNode(Nodes, "Twitter", "\ue134", string.Empty, "TwitterListPage", true);			
			AddNode(Nodes, "Videos Channel 9", "\ue116", string.Empty, "VideosChannel9ListPage", true);
            AddNode(Nodes, "Mapa", "\ue707", string.Empty, "MapListPage", true);
            AddNode(Nodes, "FAQ", "\ue160", string.Empty, "FAQListPage", true);			
			AddNode(Nodes, resourceLoader.GetString("NavigationPaneAbout"), "\ue11b", string.Empty, "AboutPage");
			AddNode(Nodes, resourceLoader.GetString("NavigationPanePrivacy"), "\ue1f7", string.Empty, string.Empty, true, string.Empty, "https://www.desarrollaconmicrosoft.com/Dotnetspain2016");            
        }

		private void AddNode(ObservableCollection<NavigationNode> nodes, string label, string fontIcon, string image, string pageName, bool isVisible = true, string title = null, string deepLinkUrl = null, bool isSelected = false)
        {
            if (nodes != null && isVisible)
            {
                var node = new ItemNavigationNode
                {
                    Title = title,
                    Label = label,
                    FontIcon = fontIcon,
                    Image = image,
                    IsSelected = isSelected,
                    IsVisible = isVisible,
                    NavigationInfo = NavigationInfo.FromPage(pageName)
                };
                if (!string.IsNullOrEmpty(deepLinkUrl))
                {
                    node.NavigationInfo = new NavigationInfo()
                    {
                        NavigationType = NavigationType.DeepLink,
                        TargetUri = new Uri(deepLinkUrl, UriKind.Absolute)
                    };
                }
                nodes.Add(node);
            }            
        }

        public NavigationNode FindPage(Type pageType)
        {
            return GetAllItemNodes(Nodes).FirstOrDefault(n => n.NavigationInfo.NavigationType == NavigationType.Page && n.NavigationInfo.TargetPage == pageType.Name);
        }

        private IEnumerable<ItemNavigationNode> GetAllItemNodes(IEnumerable<NavigationNode> nodes)
        {
            foreach (var node in nodes)
            {
                if (node is ItemNavigationNode)
                {
                    yield return node as ItemNavigationNode;
                }
                else if(node is GroupNavigationNode)
                {
                    var gNode = node as GroupNavigationNode;

                    foreach (var innerNode in GetAllItemNodes(gNode.Nodes))
                    {
                        yield return innerNode;
                    }
                }
            }
        }
    }
}
