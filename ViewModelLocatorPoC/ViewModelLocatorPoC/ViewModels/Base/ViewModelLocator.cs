using Microsoft.Practices.ServiceLocation;

using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace ViewModelLocatorPoC
{
    public class ViewModelLocator
    {
        public const string SecondViewKey = "SecondView";

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //Services registration
            var navigationService = new NavigationService();
            navigationService.Configure(SecondViewKey, typeof(SecondView));
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<IDataService, DataService>();
            
            //ViewModel registration
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SecondViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public SecondViewModel Second => ServiceLocator.Current.GetInstance<SecondViewModel>();
    }
}
