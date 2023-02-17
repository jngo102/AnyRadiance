using UnityEngine;
using UnityEngine.UI;

public class AchievementPopup : MonoBehaviour
{
	public delegate void SelfEvent(AchievementPopup sender);

	private enum FadeState
	{
		None,
		FadeUp,
		Wait,
		FadeDown,
		Finish
	}

	public Image image;

	public Text nameText;

	public Text descriptionText;

	private CanvasGroup group;

	[Space]
	public float fadeInTime = 0.25f;

	public float holdTime = 3f;

	public float fadeOutTime = 0.5f;

	[Space]
	public AudioSource audioPlayerPrefab;

	public AudioEvent sound;

	[Space]
	public Animator fluerAnimator;

	public string fluerCloseName = "Close";

	private FadeState currentState;

	private FadeState previousState;

	private float elapsed;

	public event SelfEvent OnFinish;

	private void Awake()
	{
		group = GetComponent<CanvasGroup>();
	}

	public void Setup(Sprite icon, string name, string description)
	{
		if ((bool)image)
		{
			image.sprite = icon;
		}
		if ((bool)nameText)
		{
			nameText.text = name;
		}
		if ((bool)descriptionText)
		{
			descriptionText.text = description;
		}
		sound.SpawnAndPlayOneShot(audioPlayerPrefab, Vector3.zero);
		currentState = FadeState.FadeUp;
	}

	private void Update()
	{
		switch (currentState)
		{
		case FadeState.FadeUp:
			if (currentState != previousState)
			{
				elapsed = 0f;
				previousState = currentState;
			}
			group.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeInTime);
			elapsed += Time.unscaledDeltaTime;
			if (elapsed >= fadeInTime)
			{
				group.alpha = 1f;
				currentState = FadeState.Wait;
			}
			break;
		case FadeState.Wait:
			if (currentState != previousState)
			{
				elapsed = 0f;
				previousState = currentState;
			}
			elapsed += Time.unscaledDeltaTime;
			if (elapsed >= holdTime)
			{
				currentState = FadeState.FadeDown;
			}
			break;
		case FadeState.FadeDown:
			if (currentState != previousState)
			{
				elapsed = 0f;
				previousState = currentState;
				if ((bool)fluerAnimator)
				{
					fluerAnimator.Play(fluerCloseName);
				}
			}
			group.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeInTime);
			elapsed += Time.unscaledDeltaTime;
			if (elapsed >= fadeInTime)
			{
				group.alpha = 0f;
				currentState = FadeState.Finish;
			}
			break;
		case FadeState.Finish:
			if (currentState != previousState)
			{
				previousState = currentState;
				if (this.OnFinish != null)
				{
					this.OnFinish(this);
				}
				else
				{
					base.gameObject.SetActive(value: false);
				}
			}
			break;
		}
	}
}
