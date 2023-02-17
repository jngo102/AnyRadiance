using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	[CallbackIdentity(1222)]
	public struct SteamNetAuthenticationStatus_t
	{
		public const int k_iCallback = 1222;
	
		public ESteamNetworkingAvailability m_eAvail;
	
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_debugMsg;
	}
}