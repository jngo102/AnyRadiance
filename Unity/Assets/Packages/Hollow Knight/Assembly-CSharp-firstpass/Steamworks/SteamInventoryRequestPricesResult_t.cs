using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	[CallbackIdentity(4705)]
	public struct SteamInventoryRequestPricesResult_t
	{
		public const int k_iCallback = 4705;
	
		public EResult m_result;
	
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
		public string m_rgchCurrency;
	}
}