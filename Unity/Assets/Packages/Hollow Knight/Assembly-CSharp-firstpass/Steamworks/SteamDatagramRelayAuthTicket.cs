using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamDatagramRelayAuthTicket
	{
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		private struct ExtraField
		{
			private enum EType
			{
				k_EType_String,
				k_EType_Int,
				k_EType_Fixed64
			}
	
			[StructLayout(LayoutKind.Explicit)]
			private struct OptionValue
			{
				[FieldOffset(0)]
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
				private byte[] m_szStringValue;
	
				[FieldOffset(0)]
				private long m_nIntValue;
	
				[FieldOffset(0)]
				private ulong m_nFixed64Value;
			}
	
			private EType m_eType;
	
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
			private byte[] m_szName;
	
			private OptionValue m_val;
		}
	
		private SteamNetworkingIdentity m_identityGameserver;
	
		private SteamNetworkingIdentity m_identityAuthorizedClient;
	
		private uint m_unPublicIP;
	
		private RTime32 m_rtimeTicketExpiry;
	
		private SteamDatagramHostedAddress m_routing;
	
		private uint m_nAppID;
	
		private int m_nRestrictToVirtualPort;
	
		private const int k_nMaxExtraFields = 16;
	
		private int m_nExtraFields;
	
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		private ExtraField[] m_vecExtraFields;
	
		public void Clear()
		{
		}
	}
}