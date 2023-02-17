using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the number of distinct scroll positions allowed of a uGui Scrollbar component.")]
	public class uGuiScrollbarSetNumberOfSteps : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Scrollbar))]
		[Tooltip("The GameObject with the Scrollbar UGui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[Tooltip("The number of distinct scroll positions allowed of the uGui Scrollbar component.")]
		public FsmInt value;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
	
		private Scrollbar _scrollbar;
	
		private int _originalValue;
	
		public override void Reset()
		{
			gameObject = null;
			value = null;
			resetOnExit = null;
			everyFrame = false;
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
				_originalValue = _scrollbar.numberOfSteps;
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
			if (_scrollbar != null)
			{
				_scrollbar.numberOfSteps = value.Value;
			}
		}
	
		public override void OnExit()
		{
			if (!(_scrollbar == null) && resetOnExit.Value)
			{
				_scrollbar.numberOfSteps = _originalValue;
			}
		}
	}
}