using UnityEngine;

public class EngagementPromptPanel : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup canvasGroup;

	[SerializeField]
	private Animator flashAnimator;

	[SerializeField]
	private float fadeRate;

	private Platform.EngagementStates lastEngagementState;

	protected void Start()
	{
		lastEngagementState = Platform.Current.EngagementState;
		UpdateContent();
	}

	protected void Update()
	{
		UpdateContent();
	}

	private void UpdateContent()
	{
		Platform.EngagementStates engagementState = Platform.Current.EngagementState;
		float target = ((engagementState == Platform.EngagementStates.NotEngaged) ? 1f : 0f);
		canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.unscaledDeltaTime * fadeRate);
		if (lastEngagementState != engagementState)
		{
			if (lastEngagementState == Platform.EngagementStates.NotEngaged)
			{
				UIManager.instance.uiAudioPlayer.PlaySubmit();
				flashAnimator.SetTrigger("Flash");
			}
			else if (engagementState == Platform.EngagementStates.NotEngaged)
			{
				UIManager.instance.uiAudioPlayer.PlayCancel();
			}
			lastEngagementState = engagementState;
		}
	}
}
