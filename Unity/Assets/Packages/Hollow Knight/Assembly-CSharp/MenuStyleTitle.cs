using System;
using Language;
using UnityEngine;

public class MenuStyleTitle : MonoBehaviour
{
	[Serializable]
	public struct TitleSpriteCollection
	{
		public RuntimePlatform[] PlatformWhitelist;

		public Sprite Default;

		public Sprite Chinese;

		public Sprite Russian;

		public Sprite Italian;

		public Sprite Japanese;

		public Sprite Spanish;

		public Sprite Korean;

		public Sprite French;

		public Sprite BrazilianPT;
	}

	public SpriteRenderer Title;

	public TitleSpriteCollection DefaultTitleSprite;

	public TitleSpriteCollection[] TitleSprites;

	public void SetTitle(int index)
	{
		TitleSpriteCollection titleSpriteCollection = ((index >= 0 && index < TitleSprites.Length) ? TitleSprites[index] : DefaultTitleSprite);
		bool flag = false;
		RuntimePlatform[] platformWhitelist = titleSpriteCollection.PlatformWhitelist;
		foreach (RuntimePlatform runtimePlatform in platformWhitelist)
		{
			if (Application.platform == runtimePlatform)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			titleSpriteCollection = DefaultTitleSprite;
		}
		if ((bool)Title)
		{
			switch (global::Language.Language.CurrentLanguage())
			{
			case LanguageCode.ZH:
				Title.sprite = (titleSpriteCollection.Chinese ? titleSpriteCollection.Chinese : titleSpriteCollection.Default);
				break;
			case LanguageCode.RU:
				Title.sprite = (titleSpriteCollection.Russian ? titleSpriteCollection.Russian : titleSpriteCollection.Default);
				break;
			case LanguageCode.IT:
				Title.sprite = (titleSpriteCollection.Italian ? titleSpriteCollection.Italian : titleSpriteCollection.Default);
				break;
			case LanguageCode.JA:
				Title.sprite = (titleSpriteCollection.Japanese ? titleSpriteCollection.Japanese : titleSpriteCollection.Default);
				break;
			case LanguageCode.ES:
				Title.sprite = (titleSpriteCollection.Spanish ? titleSpriteCollection.Spanish : titleSpriteCollection.Default);
				break;
			case LanguageCode.KO:
				Title.sprite = (titleSpriteCollection.Korean ? titleSpriteCollection.Korean : titleSpriteCollection.Default);
				break;
			case LanguageCode.FR:
				Title.sprite = (titleSpriteCollection.French ? titleSpriteCollection.French : titleSpriteCollection.Default);
				break;
			case LanguageCode.PT:
			case LanguageCode.PT_BR:
				Title.sprite = (titleSpriteCollection.BrazilianPT ? titleSpriteCollection.BrazilianPT : titleSpriteCollection.Default);
				break;
			default:
				Title.sprite = titleSpriteCollection.Default;
				break;
			}
		}
	}
}
