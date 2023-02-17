using System;
using Language;
using UnityEngine;

public class ActivatePerLanguage : MonoBehaviour
{
	[Serializable]
	public struct LangBoolPair
	{
		public LanguageCode language;

		public bool activate;
	}

	public GameObject target;

	public GameObject alt;

	[Space]
	public LangBoolPair[] languages;

	[Space]
	public bool defaultActivation = true;

	private void Start()
	{
		LanguageCode languageCode = global::Language.Language.CurrentLanguage();
		LangBoolPair[] array = languages;
		for (int i = 0; i < array.Length; i++)
		{
			LangBoolPair langBoolPair = array[i];
			if (langBoolPair.language == languageCode)
			{
				if ((bool)target)
				{
					target.SetActive(langBoolPair.activate);
				}
				if ((bool)alt)
				{
					alt.SetActive(!langBoolPair.activate);
				}
				return;
			}
		}
		target.SetActive(defaultActivation);
	}
}
