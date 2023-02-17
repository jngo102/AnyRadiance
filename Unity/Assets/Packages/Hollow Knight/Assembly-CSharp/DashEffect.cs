using UnityEngine;

public class DashEffect : MonoBehaviour
{
	public GameObject heroDashPuff;

	public GameObject dashDust;

	public GameObject dashBone;

	public GameObject dashGrass;

	public GameObject waterCut;

	public tk2dSpriteAnimator heroDashPuff_anim;

	public AudioClip splashClip;

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
		if (instance != null && instance.isHeroInPosition)
		{
			switch (pd.GetInt("environmentType"))
			{
			case 1:
				PlayDashPuff();
				PlayGrass();
				break;
			case 2:
				PlayDashPuff();
				PlayBone();
				break;
			case 3:
				PlaySpaEffects();
				break;
			default:
				PlayDashPuff();
				PlayDust();
				break;
			case 6:
				break;
			}
		}
	}

	private void PlayDashPuff()
	{
		heroDashPuff.SetActive(value: true);
		heroDashPuff_anim.PlayFromFrame(0);
	}

	private void PlayDust()
	{
		dashDust.SetActive(value: true);
	}

	private void PlayGrass()
	{
		dashGrass.SetActive(value: true);
	}

	private void PlayBone()
	{
		dashBone.SetActive(value: true);
	}

	private void PlaySpaEffects()
	{
		waterCut.SetActive(value: true);
		audioSource.PlayOneShot(splashClip);
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
