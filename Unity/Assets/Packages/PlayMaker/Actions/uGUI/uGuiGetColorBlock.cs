using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the Color Block of a Selectable Ugui component.")]
	public class uGuiGetColorBlock : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("The fade duration value. Leave to none for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmFloat fadeDuration;
	
		[Tooltip("The color multiplier value. Leave to none for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmFloat colorMultiplier;
	
		[Tooltip("The normal color value. Leave to none for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor normalColor;
	
		[Tooltip("The pressed color value. Leave to none for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor pressedColor;
	
		[Tooltip("The highlighted color value. Leave to none for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor highlightedColor;
	
		[Tooltip("The disabled color value. Leave to none for no effect")]
		[UIHint(UIHint.Variable)]
		public FsmColor disabledColor;
	
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;
	
		private Selectable _selectable;
	
		public override void Reset()
		{
			gameObject = null;
			fadeDuration = null;
			colorMultiplier = null;
			normalColor = null;
			highlightedColor = null;
			pressedColor = null;
			disabledColor = null;
			everyFrame = false;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_selectable = ownerDefaultTarget.GetComponent<Selectable>();
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
			if (!(_selectable == null))
			{
				if (!colorMultiplier.IsNone)
				{
					colorMultiplier.Value = _selectable.colors.colorMultiplier;
				}
				if (!fadeDuration.IsNone)
				{
					fadeDuration.Value = _selectable.colors.fadeDuration;
				}
				if (!normalColor.IsNone)
				{
					normalColor.Value = _selectable.colors.normalColor;
				}
				if (!pressedColor.IsNone)
				{
					pressedColor.Value = _selectable.colors.pressedColor;
				}
				if (!highlightedColor.IsNone)
				{
					highlightedColor.Value = _selectable.colors.highlightedColor;
				}
				if (!disabledColor.IsNone)
				{
					disabledColor.Value = _selectable.colors.disabledColor;
				}
			}
		}
	}
}