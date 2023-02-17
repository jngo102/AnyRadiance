using System;

namespace Steamworks
{
	
	[Serializable]
	public struct SteamNetworkingMessage_t
	{
		public IntPtr m_pData;
	
		public int m_cbSize;
	
		public HSteamNetConnection m_conn;
	
		public SteamNetworkingIdentity m_identityPeer;
	
		public long m_nConnUserData;
	
		public SteamNetworkingMicroseconds m_usecTimeReceived;
	
		public long m_nMessageNumber;
	
		internal IntPtr m_pfnFreeData;
	
		internal IntPtr m_pfnRelease;
	
		public int m_nChannel;
	
		public int m_nFlags;
	
		public long m_nUserData;
	
		public void Release()
		{
			NativeMethods.SteamAPI_SteamNetworkingMessage_t_Release(m_pfnRelease);
		}
	}
}