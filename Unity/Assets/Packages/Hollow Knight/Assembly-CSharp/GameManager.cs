// GameManager
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using GlobalEnums;
using InControl;
using Language;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, Platform.IDisengageHandler
{
	public delegate void SavePersistentState();

	public delegate void ResetSemiPersistentState();

	public delegate void DestroyPooledObjects();

	public delegate void UnloadLevel();

	public delegate void RefreshLanguage();

	public delegate void RefreshParticles();

	public delegate void BossLoad();

	public delegate void EnterSceneEvent();

	public delegate void SceneTransitionFinishEvent();

	public enum SceneLoadVisualizations
	{
		Default = 0,
		Custom = -1,
		Dream = 1,
		Colosseum = 2,
		GrimmDream = 3,
		ContinueFromSave = 4,
		GodsAndGlory = 5
	}

	public class SceneLoadInfo
	{
		public bool IsFirstLevelForPlayer;

		public string SceneName;

		public GatePosition? HeroLeaveDirection;

		public string EntryGateName;

		public float EntryDelay;

		public bool PreventCameraFadeOut;

		public bool WaitForSceneTransitionCameraFade;

		public SceneLoadVisualizations Visualization;

		public bool AlwaysUnloadUnusedAssets;

		public bool forceWaitFetch;

		public virtual void NotifyFetchComplete()
		{
		}

		public virtual bool IsReadyToActivate()
		{
			return true;
		}

		public virtual void NotifyFinished()
		{
		}
	}

	public delegate void SceneTransitionBeganDelegate(SceneLoad sceneLoad);

	public enum ReturnToMainMenuSaveModes
	{
		SaveAndCancelOnFail,
		SaveAndContinueOnFail,
		DontSave
	}

	public enum ControllerConnectionStates
	{
		DetachedDevice,
		DummyDevice,
		NullDevice,
		PossiblyConnected,
		ConnectedAndReady
	}

	private bool verboseMode;

	public GameState gameState;

	public bool isPaused;

	private int timeSlowedCount;

	public string sceneName;

	public string nextSceneName;

	public string entryGateName;

	private TransitionPoint callingGate;

	private Vector3 entrySpawnPoint;

	private float entryDelay;

	public float sceneWidth;

	public float sceneHeight;

	public GameConfig gameConfig;

	private GameCameras gameCams;

	[SerializeField]
	private AudioManager audioManager;

	[SerializeField]
	private InControlManager inControlManagerPrefab;

	private static InControlManager _spawnedInControlManager;

	[SerializeField]
	public GameSettings gameSettings;

	public TimeScaleIndependentUpdate timeTool;

	public GameObject gameMap;

	public PlayMakerFSM inventoryFSM;

	[SerializeField]
	public PlayerData playerData;

	[SerializeField]
	public SceneData sceneData;

	public const int NoSaveSlotID = -1;

	public int profileID;

	private bool needsFlush;

	private bool isEmergencyReturningToMenu;

	private float sessionPlayTimer;

	private float sessionStartTime;

	public bool startedOnThisScene = true;

	private bool hazardRespawningHero;

	private string targetScene;

	private bool tilemapDirty;

	private bool needFirstFadeIn;

	private bool waitForManualLevelStart;

	private bool startedSteamEnabled;

	private bool startedGOGEnabled;

	private bool startedLanguageDisabled;

	public AudioMixerSnapshot actorSnapshotUnpaused;

	public AudioMixerSnapshot actorSnapshotPaused;

	public AudioMixerSnapshot silentSnapshot;

	public AudioMixerSnapshot noMusicSnapshot;

	public MusicCue noMusicCue;

	public AudioMixerSnapshot noAtmosSnapshot;

	private bool hasFinishedEnteringScene;

	[SerializeField]
	private WorldInfo worldInfo;

	private bool isLoading;

	private SceneLoadVisualizations loadVisualization;

	private float currentLoadDuration;

	private int sceneLoadsWithoutGarbageCollect;

	private bool isUsingCustomLoadAnimation;

	[SerializeField]
	private StandaloneLoadingSpinner standaloneLoadingSpinnerPrefab;

	public static GameManager _instance;

	private SceneLoad sceneLoad;

	private static readonly string[] SubSceneNameSuffixes = new string[3] { "_boss_defeated", "_boss", "_preload" };

	private int saveIconShowCounter;

	public bool TimeSlowed => timeSlowedCount > 0;

	public InputHandler inputHandler { get; private set; }

	public AchievementHandler achievementHandler { get; private set; }

	public AudioManager AudioManager => audioManager;

	public CameraController cameraCtrl { get; private set; }

	public HeroController hero_ctrl { get; private set; }

	public SpriteRenderer heroLight { get; private set; }

	public SceneManager sm { get; private set; }

	public UIManager ui { get; private set; }

	public tk2dTileMap tilemap { get; private set; }

	public PlayMakerFSM soulOrb_fsm { get; private set; }

	public PlayMakerFSM soulVessel_fsm { get; private set; }

	public float PlayTime => sessionStartTime + sessionPlayTimer;

	public bool RespawningHero { get; set; }

	public bool IsInSceneTransition { get; private set; }

	public bool HasFinishedEnteringScene => hasFinishedEnteringScene;

	public WorldInfo WorldInfo => worldInfo;

	public bool IsLoadingSceneTransition => isLoading;

	public SceneLoadVisualizations LoadVisualization => loadVisualization;

	public float CurrentLoadDuration
	{
		get
		{
			if (!isLoading)
			{
				return 0f;
			}
			return currentLoadDuration;
		}
	}

	public bool IsUsingCustomLoadAnimation => isUsingCustomLoadAnimation;

	public static GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType<GameManager>();
				if (_instance == null)
				{
					Debug.LogError("Couldn't find a Game Manager, make sure one exists in the scene.");
				}
				else if (Application.isPlaying)
				{
					UnityEngine.Object.DontDestroyOnLoad(_instance.gameObject);
				}
			}
			return _instance;
		}
	}

	public static GameManager UnsafeInstance => _instance;

	public event SavePersistentState SavePersistentObjects;

	public event ResetSemiPersistentState ResetSemiPersistentObjects;

	public event DestroyPooledObjects DestroyPersonalPools;

	public event UnloadLevel UnloadingLevel;

	public event RefreshLanguage RefreshLanguageText;

	public event RefreshParticles RefreshParticleLevel;

	public event BossLoad OnLoadedBoss;

	public event EnterSceneEvent OnFinishedEnteringScene;

	public event SceneTransitionFinishEvent OnFinishedSceneTransition;

	public static event SceneTransitionBeganDelegate SceneTransitionBegan;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			UnityEngine.Object.DontDestroyOnLoad(this);
			SetupGameRefs();
		}
		else if (this != _instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			SetupGameRefs();
		}
	}

	private void Start()
	{
		if (this == _instance)
		{
			SetupStatusModifiers();
		}
	}

	protected void Update()
	{
		if (isLoading)
		{
			currentLoadDuration += Time.unscaledDeltaTime;
		}
		else
		{
			currentLoadDuration = 0f;
		}
		IncreaseGameTimer(ref sessionPlayTimer);
		UpdateEngagement();
	}

	private void UpdateEngagement()
	{
		if (gameState == GameState.MAIN_MENU)
		{
			if (!ui.didLeaveEngageMenu)
			{
				if (ui.menuState != MainMenuState.ENGAGE_MENU)
				{
					return;
				}
				if (Platform.Current.EngagementState != Platform.EngagementStates.Engaged)
				{
					Platform.Current.UpdateWaitingForEngagement();
				}
				else if (Platform.Current.IsPlayerPrefsLoaded)
				{
					if (Platform.Current.RequiresPreferencesSyncOnEngage)
					{
						Debug.LogFormat("Syncing preferences...");
						UIManager.instance.LoadStoredSettings();
						global::Language.Language.LoadLanguage();
						InputHandler.Instance.SetActiveGamepadType(InputManager.ActiveDevice);
						MenuStyles.Instance.LoadStyle(force: false, fade: true);
					}
					ui.didLeaveEngageMenu = true;
					if (Platform.Current.IsSavingAllowedByEngagement)
					{
						ui.UIGoToMainMenu();
					}
					else
					{
						ui.UIGoToNoSaveMenu();
					}
				}
			}
			else if (Platform.Current.EngagementState != Platform.EngagementStates.Engaged && inputHandler.acceptingInput && !ui.IsAnimatingMenus && ui.menuState != MainMenuState.ENGAGE_MENU)
			{
				ui.UIGoToEngageMenu();
				ui.slotOne.ClearCache();
				ui.slotTwo.ClearCache();
				ui.slotThree.ClearCache();
				ui.slotFour.ClearCache();
			}
		}
		else if ((gameState == GameState.PLAYING || gameState == GameState.PAUSED) && Platform.Current.EngagementState != Platform.EngagementStates.Engaged && !isEmergencyReturningToMenu)
		{
			Debug.LogFormat("Engagement is not set up right, returning to menu...");
			EmergencyReturnToMenu(delegate
			{
			});
		}
	}

	private void LevelActivated(Scene sceneFrom, Scene sceneTo)
	{
		if (!(this == _instance))
		{
			return;
		}
		if (!waitForManualLevelStart)
		{
			Debug.LogFormat(this, "Performing automatic level start.");
			if (startedOnThisScene && IsGameplayScene())
			{
				tilemapDirty = true;
			}
			SetupSceneRefs(refreshTilemapInfo: true);
			BeginScene();
			OnNextLevelReady();
		}
		else
		{
			Debug.LogFormat(this, "Deferring level start (marked as manual).");
		}
	}

	private void OnDisable()
	{
		UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= LevelActivated;
	}

	private void OnApplicationQuit()
	{
		if (startedLanguageDisabled)
		{
			gameConfig.hideLanguageOption = true;
		}
	}

	public void BeginSceneTransition(SceneLoadInfo info)
	{
		inventoryFSM.SendEvent("INVENTORY CANCEL");
		if (info.IsFirstLevelForPlayer)
		{
			ResetGameTimer();
		}
		if (BossSequenceController.IsInSequence)
		{
			if (BossSequenceController.ForceAssetUnload)
			{
				info.AlwaysUnloadUnusedAssets = true;
				Debug.Log("Asset unload forced by next boss scene.");
			}
			else if (HeroController.instance.IsDreamReturning)
			{
				info.AlwaysUnloadUnusedAssets = true;
				Debug.Log("Asset unload force by death in boss sequence.");
			}
		}
		else if (BossStatueLoadManager.ShouldUnload)
		{
			info.AlwaysUnloadUnusedAssets = true;
			Debug.Log("Asset unload forced by challenging new boss statue");
		}
		StartCoroutine(BeginSceneTransitionRoutine(info));
	}

	private IEnumerator BeginSceneTransitionRoutine(SceneLoadInfo info)
	{
		if (sceneLoad != null)
		{
			Debug.LogErrorFormat(this, "Cannot scene transition to {0}, while a scene transition is in progress", info.SceneName);
			yield break;
		}
		IsInSceneTransition = true;
		sceneLoad = new SceneLoad(this, info.SceneName);
		isLoading = true;
		loadVisualization = info.Visualization;
		if (hero_ctrl != null)
		{
			if (hero_ctrl.cState.superDashing)
			{
				hero_ctrl.exitedSuperDashing = true;
			}
			if (hero_ctrl.cState.spellQuake)
			{
				hero_ctrl.exitedQuake = true;
			}
			hero_ctrl.proxyFSM.SendEvent("HeroCtrl-LeavingScene");
			hero_ctrl.SetHeroParent(null);
		}
		if (!info.IsFirstLevelForPlayer)
		{
			NoLongerFirstGame();
		}
		SaveLevelState();
		SetState(GameState.EXITING_LEVEL);
		entryGateName = info.EntryGateName ?? "";
		targetScene = info.SceneName;
		if (hero_ctrl != null)
		{
			hero_ctrl.LeaveScene(info.HeroLeaveDirection);
		}
		if (!info.PreventCameraFadeOut)
		{
			cameraCtrl.FreezeInPlace(freezeTargetAlso: true);
			cameraCtrl.FadeOut(CameraFadeType.LEVEL_TRANSITION);
		}
		tilemapDirty = true;
		startedOnThisScene = false;
		nextSceneName = info.SceneName;
		waitForManualLevelStart = true;
		if (this.UnloadingLevel != null)
		{
			this.UnloadingLevel();
		}
		string lastSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		sceneLoad.FetchComplete += delegate
		{
			info.NotifyFetchComplete();
		};
		sceneLoad.WillActivate += delegate
		{
			if (this.DestroyPersonalPools != null)
			{
				this.DestroyPersonalPools();
			}
			entryDelay = info.EntryDelay;
		};
		sceneLoad.ActivationComplete += delegate
		{
			UnityEngine.SceneManagement.SceneManager.UnloadScene(lastSceneName);
			RefreshTilemapInfo(info.SceneName);
			sceneLoad.IsUnloadAssetsRequired = info.AlwaysUnloadUnusedAssets || IsUnloadAssetsRequired(lastSceneName, info.SceneName);
			bool flag2 = true;
			if (!sceneLoad.IsUnloadAssetsRequired)
			{
				float? beginTime = sceneLoad.BeginTime;
				if (beginTime.HasValue && Time.realtimeSinceStartup - beginTime.Value > Platform.Current.MaximumLoadDurationForNonCriticalGarbageCollection && sceneLoadsWithoutGarbageCollect < Platform.Current.MaximumSceneTransitionsWithoutNonCriticalGarbageCollection)
				{
					flag2 = false;
				}
			}
			if (flag2)
			{
				sceneLoadsWithoutGarbageCollect = 0;
			}
			else
			{
				sceneLoadsWithoutGarbageCollect++;
			}
			sceneLoad.IsGarbageCollectRequired = flag2;
			Platform.Current.FlushSocialEvents();
		};
		sceneLoad.Complete += delegate
		{
			SetupSceneRefs(refreshTilemapInfo: false);
			BeginScene();
			if (gameMap != null)
			{
				gameMap.GetComponent<GameMap>().LevelReady();
			}
		};
		sceneLoad.Finish += delegate
		{
			sceneLoad = null;
			isLoading = false;
			waitForManualLevelStart = false;
			info.NotifyFinished();
			OnNextLevelReady();
			IsInSceneTransition = false;
			if (this.OnFinishedSceneTransition != null)
			{
				this.OnFinishedSceneTransition();
			}
		};
		if (GameManager.SceneTransitionBegan != null)
		{
			try
			{
				GameManager.SceneTransitionBegan(sceneLoad);
			}
			catch (Exception exception)
			{
				Debug.LogError("Exception in responders to GameManager.SceneTransitionBegan. Attempting to continue load regardless.");
				Debug.LogException(exception);
			}
		}
		sceneLoad.IsFetchAllowed = !info.forceWaitFetch && (Platform.Current.FetchScenesBeforeFade || info.PreventCameraFadeOut);
		sceneLoad.IsActivationAllowed = false;
		sceneLoad.Begin();
		float cameraFadeTimer = 0.5f;
		while (true)
		{
			bool flag = false;
			cameraFadeTimer -= Time.unscaledDeltaTime;
			if (info.WaitForSceneTransitionCameraFade && cameraFadeTimer > 0f)
			{
				flag = true;
			}
			if (!info.IsReadyToActivate())
			{
				flag = true;
			}
			if (!flag)
			{
				break;
			}
			yield return null;
		}
		sceneLoad.IsFetchAllowed = true;
		sceneLoad.IsActivationAllowed = true;
	}

	public IEnumerator TransitionScene(TransitionPoint gate)
	{
		Debug.LogError("TransitionScene(TransitionPoint) is no longer supported");
		callingGate = gate;
		if (hero_ctrl.cState.superDashing)
		{
			hero_ctrl.exitedSuperDashing = true;
		}
		if (hero_ctrl.cState.spellQuake)
		{
			hero_ctrl.exitedQuake = true;
		}
		hero_ctrl.GetComponent<PlayMakerFSM>().SendEvent("HeroCtrl-LeavingScene");
		NoLongerFirstGame();
		SaveLevelState();
		SetState(GameState.EXITING_LEVEL);
		entryGateName = gate.entryPoint;
		targetScene = gate.targetScene;
		hero_ctrl.LeaveScene(gate.GetGatePosition());
		cameraCtrl.FreezeInPlace(freezeTargetAlso: true);
		cameraCtrl.FadeOut(CameraFadeType.LEVEL_TRANSITION);
		hasFinishedEnteringScene = false;
		yield return new WaitForSeconds(0.5f);
		LeftScene(doAdditiveLoad: true);
	}

	public void ChangeToScene(string targetScene, string entryGateName, float pauseBeforeEnter)
	{
		if (hero_ctrl != null)
		{
			hero_ctrl.proxyFSM.SendEvent("HeroCtrl-LeavingScene");
			hero_ctrl.transform.SetParent(null);
		}
		NoLongerFirstGame();
		SaveLevelState();
		SetState(GameState.EXITING_LEVEL);
		this.entryGateName = entryGateName;
		this.targetScene = targetScene;
		entryDelay = pauseBeforeEnter;
		cameraCtrl.FreezeInPlace();
		if (hero_ctrl != null)
		{
			hero_ctrl.ResetState();
		}
		LeftScene();
	}

	public void WarpToDreamGate()
	{
		entryGateName = "dreamGate";
		targetScene = playerData.dreamGateScene;
		entryDelay = 0f;
		cameraCtrl.FreezeInPlace();
		BeginSceneTransition(new SceneLoadInfo
		{
			AlwaysUnloadUnusedAssets = true,
			EntryGateName = "dreamGate",
			PreventCameraFadeOut = true,
			SceneName = playerData.dreamGateScene,
			Visualization = SceneLoadVisualizations.Dream
		});
	}

	public void LeftScene(bool doAdditiveLoad = false)
	{
		UnityEngine.SceneManagement.SceneManager.GetSceneByName(targetScene);
		if (doAdditiveLoad)
		{
			StartCoroutine(LoadSceneAdditive(targetScene));
		}
		else
		{
			LoadScene(targetScene);
		}
	}

	public IEnumerator PlayerDead(float waitTime)
	{
		cameraCtrl.FreezeInPlace(freezeTargetAlso: true);
		NoLongerFirstGame();
		ResetSemiPersistentItems();
		bool finishedSaving = false;
		SaveGame(profileID, delegate
		{
			finishedSaving = true;
		});
		yield return new WaitForSeconds(waitTime);
		cameraCtrl.FadeOut(CameraFadeType.HERO_DEATH);
		yield return new WaitForSeconds(0.8f);
		while (!finishedSaving)
		{
			yield return null;
		}
		if (playerData.permadeathMode == 0)
		{
			ReadyForRespawn(isFirstLevelForPlayer: false);
		}
		else if (playerData.permadeathMode == 2)
		{
			LoadScene("PermaDeath");
		}
	}

	public IEnumerator PlayerDeadFromHazard(float waitTime)
	{
		cameraCtrl.FreezeInPlace(freezeTargetAlso: true);
		NoLongerFirstGame();
		SaveLevelState();
		yield return new WaitForSeconds(waitTime);
		cameraCtrl.FadeOut(CameraFadeType.HERO_HAZARD_DEATH);
		yield return new WaitForSeconds(0.8f);
		PlayMakerFSM.BroadcastEvent("HAZARD RELOAD");
		HazardRespawn();
	}

	public void ReadyForRespawn(bool isFirstLevelForPlayer)
	{
		RespawningHero = true;
		BeginSceneTransition(new SceneLoadInfo
		{
			PreventCameraFadeOut = true,
			WaitForSceneTransitionCameraFade = false,
			EntryGateName = "",
			SceneName = playerData.respawnScene,
			Visualization = (isFirstLevelForPlayer ? SceneLoadVisualizations.ContinueFromSave : SceneLoadVisualizations.Default),
			AlwaysUnloadUnusedAssets = true,
			IsFirstLevelForPlayer = isFirstLevelForPlayer
		});
	}

	public void HazardRespawn()
	{
		hazardRespawningHero = true;
		entryGateName = "";
		cameraCtrl.ResetStartTimer();
		cameraCtrl.camTarget.mode = CameraTarget.TargetMode.FOLLOW_HERO;
		EnterHero();
	}

	public void TimePasses()
	{
		playerData.SetBool("bankerTheftCheck", value: true);
		if (playerData.defeatedDungDefender && !playerData.dungDefenderEncounterReady)
		{
			playerData.dungDefenderEncounterReady = true;
		}
		if (playerData.nailsmithCliff && !playerData.nailsmithKilled)
		{
			playerData.nailsmithSpared = true;
		}
		if (playerData.hasDashSlash && playerData.nailsmithSpared)
		{
			playerData.nailsmithSheo = true;
		}
		if (playerData.brettaRescued && sm.mapZone.ToString() != "TOWN")
		{
			if (UnityEngine.Random.Range(0f, 1f) >= 0.5f)
			{
				playerData.brettaPosition = 0;
			}
			else
			{
				playerData.brettaPosition = 1;
			}
			if (playerData.brettaSeenBench && !playerData.brettaSeenBenchDiary)
			{
				playerData.brettaSeenBenchDiary = true;
			}
			if (playerData.brettaSeenBed && !playerData.brettaSeenBedDiary)
			{
				playerData.brettaSeenBedDiary = true;
			}
		}
		if (playerData.legEaterLeft && playerData.defeatedNightmareGrimm && sm.mapZone.ToString() != "TOWN" && playerData.divineFinalConvo)
		{
			playerData.divineInTown = false;
		}
		if (playerData.zoteSpokenCity)
		{
			playerData.zoteLeftCity = true;
		}
		if (playerData.finalGrubRewardCollected)
		{
			playerData.fatGrubKing = true;
		}
		if (playerData.dungDefenderAwoken)
		{
			playerData.dungDefenderLeft = true;
		}
		if (playerData.scaredFlukeHermitEncountered)
		{
			playerData.scaredFlukeHermitReturned = true;
		}
		if (playerData.xunFlowerGiven)
		{
			playerData.extraFlowerAppear = true;
		}
		if (playerData.nailsmithKilled && playerData.godseekerSpokenAwake)
		{
			playerData.nailsmithCorpseAppeared = true;
		}
	}

	public void FadeSceneIn()
	{
		cameraCtrl.FadeSceneIn();
	}

	public IEnumerator FadeSceneInWithDelay(float delay)
	{
		if (delay >= 0f)
		{
			yield return new WaitForSeconds(delay);
		}
		else
		{
			yield return null;
		}
		FadeSceneIn();
	}

	public bool IsGamePaused()
	{
		if (gameState == GameState.PAUSED)
		{
			return true;
		}
		return false;
	}

	public void SetGameMap(GameObject go_gameMap)
	{
		gameMap = go_gameMap;
	}

	public void CalculateNotchesUsed()
	{
		playerData.CalculateNotchesUsed();
	}

	public string GetLanguageAsString()
	{
		return gameSettings.gameLanguage.ToString();
	}

	public string GetEntryGateName()
	{
		return entryGateName;
	}

	public void SetPlayerDataBool(string boolName, bool value)
	{
		playerData.SetBool(boolName, value);
	}

	public void SetPlayerDataInt(string intName, int value)
	{
		playerData.SetInt(intName, value);
	}

	public void SetPlayerDataFloat(string floatName, float value)
	{
		playerData.SetFloat(floatName, value);
	}

	public void SetPlayerDataString(string stringName, string value)
	{
		playerData.SetString(stringName, value);
	}

	public void IncrementPlayerDataInt(string intName)
	{
		playerData.IncrementInt(intName);
	}

	public void DecrementPlayerDataInt(string intName)
	{
		playerData.DecrementInt(intName);
	}

	public void IntAdd(string intName, int amount)
	{
		playerData.IntAdd(intName, amount);
	}

	public bool GetPlayerDataBool(string boolName)
	{
		return playerData.GetBool(boolName);
	}

	public int GetPlayerDataInt(string intName)
	{
		return playerData.GetInt(intName);
	}

	public float GetPlayerDataFloat(string floatName)
	{
		return playerData.GetFloat(floatName);
	}

	public string GetPlayerDataString(string stringName)
	{
		return playerData.GetString(stringName);
	}

	public void SetPlayerDataVector3(string vectorName, Vector3 value)
	{
		playerData.SetVector3(vectorName, value);
	}

	public Vector3 GetPlayerDataVector3(string vectorName)
	{
		return playerData.GetVector3(vectorName);
	}

	public T GetPlayerDataVariable<T>(string fieldName)
	{
		return playerData.GetVariable<T>(fieldName);
	}

	public void SetPlayerDataVariable<T>(string fieldName, T value)
	{
		playerData.SetVariable(fieldName, value);
	}

	public void EquipCharm(int charmNum)
	{
		playerData.EquipCharm(charmNum);
	}

	public void UnequipCharm(int charmNum)
	{
		playerData.UnequipCharm(charmNum);
	}

	public void RefreshOvercharm()
	{
		if (playerData.charmSlotsFilled > playerData.charmSlots)
		{
			playerData.overcharmed = true;
		}
		else
		{
			playerData.overcharmed = false;
		}
	}

	public void UpdateBlueHealth()
	{
		playerData.UpdateBlueHealth();
	}

	public void SetCurrentMapZoneAsRespawn()
	{
		playerData.mapZone = sm.mapZone;
	}

	public void SetMapZoneToSpecific(string mapZone)
	{
		object obj = Enum.Parse(typeof(MapZone), mapZone);
		if (obj != null)
		{
			playerData.mapZone = (MapZone)obj;
		}
		else
		{
			Debug.LogError("Couldn't convert " + mapZone + " to a MapZone");
		}
	}

	public void StartSoulLimiter()
	{
		playerData.StartSoulLimiter();
	}

	public void EndSoulLimiter()
	{
		playerData.EndSoulLimiter();
	}

	public bool UpdateGameMap()
	{
		return playerData.UpdateGameMap();
	}

	public void CheckAllMaps()
	{
		playerData.CheckAllMaps();
	}

	public void AddToScenesVisited(string scene)
	{
		if (!playerData.scenesVisited.Contains(scene))
		{
			playerData.scenesVisited.Add(scene);
		}
	}

	public bool GetIsSceneVisited(string scene)
	{
		return playerData.scenesVisited.Contains(scene);
	}

	public void AddToBenchList()
	{
		if (!playerData.scenesEncounteredBench.Contains(GetSceneNameString()))
		{
			playerData.scenesEncounteredBench.Add(GetSceneNameString());
		}
	}

	public void AddToGrubList()
	{
		if (!playerData.scenesGrubRescued.Contains(GetSceneNameString()))
		{
			playerData.scenesGrubRescued.Add(GetSceneNameString());
			if (gameMap != null)
			{
				gameMap.GetComponent<GameMap>().SetupMap(pinsOnly: true);
			}
		}
	}

	public void AddToFlameList()
	{
		if (!playerData.scenesFlameCollected.Contains(GetSceneNameString()))
		{
			playerData.scenesFlameCollected.Add(GetSceneNameString());
		}
	}

	public void AddToCocoonList()
	{
		if (!playerData.scenesEncounteredCocoon.Contains(GetSceneNameString()))
		{
			playerData.scenesEncounteredCocoon.Add(GetSceneNameString());
		}
	}

	public void AddToDreamPlantList()
	{
		if (!playerData.scenesEncounteredDreamPlant.Contains(GetSceneNameString()))
		{
			playerData.scenesEncounteredDreamPlant.Add(GetSceneNameString());
		}
	}

	public void AddToDreamPlantCList()
	{
		if (!playerData.scenesEncounteredDreamPlantC.Contains(GetSceneNameString()))
		{
			playerData.scenesEncounteredDreamPlantC.Add(GetSceneNameString());
		}
	}

	public void CountGameCompletion()
	{
		playerData.CountGameCompletion();
	}

	public void CountCharms()
	{
		playerData.CountCharms();
	}

	public void CountJournalEntries()
	{
		playerData.CountJournalEntries();
	}

	public void ActivateTestingCheats()
	{
		playerData.ActivateTestingCheats();
	}

	public void GetAllPowerups()
	{
		playerData.GetAllPowerups();
	}

	public void StoryRecord_death()
	{
	}

	public void StoryRecord_rescueGrub()
	{
	}

	public void StoryRecord_defeatedShade()
	{
	}

	public void StoryRecord_discoveredArea(string areaName)
	{
	}

	public void StoryRecord_travelledToArea(string areaName)
	{
	}

	public void StoryRecord_bankDeposit(int amount)
	{
	}

	public void StoryRecord_bankWithdraw(int amount)
	{
	}

	public void StoryRecord_boughtCorniferMap(string map)
	{
	}

	public void StoryRecord_visited(string visited)
	{
	}

	public void StoryRecord_defeated(string defeated)
	{
	}

	public void StoryRecord_jiji()
	{
	}

	public void StoryRecord_rodeStag(string dest)
	{
	}

	public void StoryRecord_acquired(string item)
	{
	}

	public void StoryRecord_bought(string item)
	{
	}

	public void StoryRecord_quit()
	{
	}

	public void StoryRecord_rest()
	{
	}

	public void StoryRecord_upgradeNail()
	{
	}

	public void StoryRecord_heartPiece()
	{
	}

	public void StoryRecord_maxHealthUp()
	{
	}

	public void StoryRecord_soulPiece()
	{
	}

	public void StoryRecord_maxSoulUp()
	{
	}

	public void StoryRecord_charmsChanged()
	{
	}

	public void StoryRecord_charmEquipped(string charmName)
	{
	}

	public void StoryRecord_start()
	{
	}

	public void AwardAchievement(string key)
	{
		achievementHandler.AwardAchievementToPlayer(key);
	}

	public void QueueAchievement(string key)
	{
		achievementHandler.QueueAchievement(key);
	}

	public void AwardQueuedAchievements()
	{
		achievementHandler.AwardQueuedAchievements();
	}

	public bool IsAchievementAwarded(string key)
	{
		return achievementHandler.AchievementWasAwarded(key);
	}

	public void ClearAllAchievements()
	{
		achievementHandler.ResetAllAchievements();
	}

	public void CheckCharmAchievements()
	{
		CountCharms();
		if (playerData.hasCharm)
		{
			AwardAchievement("CHARMED");
		}
		if (playerData.charmsOwned >= 20)
		{
			AwardAchievement("ENCHANTED");
		}
		if (playerData.salubraBlessing)
		{
			AwardAchievement("BLESSED");
		}
	}

	public void CheckGrubAchievements()
	{
		if (playerData.grubsCollected >= 23)
		{
			AwardAchievement("GRUBFRIEND");
		}
		if (playerData.grubsCollected >= 46)
		{
			AwardAchievement("METAMORPHOSIS");
		}
	}

	public void CheckStagStationAchievements()
	{
		if (playerData.stationsOpened >= 4)
		{
			AwardAchievement("STAG_STATION_HALF");
		}
	}

	public void CheckMapAchievement()
	{
		if (playerData.mapCrossroads && playerData.mapGreenpath && playerData.mapFogCanyon && playerData.mapRoyalGardens && playerData.mapFungalWastes && playerData.mapCity && playerData.mapWaterways && playerData.mapMines && playerData.mapDeepnest && playerData.mapCliffs && playerData.mapOutskirts && playerData.mapRestingGrounds && playerData.mapAbyss)
		{
			AwardAchievement("MAP");
		}
	}

	public void CheckJournalAchievements()
	{
		playerData.CountJournalEntries();
		if (playerData.journalEntriesCompleted >= playerData.journalEntriesTotal)
		{
			AwardAchievement("HUNTER_1");
		}
		if (playerData.killedHunterMark)
		{
			AwardAchievement("HUNTER_2");
		}
	}

	public void CheckAllAchievements()
	{
		if (Platform.Current.IsFiringAchievementsFromSavesAllowed)
		{
			CheckMapAchievement();
			CheckStagStationAchievements();
			CheckGrubAchievements();
			CheckCharmAchievements();
			CheckJournalAchievements();
			if (playerData.MPReserveMax > 0)
			{
				AwardAchievement("SOULFUL");
			}
			if (playerData.MPReserveMax == playerData.MPReserveCap)
			{
				AwardAchievement("WORLDSOUL");
			}
			if (playerData.maxHealthBase > 5)
			{
				AwardAchievement("PROTECTED");
			}
			if (playerData.maxHealthBase == playerData.maxHealthCap)
			{
				AwardAchievement("MASKED");
			}
			if (playerData.dreamOrbs >= 600)
			{
				AwardAchievement("ATTUNEMENT");
			}
			if (playerData.dreamNailUpgraded)
			{
				AwardAchievement("AWAKENING");
			}
			if (playerData.mothDeparted)
			{
				AwardAchievement("ASCENSION");
			}
			if (playerData.hornet1Defeated)
			{
				AwardAchievement("HORNET_1");
			}
			if (playerData.hornetOutskirtsDefeated)
			{
				AwardAchievement("HORNET_2");
			}
			if (playerData.mageLordDefeated)
			{
				AwardAchievement("SOUL_MASTER_DEFEAT");
			}
			if (playerData.mageLordDreamDefeated)
			{
				AwardAchievement("DREAM_SOUL_MASTER_DEFEAT");
			}
			if (playerData.killedInfectedKnight)
			{
				AwardAchievement("BROKEN_VESSEL");
			}
			if (playerData.infectedKnightDreamDefeated)
			{
				AwardAchievement("DREAM_BROKEN_VESSEL");
			}
			if (playerData.killedDungDefender)
			{
				AwardAchievement("DUNG_DEFENDER");
			}
			if (playerData.falseKnightDreamDefeated)
			{
				AwardAchievement("DREAM_FK");
			}
			if (playerData.killedMantisLord)
			{
				AwardAchievement("MANTIS_LORDS");
			}
			if (playerData.killedJarCollector)
			{
				AwardAchievement("COLLECTOR");
			}
			if (playerData.killedTraitorLord)
			{
				AwardAchievement("TRAITOR_LORD");
			}
			if (playerData.killedWhiteDefender)
			{
				AwardAchievement("WHITE_DEFENDER");
			}
			if (playerData.killedGreyPrince)
			{
				AwardAchievement("GREY_PRINCE");
			}
			if (playerData.hegemolDefeated)
			{
				AwardAchievement("BEAST");
			}
			if (playerData.lurienDefeated)
			{
				AwardAchievement("WATCHER");
			}
			if (playerData.monomonDefeated)
			{
				AwardAchievement("TEACHER");
			}
			if (playerData.colosseumBronzeCompleted)
			{
				AwardAchievement("COLOSSEUM_1");
			}
			if (playerData.colosseumSilverCompleted)
			{
				AwardAchievement("COLOSSEUM_2");
			}
			if (playerData.colosseumGoldCompleted)
			{
				AwardAchievement("COLOSSEUM_3");
			}
			if (playerData.killedGrimm)
			{
				AwardAchievement("GRIMM");
			}
			if (playerData.defeatedNightmareGrimm)
			{
				AwardAchievement("NIGHTMARE_GRIMM");
			}
			CheckBanishmentAchievement();
			if (playerData.nailsmithKilled)
			{
				AwardAchievement("NAILSMITH_KILL");
			}
			if (playerData.nailsmithConvoArt)
			{
				AwardAchievement("NAILSMITH_SPARE");
			}
			if (playerData.mothDeparted)
			{
				playerData.hasDreamGate = true;
			}
			if (playerData.hasTramPass)
			{
				AddToScenesVisited("Deepnest_26_b");
			}
			if (playerData.slyRescued)
			{
				AddToScenesVisited("Crossroads_04_b");
			}
			if (playerData.gotCharm_32)
			{
				AddToScenesVisited("Deepnest_East_14a");
			}
			if (playerData.awardAllAchievements)
			{
				achievementHandler.AwardAllAchievements();
			}
		}
	}

	public void CheckBanishmentAchievement()
	{
		if (playerData.destroyedNightmareLantern)
		{
			AwardAchievement("BANISHMENT");
		}
	}

	public void SetStatusRecordInt(string key, int value)
	{
		Platform.Current.EncryptedSharedData.SetInt(key, value);
	}

	public int GetStatusRecordInt(string key)
	{
		return Platform.Current.EncryptedSharedData.GetInt(key, 0);
	}

	public void ResetStatusRecords()
	{
		Platform.Current.EncryptedSharedData.DeleteKey("RecPermadeathMode");
	}

	public void SaveStatusRecords()
	{
		Platform.Current.EncryptedSharedData.Save();
	}

	public void SetState(GameState newState)
	{
		gameState = newState;
	}

	public void LoadScene(string destScene)
	{
		tilemapDirty = true;
		startedOnThisScene = false;
		nextSceneName = destScene;
		if (this.DestroyPersonalPools != null)
		{
			this.DestroyPersonalPools();
		}
		if (this.UnloadingLevel != null)
		{
			this.UnloadingLevel();
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene(destScene);
	}

	public IEnumerator LoadSceneAdditive(string destScene)
	{
		tilemapDirty = true;
		startedOnThisScene = false;
		nextSceneName = destScene;
		waitForManualLevelStart = true;
		if (this.DestroyPersonalPools != null)
		{
			this.DestroyPersonalPools();
		}
		if (this.UnloadingLevel != null)
		{
			this.UnloadingLevel();
		}
		string exitingScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(destScene, LoadSceneMode.Additive);
		asyncOperation.allowSceneActivation = true;
		yield return asyncOperation;
		UnityEngine.SceneManagement.SceneManager.UnloadScene(exitingScene);
		RefreshTilemapInfo(destScene);
		if (IsUnloadAssetsRequired(exitingScene, destScene))
		{
			Debug.LogFormat(this, "Unloading assets due to zone transition");
			yield return Resources.UnloadUnusedAssets();
		}
		GCManager.Collect();
		SetupSceneRefs(refreshTilemapInfo: true);
		BeginScene();
		OnNextLevelReady();
		waitForManualLevelStart = false;
	}

	public void OnNextLevelReady()
	{
		if (IsGameplayScene())
		{
			SetState(GameState.ENTERING_LEVEL);
			playerData.disablePause = false;
			inputHandler.AllowPause();
			inputHandler.StartAcceptingInput();
			EnterHero(additiveGateSearch: true);
			UpdateUIStateFromGameState();
		}
	}

	public void OnWillActivateFirstLevel()
	{
		HeroController.instance.isEnteringFirstLevel = true;
		entryGateName = "top1";
		SetState(GameState.PLAYING);
		ui.ConfigureMenu();
	}

	public IEnumerator LoadFirstScene()
	{
		yield return new WaitForEndOfFrame();
		OnWillActivateFirstLevel();
		LoadScene("Tutorial_01");
	}

	public void LoadPermadeathUnlockScene()
	{
		if (GetStatusRecordInt("RecPermadeathMode") == 0)
		{
			LoadScene("PermaDeath_Unlock");
		}
		else
		{
			StartCoroutine(ReturnToMainMenu(ReturnToMainMenuSaveModes.SaveAndContinueOnFail));
		}
	}

	public void LoadMrMushromScene()
	{
		if (playerData.mrMushroomState >= 8)
		{
			LoadScene("Cinematic_MrMushroom");
		}
		else
		{
			LoadScene("End_Game_Completion");
		}
	}

	public void LoadOpeningCinematic()
	{
		SetState(GameState.CUTSCENE);
		LoadScene("Intro_Cutscene");
	}

	private void PositionHeroAtSceneEntrance()
	{
		Vector2 position = FindEntryPoint(entryGateName, default(Scene)) ?? new Vector2(-20000f, 20000f);
		if (hero_ctrl != null)
		{
			hero_ctrl.transform.SetPosition2D(position);
		}
	}

	private Vector2? FindEntryPoint(string entryPointName, Scene filterScene)
	{
		if (RespawningHero)
		{
			Transform transform = hero_ctrl.LocateSpawnPoint();
			if (transform != null)
			{
				return transform.transform.position;
			}
			return null;
		}
		if (hazardRespawningHero)
		{
			return playerData.hazardRespawnLocation;
		}
		if (entryGateName == "dreamGate")
		{
			return new Vector2(playerData.dreamGateX, playerData.dreamGateY);
		}
		TransitionPoint transitionPoint = FindTransitionPoint(entryPointName, filterScene, fallbackToAnyAvailable: true);
		if (transitionPoint != null)
		{
			return (Vector2)transitionPoint.transform.position + transitionPoint.entryOffset;
		}
		return null;
	}

	private TransitionPoint FindTransitionPoint(string entryPointName, Scene filterScene, bool fallbackToAnyAvailable)
	{
		List<TransitionPoint> transitionPoints = TransitionPoint.TransitionPoints;
		for (int i = 0; i < transitionPoints.Count; i++)
		{
			TransitionPoint transitionPoint = transitionPoints[i];
			if (transitionPoint.name == entryPointName && (!filterScene.IsValid() || transitionPoint.gameObject.scene == filterScene))
			{
				return transitionPoint;
			}
		}
		if (fallbackToAnyAvailable && transitionPoints.Count > 0)
		{
			return transitionPoints[0];
		}
		return null;
	}

	private void EnterHero(bool additiveGateSearch = false)
	{
		if (entryGateName == "door_dreamReturn" && !string.IsNullOrEmpty(playerData.bossReturnEntryGate))
		{
			if (GetCurrentMapZone() == MapZone.GODS_GLORY.ToString())
			{
				entryGateName = playerData.bossReturnEntryGate;
			}
			playerData.bossReturnEntryGate = string.Empty;
		}
		if (RespawningHero)
		{
			if (needFirstFadeIn)
			{
				StartCoroutine(FadeSceneInWithDelay(0.3f));
				needFirstFadeIn = false;
			}
			StartCoroutine(hero_ctrl.Respawn());
			FinishedEnteringScene();
			RespawningHero = false;
		}
		else if (hazardRespawningHero)
		{
			StartCoroutine(hero_ctrl.HazardRespawn());
			FinishedEnteringScene();
			hazardRespawningHero = false;
		}
		else if (entryGateName == "dreamGate")
		{
			hero_ctrl.EnterSceneDreamGate();
		}
		else if (!startedOnThisScene)
		{
			SetState(GameState.ENTERING_LEVEL);
			if (!string.IsNullOrEmpty(entryGateName))
			{
				if (additiveGateSearch)
				{
					if (verboseMode)
					{
						Debug.Log("Searching for entry gate " + entryGateName + " in the next scene: " + nextSceneName);
					}
					GameObject[] rootGameObjects = UnityEngine.SceneManagement.SceneManager.GetSceneByName(nextSceneName).GetRootGameObjects();
					foreach (GameObject gameObject in rootGameObjects)
					{
						TransitionPoint component = gameObject.GetComponent<TransitionPoint>();
						if (component != null && component.name == entryGateName)
						{
							if (verboseMode)
							{
								Debug.Log("SUCCESS - Found as root object");
							}
							StartCoroutine(hero_ctrl.EnterScene(component, entryDelay));
							return;
						}
						if (gameObject.name == "_Transition Gates")
						{
							TransitionPoint[] componentsInChildren = gameObject.GetComponentsInChildren<TransitionPoint>();
							for (int j = 0; j < componentsInChildren.Length; j++)
							{
								if (componentsInChildren[j].name == entryGateName)
								{
									if (verboseMode)
									{
										Debug.Log("SUCCESS - Found in _Transition Gates folder");
									}
									StartCoroutine(hero_ctrl.EnterScene(componentsInChildren[j], entryDelay));
									return;
								}
							}
						}
						TransitionPoint[] componentsInChildren2 = gameObject.GetComponentsInChildren<TransitionPoint>();
						for (int k = 0; k < componentsInChildren2.Length; k++)
						{
							if (componentsInChildren2[k].name == entryGateName)
							{
								if (verboseMode)
								{
									Debug.Log("SUCCESS - Found as a child of a random scene object, can't win em all");
								}
								StartCoroutine(hero_ctrl.EnterScene(componentsInChildren2[k], entryDelay));
								return;
							}
						}
					}
					Debug.LogError("Searching in next scene for TransitionGate failed.");
					return;
				}
				GameObject gameObject2 = GameObject.Find(entryGateName);
				if (gameObject2 != null)
				{
					TransitionPoint component2 = gameObject2.GetComponent<TransitionPoint>();
					StartCoroutine(hero_ctrl.EnterScene(component2, entryDelay));
					return;
				}
				Debug.LogError("No entry point found with the name \"" + entryGateName + "\" in this scene (" + sceneName + "). Unable to move hero into position, trying alternative gates...");
				TransitionPoint[] array = UnityEngine.Object.FindObjectsOfType<TransitionPoint>();
				if (array != null)
				{
					StartCoroutine(hero_ctrl.EnterScene(array[0], entryDelay));
					return;
				}
				Debug.LogError("Could not find any gates in this scene. Trying last ditch spawn...");
				hero_ctrl.transform.SetPosition2D((float)tilemap.width / 2f, (float)tilemap.height / 2f);
				hero_ctrl.EnterSceneDreamGate();
			}
			else
			{
				Debug.LogError("No entry gate has been defined in the Game Manager, unable to move hero into position.");
				FinishedEnteringScene();
			}
		}
		else if (IsGameplayScene())
		{
			FinishedEnteringScene();
			FadeSceneIn();
		}
	}

	public void FinishedEnteringScene()
	{
		SetState(GameState.PLAYING);
		entryDelay = 0f;
		hasFinishedEnteringScene = true;
		if (this.OnFinishedEnteringScene != null)
		{
			this.OnFinishedEnteringScene();
		}
	}

	private void SetupGameRefs()
	{
		playerData = PlayerData.instance;
		sceneData = SceneData.instance;
		gameCams = GameCameras.instance;
		cameraCtrl = gameCams.cameraController;
		gameSettings = new GameSettings();
		inputHandler = GetComponent<InputHandler>();
		achievementHandler = GetComponent<AchievementHandler>();
		if (_spawnedInControlManager == null)
		{
			_spawnedInControlManager = UnityEngine.Object.Instantiate(inControlManagerPrefab);
			UnityEngine.Object.DontDestroyOnLoad(_spawnedInControlManager);
		}
		inventoryFSM = gameCams.gameObject.transform.Find("HudCamera").gameObject.transform.Find("Inventory").gameObject.GetComponent<PlayMakerFSM>();
		if ((bool)AchievementPopupHandler.Instance)
		{
			AchievementPopupHandler.Instance.Setup(achievementHandler);
		}
		Platform.Current.AdjustGraphicsSettings(gameSettings);
		if (inputHandler == null)
		{
			Debug.LogError("Couldn't find InputHandler component.");
		}
		if (achievementHandler == null)
		{
			Debug.LogError("Couldn't find AchievementHandler component.");
		}
		UnityEngine.SceneManagement.SceneManager.activeSceneChanged += LevelActivated;
		Platform.Current.SetDisengageHandler(this);
	}

	public void SetupSceneRefs(bool refreshTilemapInfo)
	{
		UpdateSceneName();
		if (ui == null)
		{
			ui = UIManager.instance;
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("SceneManager");
		if (gameObject != null)
		{
			sm = gameObject.GetComponent<SceneManager>();
		}
		else
		{
			Debug.Log("Scene Manager missing from scene " + sceneName);
		}
		if (IsGameplayScene())
		{
			if (hero_ctrl == null)
			{
				SetupHeroRefs();
			}
			inputHandler.AttachHeroController(hero_ctrl);
			if (refreshTilemapInfo)
			{
				RefreshTilemapInfo(sceneName);
			}
			soulOrb_fsm = gameCams.soulOrbFSM;
			soulVessel_fsm = gameCams.soulVesselFSM;
		}
	}

	public void SetupHeroRefs()
	{
		hero_ctrl = HeroController.instance;
		heroLight = GameObject.FindGameObjectWithTag("HeroLightMain").GetComponent<SpriteRenderer>();
	}

	public void BeginScene()
	{
		inputHandler.SceneInit();
		ui.SceneInit();
		if ((bool)hero_ctrl)
		{
			hero_ctrl.SceneInit();
		}
		gameCams.SceneInit();
		if (IsMenuScene())
		{
			SetState(GameState.MAIN_MENU);
			UpdateUIStateFromGameState();
			Platform.Current.SetSocialPresence("IN_MENU", isActive: true);
		}
		else if (IsGameplayScene())
		{
			if ((!Application.isEditor && !Debug.isDebugBuild) || Time.renderedFrameCount > 3)
			{
				PositionHeroAtSceneEntrance();
			}
			if (sm != null)
			{
				Platform.Current.SetSocialPresence("EXPLORING_" + sm.mapZone, isActive: true);
			}
		}
		else if (IsNonGameplayScene())
		{
			SetState(GameState.CUTSCENE);
			UpdateUIStateFromGameState();
		}
		else
		{
			Debug.LogError("GM - Scene type is not set to a standard scene type.");
			UpdateUIStateFromGameState();
		}
	}

	private void UpdateUIStateFromGameState()
	{
		if (ui != null)
		{
			ui.SetUIStartState(gameState);
			return;
		}
		ui = UnityEngine.Object.FindObjectOfType<UIManager>();
		if (ui != null)
		{
			ui.SetUIStartState(gameState);
		}
		else
		{
			Debug.LogError("GM: Could not find the UI manager in this scene.");
		}
	}

	public void SkipCutscene()
	{
		StartCoroutine(SkipCutsceneNoMash());
	}

	private IEnumerator SkipCutsceneNoMash()
	{
		if (gameState != GameState.CUTSCENE)
		{
			yield break;
		}
		ui.HideCutscenePrompt();
		CinematicPlayer cinematicPlayer = UnityEngine.Object.FindObjectOfType<CinematicPlayer>();
		if (cinematicPlayer != null)
		{
			yield return StartCoroutine(cinematicPlayer.SkipVideo());
			inputHandler.skippingCutscene = false;
			yield break;
		}
		CutsceneHelper cutsceneHelper = UnityEngine.Object.FindObjectOfType<CutsceneHelper>();
		if (cutsceneHelper != null)
		{
			yield return StartCoroutine(cutsceneHelper.SkipCutscene());
			inputHandler.skippingCutscene = false;
			yield break;
		}
		OpeningSequence openingSequence = UnityEngine.Object.FindObjectOfType<OpeningSequence>();
		if (openingSequence != null)
		{
			yield return StartCoroutine(openingSequence.Skip());
			inputHandler.skippingCutscene = false;
			yield break;
		}
		StagTravel stagTravel = UnityEngine.Object.FindObjectOfType<StagTravel>();
		if (stagTravel != null)
		{
			yield return StartCoroutine(stagTravel.Skip());
			inputHandler.skippingCutscene = false;
		}
		else
		{
			Debug.LogError("Unable to skip, please ensure there is a CinematicPlayer or CutsceneHelper in this scene.");
		}
	}

	public void NoLongerFirstGame()
	{
		if (playerData.isFirstGame)
		{
			playerData.isFirstGame = false;
		}
	}

	private void SetupStatusModifiers()
	{
		if (gameConfig.clearRecordsOnStart)
		{
			ResetStatusRecords();
		}
		if (gameConfig.unlockPermadeathMode)
		{
			SetStatusRecordInt("RecPermadeathMode", 1);
		}
		if (gameConfig.unlockBossRushMode)
		{
			SetStatusRecordInt("RecBossRushMode", 1);
		}
		if (gameConfig.clearPreferredLanguageSetting)
		{
			Platform.Current.SharedData.DeleteKey("GameLangSet");
		}
		if (gameSettings.CommandArgumentUsed("-forcelang"))
		{
			Debug.Log("== Language option forced on by command argument.");
			gameConfig.hideLanguageOption = true;
		}
	}

	public void MatchBackerCreditsSetting()
	{
		if (gameSettings.backerCredits > 0)
		{
			playerData.backerCredits = true;
		}
		else
		{
			playerData.backerCredits = false;
		}
	}

	public void RefreshLocalization()
	{
		if (this.RefreshLanguageText != null)
		{
			this.RefreshLanguageText();
		}
	}

	public void RefreshParticleSystems()
	{
		if (this.RefreshParticleLevel != null)
		{
			this.RefreshParticleLevel();
		}
	}

	public void ApplyNativeInput()
	{
	}

	public void EnablePermadeathMode()
	{
		SetStatusRecordInt("RecPermadeathMode", 1);
	}

	public string GetCurrentMapZone()
	{
		return sm.mapZone.ToString();
	}

	public float GetSceneWidth()
	{
		if (IsGameplayScene())
		{
			return sceneWidth;
		}
		return 0f;
	}

	public float GetSceneHeight()
	{
		if (IsGameplayScene())
		{
			return sceneHeight;
		}
		return 0f;
	}

	public GameObject GetSceneManager()
	{
		return sm.gameObject;
	}

	public string GetFormattedMapZoneString(MapZone mapZone)
	{
		return global::Language.Language.Get(mapZone.ToString(), "Map Zones");
	}

	public void UpdateSceneName()
	{
		sceneName = GetBaseSceneName(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
	}

	public static string GetBaseSceneName(string fullSceneName)
	{
		for (int i = 0; i < SubSceneNameSuffixes.Length; i++)
		{
			string text = SubSceneNameSuffixes[i];
			if (fullSceneName.EndsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				return fullSceneName.Substring(0, fullSceneName.Length - text.Length);
			}
		}
		return fullSceneName;
	}

	public string GetSceneNameString()
	{
		UpdateSceneName();
		return sceneName;
	}

	private static tk2dTileMap GetTileMap(GameObject gameObject)
	{
		if (gameObject.CompareTag("TileMap"))
		{
			return gameObject.GetComponent<tk2dTileMap>();
		}
		return null;
	}

	public void RefreshTilemapInfo(string targetScene)
	{
		tk2dTileMap tk2dTileMap2 = null;
		int num = 0;
		while (tk2dTileMap2 == null && num < UnityEngine.SceneManagement.SceneManager.sceneCount)
		{
			Scene sceneAt = UnityEngine.SceneManagement.SceneManager.GetSceneAt(num);
			if (string.IsNullOrEmpty(targetScene) || !(sceneAt.name != targetScene))
			{
				GameObject[] rootGameObjects = sceneAt.GetRootGameObjects();
				int num2 = 0;
				while (tk2dTileMap2 == null && num2 < rootGameObjects.Length)
				{
					tk2dTileMap2 = GetTileMap(rootGameObjects[num2]);
					num2++;
				}
			}
			num++;
		}
		if (tk2dTileMap2 == null)
		{
			Debug.LogErrorFormat("Using fallback 1 to find tilemap. Scene {0} requires manual fixing.", targetScene);
			GameObject[] array = GameObject.FindGameObjectsWithTag("TileMap");
			int num3 = 0;
			while (tk2dTileMap2 == null && num3 < array.Length)
			{
				tk2dTileMap2 = array[num3].GetComponent<tk2dTileMap>();
				num3++;
			}
		}
		if (tk2dTileMap2 == null)
		{
			Debug.LogErrorFormat("Using fallback 2 to find tilemap. Scene {0} requires manual fixing.", targetScene);
			GameObject gameObject = GameObject.Find("TileMap");
			if (gameObject != null)
			{
				tk2dTileMap2 = GetTileMap(gameObject);
			}
		}
		if (tk2dTileMap2 == null)
		{
			Debug.LogErrorFormat("Failed to find tilemap in {0} entirely.", targetScene);
		}
		else
		{
			tilemap = tk2dTileMap2;
			sceneWidth = tilemap.width;
			sceneHeight = tilemap.height;
		}
	}

	public void SaveLevelState()
	{
		if (this.SavePersistentObjects != null)
		{
			this.SavePersistentObjects();
		}
	}

	public void ResetSemiPersistentItems()
	{
		if (this.ResetSemiPersistentObjects != null)
		{
			this.ResetSemiPersistentObjects();
		}
		sceneData.ResetSemiPersistentItems();
	}

	public bool IsMenuScene()
	{
		UpdateSceneName();
		if (sceneName == "Menu_Title")
		{
			return true;
		}
		return false;
	}

	public bool IsTitleScreenScene()
	{
		UpdateSceneName();
		if (string.Compare(sceneName, "Title_Screens", ignoreCase: true) == 0)
		{
			return true;
		}
		return false;
	}

	public bool IsGameplayScene()
	{
		UpdateSceneName();
		if (IsNonGameplayScene())
		{
			return false;
		}
		return true;
	}

	public bool IsNonGameplayScene()
	{
		UpdateSceneName();
		if (IsCinematicScene())
		{
			return true;
		}
		if (sceneName == "Knight Pickup")
		{
			return true;
		}
		if (sceneName == "Pre_Menu_Intro")
		{
			return true;
		}
		if (sceneName == "Menu_Title")
		{
			return true;
		}
		if (sceneName == "End_Credits")
		{
			return true;
		}
		if (sceneName == "Menu_Credits")
		{
			return true;
		}
		if (sceneName == "Cutscene_Boss_Door")
		{
			return true;
		}
		if (sceneName == "PermaDeath_Unlock")
		{
			return true;
		}
		if (sceneName == "GG_Unlock")
		{
			return true;
		}
		if (sceneName == "GG_End_Sequence")
		{
			return true;
		}
		if (sceneName == "End_Game_Completion")
		{
			return true;
		}
		if (sceneName == "BetaEnd")
		{
			return true;
		}
		if (sceneName == "PermaDeath")
		{
			return true;
		}
		if (sceneName == "GG_Entrance_Cutscene")
		{
			return true;
		}
		if (sceneName == "GG_Boss_Door_Entrance")
		{
			return true;
		}
		if (InGameCutsceneInfo.IsInCutscene)
		{
			return true;
		}
		return false;
	}

	public bool IsCinematicScene()
	{
		UpdateSceneName();
		if (sceneName == "Intro_Cutscene_Prologue")
		{
			return true;
		}
		if (sceneName == "Opening_Sequence")
		{
			return true;
		}
		if (sceneName == "Prologue_Excerpt")
		{
			return true;
		}
		if (sceneName == "Intro_Cutscene")
		{
			return true;
		}
		if (sceneName == "Cinematic_Stag_travel")
		{
			return true;
		}
		if (sceneName == "PermaDeath")
		{
			return true;
		}
		if (sceneName == "Cinematic_Ending_A")
		{
			return true;
		}
		if (sceneName == "Cinematic_Ending_B")
		{
			return true;
		}
		if (sceneName == "Cinematic_Ending_C")
		{
			return true;
		}
		if (sceneName == "Cinematic_Ending_D")
		{
			return true;
		}
		if (sceneName == "Cinematic_Ending_E")
		{
			return true;
		}
		if (sceneName == "Cinematic_MrMushroom")
		{
			return true;
		}
		if (sceneName == "BetaEnd")
		{
			return true;
		}
		return false;
	}

	public bool IsStagTravelScene()
	{
		UpdateSceneName();
		if (sceneName == "Cinematic_Stag_travel")
		{
			return true;
		}
		return false;
	}

	public bool IsBetaEndScene()
	{
		UpdateSceneName();
		if (sceneName == "BetaEnd")
		{
			return true;
		}
		return false;
	}

	public bool IsTutorialScene()
	{
		UpdateSceneName();
		if (sceneName == "Tutorial_01")
		{
			return true;
		}
		return false;
	}

	public bool IsTestScene()
	{
		UpdateSceneName();
		if (sceneName.Contains("test"))
		{
			return true;
		}
		return false;
	}

	public bool IsBossDoorScene()
	{
		UpdateSceneName();
		if (sceneName == "Cutscene_Boss_Door")
		{
			return true;
		}
		return false;
	}

	public bool ShouldKeepHUDCameraActive()
	{
		UpdateSceneName();
		if (!(sceneName == "GG_Entrance_Cutscene") && !(sceneName == "GG_Boss_Door_Entrance") && !(sceneName == "GG_End_Sequence") && !(sceneName == "Cinematic_Ending_D"))
		{
			return InGameCutsceneInfo.IsInCutscene;
		}
		return true;
	}

	private static string GetSceneZoneName(string str)
	{
		int num = str.IndexOf('_');
		if (num <= 0)
		{
			return str;
		}
		int num2 = num - 1;
		while (num2 > 0 && char.IsDigit(str[num2]))
		{
			num2--;
		}
		return str.Substring(0, num2 + 1);
	}

	private static int CountBits(int val)
	{
		int num = 0;
		while (val != 0)
		{
			if (((uint)val & (true ? 1u : 0u)) != 0)
			{
				num++;
			}
			val >>= 1;
		}
		return num;
	}

	public bool IsUnloadAssetsRequired(string sourceSceneName, string destinationSceneName)
	{
		WorldInfo.SceneInfo sceneInfo = worldInfo.GetSceneInfo(sourceSceneName);
		WorldInfo.SceneInfo sceneInfo2 = worldInfo.GetSceneInfo(destinationSceneName);
		bool flag = false;
		if (sceneInfo != null && sceneInfo2 != null)
		{
			bool flag2 = (sceneInfo.ZoneTags & 1) == 0;
			bool flag3 = (sceneInfo2.ZoneTags & 1) == 0;
			if (CountBits(sceneInfo2.ZoneTags) == 1 && sceneInfo2.ZoneTags != sceneInfo.ZoneTags && !flag2 && !flag3)
			{
				flag = true;
			}
		}
		if (Application.isEditor)
		{
			if (flag)
			{
				Debug.LogFormat(this, "This transition is a zone transition, which would have a lower memory threshold.");
			}
			return false;
		}
		long memoryUsage = GCManager.GetMemoryUsage();
		if (memoryUsage > 2621440000u)
		{
			Debug.LogFormat(this, "Memory exceeds high memory limit ({0}/{1}).", memoryUsage, 2621440000L);
			return true;
		}
		if (flag && memoryUsage > 2202009600u)
		{
			Debug.LogFormat(this, "Memory exceeds low memory limit ({0}/{1}), and this is a zone transition.", memoryUsage, 2202009600L);
			return true;
		}
		return false;
	}

	public void HasSaveFile(int saveSlot, Action<bool> callback)
	{
		Platform.Current.IsSaveSlotInUse(saveSlot, callback);
	}

	public void SaveGame()
	{
		SaveGame(delegate
		{
		});
	}

	private void ShowSaveIcon()
	{
		UIManager uIManager = UIManager.instance;
		if (uIManager != null && saveIconShowCounter == 0)
		{
			CheckpointSprite checkpointSprite = uIManager.checkpointSprite;
			if (checkpointSprite != null)
			{
				checkpointSprite.Show();
			}
		}
		saveIconShowCounter++;
	}

	private void HideSaveIcon()
	{
		saveIconShowCounter--;
		UIManager uIManager = UIManager.instance;
		if (uIManager != null && saveIconShowCounter == 0)
		{
			CheckpointSprite checkpointSprite = uIManager.checkpointSprite;
			if (checkpointSprite != null)
			{
				checkpointSprite.Hide();
			}
		}
	}

	public void SaveGame(Action<bool> callback)
	{
		SaveGame(profileID, callback);
	}

	private void ResetGameTimer()
	{
		sessionPlayTimer = 0f;
		sessionStartTime = playerData.playTime;
	}

	public void IncreaseGameTimer(ref float timer)
	{
		if ((gameState == GameState.PLAYING || gameState == GameState.ENTERING_LEVEL || gameState == GameState.EXITING_LEVEL) && Time.unscaledDeltaTime < 1f)
		{
			timer += Time.unscaledDeltaTime;
		}
	}

	private void SaveGame(int saveSlot, Action<bool> callback)
	{
		if (saveSlot >= 0)
		{
			SaveLevelState();
			if (!gameConfig.disableSaveGame)
			{
				ShowSaveIcon();
				if (achievementHandler != null)
				{
					achievementHandler.FlushRecordsToDisk();
				}
				else
				{
					Debug.LogError("Error saving achievements (PlayerAchievements is null)");
				}
				if (playerData != null)
				{
					playerData.playTime += sessionPlayTimer;
					ResetGameTimer();
					playerData.version = "1.5.78.11833";
					playerData.profileID = saveSlot;
					playerData.CountGameCompletion();
				}
				else
				{
					Debug.LogError("Error updating PlayerData before save (PlayerData is null)");
				}
				try
				{
					string text = JsonUtility.ToJson(new SaveGameData(playerData, sceneData));
					if (gameConfig.useSaveEncryption && !Platform.Current.IsFileSystemProtected)
					{
						string graph = Encryption.Encrypt(text);
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						MemoryStream memoryStream = new MemoryStream();
						binaryFormatter.Serialize(memoryStream, graph);
						byte[] binary = memoryStream.ToArray();
						memoryStream.Close();
						Platform.Current.WriteSaveSlot(saveSlot, binary, delegate (bool didSave)
						{
							HideSaveIcon();
							callback(didSave);
						});
						return;
					}
					Platform.Current.WriteSaveSlot(saveSlot, Encoding.UTF8.GetBytes(text), delegate (bool didSave)
					{
						HideSaveIcon();
						if (callback != null)
						{
							callback(didSave);
						}
					});
					return;
				}
				catch (Exception ex)
				{
					Debug.LogError("GM Save - There was an error saving the game: " + ex);
					HideSaveIcon();
					if (callback != null)
					{
						CoreLoop.InvokeNext(delegate
						{
							callback(obj: false);
						});
					}
					return;
				}
			}
			Debug.Log("Saving game disabled. No save file written.");
			if (callback != null)
			{
				CoreLoop.InvokeNext(delegate
				{
					callback(obj: false);
				});
			}
			return;
		}
		Debug.LogError("Save game slot not valid: " + saveSlot);
		if (callback != null)
		{
			CoreLoop.InvokeNext(delegate
			{
				callback(obj: false);
			});
		}
	}

	public void LoadGameFromUI(int saveSlot)
	{
		StartCoroutine(LoadGameFromUIRoutine(saveSlot));
	}

	private IEnumerator LoadGameFromUIRoutine(int saveSlot)
	{
		ui.ContinueGame();
		bool finishedLoading = false;
		bool successfullyLoaded = false;
		LoadGame(saveSlot, delegate (bool didLoad)
		{
			finishedLoading = true;
			successfullyLoaded = didLoad;
		});
		while (!finishedLoading)
		{
			yield return null;
		}
		if (successfullyLoaded)
		{
			ContinueGame();
		}
		else
		{
			ui.UIGoToMainMenu();
		}
	}

	public void LoadGame(int saveSlot, Action<bool> callback)
	{
		if (!Platform.IsSaveSlotIndexValid(saveSlot))
		{
			Debug.LogErrorFormat("Cannot load from invalid save slot index {0}", saveSlot);
			if (callback != null)
			{
				CoreLoop.InvokeNext(delegate
				{
					callback(obj: false);
				});
			}
			return;
		}
		Platform.Current.ReadSaveSlot(saveSlot, delegate (byte[] fileBytes)
		{
			bool obj;
			try
			{
				string json;
				if (gameConfig.useSaveEncryption && !Platform.Current.IsFileSystemProtected)
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream serializationStream = new MemoryStream(fileBytes);
					json = Encryption.Decrypt((string)binaryFormatter.Deserialize(serializationStream));
				}
				else
				{
					json = Encoding.UTF8.GetString(fileBytes);
				}
				SaveGameData saveGameData = JsonUtility.FromJson<SaveGameData>(json);
				PlayerData playerData = saveGameData.playerData;
				SceneData sceneData = saveGameData.sceneData;
				PlayerData.instance = playerData;
				this.playerData = playerData;
				SceneData.instance = sceneData;
				this.sceneData = sceneData;
				profileID = saveSlot;
				inputHandler.RefreshPlayerData();
				obj = true;
			}
			catch (Exception ex)
			{
				Debug.LogFormat("Error loading save file for slot {0}: {1}", saveSlot, ex);
				obj = false;
			}
			if (callback != null)
			{
				callback(obj);
			}
		});
	}

	public void ClearSaveFile(int saveSlot, Action<bool> callback)
	{
		if (!Platform.IsSaveSlotIndexValid(saveSlot))
		{
			Debug.LogErrorFormat("Cannot clear invalid save slot index {0}", saveSlot);
			if (callback != null)
			{
				CoreLoop.InvokeNext(delegate
				{
					callback(obj: false);
				});
			}
			return;
		}
		Debug.LogFormat("Save file {0} {1}", saveSlot, "clearing...");
		Platform.Current.ClearSaveSlot(saveSlot, delegate (bool didClear)
		{
			Debug.LogFormat("Save file {0} {1}", saveSlot, didClear ? "cleared" : "failed to clear");
			if (didClear)
			{
				playerData.Reset();
				sceneData.Reset();
			}
			if (callback != null)
			{
				callback(didClear);
			}
		});
	}

	public void GetSaveStatsForSlot(int saveSlot, Action<SaveStats> callback)
	{
		if (!Platform.IsSaveSlotIndexValid(saveSlot))
		{
			Debug.LogErrorFormat("Cannot get save stats for invalid slot {0}", saveSlot);
			if (callback != null)
			{
				CoreLoop.InvokeNext(delegate
				{
					callback(null);
				});
			}
			return;
		}
		SaveStats saveStats;
		Platform.Current.ReadSaveSlot(saveSlot, delegate (byte[] fileBytes)
		{
			if (fileBytes == null)
			{
				if (callback != null)
				{
					CoreLoop.InvokeNext(delegate
					{
						callback(null);
					});
				}
				return;
			}
			try
			{
				string json;
				if (gameConfig.useSaveEncryption && !Platform.Current.IsFileSystemProtected)
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream serializationStream = new MemoryStream(fileBytes);
					json = Encryption.Decrypt((string)binaryFormatter.Deserialize(serializationStream));
				}
				else
				{
					json = Encoding.UTF8.GetString(fileBytes);
				}
				PlayerData playerData = JsonUtility.FromJson<SaveGameData>(json).playerData;
				Debug.LogFormat("pd.maxHealth: {0} pd.maxHealthBase: {1}", playerData.maxHealth, playerData.maxHealthBase);
				saveStats = new SaveStats(playerData.maxHealthBase, playerData.geo, playerData.mapZone, playerData.playTime, playerData.MPReserveMax, playerData.permadeathMode, playerData.bossRushMode, playerData.completionPercentage, playerData.unlockedCompletionRate);
				if (callback != null)
				{
					CoreLoop.InvokeNext(delegate
					{
						callback(saveStats);
					});
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Error while loading save file for slot " + saveSlot + " Exception: " + ex);
				if (callback != null)
				{
					CoreLoop.InvokeNext(delegate
					{
						callback(null);
					});
				}
			}
		});
	}

	public IEnumerator PauseGameToggleByMenu()
	{
		yield return null;
		IEnumerator iterator = PauseGameToggle();
		while (iterator.MoveNext())
		{
			yield return iterator.Current;
		}
	}

	public IEnumerator PauseGameToggle()
	{
		if (TimeSlowed)
		{
			yield break;
		}
		if (!playerData.disablePause && gameState == GameState.PLAYING)
		{
			gameCams.StopCameraShake();
			inputHandler.PreventPause();
			inputHandler.StopUIInput();
			actorSnapshotPaused.TransitionTo(0f);
			isPaused = true;
			SetState(GameState.PAUSED);
			ui.AudioGoToPauseMenu(0.2f);
			ui.SetState(UIState.PAUSED);
			if (HeroController.instance != null)
			{
				HeroController.instance.Pause();
			}
			gameCams.MoveMenuToHUDCamera();
			SetTimeScale(0f);
			yield return new WaitForSecondsRealtime(0.8f);
			inputHandler.AllowPause();
		}
		else if (gameState == GameState.PAUSED)
		{
			gameCams.ResumeCameraShake();
			inputHandler.PreventPause();
			actorSnapshotUnpaused.TransitionTo(0f);
			isPaused = false;
			ui.AudioGoToGameplay(0.2f);
			ui.SetState(UIState.PLAYING);
			SetState(GameState.PLAYING);
			if (HeroController.instance != null)
			{
				HeroController.instance.UnPause();
			}
			MenuButtonList.ClearAllLastSelected();
			SetTimeScale(1f);
			yield return new WaitForSecondsRealtime(0.8f);
			inputHandler.AllowPause();
		}
	}

	private IEnumerator SetTimeScale(float newTimeScale, float duration)
	{
		float lastTimeScale = TimeController.GenericTimeScale;
		for (float timer = 0f; timer < duration; timer += Time.unscaledDeltaTime)
		{
			float t = Mathf.Clamp01(timer / duration);
			SetTimeScale(Mathf.Lerp(lastTimeScale, newTimeScale, t));
			yield return null;
		}
		SetTimeScale(newTimeScale);
	}

	private void SetTimeScale(float newTimeScale)
	{
		if (timeSlowedCount > 1)
		{
			newTimeScale = Mathf.Min(newTimeScale, TimeController.GenericTimeScale);
		}
		TimeController.GenericTimeScale = ((newTimeScale > 0.01f) ? newTimeScale : 0f);
	}

	public void FreezeMoment(int type)
	{
		switch (type)
		{
			case 0:
				StartCoroutine(FreezeMoment(0.01f, 0.35f, 0.1f, 0f));
				break;
			case 1:
				StartCoroutine(FreezeMoment(0.04f, 0.03f, 0.04f, 0f));
				break;
			case 2:
				StartCoroutine(FreezeMoment(0.25f, 2f, 0.25f, 0.15f));
				break;
			case 3:
				StartCoroutine(FreezeMoment(0.01f, 0.25f, 0.1f, 0f));
				break;
		}
		if (type == 4)
		{
			StartCoroutine(FreezeMoment(0.01f, 0.25f, 0.1f, 0f));
		}
		if (type == 5)
		{
			StartCoroutine(FreezeMoment(0.01f, 0.25f, 0.1f, 0f));
		}
	}

	public IEnumerator FreezeMoment(float rampDownTime, float waitTime, float rampUpTime, float targetSpeed)
	{
		timeSlowedCount++;
		yield return StartCoroutine(SetTimeScale(targetSpeed, rampDownTime));
		for (float timer = 0f; timer < waitTime; timer += Time.unscaledDeltaTime)
		{
			yield return null;
		}
		yield return StartCoroutine(SetTimeScale(1f, rampUpTime));
		timeSlowedCount--;
	}

	public IEnumerator FreezeMomentGC(float rampDownTime, float waitTime, float rampUpTime, float targetSpeed)
	{
		timeSlowedCount++;
		yield return StartCoroutine(SetTimeScale(targetSpeed, rampDownTime));
		for (float timer = 0f; timer < waitTime; timer += Time.unscaledDeltaTime)
		{
			yield return null;
		}
		GCManager.Collect();
		yield return StartCoroutine(SetTimeScale(1f, rampUpTime));
		timeSlowedCount--;
	}

	public IEnumerator FreezeMoment(float rampDownTime, float waitTime, float rampUpTime, bool runGc = false)
	{
		timeSlowedCount++;
		yield return StartCoroutine(SetTimeScale(0f, rampDownTime));
		for (float timer = 0f; timer < waitTime; timer += Time.unscaledDeltaTime)
		{
			yield return null;
		}
		yield return StartCoroutine(SetTimeScale(1f, rampUpTime));
		timeSlowedCount--;
	}

	public void EnsureSaveSlotSpace(Action<bool> callback)
	{
		Platform.Current.EnsureSaveSlotSpace(profileID, callback);
	}

	public void StartNewGame(bool permadeathMode = false, bool bossRushMode = false)
	{
		if (permadeathMode)
		{
			playerData.permadeathMode = 1;
		}
		else
		{
			playerData.permadeathMode = 0;
		}
		MatchBackerCreditsSetting();
		if (bossRushMode)
		{
			playerData.AddGGPlayerDataOverrides();
			StartCoroutine(RunContinueGame());
		}
		else
		{
			StartCoroutine(RunStartNewGame());
		}
	}

	public IEnumerator RunStartNewGame()
	{
		cameraCtrl.FadeOut(CameraFadeType.START_FADE);
		noMusicSnapshot.TransitionTo(2f);
		noAtmosSnapshot.TransitionTo(2f);
		yield return new WaitForSeconds(2.6f);
		ui.MakeMenuLean();
		BeginSceneTransition(new SceneLoadInfo
		{
			AlwaysUnloadUnusedAssets = true,
			IsFirstLevelForPlayer = true,
			PreventCameraFadeOut = true,
			WaitForSceneTransitionCameraFade = false,
			SceneName = "Opening_Sequence",
			Visualization = SceneLoadVisualizations.Custom
		});
	}

	public void ContinueGame()
	{
		MatchBackerCreditsSetting();
		StartCoroutine(RunContinueGame());
	}

	public IEnumerator RunContinueGame()
	{
		cameraCtrl.FadeOut(CameraFadeType.START_FADE);
		noMusicSnapshot.TransitionTo(2f);
		noAtmosSnapshot.TransitionTo(2f);
		yield return new WaitForSeconds(2.6f);
		audioManager.ApplyMusicCue(noMusicCue, 0f, 0f, applySnapshot: false);
		ui.MakeMenuLean();
		isLoading = true;
		SetState(GameState.LOADING);
		loadVisualization = SceneLoadVisualizations.Default;
		SaveDataUpgradeHandler.UpgradeSaveData(ref playerData);
		yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Knight_Pickup", LoadSceneMode.Additive);
		SetupSceneRefs(refreshTilemapInfo: false);
		yield return null;
		UnityEngine.SceneManagement.SceneManager.UnloadScene("Knight_Pickup");
		needFirstFadeIn = true;
		isLoading = false;
		ReadyForRespawn(isFirstLevelForPlayer: true);
	}

	public IEnumerator ReturnToMainMenu(ReturnToMainMenuSaveModes saveMode, Action<bool> callback = null)
	{
		if (BossSequenceController.IsInSequence)
		{
			BossSequenceController.RestoreBindings();
		}
		StoryRecord_quit();
		TimePasses();
		if (saveMode != ReturnToMainMenuSaveModes.DontSave)
		{
			bool? saveComplete = null;
			SaveGame(profileID, delegate (bool didSave)
			{
				saveComplete = didSave;
			});
			while (!saveComplete.HasValue)
			{
				yield return null;
			}
			callback?.Invoke(saveComplete.Value);
			if (saveMode == ReturnToMainMenuSaveModes.SaveAndCancelOnFail && !saveComplete.Value)
			{
				yield break;
			}
		}
		else
		{
			callback?.Invoke(obj: false);
		}
		cameraCtrl.FreezeInPlace(freezeTargetAlso: true);
		cameraCtrl.FadeOut(CameraFadeType.JUST_FADE);
		noMusicSnapshot.TransitionTo(1.5f);
		noAtmosSnapshot.TransitionTo(1.5f);
		for (float timer = 0f; timer < 2f; timer += Time.unscaledDeltaTime)
		{
			yield return null;
		}
		StandaloneLoadingSpinner standaloneLoadingSpinner = UnityEngine.Object.Instantiate(standaloneLoadingSpinnerPrefab);
		standaloneLoadingSpinner.Setup(this);
		UnityEngine.Object.DontDestroyOnLoad(standaloneLoadingSpinner.gameObject);
		if (this.UnloadingLevel != null)
		{
			try
			{
				this.UnloadingLevel();
			}
			catch (Exception exception)
			{
				Debug.LogErrorFormat("Error while UnloadingLevel in QuitToMenu, attempting to continue regardless.");
				Debug.LogException(exception);
			}
		}
		if (this.DestroyPersonalPools != null)
		{
			try
			{
				this.DestroyPersonalPools();
			}
			catch (Exception exception2)
			{
				Debug.LogErrorFormat("Error while DestroyingPersonalPools in QuitToMenu, attempting to continue regardless.");
				Debug.LogException(exception2);
			}
		}
		PlayMakerFSM.BroadcastEvent("QUIT TO MENU");
		waitForManualLevelStart = true;
		StaticVariableList.Clear();
		yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Quit_To_Menu", LoadSceneMode.Single);
	}

	public void WillTerminateInBackground()
	{
		if (gameState == GameState.PLAYING || gameState == GameState.PAUSED)
		{
			Debug.LogFormat("Saving in background, because we're about to terminate.");
			SaveGame();
		}
	}

	void Platform.IDisengageHandler.OnDisengage(Action next)
	{
		if (gameState == GameState.PLAYING || gameState == GameState.PAUSED)
		{
			EmergencyReturnToMenu(delegate
			{
				next();
			});
		}
		else
		{
			next();
		}
	}

	public ControllerConnectionStates GetControllerConnectionState()
	{
		if (inputHandler == null)
		{
			return ControllerConnectionStates.PossiblyConnected;
		}
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (activeDevice == null)
		{
			return ControllerConnectionStates.NullDevice;
		}
		if (activeDevice == InputDevice.Null)
		{
			return ControllerConnectionStates.DummyDevice;
		}
		if (!activeDevice.IsAttached)
		{
			return ControllerConnectionStates.DetachedDevice;
		}
		return ControllerConnectionStates.ConnectedAndReady;
	}

	private void EmergencyReturnToMenu(Action callback)
	{
		isEmergencyReturningToMenu = true;
		Debug.LogFormat("Performing emergency return to menu...");
		if (BossSequenceController.IsInSequence)
		{
			BossSequenceController.RestoreBindings();
		}
		if (callback != null)
		{
			CoreLoop.InvokeNext(callback);
		}
		inputHandler.StopUIInput();
		cameraCtrl.FreezeInPlace(freezeTargetAlso: true);
		noMusicSnapshot.TransitionTo(0f);
		noAtmosSnapshot.TransitionTo(0f);
		StandaloneLoadingSpinner standaloneLoadingSpinner = UnityEngine.Object.Instantiate(standaloneLoadingSpinnerPrefab);
		standaloneLoadingSpinner.Setup(this);
		UnityEngine.Object.DontDestroyOnLoad(standaloneLoadingSpinner.gameObject);
		if (this.UnloadingLevel != null)
		{
			try
			{
				this.UnloadingLevel();
			}
			catch (Exception exception)
			{
				Debug.LogErrorFormat("Error while UnloadingLevel in QuitToMenu, attempting to continue regardless.");
				Debug.LogException(exception);
			}
		}
		if (this.DestroyPersonalPools != null)
		{
			try
			{
				this.DestroyPersonalPools();
			}
			catch (Exception exception2)
			{
				Debug.LogErrorFormat("Error while DestroyingPersonalPools in QuitToMenu, attempting to continue regardless.");
				Debug.LogException(exception2);
			}
		}
		PlayMakerFSM.BroadcastEvent("QUIT TO MENU");
		waitForManualLevelStart = true;
		UnityEngine.SceneManagement.SceneManager.LoadScene("Quit_To_Menu", LoadSceneMode.Single);
	}

	public IEnumerator QuitGame()
	{
		StoryRecord_quit();
		FSMUtility.SendEventToGameObject(GameObject.Find("Quit Blanker"), "START FADE");
		yield return new WaitForSeconds(0.5f);
		Application.Quit();
	}

	public void LoadedBoss()
	{
		if (this.OnLoadedBoss != null)
		{
			this.OnLoadedBoss();
		}
	}

	public void DoDestroyPersonalPools()
	{
		if (this.DestroyPersonalPools != null)
		{
			this.DestroyPersonalPools();
		}
	}

	public float GetImplicitCinematicVolume()
	{
		return Mathf.Clamp01(gameSettings.masterVolume / 10f) * Mathf.Clamp01(gameSettings.soundVolume / 10f);
	}
}
