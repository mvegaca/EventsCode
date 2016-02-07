using DotNetSensorApp.Services.Interface;
using DotNetSensorApp.ViewModel.Entities;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace DotNetSensorApp.ViewModel
{
    public class VMBase : SimpleViewModelBase
    {
        #region Services
        public INavigationService _navigationService;
        #endregion

        #region Properties
        public CoreDispatcher dispatcher;
        private bool _isProgressIndicatorVisible;
        public bool IsProgressIndicatorVisible
        {
            get { return _isProgressIndicatorVisible; }
            set { Set("IsProgressIndicatorVisible", ref _isProgressIndicatorVisible, value); }
        }
        private VMStatus _status;
        public VMStatus Status
        {
            get { return _status; }
            set { Set("Status", ref _status, value); }
        }
        #endregion

        #region Constructors
        public VMBase() { }
        public VMBase(INavigationService navigationService)
        {
            this.dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            this.Status = new VMStatus();
            this._navigationService = navigationService;
        }
        #endregion

        #region Methods
        public void SetPage(Page page)
        {
            this._navigationService.SetPage(page);
        }
        #endregion

        #region Commands
        #region NavigateCommand
        private RelayCommand<string> _navigateCommand;
        public RelayCommand<string> NavigateCommand
        {
            get
            {
                return _navigateCommand ?? (_navigateCommand = new RelayCommand<string>(ExecuteNavigateCommand));
            }
        }
        private void ExecuteNavigateCommand(string parameter)
        {
            _navigationService.Navigate(parameter);
        }
        #endregion
        #region GoBackCommand
        private RelayCommand _goBackCommand;
        public RelayCommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new RelayCommand(ExecuteGoBackCommand, CanExecuteGoBackCommand));
            }
        }

        private bool CanExecuteGoBackCommand()
        {
            return _navigationService.CanGoBack();
        }
        private void ExecuteGoBackCommand()
        {
            _navigationService.GoBack();
        }
        #endregion
        #region OpenWebBrowserCommand
        private RelayCommand<string> _openWebBrowserCommand;
        public RelayCommand<string> OpenWebBrowserCommand
        {
            get
            {
                return _openWebBrowserCommand ?? (_openWebBrowserCommand = new RelayCommand<string>(ExecuteOpenWebBrowserCommand));
            }
        }
        private void ExecuteOpenWebBrowserCommand(string parameter)
        {
            //TODO mvegaca change this implementation
            //WebBrowserTask task = new WebBrowserTask();
            //task.Uri = new Uri(parameter, UriKind.Absolute);
            //AnalyticsService.SendEvent(AnalyticsService.EventCategory.Actions, AnalyticsService.EventAction.OpenWebBrowser);
            //task.Show();
        }
        #endregion
        #region OpenMailCommand
        private RelayCommand<string> _openMailCommand;
        public RelayCommand<string> OpenMailCommand
        {
            get
            {
                return _openMailCommand ?? (_openMailCommand = new RelayCommand<string>(ExecuteOpenMailCommand));
            }
        }
        private void ExecuteOpenMailCommand(string parameter)
        {
            //TODO mvegaca change this implementation
            //EmailComposeTask task = new EmailComposeTask();
            //task.To = parameter;
            //AnalyticsService.SendEvent(AnalyticsService.EventCategory.Actions, AnalyticsService.EventAction.SendMail);
            //task.Show();
        }
        #endregion
        #endregion
    }
}
