using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using Modding;
using UnityEngine;
using UnityEngine.Audio;

public class HealthManager : MonoBehaviour, IHitResponder
{
	[Serializable]
	private struct HPScaleGG
	{
		public int level1;

		public int level2;

		public int level3;

		public int GetScaledHP(int originalHP)
		{
			if (BossSceneController.IsBossScene)
			{
				switch (BossSceneController.Instance.BossLevel)
				{
				case 0:
					if (level1 <= 0)
					{
						return originalHP;
					}
					return level1;
				case 1:
					if (level2 <= 0)
					{
						return originalHP;
					}
					return level2;
				case 2:
					if (level3 <= 0)
					{
						return originalHP;
					}
					return level3;
				}
			}
			return originalHP;
		}
	}

	public delegate void DeathEvent();

	private BoxCollider2D boxCollider;

	private Recoil recoil;

	private IHitEffectReciever hitEffectReceiver;

	private EnemyDeathEffects enemyDeathEffects;

	private tk2dSpriteAnimator animator;

	private tk2dSprite sprite;

	private DamageHero damageHero;

	[Header("Assets")]
	[SerializeField]
	private AudioSource audioPlayerPrefab;

	[SerializeField]
	private AudioEvent regularInvincibleAudio;

	[SerializeField]
	private GameObject blockHitPrefab;

	[SerializeField]
	private GameObject strikeNailPrefab;

	[SerializeField]
	private GameObject slashImpactPrefab;

	[SerializeField]
	private GameObject fireballHitPrefab;

	[SerializeField]
	private GameObject sharpShadowImpactPrefab;

	[SerializeField]
	private GameObject corpseSplatPrefab;

	[SerializeField]
	private AudioEvent enemyDeathSwordAudio;

	[SerializeField]
	private AudioEvent enemyDamageAudio;

	[SerializeField]
	private GameObject smallGeoPrefab;

	[SerializeField]
	private GameObject mediumGeoPrefab;

	[SerializeField]
	private GameObject largeGeoPrefab;

	[Header("Body")]
	[SerializeField]
	public int hp;

	[SerializeField]
	private int enemyType;

	[SerializeField]
	private Vector3 effectOrigin;

	[SerializeField]
	private bool ignoreKillAll;

	[SerializeField]
	[Space]
	[UnityEngine.Tooltip("HP is scaled if in a GG boss scene (These are absolute values, not a multiplier. Leave 0 for no scaling).")]
	private HPScaleGG hpScale;

	[Header("Scene")]
	[SerializeField]
	private GameObject battleScene;

	[SerializeField]
	private GameObject sendHitTo;

	[SerializeField]
	private GameObject sendKilledToObject;

	[SerializeField]
	private string sendKilledToName;

	[Header("Geo")]
	[SerializeField]
	private int smallGeoDrops;

	[SerializeField]
	private int mediumGeoDrops;

	[SerializeField]
	private int largeGeoDrops;

	[SerializeField]
	private bool megaFlingGeo;

	[Header("Hit")]
	[SerializeField]
	private bool hasAlternateHitAnimation;

	[SerializeField]
	private string alternateHitAnimation;

	[Header("Invincible")]
	[SerializeField]
	private bool invincible;

	[SerializeField]
	private int invincibleFromDirection;

	[SerializeField]
	private bool preventInvincibleEffect;

	[SerializeField]
	private bool hasAlternateInvincibleSound;

	[SerializeField]
	private AudioClip alternateInvincibleSound;

	[Header("Death")]
	[SerializeField]
	private AudioMixerSnapshot deathAudioSnapshot;

	[SerializeField]
	public bool hasSpecialDeath;

	[SerializeField]
	public bool deathReset;

	[SerializeField]
	public bool damageOverride;

	[SerializeField]
	private bool ignoreAcid;

	[Space]
	[SerializeField]
	private bool showGodfinderIcon;

	[SerializeField]
	private float showGodFinderDelay = 7f;

	[SerializeField]
	private BossScene unlockBossScene;

	[Header("Deprecated/Unusued Variables")]
	[SerializeField]
	private bool ignoreHazards;

	[SerializeField]
	private bool ignoreWater;

	[SerializeField]
	private float invulnerableTime;

	[SerializeField]
	private bool semiPersistent;

	public bool isDead;

	private GameObject sendKilledTo;

	private float evasionByHitRemaining;

	private int directionOfLastAttack;

	private PlayMakerFSM stunControlFSM;

	private bool notifiedBattleScene;

	private const string CheckPersistenceKey = "CheckPersistence";

	public bool IsInvincible
	{
		get
		{
			return invincible;
		}
		set
		{
			invincible = value;
		}
	}

