using System;

namespace Steamworks
{
	
	internal static class CSteamGameServerAPIContext
	{
		private static IntPtr m_pSteamClient;
	
		private static IntPtr m_pSteamGameServer;
	
		private static IntPtr m_pSteamUtils;
	
		private static IntPtr m_pSteamNetworking;
	
		private static IntPtr m_pSteamGameServerStats;
	
		private static IntPtr m_pSteamHTTP;
	
		private static IntPtr m_pSteamInventory;
	
		private static IntPtr m_pSteamUGC;
	
		private static IntPtr m_pSteamApps;
	
		private static IntPtr m_pSteamNetworkingUtils;
	
		private static IntPtr m_pSteamNetworkingSockets;
	
		internal static void Clear()
		{
			m_pSteamClient = IntPtr.Zero;
			m_pSteamGameServer = IntPtr.Zero;
			m_pSteamUtils = IntPtr.Zero;
			m_pSteamNetworking = IntPtr.Zero;
			m_pSteamGameServerStats = IntPtr.Zero;
			m_pSteamHTTP = IntPtr.Zero;
			m_pSteamInventory = IntPtr.Zero;
			m_pSteamUGC = IntPtr.Zero;
			m_pSteamApps = IntPtr.Zero;
			m_pSteamNetworkingUtils = IntPtr.Zero;
			m_pSteamNetworkingSockets = IntPtr.Zero;
		}
	
		internal static bool Init()
		{
			HSteamUser hSteamUser = GameServer.GetHSteamUser();
			HSteamPipe hSteamPipe = GameServer.GetHSteamPipe();
			if (hSteamPipe == (HSteamPipe)0)
			{
				return false;
			}
			using (InteropHelp.UTF8StringHandle ver = new InteropHelp.UTF8StringHandle("SteamClient020"))
			{
				m_pSteamClient = NativeMethods.SteamInternal_CreateInterface(ver);
			}
			if (m_pSteamClient == IntPtr.Zero)
			{
				return false;
			}
			m_pSteamGameServer = SteamGameServerClient.GetISteamGameServer(hSteamUser, hSteamPipe, "SteamGameServer013");
			if (m_pSteamGameServer == IntPtr.Zero)
			{
				return false;
			}
			m_pSteamUtils = SteamGameServerClient.GetISteamUtils(hSteamPipe, "SteamUtils009");
			if (m_pSteamUtils == IntPtr.Zero)
			{
				return false;
			}
			m_pSteamNetworking = SteamGameServerClient.GetISteamNetworking(hSteamUser, hSteamPipe, "SteamNetworking006");
			if (m_pSteamNetworking == IntPtr.Zero)
			{
				return false;
			}
			m_pSteamGameServerStats = SteamGameServerClient.GetISteamGameServerStats(hSteamUser, hSteamPipe, "SteamGameServerStats001");
			if (m_pSteamGameServerStats == IntPtr.Zero)
			{
				return false;
			}
			m_pSteamHTTP = SteamGameServerClient.GetISteamHTTP(hSteamUser, hSteamPipe, "STEAMHTTP_INTERFACE_VERSION003");
			if (m_pSteamHTTP == IntPtr.Zero)
			{
				return false;
			}
			m_pSteamInventory = SteamGameServerClient.GetISteamInventory(hSteamUser, hSteamPipe, "STEAMINVENTORY_INTERFACE_V003");
			if (m_pSteamInventory == IntPtr.Zero)
			{
				return false;
			}
			m_pSteamUGC = SteamGameServerClient.GetISteamUGC(hSteamUser, hSteamPipe, "STEAMUGC_INTERFACE_VERSION014");
			if (m_pSteamUGC == IntPtr.Zero)
			{
				return false;
			}
			m_pSteamApps = SteamGameServerClient.GetISteamApps(hSteamUser, hSteamPipe, "STEAMAPPS_INTERFACE_VERSION008");
			if (m_pSteamApps == IntPtr.Zero)
			{
				return false;
			}
			using (InteropHelp.UTF8StringHandle pszVersion = new InteropHelp.UTF8StringHandle("SteamNetworkingUtils003"))
			{
				m_pSteamNetworkingUtils = ((NativeMethods.SteamInternal_FindOrCreateUserInterface(hSteamUser, pszVersion) != IntPtr.Zero) ? NativeMethods.SteamInternal_FindOrCreateUserInterface(hSteamUser, pszVersion) : NativeMethods.SteamInternal_FindOrCreateGameServerInterface(hSteamUser, pszVersion));
			}
			if (m_pSteamNetworkingUtils == IntPtr.Zero)
			{
				return false;
			}
			using (InteropHelp.UTF8StringHandle pszVersion2 = new InteropHelp.UTF8StringHandle("SteamNetworkingSockets008"))
			{
				m_pSteamNetworkingSockets = NativeMethods.SteamInternal_FindOrCreateGameServerInterface(hSteamUser, pszVersion2);
			}
			if (m_pSteamNetworkingSockets == IntPtr.Zero)
			{
				return false;
			}
			return true;
		}
	
		internal static IntPtr GetSteamClient()
		{
			return m_pSteamClient;
		}
	
		internal static IntPtr GetSteamGameServer()
		{
			return m_pSteamGameServer;
		}
	
		internal static IntPtr GetSteamUtils()
		{
			return m_pSteamUtils;
		}
	
		internal static IntPtr GetSteamNetworking()
		{
			return m_pSteamNetworking;
		}
	
		internal static IntPtr GetSteamGameServerStats()
		{
			return m_pSteamGameServerStats;
		}
	
		internal static IntPtr GetSteamHTTP()
		{
			return m_pSteamHTTP;
		}
	
		internal static IntPtr GetSteamInventory()
		{
			return m_pSteamInventory;
		}
	
		internal static IntPtr GetSteamUGC()
		{
			return m_pSteamUGC;
		}
	
		internal static IntPtr GetSteamApps()
		{
			return m_pSteamApps;
		}
	
		internal static IntPtr GetSteamNetworkingUtils()
		{
			return m_pSteamNetworkingUtils;
		}
	
		internal static IntPtr GetSteamNetworkingSockets()
		{
			return m_pSteamNetworkingSockets;
		}
	}
}