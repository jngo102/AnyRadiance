using UnityEngine;
using UnityEngine.UI;

public class MenuSetting : MonoBehaviour
{
	public enum MenuSettingType
	{
		Resolution = 10,
		FullScreen = 11,
		VSync = 12,
		MonitorSelect = 14,
		FrameCap = 15,
		ParticleLevel = 16,
		ShaderQuality = 17,
		GameLanguage = 33,
		GameBackerCredits = 34,
		NativeAchievements = 35,
		NativeInput = 36,
		ControllerRumble = 37,
		CustomSetting = 38
	}

	public delegate void ApplySetting(MenuSetting self, int settingIndex);

	public delegate void RefreshSetting(MenuSetting self, bool alsoApplySetting);

	public MenuSettingType settingType;

	public MenuOptionHorizontal optionList;

	private GameManager gm;

	private GameSettings gs;

	private bool verboseMode;

	public ApplySetting customApplySetting { get; set; }

	public RefreshSetting customRefreshSetting { get; set; }

	private void Start()
	{
		gm = GameManager.instance;
		gs = gm.gameSettings;
	}

	public void RefreshValueFromGameSettings(bool alsoApplySetting = false)
	{
		if (settingType == MenuSettingType.CustomSetting && customRefreshSetting != null)
		{
			customRefreshSetting?.Invoke(this, alsoApplySetting);
		}
		else
		{
			orig_RefreshValueFromGameSettings(alsoApplySetting);
		}
	}

	public void UpdateSetting(int settingIndex)
	{
		if (settingType == MenuSettingType.CustomSetting && customApplySetting != null)
		{
			customApplySetting?.Invoke(this, settingIndex);
		}
		else
		{
			orig_UpdateSetting(settingIndex);
		}
	}

	public void orig_UpdateSetting(int settingIndex)
	{
		if (verboseMode)
		{
			Debug.Log(settingType.ToString() + " " + settingIndex);
		}
		if (gs == null)
		{
			gs = GameManager.instance.gameSettings;
		}
		if (settingType == MenuSettingType.GameBackerCredits)
		{
			if (settingIndex == 0)
			{
				gs.backerCredits = 0;
			}
			else
			{
				gs.backerCredits = 1;
			}
			gm.MatchBackerCreditsSetting();
		}
		else if (settingType == MenuSettingType.NativeAchievements)
		{
			if (settingIndex == 0)
			{
				gs.showNativeAchievementPopups = 0;
			}
			else
			{
				gs.showNativeAchievementPopups = 1;
			}
		}
		if (settingType == MenuSettingType.FullScreen)
		{
			gs.fullScreen = settingIndex;
			switch (settingIndex)
			{
			case 0:
				Screen.fullScreenMode = FullScreenMode.Windowed;
				break;
			case 2:
				Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
				break;
			default:
				Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
				break;
			}
		}
		else if (settingType == MenuSettingType.VSync)
		{
			if (settingIndex == 0)
			{
				gs.vSync = 0;
				QualitySettings.vSyncCount = 0;
				return;
			}
			gs.vSync = 1;
			QualitySettings.vSyncCount = 1;
			Application.targetFrameRate = -1;
			UIManager.instance.DisableFrameCapSetting();
		}
		else if (settingType == MenuSettingType.ParticleLevel)
		{
			if (settingIndex == 0)
			{
				gs.particleEffectsLevel = 0;
				gm.RefreshParticleSystems();
			}
			else
			{
				gs.particleEffectsLevel = 1;
				gm.RefreshParticleSystems();
			}
		}
		else if (settingType == MenuSettingType.ShaderQuality)
		{
			switch (settingIndex)
			{
			case 0:
				gs.shaderQuality = ShaderQualities.Low;
				break;
			case 1:
				gs.shaderQuality = ShaderQualities.Medium;
				break;
			default:
				gs.shaderQuality = ShaderQualities.High;
				break;
			}
		}
		else if (settingType == MenuSettingType.NativeInput)
		{
			GameSettings gameSettings = GameManager.instance.gameSettings;
			gameSettings.isNativeInput = settingIndex != 0;
			NativeInputModuleManager.IsUsed = gameSettings.isNativeInput;
		}
		else if (settingType == MenuSettingType.ControllerRumble)
		{
			bool vibrationMuted = (VibrationManager.IsMuted = settingIndex == 0);
			gs.vibrationMuted = vibrationMuted;
		}
		else if (settingType == MenuSettingType.FrameCap)
		{
			gs.frameCapOn = settingIndex == 1;
			if (settingIndex == 0)
			{
				Application.targetFrameRate = -1;
				return;
			}
			UIManager.instance.DisableVsyncSetting();
			Application.targetFrameRate = Platform.Current.SharedData.GetInt("VidTFR", 400);
		}
	}

	public void orig_RefreshValueFromGameSettings(bool alsoApplySetting = false)
	{
		if (gs == null)
		{
			gs = GameManager.instance.gameSettings;
		}
		if (settingType == MenuSettingType.FullScreen)
		{
			optionList.SetOptionTo(gs.fullScreen);
		}
		else if (settingType == MenuSettingType.VSync)
		{
			optionList.SetOptionTo(gs.vSync);
		}
		else if (settingType == MenuSettingType.ParticleLevel)
		{
			optionList.SetOptionTo(gs.particleEffectsLevel);
		}
		else if (settingType == MenuSettingType.ShaderQuality)
		{
			optionList.SetOptionTo((int)gs.shaderQuality);
		}
		else if (settingType == MenuSettingType.GameBackerCredits)
		{
			optionList.SetOptionTo(gs.backerCredits);
		}
		else if (settingType == MenuSettingType.NativeAchievements)
		{
			optionList.SetOptionTo(gs.showNativeAchievementPopups);
		}
		else if (settingType == MenuSettingType.ParticleLevel)
		{
			optionList.SetOptionTo(gs.particleEffectsLevel);
		}
		else if (settingType == MenuSettingType.NativeInput)
		{
			optionList.SetOptionTo(gs.isNativeInput ? 1 : 0);
		}
		else if (settingType == MenuSettingType.ControllerRumble)
		{
			optionList.SetOptionTo((!gs.vibrationMuted) ? 1 : 0);
		}
		else if (settingType == MenuSettingType.FrameCap)
		{
			optionList.SetOptionTo(gs.frameCapOn ? 1 : 0);
		}
		if (alsoApplySetting)
		{
			UpdateSetting(optionList.selectedOptionIndex);
		}
	}
}
