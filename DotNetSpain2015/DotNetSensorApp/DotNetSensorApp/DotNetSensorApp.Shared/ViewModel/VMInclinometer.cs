using DotNetSensorApp.Services.Base;
using DotNetSensorApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Core;

namespace DotNetSensorApp.ViewModel
{
    public class VMInclinometer : VMBase
    {
        #region Services
        private readonly InclinometerService _inclinometerService;
        #endregion

        #region Properties
        private double _rollEllipseLeft;
        public double RollEllipseLeft { get { return _rollEllipseLeft; } set { Set("RollEllipseLeft", ref _rollEllipseLeft, value); } }

        private double _pitchEllipseTop;
        public double PitchEllipseTop { get { return _pitchEllipseTop; } set { Set("PitchEllipseTop", ref _pitchEllipseTop, value); } }
        private double _yawEllipse;
        public double YawEllipse { get { return _yawEllipse; } set { Set("YawEllipse", ref _yawEllipse, value); } }
        #endregion

        #region Constructors
        public VMInclinometer(INavigationService navigationService, InclinometerService inclinometerService)
            : base(navigationService)
        {

            this._inclinometerService = inclinometerService;
            this._inclinometerService.ReadingChanged += InclinometerServiceReadingChanged;
            this.RollEllipseLeft = (400 / 2) - (30 / 2);
            this.PitchEllipseTop = (400 / 2) - (30 / 2);
            this.YawEllipse = (400 / 2) - (30 / 2);
        }
        #endregion

        #region Methods
        private async void InclinometerServiceReadingChanged(object sender, Windows.Devices.Sensors.InclinometerReadingChangedEventArgs e)
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.Status.Model.PitchDegrees = e.Reading.PitchDegrees;
                this.Status.Model.RollDegrees = e.Reading.RollDegrees;
                this.Status.Model.YawDegrees = e.Reading.YawDegrees;
                UpdateRollControl(e.Reading.RollDegrees);
                UpdatePitchControl(e.Reading.PitchDegrees);
                UpdateYawControl(e.Reading.YawDegrees);
            });
        } 
        public void Start() { this._inclinometerService.Start(); }
        public void Stop() { this._inclinometerService.Stop(); }
        private void UpdateRollControl(float rollDegrees)
        {
            int minDegree = -90;
            int maxDegree = 90;
            int minControl = 0;
            int maxControl = 400 - 30;
            this.RollEllipseLeft = ConvertRange(minDegree, maxDegree, minControl, maxControl, (Int32)rollDegrees);
        }
        private void UpdatePitchControl(float pitchDegrees)
        {
            int minDegree = -180;
            int maxDegree = 180;
            int minControl = 0;
            int maxControl = 400 - 30;
            this.PitchEllipseTop = ConvertRange(minDegree, maxDegree, minControl, maxControl, (Int32)pitchDegrees);
        }
        private void UpdateYawControl(float yawDegrees)
        {
            int minDegree = 0;
            int maxDegree = 360;
            int minControl = 0;
            int maxControl = 400 - 30;
            this.YawEllipse = ConvertRange(minDegree, maxDegree, minControl, maxControl, (Int32)yawDegrees);
        }
        public int ConvertRange(int originalStart, int originalEnd, int newStart, int newEnd, int value)
        {
            double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
            return (int)(newStart + ((value - originalStart) * scale));
        }
        #endregion
    }
}
