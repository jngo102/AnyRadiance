using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the text value of a UGui InputField component.")]
	public class uGuiInputFieldSetText : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The text of the UGui InputField component.")]
		public FsmString text;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
	
		private InputField _inputField;
	
		private string _originalString;
	
		public override void Reset()
		{
			gameObject = null;
			text = null;
			resetOnExit = null;
			everyFrame = false;
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
				_originalString = _inputField.text;
			}
			DoSetTextValue();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			DoSetTextValue();
		}
	
		private void DoSetTextValue()
		{
			if (_inputField != null)
			{
				_inputField.text = text.Value;
			}
		}
	
		public override void OnExit()
		{
			if (!(_inputField == null) && resetOnExit.Value)
			{
				_inputField.text = _originalString;
			}
		}
	}
}