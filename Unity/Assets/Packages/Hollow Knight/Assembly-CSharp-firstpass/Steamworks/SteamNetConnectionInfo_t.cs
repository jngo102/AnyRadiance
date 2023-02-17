using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionInfo_t
	{
		public SteamNetworkingIdentity m_identityRemote;
	
		public long m_nUserData;
	
		public HSteamListenSocket m_hListenSocket;
	
		public SteamNetworkingIPAddr m_addrRemote;
	
		public ushort m__pad1;
	
		public SteamNetworkingPOPID m_idPOPRemote;
	
		public SteamNetworkingPOPID m_idPOPRelay;
	
		public ESteamNetworkingConnectionState m_eState;
	
		public int m_eEndReason;
	
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_szEndDebug;
	
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_szConnectionDescription;
	
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		public uint[] reserved;
	}
}