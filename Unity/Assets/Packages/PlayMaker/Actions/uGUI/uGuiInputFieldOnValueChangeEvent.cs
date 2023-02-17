using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Fires an event on value change for a UGui InputField component. Event string data will feature the text value")]
	public class uGuiInputFieldOnValueChangeEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("Send this event when value changed.")]
		public FsmEvent sendEvent;
	
		private InputField _inputField;
	
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
				_inputField = ownerDefaultTarget.GetComponent<InputField>();
				if (_inputField != null)
				{
					_inputField.onValueChange.AddListener(DoOnValueChange);
				}
				else
				{
					LogError("Missing UI.InputField on " + ownerDefaultTarget.name);
				}
			}
			else
			{
				LogError("Missing GameObject");
			}
		}
	
		public override void OnExit()
		{
			if (_inputField != null)
			{
				_inputField.onValueChange.RemoveListener(DoOnValueChange);
			}
		}
	
		public void DoOnValueChange(string value)
		{
			Fsm.EventData.StringData = value;
			base.Fsm.Event(sendEvent);
		}
	}
}