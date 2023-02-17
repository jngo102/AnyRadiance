using System;
using UnityEngine;

public class PlatformSpecificSprite : MonoBehaviour
{
	[Serializable]
	public struct PlatformSpriteMatch
	{
		public RuntimePlatform Platform;

		public Sprite Sprite;
	}

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private PlatformSpriteMatch[] sprites;

	private void OnEnable()
	{
		if (!spriteRenderer)
		{
			return;
		}
		PlatformSpriteMatch[] array = sprites;
		for (int i = 0; i < array.Length; i++)
		{
			PlatformSpriteMatch platformSpriteMatch = array[i];
			if (platformSpriteMatch.Platform == Application.platform)
			{
				spriteRenderer.sprite = platformSpriteMatch.Sprite;
				break;
			}
		}
	}
}
