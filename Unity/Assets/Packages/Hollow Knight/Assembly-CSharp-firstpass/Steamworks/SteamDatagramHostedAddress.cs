using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamDatagramHostedAddress
	{
		public int m_cbSize;
	
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public byte[] m_data;
	
		public void Clear()
		{
			m_cbSize = 0;
			m_data = new byte[128];
		}
	}
}