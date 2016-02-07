using DotNetSensorApp.Services;
using DotNetSensorApp.Services.Base;
using DotNetSensorApp.Services.Interface;
using DotNetSensorApp.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSensorApp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);            
            SimpleIoc.Default.Register<INetworkService, NetworkService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();            
            
            //Services
            
            //ViewModels
            SimpleIoc.Default.Register<VMHome>();
            SimpleIoc.Default.Register<VMAccelerometer>();
            SimpleIoc.Default.Register<VMInclinometer>();
        }

        public VMHome Home { get { return ServiceLocator.Current.GetInstance<VMHome>(); } }
        public VMAccelerometer Accelerometer { get { return ServiceLocator.Current.GetInstance<VMAccelerometer>(); } }
        public VMInclinometer Inclinometer { get { return ServiceLocator.Current.GetInstance<VMInclinometer>(); } }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}