using System;
using UnityEngine;
using UnityEngine.UI;

public class StandaloneLoadingSpinner : MonoBehaviour
{
	[SerializeField]
	private Camera renderingCamera;

	[SerializeField]
	private Image backgroundImage;

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

	private float timeRunning;

	private bool isComplete;

	private GameManager lastGameManager;

	public void Setup(GameManager lastGameManager)
	{
		this.lastGameManager = lastGameManager;
	}

	protected void OnEnable()
	{
		fadeFactor = 0f;
	}

	protected void Start()
	{
		image.color = new Color(1f, 1f, 1f, 0f);
		image.enabled = false;
		fadeFactor = 0f;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	protected void LateUpdate()
	{
		GameManager unsafeInstance = GameManager.UnsafeInstance;
		if (lastGameManager == null && unsafeInstance != null && (lastGameManager != unsafeInstance || lastGameManager == null) && !isComplete)
		{
			renderingCamera.enabled = false;
			isComplete = true;
		}
		timeRunning += Time.unscaledDeltaTime;
		float num = Mathf.Max(1f / 60f, Time.unscaledDeltaTime);
		float target = ((timeRunning > displayDelay && !isComplete) ? 1f : 0f);
		fadeFactor = Mathf.MoveTowards(fadeFactor, target, num / fadeDuration);
		if (fadeFactor < Mathf.Epsilon)
		{
			if (image.enabled)
			{
				image.enabled = false;
			}
			if (isComplete)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else
		{
			if (!image.enabled)
			{
				image.enabled = true;
			}
			image.color = new Color(1f, 1f, 1f, fadeFactor * (fadeAmount + fadeVariance * Mathf.Sin(timeRunning * (float)Math.PI * 2f / fadePulseDuration)));
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
