using System;
using System.Collections;
using UnityEngine;

public class VinePlatform : MonoBehaviour
{
	public GameObject platformSprite;

	public GameObject activatedSprite;

	public Collider2D collider;

	[Space]
	public AudioClip playerLandSound;

	public ParticleSystem playerLandParticles;

	public AnimationCurve playerLandAnimCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));

	public float playerLandAnimLength = 0.5f;

	[HideInInspector]
	public Coroutine landRoutine;

	[HideInInspector]
	public bool respondOnLand = true;

	private Action landReturnAction;

	[Space]
	public TriggerEnterEvent landingDetector;

	public AudioClip landSound;

	public ParticleSystem[] landParticles;

	public GameObject slamEffect;

	[Space]
	public TriggerEnterEvent enemyDetector;

	[Space]
	public bool acidLander;

	public float acidTargetY;

	public AudioClip acidSplashSound;

	public GameObject acidSplashPrefab;

	private AudioSource audioSource;

	private Rigidbody2D body;

	private bool activated;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		body = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		PersistentBoolItem component = GetComponent<PersistentBoolItem>();
		if ((bool)component)
		{
			component.OnGetSaveState += delegate(ref bool value)
			{
				value = activated;
			};
			component.OnSetSaveState += delegate(bool value)
			{
				activated = value;
				if (activated)
				{
					platformSprite.SetActive(value: false);
					activatedSprite.SetActive(value: true);
					if ((bool)landingDetector)
					{
						landingDetector.gameObject.SetActive(value: false);
					}
					if ((bool)collider)
					{
						collider.enabled = false;
					}
				}
			};
		}
		if ((bool)landingDetector && !acidLander)
		{
			landingDetector.OnTriggerEntered += delegate
			{
				Land();
			};
		}
		if (!enemyDetector)
		{
			return;
		}
		enemyDetector.OnTriggerEntered += delegate(Collider2D collider, GameObject sender)
		{
			HealthManager component2 = collider.GetComponent<HealthManager>();
			if ((bool)component2)
			{
				component2.Die(0f, AttackTypes.Splatter, ignoreEvasion: false);
			}
		};
		enemyDetector.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		if (acidLander && !activated && collider.bounds.min.y <= acidTargetY)
		{
			Land();
		}
	}

	private void Land()
	{
		PlaySound(landSound);
		if (!acidLander)
		{
			GameCameras gameCameras = UnityEngine.Object.FindObjectOfType<GameCameras>();
			if ((bool)gameCameras)
			{
				gameCameras.cameraShakeFSM.SendEvent("AverageShake");
			}
			ParticleSystem[] array = landParticles;
			foreach (ParticleSystem particleSystem in array)
			{
				if (particleSystem.gameObject.activeInHierarchy)
				{
					particleSystem.Play();
				}
			}
			if ((bool)slamEffect)
			{
				slamEffect.SetActive(value: true);
			}
		}
		else
		{
			PlaySound(acidSplashSound);
			if ((bool)acidSplashPrefab)
			{
				UnityEngine.Object.Instantiate(acidSplashPrefab, new Vector3(base.transform.position.x, collider.bounds.min.y, base.transform.position.z), Quaternion.identity);
			}
			float num = base.transform.position.y - collider.bounds.min.y;
			base.transform.SetPositionY(acidTargetY + num);
		}
		if ((bool)body)
		{
			body.isKinematic = true;
			body.velocity = Vector2.zero;
		}
		if ((bool)enemyDetector)
		{
			enemyDetector.gameObject.SetActive(value: false);
		}
		activated = true;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (respondOnLand && collision.gameObject.layer == 9 && collision.collider.bounds.min.y >= collider.bounds.max.y)
		{
			if (landRoutine != null)
			{
				StopCoroutine(landRoutine);
			}
			if (landReturnAction != null)
			{
				landReturnAction();
			}
			landRoutine = StartCoroutine(PlayerLand());
		}
		else if (!body.isKinematic && collision.gameObject.layer != 8 && collision.gameObject.layer != 9)
		{
			Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
		}
	}

	private void PlaySound(AudioClip clip)
	{
		if ((bool)audioSource && (bool)clip)
		{
			audioSource.PlayOneShot(clip);
		}
	}

	private IEnumerator PlayerLand()
	{
		PlaySound(playerLandSound);
		if ((bool)playerLandParticles)
		{
			playerLandParticles.Play();
		}
		if ((bool)platformSprite)
		{
			Vector3 initialPos = platformSprite.transform.position;
			landReturnAction = delegate
			{
				platformSprite.transform.position = initialPos;
			};
			for (float elapsed = 0f; elapsed < playerLandAnimLength; elapsed += Time.deltaTime)
			{
				Vector3 position = initialPos;
				position.y += playerLandAnimCurve.Evaluate(elapsed / playerLandAnimLength);
				platformSprite.transform.position = position;
				yield return null;
			}
		}
		if (landReturnAction != null)
		{
			landReturnAction();
		}
		landRoutine = null;
	}

	private void OnDrawGizmosSelected()
	{
		if (acidLander)
		{
			Vector3 position = base.transform.position;
			position.y = acidTargetY;
			Gizmos.DrawWireSphere(position, 0.5f);
		}
	}
}
