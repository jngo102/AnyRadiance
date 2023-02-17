using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	[CallbackIdentity(166)]
	public struct MarketEligibilityResponse_t
	{
		public const int k_iCallback = 166;
	
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAllowed;
	
		public EMarketNotAllowedReasonFlags m_eNotAllowedReason;
	
		public RTime32 m_rtAllowedAtTime;
	
		public int m_cdaySteamGuardRequiredDays;
	
		public int m_cdayNewDeviceCooldown;
	}
}