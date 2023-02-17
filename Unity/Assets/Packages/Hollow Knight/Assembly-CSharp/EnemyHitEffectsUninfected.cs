using UnityEngine;

public class EnemyHitEffectsUninfected : MonoBehaviour, IHitEffectReciever
{
	public Vector3 effectOrigin;

	[Space]
	public AudioSource audioPlayerPrefab;

	public AudioEvent enemyDamage;

	[Space]
	public GameObject uninfectedHitPt;

	public GameObject slashEffectGhost1;

	public GameObject slashEffectGhost2;

	private SpriteFlash spriteFlash;

	[Tooltip("Disable if there are no listeners for this event, to save the expensive recursive send event.")]
	public bool sendDamageFlashEvent = true;

	private bool didFireThisFrame;

	protected void Awake()
	{
		spriteFlash = GetComponent<SpriteFlash>();
	}

	public void RecieveHitEffect(float attackDirection)
	{
		if (didFireThisFrame)
		{
			return;
		}
		if (sendDamageFlashEvent)
		{
			FSMUtility.SendEventToGameObject(base.gameObject, "DAMAGE FLASH", isRecursive: true);
		}
		enemyDamage.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
		if ((bool)spriteFlash)
		{
			spriteFlash.flashFocusHeal();
		}
		GameObject gameObject = null;
		gameObject = uninfectedHitPt.Spawn(base.transform.position + effectOrigin);
		switch (DirectionUtils.GetCardinalDirection(attackDirection))
		{
		case 0:
		{
			if ((bool)gameObject)
			{
				gameObject.transform.SetRotation2D(-45f);
			}
			FlingUtils.Config config = default(FlingUtils.Config);
			config.Prefab = slashEffectGhost1;
			config.AmountMin = 2;
			config.AmountMax = 3;
			config.SpeedMin = 20f;
			config.SpeedMax = 35f;
			config.AngleMin = -40f;
			config.AngleMax = 40f;
			config.OriginVariationX = 0f;
			config.OriginVariationY = 0f;
			FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
			config = default(FlingUtils.Config);
			config.Prefab = slashEffectGhost2;
			config.AmountMin = 2;
			config.AmountMax = 3;
			config.SpeedMin = 10f;
			config.SpeedMax = 35f;
			config.AngleMin = -40f;
			config.AngleMax = 40f;
			config.OriginVariationX = 0f;
			config.OriginVariationY = 0f;
			FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
			break;
		}
		case 2:
		{
			if ((bool)gameObject)
			{
				gameObject.transform.SetRotation2D(-225f);
			}
			FlingUtils.Config config = default(FlingUtils.Config);
			config.Prefab = slashEffectGhost1;
			config.AmountMin = 2;
			config.AmountMax = 3;
			config.SpeedMin = 20f;
			config.SpeedMax = 35f;
			config.AngleMin = 140f;
			config.AngleMax = 220f;
			config.OriginVariationX = 0f;
			config.OriginVariationY = 0f;
			FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
			config = default(FlingUtils.Config);
			config.Prefab = slashEffectGhost2;
			config.AmountMin = 2;
			config.AmountMax = 3;
			config.SpeedMin = 10f;
			config.SpeedMax = 35f;
			config.AngleMin = 140f;
			config.AngleMax = 220f;
			config.OriginVariationX = 0f;
			config.OriginVariationY = 0f;
			FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
			break;
		}
		case 1:
		{
			if ((bool)gameObject)
			{
				gameObject.transform.SetRotation2D(45f);
			}
			FlingUtils.Config config = default(FlingUtils.Config);
			config.Prefab = slashEffectGhost1;
			config.AmountMin = 2;
			config.AmountMax = 3;
			config.SpeedMin = 20f;
			config.SpeedMax = 35f;
			config.AngleMin = 50f;
			config.AngleMax = 130f;
			config.OriginVariationX = 0f;
			config.OriginVariationY = 0f;
			FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
			config = default(FlingUtils.Config);
			config.Prefab = slashEffectGhost2;
			config.AmountMin = 2;
			config.AmountMax = 3;
			config.SpeedMin = 10f;
			config.SpeedMax = 35f;
			config.AngleMin = 50f;
			config.AngleMax = 130f;
			config.OriginVariationX = 0f;
			config.OriginVariationY = 0f;
			FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
			break;
		}
		case 3:
		{
			if ((bool)gameObject)
			{
				gameObject.transform.SetRotation2D(225f);
			}
			FlingUtils.Config config = default(FlingUtils.Config);
			config.Prefab = slashEffectGhost1;
			config.AmountMin = 2;
			config.AmountMax = 3;
			config.SpeedMin = 20f;
			config.SpeedMax = 35f;
			config.AngleMin = 230f;
			config.AngleMax = 310f;
			config.OriginVariationX = 0f;
			config.OriginVariationY = 0f;
			FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
			config = default(FlingUtils.Config);
			config.Prefab = slashEffectGhost2;
			config.AmountMin = 2;
			config.AmountMax = 3;
			config.SpeedMin = 10f;
			config.SpeedMax = 35f;
			config.AngleMin = 230f;
			config.AngleMax = 310f;
			config.OriginVariationX = 0f;
			config.OriginVariationY = 0f;
			FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
			break;
		}
		}
		didFireThisFrame = true;
	}

	protected void Update()
	{
		didFireThisFrame = false;
	}
}
