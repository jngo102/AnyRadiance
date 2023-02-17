using UnityEngine;

public class LiftPlatform : MonoBehaviour
{
	public GameObject part1;

	public GameObject part2;

	public ParticleSystem dustParticle;

	public AudioSource audioSource;

	private float part1_start_y;

	private float part2_start_y;

	private int state;

	private float timer;

	private void Start()
	{
		part1_start_y = part1.transform.position.y;
		part2_start_y = part2.transform.position.y;
	}

	private void Update()
	{
		if (state == 1)
		{
			if (timer < 0.12f)
			{
				part1.transform.position = new Vector3(part1.transform.position.x, part1_start_y - timer * 0.75f, part1.transform.position.z);
				part2.transform.position = new Vector3(part2.transform.position.x, part2_start_y - timer * 0.75f, part2.transform.position.z);
				timer += Time.deltaTime;
			}
			else
			{
				part1.transform.position = new Vector3(part1.transform.position.x, part1_start_y - 0.09f, part1.transform.position.z);
				part2.transform.position = new Vector3(part2.transform.position.x, part2_start_y - 0.09f, part2.transform.position.z);
				state = 2;
				timer = 0.12f;
			}
		}
		if (state == 2)
		{
			if (timer > 0f)
			{
				part1.transform.position = new Vector3(part1.transform.position.x, part1_start_y - timer * 0.75f, part1.transform.position.z);
				part2.transform.position = new Vector3(part2.transform.position.x, part2_start_y - timer * 0.75f, part2.transform.position.z);
				timer -= Time.deltaTime;
			}
			else
			{
				part1.transform.position = new Vector3(part1.transform.position.x, part1_start_y, part1.transform.position.z);
				part2.transform.position = new Vector3(part2.transform.position.x, part2_start_y, part2.transform.position.z);
				state = 0;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (state == 0 && collision.collider.gameObject.layer != 16 && collision.collider.gameObject.layer != 18 && collision.GetSafeContact().Normal.y < 0.1f)
		{
			audioSource.pitch = Random.Range(0.85f, 1.15f);
			audioSource.Play();
			dustParticle.Play();
			state = 1;
			timer = 0f;
		}
	}
}
