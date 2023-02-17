using System.Collections;
using UnityEngine;

public class Corpse : MonoBehaviour
{
	private enum States
	{
		NotStarted,
		InAir,
		DeathAnimation,
		Complete,
		PendingLandEffects
	}

	protected MeshRenderer meshRenderer;

	protected tk2dSprite sprite;

	protected tk2dSpriteAnimator spriteAnimator;

	protected SpriteFlash spriteFlash;

	protected Rigidbody2D body;

	protected Collider2D bodyCollider;

	[SerializeField]
	protected ParticleSystem corpseFlame;

	[SerializeField]
	protected ParticleSystem corpseSteam;

	[SerializeField]
	protected GameObject landEffects;

	[SerializeField]
	protected AudioSource audioPlayerPrefab;

	[SerializeField]
	protected GameObject deathWaveInfectedPrefab;

	[SerializeField]
	protected GameObject spatterOrangePrefab;

	[SerializeField]
	protected RandomAudioClipTable splatAudioClipTable;

	[SerializeField]
	private int smashBounces;

	[SerializeField]
	private bool breaker;

	[SerializeField]
	private bool bigBreaker;

	[SerializeField]
	private bool chunker;

	[SerializeField]
	private bool deathStun;

	[SerializeField]
	private bool fungusExplode;

	[SerializeField]
	private bool goopExplode;

	[SerializeField]
	private bool hatcher;

	[SerializeField]
	private bool instantChunker;

	[SerializeField]
	private bool massless;

	[SerializeField]
	private bool resetRotation;

	[SerializeField]
	private AudioEvent startAudio;

	[SerializeField]
	private bool spineBurst;

	[SerializeField]
	private bool zomHive;

	private bool noSteam;

	protected bool spellBurn;

	protected bool hitAcid;

	private States state;

	private bool bouncedThisFrame;

	private int bounceCount;

	private float landEffectsDelayRemaining;

	private void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		sprite = GetComponent<tk2dSprite>();
		spriteAnimator = GetComponent<tk2dSpriteAnimator>();
		spriteFlash = GetComponent<SpriteFlash>();
		body = GetComponent<Rigidbody2D>();
		bodyCollider = GetComponent<Collider2D>();
	}

	public void Setup(bool noSteam, bool spellBurn)
	{
		this.noSteam = noSteam;
		this.spellBurn = spellBurn;
	}

	protected virtual void Start()
	{
		startAudio.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
		if (resetRotation)
		{
			base.transform.SetRotation2D(0f);
		}
		if (noSteam && corpseSteam != null)
		{
			corpseSteam.gameObject.SetActive(value: false);
		}
		if (spellBurn)
		{
			if (sprite != null)
			{
				sprite.color = new Color(10f / 51f, 10f / 51f, 10f / 51f, 1f);
			}
			if (corpseFlame != null)
			{
				corpseFlame.Play();
			}
		}
		if (massless)
		{
			state = States.DeathAnimation;
		}
		else
		{
			state = States.InAir;
			if (spriteAnimator != null)
			{
				tk2dSpriteAnimationClip clipByName = spriteAnimator.GetClipByName("Death Air");
				if (clipByName != null)
				{
					spriteAnimator.Play(clipByName);
				}
			}
		}
		if (instantChunker && !breaker)
		{
			Land();
		}
		if (GameManager.instance.GetCurrentMapZone() == "COLOSSEUM")
		{
			StartCoroutine(DropThroughFloor());
		}
		StartCoroutine(DisableFlame());
	}

	protected void Update()
	{
		if (state == States.DeathAnimation)
		{
			if (spriteAnimator == null || !spriteAnimator.Playing)
			{
				Complete(detachChildren: true, destroyMe: true);
			}
		}
		else if (state == States.InAir)
		{
			bouncedThisFrame = false;
			if (base.transform.position.y < -10f)
			{
				Complete(detachChildren: true, destroyMe: true);
			}
		}
		else if (state == States.PendingLandEffects)
		{
			landEffectsDelayRemaining -= Time.deltaTime;
			if (landEffectsDelayRemaining <= 0f)
			{
				Complete(detachChildren: false, destroyMe: false);
			}
		}
	}

	private void Complete(bool detachChildren, bool destroyMe)
	{
		state = States.Complete;
		base.enabled = false;
		if (corpseSteam != null)
		{
			corpseSteam.Stop();
		}
		if (corpseFlame != null)
		{
			corpseFlame.Stop();
		}
		if (detachChildren)
		{
			base.transform.DetachChildren();
		}
		if (destroyMe)
		{
			Object.Destroy(base.gameObject);
		}
	}

	protected void OnCollisionEnter2D(Collision2D collision)
	{
		OnCollision(collision);
	}

	protected void OnCollisionStay2D(Collision2D collision)
	{
		OnCollision(collision);
	}

	private void OnCollision(Collision2D collision)
	{
		if (state == States.InAir && new Sweep(bodyCollider, 3, 3).Check(base.transform.position, 0.08f, 256, out var _))
		{
			Land();
		}
	}

	private void Land()
	{
		if (breaker)
		{
			if (!bouncedThisFrame)
			{
				bounceCount++;
				bouncedThisFrame = true;
				if (bounceCount >= smashBounces)
				{
					Smash();
				}
			}
			return;
		}
		if (spriteAnimator != null && !hitAcid)
		{
			tk2dSpriteAnimationClip clipByName = spriteAnimator.GetClipByName("Death Land");
			if (clipByName != null)
			{
				spriteAnimator.Play(clipByName);
			}
		}
		landEffectsDelayRemaining = 1f;
		if (landEffects != null)
		{
			landEffects.SetActive(value: true);
		}
		state = States.PendingLandEffects;
		if (!hitAcid)
		{
			LandEffects();
		}
	}

	protected virtual void LandEffects()
	{
	}

	protected virtual void Smash()
	{
		if (!hitAcid)
		{
			GlobalPrefabDefaults.Instance.SpawnBlood(base.transform.position, 6, 8, 10f, 20f, 75f, 105f);
		}
		splatAudioClipTable.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
		if (corpseFlame != null)
		{
			corpseFlame.Stop();
		}
		if (corpseSteam != null)
		{
			corpseSteam.Stop();
		}
		if (spriteAnimator != null)
		{
			spriteAnimator.Play("Death Land");
		}
		body.velocity = Vector2.zero;
		state = States.DeathAnimation;
		if (bigBreaker)
		{
			if (!hitAcid)
			{
				GlobalPrefabDefaults.Instance.SpawnBlood(base.transform.position, 30, 30, 20f, 30f, 80f, 100f);
			}
			GameCameras instance = GameCameras.instance;
			if ((bool)instance)
			{
				instance.cameraShakeFSM.SendEvent("EnemyKillShake");
			}
		}
	}

	public void Acid()
	{
		hitAcid = true;
		if ((bool)corpseFlame)
		{
			corpseFlame.Stop();
		}
		if ((bool)corpseSteam)
		{
			corpseSteam.Stop();
		}
		Land();
	}

	private IEnumerator DropThroughFloor()
	{
		yield return new WaitForSeconds(Random.Range(3f, 6f));
		Collider2D[] componentsInChildren = GetComponentsInChildren<Collider2D>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
		if ((bool)body)
		{
			body.isKinematic = false;
		}
		yield return new WaitForSeconds(1f);
		gameObject.SetActive(value: false);
	}

	private IEnumerator DisableFlame()
	{
		yield return new WaitForSeconds(5f);
		if ((bool)corpseFlame)
		{
			corpseFlame.Stop();
		}
	}
}
