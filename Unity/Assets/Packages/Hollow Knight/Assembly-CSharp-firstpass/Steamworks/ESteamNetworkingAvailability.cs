namespace Steamworks
{
	
	public enum ESteamNetworkingAvailability
	{
		k_ESteamNetworkingAvailability_CannotTry = -102,
		k_ESteamNetworkingAvailability_Failed = -101,
		k_ESteamNetworkingAvailability_Previously = -100,
		k_ESteamNetworkingAvailability_Retrying = -10,
		k_ESteamNetworkingAvailability_NeverTried = 1,
		k_ESteamNetworkingAvailability_Waiting = 2,
		k_ESteamNetworkingAvailability_Attempting = 3,
		k_ESteamNetworkingAvailability_Current = 100,
		k_ESteamNetworkingAvailability_Unknown = 0,
		k_ESteamNetworkingAvailability__Force32bit = int.MaxValue
	}
}