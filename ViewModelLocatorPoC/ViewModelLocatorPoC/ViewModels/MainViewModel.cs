using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ViewModelLocatorPoC
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        #region ImageSource
        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set { Set(ref _imageSource, value); }
        }
        #endregion        

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ImageSource = "http://icons.iconarchive.com/icons/dakirby309/simply-styled/256/Microsoft-Visual-Studio-icon.png";
        }

        #region NavigationCommand
        private ICommand _navigationCommand;
        public ICommand NavigationCommand
        {
            get
            {
                return _navigationCommand ?? (_navigationCommand = new RelayCommand<string>((page) =>
                {
                    _navigationService.NavigateTo(page);
                }));
            }
        }
        #endregion
    }
}
