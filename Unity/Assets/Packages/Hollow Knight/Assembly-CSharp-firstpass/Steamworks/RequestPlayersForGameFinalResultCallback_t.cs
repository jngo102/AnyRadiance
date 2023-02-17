using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	[CallbackIdentity(5213)]
	public struct RequestPlayersForGameFinalResultCallback_t
	{
		public const int k_iCallback = 5213;
	
		public EResult m_eResult;
	
		public ulong m_ullSearchID;
	
		public ulong m_ullUniqueGameID;
	}
}