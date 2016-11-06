using AppStudio.Uwp;
using AppStudio.DataProviders.WordPress;

namespace WorkshopApp.ViewModels
{
    public class DetailViewModel : ObservableBase
    {
        private WordPressSchema _item;
        public WordPressSchema Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }
    }
}
