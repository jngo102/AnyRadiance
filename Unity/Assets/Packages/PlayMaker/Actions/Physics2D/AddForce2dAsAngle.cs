using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Physics 2d")]
	[Tooltip("Adds a 2d force to a Game Object. Use Vector2 variable and/or Float variables for each axis. I added the ability to limit speed.")]
	public class AddForce2dAsAngle : RigidBody2dActionBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject to apply the force to.")]
		public FsmOwnerDefault gameObject;
	
		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally apply the force at a position on the object. This will also add some torque. The position is often returned from MousePick or GetCollision2dInfo actions.")]
		public FsmVector2 atPosition;
	
		[RequiredField]
		public FsmFloat angle;
	
		[RequiredField]
		public FsmFloat speed;
	
		private float x;
	
		private float y;
	
		public FsmFloat maxSpeed;
	
		public FsmFloat maxSpeedX;
	
		public FsmFloat maxSpeedY;
	
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	
		public override void Reset()
		{
			gameObject = null;
			atPosition = new FsmVector2
			{
				UseVariable = true
			};
			angle = null;
			speed = null;
			maxSpeed = null;
			maxSpeedX = null;
			maxSpeedY = null;
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
			DoAddForce();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnFixedUpdate()
		{
			DoAddForce();
		}
	
		private void DoAddForce()
		{
			x = speed.Value * Mathf.Cos(angle.Value * ((float)Math.PI / 180f));
			y = speed.Value * Mathf.Sin(angle.Value * ((float)Math.PI / 180f));
			if (!rb2d)
			{
				return;
			}
			Vector2 force = new Vector2(x, y);
			if (!atPosition.IsNone)
			{
				rb2d.AddForceAtPosition(force, atPosition.Value);
			}
			else
			{
				rb2d.AddForce(force);
			}
			if (!maxSpeedX.IsNone)
			{
				Vector2 velocity = rb2d.velocity;
				if (velocity.x > maxSpeedX.Value)
				{
					velocity = new Vector2(maxSpeedX.Value, velocity.y);
				}
				if (velocity.x < 0f - maxSpeedX.Value)
				{
					velocity = new Vector2(0f - maxSpeedX.Value, velocity.y);
				}
				rb2d.velocity = velocity;
			}
			if (!maxSpeedY.IsNone)
			{
				Vector2 velocity2 = rb2d.velocity;
				if (velocity2.y > maxSpeedY.Value)
				{
					velocity2 = new Vector2(velocity2.x, maxSpeedY.Value);
				}
				if (velocity2.y < 0f - maxSpeedY.Value)
				{
					velocity2 = new Vector2(velocity2.x, 0f - maxSpeedY.Value);
				}
				rb2d.velocity = velocity2;
			}
			if (!maxSpeed.IsNone)
			{
				Vector2 velocity3 = rb2d.velocity;
				velocity3 = Vector2.ClampMagnitude(velocity3, maxSpeed.Value);
				rb2d.velocity = velocity3;
			}
		}
	}
}