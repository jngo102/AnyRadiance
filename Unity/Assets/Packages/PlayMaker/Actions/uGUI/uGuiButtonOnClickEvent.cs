using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Fires an event on click for a UGui Slider component.")]
	public class uGuiButtonOnClickEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Button))]
		[Tooltip("The GameObject with the UGui button component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("Send this event when Clicked.")]
		public FsmEvent sendEvent;
	
		private Button button;
	
		public override void Reset()
		{
			gameObject = null;
			sendEvent = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				button = ownerDefaultTarget.GetComponent<Button>();
				if (button != null)
				{
					button.onClick.AddListener(DoOnClick);
				}
				else
				{
					LogError("Missing UI.Button on " + ownerDefaultTarget.name);
				}
			}
			else
			{
				LogError("Missing GameObject ");
			}
		}
	
		public override void OnExit()
		{
			if (button != null)
			{
				button.onClick.RemoveListener(DoOnClick);
			}
		}
	
		public void DoOnClick()
		{
			base.Fsm.Event(sendEvent);
		}
	}
}