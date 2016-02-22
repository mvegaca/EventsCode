using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DotNetSpainConference.Sections;
namespace DotNetSpainConference.ViewModels
{
    public class SearchViewModel : ObservableBase
    {
        public SearchViewModel() : base()
        {
			PageTitle = "dotNet Spain Conference";
            Agenda = ListViewModel.CreateNew(Singleton<AgendaConfig>.Instance);
            Ponentes = ListViewModel.CreateNew(Singleton<PonentesConfig>.Instance);
            Fotos = ListViewModel.CreateNew(Singleton<FotosConfig>.Instance);
            Twitter = ListViewModel.CreateNew(Singleton<TwitterConfig>.Instance);
            VideosChannel9 = ListViewModel.CreateNew(Singleton<VideosChannel9Config>.Instance);
            FAQ = ListViewModel.CreateNew(Singleton<FAQConfig>.Instance);
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel Agenda { get; private set; }
        public ListViewModel Ponentes { get; private set; }
        public ListViewModel Fotos { get; private set; }
        public ListViewModel Twitter { get; private set; }
        public ListViewModel VideosChannel9 { get; private set; }
        public ListViewModel FAQ { get; private set; }
        
		public string PageTitle { get; set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Agenda;
            yield return Ponentes;
            yield return Fotos;
            yield return Twitter;
            yield return VideosChannel9;
            yield return FAQ;
        }
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
