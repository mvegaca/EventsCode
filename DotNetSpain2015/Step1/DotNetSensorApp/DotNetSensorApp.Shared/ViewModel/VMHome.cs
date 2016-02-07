using DotNetSensorApp.Services.Base;
using DotNetSensorApp.Services.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using DotNetSensorApp.Model;

namespace DotNetSensorApp.ViewModel
{
    public class VMHome : VMBase
    {
        #region Services

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public VMHome(INavigationService navigationService)
            : base(navigationService)
        {
        }
        #endregion

        #region Methods
        private void CreateDesignData()
        {
        }
        private async Task GetData(bool force)
        {
            this.IsProgressIndicatorVisible = true;

            this.IsProgressIndicatorVisible = false;
        }
        #endregion

        #region Commands
        #region GetStatusCommand
        private RelayCommand _getStatusCommand;
        public RelayCommand GetStatusCommand
        {
            get
            {
                return _getStatusCommand ?? (_getStatusCommand = new RelayCommand(ExecuteGetStatusCommand));
            }
        }
        private async void ExecuteGetStatusCommand()
        {
            await GetData(false);
        }
        #endregion
        #endregion
    }
}
