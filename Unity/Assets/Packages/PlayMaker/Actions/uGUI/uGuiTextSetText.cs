using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the text value of a UGui Text component.")]
	public class uGuiTextSetText : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Text))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The text of the UGui Text component.")]
		public FsmString text;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
	
		private Text _text;
	
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
				_text = ownerDefaultTarget.GetComponent<Text>();
			}
			if (resetOnExit.Value)
			{
				_originalString = _text.text;
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
			if (_text != null)
			{
				_text.text = text.Value;
			}
		}
	
		public override void OnExit()
		{
			if (!(_text == null) && resetOnExit.Value)
			{
				_text.text = _originalString;
			}
		}
	}
}