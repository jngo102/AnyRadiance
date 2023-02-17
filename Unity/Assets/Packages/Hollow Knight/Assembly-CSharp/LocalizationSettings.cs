using System;
using Language;
using UnityEngine;

[Serializable]
public class LocalizationSettings : ScriptableObject
{
	public string[] sheetTitles;

	public bool useSystemLanguagePerDefault = true;

	public string defaultLangCode = "EN";

	public string gDocsURL;

	public static LanguageCode GetLanguageEnum(string langCode)
	{
		langCode = langCode.ToUpper();
		foreach (LanguageCode value in Enum.GetValues(typeof(LanguageCode)))
		{
			if (value.ToString().Equals(langCode, StringComparison.InvariantCultureIgnoreCase))
			{
				return value;
			}
		}
		Debug.LogError("ERORR: There is no language: [" + langCode + "]");
		return LanguageCode.EN;
	}
}
