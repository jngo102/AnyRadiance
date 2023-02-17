using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the value of a UGui Scrollbar component.")]
	public class uGuiScrollbarGetValue : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Scrollbar))]
		[Tooltip("The GameObject with the Scrollbar UGui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The value of the UGui Scrollbar component.")]
		public FsmFloat value;
	
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
	
		private Scrollbar _scrollbar;
	
		public override void Reset()
		{
			gameObject = null;
			value = null;
			everyFrame = false;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_scrollbar = ownerDefaultTarget.GetComponent<Scrollbar>();
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
			if (_scrollbar != null)
			{
				value.Value = _scrollbar.value;
			}
		}
	}
}