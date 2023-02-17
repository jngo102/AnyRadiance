using UnityEngine;

public class EnemyDeathEffectsDung : EnemyDeathEffects
{
	[Header("Dung Variables")]
	public GameObject deathPuffDung;

	protected override void EmitEffects()
	{
		if (corpse != null)
		{
			SpriteFlash component = corpse.GetComponent<SpriteFlash>();
			if (component != null)
			{
				component.flashDung();
			}
		}
		deathPuffDung.Spawn(base.transform.position + effectOrigin);
		EmitSound();
		ShakeCameraIfVisible("AverageShake");
		Object.Instantiate(deathWaveInfectedPrefab, base.transform.position + effectOrigin, Quaternion.identity).transform.localScale = new Vector3(2f, 2f, 2f);
		GameManager.instance.FreezeMoment(1);
	}
}
