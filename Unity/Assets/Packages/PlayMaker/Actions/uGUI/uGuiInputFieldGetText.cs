using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the text value of a UGui InputField component.")]
	public class uGuiInputFieldGetText : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value of the UGui InputField component.")]
		public FsmString text;
	
		public bool everyFrame;
	
		private InputField _inputField;
	
		public override void Reset()
		{
			text = null;
			everyFrame = false;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_inputField = ownerDefaultTarget.GetComponent<InputField>();
			}
			DoGetTextValue();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			DoGetTextValue();
		}
	
		private void DoGetTextValue()
		{
			if (_inputField != null)
			{
				text.Value = _inputField.text;
			}
		}
	}
}