using UnityEngine;

public class Walker : MonoBehaviour
{
	private enum States
	{
		NotReady,
		WaitingForConditions,
		Stopped,
		Walking,
		Turning
	}

	public enum StopReasons
	{
		Bored,
		Controlled
	}

	[Header("Structure")]
	[SerializeField]
	private LineOfSightDetector lineOfSightDetector;

	[SerializeField]
	private AlertRange alertRange;

	private Rigidbody2D body;

	private Collider2D bodyCollider;

	private tk2dSpriteAnimator animator;

	private AudioSource audioSource;

	private Camera mainCamera;

	private HeroController hero;

	private const float CameraDistanceForActivation = 60f;

	private const float WaitHeroXThreshold = 1f;

	[Header("Configuration")]
	[SerializeField]
	private bool ambush;

	[SerializeField]
	private string idleClip;

	[SerializeField]
	private string turnClip;

	[SerializeField]
	private string walkClip;

	[SerializeField]
	private float edgeXAdjuster;

	[SerializeField]
	private bool preventScaleChange;

	[SerializeField]
	private bool preventTurn;

	[SerializeField]
	private float pauseTimeMin;

	[SerializeField]
	private float pauseTimeMax;

	[SerializeField]
	private float pauseWaitMin;

	[SerializeField]
	private float pauseWaitMax;

	[SerializeField]
	private bool pauses;

	[SerializeField]
	private float rightScale;

	[SerializeField]
	public bool startInactive;

	[SerializeField]
	private int turnAfterIdlePercentage;

	[SerializeField]
	private float turnPause;

	[SerializeField]
	private bool waitForHeroX;

	[SerializeField]
	private float waitHeroX;

	[SerializeField]
	public float walkSpeedL;

	[SerializeField]
	public float walkSpeedR;

	[SerializeField]
	public bool ignoreHoles;

	[SerializeField]
	private bool preventTurningToFaceHero;

	private States state;

	private bool didFulfilCameraDistanceCondition;

	private bool didFulfilHeroXCondition;

	private int currentFacing;

	private int turningFacing;

	private float walkTimeRemaining;

	private float pauseTimeRemaining;

	private float turnCooldownRemaining;

	private StopReasons stopReason;

