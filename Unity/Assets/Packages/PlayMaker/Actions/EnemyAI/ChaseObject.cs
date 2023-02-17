using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Enemy AI")]
	[Tooltip("Object buzzes towards target")]
	public class ChaseObject : RigidBody2dActionBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[UIHint(UIHint.Variable)]
		public FsmOwnerDefault gameObject;
	
		[UIHint(UIHint.Variable)]
		public FsmGameObject target;
	
		public FsmFloat speedMax;
	
		public FsmFloat acceleration;
	
		public FsmFloat targetSpread;
	
		public FsmFloat spreadResetTimeMin;
	
		public FsmFloat spreadResetTimeMax;
	
		private bool spreadSet;
	
		private float spreadResetTime;
	
		private float spreadX;
	
		private float spreadY;
	
		private FsmGameObject self;
	
		private float timer;
	
		private float spreadResetTimer;
	
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
			DoBuzz();
		}
	
		public override void OnFixedUpdate()
		{
			DoBuzz();
		}
	
		private void DoBuzz()
		{
			if (rb2d == null)
			{
				return;
			}
			if (targetSpread.Value > 0f)
			{
				if (timer >= spreadResetTime)
				{
					spreadX = Random.Range(0f - targetSpread.Value, targetSpread.Value);
					spreadY = Random.Range(0f - targetSpread.Value, targetSpread.Value);
					timer = 0f;
					spreadResetTime = Random.Range(spreadResetTimeMin.Value, spreadResetTimeMax.Value);
				}
				else
				{
					timer += Time.deltaTime;
				}
			}
			Vector2 velocity = rb2d.velocity;
			if (self.Value.transform.position.x < target.Value.transform.position.x + spreadX)
			{
				velocity.x += acceleration.Value;
			}
			else
			{
				velocity.x -= acceleration.Value;
			}
			if (self.Value.transform.position.y < target.Value.transform.position.y + spreadY)
			{
				velocity.y += acceleration.Value;
			}
			else
			{
				velocity.y -= acceleration.Value;
			}
			if (velocity.x > speedMax.Value)
			{
				velocity.x = speedMax.Value;
			}
			if (velocity.x < 0f - speedMax.Value)
			{
				velocity.x = 0f - speedMax.Value;
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