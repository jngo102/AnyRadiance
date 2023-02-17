using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	[CallbackIdentity(5214)]
	public struct SubmitPlayerResultResultCallback_t
	{
		public const int k_iCallback = 5214;
	
		public EResult m_eResult;
	
		public ulong ullUniqueGameID;
	
		public CSteamID steamIDPlayer;
	}
}