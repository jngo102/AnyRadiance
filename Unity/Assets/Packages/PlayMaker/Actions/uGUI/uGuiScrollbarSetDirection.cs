using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the direction of a UGui Scrollbar component.")]
	public class uGuiScrollbarSetDirection : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Scrollbar))]
		[Tooltip("The GameObject with the Scrollbar UGui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[Tooltip("The direction of the UGui Scrollbar component.")]
		public Scrollbar.Direction direction;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		private Scrollbar _scrollbar;
	
		private Scrollbar.Direction _originalValue;
	
		public override void Reset()
		{
			gameObject = null;
			direction = Scrollbar.Direction.LeftToRight;
			resetOnExit = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_scrollbar = ownerDefaultTarget.GetComponent<Scrollbar>();
			}
			if (resetOnExit.Value)
			{
				_originalValue = _scrollbar.direction;
			}
			DoSetValue();
			Finish();
		}
	
		private void DoSetValue()
		{
			if (_scrollbar != null)
			{
				_scrollbar.direction = direction;
			}
		}
	
		public override void OnExit()
		{
			if (!(_scrollbar == null) && resetOnExit.Value)
			{
				_scrollbar.direction = _originalValue;
			}
		}
	}
}