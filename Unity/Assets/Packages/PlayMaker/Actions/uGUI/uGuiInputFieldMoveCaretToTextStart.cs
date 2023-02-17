using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Move Caret to text start on a UGui InputField component. Optionaly select from the current caret position")]
	public class uGuiInputFieldMoveCaretToTextStart : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("Define if we select or not from the current caret position. Default is true = no selection")]
		public FsmBool shift;
	
		private InputField _inputField;
	
		public override void Reset()
		{
			gameObject = null;
			shift = true;
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
				_inputField.MoveTextStart(shift.Value);
			}
		}
	}
}