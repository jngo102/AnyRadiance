using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Physics 2d")]
	[Tooltip("Sets the 2d Velocity of a Game Object, using an angle and a speed value. For the angle, 0 is to the right and the degrees increase clockwise.")]
	public class SetVelocityAsAngle : RigidBody2dActionBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		public FsmFloat angle;
	
		[RequiredField]
		public FsmFloat speed;
	
		private FsmFloat x;
	
		private FsmFloat y;
	
		public bool everyFrame;
	
		public override void Reset()
		{
			gameObject = null;
			angle = new FsmFloat
			{
				UseVariable = true
			};
			speed = new FsmFloat
			{
				UseVariable = true
			};
			everyFrame = false;
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
			DoSetVelocity();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnFixedUpdate()
		{
			DoSetVelocity();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		private void DoSetVelocity()
		{
			if (!(rb2d == null))
			{
				x = speed.Value * Mathf.Cos(angle.Value * ((float)Math.PI / 180f));
				y = speed.Value * Mathf.Sin(angle.Value * ((float)Math.PI / 180f));
				Vector2 velocity = default(Vector2);
				velocity.x = x.Value;
				velocity.y = y.Value;
				rb2d.velocity = velocity;
			}
		}
	}
}