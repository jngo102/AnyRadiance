using System.IO;
using Language;
using UnityEngine;

public static class ConfigManager
{
	private static bool _isInit;

	private static string _path;

	public static float CameraShakeMultiplier { get; private set; }

	public static float ControllerRumbleMultiplier { get; private set; }

	private static bool IsConfigFileSupported
	{
		get
		{
			if (Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.OSXPlayer)
			{
				return Application.platform == RuntimePlatform.LinuxPlayer;
			}
			return true;
		}
	}

	public static bool IsSavingConfig { get; private set; }

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Init()
	{
		_isInit = true;
		CameraShakeMultiplier = 1f;
		ControllerRumbleMultiplier = 1f;
		if (IsConfigFileSupported)
		{
			_path = Path.Combine(Application.dataPath, "Config.ini");
			try
			{
				string contents = (File.Exists(_path) ? File.ReadAllText(_path) : string.Empty);
				File.WriteAllText(_path, contents);
			}
			catch
			{
				_path = Path.Combine(Application.persistentDataPath, "AppConfig.ini");
			}
			if ((bool)Platform.Current)
			{
				LoadConfig();
			}
			else
			{
				Platform.PlatformBecameCurrent += LoadConfig;
			}
		}
	}

	private static void LoadConfig()
	{
		if (!IsConfigFileSupported || !_isInit)
		{
			return;
		}
		if (File.Exists(_path))
		{
			INIParser iNIParser = new INIParser();
			iNIParser.Open(_path);
			string text = iNIParser.ReadValue("Localization", "Language", global::Language.Language.CurrentLanguage().ToString()).ToUpper();
			if (IsLanguageValid(text))
			{
				PlayerPrefs.SetInt("GameLangSet", 1);
				global::Language.Language.SwitchLanguage(text);
			}
			else
			{
				SetDefaultLanguageSetting();
			}
			string text2 = iNIParser.ReadValue("VideoSettings", "FrameRateCap", 400.ToString());
			if (int.TryParse(text2, out var result))
			{
				Platform.Current.SharedData.SetInt("VidTFR", result);
			}
			else
			{
				Debug.LogError("Framerate cap (" + text2 + ") defined in \"" + _path + "\" is not valid!");
			}
			CameraShakeMultiplier = (float)iNIParser.ReadValue("Accessibility", "CameraShakeMultiplier", 1.0);
			ControllerRumbleMultiplier = (float)iNIParser.ReadValue("Accessibility", "ControllerRumbleMultiplier", 1.0);
			iNIParser.Close();
		}
		else
		{
			SetDefaultLanguageSetting();
		}
		SaveConfig();
		LogoLanguage logoLanguage = Object.FindObjectOfType<LogoLanguage>();
		if ((bool)logoLanguage)
		{
			logoLanguage.SetSprite();
		}
	}

	private static void SetDefaultLanguageSetting()
	{
		PlayerPrefs.SetInt("GameLangSet", 0);
	}

	public static void SaveConfig()
	{
		if (IsConfigFileSupported && _isInit)
		{
			IsSavingConfig = true;
			string text = global::Language.Language.CurrentLanguage().ToString();
			INIParser iNIParser = new INIParser();
			iNIParser.Open(_path);
			iNIParser.WriteValue("Localization", "Language", text);
			iNIParser.WriteValue("VideoSettings", "FrameRateCap", Platform.Current.SharedData.GetInt("VidTFR", 400));
			iNIParser.WriteValue("Accessibility", "CameraShakeMultiplier", CameraShakeMultiplier);
			iNIParser.WriteValue("Accessibility", "ControllerRumbleMultiplier", ControllerRumbleMultiplier);
			iNIParser.Close();
			string text2 = File.ReadAllText(_path);
			string text3 = "Available Languages:\n";
			string[] languages = global::Language.Language.GetLanguages();
			foreach (string text4 in languages)
			{
				global::Language.Language.SwitchLanguage(text4);
				text3 = text3 + " - " + text4 + " (" + global::Language.Language.Get("LANG_" + text4, "MainMenu") + ")\n";
			}
			global::Language.Language.SwitchLanguage(text);
			IsSavingConfig = false;
			text3 += "\n";
			string contents = text3 + text2;
			File.WriteAllText(_path, contents);
		}
	}

	private static bool IsLanguageValid(string languageCode)
	{
		string[] languages = global::Language.Language.GetLanguages();
		for (int i = 0; i < languages.Length; i++)
		{
			if (languages[i] == languageCode)
			{
				return true;
			}
		}
		return false;
	}
}
