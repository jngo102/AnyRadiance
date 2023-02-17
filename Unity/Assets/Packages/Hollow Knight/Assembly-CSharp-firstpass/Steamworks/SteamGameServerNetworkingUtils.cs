using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	
	public static class SteamGameServerNetworkingUtils
	{
		public static IntPtr AllocateMessage(int cbAllocateBuffer)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_AllocateMessage(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), cbAllocateBuffer);
		}
	
		public static ESteamNetworkingAvailability GetRelayNetworkStatus(out SteamRelayNetworkStatus_t pDetails)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_GetRelayNetworkStatus(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), out pDetails);
		}
	
		public static float GetLocalPingLocation(out SteamNetworkPingLocation_t result)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_GetLocalPingLocation(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), out result);
		}
	
		public static int EstimatePingTimeBetweenTwoLocations(ref SteamNetworkPingLocation_t location1, ref SteamNetworkPingLocation_t location2)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref location1, ref location2);
		}
	
		public static int EstimatePingTimeFromLocalHost(ref SteamNetworkPingLocation_t remoteLocation)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_EstimatePingTimeFromLocalHost(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref remoteLocation);
		}
	
		public static void ConvertPingLocationToString(ref SteamNetworkPingLocation_t location, out string pszBuf, int cchBufSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal(cchBufSize);
			NativeMethods.ISteamNetworkingUtils_ConvertPingLocationToString(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref location, intPtr, cchBufSize);
			pszBuf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}
	
		public static bool ParsePingLocationString(string pszString, out SteamNetworkPingLocation_t result)
		{
			InteropHelp.TestIfAvailableGameServer();
			using InteropHelp.UTF8StringHandle pszString2 = new InteropHelp.UTF8StringHandle(pszString);
			return NativeMethods.ISteamNetworkingUtils_ParsePingLocationString(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), pszString2, out result);
		}
	
		public static bool CheckPingDataUpToDate(float flMaxAgeSeconds)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_CheckPingDataUpToDate(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), flMaxAgeSeconds);
		}
	
		public static int GetPingToDataCenter(SteamNetworkingPOPID popID, out SteamNetworkingPOPID pViaRelayPoP)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_GetPingToDataCenter(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), popID, out pViaRelayPoP);
		}
	
		public static int GetDirectPingToPOP(SteamNetworkingPOPID popID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_GetDirectPingToPOP(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), popID);
		}
	
		public static int GetPOPCount()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_GetPOPCount(CSteamGameServerAPIContext.GetSteamNetworkingUtils());
		}
	
		public static int GetPOPList(out SteamNetworkingPOPID list, int nListSz)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_GetPOPList(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), out list, nListSz);
		}
	
		public static SteamNetworkingMicroseconds GetLocalTimestamp()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamNetworkingMicroseconds)NativeMethods.ISteamNetworkingUtils_GetLocalTimestamp(CSteamGameServerAPIContext.GetSteamNetworkingUtils());
		}
	
		public static void SetDebugOutputFunction(ESteamNetworkingSocketsDebugOutputType eDetailLevel, FSteamNetworkingSocketsDebugOutput pfnFunc)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamNetworkingUtils_SetDebugOutputFunction(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), eDetailLevel, pfnFunc);
		}
	
		public static bool SetConfigValue(ESteamNetworkingConfigValue eValue, ESteamNetworkingConfigScope eScopeType, IntPtr scopeObj, ESteamNetworkingConfigDataType eDataType, IntPtr pArg)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_SetConfigValue(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), eValue, eScopeType, scopeObj, eDataType, pArg);
		}
	
		public static ESteamNetworkingGetConfigValueResult GetConfigValue(ESteamNetworkingConfigValue eValue, ESteamNetworkingConfigScope eScopeType, IntPtr scopeObj, out ESteamNetworkingConfigDataType pOutDataType, IntPtr pResult, out ulong cbResult)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_GetConfigValue(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), eValue, eScopeType, scopeObj, out pOutDataType, pResult, out cbResult);
		}
	
		public static bool GetConfigValueInfo(ESteamNetworkingConfigValue eValue, IntPtr pOutName, out ESteamNetworkingConfigDataType pOutDataType, out ESteamNetworkingConfigScope pOutScope, out ESteamNetworkingConfigValue pOutNextValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_GetConfigValueInfo(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), eValue, pOutName, out pOutDataType, out pOutScope, out pOutNextValue);
		}
	
		public static ESteamNetworkingConfigValue GetFirstConfigValue()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingUtils_GetFirstConfigValue(CSteamGameServerAPIContext.GetSteamNetworkingUtils());
		}
	
		public static void SteamNetworkingIPAddr_ToString(ref SteamNetworkingIPAddr addr, out string buf, uint cbBuf, bool bWithPort)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cbBuf);
			NativeMethods.ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref addr, intPtr, cbBuf, bWithPort);
			buf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}
	
		public static bool SteamNetworkingIPAddr_ParseString(out SteamNetworkingIPAddr pAddr, string pszStr)
		{
			InteropHelp.TestIfAvailableGameServer();
			using InteropHelp.UTF8StringHandle pszStr2 = new InteropHelp.UTF8StringHandle(pszStr);
			return NativeMethods.ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), out pAddr, pszStr2);
		}
	
		public static void SteamNetworkingIdentity_ToString(ref SteamNetworkingIdentity identity, out string buf, uint cbBuf)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cbBuf);
			NativeMethods.ISteamNetworkingUtils_SteamNetworkingIdentity_ToString(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), ref identity, intPtr, cbBuf);
			buf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}
	
		public static bool SteamNetworkingIdentity_ParseString(out SteamNetworkingIdentity pIdentity, string pszStr)
		{
			InteropHelp.TestIfAvailableGameServer();
			using InteropHelp.UTF8StringHandle pszStr2 = new InteropHelp.UTF8StringHandle(pszStr);
			return NativeMethods.ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString(CSteamGameServerAPIContext.GetSteamNetworkingUtils(), out pIdentity, pszStr2);
		}
	}
}