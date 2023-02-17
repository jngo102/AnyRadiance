using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[Serializable]
	public struct SteamNetworkingConfigValue_t
	{
		[StructLayout(LayoutKind.Explicit)]
		public struct OptionValue
		{
			[FieldOffset(0)]
			public int m_int32;
	
			[FieldOffset(0)]
			public long m_int64;
	
			[FieldOffset(0)]
			public float m_float;
	
			[FieldOffset(0)]
			public IntPtr m_string;
	
			[FieldOffset(0)]
			public IntPtr m_functionPtr;
		}
	
		public ESteamNetworkingConfigValue m_eValue;
	
		public ESteamNetworkingConfigDataType m_eDataType;
	
		public OptionValue m_val;
	}
}