	protected void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		bodyCollider = GetComponent<Collider2D>();
		animator = GetComponent<tk2dSpriteAnimator>();
		audioSource = GetComponent<AudioSource>();
	}

	protected void Start()
	{
		mainCamera = GameCameras.instance.mainCamera;
		hero = HeroController.instance;
		if (currentFacing == 0)
		{
			currentFacing = ((base.transform.localScale.x * rightScale >= 0f) ? 1 : (-1));
		}
		if (state == States.NotReady)
		{
			turnCooldownRemaining = 0f - Mathf.Epsilon;
			BeginWaitingForConditions();
		}
	}

	protected void Update()
	{
		turnCooldownRemaining -= Time.deltaTime;
		switch (state)
		{
		case States.WaitingForConditions:
			UpdateWaitingForConditions();
			break;
		case States.Stopped:
			UpdateStopping();
			break;
		case States.Walking:
			UpdateWalking();
			break;
		case States.Turning:
			UpdateTurning();
			break;
		}
	}

	public void StartMoving()
	{
		if (state == States.Stopped || state == States.WaitingForConditions)
		{
			startInactive = false;
			int facing = ((currentFacing != 0) ? currentFacing : ((Random.Range(0, 2) != 0) ? 1 : (-1)));
			BeginWalkingOrTurning(facing);
		}
		Update();
	}

	public void CancelTurn()
	{
		if (state == States.Turning)
		{
			BeginWalking(currentFacing);
		}
	}

	public void Go(int facing)
	{
		turnCooldownRemaining = 0f - Mathf.Epsilon;
		if (state == States.Stopped || state == States.Walking)
		{
			BeginWalkingOrTurning(facing);
		}
		else if (state == States.Turning && currentFacing == facing)
		{
			CancelTurn();
		}
		Update();
	}

	public void RecieveGoMessage(int facing)
	{
		if (state != States.Stopped || stopReason != StopReasons.Controlled)
		{
			Go(facing);
		}
	}

	public void Stop(StopReasons reason)
	{
		BeginStopped(reason);
	}

	public void ChangeFacing(int facing)
	{
		if (state == States.Turning)
		{
			turningFacing = facing;
			currentFacing = -facing;
		}
		else
		{
			currentFacing = facing;
		}
	}

	private void BeginWaitingForConditions()
	{
		state = States.WaitingForConditions;
		didFulfilCameraDistanceCondition = false;
		didFulfilHeroXCondition = false;
		UpdateWaitingForConditions();
	}

	private void UpdateWaitingForConditions()
	{
		if (!didFulfilCameraDistanceCondition && (mainCamera.transform.position - base.transform.position).sqrMagnitude < 3600f)
		{
			didFulfilCameraDistanceCondition = true;
		}
		if (didFulfilCameraDistanceCondition && !didFulfilHeroXCondition && hero != null && Mathf.Abs(hero.transform.GetPositionX() - waitHeroX) < 1f)
		{
			didFulfilHeroXCondition = true;
		}
		if (didFulfilCameraDistanceCondition && (!waitForHeroX || didFulfilHeroXCondition) && !startInactive && !ambush)
		{
			BeginStopped(StopReasons.Bored);
			StartMoving();
		}
	}

	private void BeginStopped(StopReasons reason)
	{
		state = States.Stopped;
		stopReason = reason;
		if ((bool)audioSource)
		{
			audioSource.Stop();
		}
		if (reason == StopReasons.Bored)
		{
			tk2dSpriteAnimationClip clipByName = animator.GetClipByName(idleClip);
			if (clipByName != null)
			{
				animator.Play(clipByName);
			}
			body.velocity = Vector2.Scale(body.velocity, new Vector2(0f, 1f));
			if (pauses)
			{
				pauseTimeRemaining = Random.Range(pauseTimeMin, pauseTimeMax);
			}
			else
			{
				EndStopping();
			}
		}
	}

	private void UpdateStopping()
	{
		if (stopReason == StopReasons.Bored)
		{
			pauseTimeRemaining -= Time.deltaTime;
			if (pauseTimeRemaining <= 0f)
			{
				EndStopping();
			}
		}
	}

	private void EndStopping()
	{
		if (currentFacing == 0)
		{
			BeginWalkingOrTurning((Random.Range(0, 2) == 0) ? 1 : (-1));
		}
		else if (Random.Range(0, 100) < turnAfterIdlePercentage)
		{
			BeginTurning(-currentFacing);
		}
		else
		{
			BeginWalking(currentFacing);
		}
	}

	private void BeginWalkingOrTurning(int facing)
	{
		if (currentFacing == facing)
		{
			BeginWalking(facing);
		}
		else
		{
			BeginTurning(facing);
		}
	}

	private void BeginWalking(int facing)
	{
		state = States.Walking;
		animator.Play(walkClip);
		if (!preventScaleChange)
		{
			base.transform.SetScaleX((float)facing * rightScale);
		}
		walkTimeRemaining = Random.Range(pauseWaitMin, pauseWaitMax);
		if ((bool)audioSource)
		{
			audioSource.Play();
		}
		body.velocity = new Vector2((facing > 0) ? walkSpeedR : walkSpeedL, body.velocity.y);
	}

	private void UpdateWalking()
	{
		if (turnCooldownRemaining <= 0f)
		{
			if (new Sweep(bodyCollider, 1 - currentFacing, 3).Check(base.transform.position, bodyCollider.bounds.extents.x + 0.5f, 256))
			{
				BeginTurning(-currentFacing);
				return;
			}
			if (!preventTurningToFaceHero && hero != null && hero.transform.GetPositionX() > base.transform.GetPositionX() != currentFacing > 0 && lineOfSightDetector != null && lineOfSightDetector.CanSeeHero && alertRange != null && alertRange.IsHeroInRange)
			{
				BeginTurning(-currentFacing);
				return;
			}
			if (!ignoreHoles && !new Sweep(bodyCollider, 3, 3).Check((Vector2)base.transform.position + new Vector2((bodyCollider.bounds.extents.x + 0.5f + edgeXAdjuster) * (float)currentFacing, 0f), 0.25f, 256))
			{
				BeginTurning(-currentFacing);
				return;
			}
		}
		if (pauses)
		{
			walkTimeRemaining -= Time.deltaTime;
			if (walkTimeRemaining <= 0f)
			{
				BeginStopped(StopReasons.Bored);
				return;
			}
		}
		body.velocity = new Vector2((currentFacing > 0) ? walkSpeedR : walkSpeedL, body.velocity.y);
	}

	private void BeginTurning(int facing)
	{
		state = States.Turning;
		turningFacing = facing;
		if (preventTurn)
		{
			EndTurning();
			return;
		}
		turnCooldownRemaining = turnPause;
		body.velocity = Vector2.Scale(body.velocity, new Vector2(0f, 1f));
		animator.Play(turnClip);
		FSMUtility.SendEventToGameObject(base.gameObject, (facing > 0) ? "TURN RIGHT" : "TURN LEFT");
	}

	private void UpdateTurning()
	{
		body.velocity = Vector2.Scale(body.velocity, new Vector2(0f, 1f));
		if (!animator.Playing)
		{
			EndTurning();
		}
	}

	private void EndTurning()
	{
		currentFacing = turningFacing;
		BeginWalking(currentFacing);
	}

	public void ClearTurnCooldown()
	{
		turnCooldownRemaining = 0f - Mathf.Epsilon;
	}
}
