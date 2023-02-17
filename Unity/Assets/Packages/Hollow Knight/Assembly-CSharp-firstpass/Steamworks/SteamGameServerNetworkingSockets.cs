using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	
	public static class SteamGameServerNetworkingSockets
	{
		public static HSteamListenSocket CreateListenSocketIP(ref SteamNetworkingIPAddr localAddress, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateListenSocketIP(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), ref localAddress, nOptions, pOptions);
		}
	
		public static HSteamNetConnection ConnectByIPAddress(ref SteamNetworkingIPAddr address, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectByIPAddress(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), ref address, nOptions, pOptions);
		}
	
		public static HSteamListenSocket CreateListenSocketP2P(int nVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateListenSocketP2P(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), nVirtualPort, nOptions, pOptions);
		}
	
		public static HSteamNetConnection ConnectP2P(ref SteamNetworkingIdentity identityRemote, int nVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectP2P(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), ref identityRemote, nVirtualPort, nOptions, pOptions);
		}
	
		public static EResult AcceptConnection(HSteamNetConnection hConn)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_AcceptConnection(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn);
		}
	
		public static bool CloseConnection(HSteamNetConnection hPeer, int nReason, string pszDebug, bool bEnableLinger)
		{
			InteropHelp.TestIfAvailableGameServer();
			using InteropHelp.UTF8StringHandle pszDebug2 = new InteropHelp.UTF8StringHandle(pszDebug);
			return NativeMethods.ISteamNetworkingSockets_CloseConnection(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPeer, nReason, pszDebug2, bEnableLinger);
		}
	
		public static bool CloseListenSocket(HSteamListenSocket hSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_CloseListenSocket(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hSocket);
		}
	
		public static bool SetConnectionUserData(HSteamNetConnection hPeer, long nUserData)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_SetConnectionUserData(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPeer, nUserData);
		}
	
		public static long GetConnectionUserData(HSteamNetConnection hPeer)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetConnectionUserData(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPeer);
		}
	
		public static void SetConnectionName(HSteamNetConnection hPeer, string pszName)
		{
			InteropHelp.TestIfAvailableGameServer();
			using InteropHelp.UTF8StringHandle pszName2 = new InteropHelp.UTF8StringHandle(pszName);
			NativeMethods.ISteamNetworkingSockets_SetConnectionName(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPeer, pszName2);
		}
	
		public static bool GetConnectionName(HSteamNetConnection hPeer, out string pszName, int nMaxLen)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal(nMaxLen);
			bool flag = NativeMethods.ISteamNetworkingSockets_GetConnectionName(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPeer, intPtr, nMaxLen);
			pszName = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}
	
		public static EResult SendMessageToConnection(HSteamNetConnection hConn, IntPtr pData, uint cbData, int nSendFlags, out long pOutMessageNumber)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_SendMessageToConnection(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, pData, cbData, nSendFlags, out pOutMessageNumber);
		}
	
		public static void SendMessages(int nMessages, SteamNetworkingMessage_t[] pMessages, long[] pOutMessageNumberOrResult)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamNetworkingSockets_SendMessages(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), nMessages, pMessages, pOutMessageNumberOrResult);
		}
	
		public static EResult FlushMessagesOnConnection(HSteamNetConnection hConn)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_FlushMessagesOnConnection(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn);
		}
	
		public static int ReceiveMessagesOnConnection(HSteamNetConnection hConn, IntPtr[] ppOutMessages, int nMaxMessages)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_ReceiveMessagesOnConnection(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, ppOutMessages, nMaxMessages);
		}
	
		public static bool GetConnectionInfo(HSteamNetConnection hConn, out SteamNetConnectionInfo_t pInfo)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetConnectionInfo(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, out pInfo);
		}
	
		public static bool GetQuickConnectionStatus(HSteamNetConnection hConn, out SteamNetworkingQuickConnectionStatus pStats)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetQuickConnectionStatus(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, out pStats);
		}
	
		public static int GetDetailedConnectionStatus(HSteamNetConnection hConn, out string pszBuf, int cbBuf)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal(cbBuf);
			int num = NativeMethods.ISteamNetworkingSockets_GetDetailedConnectionStatus(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, intPtr, cbBuf);
			pszBuf = ((num != -1) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return num;
		}
	
		public static bool GetListenSocketAddress(HSteamListenSocket hSocket, out SteamNetworkingIPAddr address)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetListenSocketAddress(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hSocket, out address);
		}
	
		public static bool CreateSocketPair(out HSteamNetConnection pOutConnection1, out HSteamNetConnection pOutConnection2, bool bUseNetworkLoopback, ref SteamNetworkingIdentity pIdentity1, ref SteamNetworkingIdentity pIdentity2)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_CreateSocketPair(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pOutConnection1, out pOutConnection2, bUseNetworkLoopback, ref pIdentity1, ref pIdentity2);
		}
	
		public static bool GetIdentity(out SteamNetworkingIdentity pIdentity)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetIdentity(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pIdentity);
		}
	
		public static ESteamNetworkingAvailability InitAuthentication()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_InitAuthentication(CSteamGameServerAPIContext.GetSteamNetworkingSockets());
		}
	
		public static ESteamNetworkingAvailability GetAuthenticationStatus(out SteamNetAuthenticationStatus_t pDetails)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetAuthenticationStatus(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pDetails);
		}
	
		public static HSteamNetPollGroup CreatePollGroup()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamNetPollGroup)NativeMethods.ISteamNetworkingSockets_CreatePollGroup(CSteamGameServerAPIContext.GetSteamNetworkingSockets());
		}
	
		public static bool DestroyPollGroup(HSteamNetPollGroup hPollGroup)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_DestroyPollGroup(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPollGroup);
		}
	
		public static bool SetConnectionPollGroup(HSteamNetConnection hConn, HSteamNetPollGroup hPollGroup)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_SetConnectionPollGroup(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, hPollGroup);
		}
	
		public static int ReceiveMessagesOnPollGroup(HSteamNetPollGroup hPollGroup, IntPtr[] ppOutMessages, int nMaxMessages)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_ReceiveMessagesOnPollGroup(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPollGroup, ppOutMessages, nMaxMessages);
		}
	
		public static bool ReceivedRelayAuthTicket(IntPtr pvTicket, int cbTicket, out SteamDatagramRelayAuthTicket pOutParsedTicket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_ReceivedRelayAuthTicket(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), pvTicket, cbTicket, out pOutParsedTicket);
		}
	
		public static int FindRelayAuthTicketForServer(ref SteamNetworkingIdentity identityGameServer, int nVirtualPort, out SteamDatagramRelayAuthTicket pOutParsedTicket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_FindRelayAuthTicketForServer(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), ref identityGameServer, nVirtualPort, out pOutParsedTicket);
		}
	
		public static HSteamNetConnection ConnectToHostedDedicatedServer(ref SteamNetworkingIdentity identityTarget, int nVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectToHostedDedicatedServer(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), ref identityTarget, nVirtualPort, nOptions, pOptions);
		}
	
		public static ushort GetHostedDedicatedServerPort()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerPort(CSteamGameServerAPIContext.GetSteamNetworkingSockets());
		}
	
		public static SteamNetworkingPOPID GetHostedDedicatedServerPOPID()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamNetworkingPOPID)NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerPOPID(CSteamGameServerAPIContext.GetSteamNetworkingSockets());
		}
	
		public static EResult GetHostedDedicatedServerAddress(out SteamDatagramHostedAddress pRouting)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerAddress(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pRouting);
		}
	
		public static HSteamListenSocket CreateHostedDedicatedServerListenSocket(int nVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), nVirtualPort, nOptions, pOptions);
		}
	
		public static EResult GetGameCoordinatorServerLogin(out SteamDatagramGameCoordinatorServerLogin pLoginInfo, out int pcbSignedBlob, IntPtr pBlob)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetGameCoordinatorServerLogin(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pLoginInfo, out pcbSignedBlob, pBlob);
		}
	
		public static HSteamNetConnection ConnectP2PCustomSignaling(out ISteamNetworkingConnectionCustomSignaling pSignaling, ref SteamNetworkingIdentity pPeerIdentity, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectP2PCustomSignaling(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pSignaling, ref pPeerIdentity, nOptions, pOptions);
		}
	
		public static bool ReceivedP2PCustomSignal(IntPtr pMsg, int cbMsg, out ISteamNetworkingCustomSignalingRecvContext pContext)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_ReceivedP2PCustomSignal(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), pMsg, cbMsg, out pContext);
		}
	
		public static bool GetCertificateRequest(out int pcbBlob, IntPtr pBlob, out SteamNetworkingErrMsg errMsg)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetCertificateRequest(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pcbBlob, pBlob, out errMsg);
		}
	
		public static bool SetCertificate(IntPtr pCertificate, int cbCertificate, out SteamNetworkingErrMsg errMsg)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_SetCertificate(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), pCertificate, cbCertificate, out errMsg);
		}
	}
}