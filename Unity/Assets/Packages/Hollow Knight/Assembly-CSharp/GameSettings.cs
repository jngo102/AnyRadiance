using System;
using GlobalEnums;
using InControl;
using UnityEngine;

[Serializable]
public class GameSettings
{
	private bool verboseMode;

	private string settingLog = "Loaded Setting: {0} ({1})";

	private string[] commArgs;

	[Header("Game Settings")]
	public SupportedLanguages gameLanguage;

	public int backerCredits;

	public int showNativeAchievementPopups;

	public bool isNativeInput;

	public bool vibrationMuted;

	[Header("Audio Settings")]
	public float masterVolume;

	public float musicVolume;

	public float soundVolume;

	[Header("Video Settings")]
	public int fullScreen;

	public int vSync;

	public int useDisplay;

	public float overScanAdjustment;

	public float brightnessAdjustment;

	public int overscanAdjusted;

	public int brightnessAdjusted;

	public int targetFrameRate;

	public bool frameCapOn;

	public int particleEffectsLevel;

	public ShaderQualities shaderQuality;

	[Header("Controller Settings")]
	public ControllerMapping controllerMapping;

	[Header("Keyboard Settings")]
	public string jumpKey;

	public string attackKey;

	public string dashKey;

	public string castKey;

	public string superDashKey;

	public string dreamNailKey;

	public string quickMapKey;

	public string quickCastKey;

	public string inventoryKey;

	public string upKey;

	public string downKey;

	public string leftKey;

	public string rightKey;

	public GameSettings()
	{
		verboseMode = false;
		ResetGameOptionsSettings();
		ResetAudioSettings();
		ResetVideoSettings();
	}

	public void LoadGameOptionsSettings()
	{
		LoadEnum("GameLang", ref gameLanguage, SupportedLanguages.EN);
		LoadInt("GameBackers", ref backerCredits, 0);
		LoadInt("GameNativePopups", ref showNativeAchievementPopups, 0);
		LoadBool("NativeInput", ref isNativeInput, def: true);
		vibrationMuted = Platform.Current.SharedData.GetBool("RumbleMuted", def: false);
	}

	public void SaveGameOptionsSettings()
	{
		Platform.Current.SharedData.SetInt("GameLang", (int)gameLanguage);
		Platform.Current.SharedData.SetInt("GameBackers", backerCredits);
		Platform.Current.SharedData.SetInt("GameNativePopups", showNativeAchievementPopups);
		Platform.Current.SharedData.SetInt("NativeInput", isNativeInput ? 1 : 0);
		Platform.Current.SharedData.SetBool("RumbleMuted", vibrationMuted);
		Platform.Current.SharedData.Save();
	}

	public void ResetGameOptionsSettings()
	{
		gameLanguage = SupportedLanguages.EN;
		backerCredits = 0;
		showNativeAchievementPopups = 0;
		isNativeInput = true;
	}

	public void LoadVideoSettings()
	{
		if (CommandArgumentUsed("-resetres"))
		{
			Screen.SetResolution(1920, 1080, fullscreen: true);
		}
		LoadInt("VidFullscreen", ref fullScreen, 2);
		LoadInt("VidVSync", ref vSync, 0);
		LoadInt("VidDisplay", ref useDisplay, 0);
		LoadInt("VidTFR", ref targetFrameRate, 400);
		LoadBool("VidFC", ref frameCapOn, def: true);
		LoadInt("VidParticles", ref particleEffectsLevel, 1);
		LoadEnum("ShaderQuality", ref shaderQuality, (!HasSetting("VidFullscreen")) ? ShaderQualities.Medium : ShaderQualities.High);
	}

	public void SaveVideoSettings()
	{
		Platform.Current.SharedData.SetInt("VidFullscreen", fullScreen);
		Platform.Current.SharedData.SetInt("VidVSync", vSync);
		Platform.Current.SharedData.SetInt("VidDisplay", useDisplay);
		Platform.Current.SharedData.SetInt("VidTFR", targetFrameRate);
		Platform.Current.SharedData.SetBool("VidFC", frameCapOn);
		Platform.Current.SharedData.SetInt("VidParticles", particleEffectsLevel);
		Platform.Current.SharedData.SetInt("ShaderQuality", (int)shaderQuality);
		Platform.Current.SharedData.Save();
	}

	public void ResetVideoSettings()
	{
		fullScreen = 2;
		vSync = 0;
		useDisplay = 0;
		targetFrameRate = 400;
		frameCapOn = true;
		particleEffectsLevel = 1;
		overscanAdjusted = 0;
		overScanAdjustment = 0f;
		brightnessAdjusted = 0;
		brightnessAdjustment = 20f;
		shaderQuality = ShaderQualities.Medium;
	}

	public void LoadOverscanSettings()
	{
		LoadFloat("VidOSValue", ref overScanAdjustment, 0f);
	}

