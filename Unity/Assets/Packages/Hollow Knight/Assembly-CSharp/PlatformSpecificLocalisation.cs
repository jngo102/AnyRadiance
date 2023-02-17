using System;
using UnityEngine;

public class PlatformSpecificLocalisation : MonoBehaviour
{
	[Serializable]
	public struct PlatformKey
	{
		public RuntimePlatform platform;

		public string sheetTitle;

		public string textKey;
	}

	public PlatformKey[] platformKeys;

	private void Awake()
	{
		AutoLocalizeTextUI component = GetComponent<AutoLocalizeTextUI>();
		SetTextMeshProGameText component2 = GetComponent<SetTextMeshProGameText>();
		if (!(component != null) && !(component2 != null))
		{
			return;
		}
		PlatformKey[] array = platformKeys;
		for (int i = 0; i < array.Length; i++)
		{
			PlatformKey platformKey = array[i];
			if (platformKey.platform == Application.platform)
			{
				if (component != null)
				{
					component.sheetTitle = platformKey.sheetTitle;
					component.textKey = platformKey.textKey;
				}
				if (component2 != null)
				{
					component2.sheetName = platformKey.sheetTitle;
					component2.convName = platformKey.textKey;
				}
				break;
			}
		}
	}
}
