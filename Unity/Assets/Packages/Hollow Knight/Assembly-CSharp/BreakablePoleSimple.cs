using System;
using UnityEngine;

public class BreakablePoleSimple : MonoBehaviour
{
	public GameObject bottom;

	public GameObject top;

	public float speed = 17f;

	public float angleMin = 40f;

	public float angleMax = 60f;

	[Space]
	public GameObject slashEffectPrefab;

	public float slashAngleMin = 340f;

	public float slashAngleMax = 380f;

	[Space]
	public float audioPitchMin = 0.85f;

	public float audioPitchMax = 1.15f;

	private bool activated;

	private AudioSource source;

	private void Awake()
	{
		source = GetComponent<AudioSource>();
	}

	private void Start()
	{
		if (Mathf.Abs(base.transform.position.z - 0.004f) > 1f)
		{
			if ((bool)source)
			{
				source.enabled = false;
			}
			Collider2D component = GetComponent<Collider2D>();
			if ((bool)component)
			{
				component.enabled = false;
			}
			base.enabled = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (activated)
		{
			return;
		}
		bool flag = false;
		float num = 1f;
		if (collision.tag == "Nail Attack")
		{
			float value = PlayMakerFSM.FindFsmOnGameObject(collision.gameObject, "damages_enemy").FsmVariables.FindFsmFloat("direction").Value;
			if (value < 45f)
			{
				flag = true;
				num = 1f;
			}
			else if (value < 135f)
			{
				flag = false;
			}
			else if (value < 225f)
			{
				flag = true;
				num = -1f;
			}
			else if (value < 360f)
			{
				flag = false;
			}
		}
		else if (collision.tag == "Hero Spell")
		{
			flag = true;
		}
		if (!flag)
		{
			return;
		}
		SpriteRenderer component = GetComponent<SpriteRenderer>();
		if ((bool)component)
		{
			component.enabled = false;
		}
		if ((bool)bottom)
		{
			bottom.SetActive(value: true);
		}
		if ((bool)top)
		{
			top.SetActive(value: true);
			float num2 = UnityEngine.Random.Range(angleMin, angleMax);
			Vector2 velocity = default(Vector2);
			velocity.x = speed * Mathf.Cos(num2 * ((float)Math.PI / 180f)) * num;
			velocity.y = speed * Mathf.Sin(num2 * ((float)Math.PI / 180f));
			Rigidbody2D component2 = top.GetComponent<Rigidbody2D>();
			if ((bool)component2)
			{
				component2.velocity = velocity;
			}
		}
		if ((bool)slashEffectPrefab)
		{
			GameObject obj = slashEffectPrefab.Spawn(base.transform.position);
			obj.transform.SetScaleX(num);
			obj.transform.SetRotationZ(UnityEngine.Random.Range(slashAngleMin, slashAngleMax));
		}
		if ((bool)source)
		{
			source.pitch = UnityEngine.Random.Range(audioPitchMin, audioPitchMax);
			source.Play();
		}
		activated = true;
	}
}
