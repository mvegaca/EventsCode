using DotNetSensorApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace DotNetSensorApp.Services.Base
{
    public class LocationService : BaseService
    {
        #region Events
        public event EventHandler<BasicGeoposition> PositionChanged;
        public event EventHandler<string> StatusChanged;
        #endregion

        #region Properties
        private readonly Geolocator Geolocator;
        private BasicGeoposition _position;
        public BasicGeoposition Position
        {
            get { return _position; }
            private set { Set("Position", ref _position, value); }
        }
        private string _positionStatus;
        public string PositionStatus
        {
            get { return _positionStatus; }
            private set { Set("PositionStatus", ref _positionStatus, value); }
        }
        #endregion

        #region Constructors
        public LocationService()
        {
            Geolocator = new Geolocator();
            Geolocator.DesiredAccuracy = PositionAccuracy.High;
            Geolocator.DesiredAccuracyInMeters = 1;
            Geolocator.MovementThreshold = 1;
            Geolocator.PositionChanged += geolocatorPositionChanged;
            Geolocator.StatusChanged += geolocator_StatusChanged;
        }
        #endregion

        #region Methods
        public async Task<BasicGeoposition> GetLocationAsync(bool force)
        {
            if (force)
            {
                var result = await Geolocator.GetGeopositionAsync();
                this.Position = result.Coordinate.Point.Position;
            }            
            return this.Position;
        }
        private void geolocatorPositionChanged(Windows.Devices.Geolocation.Geolocator sender, PositionChangedEventArgs args)
        {
            this.Position = args.Position.Coordinate.Point.Position;
            if (PositionChanged != null) PositionChanged(this.Geolocator, this.Position);
        }
        private void geolocator_StatusChanged(Windows.Devices.Geolocation.Geolocator sender, StatusChangedEventArgs args)
        {
            this.PositionStatus = args.Status.ToString();
            if (StatusChanged != null) StatusChanged(this.Geolocator, args.Status.ToString());
        }
        #endregion
    }
}