	public int InvincibleFromDirection
	{
		get
		{
			return invincibleFromDirection;
		}
		set
		{
			invincibleFromDirection = value;
		}
	}

	public event DeathEvent OnDeath;

	protected void Awake()
	{
		boxCollider = GetComponent<BoxCollider2D>();
		recoil = GetComponent<Recoil>();
		hitEffectReceiver = GetComponent<IHitEffectReciever>();
		enemyDeathEffects = GetComponent<EnemyDeathEffects>();
		animator = GetComponent<tk2dSpriteAnimator>();
		sprite = GetComponent<tk2dSprite>();
		damageHero = GetComponent<DamageHero>();
		PlayMakerFSM[] components = base.gameObject.GetComponents<PlayMakerFSM>();
		foreach (PlayMakerFSM playMakerFSM in components)
		{
			if (playMakerFSM.FsmName == "Stun Control" || playMakerFSM.FsmName == "Stun")
			{
				stunControlFSM = playMakerFSM;
				break;
			}
		}
		PersistentBoolItem component = GetComponent<PersistentBoolItem>();
		if (!(component != null))
		{
			return;
		}
		component.OnGetSaveState += delegate(ref bool val)
		{
			if (GameManager.instance.GetCurrentMapZone() != "COLOSSEUM")
			{
				val = isDead;
			}
		};
		component.OnSetSaveState += delegate(bool val)
		{
			if (GameManager.instance.GetCurrentMapZone() != "COLOSSEUM" && val)
			{
				isDead = true;
				base.gameObject.SetActive(value: false);
			}
		};
	}

	protected void OnEnable()
	{
		StartCoroutine("CheckPersistence");
	}

	protected void Start()
	{
		evasionByHitRemaining = -1f;
		if (!string.IsNullOrEmpty(sendKilledToName))
		{
			sendKilledTo = GameObject.Find(sendKilledToName);
			if (sendKilledTo == null)
			{
				Debug.LogErrorFormat(this, "Failed to find GameObject '{0}' to send KILLED to.", sendKilledToName);
			}
		}
		else if (sendKilledToObject != null)
		{
			sendKilledTo = sendKilledToObject;
		}
		int baseHP = hp;
		hp = hpScale.GetScaledHP(hp);
		BossSceneController.ReportHealth(this, baseHP, hp);
	}

	protected IEnumerator CheckPersistence()
	{
		yield return null;
		isDead = ModHooks.OnEnableEnemy(gameObject, isDead);
		if (isDead)
		{
			gameObject.SetActive(value: false);
		}
	}

	protected void Update()
	{
		evasionByHitRemaining -= Time.deltaTime;
	}

	public void Hit(HitInstance hitInstance)
	{
		if (!isDead && !(evasionByHitRemaining > 0f) && hitInstance.DamageDealt > 0)
		{
			FSMUtility.SendEventToGameObject(hitInstance.Source, "DEALT DAMAGE");
			int cardinalDirection = DirectionUtils.GetCardinalDirection(hitInstance.GetActualDirection(base.transform));
			if (IsBlockingByDirection(cardinalDirection, hitInstance.AttackType))
			{
				Invincible(hitInstance);
			}
			else
			{
				TakeDamage(hitInstance);
			}
		}
	}

