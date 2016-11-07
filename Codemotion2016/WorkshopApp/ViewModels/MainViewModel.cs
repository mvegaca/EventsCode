using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using AppStudio.DataProviders.WordPress;
using Windows.UI.Xaml;

namespace WorkshopApp.ViewModels
{
    public class MainViewModel : ObservableBase
    {
        private WordPressDataConfig _config;
        private WordPressDataProvider _dataProvider;

        private ObservableCollection<WordPressSchema> _items;
        public ObservableCollection<WordPressSchema> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private Visibility _loadingGridVisibility;
        public Visibility LoadingGridVisibility
        {
            get { return _loadingGridVisibility; }
            set { SetProperty(ref _loadingGridVisibility, value); }
        }

        public MainViewModel()
        {
            string wordPressQuery = "en.blog.wordpress.com";
            _config = new WordPressDataConfig()
            {
                QueryType = WordPressQueryType.Posts,
                Query = wordPressQuery
            };
            _dataProvider = new WordPressDataProvider();
            LoadingGridVisibility = Visibility.Visible;
        }

        public async Task LoadDataAsync()
        {
            if (Items == null)
            {
                LoadingGridVisibility = Visibility.Visible;
                var freshData = await _dataProvider.LoadDataAsync(_config);
                Items = new ObservableCollection<WordPressSchema>(freshData);
                LoadingGridVisibility = Visibility.Collapsed;
            }
        }

        public ICommand LoadMoreCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    LoadingGridVisibility = Visibility.Visible;
                    var freshData = await _dataProvider.LoadMoreDataAsync();
                    foreach (var item in freshData)
                    {
                        Items.Add(item);
                    }
                    LoadingGridVisibility = Visibility.Collapsed;
                });
            }
        }

        public ICommand GoToDetailCommand
        {
            get
            {
                return new RelayCommand<WordPressSchema>((item) =>
                {
                    NavigationService.NavigateToPage("DetailPage", item);
                });
            }
        }
    }
}
