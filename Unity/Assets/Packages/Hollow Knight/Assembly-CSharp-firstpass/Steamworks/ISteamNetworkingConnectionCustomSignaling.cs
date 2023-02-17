using System;

namespace Steamworks
{
	
	[Serializable]
	public struct ISteamNetworkingConnectionCustomSignaling
	{
		public bool SendSignal(HSteamNetConnection hConn, ref SteamNetConnectionInfo_t info, IntPtr pMsg, int cbMsg)
		{
			return NativeMethods.SteamAPI_ISteamNetworkingConnectionCustomSignaling_SendSignal(ref this, hConn, ref info, pMsg, cbMsg);
		}
	
		public void Release()
		{
			NativeMethods.SteamAPI_ISteamNetworkingConnectionCustomSignaling_Release(ref this);
		}
	}
}