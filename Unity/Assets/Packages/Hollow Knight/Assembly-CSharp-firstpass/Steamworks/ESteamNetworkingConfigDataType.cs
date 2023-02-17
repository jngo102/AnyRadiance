namespace Steamworks
{
	
	public enum ESteamNetworkingConfigDataType
	{
		k_ESteamNetworkingConfig_Int32 = 1,
		k_ESteamNetworkingConfig_Int64 = 2,
		k_ESteamNetworkingConfig_Float = 3,
		k_ESteamNetworkingConfig_String = 4,
		k_ESteamNetworkingConfig_FunctionPtr = 5,
		k_ESteamNetworkingConfigDataType__Force32Bit = int.MaxValue
	}
}