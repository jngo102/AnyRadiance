using HutongGames.PlayMaker;
using UnityEngine;

public class ExtraDamageable : MonoBehaviour, IExtraDamageable
{
	private PlayMakerFSM healthManagerFsm;

	private FsmBool invincibleVar;

	private FsmInt hpVar;

	private SpriteFlash spriteFlash;

	private bool isSpellVulnerable;

	private HealthManager healthManager;

	[SerializeField]
	private RandomAudioClipTable impactClipTable;

	[SerializeField]
	private AudioSource audioPlayerPrefab;

	private bool damagedThisFrame;

	protected void Awake()
	{
		healthManagerFsm = FSMUtility.LocateFSM(base.gameObject, "health_manager_enemy");
		if (healthManagerFsm != null)
		{
			invincibleVar = healthManagerFsm.FsmVariables.GetFsmBool("Invincible");
			hpVar = healthManagerFsm.FsmVariables.GetFsmInt("HP");
		}
		healthManager = GetComponent<HealthManager>();
		spriteFlash = GetComponent<SpriteFlash>();
		isSpellVulnerable = base.gameObject.CompareTag("Spell Vulnerable");
	}

	private void LateUpdate()
	{
		damagedThisFrame = false;
	}

	public void RecieveExtraDamage(ExtraDamageTypes extraDamageType)
	{
		if (damagedThisFrame)
		{
			return;
		}
		damagedThisFrame = true;
		if (!isSpellVulnerable && ((invincibleVar != null && invincibleVar.Value) || (healthManager != null && healthManager.IsInvincible)))
		{
			return;
		}
		impactClipTable.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
		if (spriteFlash != null)
		{
			switch (extraDamageType)
			{
			case ExtraDamageTypes.Spore:
				spriteFlash.flashSporeQuick();
				break;
			case ExtraDamageTypes.Dung:
			case ExtraDamageTypes.Dung2:
				spriteFlash.flashDungQuick();
				break;
			}
		}
		ApplyExtraDamageToHealthManager(GetDamageOfType(extraDamageType));
	}

	public static int GetDamageOfType(ExtraDamageTypes extraDamageTypes)
	{
		if ((uint)extraDamageTypes <= 1u || extraDamageTypes != ExtraDamageTypes.Dung2)
		{
			return 1;
		}
		return 2;
	}

	private void ApplyExtraDamageToHealthManager(int damageAmount)
	{
		if (healthManagerFsm != null && hpVar != null)
		{
			int num = hpVar.Value - damageAmount;
			hpVar.Value = num;
			if (num <= 0)
			{
				FSMUtility.SendEventToGameObject(base.gameObject, "EXTRA KILL");
			}
		}
		if (healthManager != null)
		{
			healthManager.ApplyExtraDamage(damageAmount);
		}
	}
}
