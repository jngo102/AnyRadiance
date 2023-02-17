using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the hide Mobile Input value of a UGui InputField component.")]
	public class uGuiInputFieldGetHideMobileInput : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The hide Mobile flag value of the UGui InputField component.")]
		public FsmBool hideMobileInput;
	
		[Tooltip("Event sent if hide mobile input property is true")]
		public FsmEvent mobileInputHiddenEvent;
	
		[Tooltip("Event sent if hide mobile input property is false")]
		public FsmEvent mobileInputShownEvent;
	
		private InputField _inputField;
	
		public override void Reset()
		{
			hideMobileInput = null;
			mobileInputHiddenEvent = null;
			mobileInputShownEvent = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_inputField = ownerDefaultTarget.GetComponent<InputField>();
			}
			DoGetValue();
			Finish();
		}
	
		private void DoGetValue()
		{
			if (_inputField != null)
			{
				hideMobileInput.Value = _inputField.shouldHideMobileInput;
				if (_inputField.shouldHideMobileInput)
				{
					base.Fsm.Event(mobileInputHiddenEvent);
				}
				else
				{
					base.Fsm.Event(mobileInputShownEvent);
				}
			}
		}
	}
}