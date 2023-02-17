using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Enemy AI")]
	[Tooltip("Object chases target on Y axis")]
	public class ChaseObjectVertical : RigidBody2dActionBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[UIHint(UIHint.Variable)]
		public FsmOwnerDefault gameObject;
	
		[UIHint(UIHint.Variable)]
		public FsmGameObject target;
	
		public FsmFloat speedMax;
	
		public FsmFloat acceleration;
	
		private FsmGameObject self;
	
		private bool turning;
	
		public override void Reset()
		{
			gameObject = null;
			target = null;
			acceleration = 0f;
			speedMax = 0f;
		}
	
		public override void Awake()
		{
			base.Fsm.HandleFixedUpdate = true;
		}
	
		public override void OnPreprocess()
		{
			base.Fsm.HandleFixedUpdate = true;
		}
	
		public override void OnEnter()
		{
			CacheRigidBody2d(base.Fsm.GetOwnerDefaultTarget(gameObject));
			self = base.Fsm.GetOwnerDefaultTarget(gameObject);
			DoChase();
		}
	
		public override void OnFixedUpdate()
		{
			DoChase();
		}
	
		private void DoChase()
		{
			if (rb2d == null)
			{
				return;
			}
			Vector2 velocity = rb2d.velocity;
			if (self.Value.transform.position.y < target.Value.transform.position.y || self.Value.transform.position.y > target.Value.transform.position.y)
			{
				if (self.Value.transform.position.y < target.Value.transform.position.y)
				{
					velocity.y += acceleration.Value;
				}
				else
				{
					velocity.y -= acceleration.Value;
				}
				if (velocity.y > speedMax.Value)
				{
					velocity.y = speedMax.Value;
				}
				if (velocity.y < 0f - speedMax.Value)
				{
					velocity.y = 0f - speedMax.Value;
				}
				rb2d.velocity = velocity;
			}
		}
	}
}