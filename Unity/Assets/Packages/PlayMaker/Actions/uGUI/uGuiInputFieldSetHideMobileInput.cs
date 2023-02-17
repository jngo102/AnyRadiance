using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the hide mobile Input value of a UGui InputField component.")]
	public class uGuiInputFieldSetHideMobileInput : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The hide Mobile flag value of the UGui InputField component.")]
		public FsmBool hideMobileInput;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		private InputField _inputField;
	
		private bool _originalValue;
	
		public override void Reset()
		{
			gameObject = null;
			hideMobileInput = null;
			resetOnExit = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_inputField = ownerDefaultTarget.GetComponent<InputField>();
			}
			if (resetOnExit.Value)
			{
				_originalValue = _inputField.shouldHideMobileInput;
			}
			DoSetValue();
			Finish();
		}
	
		private void DoSetValue()
		{
			if (_inputField != null)
			{
				_inputField.shouldHideMobileInput = hideMobileInput.Value;
			}
		}
	
		public override void OnExit()
		{
			if (!(_inputField == null) && resetOnExit.Value)
			{
				_inputField.shouldHideMobileInput = _originalValue;
			}
		}
	}
}