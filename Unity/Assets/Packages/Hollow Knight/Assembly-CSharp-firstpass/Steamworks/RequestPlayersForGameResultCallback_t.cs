using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	[CallbackIdentity(5212)]
	public struct RequestPlayersForGameResultCallback_t
	{
		public const int k_iCallback = 5212;
	
		public EResult m_eResult;
	
		public ulong m_ullSearchID;
	
		public CSteamID m_SteamIDPlayerFound;
	
		public CSteamID m_SteamIDLobby;
	
		public PlayerAcceptState_t m_ePlayerAcceptState;
	
		public int m_nPlayerIndex;
	
		public int m_nTotalPlayersFound;
	
		public int m_nTotalPlayersAcceptedGame;
	
		public int m_nSuggestedTeamIndex;
	
		public ulong m_ullUniqueGameID;
	}
}