	private void Invincible(HitInstance hitInstance)
	{
		int num = (directionOfLastAttack = DirectionUtils.GetCardinalDirection(hitInstance.GetActualDirection(base.transform)));
		FSMUtility.SendEventToGameObject(base.gameObject, "BLOCKED HIT");
		FSMUtility.SendEventToGameObject(hitInstance.Source, "HIT LANDED");
		if (!(GetComponent<DontClinkGates>() != null))
		{
			FSMUtility.SendEventToGameObject(base.gameObject, "HIT");
			if (!preventInvincibleEffect)
			{
				if (hitInstance.AttackType == AttackTypes.Nail)
				{
					switch (num)
					{
					case 0:
						HeroController.instance.RecoilLeft();
						break;
					case 2:
						HeroController.instance.RecoilRight();
						break;
					}
				}
				GameManager.instance.FreezeMoment(1);
				GameCameras.instance.cameraShakeFSM.SendEvent("EnemyKillShake");
				Vector2 vector;
				Vector3 eulerAngles;
				if (boxCollider != null)
				{
					switch (num)
					{
					case 0:
						vector = new Vector2(base.transform.GetPositionX() + boxCollider.offset.x - boxCollider.size.x * 0.5f, hitInstance.Source.transform.GetPositionY());
						eulerAngles = new Vector3(0f, 0f, 0f);
						FSMUtility.SendEventToGameObject(base.gameObject, "BLOCKED HIT R");
						break;
					case 2:
						vector = new Vector2(base.transform.GetPositionX() + boxCollider.offset.x + boxCollider.size.x * 0.5f, hitInstance.Source.transform.GetPositionY());
						eulerAngles = new Vector3(0f, 0f, 180f);
						FSMUtility.SendEventToGameObject(base.gameObject, "BLOCKED HIT L");
						break;
					case 1:
						vector = new Vector2(hitInstance.Source.transform.GetPositionX(), Mathf.Max(hitInstance.Source.transform.GetPositionY(), base.transform.GetPositionY() + boxCollider.offset.y - boxCollider.size.y * 0.5f));
						eulerAngles = new Vector3(0f, 0f, 90f);
						FSMUtility.SendEventToGameObject(base.gameObject, "BLOCKED HIT U");
						break;
					case 3:
						vector = new Vector2(hitInstance.Source.transform.GetPositionX(), Mathf.Min(hitInstance.Source.transform.GetPositionY(), base.transform.GetPositionY() + boxCollider.offset.y + boxCollider.size.y * 0.5f));
						eulerAngles = new Vector3(0f, 0f, 270f);
						FSMUtility.SendEventToGameObject(base.gameObject, "BLOCKED DOWN");
						break;
					default:
						vector = base.transform.position;
						eulerAngles = new Vector3(0f, 0f, 0f);
						break;
					}
				}
				else
				{
					vector = base.transform.position;
					eulerAngles = new Vector3(0f, 0f, 0f);
				}
				GameObject obj = blockHitPrefab.Spawn();
				obj.transform.position = vector;
				obj.transform.eulerAngles = eulerAngles;
				if (hasAlternateInvincibleSound)
				{
					AudioSource component = GetComponent<AudioSource>();
					if (alternateInvincibleSound != null && component != null)
					{
						component.PlayOneShot(alternateInvincibleSound);
					}
				}
				else
				{
					regularInvincibleAudio.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
				}
			}
		}
		evasionByHitRemaining = 0.15f;
	}

	private void TakeDamage(HitInstance hitInstance)
	{
		if (hitInstance.AttackType == AttackTypes.Acid && ignoreAcid)
		{
			return;
		}
		if (CheatManager.IsInstaKillEnabled)
		{
			hitInstance.DamageDealt = 9999;
		}
		int num = (directionOfLastAttack = DirectionUtils.GetCardinalDirection(hitInstance.GetActualDirection(base.transform)));
		FSMUtility.SendEventToGameObject(base.gameObject, "HIT");
		FSMUtility.SendEventToGameObject(hitInstance.Source, "HIT LANDED");
		FSMUtility.SendEventToGameObject(base.gameObject, "TOOK DAMAGE");
		if (sendHitTo != null)
		{
			FSMUtility.SendEventToGameObject(sendHitTo, "HIT");
		}
		if (recoil != null)
		{
			recoil.RecoilByDirection(num, hitInstance.MagnitudeMultiplier);
		}
		switch (hitInstance.AttackType)
		{
		case AttackTypes.Nail:
		case AttackTypes.NailBeam:
		{
			if (hitInstance.AttackType == AttackTypes.Nail && enemyType != 3 && enemyType != 6)
			{
				HeroController.instance.SoulGain();
			}
			Vector3 position = (hitInstance.Source.transform.position + base.transform.position) * 0.5f + effectOrigin;
			strikeNailPrefab.Spawn(position, Quaternion.identity);
			GameObject gameObject = slashImpactPrefab.Spawn(position, Quaternion.identity);
			switch (num)
			{
			case 2:
				gameObject.transform.SetRotation2D(UnityEngine.Random.Range(340, 380));
				gameObject.transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
				break;
			case 0:
				gameObject.transform.SetRotation2D(UnityEngine.Random.Range(340, 380));
				gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
				break;
			case 1:
				gameObject.transform.SetRotation2D(UnityEngine.Random.Range(70, 110));
				gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
				break;
			case 3:
				gameObject.transform.SetRotation2D(UnityEngine.Random.Range(250, 290));
				gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
				break;
			}
			break;
		}
		case AttackTypes.Generic:
			strikeNailPrefab.Spawn(base.transform.position + effectOrigin, Quaternion.identity).transform.SetPositionZ(0.0031f);
			break;
		case AttackTypes.Spell:
			fireballHitPrefab.Spawn(base.transform.position + effectOrigin, Quaternion.identity).transform.SetPositionZ(0.0031f);
			break;
		case AttackTypes.SharpShadow:
			sharpShadowImpactPrefab.Spawn(base.transform.position + effectOrigin, Quaternion.identity);
			break;
		}
		if (hitEffectReceiver != null && hitInstance.AttackType != AttackTypes.RuinsWater)
		{
			hitEffectReceiver.RecieveHitEffect(hitInstance.GetActualDirection(base.transform));
		}
		int num2 = Mathf.RoundToInt((float)hitInstance.DamageDealt * hitInstance.Multiplier);
		if (damageOverride)
		{
			num2 = 1;
		}
		hp = Mathf.Max(hp - num2, -50);
		if (hp > 0)
		{
			NonFatalHit(hitInstance.IgnoreInvulnerable);
			if ((bool)stunControlFSM)
			{
				stunControlFSM.SendEvent("STUN DAMAGE");
			}
		}
		else
		{
			Die(hitInstance.GetActualDirection(base.transform), hitInstance.AttackType, hitInstance.IgnoreInvulnerable);
		}
	}

