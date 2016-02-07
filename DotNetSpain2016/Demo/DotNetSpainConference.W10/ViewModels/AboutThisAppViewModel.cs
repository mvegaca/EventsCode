using System;
using AppStudio.Uwp;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.Resources;

namespace DotNetSpainConference.ViewModels
{
    public class AboutThisAppViewModel : ObservableBase
    {
		private ResourceLoader _resourceLoader;
        private ResourceLoader ResourceLoader
        {
            get
            {
                if (_resourceLoader == null)
                {
                    _resourceLoader = new ResourceLoader();
                }
                return _resourceLoader;
            }
        }

        public string PageTitle
        {
            get
            {
                return ResourceLoader.GetString("NavigationPaneAbout");
            }
        }

        public string Publisher
        {
            get
            {
                return "mvegaca";
            }
        }

        public string AppVersion
        {
            get
            {
                return string.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
            }
        }

        public string AboutText
        {
            get
            {
                return "App patrocinada por Microsoft para el evento dotNet Spain Conference del 1 de Mar" +
    "zo de 2016 en Madrid. Información del evento, charlas y speakers, Más Informacio" +
    "n en https://www.desarrollaconmicrosoft.com/Dotnetspain2016 y en Twitter @mvegac" +
    "a";
            }
        }
		
        public string AppName
        {
            get
            {
                return "dotNet Spain Conference";
            }
        }

		public string PrivacyUrl
        {
            get
            {
                return "https://www.desarrollaconmicrosoft.com/Dotnetspain2016";
            }
        }

		public BitmapImage AppLogo
        {
            get
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/ApplicationLogo.png"));
            }
        }
    }
}

