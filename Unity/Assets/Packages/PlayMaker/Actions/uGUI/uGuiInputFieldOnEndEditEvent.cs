using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Fires an event when editing ended for a UGui InputField component. Event string data will feature the text value")]
	public class uGuiInputFieldOnEndEditEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("Send this event when editing ended.")]
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
					_inputField.onEndEdit.AddListener(DoOnEndEdit);
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
				_inputField.onEndEdit.RemoveListener(DoOnEndEdit);
			}
		}
	
		public void DoOnEndEdit(string value)
		{
			Fsm.EventData.StringData = value;
			base.Fsm.Event(sendEvent);
		}
	}
}