	public void SaveOverscanSettings()
	{
		Platform.Current.SharedData.SetFloat("VidOSValue", overScanAdjustment);
		overscanAdjusted = 1;
		Platform.Current.SharedData.SetInt("VidOSSet", overscanAdjusted);
		if (verboseMode)
		{
			LogSavedKey("VidOSValue", overScanAdjustment);
		}
		Platform.Current.SharedData.Save();
	}

	public void ResetOverscanSettings()
	{
		overScanAdjustment = 0f;
	}

	public void LoadOverscanConfigured()
	{
		LoadInt("VidOSSet", ref overscanAdjusted, 0);
	}

	public void LoadBrightnessSettings()
	{
		LoadFloat("VidBrightValue", ref brightnessAdjustment, 20f);
	}

	public void SaveBrightnessSettings()
	{
		Platform.Current.SharedData.SetFloat("VidBrightValue", brightnessAdjustment);
		brightnessAdjusted = 1;
		Platform.Current.SharedData.SetInt("VidBrightSet", brightnessAdjusted);
		if (verboseMode)
		{
			LogSavedKey("VidBrightValue", brightnessAdjustment);
		}
		Platform.Current.SharedData.Save();
	}

	public void ResetBrightnessSettings()
	{
		brightnessAdjustment = 20f;
	}

	public void LoadBrightnessConfigured()
	{
		brightnessAdjusted = Platform.Current.SharedData.GetInt("VidBrightSet", 0);
	}

	public void LoadAudioSettings()
	{
		LoadFloat("MasterVolume", ref masterVolume, 10f);
		LoadFloat("MusicVolume", ref musicVolume, 10f);
		LoadFloat("SoundVolume", ref soundVolume, 10f);
	}

	public void SaveAudioSettings()
	{
		Platform.Current.SharedData.SetFloat("MasterVolume", masterVolume);
		Platform.Current.SharedData.SetFloat("MusicVolume", musicVolume);
		Platform.Current.SharedData.SetFloat("SoundVolume", soundVolume);
		Platform.Current.SharedData.Save();
	}

	public void ResetAudioSettings()
	{
		masterVolume = 10f;
		musicVolume = 10f;
		soundVolume = 10f;
	}

	public void LoadKeyboardSettings()
	{
		LoadAndUpgradeKeyboardKey("KeyJump", ref jumpKey, Key.Z);
		LoadAndUpgradeKeyboardKey("KeyAttack", ref attackKey, Key.X);
		LoadAndUpgradeKeyboardKey("KeyDash", ref dashKey, Key.C);
		LoadAndUpgradeKeyboardKey("KeyCast", ref castKey, Key.A);
		LoadAndUpgradeKeyboardKey("KeySupDash", ref superDashKey, Key.S);
		LoadAndUpgradeKeyboardKey("KeyDreamnail", ref dreamNailKey, Key.D);
		LoadAndUpgradeKeyboardKey("KeyQuickMap", ref quickMapKey, Key.Tab);
		LoadAndUpgradeKeyboardKey("KeyQuickCast", ref quickCastKey, Key.F);
		LoadAndUpgradeKeyboardKey("KeyInventory", ref inventoryKey, Key.I);
		LoadAndUpgradeKeyboardKey("KeyUp", ref upKey, Key.UpArrow);
		LoadAndUpgradeKeyboardKey("KeyDown", ref downKey, Key.DownArrow);
		LoadAndUpgradeKeyboardKey("KeyLeft", ref leftKey, Key.LeftArrow);
		LoadAndUpgradeKeyboardKey("KeyRight", ref rightKey, Key.RightArrow);
	}

	private void LoadAndUpgradeKeyboardKey(string prefsKey, ref string setString, Key defaultKey)
	{
		string text = defaultKey.ToString();
		if (LoadString(prefsKey + "_V2", ref setString, text))
		{
			return;
		}
		Key val = Key.None;
		if (LoadEnum(prefsKey, ref val, defaultKey))
		{
			switch (val)
			{
			case Key.F5:
				setString = "LeftButton";
				break;
			case Key.F6:
				setString = "RightButton";
				break;
			case Key.F7:
				setString = "MiddleButton";
				break;
			default:
				setString = val.ToString();
				break;
			}
		}
		else
		{
			setString = text;
		}
	}

	public void SaveKeyboardSettings()
	{
		Platform.Current.SharedData.SetString("KeyJump_V2", jumpKey);
		Platform.Current.SharedData.SetString("KeyAttack_V2", attackKey);
		Platform.Current.SharedData.SetString("KeyDash_V2", dashKey);
		Platform.Current.SharedData.SetString("KeyCast_V2", castKey);
		Platform.Current.SharedData.SetString("KeySupDash_V2", superDashKey);
		Platform.Current.SharedData.SetString("KeyDreamnail_V2", dreamNailKey);
		Platform.Current.SharedData.SetString("KeyQuickMap_V2", quickMapKey);
		Platform.Current.SharedData.SetString("KeyQuickCast_V2", quickCastKey);
		Platform.Current.SharedData.SetString("KeyInventory_V2", inventoryKey);
		Platform.Current.SharedData.SetString("KeyUp_V2", upKey);
		Platform.Current.SharedData.SetString("KeyDown_V2", downKey);
		Platform.Current.SharedData.SetString("KeyLeft_V2", leftKey);
		Platform.Current.SharedData.SetString("KeyRight_V2", rightKey);
	}

