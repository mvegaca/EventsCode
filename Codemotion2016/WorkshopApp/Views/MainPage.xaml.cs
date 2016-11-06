using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using WorkshopApp.ViewModels;

namespace WorkshopApp
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; private set; }        
       
        public MainPage()
        {            
            this.InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadDataAsync();
        }              
    }
}
