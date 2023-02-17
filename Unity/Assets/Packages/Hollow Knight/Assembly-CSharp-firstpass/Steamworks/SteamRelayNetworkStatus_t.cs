using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[CallbackIdentity(1281)]
	public struct SteamRelayNetworkStatus_t
	{
		public const int k_iCallback = 1281;
	
		public ESteamNetworkingAvailability m_eAvail;
	
		public int m_bPingMeasurementInProgress;
	
		public ESteamNetworkingAvailability m_eAvailNetworkConfig;
	
		public ESteamNetworkingAvailability m_eAvailAnyRelay;
	
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_debugMsg;
	}
}