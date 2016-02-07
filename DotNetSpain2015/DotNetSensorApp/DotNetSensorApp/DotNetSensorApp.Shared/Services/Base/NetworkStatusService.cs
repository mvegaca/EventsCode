using System;
using System.Collections.Generic;
using System.Text;
using Windows.Networking.Connectivity;

namespace DotNetSensorApp.Services.Base
{
    public class NetworkStatusService : BaseService
    {
        #region Properties
        private const string NotAvailableString = "N/A";
        private ConnectionProfile internetConnectionProfile { get { return NetworkInformation.GetInternetConnectionProfile(); } }
        private ConnectionCost connectionCost { get { return (internetConnectionProfile != null) ? internetConnectionProfile.GetConnectionCost() : null; } }
        private DataPlanStatus dataPlanStatus { get { return (internetConnectionProfile != null) ? internetConnectionProfile.GetDataPlanStatus() : null; } }
        private NetworkSecuritySettings netSecuritySettings { get { return (internetConnectionProfile != null) ? internetConnectionProfile.NetworkSecuritySettings : null; } }

        public NetworkConnectivityLevel NetworkConnectivityLevel { get { return (internetConnectionProfile != null) ? internetConnectionProfile.GetNetworkConnectivityLevel() : NetworkConnectivityLevel.None; } }
        public DomainConnectivityLevel DomainConnectivityLevel { get { return (internetConnectionProfile != null) ? internetConnectionProfile.GetDomainConnectivityLevel() : DomainConnectivityLevel.None; } }
        public NetworkCostType NetworkCostType { get { return (connectionCost != null) ? connectionCost.NetworkCostType : NetworkCostType.Unknown; } }
        public bool Roaming { get { return (connectionCost != null) ? connectionCost.Roaming : false; } }
        public bool ApproachingDataLimit { get { return (connectionCost != null) ? connectionCost.ApproachingDataLimit : false; } }
        public bool OverDataLimit { get { return (connectionCost != null) ? connectionCost.OverDataLimit : false; } }
        public uint? DataLimitInMegabytes { get { return (dataPlanStatus != null) ? dataPlanStatus.DataLimitInMegabytes : null; } }
        public uint MegabytesUsed { get { return (dataPlanStatus != null && dataPlanStatus.DataPlanUsage != null) ? dataPlanStatus.DataPlanUsage.MegabytesUsed : default(uint); } }
        public ulong? InboundBitsPerSecond { get { return (dataPlanStatus != null) ? dataPlanStatus.InboundBitsPerSecond : null; } }
        public uint? MaxTransferSizeInMegabytes { get { return (dataPlanStatus != null) ? dataPlanStatus.MaxTransferSizeInMegabytes : null; } }
        public DateTimeOffset? NextBillingCycle { get { return (dataPlanStatus != null) ? dataPlanStatus.NextBillingCycle : null; } }
        public ulong? OutboundBitsPerSecond { get { return (dataPlanStatus != null) ? dataPlanStatus.OutboundBitsPerSecond : null; } }
        public NetworkAuthenticationType NetworkAuthenticationType { get { return (netSecuritySettings != null) ? netSecuritySettings.NetworkAuthenticationType : NetworkAuthenticationType.Unknown; } }
        public NetworkEncryptionType NetworkEncryptionType { get { return (netSecuritySettings != null) ? netSecuritySettings.NetworkEncryptionType : NetworkEncryptionType.Unknown; } }
        #endregion

    }
}
