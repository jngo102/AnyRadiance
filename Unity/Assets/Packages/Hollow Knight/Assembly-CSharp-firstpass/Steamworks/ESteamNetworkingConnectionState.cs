namespace Steamworks
{
	
	public enum ESteamNetworkingConnectionState
	{
		k_ESteamNetworkingConnectionState_None = 0,
		k_ESteamNetworkingConnectionState_Connecting = 1,
		k_ESteamNetworkingConnectionState_FindingRoute = 2,
		k_ESteamNetworkingConnectionState_Connected = 3,
		k_ESteamNetworkingConnectionState_ClosedByPeer = 4,
		k_ESteamNetworkingConnectionState_ProblemDetectedLocally = 5,
		k_ESteamNetworkingConnectionState_FinWait = -1,
		k_ESteamNetworkingConnectionState_Linger = -2,
		k_ESteamNetworkingConnectionState_Dead = -3,
		k_ESteamNetworkingConnectionState__Force32Bit = int.MaxValue
	}
}