	private void NonFatalHit(bool ignoreEvasion)
	{
		if (ignoreEvasion)
		{
			return;
		}
		if (hasAlternateHitAnimation)
		{
			if (animator != null)
			{
				animator.Play(alternateHitAnimation);
			}
		}
		else
		{
			evasionByHitRemaining = 0.2f;
		}
	}

	public void ApplyExtraDamage(int damageAmount)
	{
		FSMUtility.SendEventToGameObject(base.gameObject, "EXTRA DAMAGED");
		hp = Mathf.Max(hp - damageAmount, 0);
		if (hp <= 0)
		{
			Die(null, AttackTypes.Generic, ignoreEvasion: true);
		}
	}

	public void Die(float? attackDirection, AttackTypes attackType, bool ignoreEvasion)
	{
		if (isDead)
		{
			return;
		}
		if ((bool)sprite)
		{
			sprite.color = Color.white;
		}
		FSMUtility.SendEventToGameObject(base.gameObject, "ZERO HP");
		if (showGodfinderIcon)
		{
			GodfinderIcon.ShowIcon(showGodFinderDelay, unlockBossScene);
		}
		if ((bool)unlockBossScene && !GameManager.instance.playerData.GetVariable<List<string>>("unlockedBossScenes").Contains(unlockBossScene.name))
		{
			GameManager.instance.playerData.GetVariable<List<string>>("unlockedBossScenes").Add(unlockBossScene.name);
		}
		if (hasSpecialDeath)
		{
			NonFatalHit(ignoreEvasion);
			return;
		}
		isDead = true;
		if (damageHero != null)
		{
			damageHero.damageDealt = 0;
		}
		if (battleScene != null && !notifiedBattleScene)
		{
			PlayMakerFSM playMakerFSM = FSMUtility.LocateFSM(battleScene, "Battle Control");
			if (playMakerFSM != null)
			{
				FsmInt fsmInt = playMakerFSM.FsmVariables.GetFsmInt("Battle Enemies");
				if (fsmInt != null)
				{
					fsmInt.Value--;
					notifiedBattleScene = true;
				}
			}
		}
		if (deathAudioSnapshot != null)
		{
			deathAudioSnapshot.TransitionTo(6f);
		}
		if (sendKilledTo != null)
		{
			FSMUtility.SendEventToGameObject(sendKilledTo, "KILLED");
		}
		switch (attackType)
		{
		case AttackTypes.Splatter:
			GameCameras.instance.cameraShakeFSM.SendEvent("AverageShake");
			Debug.LogWarningFormat(this, "Instantiate!");
			UnityEngine.Object.Instantiate(corpseSplatPrefab, base.transform.position + effectOrigin, Quaternion.identity);
			if ((bool)enemyDeathEffects)
			{
				enemyDeathEffects.EmitSound();
			}
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		default:
		{
			float angleMin = (megaFlingGeo ? 65 : 80);
			float angleMax = (megaFlingGeo ? 115 : 100);
			float speedMin = (megaFlingGeo ? 30 : 15);
			float speedMax = (megaFlingGeo ? 45 : 30);
			int num = smallGeoDrops;
			int num2 = mediumGeoDrops;
			int num3 = largeGeoDrops;
			bool flag = false;
			if (GameManager.instance.playerData.GetBool("equippedCharm_24") && !GameManager.instance.playerData.GetBool("brokenCharm_24"))
			{
				num += Mathf.CeilToInt((float)num * 0.2f);
				num2 += Mathf.CeilToInt((float)num2 * 0.2f);
				num3 += Mathf.CeilToInt((float)num3 * 0.2f);
				flag = true;
			}
			FlingUtils.Config config = default(FlingUtils.Config);
			config.Prefab = smallGeoPrefab;
			config.AmountMin = num;
			config.AmountMax = num;
			config.SpeedMin = speedMin;
			config.SpeedMax = speedMax;
			config.AngleMin = angleMin;
			config.AngleMax = angleMax;
			GameObject[] gameObjects = FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
			if (flag)
			{
				SetGeoFlashing(gameObjects, smallGeoDrops);
			}
			config = default(FlingUtils.Config);
			config.Prefab = mediumGeoPrefab;
			config.AmountMin = num2;
			config.AmountMax = num2;
			config.SpeedMin = speedMin;
			config.SpeedMax = speedMax;
			config.AngleMin = angleMin;
			config.AngleMax = angleMax;
			gameObjects = FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
			if (flag)
			{
				SetGeoFlashing(gameObjects, mediumGeoDrops);
			}
			config = default(FlingUtils.Config);
			config.Prefab = largeGeoPrefab;
			config.AmountMin = num3;
			config.AmountMax = num3;
			config.SpeedMin = speedMin;
			config.SpeedMax = speedMax;
			config.AngleMin = angleMin;
			config.AngleMax = angleMax;
			gameObjects = FlingUtils.SpawnAndFling(config, base.transform, effectOrigin);
			if (flag)
			{
				SetGeoFlashing(gameObjects, largeGeoDrops);
			}
			break;
		}
		case AttackTypes.RuinsWater:
			break;
		}
		if (enemyDeathEffects != null)
		{
			if (attackType == AttackTypes.RuinsWater || attackType == AttackTypes.Acid || attackType == AttackTypes.Generic)
			{
				enemyDeathEffects.doKillFreeze = false;
			}
			enemyDeathEffects.RecieveDeathEvent(attackDirection, deathReset, attackType == AttackTypes.Spell, attackType == AttackTypes.RuinsWater || attackType == AttackTypes.Acid);
		}
		SendDeathEvent();
	}

