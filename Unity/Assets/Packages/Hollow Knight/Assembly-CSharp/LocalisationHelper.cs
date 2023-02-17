using System.Collections.Generic;
using UnityEngine;

public static class LocalisationHelper
{
	public enum FontSource
	{
		Trajan,
		Perpetua
	}

	private static Dictionary<FontSource, Dictionary<string, string>> substitutions = new Dictionary<FontSource, Dictionary<string, string>> { 
	{
		FontSource.Trajan,
		new Dictionary<string, string> { { "ß", "ss" } }
	} };

	public static string GetProcessed(this string text, FontSource fontSource)
	{
		if (substitutions.ContainsKey(fontSource))
		{
			string text2 = text;
			foreach (KeyValuePair<string, string> item in substitutions[fontSource])
			{
				text2 = text2.Replace(item.Key, item.Value);
			}
			if (text2 != text)
			{
				Debug.Log($"LocalisationHelper processed string \"<b>{text}</b>\", result: \"<b>{text2}</b>\".");
				text = text2;
			}
		}
		return text;
	}
}
