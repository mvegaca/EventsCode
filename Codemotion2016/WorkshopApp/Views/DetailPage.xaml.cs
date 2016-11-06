using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using AppStudio.DataProviders.WordPress;

using WorkshopApp.ViewModels;

namespace WorkshopApp
{
    public sealed partial class DetailPage : Page
    {
        public DetailViewModel ViewModel { get; private set; }
        public DetailPage()
        {
            this.InitializeComponent();
            ViewModel = new DetailViewModel();
            DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Item = e.Parameter as WordPressSchema;
        }
    }
}
