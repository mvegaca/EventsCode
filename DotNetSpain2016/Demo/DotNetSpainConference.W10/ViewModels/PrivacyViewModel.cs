using System;
using AppStudio.Uwp;

namespace DotNetSpainConference.ViewModels
{
    public class PrivacyViewModel : ObservableBase
    {
        public Uri Url
        {
            get
            {
                return new Uri(UrlText, UriKind.RelativeOrAbsolute);
            }
        }
        public string UrlText
        {
            get
            {
                return "https://www.desarrollaconmicrosoft.com/Dotnetspain2016";
            }
        }
    }
}

