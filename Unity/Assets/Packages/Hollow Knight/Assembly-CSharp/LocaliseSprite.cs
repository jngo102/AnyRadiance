using System;
using Language;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LocaliseSprite : MonoBehaviour
{
	[Serializable]
	public struct LangSpritePair
	{
		public LanguageCode language;

		public Sprite sprite;
	}

	public LangSpritePair[] sprites;

	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		LanguageCode languageCode = global::Language.Language.CurrentLanguage();
		LangSpritePair[] array = sprites;
		for (int i = 0; i < array.Length; i++)
		{
			LangSpritePair langSpritePair = array[i];
			if (langSpritePair.language == languageCode)
			{
				spriteRenderer.sprite = langSpritePair.sprite;
				break;
			}
		}
	}
}
