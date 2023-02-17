using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	
	public static class SteamNetworkingUtils
	{
		public static IntPtr AllocateMessage(int cbAllocateBuffer)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_AllocateMessage(CSteamAPIContext.GetSteamNetworkingUtils(), cbAllocateBuffer);
		}
	
		public static ESteamNetworkingAvailability GetRelayNetworkStatus(out SteamRelayNetworkStatus_t pDetails)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_GetRelayNetworkStatus(CSteamAPIContext.GetSteamNetworkingUtils(), out pDetails);
		}
	
		public static float GetLocalPingLocation(out SteamNetworkPingLocation_t result)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_GetLocalPingLocation(CSteamAPIContext.GetSteamNetworkingUtils(), out result);
		}
	
		public static int EstimatePingTimeBetweenTwoLocations(ref SteamNetworkPingLocation_t location1, ref SteamNetworkPingLocation_t location2)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations(CSteamAPIContext.GetSteamNetworkingUtils(), ref location1, ref location2);
		}
	
		public static int EstimatePingTimeFromLocalHost(ref SteamNetworkPingLocation_t remoteLocation)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_EstimatePingTimeFromLocalHost(CSteamAPIContext.GetSteamNetworkingUtils(), ref remoteLocation);
		}
	
		public static void ConvertPingLocationToString(ref SteamNetworkPingLocation_t location, out string pszBuf, int cchBufSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchBufSize);
			NativeMethods.ISteamNetworkingUtils_ConvertPingLocationToString(CSteamAPIContext.GetSteamNetworkingUtils(), ref location, intPtr, cchBufSize);
			pszBuf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}
	
		public static bool ParsePingLocationString(string pszString, out SteamNetworkPingLocation_t result)
		{
			InteropHelp.TestIfAvailableClient();
			using InteropHelp.UTF8StringHandle pszString2 = new InteropHelp.UTF8StringHandle(pszString);
			return NativeMethods.ISteamNetworkingUtils_ParsePingLocationString(CSteamAPIContext.GetSteamNetworkingUtils(), pszString2, out result);
		}
	
		public static bool CheckPingDataUpToDate(float flMaxAgeSeconds)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_CheckPingDataUpToDate(CSteamAPIContext.GetSteamNetworkingUtils(), flMaxAgeSeconds);
		}
	
		public static int GetPingToDataCenter(SteamNetworkingPOPID popID, out SteamNetworkingPOPID pViaRelayPoP)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_GetPingToDataCenter(CSteamAPIContext.GetSteamNetworkingUtils(), popID, out pViaRelayPoP);
		}
	
		public static int GetDirectPingToPOP(SteamNetworkingPOPID popID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_GetDirectPingToPOP(CSteamAPIContext.GetSteamNetworkingUtils(), popID);
		}
	
		public static int GetPOPCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_GetPOPCount(CSteamAPIContext.GetSteamNetworkingUtils());
		}
	
		public static int GetPOPList(out SteamNetworkingPOPID list, int nListSz)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_GetPOPList(CSteamAPIContext.GetSteamNetworkingUtils(), out list, nListSz);
		}
	
		public static SteamNetworkingMicroseconds GetLocalTimestamp()
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamNetworkingMicroseconds)NativeMethods.ISteamNetworkingUtils_GetLocalTimestamp(CSteamAPIContext.GetSteamNetworkingUtils());
		}
	
		public static void SetDebugOutputFunction(ESteamNetworkingSocketsDebugOutputType eDetailLevel, FSteamNetworkingSocketsDebugOutput pfnFunc)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamNetworkingUtils_SetDebugOutputFunction(CSteamAPIContext.GetSteamNetworkingUtils(), eDetailLevel, pfnFunc);
		}
	
		public static bool SetConfigValue(ESteamNetworkingConfigValue eValue, ESteamNetworkingConfigScope eScopeType, IntPtr scopeObj, ESteamNetworkingConfigDataType eDataType, IntPtr pArg)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_SetConfigValue(CSteamAPIContext.GetSteamNetworkingUtils(), eValue, eScopeType, scopeObj, eDataType, pArg);
		}
	
		public static ESteamNetworkingGetConfigValueResult GetConfigValue(ESteamNetworkingConfigValue eValue, ESteamNetworkingConfigScope eScopeType, IntPtr scopeObj, out ESteamNetworkingConfigDataType pOutDataType, IntPtr pResult, out ulong cbResult)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_GetConfigValue(CSteamAPIContext.GetSteamNetworkingUtils(), eValue, eScopeType, scopeObj, out pOutDataType, pResult, out cbResult);
		}
	
		public static bool GetConfigValueInfo(ESteamNetworkingConfigValue eValue, IntPtr pOutName, out ESteamNetworkingConfigDataType pOutDataType, out ESteamNetworkingConfigScope pOutScope, out ESteamNetworkingConfigValue pOutNextValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_GetConfigValueInfo(CSteamAPIContext.GetSteamNetworkingUtils(), eValue, pOutName, out pOutDataType, out pOutScope, out pOutNextValue);
		}
	
		public static ESteamNetworkingConfigValue GetFirstConfigValue()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingUtils_GetFirstConfigValue(CSteamAPIContext.GetSteamNetworkingUtils());
		}
	
		public static void SteamNetworkingIPAddr_ToString(ref SteamNetworkingIPAddr addr, out string buf, uint cbBuf, bool bWithPort)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cbBuf);
			NativeMethods.ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString(CSteamAPIContext.GetSteamNetworkingUtils(), ref addr, intPtr, cbBuf, bWithPort);
			buf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}
	
		public static bool SteamNetworkingIPAddr_ParseString(out SteamNetworkingIPAddr pAddr, string pszStr)
		{
			InteropHelp.TestIfAvailableClient();
			using InteropHelp.UTF8StringHandle pszStr2 = new InteropHelp.UTF8StringHandle(pszStr);
			return NativeMethods.ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString(CSteamAPIContext.GetSteamNetworkingUtils(), out pAddr, pszStr2);
		}
	
		public static void SteamNetworkingIdentity_ToString(ref SteamNetworkingIdentity identity, out string buf, uint cbBuf)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cbBuf);
			NativeMethods.ISteamNetworkingUtils_SteamNetworkingIdentity_ToString(CSteamAPIContext.GetSteamNetworkingUtils(), ref identity, intPtr, cbBuf);
			buf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}
	
		public static bool SteamNetworkingIdentity_ParseString(out SteamNetworkingIdentity pIdentity, string pszStr)
		{
			InteropHelp.TestIfAvailableClient();
			using InteropHelp.UTF8StringHandle pszStr2 = new InteropHelp.UTF8StringHandle(pszStr);
			return NativeMethods.ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString(CSteamAPIContext.GetSteamNetworkingUtils(), out pIdentity, pszStr2);
		}
	}
}