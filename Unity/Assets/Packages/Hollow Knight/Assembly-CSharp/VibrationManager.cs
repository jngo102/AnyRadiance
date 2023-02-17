using InControl;

public static class VibrationManager
{
	public interface IVibrationMixerProvider
	{
		VibrationMixer GetVibrationMixer();
	}

	private static bool isMuted;

	public static bool IsMuted
	{
		get
		{
			return isMuted;
		}
		set
		{
			if (isMuted != value)
			{
				isMuted = value;
				if (isMuted)
				{
					StopAllVibration();
				}
			}
		}
	}

	public static VibrationMixer GetMixer()
	{
		Platform current = Platform.Current;
		if (current != null && current is IVibrationMixerProvider vibrationMixerProvider)
		{
			VibrationMixer vibrationMixer = vibrationMixerProvider.GetVibrationMixer();
			if (vibrationMixer != null)
			{
				return vibrationMixer;
			}
		}
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice != null && activeDevice.IsAttached && activeDevice is IVibrationMixerProvider vibrationMixerProvider2)
		{
			VibrationMixer vibrationMixer2 = vibrationMixerProvider2.GetVibrationMixer();
			if (vibrationMixer2 != null)
			{
				return vibrationMixer2;
			}
		}
		return null;
	}

	public static VibrationEmission PlayVibrationClipOneShot(VibrationData vibrationData, VibrationTarget? vibrationTarget = null, bool isLooping = false, string tag = "")
	{
		if (isMuted)
		{
			return null;
		}
		return GetMixer()?.PlayEmission(vibrationData, vibrationTarget ?? new VibrationTarget(VibrationMotors.All), isLooping, tag);
	}

	public static void StopAllVibration()
	{
		if (!isMuted)
		{
			GetMixer()?.StopAllEmissions();
		}
	}

	public static void StopAllVibrationsWithTag(string tag)
	{
		if (!isMuted)
		{
			GetMixer()?.StopAllEmissionsWithTag(tag);
		}
	}
}
