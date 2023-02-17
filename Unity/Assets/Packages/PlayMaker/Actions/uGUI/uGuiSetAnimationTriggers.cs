using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the Animation Triggers of a Selectable Ugui component. Modifications will not be visible if transition is not Animation")]
	public class uGuiSetAnimationTriggers : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("The normal trigger value. Leave to none for no effect")]
		public FsmString normalTrigger;
	
		[Tooltip("The highlighted trigger value. Leave to none for no effect")]
		public FsmString highlightedTrigger;
	
		[Tooltip("The pressed trigger value. Leave to none for no effect")]
		public FsmString pressedTrigger;
	
		[Tooltip("The disabled trigger value. Leave to none for no effect")]
		public FsmString disabledTrigger;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		private Selectable _selectable;
	
		private AnimationTriggers _animationTriggers;
	
		private AnimationTriggers _originalAnimationTriggers;
	
		public override void Reset()
		{
			gameObject = null;
			normalTrigger = new FsmString
			{
				UseVariable = true
			};
			highlightedTrigger = new FsmString
			{
				UseVariable = true
			};
			pressedTrigger = new FsmString
			{
				UseVariable = true
			};
			disabledTrigger = new FsmString
			{
				UseVariable = true
			};
			resetOnExit = null;
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
				_originalAnimationTriggers = _selectable.animationTriggers;
			}
			DoSetValue();
			Finish();
		}
	
		private void DoSetValue()
		{
			if (!(_selectable == null))
			{
				_animationTriggers = _selectable.animationTriggers;
				if (!normalTrigger.IsNone)
				{
					_animationTriggers.normalTrigger = normalTrigger.Value;
				}
				if (!highlightedTrigger.IsNone)
				{
					_animationTriggers.highlightedTrigger = highlightedTrigger.Value;
				}
				if (!pressedTrigger.IsNone)
				{
					_animationTriggers.pressedTrigger = pressedTrigger.Value;
				}
				if (!disabledTrigger.IsNone)
				{
					_animationTriggers.disabledTrigger = disabledTrigger.Value;
				}
				_selectable.animationTriggers = _animationTriggers;
			}
		}
	
		public override void OnExit()
		{
			if (!(_selectable == null) && resetOnExit.Value)
			{
				_selectable.animationTriggers = _originalAnimationTriggers;
			}
		}
	}
}