using UnityEngine;

public class EnemyDeathEffectsBlackKnight : EnemyDeathEffects
{
	protected override void EmitEffects()
	{
		if (corpse != null)
		{
			SpriteFlash component = corpse.GetComponent<SpriteFlash>();
			if (component != null)
			{
				component.flashFocusHeal();
			}
		}
		deathPuffLargePrefab.Spawn(base.transform.position + effectOrigin);
		EmitSound();
		ShakeCameraIfVisible("AverageShake");
		Object.Instantiate(deathWaveInfectedPrefab, base.transform.position + effectOrigin, Quaternion.identity).transform.localScale = new Vector3(2f, 2f, 2f);
	}
}
