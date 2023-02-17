

// Language.Language
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Language;
using Modding;
using UnityEngine;

namespace Language
{
	public static class Language
	{
		public static string settingsAssetPath;

		private static LocalizationSettings _settings;

		private static List<string> availableLanguages;

		private static LanguageCode currentLanguage;

		private static Dictionary<string, Dictionary<string, string>> currentEntrySheets;

		private const string LastLanguageKey = "M2H_lastLanguage";

		public static LocalizationSettings settings
		{
			get
			{
				if (_settings == null)
				{
					_settings = (LocalizationSettings)Resources.Load("Languages/" + Path.GetFileNameWithoutExtension(settingsAssetPath), typeof(LocalizationSettings));
				}
				return _settings;
			}
		}

		static Language()
		{
			settingsAssetPath = "Assets/Localization/Resources/Languages/LocalizationSettings.asset";
			_settings = null;
			currentLanguage = LanguageCode.N;
			LoadAvailableLanguages();
			LoadLanguage();
		}

		public static void LoadLanguage()
		{
			string text = RestoreLanguageSelection();
			Debug.LogFormat("Restored language code '{0}'", text);
			SwitchLanguage(text);
		}

		private static string RestoreLanguageSelection()
		{
			if ((bool)Platform.Current && Platform.Current.SharedData.HasKey("M2H_lastLanguage"))
			{
				string @string = Platform.Current.SharedData.GetString("M2H_lastLanguage", "");
				Debug.LogFormat("Loaded saved language code '{0}'", @string);
				if (availableLanguages.Contains(@string))
				{
					return @string;
				}
				Debug.LogErrorFormat("Loaded saved language code '{0}' is not an available language", @string);
			}
			if (settings.useSystemLanguagePerDefault)
			{
				SystemLanguage systemLanguage = Platform.Current.GetSystemLanguage();
				Debug.LogFormat("Loaded system language '{0}'", systemLanguage);
				string text = LanguageNameToCode(systemLanguage).ToString();
				Debug.LogFormat("Loaded system language code '{0}'", text);
				if (availableLanguages.Contains(text))
				{
					return text;
				}
				Debug.LogErrorFormat("System language code '{0}' is not an available language", text);
			}
			Debug.LogFormat("Falling back to default language code '{0}'", settings.defaultLangCode);
			return LocalizationSettings.GetLanguageEnum(settings.defaultLangCode).ToString();
		}

