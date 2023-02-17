using UnityEngine;

public class SimpleSpriteFade : MonoBehaviour
{
	public Color fadeInColor;

	private SpriteRenderer spriteRenderer;

	private Color normalColor;

	public float fadeDuration;

	private bool fadingIn;

	private bool fadingOut;

	private float currentLerpTime;

	public bool fadeInOnStart;

	public bool deactivateOnFadeIn;

	public bool recycleOnFadeIn;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		normalColor = spriteRenderer.color;
	}

	private void OnEnable()
	{
		spriteRenderer.color = normalColor;
		if (fadeInOnStart)
		{
			FadeIn();
		}
	}

	private void Update()
	{
		if (!fadingIn && !fadingOut)
		{
			return;
		}
		if (fadingIn)
		{
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > fadeDuration)
			{
				currentLerpTime = fadeDuration;
				fadingIn = false;
				if (recycleOnFadeIn)
				{
					base.gameObject.Recycle();
				}
				if (deactivateOnFadeIn)
				{
					base.gameObject.SetActive(value: false);
				}
			}
		}
		else if (fadingOut)
		{
			currentLerpTime -= Time.deltaTime;
			if (currentLerpTime < 0f)
			{
				currentLerpTime = 0f;
				fadingOut = false;
			}
		}
		float t = currentLerpTime / fadeDuration;
		spriteRenderer.color = Color.Lerp(normalColor, fadeInColor, t);
	}

	public void FadeIn()
	{
		fadingIn = true;
		currentLerpTime = 0f;
	}

	public void FadeOut()
	{
		fadingOut = true;
		currentLerpTime = fadeDuration;
	}
}
