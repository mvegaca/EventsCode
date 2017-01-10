using System.Windows.Input;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

using AppStudio.DataProviders.Rss;

namespace ViewModelLocatorPoC
{
    public class SecondViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IDataService _dataService;

        #region Items
        private ObservableCollection<RssSchema> _items;
        public ObservableCollection<RssSchema> Items
        {
            get { return _items; }
            set { Set(ref _items, value); }
        }
        #endregion        

        public SecondViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }

        #region GoBackCommand
        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new RelayCommand(() =>
                {
                    _navigationService.GoBack();
                }));
            }
        }
        #endregion

        public async void LoadData()
        {
            var data = await _dataService.GetData();
            this.Items = new ObservableCollection<RssSchema>(data);
        }
    }
}
