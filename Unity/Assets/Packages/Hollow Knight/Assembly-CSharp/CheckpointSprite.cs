using UnityEngine;
using UnityEngine.UI;

public class CheckpointSprite : MonoBehaviour
{
	private enum States
	{
		NotStarted,
		Starting,
		Looping,
		Ending
	}

	private Image image;

	private AudioSource audioSource;

	[SerializeField]
	private Sprite[] startSprites;

	[SerializeField]
	private Sprite[] loopSprites;

	[SerializeField]
	private Sprite[] endSprites;

	[SerializeField]
	private float framesPerSecond;

	private bool isShowing;

	private States state;

	private float frameTimer;

	protected void Awake()
	{
		image = GetComponent<Image>();
		audioSource = GetComponent<AudioSource>();
	}

	protected void OnEnable()
	{
		state = States.NotStarted;
		image.enabled = false;
		Update(0f);
	}

	protected void Start()
	{
	}

	public void Show()
	{
		isShowing = true;
		if (base.isActiveAndEnabled)
		{
			Update(0f);
		}
	}

	public void Hide()
	{
		isShowing = false;
	}

	protected void Update()
	{
		Update(Mathf.Min(1f / 60f, Time.unscaledDeltaTime));
	}

	private void Update(float deltaTime)
	{
		bool flag = false;
		frameTimer += deltaTime * framesPerSecond;
		if (state == States.NotStarted && isShowing)
		{
			frameTimer = 0f;
			state = States.Starting;
			audioSource.Play();
			image.enabled = true;
		}
		if (state == States.Starting)
		{
			int num = (int)frameTimer;
			if (num < startSprites.Length)
			{
				image.sprite = startSprites[num];
			}
			else
			{
				frameTimer -= startSprites.Length;
				if (isShowing || flag)
				{
					state = States.Looping;
				}
				else
				{
					state = States.Ending;
				}
			}
		}
		if (state == States.Looping)
		{
			int num2 = (int)frameTimer;
			if (num2 >= loopSprites.Length)
			{
				frameTimer -= loopSprites.Length * (num2 / loopSprites.Length);
				if (!isShowing && !flag)
				{
					state = States.Ending;
				}
				else
				{
					image.sprite = loopSprites[num2 % loopSprites.Length];
				}
			}
			else
			{
				image.sprite = loopSprites[num2];
			}
		}
		if (state == States.Ending)
		{
			int num3 = (int)frameTimer;
			if (num3 < endSprites.Length)
			{
				image.sprite = endSprites[num3];
				return;
			}
			image.enabled = false;
			state = States.NotStarted;
		}
	}
}
