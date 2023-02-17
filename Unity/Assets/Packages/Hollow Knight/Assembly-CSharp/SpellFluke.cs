using System;
using System.Collections;
using UnityEngine;

public class SpellFluke : MonoBehaviour
{
	public string airAnim = "Air";

	public string flopAnim = "Flop";

	public TriggerEnterEvent damager;

	public GameObject splatEffect;

	public AudioSource audioPlayerPrefab;

	public AudioEventRandom splatSounds;

	private float lifeEndTime;

	private int damage;

	private bool hasBounced;

	private bool hasBursted;

	private tk2dSpriteAnimator animator;

	private MeshRenderer meshRenderer;

	private Rigidbody2D body;

	private SpriteFlash spriteFlash;

	private ObjectBounce objectBounce;

	private void Awake()
	{
		animator = GetComponent<tk2dSpriteAnimator>();
		meshRenderer = GetComponent<MeshRenderer>();
		body = GetComponent<Rigidbody2D>();
		spriteFlash = GetComponent<SpriteFlash>();
		objectBounce = GetComponent<ObjectBounce>();
	}

	private void Start()
	{
		damager.OnTriggerEntered += delegate(Collider2D collider, GameObject sender)
		{
			DoDamage(collider.gameObject, 2);
		};
		if (!objectBounce)
		{
			return;
		}
		objectBounce.OnBounce += delegate
		{
			if (!hasBursted)
			{
				hasBounced = true;
				if ((bool)body)
				{
					Vector2 velocity = body.velocity;
					velocity.x = UnityEngine.Random.Range(-5f, 5f);
					velocity.y = Mathf.Clamp(velocity.y, UnityEngine.Random.Range(7.3f, 15f), UnityEngine.Random.Range(20f, 25f));
					body.velocity = velocity;
				}
				if ((bool)animator)
				{
					animator.Play(flopAnim);
				}
				base.transform.SetRotationZ(0f);
			}
		};
	}

	private void DoDamage(GameObject obj, int upwardRecursionAmount, bool burst = true)
	{
		HealthManager component = obj.GetComponent<HealthManager>();
		if ((bool)component)
		{
			if (component.IsInvincible && obj.tag != "Spell Vulnerable")
			{
				return;
			}
			if (!component.isDead)
			{
				component.hp -= damage;
				if (component.hp <= 0)
				{
					component.Die(0f, AttackTypes.Generic, ignoreEvasion: false);
				}
			}
		}
		SpriteFlash component2 = obj.GetComponent<SpriteFlash>();
		if ((bool)component2)
		{
			component2.FlashShadowRecharge();
		}
		FSMUtility.SendEventToGameObject(obj.gameObject, "TOOK DAMAGE");
		upwardRecursionAmount--;
		if (upwardRecursionAmount > 0 && (bool)obj.transform.parent)
		{
			DoDamage(obj.transform.parent.gameObject, upwardRecursionAmount, burst: false);
		}
		if (burst)
		{
			Burst();
		}
	}

	private void OnEnable()
	{
		if ((bool)animator)
		{
			animator.Play(airAnim);
		}
		lifeEndTime = Time.time + UnityEngine.Random.Range(2f, 3f);
		if ((bool)meshRenderer)
		{
			meshRenderer.enabled = true;
		}
		if ((bool)body)
		{
			body.isKinematic = false;
		}
		if (GameManager.instance.playerData.GetBool("equippedCharm_19"))
		{
			float num = UnityEngine.Random.Range(0.9f, 1.2f);
			base.transform.localScale = new Vector3(num, num, 0f);
			damage = 5;
		}
		else
		{
			float num2 = UnityEngine.Random.Range(0.7f, 0.9f);
			base.transform.localScale = new Vector3(num2, num2, 0f);
			damage = 4;
		}
		if ((bool)spriteFlash)
		{
			spriteFlash.flashArmoured();
		}
		hasBounced = false;
		hasBursted = false;
	}

	private void Update()
	{
		if (!hasBursted)
		{
			if (!hasBounced)
			{
				Vector2 velocity = body.velocity;
				float z = Mathf.Atan2(velocity.y, velocity.x) * (180f / (float)Math.PI);
				base.transform.localEulerAngles = new Vector3(0f, 0f, z);
			}
			if (Time.time >= lifeEndTime)
			{
				Burst();
			}
		}
	}

	private void Burst()
	{
		if (!hasBursted)
		{
			StartCoroutine(BurstSequence());
		}
		hasBursted = true;
	}

	private IEnumerator BurstSequence()
	{
		if ((bool)meshRenderer)
		{
			meshRenderer.enabled = false;
		}
		if ((bool)body)
		{
			body.velocity = Vector2.zero;
			body.angularVelocity = 0f;
			body.isKinematic = true;
		}
		if ((bool)splatEffect)
		{
			splatEffect.SetActive(value: true);
		}
		splatSounds.SpawnAndPlayOneShot(audioPlayerPrefab, transform.position);
		yield return new WaitForSeconds(1f);
		gameObject.Recycle();
	}
}
