using System;
using HutongGames.PlayMaker;
using Modding;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyDeathEffects : MonoBehaviour
{
	[SerializeField]
	private GameObject corpsePrefab;

	[SerializeField]
	private bool corpseFacesRight;

	[SerializeField]
	private float corpseFlingSpeed;

	[SerializeField]
	public Vector3 corpseSpawnPoint;

	[SerializeField]
	private string deathBroadcastEvent;

	[SerializeField]
	public Vector3 effectOrigin;

	[SerializeField]
	private bool lowCorpseArc;

	[SerializeField]
	private string playerDataName;

	[SerializeField]
	private bool recycle;

	[SerializeField]
	private bool rotateCorpse;

	[SerializeField]
	private AudioMixerSnapshot audioSnapshotOnDeath;

	[SerializeField]
	private GameObject journalUpdateMessagePrefab;

	private static GameObject journalUpdateMessageSpawned;

	[SerializeField]
	private EnemyDeathTypes enemyDeathType;

	[SerializeField]
	protected AudioSource audioPlayerPrefab;

	[SerializeField]
	protected AudioEvent enemyDeathSwordAudio;

	[SerializeField]
	protected AudioEvent enemyDamageAudio;

	[SerializeField]
	protected AudioClip enemyDeathSwordClip;

	[SerializeField]
	protected AudioClip enemyDamageClip;

	[SerializeField]
	protected GameObject deathWaveInfectedPrefab;

	[SerializeField]
	protected GameObject deathWaveInfectedSmallPrefab;

	[SerializeField]
	protected GameObject spatterOrangePrefab;

	[SerializeField]
	protected GameObject deathPuffMedPrefab;

	[SerializeField]
	protected GameObject deathPuffLargePrefab;

	[SerializeField]
	protected GameObject dreamEssenceCorpseGetPrefab;

	protected GameObject corpse;

	private bool didFire;

	[HideInInspector]
	public bool doKillFreeze = true;

	protected void Start()
	{
		PreInstantiate();
	}

	public void PreInstantiate()
	{
		if (!corpse && (bool)corpsePrefab)
		{
			corpse = UnityEngine.Object.Instantiate(corpsePrefab, base.transform.position + corpseSpawnPoint, Quaternion.identity, base.transform);
			tk2dSprite[] componentsInChildren = corpse.GetComponentsInChildren<tk2dSprite>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ForceBuild();
			}
			corpse.SetActive(value: false);
		}
		if (!journalUpdateMessageSpawned && (bool)journalUpdateMessagePrefab)
		{
			journalUpdateMessageSpawned = UnityEngine.Object.Instantiate(journalUpdateMessagePrefab);
			journalUpdateMessageSpawned.SetActive(value: false);
		}
		PersonalObjectPool component = GetComponent<PersonalObjectPool>();
		if ((bool)component)
		{
			component.CreateStartupPools();
		}
	}

	public void RecieveDeathEvent(float? attackDirection, bool resetDeathEvent = false, bool spellBurn = false, bool isWatery = false)
	{
		ModHooks.OnRecieveDeathEvent(this, didFire, ref attackDirection, ref resetDeathEvent, ref spellBurn, ref isWatery);
		orig_RecieveDeathEvent(attackDirection, resetDeathEvent, spellBurn, isWatery);
	}

	private void RecordKillForJournal()
	{
		string killedBoolPlayerDataLookupKey = "killed" + playerDataName;
		string killCountIntPlayerDataLookupKey = "kills" + playerDataName;
		string newDataBoolPlayerDataLookupKey = "newData" + playerDataName;
		ModHooks.OnRecordKillForJournal(this, playerDataName, killedBoolPlayerDataLookupKey, killCountIntPlayerDataLookupKey, newDataBoolPlayerDataLookupKey);
		orig_RecordKillForJournal();
	}

	private void EmitCorpse(float? attackDirection, bool isWatery, bool spellBurn = false)
	{
		if (corpse == null)
		{
			return;
		}
		corpse.transform.SetParent(null);
		corpse.transform.SetPositionZ(UnityEngine.Random.Range(0.008f, 0.009f));
		corpse.SetActive(value: true);
		PlayMakerFSM playMakerFSM = FSMUtility.LocateFSM(corpse, "corpse");
		if (playMakerFSM != null)
		{
			FsmBool fsmBool = playMakerFSM.FsmVariables.GetFsmBool("spellBurn");
			if (fsmBool != null)
			{
				fsmBool.Value = false;
			}
		}
		Corpse component = corpse.GetComponent<Corpse>();
		if ((bool)component)
		{
			component.Setup(isWatery, spellBurn);
		}
		if (isWatery)
		{
			return;
		}
		corpse.transform.SetRotation2D(rotateCorpse ? base.transform.GetRotation2D() : 0f);
		if (Mathf.Abs(base.transform.eulerAngles.z) >= 45f)
		{
			Collider2D component2 = GetComponent<Collider2D>();
			Collider2D component3 = corpse.GetComponent<Collider2D>();
			if (!rotateCorpse && (bool)component2 && (bool)component3)
			{
				Vector3 vector = component2.bounds.center - component3.bounds.center;
				vector.z = 0f;
				corpse.transform.position += vector;
			}
		}
		float num = 1f;
		if (!attackDirection.HasValue)
		{
			num = 0f;
		}
		int cardinalDirection = DirectionUtils.GetCardinalDirection(attackDirection.GetValueOrDefault());
		Rigidbody2D component4 = corpse.GetComponent<Rigidbody2D>();
		if (component4 != null && !component4.isKinematic)
		{
			float num2 = corpseFlingSpeed;
			float num3;
			switch (cardinalDirection)
			{
			case 0:
				num3 = (lowCorpseArc ? 10f : 60f);
				corpse.transform.SetScaleX(corpse.transform.localScale.x * (corpseFacesRight ? (-1f) : 1f) * Mathf.Sign(base.transform.localScale.x));
				break;
			case 2:
				num3 = (lowCorpseArc ? 170f : 120f);
				corpse.transform.SetScaleX(corpse.transform.localScale.x * (corpseFacesRight ? 1f : (-1f)) * Mathf.Sign(base.transform.localScale.x));
				break;
			case 3:
				num3 = 270f;
				break;
			case 1:
				num3 = UnityEngine.Random.Range(75f, 105f);
				num2 *= 1.3f;
				break;
			default:
				num3 = 90f;
				break;
			}
			component4.velocity = new Vector2(Mathf.Cos(num3 * ((float)Math.PI / 180f)), Mathf.Sin(num3 * ((float)Math.PI / 180f))) * num2 * num;
		}
	}

	protected virtual void EmitEffects()
	{
		switch (enemyDeathType)
		{
		case EnemyDeathTypes.Infected:
			EmitInfectedEffects();
			break;
		case EnemyDeathTypes.SmallInfected:
			EmitSmallInfectedEffects();
			break;
		case EnemyDeathTypes.LargeInfected:
			EmitLargeInfectedEffects();
			break;
		default:
			Debug.LogWarningFormat(this, "Enemy death type {0} not implemented!", enemyDeathType);
			break;
		}
	}

	public void EmitSound()
	{
		enemyDeathSwordAudio.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
		enemyDamageAudio.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
	}

	private void EmitInfectedEffects()
	{
		EmitSound();
		if (corpse != null)
		{
			SpriteFlash component = corpse.GetComponent<SpriteFlash>();
			if (component != null)
			{
				component.flashInfected();
			}
		}
		GameObject obj = deathWaveInfectedPrefab.Spawn(base.transform.position + effectOrigin);
		obj.transform.SetScaleX(1.25f);
		obj.transform.SetScaleY(1.25f);
		GlobalPrefabDefaults.Instance.SpawnBlood(base.transform.position + effectOrigin, 8, 10, 15f, 20f);
		deathPuffMedPrefab.Spawn(base.transform.position + effectOrigin);
		ShakeCameraIfVisible("EnemyKillShake");
	}

	private void EmitSmallInfectedEffects()
	{
		AudioEvent audioEvent = default(AudioEvent);
		audioEvent.Clip = enemyDeathSwordClip;
		audioEvent.PitchMin = 1.2f;
		audioEvent.PitchMax = 1.4f;
		audioEvent.Volume = 1f;
		audioEvent.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
		audioEvent = default(AudioEvent);
		audioEvent.Clip = enemyDamageClip;
		audioEvent.PitchMin = 1.2f;
		audioEvent.PitchMax = 1.4f;
		audioEvent.Volume = 1f;
		audioEvent.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
		if (deathWaveInfectedSmallPrefab != null)
		{
			GameObject obj = deathWaveInfectedSmallPrefab.Spawn(base.transform.position + effectOrigin);
			Vector3 localScale = obj.transform.localScale;
			localScale.x = 0.5f;
			localScale.y = 0.5f;
			obj.transform.localScale = localScale;
		}
		GlobalPrefabDefaults.Instance.SpawnBlood(base.transform.position + effectOrigin, 8, 10, 15f, 20f);
	}

	private void EmitLargeInfectedEffects()
	{
		AudioEvent audioEvent = default(AudioEvent);
		audioEvent.Clip = enemyDeathSwordClip;
		audioEvent.PitchMin = 0.75f;
		audioEvent.PitchMax = 0.75f;
		audioEvent.Volume = 1f;
		audioEvent.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
		audioEvent = default(AudioEvent);
		audioEvent.Clip = enemyDamageClip;
		audioEvent.PitchMin = 0.75f;
		audioEvent.PitchMax = 0.75f;
		audioEvent.Volume = 1f;
		audioEvent.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
		if (corpse != null)
		{
			SpriteFlash component = corpse.GetComponent<SpriteFlash>();
			if (component != null)
			{
				component.flashInfected();
			}
		}
		if (!(deathPuffLargePrefab == null))
		{
			deathPuffLargePrefab.Spawn(base.transform.position + effectOrigin);
		}
		ShakeCameraIfVisible("AverageShake");
		if (!(deathWaveInfectedPrefab == null))
		{
			GameObject obj = deathWaveInfectedPrefab.Spawn(base.transform.position + effectOrigin);
			obj.transform.SetScaleX(2f);
			obj.transform.SetScaleY(2f);
		}
		GlobalPrefabDefaults.Instance.SpawnBlood(base.transform.position + effectOrigin, 75, 80, 20f, 25f);
	}

	protected void ShakeCameraIfVisible(string eventName)
	{
		Renderer renderer = GetComponent<Renderer>();
		if (renderer == null)
		{
			renderer = GetComponentInChildren<Renderer>();
		}
		if (renderer != null && renderer.isVisible)
		{
			GameCameras.instance.cameraShakeFSM.SendEvent(eventName);
		}
	}

	private void EmitEssence()
	{
		PlayerData playerData = GameManager.instance.playerData;
		if (playerData.GetBool("hasDreamNail"))
		{
			bool @bool = playerData.GetBool("equippedCharm_30");
			bool flag = playerData.GetInt("dreamOrbsSpent") > 0;
			int maxExclusive = ((@bool && flag) ? 40 : ((@bool && !flag) ? 200 : ((playerData.GetInt("dreamOrbsSpent") > 0) ? 60 : 300)));
			if (UnityEngine.Random.Range(0, maxExclusive) == 0)
			{
				dreamEssenceCorpseGetPrefab.Spawn(base.transform.position + effectOrigin);
				playerData.SetIntSwappedArgs(playerData.GetInt("dreamOrbs") + 1, "dreamOrbs");
				playerData.SetIntSwappedArgs(playerData.GetInt("dreamOrbsSpent") - 1, "dreamOrbsSpent");
				EventRegister.SendEvent("DREAM ORB COLLECT");
			}
		}
	}

	public void orig_RecieveDeathEvent(float? attackDirection, bool resetDeathEvent = false, bool spellBurn = false, bool isWatery = false)
	{
		if (didFire)
		{
			return;
		}
		didFire = true;
		RecordKillForJournal();
		if (corpse != null)
		{
			EmitCorpse(attackDirection, isWatery, spellBurn);
		}
		if (!isWatery)
		{
			EmitEffects();
		}
		if (doKillFreeze)
		{
			GameManager.instance.FreezeMoment(1);
		}
		if ((enemyDeathType == EnemyDeathTypes.Infected || enemyDeathType == EnemyDeathTypes.LargeInfected || enemyDeathType == EnemyDeathTypes.SmallInfected || enemyDeathType == EnemyDeathTypes.Uninfected) && !BossSceneController.IsBossScene)
		{
			EmitEssence();
		}
		if (audioSnapshotOnDeath != null)
		{
			audioSnapshotOnDeath.TransitionTo(2f);
		}
		if (!string.IsNullOrEmpty(deathBroadcastEvent))
		{
			Debug.LogWarningFormat(this, "Death broadcast event '{0}' not implemented!", deathBroadcastEvent);
		}
		if (!resetDeathEvent)
		{
			PersistentBoolItem component = GetComponent<PersistentBoolItem>();
			if ((bool)component)
			{
				component.SaveState();
			}
			if (recycle)
			{
				PlayMakerFSM playMakerFSM = FSMUtility.LocateFSM(base.gameObject, "health_manager_enemy");
				if (playMakerFSM != null)
				{
					playMakerFSM.FsmVariables.GetFsmBool("Activated").Value = false;
				}
				HealthManager component2 = GetComponent<HealthManager>();
				if (component2 != null)
				{
					component2.SetIsDead(set: false);
				}
				didFire = false;
				base.gameObject.Recycle();
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else
		{
			FSMUtility.SendEventToGameObject(base.gameObject, "CENTIPEDE DEATH");
			didFire = false;
		}
	}

	private void orig_RecordKillForJournal()
	{
		PlayerData playerData = GameManager.instance.playerData;
		string boolName = "killed" + playerDataName;
		string intName = "kills" + playerDataName;
		string boolName2 = "newData" + playerDataName;
		bool flag = false;
		if (!playerData.GetBool(boolName))
		{
			flag = true;
			playerData.SetBool(boolName, value: true);
			playerData.SetBool(boolName2, value: true);
		}
		bool flag2 = false;
		int @int = playerData.GetInt(intName);
		if (@int > 0)
		{
			@int--;
			playerData.SetInt(intName, @int);
			if (@int <= 0)
			{
				flag2 = true;
			}
		}
		if (!playerData.GetBool("hasJournal"))
		{
			return;
		}
		bool flag3 = false;
		if (flag2)
		{
			flag3 = true;
			playerData.SetIntSwappedArgs(playerData.GetInt("journalEntriesCompleted") + 1, "journalEntriesCompleted");
		}
		else if (flag)
		{
			flag3 = true;
			playerData.SetIntSwappedArgs(playerData.GetInt("journalNotesCompleted") + 1, "journalNotesCompleted");
		}
		if (!flag3)
		{
			return;
		}
		if ((bool)journalUpdateMessageSpawned)
		{
			if (journalUpdateMessageSpawned.activeSelf)
			{
				journalUpdateMessageSpawned.SetActive(value: false);
			}
			journalUpdateMessageSpawned.SetActive(value: true);
			PlayMakerFSM playMakerFSM = PlayMakerFSM.FindFsmOnGameObject(journalUpdateMessageSpawned, "Journal Msg");
			if ((bool)playMakerFSM)
			{
				FSMUtility.SetBool(playMakerFSM, "Full", flag2);
				FSMUtility.SetBool(playMakerFSM, "Should Recycle", value: true);
			}
		}
		else
		{
			Debug.LogWarning("Previously spawned Journal Update Msg has been destroyed!", this);
		}
	}
}
