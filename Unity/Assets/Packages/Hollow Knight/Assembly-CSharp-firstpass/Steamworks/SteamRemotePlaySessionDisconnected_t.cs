using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	[CallbackIdentity(5702)]
	public struct SteamRemotePlaySessionDisconnected_t
	{
		public const int k_iCallback = 5702;
	
		public RemotePlaySessionID_t m_unSessionID;
	}
}