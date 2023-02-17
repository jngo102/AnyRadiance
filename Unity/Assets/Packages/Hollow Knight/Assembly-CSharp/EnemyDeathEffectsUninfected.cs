using UnityEngine;

public class EnemyDeathEffectsUninfected : EnemyDeathEffects
{
	[Header("Uninfected Variables")]
	public GameObject uninfectedDeathPt;

	public GameObject slashEffectGhost1;

	public GameObject slashEffectGhost2;

	public GameObject whiteWave;

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
		uninfectedDeathPt.Spawn(base.transform.position + effectOrigin);
		FlingUtils.Config config = default(FlingUtils.Config);
		config.Prefab = slashEffectGhost1;
		config.AmountMin = 8;
		config.AmountMax = 8;
		config.SpeedMin = 2f;
		config.SpeedMax = 35f;
		config.AngleMin = 0f;
		config.AngleMax = 360f;
		config.OriginVariationX = 0f;
		config.OriginVariationY = 0f;
		FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
		config = default(FlingUtils.Config);
		config.Prefab = slashEffectGhost2;
		config.AmountMin = 2;
		config.AmountMax = 3;
		config.SpeedMin = 2f;
		config.SpeedMax = 35f;
		config.AngleMin = 0f;
		config.AngleMax = 360f;
		config.OriginVariationX = 0f;
		config.OriginVariationY = 0f;
		FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
		EmitSound();
		ShakeCameraIfVisible("EnemyKillShake");
		Object.Instantiate(whiteWave, base.transform.position + effectOrigin, Quaternion.identity);
	}
}
