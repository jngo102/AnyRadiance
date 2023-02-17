using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("RectTransform")]
	[Tooltip("Get the scale of RectTransform")]
	public class RectTransformGetScale : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;
	
		[UIHint(UIHint.Variable)]
		public FsmFloat xScale;
	
		[UIHint(UIHint.Variable)]
		public FsmFloat yScale;
	
		private RectTransform _rt;
	
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			xScale = null;
			yScale = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			DoGetValues();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnActionUpdate()
		{
			DoGetValues();
		}
	
		private void DoGetValues()
		{
			if (!xScale.IsNone)
			{
				xScale.Value = _rt.localScale.x;
			}
			if (!yScale.IsNone)
			{
				yScale.Value = _rt.localScale.y;
			}
		}
	}
}