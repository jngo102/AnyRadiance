using UnityEngine;

public class InvulnerablePulse : MonoBehaviour
{
	public Color invulColor;

	public float pulseDuration;

	private Color normalColor;

	private tk2dSprite sprite;

	private bool pulsing;

	private bool reverse;

	private float currentLerpTime;

	private void Start()
	{
		sprite = GetComponent<tk2dSprite>();
		normalColor = sprite.color;
		pulsing = false;
		currentLerpTime = 0f;
	}

	private void Update()
	{
		if (!pulsing)
		{
			return;
		}
		if (!reverse)
		{
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > pulseDuration)
			{
				currentLerpTime = pulseDuration;
				reverse = true;
			}
		}
		else
		{
			currentLerpTime -= Time.deltaTime;
			if (currentLerpTime < 0f)
			{
				currentLerpTime = 0f;
				reverse = false;
			}
		}
		float t = currentLerpTime / pulseDuration;
		sprite.color = Color.Lerp(normalColor, invulColor, t);
	}

	public void startInvulnerablePulse()
	{
		pulsing = true;
		currentLerpTime = 0f;
	}

	public void stopInvulnerablePulse()
	{
		pulsing = false;
		updateSpriteColor(normalColor);
		currentLerpTime = 0f;
	}

	public void updateSpriteColor(Color color)
	{
		sprite.color = color;
	}
}
