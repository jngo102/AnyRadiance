using UnityEngine;

public class Recoil : MonoBehaviour
{
	public delegate void FreezeEvent();

	public delegate void CancelRecoilEvent();

	private enum States
	{
		Ready,
		Frozen,
		Recoiling
	}

	private Rigidbody2D body;

	private Collider2D bodyCollider;

	[SerializeField]
	public bool freezeInPlace;

	[SerializeField]
	private bool stopVelocityXWhenRecoilingUp;

	[SerializeField]
	private bool preventRecoilUp;

	[SerializeField]
	private float recoilSpeedBase;

	[SerializeField]
	private float recoilDuration;

	private bool skipFreezingByController;

	private States state;

	private float recoilTimeRemaining;

	private float recoilSpeed;

	private Sweep recoilSweep;

	private bool isRecoilSweeping;

	private const int SweepLayerMask = 256;

	public bool SkipFreezingByController
	{
		get
		{
			return skipFreezingByController;
		}
		set
		{
			skipFreezingByController = value;
		}
	}

	public bool IsRecoiling
	{
		get
		{
			if (state != States.Recoiling)
			{
				return state == States.Frozen;
			}
			return true;
		}
	}

	public event FreezeEvent OnHandleFreeze;

	public event CancelRecoilEvent OnCancelRecoil;

	protected void Reset()
	{
		freezeInPlace = false;
		stopVelocityXWhenRecoilingUp = true;
		recoilDuration = 0.5f;
		recoilSpeedBase = 15f;
		preventRecoilUp = false;
	}

	protected void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		bodyCollider = GetComponent<Collider2D>();
	}

	private void OnEnable()
	{
		CancelRecoil();
	}

	public void RecoilByHealthManagerFSMParameters()
	{
		PlayMakerFSM playMakerFSM = PlayMakerFSM.FindFsmOnGameObject(base.gameObject, "health_manager_enemy");
		int cardinalDirection = DirectionUtils.GetCardinalDirection(playMakerFSM.FsmVariables.GetFsmFloat("Attack Direction").Value);
		_ = playMakerFSM.FsmVariables.GetFsmInt("Attack Type").Value;
		float value = playMakerFSM.FsmVariables.GetFsmFloat("Attack Magnitude").Value;
		RecoilByDirection(cardinalDirection, value);
	}

	public void RecoilByDamage(HitInstance damageInstance)
	{
		int cardinalDirection = DirectionUtils.GetCardinalDirection(damageInstance.Direction);
		RecoilByDirection(cardinalDirection, damageInstance.MagnitudeMultiplier);
	}

	public void RecoilByDirection(int attackDirection, float attackMagnitude)
	{
		if (state != 0)
		{
			return;
		}
		if (freezeInPlace)
		{
			Freeze();
		}
		else if (attackDirection != 1 || !preventRecoilUp)
		{
			if (bodyCollider == null)
			{
				bodyCollider = GetComponent<Collider2D>();
			}
			state = States.Recoiling;
			recoilSpeed = recoilSpeedBase * attackMagnitude;
			recoilSweep = new Sweep(bodyCollider, attackDirection, 3);
			isRecoilSweeping = true;
			recoilTimeRemaining = recoilDuration;
			switch (attackDirection)
			{
			case 2:
				FSMUtility.SendEventToGameObject(base.gameObject, "RECOIL HORIZONTAL");
				FSMUtility.SendEventToGameObject(base.gameObject, "HIT LEFT");
				break;
			case 0:
				FSMUtility.SendEventToGameObject(base.gameObject, "RECOIL HORIZONTAL");
				FSMUtility.SendEventToGameObject(base.gameObject, "HIT RIGHT");
				break;
			case 3:
				FSMUtility.SendEventToGameObject(base.gameObject, "HIT DOWN");
				break;
			case 1:
				FSMUtility.SendEventToGameObject(base.gameObject, "HIT UP");
				break;
			}
			UpdatePhysics(0f);
		}
	}

	public void CancelRecoil()
	{
		if (state != 0)
		{
			state = States.Ready;
			if (this.OnCancelRecoil != null)
			{
				this.OnCancelRecoil();
			}
		}
	}

	private void Freeze()
	{
		if (skipFreezingByController)
		{
			if (this.OnHandleFreeze != null)
			{
				this.OnHandleFreeze();
			}
			state = States.Ready;
			return;
		}
		state = States.Frozen;
		if (body != null)
		{
			body.velocity = Vector2.zero;
		}
		PlayMakerFSM playMakerFSM = PlayMakerFSM.FindFsmOnGameObject(base.gameObject, "Climber Control");
		if (playMakerFSM != null)
		{
			playMakerFSM.SendEvent("FREEZE IN PLACE");
		}
		recoilTimeRemaining = recoilDuration;
		UpdatePhysics(0f);
	}

	protected void FixedUpdate()
	{
		UpdatePhysics(Time.fixedDeltaTime);
	}

	private void UpdatePhysics(float deltaTime)
	{
		if (state == States.Frozen)
		{
			if (body != null)
			{
				body.velocity = Vector2.zero;
			}
			recoilTimeRemaining -= deltaTime;
			if (recoilTimeRemaining <= 0f)
			{
				CancelRecoil();
			}
		}
		else
		{
			if (state != States.Recoiling)
			{
				return;
			}
			if (isRecoilSweeping)
			{
				if (recoilSweep.Check(base.transform.position, recoilSpeed * deltaTime, 256, out var clippedDistance))
				{
					isRecoilSweeping = false;
				}
				if (clippedDistance > Mathf.Epsilon)
				{
					base.transform.Translate(recoilSweep.Direction * clippedDistance, Space.World);
				}
			}
			recoilTimeRemaining -= deltaTime;
			if (recoilTimeRemaining <= 0f)
			{
				CancelRecoil();
			}
		}
	}

	public void SetRecoilSpeed(float newSpeed)
	{
		recoilSpeedBase = newSpeed;
	}
}
