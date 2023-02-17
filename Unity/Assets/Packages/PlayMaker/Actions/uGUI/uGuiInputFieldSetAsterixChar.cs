using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the Asterix Character of a UGui InputField component.")]
	public class uGuiInputFieldSetAsterixChar : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The asterix Character used for password field type of the UGui InputField component. Only the first character will be used, the rest of the string will be ignored")]
		public FsmString asterixChar;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		private InputField _inputField;
	
		private char _originalValue;
	
		private static char __char__ = ' ';
	
		public override void Reset()
		{
			gameObject = null;
			asterixChar = "*";
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
				_originalValue = _inputField.asteriskChar;
			}
			DoSetValue();
			Finish();
		}
	
		private void DoSetValue()
		{
			char asteriskChar = __char__;
			if (asterixChar.Value.Length > 0)
			{
				asteriskChar = asterixChar.Value[0];
			}
			if (_inputField != null)
			{
				_inputField.asteriskChar = asteriskChar;
			}
		}
	
		public override void OnExit()
		{
			if (!(_inputField == null) && resetOnExit.Value)
			{
				_inputField.asteriskChar = _originalValue;
			}
		}
	}
}