using System;

namespace Steamworks
{
	
	[Flags]
	public enum ERemoteStoragePlatform
	{
		k_ERemoteStoragePlatformNone = 0,
		k_ERemoteStoragePlatformWindows = 1,
		k_ERemoteStoragePlatformOSX = 2,
		k_ERemoteStoragePlatformPS3 = 4,
		k_ERemoteStoragePlatformLinux = 8,
		k_ERemoteStoragePlatformSwitch = 0x10,
		k_ERemoteStoragePlatformAndroid = 0x20,
		k_ERemoteStoragePlatformIOS = 0x40,
		k_ERemoteStoragePlatformAll = -1
	}
}