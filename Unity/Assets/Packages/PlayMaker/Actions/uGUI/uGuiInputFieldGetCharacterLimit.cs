using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the Character Limit value of a UGui InputField component. This is the maximum number of characters that the user can type into the field.")]
	public class uGuiInputFieldGetCharacterLimit : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The maximum number of characters that the user can type into the UGui InputField component.")]
		public FsmInt characterLimit;
	
		[Tooltip("Event sent if limit is infinite (equal to 0)")]
		public FsmEvent hasNoLimitEvent;
	
		[Tooltip("Event sent if limit is more than 0")]
		public FsmEvent isLimitedEvent;
	
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;
	
		private InputField _inputField;
	
		public override void Reset()
		{
			characterLimit = null;
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
				characterLimit.Value = _inputField.characterLimit;
				if (_inputField.characterLimit > 0)
				{
					base.Fsm.Event(isLimitedEvent);
				}
				else
				{
					base.Fsm.Event(hasNoLimitEvent);
				}
			}
		}
	}
}