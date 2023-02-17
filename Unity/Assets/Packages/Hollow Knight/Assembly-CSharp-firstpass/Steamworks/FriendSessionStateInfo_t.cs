using System.Runtime.InteropServices;

namespace Steamworks
{
	
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FriendSessionStateInfo_t
	{
		public uint m_uiOnlineSessionInstances;
	
		public byte m_uiPublishedToFriendsSessionInstance;
	}
}