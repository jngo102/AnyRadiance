using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	[CallbackIdentity(5304)]
	public struct ChangeNumOpenSlotsCallback_t
	{
		public const int k_iCallback = 5304;
	
		public EResult m_eResult;
	}
}