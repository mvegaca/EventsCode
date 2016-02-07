using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Sensors;

namespace DotNetSensorApp.Services.Base
{
    public class AccelerometerService : BaseService
    {

        #region Properties
        private readonly Accelerometer _sensor;
        #endregion

        #region Constructor
        public AccelerometerService()
        {
            this._sensor = Accelerometer.GetDefault();
        }
        #endregion

        #region Events
        public event EventHandler<AccelerometerReadingChangedEventArgs> ReadingChanged;
        private void _sensor_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            if (this.ReadingChanged != null) ReadingChanged(_sensor, args);
        }
        #endregion

        #region Methods
        internal void Start()
        {
            if (this._sensor != null) this._sensor.ReadingChanged += _sensor_ReadingChanged;
        }
        internal void Stop()
        {
            if (this._sensor != null) this._sensor.ReadingChanged -= _sensor_ReadingChanged;
        }
        #endregion
    }
}
