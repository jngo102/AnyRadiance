using System;

namespace Steamworks
{
	
	[Flags]
	public enum EAppOwnershipFlags
	{
		k_EAppOwnershipFlags_None = 0,
		k_EAppOwnershipFlags_OwnsLicense = 1,
		k_EAppOwnershipFlags_FreeLicense = 2,
		k_EAppOwnershipFlags_RegionRestricted = 4,
		k_EAppOwnershipFlags_LowViolence = 8,
		k_EAppOwnershipFlags_InvalidPlatform = 0x10,
		k_EAppOwnershipFlags_SharedLicense = 0x20,
		k_EAppOwnershipFlags_FreeWeekend = 0x40,
		k_EAppOwnershipFlags_RetailLicense = 0x80,
		k_EAppOwnershipFlags_LicenseLocked = 0x100,
		k_EAppOwnershipFlags_LicensePending = 0x200,
		k_EAppOwnershipFlags_LicenseExpired = 0x400,
		k_EAppOwnershipFlags_LicensePermanent = 0x800,
		k_EAppOwnershipFlags_LicenseRecurring = 0x1000,
		k_EAppOwnershipFlags_LicenseCanceled = 0x2000,
		k_EAppOwnershipFlags_AutoGrant = 0x4000,
		k_EAppOwnershipFlags_PendingGift = 0x8000,
		k_EAppOwnershipFlags_RentalNotActivated = 0x10000,
		k_EAppOwnershipFlags_Rental = 0x20000,
		k_EAppOwnershipFlags_SiteLicense = 0x40000,
		k_EAppOwnershipFlags_LegacyFreeSub = 0x80000,
		k_EAppOwnershipFlags_InvalidOSType = 0x100000
	}
}