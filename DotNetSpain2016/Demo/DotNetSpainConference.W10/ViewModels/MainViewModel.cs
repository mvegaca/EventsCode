using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using AppStudio.DataProviders;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AppStudio.DataProviders.Twitter;
using AppStudio.DataProviders.Rss;
using AppStudio.DataProviders.Menu;
using AppStudio.DataProviders.Html;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.DynamicStorage;
using DotNetSpainConference.Sections;


namespace DotNetSpainConference.ViewModels
{
    public class MainViewModel : ObservableBase
    {
        public MainViewModel(int visibleItems) : base()
        {
            PageTitle = "dotNet Spain Conference";
            Agenda = ListViewModel.CreateNew(Singleton<AgendaConfig>.Instance, visibleItems);
            Ponentes = ListViewModel.CreateNew(Singleton<PonentesConfig>.Instance, visibleItems);
            Fotos = ListViewModel.CreateNew(Singleton<FotosConfig>.Instance, visibleItems);
            Twitter = ListViewModel.CreateNew(Singleton<TwitterConfig>.Instance, visibleItems);
            VideosChannel9 = ListViewModel.CreateNew(Singleton<VideosChannel9Config>.Instance, visibleItems);
            MasInfo = ListViewModel.CreateNew(Singleton<MasInfoConfig>.Instance);
            Patrocinadores = ListViewModel.CreateNew(Singleton<PatrocinadoresConfig>.Instance);
            Colaboradores = ListViewModel.CreateNew(Singleton<ColaboradoresConfig>.Instance);

            Actions = new List<ActionInfo>();

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = new RelayCommand(Refresh),
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

        public string PageTitle { get; set; }
        public ListViewModel Agenda { get; private set; }
        public ListViewModel Ponentes { get; private set; }
        public ListViewModel Fotos { get; private set; }
        public ListViewModel Twitter { get; private set; }
        public ListViewModel VideosChannel9 { get; private set; }
        public ListViewModel MasInfo { get; private set; }
        public ListViewModel Patrocinadores { get; private set; }
        public ListViewModel Colaboradores { get; private set; }

        public RelayCommand<INavigable> SectionHeaderClickCommand
        {
            get
            {
                return new RelayCommand<INavigable>(item =>
                    {
                        NavigationService.NavigateTo(item);
                    });
            }
        }

        public DateTime? LastUpdated
        {
            get
            {
                return GetViewModels().Select(vm => vm.LastUpdated)
                            .OrderByDescending(d => d).FirstOrDefault();
            }
        }

        public List<ActionInfo> Actions { get; private set; }

        public bool HasActions
        {
            get
            {
                return Actions != null && Actions.Count > 0;
            }
        }

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private async void Refresh()
        {
            var refreshDataTasks = GetViewModels()
                                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

            await Task.WhenAll(refreshDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Agenda;
            yield return Ponentes;
            yield return Fotos;
            yield return Twitter;
            yield return VideosChannel9;
            yield return MasInfo;
            yield return Patrocinadores;
            yield return Colaboradores;
        }
    }
}
