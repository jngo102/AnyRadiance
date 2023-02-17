namespace Steamworks
{
	
	public enum EBroadcastUploadResult
	{
		k_EBroadcastUploadResultNone,
		k_EBroadcastUploadResultOK,
		k_EBroadcastUploadResultInitFailed,
		k_EBroadcastUploadResultFrameFailed,
		k_EBroadcastUploadResultTimeout,
		k_EBroadcastUploadResultBandwidthExceeded,
		k_EBroadcastUploadResultLowFPS,
		k_EBroadcastUploadResultMissingKeyFrames,
		k_EBroadcastUploadResultNoConnection,
		k_EBroadcastUploadResultRelayFailed,
		k_EBroadcastUploadResultSettingsChanged,
		k_EBroadcastUploadResultMissingAudio,
		k_EBroadcastUploadResultTooFarBehind,
		k_EBroadcastUploadResultTranscodeBehind,
		k_EBroadcastUploadResultNotAllowedToPlay,
		k_EBroadcastUploadResultBusy,
		k_EBroadcastUploadResultBanned,
		k_EBroadcastUploadResultAlreadyActive,
		k_EBroadcastUploadResultForcedOff,
		k_EBroadcastUploadResultAudioBehind,
		k_EBroadcastUploadResultShutdown,
		k_EBroadcastUploadResultDisconnect,
		k_EBroadcastUploadResultVideoInitFailed,
		k_EBroadcastUploadResultAudioInitFailed
	}
}