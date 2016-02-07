using DotNetSensorApp.Services.Base;
using DotNetSensorApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Core;

namespace DotNetSensorApp.ViewModel
{
    public class VMAccelerometer : VMBase
    {
        #region Services
        private readonly AccelerometerService _accelerometerService;
        #endregion

        #region Properties
        private double _accelerometerControlCanvasWidth;
        public double AccelerometerControlCanvasWidth { get { return _accelerometerControlCanvasWidth; } set { Set("AccelerometerControlCanvasWidth", ref _accelerometerControlCanvasWidth, value); } }
        private double _accelerometerControlCanvasHeight;
        public double AccelerometerControlCanvasHeight { get { return _accelerometerControlCanvasHeight; } set { Set("AccelerometerControlCanvasHeight", ref _accelerometerControlCanvasHeight, value); } }
        public double AccelerometerControlCanvasHalfHeight { get { return _accelerometerControlCanvasHeight / 2; } }
        public double AccelerometerControlCanvasHalfWidth { get { return _accelerometerControlCanvasWidth / 2; } }
        private double _accelerometerControlCircleSize;
        public double AccelerometerControlCircleSize { get { return _accelerometerControlCircleSize; } set { Set("AccelerometerControlCircleSize", ref _accelerometerControlCircleSize, value); } }
        public double AccelerometerControlCircleHalfSize { get { return _accelerometerControlCircleSize / 2; } }
        private System.Double _accelerometerControlEllipseLeft;
        public System.Double AccelerometerControlEllipseLeft { get { return _accelerometerControlEllipseLeft; } set { Set("AccelerometerControlEllipseLeft", ref _accelerometerControlEllipseLeft, value); } }
        private System.Double _accelerometerControlEllipseTop;
        public System.Double AccelerometerControlEllipseTop { get { return _accelerometerControlEllipseTop; } set { Set("AccelerometerControlEllipseTop", ref _accelerometerControlEllipseTop, value); } }
        #endregion
        public VMAccelerometer(INavigationService navigationService, AccelerometerService accelerometerService)
            : base(navigationService)
        {
            this._accelerometerService = accelerometerService;
            this._accelerometerService.ReadingChanged += _accelerometerService_ReadingChanged;
            this.AccelerometerControlCanvasWidth = 400;
            this.AccelerometerControlCanvasHeight = 400;
            this.AccelerometerControlCircleSize = 50;
            this.AccelerometerControlEllipseLeft = (this.AccelerometerControlCanvasWidth / 2) - this.AccelerometerControlCircleHalfSize;
            this.AccelerometerControlEllipseTop = (this.AccelerometerControlCanvasHeight / 2) - this.AccelerometerControlCircleHalfSize;
        }

        #region Events
        private async void _accelerometerService_ReadingChanged(object sender, Windows.Devices.Sensors.AccelerometerReadingChangedEventArgs e)
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.Status.Model.AccelerationX = e.Reading.AccelerationX;
                this.Status.Model.AccelerationY = e.Reading.AccelerationY;
                this.Status.Model.AccelerationZ = e.Reading.AccelerationZ;
                UpdateEllipseValues(e);
            });
        }
        #endregion

        #region Methods
        private void UpdateEllipseValues(Windows.Devices.Sensors.AccelerometerReadingChangedEventArgs e)
        {
            double delta = 50;
            var qtdWidth = e.Reading.AccelerationX * delta;
            var qtdHeight = e.Reading.AccelerationY * delta;
            var sumWidth = this.AccelerometerControlEllipseLeft + qtdWidth;
            var sumHeight = this.AccelerometerControlEllipseTop - qtdHeight;
            if (sumWidth > 0 && sumWidth <= this.AccelerometerControlCanvasWidth - this.AccelerometerControlCircleSize)
            {
                this.AccelerometerControlEllipseLeft = sumWidth;
            }
            if (sumHeight > 0 && sumHeight <= this.AccelerometerControlCanvasHeight - this.AccelerometerControlCircleSize)
            {
                this.AccelerometerControlEllipseTop = sumHeight;
            }
        }
        public void Start() { this._accelerometerService.Start(); }
        public void Stop() { this._accelerometerService.Stop(); }
        #endregion
    }
}
