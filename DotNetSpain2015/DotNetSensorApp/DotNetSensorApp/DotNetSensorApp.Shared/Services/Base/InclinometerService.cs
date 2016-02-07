using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Sensors;

namespace DotNetSensorApp.Services.Base
{
    public class InclinometerService : BaseService
    {
        #region Properties
        private readonly Inclinometer _sensor;
        #endregion

        #region Constructors
        public InclinometerService()
            : base()
        {
            _sensor = Inclinometer.GetDefault();
        }
        #endregion

        #region Events
        public event EventHandler<InclinometerReadingChangedEventArgs> ReadingChanged;
        private void InclinometerReadingChanged(Inclinometer sender, InclinometerReadingChangedEventArgs args)
        {
            if (this.ReadingChanged != null) ReadingChanged(_sensor, args);
        }
        #endregion

        #region Methods
        internal void Start()
        {
            if (_sensor != null)
            {
                this._sensor.ReadingChanged += InclinometerReadingChanged;
            }
        }

        internal void Stop()
        {
            if (_sensor != null)
            {
                this._sensor.ReadingChanged -= InclinometerReadingChanged;
            }
        }
        #endregion
    }
}
