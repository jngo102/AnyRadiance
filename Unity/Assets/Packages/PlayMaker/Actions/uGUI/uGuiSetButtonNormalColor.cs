using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the button normal color value of a UGui button component. With reset on exit option ")]
	public class uGuiSetButtonNormalColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Button))]
		[Tooltip("The GameObject with the button ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The new color of the UGui Button component.")]
		public FsmColor normalColor;
	
		[Tooltip("Reset when exiting this state.")]
		public bool resetOnExit;
	
		[Tooltip("Bypass button to drive the action by bool. Action will not be performed if False")]
		public FsmBool enabled = true;
	
		[Tooltip("Runs everyframe. Useful to animate values over time.")]
		public bool everyFrame;
	
		private Button _Button;
	
		private Color _OriginalNormalColor;
	
		public override void Reset()
		{
			normalColor = null;
			resetOnExit = false;
			everyFrame = false;
			enabled = true;
		}
	
		public override void OnEnter()
		{
			Initialize(base.Fsm.GetOwnerDefaultTarget(gameObject));
			if (_Button != null && resetOnExit)
			{
				_OriginalNormalColor = _Button.colors.normalColor;
			}
			DoSetButtonColor();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			DoSetButtonColor();
		}
	
		public override void OnExit()
		{
			if (resetOnExit)
			{
				DoSetOldColorValue();
			}
		}
	
		private void Initialize(GameObject go)
		{
			if (go == null)
			{
				LogError("Missing Button Component!");
				return;
			}
			_Button = go.GetComponent<Button>();
			if (_Button == null)
			{
				LogError("Missing UI.Button on " + go.name);
			}
		}
	
		private void DoSetButtonColor()
		{
			if (_Button != null && enabled.Value)
			{
				ColorBlock colors = _Button.colors;
				colors.normalColor = normalColor.Value;
				_Button.colors = colors;
			}
		}
	
		private void DoSetOldColorValue()
		{
			if (_Button != null && enabled.Value)
			{
				ColorBlock colors = _Button.colors;
				colors.normalColor = _OriginalNormalColor;
				_Button.colors = colors;
			}
		}
	}
}