using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DotNetSensorApp.ViewModel
{
    public abstract class SimpleViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        public bool Set<T>(string propertyName, ref T field, T newValue)
        {
            field = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
