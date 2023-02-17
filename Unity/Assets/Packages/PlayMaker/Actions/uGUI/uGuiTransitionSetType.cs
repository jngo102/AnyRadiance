using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the transition type of a Selectable Ugui component.")]
	public class uGuiTransitionSetType : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("The transition value")]
		public Selectable.Transition transition;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		private Selectable _selectable;
	
		private Selectable.Transition _originalTransition;
	
		public override void Reset()
		{
			gameObject = null;
			transition = Selectable.Transition.ColorTint;
			resetOnExit = false;
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
				_originalTransition = _selectable.transition;
			}
			DoSetValue();
			Finish();
		}
	
		private void DoSetValue()
		{
			if (_selectable != null)
			{
				_selectable.transition = transition;
			}
		}
	
		public override void OnExit()
		{
			if (!(_selectable == null) && resetOnExit.Value)
			{
				_selectable.transition = _originalTransition;
			}
		}
	}
}