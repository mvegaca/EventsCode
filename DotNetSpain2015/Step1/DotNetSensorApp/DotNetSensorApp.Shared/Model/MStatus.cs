using DotNetSensorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Networking.Connectivity;

namespace DotNetSensorApp.Model
{
    [DataContract]
    public class MStatus : SimpleViewModelBase
    {
        #region Properties
        #region GlobalProperties
        private DateTime _creationTime;
        [DataMember]
        public DateTime CreationTime
        {
            get { return _creationTime; }
            set { Set("CreationTime", ref _creationTime, value); }
        }
        #endregion
        #region LocationInfo
        private double? _altitude;
        [DataMember]
        public double? Altitude
        {
            get { return _altitude; }
            set { Set("Altitude", ref _altitude, value); }
        }

        private double? _latitude;
        [DataMember]
        public double? Latitude
        {
            get { return _latitude; }
            set { Set("Latitude", ref _latitude, value); }
        }

        private double? _longitude;
        [DataMember]
        public double? Longitude
        {
            get { return _longitude; }
            set { Set("Longitude", ref _longitude, value); }
        }

        private double? _speed;
        [DataMember]
        public double? Speed
        {
            get { return _speed; }
            set { Set("Speed", ref _speed, value); }
        }

        private string _positionSource;
        [DataMember]
        public string PositionSource
        {
            get { return _positionSource; }
            set { Set("PositionSource", ref _positionSource, value); }
        }

        private double? _horizontalDilutionOfPrecision;
        [DataMember]
        public double? HorizontalDilutionOfPrecision
        {
            get { return _horizontalDilutionOfPrecision; }
            set { Set("HorizontalDilutionOfPrecision", ref _horizontalDilutionOfPrecision, value); }
        }

        private double? _positionDilutionOfPrecision;
        [DataMember]
        public double? PositionDilutionOfPrecision
        {
            get { return _positionDilutionOfPrecision; }
            set { Set("PositionDilutionOfPrecision", ref _positionDilutionOfPrecision, value); }
        }

        private double? _verticalDilutionOfPrecision;
        [DataMember]
        public double? VerticalDilutionOfPrecision
        {
            get { return _verticalDilutionOfPrecision; }
            set { Set("VerticalDilutionOfPrecision", ref _verticalDilutionOfPrecision, value); }
        }
        private double? _heading;
        [DataMember]
        public double? Heading
        {
            get { return _heading; }
            set { Set("Heading", ref _heading, value); }
        }
        private double? _accuracy;
        [DataMember]
        public double? Accuracy
        {
            get { return _accuracy; }
            set { Set("Accuracy", ref _accuracy, value); }
        }
        private double? _altitudeAccuracy;
        [DataMember]
        public double? AltitudeAccuracy
        {
            get { return _altitudeAccuracy; }
            set { Set("AltitudeAccuracy", ref _altitudeAccuracy, value); }
        }
        private string _city;
        [DataMember]
        public string City
        {
            get { return _city; }
            set { Set("City", ref _city, value); }
        }
        private string _state;
        [DataMember]
        public string State
        {
            get { return _state; }
            set { Set("State", ref _state, value); }
        }
        private string _country;
        [DataMember]
        public string Country
        {
            get { return _country; }
            set { Set("Country", ref _country, value); }
        }
        private string _postalCode;
        [DataMember]
        public string PostalCode
        {
            get { return _postalCode; }
            set { Set("PostalCode", ref _postalCode, value); }
        }
        private string _positionStatus;
        [DataMember]
        public string PositionStatus
        {
            get { return _positionStatus; }
            set { Set("PositionStatus", ref _positionStatus, value); }
        }
        private bool? _isDeviceMoving;
        [DataMember]
        public bool? IsDeviceMoving
        {
            get { return _isDeviceMoving; }
            set { Set("IsDeviceMoving", ref _isDeviceMoving, value); }
        }

