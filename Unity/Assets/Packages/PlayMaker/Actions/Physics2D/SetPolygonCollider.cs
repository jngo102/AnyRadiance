using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Physics 2d")]
	[Tooltip("Set PolygonCollider to active or inactive. Can only be one collider on object. ")]
	public class SetPolygonCollider : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The particle emitting GameObject")]
		public FsmOwnerDefault gameObject;
	
		public FsmBool active;
	
		public override void Reset()
		{
			gameObject = null;
			active = false;
		}
	
		public override void OnEnter()
		{
			if (gameObject != null)
			{
				PolygonCollider2D component = base.Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<PolygonCollider2D>();
				if (component != null)
				{
					component.enabled = active.Value;
				}
			}
			Finish();
		}
	}
}