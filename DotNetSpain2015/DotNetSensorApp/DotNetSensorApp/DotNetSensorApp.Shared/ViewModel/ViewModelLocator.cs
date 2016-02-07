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
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);            
            SimpleIoc.Default.Register<LocationService>();
            SimpleIoc.Default.Register<INetworkService, NetworkService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<WeatherService>();
            SimpleIoc.Default.Register<InclinometerService>();
            SimpleIoc.Default.Register<AccelerometerService>();
            SimpleIoc.Default.Register<NetworkStatusService>();
            
            //ViewModels
            SimpleIoc.Default.Register<VMHome>();
            SimpleIoc.Default.Register<VMAccelerometer>();
            SimpleIoc.Default.Register<VMInclinometer>();
        }

        public VMHome Home { get { return ServiceLocator.Current.GetInstance<VMHome>(); } }
        public VMAccelerometer Accelerometer { get { return ServiceLocator.Current.GetInstance<VMAccelerometer>(); } }
        public VMInclinometer Inclinometer { get { return ServiceLocator.Current.GetInstance<VMInclinometer>(); } }

        public static void Cleanup()
        {
        }
    }
}