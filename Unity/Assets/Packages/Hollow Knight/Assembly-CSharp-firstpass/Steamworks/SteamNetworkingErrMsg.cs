using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[Serializable]
	public struct SteamNetworkingErrMsg
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		public byte[] m_SteamNetworkingErrMsg;
	}
}