namespace Steamworks
{
	
	public enum ESteamNetworkingConfigValue
	{
		k_ESteamNetworkingConfig_Invalid = 0,
		k_ESteamNetworkingConfig_FakePacketLoss_Send = 2,
		k_ESteamNetworkingConfig_FakePacketLoss_Recv = 3,
		k_ESteamNetworkingConfig_FakePacketLag_Send = 4,
		k_ESteamNetworkingConfig_FakePacketLag_Recv = 5,
		k_ESteamNetworkingConfig_FakePacketReorder_Send = 6,
		k_ESteamNetworkingConfig_FakePacketReorder_Recv = 7,
		k_ESteamNetworkingConfig_FakePacketReorder_Time = 8,
		k_ESteamNetworkingConfig_FakePacketDup_Send = 26,
		k_ESteamNetworkingConfig_FakePacketDup_Recv = 27,
		k_ESteamNetworkingConfig_FakePacketDup_TimeMax = 28,
		k_ESteamNetworkingConfig_TimeoutInitial = 24,
		k_ESteamNetworkingConfig_TimeoutConnected = 25,
		k_ESteamNetworkingConfig_SendBufferSize = 9,
		k_ESteamNetworkingConfig_SendRateMin = 10,
		k_ESteamNetworkingConfig_SendRateMax = 11,
		k_ESteamNetworkingConfig_NagleTime = 12,
		k_ESteamNetworkingConfig_IP_AllowWithoutAuth = 23,
		k_ESteamNetworkingConfig_MTU_PacketSize = 32,
		k_ESteamNetworkingConfig_MTU_DataSize = 33,
		k_ESteamNetworkingConfig_Unencrypted = 34,
		k_ESteamNetworkingConfig_EnumerateDevVars = 35,
		k_ESteamNetworkingConfig_SDRClient_ConsecutitivePingTimeoutsFailInitial = 19,
		k_ESteamNetworkingConfig_SDRClient_ConsecutitivePingTimeoutsFail = 20,
		k_ESteamNetworkingConfig_SDRClient_MinPingsBeforePingAccurate = 21,
		k_ESteamNetworkingConfig_SDRClient_SingleSocket = 22,
		k_ESteamNetworkingConfig_SDRClient_ForceRelayCluster = 29,
		k_ESteamNetworkingConfig_SDRClient_DebugTicketAddress = 30,
		k_ESteamNetworkingConfig_SDRClient_ForceProxyAddr = 31,
		k_ESteamNetworkingConfig_SDRClient_FakeClusterPing = 36,
		k_ESteamNetworkingConfig_LogLevel_AckRTT = 13,
		k_ESteamNetworkingConfig_LogLevel_PacketDecode = 14,
		k_ESteamNetworkingConfig_LogLevel_Message = 15,
		k_ESteamNetworkingConfig_LogLevel_PacketGaps = 16,
		k_ESteamNetworkingConfig_LogLevel_P2PRendezvous = 17,
		k_ESteamNetworkingConfig_LogLevel_SDRRelayPings = 18,
		k_ESteamNetworkingConfigValue__Force32Bit = int.MaxValue
	}
}