using UnityEngine;

public class ProgressSaveMessagePanel : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup canvasGroup;

	protected void OnEnable()
	{
		canvasGroup.alpha = (Platform.Current.IsSavingAllowedByEngagement ? 1f : 0f);
	}
}
