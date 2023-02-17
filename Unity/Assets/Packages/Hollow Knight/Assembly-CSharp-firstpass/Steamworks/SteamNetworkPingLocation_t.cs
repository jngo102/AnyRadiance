using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetworkPingLocation_t
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public byte[] m_data;
	}
}