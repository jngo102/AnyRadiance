using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Activate a UGui InputField component to begin processing Events. Optionally Deactivate on state exit")]
	public class uGuiInputFieldActivate : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool deactivateOnExit;
	
		private InputField _inputField;
	
		public override void Reset()
		{
			gameObject = null;
			deactivateOnExit = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_inputField = ownerDefaultTarget.GetComponent<InputField>();
			}
			DoAction();
			Finish();
		}
	
		private void DoAction()
		{
			if (_inputField != null)
			{
				_inputField.ActivateInputField();
			}
		}
	
		public override void OnExit()
		{
			if (!(_inputField == null) && deactivateOnExit.Value)
			{
				_inputField.DeactivateInputField();
			}
		}
	}
}