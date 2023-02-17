using Language;
using UnityEngine;
using UnityEngine.UI;

public class LogoLanguage : MonoBehaviour
{
	public SpriteRenderer spriteRenderer;

	[Space]
	public Image uiImage;

	public bool setNativeSize = true;

	[Space]
	public Sprite englishSprite;

	public Sprite chineseSprite;

	private void OnEnable()
	{
		SetSprite();
	}

	public void SetSprite()
	{
		if (global::Language.Language.CurrentLanguage().ToString() == "ZH")
		{
			if ((bool)spriteRenderer)
			{
				spriteRenderer.sprite = chineseSprite;
			}
			if ((bool)uiImage)
			{
				uiImage.sprite = chineseSprite;
			}
		}
		else
		{
			if ((bool)spriteRenderer)
			{
				spriteRenderer.sprite = englishSprite;
			}
			if ((bool)uiImage)
			{
				uiImage.sprite = englishSprite;
			}
		}
		if ((bool)uiImage && setNativeSize)
		{
			uiImage.SetNativeSize();
		}
	}
}
