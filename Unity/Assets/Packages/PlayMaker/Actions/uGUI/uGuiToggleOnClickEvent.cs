using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Fires an event on value changed for a UGui Toggle component. Event bool data will feature the Toggle value")]
	public class uGuiToggleOnClickEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Toggle))]
		[Tooltip("The GameObject with the Toggle ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("Send this event when value changed.")]
		public FsmEvent sendEvent;
	
		private Toggle _toggle;
	
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
				_toggle = ownerDefaultTarget.GetComponent<Toggle>();
				if (_toggle != null)
				{
					_toggle.onValueChanged.AddListener(DoOnValueChanged);
				}
				else
				{
					LogError("Missing UI.Toggle on " + ownerDefaultTarget.name);
				}
			}
			else
			{
				LogError("Missing GameObject");
			}
		}
	
		public override void OnExit()
		{
			if (_toggle != null)
			{
				_toggle.onValueChanged.RemoveListener(DoOnValueChanged);
			}
		}
	
		public void DoOnValueChanged(bool value)
		{
			Fsm.EventData.BoolData = value;
			base.Fsm.Event(sendEvent);
		}
	}
}