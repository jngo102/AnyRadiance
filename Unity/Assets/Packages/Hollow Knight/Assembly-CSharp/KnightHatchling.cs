using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHatchling : MonoBehaviour
{
	[Serializable]
	public struct TypeDetails
	{
		public int damage;

		public AudioEvent birthSound;

		public Color spatterColor;

		public bool dung;

		public Transform groundPoint;

		public string attackAnim;

		public string flyAnim;

		public string hatchAnim;

		public string teleEndAnim;

		public string teleStartAnim;

		public string turnAttackAnim;

		public string turnFlyAnim;

		public string restStartAnim;

		public string restEndAnim;
	}

	public enum State
	{
		None,
		Follow,
		Tele,
		Attack,
		BenchRestStart,
		BenchRestLower,
		BenchResting
	}

	public TriggerEnterEvent enemyRange;

	public TriggerEnterEvent groundRange;

	public Collider2D terrainCollider;

	private List<Collider2D> groundColliders = new List<Collider2D>();

	private GameObject target;

	public TypeDetails normalDetails = new TypeDetails
	{
		damage = 9,
		dung = false,
		attackAnim = "Attack",
		flyAnim = "Fly",
		hatchAnim = "Hatch",
		teleEndAnim = "Tele End",
		teleStartAnim = "Tele Start",
		turnAttackAnim = "TurnToAttack",
		turnFlyAnim = "TurnToFly",
		restStartAnim = "Rest Start",
		restEndAnim = "Rest End",
		spatterColor = Color.black
	};

	public TypeDetails dungDetails = new TypeDetails
	{
		damage = 4,
		dung = true,
		attackAnim = "D Attack",
		flyAnim = "D Fly",
		hatchAnim = "D Hatch",
		teleEndAnim = "D Tele End",
		teleStartAnim = "D Tele Start",
		turnAttackAnim = "D TurnToAttack",
		turnFlyAnim = "D TurnToFly",
		restStartAnim = "D Rest Start",
		restEndAnim = "D Rest End",
		spatterColor = new Color(0.749f, 0.522f, 0.353f)
	};

	private TypeDetails details;

	public ParticleSystem dungPt;

	public AudioClip[] loopClips;

	public AudioClip attackChargeClip;

	public AudioSource audioSourcePrefab;

	public AudioEvent explodeSound = new AudioEvent
	{
		PitchMin = 0.85f,
		PitchMax = 1.15f,
		Volume = 1f
	};

	public AudioEvent dungExplodeSound = new AudioEvent
	{
		PitchMin = 0.9f,
		PitchMax = 1.1f,
		Volume = 1f
	};

	public AudioEventRandom dungSleepPlopSound = new AudioEventRandom
	{
		PitchMin = 0.9f,
		PitchMax = 1.1f,
		Volume = 1f
	};

	public GameObject dungExplosionPrefab;

	private State currentState;

	private float targetRadius;

	private Vector3 offset;

	private float awayTimer;

	private float attackFinishTime;

	private float benchRestWaitTime;

	private bool quickSpawn;

	private bool dreamSpawn;

	private float startZ;

	private float sleepZ;

	public DamageEnemies damageEnemies;

	private AudioSource audioSource;

	private MeshRenderer meshRenderer;

	private tk2dSpriteAnimator animator;

	private Rigidbody2D body;

	private Collider2D col;

	private SpriteFlash spriteFlash;

	private float pauseTimer;

	private float xScale;

	private float startX;

	private float startY;

	private float accelY;

	private float accelX;

	private float waitTime;

	public bool IsGrounded => groundColliders.Count > 0;

	public State CurrentState
	{
		get
		{
			return currentState;
		}
		private set
		{
			if (currentState != value)
			{
				PreviousState = currentState;
			}
			currentState = value;
		}
	}

	public State LastFrameState { get; private set; }

	public State PreviousState { get; private set; }

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		meshRenderer = GetComponent<MeshRenderer>();
		animator = GetComponent<tk2dSpriteAnimator>();
		body = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		spriteFlash = GetComponent<SpriteFlash>();
	}

	private void Start()
	{
		if ((bool)enemyRange)
		{
			enemyRange.OnTriggerStayed += delegate(Collider2D collision, GameObject obj)
			{
				if (!target)
				{
					if (collision.tag == "Hatchling Magnet")
					{
						target = collision.gameObject;
					}
					else if (collision.tag != "Ignore Hatchling" && Physics2D.Linecast(base.transform.position, collision.transform.position, LayerMask.GetMask("Terrain", "Soft Terrain")).collider == null)
					{
						target = collision.gameObject;
					}
				}
				if (CurrentState == State.Follow && (bool)target)
				{
					CurrentState = State.Attack;
				}
			};
			enemyRange.OnTriggerExited += delegate(Collider2D collision, GameObject obj)
			{
				if (CurrentState != State.Attack && (bool)target && target == collision.gameObject)
				{
					target = null;
				}
			};
		}
		if ((bool)groundRange)
		{
			groundRange.OnTriggerEntered += delegate(Collider2D collision, GameObject obj)
			{
				if (!groundColliders.Contains(collision))
				{
					groundColliders.Add(collision);
				}
			};
			groundRange.OnTriggerExited += delegate(Collider2D collision, GameObject obj)
			{
				if (groundColliders.Contains(collision))
				{
					groundColliders.Remove(collision);
				}
			};
		}
		startZ = UnityEngine.Random.Range(0.0041f, 0.0049f);
		sleepZ = UnityEngine.Random.Range(0.003f, 0.0035f);
		base.transform.SetPositionZ(startZ);
	}

	private void OnEnable()
	{
		if (GameManager.instance.entryGateName == "dreamGate")
		{
			dreamSpawn = true;
		}
		PlayerData playerData = GameManager.instance.playerData;
		details = (playerData.GetBool("equippedCharm_10") ? dungDetails : normalDetails);
		if ((bool)audioSource)
		{
			audioSource.pitch = UnityEngine.Random.Range(0.85f, 1.15f);
			if (loopClips.Length != 0)
			{
				audioSource.clip = loopClips[UnityEngine.Random.Range(0, loopClips.Length)];
			}
		}
		if (playerData.GetBool("equippedCharm_6") && playerData.GetInt("health") == 1 && (!playerData.GetBool("equippedCharm_27") || playerData.GetInt("healthBlue") <= 0))
		{
			if ((bool)spriteFlash)
			{
				spriteFlash.FlashingFury();
			}
			details.damage += 5;
		}
		if ((bool)dungPt)
		{
			if (details.dung && !dreamSpawn)
			{
				dungPt.Play();
			}
			else
			{
				dungPt.Stop();
			}
		}
		if ((bool)enemyRange)
		{
			enemyRange.gameObject.SetActive(value: false);
		}
		if ((bool)groundRange)
		{
			groundRange.gameObject.SetActive(value: true);
		}
		if ((bool)col)
		{
			col.enabled = false;
		}
		groundColliders.Clear();
		target = null;
		LastFrameState = State.None;
		CurrentState = State.None;
		if ((bool)terrainCollider)
		{
			terrainCollider.enabled = true;
		}
		if ((bool)meshRenderer)
		{
			meshRenderer.enabled = false;
		}
		StartCoroutine(Spawn());
	}

	private void OnDisable()
	{
		quickSpawn = false;
		dreamSpawn = false;
	}

	private void FixedUpdate()
	{
		State state = CurrentState;
		switch (CurrentState)
		{
		case State.Follow:
		{
			if (LastFrameState != State.Follow)
			{
				if ((bool)damageEnemies)
				{
					damageEnemies.damageDealt = 0;
				}
				if ((bool)col)
				{
					col.enabled = true;
				}
				if ((bool)enemyRange)
				{
					enemyRange.gameObject.SetActive(value: true);
				}
				if ((bool)animator)
				{
					animator.Play(details.flyAnim);
				}
				if ((bool)audioSource)
				{
					audioSource.Play();
				}
				body.isKinematic = false;
				targetRadius = UnityEngine.Random.Range(0.1f, 0.75f);
				offset = new Vector3(UnityEngine.Random.Range(-3.5f, 3.5f), UnityEngine.Random.Range(0.25f, 2f));
				awayTimer = 0f;
				base.transform.SetPositionZ(startZ);
			}
			float heroDistance = GetHeroDistance();
			float speedMax = Mathf.Clamp(heroDistance + 4f, 4f, 18f);
			DoFace(spriteFacesRight: false, playNewAnimation: true, details.turnFlyAnim, pauseBetweenTurns: true, 0.5f);
			DoChase(HeroController.instance.transform, 2f, speedMax, 40f, targetRadius, 0.9f, offset);
			DoBuzz(0.75f, 1f, 18f, 80f, 110f, new Vector2(50f, 50f));
			if (heroDistance * 1.15f > 10f)
			{
				awayTimer += Time.fixedDeltaTime;
				if (awayTimer >= 4f)
				{
					state = State.Tele;
				}
			}
			else
			{
				awayTimer = 0f;
			}
			break;
		}
		case State.Tele:
			if (LastFrameState != State.Tele)
			{
				if ((bool)audioSource)
				{
					audioSource.Stop();
				}
				if ((bool)animator)
				{
					animator.Play(details.teleStartAnim);
				}
				if ((bool)enemyRange)
				{
					enemyRange.gameObject.SetActive(value: false);
				}
				if ((bool)groundRange)
				{
					groundRange.gameObject.SetActive(value: false);
				}
				if ((bool)terrainCollider)
				{
					terrainCollider.enabled = false;
				}
			}
			DoChase(HeroController.instance.transform, 2f, 25f, 150f, 0f, 0f, new Vector2(0f, -0.5f));
			if (GetHeroDistance() < 1f)
			{
				state = State.None;
				StartCoroutine(TeleEnd());
			}
			break;
		case State.Attack:
			if (LastFrameState != State.Attack)
			{
				if ((bool)audioSource)
				{
					audioSource.Stop();
					if ((bool)attackChargeClip)
					{
						audioSource.PlayOneShot(attackChargeClip);
					}
				}
				if ((bool)animator)
				{
					animator.Play(details.attackAnim);
				}
				if ((bool)enemyRange)
				{
					enemyRange.gameObject.SetActive(value: false);
				}
				if ((bool)damageEnemies)
				{
					damageEnemies.damageDealt = details.damage;
				}
				attackFinishTime = Time.time + 2f;
			}
			if (Time.time > attackFinishTime || target == null)
			{
				target = null;
				state = State.Follow;
			}
			else
			{
				DoFace(spriteFacesRight: false, playNewAnimation: true, details.turnAttackAnim, pauseBetweenTurns: true, 0.1f);
				DoChaseSimple(target.transform, 25f, 100f, 0f, 0f);
			}
			break;
		case State.BenchRestStart:
			if (LastFrameState != State.BenchRestStart)
			{
				body.velocity = Vector2.zero;
				if ((bool)animator)
				{
					animator.Play(details.flyAnim);
				}
				benchRestWaitTime = Time.time + UnityEngine.Random.Range(2f, 5f);
			}
			if (Time.time < benchRestWaitTime)
			{
				DoBuzz(0.75f, 1f, 2f, 30f, 50f, new Vector2(1f, 1f));
				DoFace(spriteFacesRight: false, playNewAnimation: true, details.turnFlyAnim, pauseBetweenTurns: true, 0.5f);
			}
			else
			{
				state = State.BenchRestLower;
			}
			break;
		case State.BenchRestLower:
		{
			if (LastFrameState != State.BenchRestLower)
			{
				if ((bool)animator)
				{
					animator.Play(details.flyAnim);
				}
				base.transform.SetPositionZ(sleepZ);
				body.isKinematic = false;
			}
			body.AddForce(new Vector2(0f, -5f));
			Vector2 velocity = body.velocity;
			velocity.x *= 0.85f;
			body.velocity = velocity;
			DoFace(spriteFacesRight: false, playNewAnimation: true, details.turnFlyAnim, pauseBetweenTurns: true, 0.5f);
			if (!IsGrounded)
			{
				break;
			}
			state = State.BenchResting;
			if (details.dung)
			{
				dungSleepPlopSound.SpawnAndPlayOneShot(audioSourcePrefab, base.transform.position);
			}
			if ((bool)audioSource)
			{
				audioSource.Stop();
			}
			body.isKinematic = true;
			body.velocity = Vector2.zero;
			if ((bool)animator)
			{
				animator.Play(details.restStartAnim);
			}
			if ((bool)details.groundPoint)
			{
				RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, Vector2.down, 2f, 256);
				if (raycastHit2D.collider != null)
				{
					Vector2 point = raycastHit2D.point;
					Vector2 vector = (Vector2)details.groundPoint.position - point;
					vector.x = 0f;
					base.transform.position -= (Vector3)vector;
				}
			}
			break;
		}
		}
		LastFrameState = CurrentState;
		CurrentState = state;
	}

	private IEnumerator Spawn()
	{
		yield return null;
		if (dreamSpawn)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(1.5f, 2f));
		}
		if ((bool)audioSource)
		{
			audioSource.Play();
		}
		if ((bool)meshRenderer && !dreamSpawn)
		{
			meshRenderer.enabled = true;
		}
		if (!quickSpawn)
		{
			float finishDelay = 0f;
			if ((bool)animator)
			{
				tk2dSpriteAnimationClip clipByName = animator.GetClipByName(details.hatchAnim);
				if (clipByName != null)
				{
					animator.Play(clipByName);
					finishDelay = clipByName.Duration;
				}
			}
			if ((bool)body)
			{
				float num = 45f;
				float num2 = UnityEngine.Random.Range(40f, 140f);
				Vector2 velocity = default(Vector2);
				velocity.x = num * Mathf.Cos(num2 * ((float)Math.PI / 180f));
				velocity.y = num * Mathf.Sin(num2 * ((float)Math.PI / 180f));
				body.velocity = velocity;
			}
			if (!dreamSpawn)
			{
				GlobalPrefabDefaults.Instance.SpawnBlood(HeroController.instance.transform.position + new Vector3(0.2f, -0.7f, 0f), 2, 4, 7f, 15f, 20f, 160f, details.spatterColor);
				details.birthSound.SpawnAndPlayOneShot(audioSourcePrefab, transform.position);
			}
			for (float elapsed = 0f; elapsed < finishDelay; elapsed += Time.fixedDeltaTime)
			{
				if ((bool)body)
				{
					body.velocity *= 0.8f;
				}
				yield return new WaitForFixedUpdate();
			}
			if (dreamSpawn)
			{
				meshRenderer.enabled = true;
				yield return StartCoroutine(animator.PlayAnimWait("Dreamgate In"));
				if ((bool)dungPt && details.dung)
				{
					dungPt.Play();
				}
			}
		}
		if (GameManager.instance.playerData.GetBool("atBench"))
		{
			CurrentState = State.BenchRestStart;
		}
		else
		{
			CurrentState = State.Follow;
		}
	}

	private float GetHeroDistance()
	{
		return Vector2.Distance(base.transform.position, HeroController.instance.transform.position);
	}

	private IEnumerator TeleEnd()
	{
		if ((bool)groundRange)
		{
			groundRange.gameObject.SetActive(value: true);
		}
		if ((bool)terrainCollider)
		{
			terrainCollider.enabled = true;
		}
		if ((bool)audioSource)
		{
			audioSource.Play();
		}
		if ((bool)animator)
		{
			tk2dSpriteAnimationClip clip = animator.GetClipByName(details.teleEndAnim);
			animator.Play(clip);
			for (float elapsed = 0f; elapsed < clip.Duration; elapsed += Time.fixedDeltaTime)
			{
				if ((bool)body)
				{
					body.velocity *= 0.7f;
				}
				yield return new WaitForFixedUpdate();
			}
		}
		CurrentState = State.Follow;
	}

	public void FsmHitLanded()
	{
		CurrentState = State.None;
		if ((bool)damageEnemies)
		{
			damageEnemies.damageDealt = 0;
		}
		StartCoroutine(Explode());
	}

	private IEnumerator Explode()
	{
		yield return new WaitForFixedUpdate();
		if ((bool)col)
		{
			col.enabled = false;
		}
		explodeSound.SpawnAndPlayOneShot(audioSourcePrefab, transform.position);
		body.velocity = Vector2.zero;
		if ((bool)spriteFlash)
		{
			spriteFlash.CancelFlash();
		}
		float seconds = 2f;
		if (details.dung)
		{
			if ((bool)dungPt)
			{
				dungPt.Stop();
			}
			dungExplodeSound.SpawnAndPlayOneShot(audioSourcePrefab, transform.position);
			if ((bool)dungExplosionPrefab)
			{
				dungExplosionPrefab.Spawn(transform.position);
			}
			if ((bool)meshRenderer)
			{
				meshRenderer.enabled = false;
			}
			GlobalPrefabDefaults.Instance.SpawnBlood(HeroController.instance.transform.position, 8, 8, 10f, 20f, 0f, 360f, details.spatterColor);
		}
		else if ((bool)animator)
		{
			tk2dSpriteAnimationClip clipByName = animator.GetClipByName("Burst");
			if (clipByName != null)
			{
				animator.Play(clipByName);
				seconds = clipByName.Duration;
			}
		}
		yield return new WaitForSeconds(seconds);
		gameObject.Recycle();
	}

	public void FsmCharmsEnd()
	{
		CurrentState = State.None;
		StartCoroutine(CharmsEnd());
	}

	private IEnumerator CharmsEnd()
	{
		float finishDelay = 0f;
		if ((bool)animator)
		{
			tk2dSpriteAnimationClip clipByName = animator.GetClipByName("Tele Start");
			if (clipByName != null)
			{
				animator.Play(clipByName);
				finishDelay = clipByName.Duration;
			}
		}
		for (float elapsed = 0f; elapsed < finishDelay; elapsed += Time.fixedDeltaTime)
		{
			if ((bool)body)
			{
				body.velocity *= 0.8f;
			}
			yield return new WaitForFixedUpdate();
		}
		meshRenderer.enabled = false;
		if (details.dung && (bool)dungPt)
		{
			dungPt.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
			while (dungPt.IsAlive(withChildren: true))
			{
				yield return null;
			}
		}
		gameObject.Recycle();
	}

	public void FsmHazardReload()
	{
		base.gameObject.Recycle();
	}

	public void FsmBenchRestStart()
	{
		CurrentState = State.BenchRestStart;
	}

	public void FsmBenchRestEnd()
	{
		if (CurrentState == State.BenchResting)
		{
			StartCoroutine(WakeUp());
		}
		else
		{
			CurrentState = State.Follow;
		}
	}

	private IEnumerator WakeUp()
	{
		float seconds = 0f;
		if ((bool)animator)
		{
			tk2dSpriteAnimationClip clipByName = animator.GetClipByName(details.restEndAnim);
			if (clipByName != null)
			{
				animator.Play(clipByName);
				seconds = clipByName.Duration;
			}
		}
		yield return new WaitForSeconds(seconds);
		CurrentState = State.Follow;
	}

	public void FsmQuickSpawn()
	{
		quickSpawn = true;
	}

	public void FsmDreamGateOut()
	{
		CurrentState = State.None;
		StartCoroutine(DreamGateOut());
	}

	private IEnumerator DreamGateOut()
	{
		float seconds = 0f;
		if ((bool)animator)
		{
			tk2dSpriteAnimationClip clipByName = animator.GetClipByName("Dreamgate Out");
			if (clipByName != null)
			{
				animator.Play(clipByName);
				seconds = clipByName.Duration;
			}
		}
		yield return new WaitForSeconds(seconds);
		meshRenderer.enabled = false;
		if (details.dung && (bool)dungPt)
		{
			dungPt.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
		}
	}

	private void DoFace(bool spriteFacesRight, bool playNewAnimation, string newAnimationClip, bool pauseBetweenTurns, float pauseTime)
	{
		if (body == null)
		{
			return;
		}
		Vector2 velocity = body.velocity;
		Vector3 localScale = base.transform.localScale;
		float x = velocity.x;
		if (CurrentState != LastFrameState)
		{
			xScale = base.transform.localScale.x;
			pauseTimer = 0f;
		}
		if (xScale < 0f)
		{
			xScale *= -1f;
		}
		if (pauseTimer <= 0f || !pauseBetweenTurns)
		{
			if (x > 0f)
			{
				if (spriteFacesRight)
				{
					if (localScale.x != xScale)
					{
						pauseTimer = pauseTime;
						localScale.x = xScale;
						if (playNewAnimation)
						{
							animator.Play(newAnimationClip);
							animator.PlayFromFrame(0);
						}
					}
				}
				else if (localScale.x != 0f - xScale)
				{
					pauseTimer = pauseTime;
					localScale.x = 0f - xScale;
					if (playNewAnimation)
					{
						animator.Play(newAnimationClip);
						animator.PlayFromFrame(0);
					}
				}
			}
			else if (x <= 0f)
			{
				if (spriteFacesRight)
				{
					if (localScale.x != 0f - xScale)
					{
						pauseTimer = pauseTime;
						localScale.x = 0f - xScale;
						if (playNewAnimation)
						{
							animator.Play(newAnimationClip);
							animator.PlayFromFrame(0);
						}
					}
				}
				else if (localScale.x != xScale)
				{
					pauseTimer = pauseTime;
					localScale.x = xScale;
					if (playNewAnimation)
					{
						animator.Play(newAnimationClip);
						animator.PlayFromFrame(0);
					}
				}
			}
		}
		else
		{
			pauseTimer -= Time.deltaTime;
		}
		base.transform.localScale = new Vector3(localScale.x, base.transform.localScale.y, base.transform.localScale.z);
	}

	private void DoChase(Transform target, float distance, float speedMax, float accelerationForce, float targetRadius, float deceleration, Vector2 offset)
	{
		if (body == null)
		{
			return;
		}
		float num = Mathf.Sqrt(Mathf.Pow(base.transform.position.x - (target.position.x + offset.x), 2f) + Mathf.Pow(base.transform.position.y - (target.position.y + offset.y), 2f));
		Vector2 velocity = body.velocity;
		if (!(num > distance - targetRadius) || !(num < distance + targetRadius))
		{
			Vector2 vector = new Vector2(target.position.x + offset.x - base.transform.position.x, target.position.y + offset.y - base.transform.position.y);
			vector = Vector2.ClampMagnitude(vector, 1f);
			vector = new Vector2(vector.x * accelerationForce, vector.y * accelerationForce);
			if (num < distance)
			{
				vector = new Vector2(0f - vector.x, 0f - vector.y);
			}
			body.AddForce(vector);
			velocity = Vector2.ClampMagnitude(velocity, speedMax);
			body.velocity = velocity;
			return;
		}
		velocity = body.velocity;
		if (velocity.x < 0f)
		{
			velocity.x *= deceleration;
			if (velocity.x > 0f)
			{
				velocity.x = 0f;
			}
		}
		else if (velocity.x > 0f)
		{
			velocity.x *= deceleration;
			if (velocity.x < 0f)
			{
				velocity.x = 0f;
			}
		}
		if (velocity.y < 0f)
		{
			velocity.y *= deceleration;
			if (velocity.y > 0f)
			{
				velocity.y = 0f;
			}
		}
		else if (velocity.y > 0f)
		{
			velocity.y *= deceleration;
			if (velocity.y < 0f)
			{
				velocity.y = 0f;
			}
		}
		body.velocity = velocity;
	}

	private void DoBuzz(float waitMin, float waitMax, float speedMax, float accelerationMin, float accelerationMax, Vector2 roamingRange)
	{
		if (body == null)
		{
			return;
		}
		float num = 1.125f;
		if (CurrentState != LastFrameState)
		{
			startX = base.transform.position.x;
			startY = base.transform.position.y;
		}
		Vector2 velocity = body.velocity;
		if (base.transform.position.y < startY - roamingRange.y)
		{
			if (velocity.y < 0f)
			{
				accelY = accelerationMax;
				accelY /= 2000f;
				velocity.y /= num;
				waitTime = UnityEngine.Random.Range(waitMin, waitMax);
			}
		}
		else if (base.transform.position.y > startY + roamingRange.y && velocity.y > 0f)
		{
			accelY = 0f - accelerationMax;
			accelY /= 2000f;
			velocity.y /= num;
			waitTime = UnityEngine.Random.Range(waitMin, waitMax);
		}
		if (base.transform.position.x < startX - roamingRange.x)
		{
			if (velocity.x < 0f)
			{
				accelX = accelerationMax;
				accelX /= 2000f;
				velocity.x /= num;
				waitTime = UnityEngine.Random.Range(waitMin, waitMax);
			}
		}
		else if (base.transform.position.x > startX + roamingRange.x && velocity.x > 0f)
		{
			accelX = 0f - accelerationMax;
			accelX /= 2000f;
			velocity.x /= num;
			waitTime = UnityEngine.Random.Range(waitMin, waitMax);
		}
		if (waitTime <= Mathf.Epsilon)
		{
			if (base.transform.position.y < startY - roamingRange.y)
			{
				accelY = UnityEngine.Random.Range(accelerationMin, accelerationMax);
			}
			else if (base.transform.position.y > startY + roamingRange.y)
			{
				accelY = UnityEngine.Random.Range(0f - accelerationMax, accelerationMin);
			}
			else
			{
				accelY = UnityEngine.Random.Range(0f - accelerationMax, accelerationMax);
			}
			if (base.transform.position.x < startX - roamingRange.x)
			{
				accelX = UnityEngine.Random.Range(accelerationMin, accelerationMax);
			}
			else if (base.transform.position.x > startX + roamingRange.x)
			{
				accelX = UnityEngine.Random.Range(0f - accelerationMax, accelerationMin);
			}
			else
			{
				accelX = UnityEngine.Random.Range(0f - accelerationMax, accelerationMax);
			}
			accelY /= 2000f;
			accelX /= 2000f;
			waitTime = UnityEngine.Random.Range(waitMin, waitMax);
		}
		if (waitTime > Mathf.Epsilon)
		{
			waitTime -= Time.deltaTime;
		}
		velocity.x += accelX;
		velocity.y += accelY;
		if (velocity.x > speedMax)
		{
			velocity.x = speedMax;
		}
		if (velocity.x < 0f - speedMax)
		{
			velocity.x = 0f - speedMax;
		}
		if (velocity.y > speedMax)
		{
			velocity.y = speedMax;
		}
		if (velocity.y < 0f - speedMax)
		{
			velocity.y = 0f - speedMax;
		}
		body.velocity = velocity;
	}

	private void DoChaseSimple(Transform target, float speedMax, float accelerationForce, float offsetX, float offsetY)
	{
		if (!(body == null))
		{
			Vector2 vector = new Vector2(target.position.x + offsetX - base.transform.position.x, target.position.y + offsetY - base.transform.position.y);
			vector = Vector2.ClampMagnitude(vector, 1f);
			vector = new Vector2(vector.x * accelerationForce, vector.y * accelerationForce);
			body.AddForce(vector);
			Vector2 velocity = body.velocity;
			velocity = Vector2.ClampMagnitude(velocity, speedMax);
			body.velocity = velocity;
		}
	}
}
