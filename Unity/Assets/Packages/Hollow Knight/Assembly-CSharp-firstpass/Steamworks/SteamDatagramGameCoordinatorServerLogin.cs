using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamDatagramGameCoordinatorServerLogin
	{
		public SteamNetworkingIdentity m_identity;
	
		public SteamDatagramHostedAddress m_routing;
	
		public AppId_t m_nAppID;
	
		public RTime32 m_rtime;
	
		public int m_cbAppData;
	
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2048)]
		public byte[] m_appData;
	}
}