        #endregion
        #region Accelerometer
        private bool? _isMoving;
        [DataMember]
        public bool? IsMoving
        {
            get { return _isMoving; }
            set { Set("IsMoving", ref _isMoving, value); }
        }
        private double _accelerationX;
        [DataMember]
        public double AccelerationX { get { return _accelerationX; } set { Set("AccelerationX", ref _accelerationX, value); } }
        private double _accelerationY;
        [DataMember]
        public double AccelerationY { get { return _accelerationY; } set { Set("AccelerationY", ref _accelerationY, value); } }
        private double _accelerationZ;
        [DataMember]
        public double AccelerationZ { get { return _accelerationZ; } set { Set("AccelerationZ", ref _accelerationZ, value); } }
        #endregion
        #region Inclinometer
        private double _pitchDegrees;
        [DataMember]
        public double PitchDegrees { get { return _pitchDegrees; } set { Set("PitchDegrees", ref _pitchDegrees, value); } }
        private double _rollDegrees;
        [DataMember]
        public double RollDegrees { get { return _rollDegrees; } set { Set("RollDegrees", ref _rollDegrees, value); } }
        private double _yawDegrees;
        [DataMember]
        public double YawDegrees { get { return _yawDegrees; } set { Set("YawDegrees", ref _yawDegrees, value); } }
        #endregion
        #region Network
        private bool _roaming;
        [DataMember]
        public bool Roaming { get { return _roaming; } set { Set("Roaming", ref _roaming, value); } }

        private NetworkConnectivityLevel _networkConnectivityLevel;
        [DataMember]
        public NetworkConnectivityLevel NetworkConnectivityLevel { get { return _networkConnectivityLevel; } set { Set("NetworkConnectivityLevel", ref _networkConnectivityLevel, value); } }
        private DomainConnectivityLevel _domainConnectivityLevel;
        [DataMember]
        public DomainConnectivityLevel DomainConnectivityLevel { get { return _domainConnectivityLevel; } set { Set("DomainConnectivityLevel", ref _domainConnectivityLevel, value); } }
        private NetworkCostType _networkCostType;
        [DataMember]
        public NetworkCostType NetworkCostType { get { return _networkCostType; } set { Set("NetworkCostType", ref _networkCostType, value); } }
        private bool _approachingDataLimit;
        [DataMember]
        public bool ApproachingDataLimit { get { return _approachingDataLimit; } set { Set("ApproachingDataLimit", ref _approachingDataLimit, value); } }
        private bool _overDataLimit;
        [DataMember]
        public bool OverDataLimit { get { return _overDataLimit; } set { Set("OverDataLimit", ref _overDataLimit, value); } }

        private uint? _dataLimitInMegabytes;
        [DataMember]
        public uint? DataLimitInMegabytes { get { return _dataLimitInMegabytes; } set { Set("DataLimitInMegabytes", ref _dataLimitInMegabytes, value); } }
        //private uint _megabytesUsed;
        //[DataMember]
        //public uint MegabytesUsed { get { return _megabytesUsed; } set { Set("MegabytesUsed", ref _megabytesUsed, value); } }
        private ulong? _inboundBitsPerSecond;
        [DataMember]
        public ulong? InboundBitsPerSecond { get { return _inboundBitsPerSecond; } set { Set("InboundBitsPerSecond", ref _inboundBitsPerSecond, value); } }
        private uint? _maxTransferSizeInMegabytes;
        [DataMember]
        public uint? MaxTransferSizeInMegabytes { get { return _maxTransferSizeInMegabytes; } set { Set("MaxTransferSizeInMegabytes", ref _maxTransferSizeInMegabytes, value); } }
        private DateTimeOffset? _nextBillingCycle;
        [DataMember]
        public DateTimeOffset? NextBillingCycle { get { return _nextBillingCycle; } set { Set("NextBillingCycle", ref _nextBillingCycle, value); } }
        private ulong? _outboundBitsPerSecond;
        [DataMember]
        public ulong? OutboundBitsPerSecond { get { return _outboundBitsPerSecond; } set { Set("OutboundBitsPerSecond", ref _outboundBitsPerSecond, value); } }
        private NetworkAuthenticationType _networkAuthenticationType;
        [DataMember]
        public NetworkAuthenticationType NetworkAuthenticationType { get { return _networkAuthenticationType; } set { Set("NetworkAuthenticationType", ref _networkAuthenticationType, value); } }
        private NetworkEncryptionType _networkEncryptionType;
        [DataMember]
        public NetworkEncryptionType NetworkEncryptionType { get { return _networkEncryptionType; } set { Set("NetworkEncryptionType", ref _networkEncryptionType, value); } }
        #endregion
        #endregion

        #region Constructors
        public MStatus() { this.CreationTime = DateTime.Now; }
        #endregion
    }
}
