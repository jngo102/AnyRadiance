using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PreselectOption : MonoBehaviour
{
	public Selectable itemToHighlight;

	public void HighlightDefault(bool deselect = false)
	{
		if (!(EventSystem.current.currentSelectedGameObject == null || deselect))
		{
			return;
		}
		if (itemToHighlight is MenuSelectable)
		{
			((MenuSelectable)itemToHighlight).DontPlaySelectSound = true;
		}
		itemToHighlight.Select();
		if (itemToHighlight is MenuSelectable)
		{
			((MenuSelectable)itemToHighlight).DontPlaySelectSound = false;
		}
		foreach (Transform item in itemToHighlight.transform)
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

	public void SetDefaultHighlight(Button button)
	{
		itemToHighlight = button;
	}

	public void DeselectAll()
	{
		StartCoroutine(ForceDeselect());
	}

	private IEnumerator ForceDeselect()
	{
		yield return new WaitForSeconds(0.165f);
		UIManager.instance.eventSystem.SetSelectedGameObject(null);
	}
}
