public class EnemyDeathEffectsNoEffect : EnemyDeathEffects
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
		doKillFreeze = false;
	}
}
