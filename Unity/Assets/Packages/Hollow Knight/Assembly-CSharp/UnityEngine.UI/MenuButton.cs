using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	
	public class MenuButton : MenuSelectable, ISubmitHandler, IEventSystemHandler, IPointerClickHandler
	{
		public enum MenuButtonType
		{
			Proceed,
			Activate,
			CustomSubmit
		}
	
		public MenuButtonType buttonType;
	
		public Animator flashEffect;
	
		public Action<MenuButton> submitAction { get; set; }
	
		public bool proceed { get; set; }
	
		private new void Start()
		{
			HookUpAudioPlayer();
		}
	
		public void OnSubmit(BaseEventData eventData)
		{
			if (buttonType == MenuButtonType.CustomSubmit)
			{
				if ((bool)flashEffect)
				{
					flashEffect.ResetTrigger("Flash");
					flashEffect.SetTrigger("Flash");
				}
				if (proceed)
				{
					ForceDeselect();
				}
				submitAction?.Invoke(this);
			}
			orig_OnSubmit(eventData);
		}
	
		public void OnPointerClick(PointerEventData eventData)
		{
			OnSubmit(eventData);
		}
	
		public void orig_OnSubmit(BaseEventData eventData)
		{
			if (buttonType == MenuButtonType.Proceed)
			{
				try
				{
					flashEffect.ResetTrigger("Flash");
					flashEffect.SetTrigger("Flash");
				}
				catch
				{
				}
				ForceDeselect();
			}
			else if (buttonType == MenuButtonType.Activate)
			{
				try
				{
					flashEffect.ResetTrigger("Flash");
					flashEffect.SetTrigger("Flash");
				}
				catch
				{
				}
			}
			PlaySubmitSound();
		}
	}
}