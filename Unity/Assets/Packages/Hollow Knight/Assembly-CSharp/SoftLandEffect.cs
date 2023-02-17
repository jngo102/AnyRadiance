using UnityEngine;

public class SoftLandEffect : MonoBehaviour
{
	public GameObject dustEffects;

	public GameObject grassEffects;

	public GameObject boneEffects;

	public GameObject splash;

	public AudioClip softLandClip;

	public AudioClip wetLandClip;

	private PlayerData pd;

	private GameObject heroObject;

	private AudioSource audioSource;

	private Rigidbody2D heroRigidBody;

	private tk2dSpriteAnimator jumpPuffAnimator;

	private float recycleTimer;

	private void OnEnable()
	{
		if (pd == null)
		{
			pd = GameManager.instance.playerData;
		}
		if (audioSource == null)
		{
			audioSource = base.gameObject.GetComponent<AudioSource>();
		}
		foreach (Transform item in base.transform)
		{
			item.gameObject.SetActive(value: false);
		}
		recycleTimer = 1f;
		HeroController instance = HeroController.instance;
		if (!(instance != null) || !instance.isHeroInPosition)
		{
			return;
		}
		switch (pd.GetInt("environmentType"))
		{
		case 1:
			grassEffects.SetActive(value: true);
			audioSource.PlayOneShot(softLandClip);
			break;
		case 2:
			boneEffects.SetActive(value: true);
			audioSource.PlayOneShot(softLandClip);
			break;
		case 3:
			audioSource.PlayOneShot(wetLandClip);
			break;
		case 6:
			audioSource.PlayOneShot(wetLandClip);
			splash.SetActive(value: true);
			if (Random.Range(1, 100) > 50)
			{
				splash.transform.localScale = new Vector3(0f - splash.transform.localScale.x, splash.transform.localScale.y, splash.transform.localScale.z);
			}
			break;
		default:
			dustEffects.SetActive(value: true);
			audioSource.PlayOneShot(softLandClip);
			break;
		case 5:
			break;
		}
	}

	private void Update()
	{
		if (recycleTimer <= 0f)
		{
			base.gameObject.Recycle();
		}
		else
		{
			recycleTimer -= Time.deltaTime;
		}
	}
}
