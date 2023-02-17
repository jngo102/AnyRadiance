using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the placeHolder GameObject of a UGui InputField component.")]
	public class uGuiInputFieldGetPlaceHolder : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[UIHint(UIHint.Variable)]
		[Tooltip("The placeholder of the UGui InputField component.")]
		public FsmGameObject placeHolder;
	
		[Tooltip("true if placeholder is found")]
		public FsmBool placeHolderDefined;
	
		[Tooltip("Event sent if no placeholder is defined")]
		public FsmEvent foundEvent;
	
		[Tooltip("Event sent if a placeholder is defined")]
		public FsmEvent notFoundEvent;
	
		private InputField _inputField;
	
		public override void Reset()
		{
			placeHolder = null;
			placeHolderDefined = null;
			foundEvent = null;
			notFoundEvent = null;
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
				Graphic placeholder = _inputField.placeholder;
				placeHolderDefined.Value = placeholder != null;
				if (placeholder != null)
				{
					placeHolder.Value = placeholder.gameObject;
					base.Fsm.Event(foundEvent);
				}
				else
				{
					base.Fsm.Event(notFoundEvent);
				}
			}
		}
	}
}