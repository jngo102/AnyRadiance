using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Physics 2d")]
	[Tooltip("Accelerates objects velocity, and clamps top speed")]
	public class AccelerateVelocity : RigidBody2dActionBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		public FsmOwnerDefault gameObject;
	
		public FsmFloat xAccel;
	
		public FsmFloat yAccel;
	
		public FsmFloat xMaxSpeed;
	
		public FsmFloat yMaxSpeed;
	
		public override void Reset()
		{
			gameObject = null;
			xAccel = new FsmFloat
			{
				UseVariable = true
			};
			yAccel = new FsmFloat
			{
				UseVariable = true
			};
			xMaxSpeed = new FsmFloat
			{
				UseVariable = true
			};
			yMaxSpeed = new FsmFloat
			{
				UseVariable = true
			};
		}
	
		public override void Awake()
		{
			base.Fsm.HandleFixedUpdate = true;
		}
	
		public override void OnEnter()
		{
			CacheRigidBody2d(base.Fsm.GetOwnerDefaultTarget(gameObject));
		}
	
		public override void OnPreprocess()
		{
			base.Fsm.HandleFixedUpdate = true;
		}
	
		public override void OnFixedUpdate()
		{
			DoSetVelocity();
		}
	
		private void DoSetVelocity()
		{
			if (!(rb2d == null))
			{
				Vector2 velocity = rb2d.velocity;
				if (!xAccel.IsNone)
				{
					float value = velocity.x + xAccel.Value;
					value = Mathf.Clamp(value, 0f - xMaxSpeed.Value, xMaxSpeed.Value);
					velocity = new Vector2(value, velocity.y);
				}
				if (!yAccel.IsNone)
				{
					float value2 = velocity.y + yAccel.Value;
					value2 = Mathf.Clamp(value2, 0f - yMaxSpeed.Value, yMaxSpeed.Value);
					velocity = new Vector2(velocity.x, value2);
				}
				rb2d.velocity = velocity;
			}
		}
	}
}