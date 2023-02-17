using UnityEngine;

public class SpriteFadePulse : MonoBehaviour
{
	public float lowAlpha;

	public float highAlpha;

	public float fadeDuration;

	private SpriteRenderer spriteRenderer;

	private int state;

	private float currentLerpTime;

	private float currentAlpha;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		FadeIn();
	}

	private void Update()
	{
		float num = currentLerpTime / fadeDuration;
		currentAlpha = Mathf.Lerp(lowAlpha, highAlpha, num);
		Color color = spriteRenderer.color;
		color.a = num;
		spriteRenderer.color = color;
		if (state == 0)
		{
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > fadeDuration)
			{
				FadeOut();
			}
		}
		else
		{
			currentLerpTime -= Time.deltaTime;
			if (currentLerpTime < 0f)
			{
				FadeIn();
			}
		}
	}

	public void FadeIn()
	{
		state = 0;
		currentLerpTime = 0f;
	}

	public void FadeOut()
	{
		state = 1;
		currentLerpTime = fadeDuration;
	}
}
