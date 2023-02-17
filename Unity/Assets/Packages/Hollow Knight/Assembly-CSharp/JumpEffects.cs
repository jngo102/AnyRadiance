using System;
using UnityEngine;

public class JumpEffects : MonoBehaviour
{
	public GameObject dustEffects;

	public GameObject grassEffects;

	public GameObject boneEffects;

	public GameObject splash;

	public GameObject jumpPuff;

	public GameObject dustTrail;

	public GameObject spatterWhitePrefab;

	private PlayerData pd;

	private GameObject heroObject;

	private Rigidbody2D heroRigidBody;

	private tk2dSpriteAnimator jumpPuffAnimator;

	private float recycleTimer;

	private float fallTimer;

	private float dripTimer;

	private float dripEndTimer;

	private bool dripping;

	private bool checkForFall;

	private bool trailAttached;

	private void OnEnable()
	{
		if (pd == null)
		{
			pd = GameManager.instance.playerData;
		}
		foreach (Transform item in base.transform)
		{
			item.gameObject.SetActive(value: false);
		}
		fallTimer = 0.1f;
		recycleTimer = 1f;
		dripTimer = 0f;
		dripEndTimer = 0f;
		dripping = false;
		checkForFall = false;
		trailAttached = false;
		switch (pd.GetInt("environmentType"))
		{
		case 1:
			grassEffects.SetActive(value: true);
			checkForFall = true;
			GetHero();
			PlayJumpPuff();
			PlayTrail();
			break;
		case 2:
			boneEffects.SetActive(value: true);
			checkForFall = true;
			GetHero();
			PlayJumpPuff();
			PlayTrail();
			break;
		case 3:
			GetHero();
			SplashOut();
			break;
		case 6:
			splash.SetActive(value: true);
			if (UnityEngine.Random.Range(1, 100) > 50)
			{
				splash.transform.localScale = new Vector3(0f - splash.transform.localScale.x, splash.transform.localScale.y, splash.transform.localScale.z);
			}
			break;
		default:
			dustEffects.SetActive(value: true);
			checkForFall = true;
			GetHero();
			PlayJumpPuff();
			PlayTrail();
			break;
		}
	}

	private void Update()
	{
		if (checkForFall)
		{
			if (fallTimer >= 0f)
			{
				fallTimer -= Time.deltaTime;
			}
			else
			{
				CheckForFall();
			}
		}
		if (recycleTimer <= 0f)
		{
			base.gameObject.Recycle();
		}
		else
		{
			recycleTimer -= Time.deltaTime;
		}
		if (trailAttached)
		{
			Vector3 position = heroObject.transform.position;
			dustTrail.transform.position = new Vector3(position.x, position.y - 1.5f, position.z + 0.001f);
		}
		if (dripping)
		{
			if (dripTimer <= 0f)
			{
				ObjectPoolExtensions.Spawn(position: new Vector3(heroObject.transform.position.x + UnityEngine.Random.Range(-0.25f, 0.25f), heroObject.transform.position.y + UnityEngine.Random.Range(-0.5f, 0.5f), heroObject.transform.position.z), prefab: spatterWhitePrefab);
				dripTimer += 0.025f;
			}
			else
			{
				dripTimer -= Time.deltaTime;
			}
			if (dripEndTimer <= 0f)
			{
				dripping = false;
			}
			else
			{
				dripEndTimer -= Time.deltaTime;
			}
		}
	}

	private void GetHero()
	{
		heroObject = HeroController.instance.gameObject;
		heroRigidBody = heroObject.GetComponent<Rigidbody2D>();
	}

	private void CheckForFall()
	{
		if (heroRigidBody.velocity.y <= 0f)
		{
			jumpPuff.SetActive(value: false);
			dustTrail.GetComponent<ParticleSystem>().Stop();
			checkForFall = false;
		}
	}

	private void PlayTrail()
	{
		dustTrail.SetActive(value: true);
		trailAttached = true;
	}

	private void PlayJumpPuff()
	{
		float z = heroRigidBody.velocity.x * -3f + 2.6f;
		jumpPuff.transform.localEulerAngles = new Vector3(0f, 0f, z);
		jumpPuff.SetActive(value: true);
		if (jumpPuffAnimator == null)
		{
			jumpPuffAnimator = jumpPuff.GetComponent<tk2dSpriteAnimator>();
		}
		jumpPuffAnimator.PlayFromFrame(0);
	}

	private void SplashOut()
	{
		dripEndTimer = 0.4f;
		dripping = true;
		Vector3 position = heroObject.transform.position;
		Vector2 velocity = default(Vector2);
		for (int i = 1; i <= 11; i++)
		{
			GameObject obj = spatterWhitePrefab.Spawn(position);
			_ = obj.transform.position;
			_ = obj.transform.position;
			_ = obj.transform.position;
			float num = UnityEngine.Random.Range(5f, 12f);
			float num2 = UnityEngine.Random.Range(80f, 110f);
			float x = num * Mathf.Cos(num2 * ((float)Math.PI / 180f));
			float y = num * Mathf.Sin(num2 * ((float)Math.PI / 180f));
			velocity.x = x;
			velocity.y = y;
			obj.GetComponent<Rigidbody2D>().velocity = velocity;
		}
	}
}
