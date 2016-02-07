using DotNetSensorApp.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;

namespace DotNetSensorApp.Services
{
    public class NavigationService : NavigationServiceBase
    {
        public NavigationService() : base()
        {
            HardwareButtons.BackPressed += BackPressed;
        }
        private void BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if(base.CanGoBack())
            {
                base.GoBack();
                e.Handled = true;
            }                
        }
    }
}