	public void SendDeathEvent()
	{
		if (this.OnDeath != null)
		{
			this.OnDeath();
		}
	}

	private void SetGeoFlashing(GameObject[] gameObjects, int originalAmount)
	{
		for (int num = gameObjects.Length - 1; num >= originalAmount; num--)
		{
			GeoControl component = gameObjects[num].GetComponent<GeoControl>();
			if ((bool)component)
			{
				component.SetFlashing();
			}
		}
	}

	public bool IsBlockingByDirection(int cardinalDirection, AttackTypes attackType)
	{
		if ((attackType == AttackTypes.Spell || attackType == AttackTypes.SharpShadow) && base.gameObject.CompareTag("Spell Vulnerable"))
		{
			return false;
		}
		if (!invincible)
		{
			return false;
		}
		if (invincibleFromDirection == 0)
		{
			return true;
		}
		switch (cardinalDirection)
		{
		case 0:
			switch (invincibleFromDirection)
			{
			case 1:
			case 5:
			case 8:
			case 10:
				return true;
			default:
				return false;
			}
		case 1:
		{
			int num = invincibleFromDirection;
			if (num == 2 || (uint)(num - 5) <= 4u)
			{
				return true;
			}
			return false;
		}
		case 2:
			switch (invincibleFromDirection)
			{
			case 3:
			case 6:
			case 9:
			case 11:
				return true;
			default:
				return false;
			}
		case 3:
		{
			int num = invincibleFromDirection;
			if (num == 4 || (uint)(num - 7) <= 4u)
			{
				return true;
			}
			return false;
		}
		default:
			return false;
		}
	}

	public void SetBattleScene(GameObject newBattleScene)
	{
		battleScene = newBattleScene;
	}

	public int GetAttackDirection()
	{
		return directionOfLastAttack;
	}

	public void SetPreventInvincibleEffect(bool set)
	{
		preventInvincibleEffect = set;
	}

	public void SetGeoSmall(int amount)
	{
		smallGeoDrops = amount;
	}

	public void SetGeoMedium(int amount)
	{
		mediumGeoDrops = amount;
	}

	public void SetGeoLarge(int amount)
	{
		largeGeoDrops = amount;
	}

	public bool GetIsDead()
	{
		return isDead;
	}

	public void SetIsDead(bool set)
	{
		isDead = set;
	}

	public void SetDamageOverride(bool set)
	{
		damageOverride = set;
	}

	public void SetSendKilledToObject(GameObject killedObject)
	{
		if (killedObject != null)
		{
			sendKilledToObject = killedObject;
		}
	}

	public bool CheckInvincible()
	{
		return invincible;
	}
}
