using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	
	public static class SteamNetworkingSockets
	{
		public static HSteamListenSocket CreateListenSocketIP(ref SteamNetworkingIPAddr localAddress, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateListenSocketIP(CSteamAPIContext.GetSteamNetworkingSockets(), ref localAddress, nOptions, pOptions);
		}
	
		public static HSteamNetConnection ConnectByIPAddress(ref SteamNetworkingIPAddr address, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectByIPAddress(CSteamAPIContext.GetSteamNetworkingSockets(), ref address, nOptions, pOptions);
		}
	
		public static HSteamListenSocket CreateListenSocketP2P(int nVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateListenSocketP2P(CSteamAPIContext.GetSteamNetworkingSockets(), nVirtualPort, nOptions, pOptions);
		}
	
		public static HSteamNetConnection ConnectP2P(ref SteamNetworkingIdentity identityRemote, int nVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectP2P(CSteamAPIContext.GetSteamNetworkingSockets(), ref identityRemote, nVirtualPort, nOptions, pOptions);
		}
	
		public static EResult AcceptConnection(HSteamNetConnection hConn)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_AcceptConnection(CSteamAPIContext.GetSteamNetworkingSockets(), hConn);
		}
	
		public static bool CloseConnection(HSteamNetConnection hPeer, int nReason, string pszDebug, bool bEnableLinger)
		{
			InteropHelp.TestIfAvailableClient();
			using InteropHelp.UTF8StringHandle pszDebug2 = new InteropHelp.UTF8StringHandle(pszDebug);
			return NativeMethods.ISteamNetworkingSockets_CloseConnection(CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, nReason, pszDebug2, bEnableLinger);
		}
	
		public static bool CloseListenSocket(HSteamListenSocket hSocket)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_CloseListenSocket(CSteamAPIContext.GetSteamNetworkingSockets(), hSocket);
		}
	
		public static bool SetConnectionUserData(HSteamNetConnection hPeer, long nUserData)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_SetConnectionUserData(CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, nUserData);
		}
	
		public static long GetConnectionUserData(HSteamNetConnection hPeer)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetConnectionUserData(CSteamAPIContext.GetSteamNetworkingSockets(), hPeer);
		}
	
		public static void SetConnectionName(HSteamNetConnection hPeer, string pszName)
		{
			InteropHelp.TestIfAvailableClient();
			using InteropHelp.UTF8StringHandle pszName2 = new InteropHelp.UTF8StringHandle(pszName);
			NativeMethods.ISteamNetworkingSockets_SetConnectionName(CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, pszName2);
		}
	
		public static bool GetConnectionName(HSteamNetConnection hPeer, out string pszName, int nMaxLen)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(nMaxLen);
			bool flag = NativeMethods.ISteamNetworkingSockets_GetConnectionName(CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, intPtr, nMaxLen);
			pszName = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}
	
		public static EResult SendMessageToConnection(HSteamNetConnection hConn, IntPtr pData, uint cbData, int nSendFlags, out long pOutMessageNumber)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_SendMessageToConnection(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, pData, cbData, nSendFlags, out pOutMessageNumber);
		}
	
		public static void SendMessages(int nMessages, SteamNetworkingMessage_t[] pMessages, long[] pOutMessageNumberOrResult)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamNetworkingSockets_SendMessages(CSteamAPIContext.GetSteamNetworkingSockets(), nMessages, pMessages, pOutMessageNumberOrResult);
		}
	
		public static EResult FlushMessagesOnConnection(HSteamNetConnection hConn)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_FlushMessagesOnConnection(CSteamAPIContext.GetSteamNetworkingSockets(), hConn);
		}
	
		public static int ReceiveMessagesOnConnection(HSteamNetConnection hConn, IntPtr[] ppOutMessages, int nMaxMessages)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_ReceiveMessagesOnConnection(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, ppOutMessages, nMaxMessages);
		}
	
		public static bool GetConnectionInfo(HSteamNetConnection hConn, out SteamNetConnectionInfo_t pInfo)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetConnectionInfo(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, out pInfo);
		}
	
		public static bool GetQuickConnectionStatus(HSteamNetConnection hConn, out SteamNetworkingQuickConnectionStatus pStats)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetQuickConnectionStatus(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, out pStats);
		}
	
		public static int GetDetailedConnectionStatus(HSteamNetConnection hConn, out string pszBuf, int cbBuf)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cbBuf);
			int num = NativeMethods.ISteamNetworkingSockets_GetDetailedConnectionStatus(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, intPtr, cbBuf);
			pszBuf = ((num != -1) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return num;
		}
	
		public static bool GetListenSocketAddress(HSteamListenSocket hSocket, out SteamNetworkingIPAddr address)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetListenSocketAddress(CSteamAPIContext.GetSteamNetworkingSockets(), hSocket, out address);
		}
	
		public static bool CreateSocketPair(out HSteamNetConnection pOutConnection1, out HSteamNetConnection pOutConnection2, bool bUseNetworkLoopback, ref SteamNetworkingIdentity pIdentity1, ref SteamNetworkingIdentity pIdentity2)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_CreateSocketPair(CSteamAPIContext.GetSteamNetworkingSockets(), out pOutConnection1, out pOutConnection2, bUseNetworkLoopback, ref pIdentity1, ref pIdentity2);
		}
	
		public static bool GetIdentity(out SteamNetworkingIdentity pIdentity)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetIdentity(CSteamAPIContext.GetSteamNetworkingSockets(), out pIdentity);
		}
	
		public static ESteamNetworkingAvailability InitAuthentication()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_InitAuthentication(CSteamAPIContext.GetSteamNetworkingSockets());
		}
	
		public static ESteamNetworkingAvailability GetAuthenticationStatus(out SteamNetAuthenticationStatus_t pDetails)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetAuthenticationStatus(CSteamAPIContext.GetSteamNetworkingSockets(), out pDetails);
		}
	
		public static HSteamNetPollGroup CreatePollGroup()
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamNetPollGroup)NativeMethods.ISteamNetworkingSockets_CreatePollGroup(CSteamAPIContext.GetSteamNetworkingSockets());
		}
	
		public static bool DestroyPollGroup(HSteamNetPollGroup hPollGroup)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_DestroyPollGroup(CSteamAPIContext.GetSteamNetworkingSockets(), hPollGroup);
		}
	
		public static bool SetConnectionPollGroup(HSteamNetConnection hConn, HSteamNetPollGroup hPollGroup)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_SetConnectionPollGroup(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, hPollGroup);
		}
	
		public static int ReceiveMessagesOnPollGroup(HSteamNetPollGroup hPollGroup, IntPtr[] ppOutMessages, int nMaxMessages)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_ReceiveMessagesOnPollGroup(CSteamAPIContext.GetSteamNetworkingSockets(), hPollGroup, ppOutMessages, nMaxMessages);
		}
	
		public static bool ReceivedRelayAuthTicket(IntPtr pvTicket, int cbTicket, out SteamDatagramRelayAuthTicket pOutParsedTicket)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_ReceivedRelayAuthTicket(CSteamAPIContext.GetSteamNetworkingSockets(), pvTicket, cbTicket, out pOutParsedTicket);
		}
	
		public static int FindRelayAuthTicketForServer(ref SteamNetworkingIdentity identityGameServer, int nVirtualPort, out SteamDatagramRelayAuthTicket pOutParsedTicket)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_FindRelayAuthTicketForServer(CSteamAPIContext.GetSteamNetworkingSockets(), ref identityGameServer, nVirtualPort, out pOutParsedTicket);
		}
	
		public static HSteamNetConnection ConnectToHostedDedicatedServer(ref SteamNetworkingIdentity identityTarget, int nVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectToHostedDedicatedServer(CSteamAPIContext.GetSteamNetworkingSockets(), ref identityTarget, nVirtualPort, nOptions, pOptions);
		}
	
		public static ushort GetHostedDedicatedServerPort()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerPort(CSteamAPIContext.GetSteamNetworkingSockets());
		}
	
		public static SteamNetworkingPOPID GetHostedDedicatedServerPOPID()
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamNetworkingPOPID)NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerPOPID(CSteamAPIContext.GetSteamNetworkingSockets());
		}
	
		public static EResult GetHostedDedicatedServerAddress(out SteamDatagramHostedAddress pRouting)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerAddress(CSteamAPIContext.GetSteamNetworkingSockets(), out pRouting);
		}
	
		public static HSteamListenSocket CreateHostedDedicatedServerListenSocket(int nVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket(CSteamAPIContext.GetSteamNetworkingSockets(), nVirtualPort, nOptions, pOptions);
		}
	
		public static EResult GetGameCoordinatorServerLogin(out SteamDatagramGameCoordinatorServerLogin pLoginInfo, out int pcbSignedBlob, IntPtr pBlob)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetGameCoordinatorServerLogin(CSteamAPIContext.GetSteamNetworkingSockets(), out pLoginInfo, out pcbSignedBlob, pBlob);
		}
	
		public static HSteamNetConnection ConnectP2PCustomSignaling(out ISteamNetworkingConnectionCustomSignaling pSignaling, ref SteamNetworkingIdentity pPeerIdentity, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectP2PCustomSignaling(CSteamAPIContext.GetSteamNetworkingSockets(), out pSignaling, ref pPeerIdentity, nOptions, pOptions);
		}
	
		public static bool ReceivedP2PCustomSignal(IntPtr pMsg, int cbMsg, out ISteamNetworkingCustomSignalingRecvContext pContext)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_ReceivedP2PCustomSignal(CSteamAPIContext.GetSteamNetworkingSockets(), pMsg, cbMsg, out pContext);
		}
	
		public static bool GetCertificateRequest(out int pcbBlob, IntPtr pBlob, out SteamNetworkingErrMsg errMsg)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetCertificateRequest(CSteamAPIContext.GetSteamNetworkingSockets(), out pcbBlob, pBlob, out errMsg);
		}
	
		public static bool SetCertificate(IntPtr pCertificate, int cbCertificate, out SteamNetworkingErrMsg errMsg)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_SetCertificate(CSteamAPIContext.GetSteamNetworkingSockets(), pCertificate, cbCertificate, out errMsg);
		}
	}
}