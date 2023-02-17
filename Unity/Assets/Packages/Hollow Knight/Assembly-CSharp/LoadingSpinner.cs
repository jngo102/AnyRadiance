using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSpinner : MonoBehaviour
{
	[SerializeField]
	private Image image;

	[SerializeField]
	private float displayDelay;

	[SerializeField]
	private float fadeDuration;

	[SerializeField]
	private float fadeAmount;

	[SerializeField]
	private float fadeVariance;

	[SerializeField]
	private float fadePulseDuration;

	[SerializeField]
	private Sprite[] sprites;

	[SerializeField]
	private float frameRate;

	private float fadeFactor;

	private float frameTimer;

	private int frameIndex;

	private float displayDelayAdjustment;

	public float DisplayDelayAdjustment
	{
		get
		{
			return displayDelayAdjustment;
		}
		set
		{
			displayDelayAdjustment = value;
		}
	}

	public float DisplayDelay => displayDelay + displayDelayAdjustment;

	protected void OnEnable()
	{
		fadeFactor = 0f;
	}

	protected void Start()
	{
		image.color = new Color(1f, 1f, 1f, 0f);
		image.enabled = false;
		fadeFactor = 0f;
	}

	protected void Update()
	{
		float num = Mathf.Max(1f / 60f, Time.unscaledDeltaTime);
		GameManager unsafeInstance = GameManager.UnsafeInstance;
		if (unsafeInstance != null)
		{
			float target = ((unsafeInstance.CurrentLoadDuration > DisplayDelay && !unsafeInstance.IsUsingCustomLoadAnimation) ? 1f : 0f);
			fadeFactor = Mathf.MoveTowards(fadeFactor, target, num / fadeDuration);
			if (fadeFactor < Mathf.Epsilon)
			{
				if (image.enabled)
				{
					image.enabled = false;
				}
			}
			else
			{
				if (!image.enabled)
				{
					image.enabled = true;
				}
				image.color = new Color(1f, 1f, 1f, fadeFactor * (fadeAmount + fadeVariance * Mathf.Sin(unsafeInstance.CurrentLoadDuration * (float)Math.PI * 2f / fadePulseDuration)));
			}
		}
		if (sprites.Length != 0)
		{
			frameTimer += num * frameRate;
			int num2 = (int)frameTimer;
			if (num2 > 0)
			{
				frameTimer -= num2;
				frameIndex = (frameIndex + num2) % sprites.Length;
				image.sprite = sprites[frameIndex];
			}
		}
	}
}
