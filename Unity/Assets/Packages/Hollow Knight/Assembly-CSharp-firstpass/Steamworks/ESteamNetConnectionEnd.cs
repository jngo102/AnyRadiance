namespace Steamworks
{
	
	public enum ESteamNetConnectionEnd
	{
		k_ESteamNetConnectionEnd_Invalid = 0,
		k_ESteamNetConnectionEnd_App_Min = 1000,
		k_ESteamNetConnectionEnd_App_Generic = 1000,
		k_ESteamNetConnectionEnd_App_Max = 1999,
		k_ESteamNetConnectionEnd_AppException_Min = 2000,
		k_ESteamNetConnectionEnd_AppException_Generic = 2000,
		k_ESteamNetConnectionEnd_AppException_Max = 2999,
		k_ESteamNetConnectionEnd_Local_Min = 3000,
		k_ESteamNetConnectionEnd_Local_OfflineMode = 3001,
		k_ESteamNetConnectionEnd_Local_ManyRelayConnectivity = 3002,
		k_ESteamNetConnectionEnd_Local_HostedServerPrimaryRelay = 3003,
		k_ESteamNetConnectionEnd_Local_NetworkConfig = 3004,
		k_ESteamNetConnectionEnd_Local_Rights = 3005,
		k_ESteamNetConnectionEnd_Local_Max = 3999,
		k_ESteamNetConnectionEnd_Remote_Min = 4000,
		k_ESteamNetConnectionEnd_Remote_Timeout = 4001,
		k_ESteamNetConnectionEnd_Remote_BadCrypt = 4002,
		k_ESteamNetConnectionEnd_Remote_BadCert = 4003,
		k_ESteamNetConnectionEnd_Remote_NotLoggedIn = 4004,
		k_ESteamNetConnectionEnd_Remote_NotRunningApp = 4005,
		k_ESteamNetConnectionEnd_Remote_BadProtocolVersion = 4006,
		k_ESteamNetConnectionEnd_Remote_Max = 4999,
		k_ESteamNetConnectionEnd_Misc_Min = 5000,
		k_ESteamNetConnectionEnd_Misc_Generic = 5001,
		k_ESteamNetConnectionEnd_Misc_InternalError = 5002,
		k_ESteamNetConnectionEnd_Misc_Timeout = 5003,
		k_ESteamNetConnectionEnd_Misc_RelayConnectivity = 5004,
		k_ESteamNetConnectionEnd_Misc_SteamConnectivity = 5005,
		k_ESteamNetConnectionEnd_Misc_NoRelaySessionsToClient = 5006,
		k_ESteamNetConnectionEnd_Misc_Max = 5999,
		k_ESteamNetConnectionEnd__Force32Bit = int.MaxValue
	}
}