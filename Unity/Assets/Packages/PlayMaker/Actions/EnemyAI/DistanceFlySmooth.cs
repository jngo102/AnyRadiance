using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Enemy AI")]
	[Tooltip("Flies and keeps a certain distance from target, with smoother movement")]
	public class DistanceFlySmooth : RigidBody2dActionBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[UIHint(UIHint.Variable)]
		public FsmOwnerDefault gameObject;
	
		[UIHint(UIHint.Variable)]
		public FsmGameObject target;
	
		public FsmFloat distance;
	
		public FsmFloat speedMax;
	
		public FsmFloat accelerationForce;
	
		public FsmFloat targetRadius;
	
		public FsmFloat deceleration;
	
		public FsmVector3 offset;
	
		private float distanceAway;
	
		private FsmGameObject self;
	
		public override void Reset()
		{
			gameObject = null;
			target = null;
			accelerationForce = 0f;
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
			distanceAway = Mathf.Sqrt(Mathf.Pow(self.Value.transform.position.x - (target.Value.transform.position.x + offset.Value.x), 2f) + Mathf.Pow(self.Value.transform.position.y - (target.Value.transform.position.y + offset.Value.y), 2f));
			Vector2 velocity = rb2d.velocity;
			if (!(distanceAway > distance.Value - targetRadius.Value) || !(distanceAway < distance.Value + targetRadius.Value))
			{
				Vector2 vector = new Vector2(target.Value.transform.position.x + offset.Value.x - self.Value.transform.position.x, target.Value.transform.position.y + offset.Value.y - self.Value.transform.position.y);
				vector = Vector2.ClampMagnitude(vector, 1f);
				vector = new Vector2(vector.x * accelerationForce.Value, vector.y * accelerationForce.Value);
				if (distanceAway < distance.Value)
				{
					vector = new Vector2(0f - vector.x, 0f - vector.y);
				}
				rb2d.AddForce(vector);
				velocity = Vector2.ClampMagnitude(velocity, speedMax.Value);
				rb2d.velocity = velocity;
				return;
			}
			velocity = rb2d.velocity;
			if (velocity.x < 0f)
			{
				velocity.x *= deceleration.Value;
				if (velocity.x > 0f)
				{
					velocity.x = 0f;
				}
			}
			else if (velocity.x > 0f)
			{
				velocity.x *= deceleration.Value;
				if (velocity.x < 0f)
				{
					velocity.x = 0f;
				}
			}
			if (velocity.y < 0f)
			{
				velocity.y *= deceleration.Value;
				if (velocity.y > 0f)
				{
					velocity.y = 0f;
				}
			}
			else if (velocity.y > 0f)
			{
				velocity.y *= deceleration.Value;
				if (velocity.y < 0f)
				{
					velocity.y = 0f;
				}
			}
			rb2d.velocity = velocity;
		}
	}
}