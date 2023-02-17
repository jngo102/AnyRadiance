using System.Collections;
using UnityEngine;

public class CorpseDeathStun : Corpse
{
	protected override void Start()
	{
		StartCoroutine(DeathStun());
	}

	private IEnumerator DeathStun()
	{
		SpriteFlash spriteFlash = GetComponent<SpriteFlash>();
		if ((bool)spriteFlash)
		{
			spriteFlash.flashInfectedLoop();
		}
		Vector2 velocity = body.velocity;
		body.isKinematic = true;
		body.velocity = Vector2.zero;
		yield return StartCoroutine(Jitter(0.75f));
		if ((bool)spriteFlash)
		{
			spriteFlash.CancelFlash();
		}
		Object.Instantiate(deathWaveInfectedPrefab, transform.position, Quaternion.identity).transform.localScale = new Vector3(2f, 2f, 2f);
		body.isKinematic = false;
		body.velocity = velocity;
		base.Start();
	}

	private IEnumerator Jitter(float duration)
	{
		Transform sprite = spriteAnimator.transform;
		Vector3 initialPos = sprite.position;
		for (float elapsed = 0f; elapsed < duration; elapsed += Time.deltaTime)
		{
			sprite.position = initialPos + new Vector3(Random.Range(-0.15f, 0.15f), 0f, 0f);
			yield return null;
		}
		sprite.position = initialPos;
	}
}