	public bool LoadGamepadSettings(GamepadType gamepadType)
	{
		gamepadType = RemapGamepadTypeForSettings(gamepadType);
		if (gamepadType != 0)
		{
			string key = "Controller" + gamepadType;
			string val = "";
			if (LoadString(key, ref val, ""))
			{
				controllerMapping = JsonUtility.FromJson<ControllerMapping>(val);
				return true;
			}
		}
		return false;
	}

	public void SaveGamepadSettings(GamepadType gamepadType)
	{
		gamepadType = RemapGamepadTypeForSettings(gamepadType);
		string key = "Controller" + gamepadType;
		string text = JsonUtility.ToJson(controllerMapping);
		Platform.Current.SharedData.SetString(key, text);
		LogSavedKey(key, text);
		Platform.Current.SharedData.Save();
	}

	public void ResetGamepadSettings(GamepadType gamepadType)
	{
		gamepadType = RemapGamepadTypeForSettings(gamepadType);
		controllerMapping = new ControllerMapping();
		controllerMapping.gamepadType = gamepadType;
		if (verboseMode)
		{
			Debug.LogFormat("ResetSettings - {0}", gamepadType);
		}
	}

	private GamepadType RemapGamepadTypeForSettings(GamepadType sourceType)
	{
		GamepadType gamepadType = ((sourceType != GamepadType.SWITCH_PRO_CONTROLLER) ? sourceType : GamepadType.SWITCH_JOYCON_DUAL);
		if (gamepadType != sourceType)
		{
			Debug.LogFormat("Remapped GamepadType from {0} to {1}", sourceType.ToString(), gamepadType.ToString());
		}
		return gamepadType;
	}

	private bool LoadInt(string key, ref int val, int def)
	{
		if (Platform.Current.SharedData.HasKey(key))
		{
			val = Platform.Current.SharedData.GetInt(key, def);
			if (verboseMode)
			{
				LogLoadedKey(key, val);
			}
			return true;
		}
		val = def;
		if (verboseMode)
		{
			LogMissingKey(key);
		}
		return false;
	}

	private bool HasSetting(string key)
	{
		return Platform.Current.SharedData.HasKey(key);
	}

	private bool LoadEnum<EnumTy>(string key, ref EnumTy val, EnumTy def)
	{
		int val2 = (int)(object)val;
		bool result = LoadInt(key, ref val2, (int)(object)def);
		val = (EnumTy)(object)val2;
		return result;
	}

	private bool LoadBool(string key, ref bool val, bool def)
	{
		int val2 = (val ? 1 : 0);
		bool result = LoadInt(key, ref val2, def ? 1 : 0);
		val = val2 > 0;
		return result;
	}

	private bool LoadFloat(string key, ref float val, float def)
	{
		if (Platform.Current.SharedData.HasKey(key))
		{
			val = Platform.Current.SharedData.GetFloat(key, def);
			if (verboseMode)
			{
				LogLoadedKey(key, val);
			}
			return true;
		}
		val = def;
		if (verboseMode)
		{
			LogMissingKey(key);
		}
		return false;
	}

	private bool LoadString(string key, ref string val, string def)
	{
		if (Platform.Current.SharedData.HasKey(key))
		{
			val = Platform.Current.SharedData.GetString(key, def);
			if (verboseMode)
			{
				LogLoadedKey(key, val);
			}
			return true;
		}
		val = def;
		if (verboseMode)
		{
			LogMissingKey(key);
		}
		return false;
	}

	private void LogMissingKey(string key)
	{
		Debug.LogFormat("LoadSettings - {0} setting not found. Loading defaults.", key);
	}

	private void LogLoadedKey(string key, int value)
	{
		Debug.LogFormat("LoadSettings - {0} Loaded ({1})", key, value);
	}

	private void LogLoadedKey(string key, float value)
	{
		Debug.LogFormat("LoadSettings - {0} Loaded ({1})", key, value);
	}

	private void LogLoadedKey(string key, string value)
	{
		Debug.LogFormat("LoadSettings - {0} Loaded ({1})", key, value);
	}

	private void LogSavedKey(string key, int value)
	{
		Debug.LogFormat("SaveSettings - {0} Saved ({1})", key, value);
	}

	private void LogSavedKey(string key, float value)
	{
		Debug.LogFormat("SaveSettings - {0} Saved ({1})", key, value);
	}

	private void LogSavedKey(string key, string value)
	{
		Debug.LogFormat("SaveSettings - {0} Saved ({1})", key, value);
	}

	public bool CommandArgumentUsed(string arg)
	{
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		if (commandLineArgs == null)
		{
			return false;
		}
		string[] array = commandLineArgs;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Equals(arg))
			{
				return true;
			}
		}
		return false;
	}
}
