using System;

namespace Steamworks
{
	
	[Serializable]
	public struct ISteamNetworkingCustomSignalingRecvContext
	{
		public IntPtr OnConnectRequest(HSteamNetConnection hConn, ref SteamNetworkingIdentity identityPeer)
		{
			return NativeMethods.SteamAPI_ISteamNetworkingCustomSignalingRecvContext_OnConnectRequest(ref this, hConn, ref identityPeer);
		}
	
		public void SendRejectionSignal(ref SteamNetworkingIdentity identityPeer, IntPtr pMsg, int cbMsg)
		{
			NativeMethods.SteamAPI_ISteamNetworkingCustomSignalingRecvContext_SendRejectionSignal(ref this, ref identityPeer, pMsg, cbMsg);
		}
	}
}