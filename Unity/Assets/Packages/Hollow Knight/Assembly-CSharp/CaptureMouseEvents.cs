using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CaptureMouseEvents : UIBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Selectable forwardTarget;

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log(base.name + "pointer enter");
		if ((bool)forwardTarget)
		{
			forwardTarget.OnPointerEnter(eventData);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log(base.name + "pointer exit");
		if ((bool)forwardTarget)
		{
			forwardTarget.OnPointerExit(eventData);
		}
	}
}
