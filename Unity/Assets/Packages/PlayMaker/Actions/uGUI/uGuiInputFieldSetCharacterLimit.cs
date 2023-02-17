using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the maximum number of characters that the user can type into a UGui InputField component. Optionally reset on exit")]
	public class uGuiInputFieldSetCharacterLimit : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[Tooltip("The maximum number of characters that the user can type into the UGui InputField component. 0 = infinite")]
		public FsmInt characterLimit;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
	
		private InputField _inputField;
	
		private int _originalValue;
	
		public override void Reset()
		{
			gameObject = null;
			characterLimit = null;
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
				_originalValue = _inputField.characterLimit;
			}
			DoSetValue();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			DoSetValue();
		}
	
		private void DoSetValue()
		{
			if (_inputField != null)
			{
				_inputField.characterLimit = characterLimit.Value;
			}
		}
	
		public override void OnExit()
		{
			if (!(_inputField == null) && resetOnExit.Value)
			{
				_inputField.characterLimit = _originalValue;
			}
		}
	}
}