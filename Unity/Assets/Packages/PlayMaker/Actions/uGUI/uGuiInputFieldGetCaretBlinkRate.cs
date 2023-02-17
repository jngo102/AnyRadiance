using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the caret's blink rate of a UGui InputField component.")]
	public class uGuiInputFieldGetCaretBlinkRate : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The caret's blink rate for the UGui InputField component.")]
		public FsmFloat caretBlinkRate;
	
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;
	
		private InputField _inputField;
	
		public override void Reset()
		{
			caretBlinkRate = null;
			everyFrame = false;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_inputField = ownerDefaultTarget.GetComponent<InputField>();
			}
			DoGetValue();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			DoGetValue();
		}
	
		private void DoGetValue()
		{
			if (_inputField != null)
			{
				caretBlinkRate.Value = _inputField.caretBlinkRate;
			}
		}
	}
}