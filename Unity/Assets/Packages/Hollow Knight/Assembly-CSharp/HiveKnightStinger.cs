using System;
using UnityEngine;

public class HiveKnightStinger : MonoBehaviour
{
	public float direction;

	private float speed = 20f;

	private float time = 2f;

	private float timer;

	private bool initialised;

	private Rigidbody2D rb;

	private Vector3 startPos;

	private void OnEnable()
	{
		if (!initialised)
		{
			startPos = base.transform.localPosition;
			initialised = true;
		}
		else
		{
			base.transform.localPosition = startPos;
		}
		if (rb == null)
		{
			rb = GetComponent<Rigidbody2D>();
		}
		timer = time;
	}

	private void Update()
	{
		float x = speed * Mathf.Cos(direction * ((float)Math.PI / 180f));
		float y = speed * Mathf.Sin(direction * ((float)Math.PI / 180f));
		Vector2 velocity = default(Vector2);
		velocity.x = x;
		velocity.y = y;
		rb.velocity = velocity;
		if (timer > 0f)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
