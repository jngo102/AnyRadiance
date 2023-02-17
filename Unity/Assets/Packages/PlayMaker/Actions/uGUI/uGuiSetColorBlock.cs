using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the Color Block of a Selectable Ugui component. Modifications will not be visible if transition is not ColorTint")]
	public class uGuiSetColorBlock : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("The fade duration value. Leave to none for no effect")]
		public FsmFloat fadeDuration;
	
		[Tooltip("The color multiplier value. Leave to none for no effect")]
		public FsmFloat colorMultiplier;
	
		[Tooltip("The normal color value. Leave to none for no effect")]
		public FsmColor normalColor;
	
		[Tooltip("The pressed color value. Leave to none for no effect")]
		public FsmColor pressedColor;
	
		[Tooltip("The highlighted color value. Leave to none for no effect")]
		public FsmColor highlightedColor;
	
		[Tooltip("The disabled color value. Leave to none for no effect")]
		public FsmColor disabledColor;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;
	
		private Selectable _selectable;
	
		private ColorBlock _colorBlock;
	
		private ColorBlock _originalColorBlock;
	
		public override void Reset()
		{
			gameObject = null;
			fadeDuration = new FsmFloat
			{
				UseVariable = true
			};
			colorMultiplier = new FsmFloat
			{
				UseVariable = true
			};
			normalColor = new FsmColor
			{
				UseVariable = true
			};
			highlightedColor = new FsmColor
			{
				UseVariable = true
			};
			pressedColor = new FsmColor
			{
				UseVariable = true
			};
			disabledColor = new FsmColor
			{
				UseVariable = true
			};
			resetOnExit = null;
			everyFrame = false;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_selectable = ownerDefaultTarget.GetComponent<Selectable>();
			}
			if (_selectable != null && resetOnExit.Value)
			{
				_originalColorBlock = _selectable.colors;
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
			if (!(_selectable == null))
			{
				_colorBlock = _selectable.colors;
				if (!colorMultiplier.IsNone)
				{
					_colorBlock.colorMultiplier = colorMultiplier.Value;
				}
				if (!fadeDuration.IsNone)
				{
					_colorBlock.fadeDuration = fadeDuration.Value;
				}
				if (!normalColor.IsNone)
				{
					_colorBlock.normalColor = normalColor.Value;
				}
				if (!pressedColor.IsNone)
				{
					_colorBlock.pressedColor = pressedColor.Value;
				}
				if (!highlightedColor.IsNone)
				{
					_colorBlock.highlightedColor = highlightedColor.Value;
				}
				if (!disabledColor.IsNone)
				{
					_colorBlock.disabledColor = disabledColor.Value;
				}
				_selectable.colors = _colorBlock;
			}
		}
	
		public override void OnExit()
		{
			if (!(_selectable == null) && resetOnExit.Value)
			{
				_selectable.colors = _originalColorBlock;
			}
		}
	}
}