		public static void LoadAvailableLanguages()
		{
			availableLanguages = new List<string>();
			if (settings.sheetTitles == null || settings.sheetTitles.Length == 0)
			{
				Debug.Log("None available");
				return;
			}
			foreach (LanguageCode value in Enum.GetValues(typeof(LanguageCode)))
			{
				if (HasLanguageFile(value.ToString() ?? "", settings.sheetTitles[0]))
				{
					availableLanguages.Add(value.ToString() ?? "");
				}
			}
			StringBuilder stringBuilder = new StringBuilder("Discovered supported languages: ");
			for (int i = 0; i < availableLanguages.Count; i++)
			{
				stringBuilder.Append(availableLanguages[i]);
				if (i < availableLanguages.Count - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			Debug.Log(stringBuilder.ToString());
			Resources.UnloadUnusedAssets();
		}

		public static string[] GetLanguages()
		{
			return availableLanguages.ToArray();
		}

		public static bool SwitchLanguage(string langCode)
		{
			return SwitchLanguage(LocalizationSettings.GetLanguageEnum(langCode));
		}

		public static bool SwitchLanguage(LanguageCode code)
		{
			if (availableLanguages.Contains(code.ToString() ?? ""))
			{
				DoSwitch(code);
				return true;
			}
			Debug.LogError("Could not switch from language " + currentLanguage.ToString() + " to " + code);
			if (currentLanguage == LanguageCode.N)
			{
				if (availableLanguages.Count > 0)
				{
					DoSwitch(LocalizationSettings.GetLanguageEnum(availableLanguages[0]));
					Debug.LogError("Switched to " + currentLanguage.ToString() + " instead");
				}
				else
				{
					Debug.LogError("Please verify that you have the file: Resources/Languages/" + code);
					Debug.Break();
				}
			}
			return false;
		}

		private static void DoSwitch(LanguageCode newLang)
		{
			if ((bool)Platform.Current)
			{
				Platform.Current.SharedData.SetString("M2H_lastLanguage", newLang.ToString() ?? "");
				Platform.Current.SharedData.Save();
			}
			currentLanguage = newLang;
			currentEntrySheets = new Dictionary<string, Dictionary<string, string>>();
			string[] sheetTitles = settings.sheetTitles;
			foreach (string text in sheetTitles)
			{
				currentEntrySheets[text] = new Dictionary<string, string>();
				string languageFileContents = GetLanguageFileContents(text);
				if (!(languageFileContents != ""))
				{
					continue;
				}
				using XmlReader xmlReader = XmlReader.Create(new StringReader(languageFileContents));
				while (xmlReader.ReadToFollowing("entry"))
				{
					xmlReader.MoveToFirstAttribute();
					string value = xmlReader.Value;
					xmlReader.MoveToElement();
					string s = xmlReader.ReadElementContentAsString().Trim();
					s = s.UnescapeXML();
					currentEntrySheets[text][value] = s;
				}
			}
			LocalizedAsset[] array = (LocalizedAsset[])UnityEngine.Object.FindObjectsOfType(typeof(LocalizedAsset));
			for (int i = 0; i < array.Length; i++)
			{
				array[i].LocalizeAsset();
			}
			SendMonoMessage("ChangedLanguage", currentLanguage);
			if (!ConfigManager.IsSavingConfig)
			{
				ConfigManager.SaveConfig();
			}
		}

		public static UnityEngine.Object GetAsset(string name)
		{
			return Resources.Load("Languages/Assets/" + CurrentLanguage().ToString() + "/" + name);
		}

		private static bool HasLanguageFile(string lang, string sheetTitle)
		{
			return (TextAsset)Resources.Load("Languages/" + lang + "_" + sheetTitle, typeof(TextAsset)) != null;
		}

		private static string GetLanguageFileContents(string sheetTitle)
		{
			TextAsset textAsset = (TextAsset)Resources.Load("Languages/" + currentLanguage.ToString() + "_" + sheetTitle, typeof(TextAsset));
			if (!(textAsset != null))
			{
				return "";
			}
			return textAsset.text;
		}

		public static LanguageCode CurrentLanguage()
		{
			return currentLanguage;
		}

		public static string Get(string key)
		{
			return Get(key, settings.sheetTitles[0]);
		}

		public static string Get(string key, string sheetTitle)
		{
			return Get(key, sheetTitle);
		}

		public static bool Has(string key)
		{
			return Has(key, settings.sheetTitles[0]);
		}

		public static bool Has(string key, string sheetTitle)
		{
			if (currentEntrySheets == null || !currentEntrySheets.ContainsKey(sheetTitle))
			{
				return false;
			}
			return currentEntrySheets[sheetTitle].ContainsKey(key);
		}

		private static void SendMonoMessage(string methodString, params object[] parameters)
		{
			if (parameters != null && parameters.Length > 1)
			{
				Debug.LogError("We cannot pass more than one argument currently!");
			}
			GameObject[] array = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
			foreach (GameObject gameObject in array)
			{
				if ((bool)gameObject && gameObject.transform.parent == null)
				{
					if (parameters != null && parameters.Length == 1)
					{
						gameObject.gameObject.BroadcastMessage(methodString, parameters[0], SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						gameObject.gameObject.BroadcastMessage(methodString, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}

		public static LanguageCode LanguageNameToCode(SystemLanguage name)
		{
			switch (name)
			{
				case SystemLanguage.Afrikaans:
					return LanguageCode.AF;
				case SystemLanguage.Arabic:
					return LanguageCode.AR;
				case SystemLanguage.Basque:
					return LanguageCode.BA;
				case SystemLanguage.Belarusian:
					return LanguageCode.BE;
				case SystemLanguage.Bulgarian:
					return LanguageCode.BG;
				case SystemLanguage.Catalan:
					return LanguageCode.CA;
				case SystemLanguage.Chinese:
					return LanguageCode.ZH;
				case SystemLanguage.Czech:
					return LanguageCode.CS;
				case SystemLanguage.Danish:
					return LanguageCode.DA;
				case SystemLanguage.Dutch:
					return LanguageCode.NL;
				case SystemLanguage.English:
					return LanguageCode.EN;
				case SystemLanguage.Estonian:
					return LanguageCode.ET;
				case SystemLanguage.Faroese:
					return LanguageCode.FA;
				case SystemLanguage.Finnish:
					return LanguageCode.FI;
				case SystemLanguage.French:
					return LanguageCode.FR;
				case SystemLanguage.German:
					return LanguageCode.DE;
				case SystemLanguage.Greek:
					return LanguageCode.EL;
				case SystemLanguage.Hebrew:
					return LanguageCode.HE;
				case SystemLanguage.Hungarian:
					return LanguageCode.HU;
				case SystemLanguage.Icelandic:
					return LanguageCode.IS;
				case SystemLanguage.Indonesian:
					return LanguageCode.ID;
				case SystemLanguage.Italian:
					return LanguageCode.IT;
				case SystemLanguage.Japanese:
					return LanguageCode.JA;
				case SystemLanguage.Korean:
					return LanguageCode.KO;
				case SystemLanguage.Latvian:
					return LanguageCode.LA;
				case SystemLanguage.Lithuanian:
					return LanguageCode.LT;
				case SystemLanguage.Norwegian:
					return LanguageCode.NO;
				case SystemLanguage.Polish:
					return LanguageCode.PL;
				case SystemLanguage.Portuguese:
					return LanguageCode.PT;
				case SystemLanguage.Romanian:
					return LanguageCode.RO;
				case SystemLanguage.Russian:
					return LanguageCode.RU;
				case SystemLanguage.SerboCroatian:
					return LanguageCode.SH;
				case SystemLanguage.Slovak:
					return LanguageCode.SK;
				case SystemLanguage.Slovenian:
					return LanguageCode.SL;
				case SystemLanguage.Spanish:
					return LanguageCode.ES;
				case SystemLanguage.Swedish:
					return LanguageCode.SW;
				case SystemLanguage.Thai:
					return LanguageCode.TH;
				case SystemLanguage.Turkish:
					return LanguageCode.TR;
				case SystemLanguage.Ukrainian:
					return LanguageCode.UK;
				case SystemLanguage.Vietnamese:
					return LanguageCode.VI;
				default:
					switch (name)
					{
						case SystemLanguage.Hungarian:
							return LanguageCode.HU;
						case SystemLanguage.ChineseSimplified:
							return LanguageCode.ZH;
						case SystemLanguage.ChineseTraditional:
							return LanguageCode.ZH;
						default:
							_ = 42;
							return LanguageCode.N;
					}
			}
		}

		public static string GetInternal(string key, string sheetTitle)
		{
			if (currentEntrySheets == null || !currentEntrySheets.ContainsKey(sheetTitle))
			{
				Debug.LogError("The sheet with title \"" + sheetTitle + "\" does not exist!");
				return string.Empty;
			}
			if (currentEntrySheets[sheetTitle].ContainsKey(key))
			{
				return currentEntrySheets[sheetTitle][key];
			}
			return "#!#" + key + "#!#";
		}

		public static string orig_Get(string key, string sheetTitle)
		{
			if (currentEntrySheets == null || !currentEntrySheets.ContainsKey(sheetTitle))
			{
				Debug.LogError("The sheet with title \"" + sheetTitle + "\" does not exist!");
				return "";
			}
			if (currentEntrySheets[sheetTitle].ContainsKey(key))
			{
				return currentEntrySheets[sheetTitle][key];
			}
			return "#!#" + key + "#!#";
		}
	}
}