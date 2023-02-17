using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
	public CanvasGroup title;

	public Animator topFleur;

	public CanvasGroup content;

	public CanvasGroup controls;

	public Selectable defaultHighlight;

	public Animator bottomFleur;

	public CanvasGroup screenCanvasGroup => GetComponent<CanvasGroup>();

	public void HighlightDefault()
	{
		EventSystem current = EventSystem.current;
		if (!(defaultHighlight != null) || !(current.currentSelectedGameObject == null))
		{
			return;
		}
		Selectable firstInteractable = defaultHighlight.GetFirstInteractable();
		if (!firstInteractable)
		{
			return;
		}
		firstInteractable.Select();
		foreach (Transform item in defaultHighlight.transform)
		{
			Animator component = item.GetComponent<Animator>();
			if (component != null)
			{
				component.ResetTrigger("hide");
				component.SetTrigger("show");
				break;
			}
		}
	}
}
