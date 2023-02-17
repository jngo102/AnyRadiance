using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 1)]
	[CallbackIdentity(5306)]
	public struct ActiveBeaconsUpdated_t
	{
		public const int k_iCallback = 5306;
	}
}