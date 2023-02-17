using System;
using UnityEngine;

public class BrokenVesselGlob : MonoBehaviour
{
	public ParticleSystem gasParticle;

	public Rigidbody2D rb;

	private float timer;

	private Vector2 force = new Vector2(0f, 25f);

	private float stretchFactor = 2f;

	private float stretchMinX = 1f;

	private float stretchMaxY = 2f;

	private float scaleModifier = 2f;

	private float scaleModifierMin = 2f;

	private float scaleModifierMax = 2f;

	private void OnEnable()
	{
		base.transform.localScale = new Vector3(2f, 2f, 2f);
		gasParticle.Play();
		rb.velocity = new Vector3(rb.velocity.x, -17.5f);
		timer = 5f;
	}

	private void Update()
	{
		FaceAngle();
		ProjectileSquash();
		if (timer >= 0f)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			base.gameObject.Recycle();
		}
	}

	private void FixedUpdate()
	{
		rb.AddForce(force);
	}

	private void FaceAngle()
	{
		Vector2 velocity = rb.velocity;
		float z = Mathf.Atan2(velocity.y, velocity.x) * (180f / (float)Math.PI);
		base.transform.localEulerAngles = new Vector3(0f, 0f, z);
	}

	private void ProjectileSquash()
	{
		float num = 1f - rb.velocity.magnitude * stretchFactor * 0.01f;
		float num2 = 1f + rb.velocity.magnitude * stretchFactor * 0.01f;
		if (num2 < stretchMinX)
		{
			num2 = stretchMinX;
		}
		if (num > stretchMaxY)
		{
			num = stretchMaxY;
		}
		num *= scaleModifier;
		num2 *= scaleModifier;
		base.transform.localScale = new Vector3(num2, num, base.transform.localScale.z);
	}
}
