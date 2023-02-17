using System;

namespace Steamworks
{
	
	[Flags]
	public enum EMarketNotAllowedReasonFlags
	{
		k_EMarketNotAllowedReason_None = 0,
		k_EMarketNotAllowedReason_TemporaryFailure = 1,
		k_EMarketNotAllowedReason_AccountDisabled = 2,
		k_EMarketNotAllowedReason_AccountLockedDown = 4,
		k_EMarketNotAllowedReason_AccountLimited = 8,
		k_EMarketNotAllowedReason_TradeBanned = 0x10,
		k_EMarketNotAllowedReason_AccountNotTrusted = 0x20,
		k_EMarketNotAllowedReason_SteamGuardNotEnabled = 0x40,
		k_EMarketNotAllowedReason_SteamGuardOnlyRecentlyEnabled = 0x80,
		k_EMarketNotAllowedReason_RecentPasswordReset = 0x100,
		k_EMarketNotAllowedReason_NewPaymentMethod = 0x200,
		k_EMarketNotAllowedReason_InvalidCookie = 0x400,
		k_EMarketNotAllowedReason_UsingNewDevice = 0x800,
		k_EMarketNotAllowedReason_RecentSelfRefund = 0x1000,
		k_EMarketNotAllowedReason_NewPaymentMethodCannotBeVerified = 0x2000,
		k_EMarketNotAllowedReason_NoRecentPurchases = 0x4000,
		k_EMarketNotAllowedReason_AcceptedWalletGift = 0x8000
	}
}