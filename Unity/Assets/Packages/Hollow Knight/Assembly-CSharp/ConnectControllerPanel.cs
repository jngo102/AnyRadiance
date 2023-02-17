using InControl;
using UnityEngine;

public class ConnectControllerPanel : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup canvasGroup;

	[SerializeField]
	private float fadeRate;

	protected void Start()
	{
		if (!Platform.Current.WillEverPauseOnControllerDisconnected)
		{
			base.enabled = false;
		}
		else
		{
			UpdateContent();
		}
	}

	protected void Update()
	{
		UpdateContent();
	}

	private void UpdateContent()
	{
		_ = InputManager.ActiveDevice;
		_ = GameManager.instance;
		float num = ((!Platform.Current.IsPausingOnControllerDisconnected) ? 0f : 1f);
		if (canvasGroup.alpha != num)
		{
			canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, num, fadeRate * Time.unscaledDeltaTime);
		}
	}
}
