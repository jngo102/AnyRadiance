using System;
using System.Collections;
using GlobalEnums;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	
	public class MenuPreventDeselect : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler, ICancelHandler
	{
		[Header("On Cancel")]
		public CancelAction cancelAction;
	
		[Header("Fleurs")]
		public Animator leftCursor;
	
		public Animator rightCursor;
	
		private MenuAudioController uiAudioPlayer;
	
		private GameObject prevSelectedObject;
	
		private bool dontPlaySelectSound;
	
		private bool deselectWasForced;
	
		public Action<MenuPreventDeselect> customCancelAction { get; set; }
	
		private void Start()
		{
			HookUpAudioPlayer();
		}
	
		public void OnSelect(BaseEventData eventData)
		{
			if (leftCursor != null)
			{
				leftCursor.SetTrigger("show");
				leftCursor.ResetTrigger("hide");
			}
			if (rightCursor != null)
			{
				rightCursor.SetTrigger("show");
				rightCursor.ResetTrigger("hide");
			}
			if (!dontPlaySelectSound)
			{
				uiAudioPlayer.PlaySelect();
			}
			else
			{
				dontPlaySelectSound = false;
			}
		}
	
		public void OnDeselect(BaseEventData eventData)
		{
			StartCoroutine(ValidateDeselect());
		}
	
		public void OnCancel(BaseEventData eventData)
		{
			if (cancelAction == CancelAction.GoToExtrasMenu && customCancelAction != null)
			{
				ForceDeselect();
				customCancelAction(this);
				uiAudioPlayer.PlayCancel();
			}
			else
			{
				orig_OnCancel(eventData);
			}
		}
	
		private IEnumerator ValidateDeselect()
		{
			prevSelectedObject = EventSystem.current.currentSelectedGameObject;
			yield return new WaitForEndOfFrame();
			if (EventSystem.current.currentSelectedGameObject != null)
			{
				if (leftCursor != null)
				{
					leftCursor.ResetTrigger("show");
					leftCursor.SetTrigger("hide");
				}
				if (rightCursor != null)
				{
					rightCursor.ResetTrigger("show");
					rightCursor.SetTrigger("hide");
				}
				deselectWasForced = false;
			}
			else if (deselectWasForced)
			{
				if (leftCursor != null)
				{
					leftCursor.ResetTrigger("show");
					leftCursor.SetTrigger("hide");
				}
				if (rightCursor != null)
				{
					rightCursor.ResetTrigger("show");
					rightCursor.SetTrigger("hide");
				}
				deselectWasForced = false;
			}
			else
			{
				deselectWasForced = false;
				dontPlaySelectSound = true;
				EventSystem.current.SetSelectedGameObject(prevSelectedObject);
			}
		}
	
		protected void HookUpAudioPlayer()
		{
			uiAudioPlayer = UIManager.instance.uiAudioPlayer;
		}
	
		public void ForceDeselect()
		{
			if (EventSystem.current.currentSelectedGameObject != null)
			{
				deselectWasForced = true;
				EventSystem.current.SetSelectedGameObject(null);
			}
		}
	
		public void SimulateSubmit()
		{
			ForceDeselect();
			UIManager.instance.uiAudioPlayer.PlaySubmit();
		}
	
		public void orig_OnCancel(BaseEventData eventData)
		{
			bool flag = true;
			if (cancelAction != 0)
			{
				ForceDeselect();
			}
			if (cancelAction == CancelAction.DoNothing)
			{
				flag = false;
			}
			else if (cancelAction == CancelAction.GoToMainMenu)
			{
				UIManager.instance.UIGoToMainMenu();
			}
			else if (cancelAction == CancelAction.GoToOptionsMenu)
			{
				UIManager.instance.UIGoToOptionsMenu();
			}
			else if (cancelAction == CancelAction.GoToVideoMenu)
			{
				UIManager.instance.UIGoToVideoMenu();
			}
			else if (cancelAction == CancelAction.GoToPauseMenu)
			{
				UIManager.instance.UIGoToPauseMenu();
			}
			else if (cancelAction == CancelAction.LeaveOptionsMenu)
			{
				UIManager.instance.UILeaveOptionsMenu();
			}
			else if (cancelAction == CancelAction.GoToExitPrompt)
			{
				UIManager.instance.UIShowQuitGamePrompt();
			}
			else if (cancelAction == CancelAction.GoToProfileMenu)
			{
				UIManager.instance.UIGoToProfileMenu();
			}
			else if (cancelAction == CancelAction.GoToControllerMenu)
			{
				UIManager.instance.UIGoToControllerMenu();
			}
			else if (cancelAction == CancelAction.ApplyRemapGamepadSettings)
			{
				UIManager.instance.ApplyRemapGamepadMenuSettings();
			}
			else if (cancelAction == CancelAction.ApplyAudioSettings)
			{
				UIManager.instance.ApplyAudioMenuSettings();
			}
			else if (cancelAction == CancelAction.ApplyVideoSettings)
			{
				UIManager.instance.ApplyVideoMenuSettings();
			}
			else if (cancelAction == CancelAction.ApplyGameSettings)
			{
				UIManager.instance.ApplyGameMenuSettings();
			}
			else if (cancelAction == CancelAction.ApplyKeyboardSettings)
			{
				UIManager.instance.ApplyKeyboardMenuSettings();
			}
			else if (cancelAction == CancelAction.ApplyControllerSettings)
			{
				UIManager.instance.ApplyControllerMenuSettings();
			}
			else if (cancelAction == CancelAction.GoToExplicitSwitchUser)
			{
				UIManager.instance.UIGoToEngageMenu();
			}
			else if (cancelAction == CancelAction.ReturnToProfileMenu)
			{
				UIManager.instance.UIReturnToProfileMenu();
			}
			else
			{
				Debug.LogError("CancelAction not implemented for this control");
				flag = false;
			}
			if (flag)
			{
				uiAudioPlayer.PlayCancel();
			}
		}
	}
}