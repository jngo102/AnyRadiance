using UnityEngine;

public class VinePlatformCut : MonoBehaviour
{
	public Rigidbody2D body;

	[Space]
	public GameObject sprites;

	[Space]
	public GameObject cutParticles;

	public GameObject cutPointParticles;

	public GameObject cutEffectPrefab;

	[Space]
	public AudioClip cutSound;

	private bool activated;

	private VinePlatform platform;

	private AudioSource audioSource;

	private Collider2D col;

	private void Awake()
	{
		platform = GetComponentInParent<VinePlatform>();
		audioSource = GetComponentInParent<AudioSource>();
		col = GetComponent<Collider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (activated || !(collision.tag == "Nail Attack") || collision.transform.position.y < col.bounds.min.y || collision.transform.position.y > col.bounds.max.y)
		{
			return;
		}
		float value = PlayMakerFSM.FindFsmOnGameObject(collision.gameObject, "damages_enemy").FsmVariables.FindFsmFloat("direction").Value;
		if (value < 45f || (!(value < 135f) && (value < 225f || !(value < 360f))))
		{
			Vector3 position = cutPointParticles.transform.position;
			position.y = collision.transform.position.y;
			if ((bool)cutEffectPrefab)
			{
				cutEffectPrefab.Spawn(position);
			}
			if ((bool)cutPointParticles)
			{
				cutPointParticles.transform.position = position;
				cutPointParticles.SetActive(value: true);
			}
			Cut();
		}
	}

	public void Cut()
	{
		activated = true;
		if ((bool)body)
		{
			body.isKinematic = false;
		}
		if ((bool)cutParticles)
		{
			cutParticles.SetActive(value: true);
		}
		if ((bool)platform.enemyDetector)
		{
			platform.enemyDetector.gameObject.SetActive(value: true);
		}
		platform.respondOnLand = false;
		if (platform.landRoutine != null)
		{
			StopCoroutine(platform.landRoutine);
		}
		if ((bool)audioSource && (bool)cutSound)
		{
			audioSource.PlayOneShot(cutSound);
		}
		Disable();
	}

	public void Disable(bool disableAll = false)
	{
		if (disableAll)
		{
			base.gameObject.SetActive(value: false);
		}
		else if ((bool)sprites)
		{
			sprites.SetActive(value: false);
		}
	}
}
