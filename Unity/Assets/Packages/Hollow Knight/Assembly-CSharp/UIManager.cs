using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using GlobalEnums;
using InControl;
using Language;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private GameManager gm;

	private GameSettings gs;

	private HeroController hero_ctrl;

	private PlayerData playerData;

	private InputHandler ih;

	public MenuAudioController uiAudioPlayer;

	public HollowKnightInputModule inputModule;

	[Space]
	public float MENU_FADE_SPEED = 3.2f;

	private const float MENU_FADE_DELAY = 0.1f;

	private const float MENU_MODAL_DIMMER_ALPHA = 0.8f;

	private const float MENU_FADE_ALPHA_TOLERANCE = 0.05f;

	private const float MENU_FADE_FAILSAFE = 2f;

	[Header("State")]
	[Space(6f)]
	public UIState uiState;

	public MainMenuState menuState;

	[Header("Event System")]
	[Space(6f)]
	public EventSystem eventSystem;

	[Header("Main Elements")]
	[Space(6f)]
	public Canvas UICanvas;

	public CanvasGroup modalDimmer;

	public CanvasScaler canvasScaler;

	[Header("Menu Audio")]
	[Space(6f)]
	public AudioMixerSnapshot gameplaySnapshot;

	public AudioMixerSnapshot menuSilenceSnapshot;

	public AudioMixerSnapshot menuPauseSnapshot;

	[Header("Main Menu")]
	[Space(6f)]
	public CanvasGroup mainMenuScreen;

	public MainMenuOptions mainMenuButtons;

	public SpriteRenderer gameTitle;

	public PlayMakerFSM subtitleFSM;

	[Header("Save Profile Menu")]
	[Space(6f)]
	public CanvasGroup saveProfileScreen;

	public CanvasGroup saveProfileTitle;

	public CanvasGroup saveProfileControls;

	public Animator saveProfileTopFleur;

	public PreselectOption saveSlots;

	public SaveSlotButton slotOne;

	public SaveSlotButton slotTwo;

	public SaveSlotButton slotThree;

	public SaveSlotButton slotFour;

	public CheckpointSprite checkpointSprite;

	[Header("Options Menu")]
	[Space(6f)]
	public MenuScreen optionsMenuScreen;

	[Header("Game Options Menu")]
	[Space(6f)]
	public MenuScreen gameOptionsMenuScreen;

	public GameMenuOptions gameMenuOptions;

	public MenuLanguageSetting languageSetting;

	public MenuSetting backerCreditsSetting;

	public MenuSetting nativeAchievementsSetting;

	public MenuSetting controllerRumbleSetting;

	public MenuSetting nativeInputSetting;

	[Header("Audio Menu")]
	[Space(6f)]
	public MenuScreen audioMenuScreen;

	public MenuAudioSlider masterSlider;

	public MenuAudioSlider musicSlider;

	public MenuAudioSlider soundSlider;

	[Header("Video Menu")]
	[Space(6f)]
	public MenuScreen videoMenuScreen;

	public VideoMenuOptions videoMenuOptions;

	public MenuResolutionSetting resolutionOption;

	public ResolutionCountdownTimer countdownTimer;

	public MenuSetting fullscreenOption;

	public MenuSetting vsyncOption;

	public MenuSetting particlesOption;

	public MenuSetting shadersOption;

	public MenuDisplaySetting displayOption;

	public MenuSetting frameCapOption;

	[Header("Controller Options Menu")]
	[Space(6f)]
	public MenuScreen gamepadMenuScreen;

	public ControllerDetect controllerDetect;

	[Header("Controller Remap Menu")]
	[Space(6f)]
	public MenuScreen remapGamepadMenuScreen;

	[Header("Keyboard Options Menu")]
	[Space(6f)]
	public MenuScreen keyboardMenuScreen;

	[Header("Overscan Setting Menu")]
	[Space(6f)]
	public MenuScreen overscanMenuScreen;

	public OverscanSetting overscanSetting;

	[Header("Brightness Setting Menu")]
	[Space(6f)]
	public MenuScreen brightnessMenuScreen;

	public BrightnessSetting brightnessSetting;

	[Header("Achievements Menu")]
	[Space(6f)]
	public MenuScreen achievementsMenuScreen;

	public RectTransform achievementListRect;

	public MenuAchievementsList menuAchievementsList;

	public Sprite hiddenIcon;

	public GameObject achievementsPopupPanel;

	[Header("Extras Menu")]
	[Space(6f)]
	public MenuScreen extrasMenuScreen;

	public MenuScreen extrasContentMenuScreen;

	[Header("Play Mode Menu")]
	[Space(6f)]
	public MenuScreen playModeMenuScreen;

	[Header("Pause Menu")]
	[Space(6f)]
	public Animator pauseMenuAnimator;

	public MenuScreen pauseMenuScreen;

	[Header("Engage Menu")]
	[Space(6f)]
	public MenuScreen engageMenuScreen;

	public bool didLeaveEngageMenu;

	public MenuScreen noSaveMenuScreen;

	[Header("Prompts")]
	[Space(6f)]
	public MenuScreen quitGamePrompt;

	public MenuScreen returnMainMenuPrompt;

	public MenuScreen resolutionPrompt;

	[Header("Cinematics")]
	[SerializeField]
	private CinematicSkipPopup cinematicSkipPopup;

	[Header("Button Skins")]
	[Space(6f)]
	public UIButtonSkins uiButtonSkins;

	private bool clearSaveFile;

	private Selectable lastSelected;

	private bool lastSubmitWasMouse;

	private MenuScreen activePrompt;

	private bool ignoreUnpause;

	private bool isFadingMenu;

	private float startMenuTime;

	private const float startMenuDelay = 0.5f;

	private Coroutine togglePauseCo;

	private Coroutine goToPauseMenuCo;

	private Coroutine leavePauseMenuCo;

	private GraphicRaycaster graphicRaycaster;

	private int menuAnimationCounter;

	private static UIManager _instance;

	private bool permaDeath;

	private bool bossRush;

	private static Action _editMenus;

	private bool hasCalledEditMenus;

	public bool IsFadingMenu
	{
		get
		{
			if (!isFadingMenu)
			{
				return Time.time < startMenuTime;
			}
			return true;
		}
	}

	public bool IsAnimatingMenus => menuAnimationCounter > 0;

	public static UIManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType<UIManager>();
				if (_instance == null)
				{
					return null;
				}
				if (Application.isPlaying)
				{
					UnityEngine.Object.DontDestroyOnLoad(_instance.gameObject);
				}
			}
			return _instance;
		}
	}

	public MenuScreen currentDynamicMenu { get; set; }

	public static event Action EditMenus
	{
		add
		{
			_editMenus = (Action)Delegate.Combine(_editMenus, value);
			if (_instance != null && _instance.hasCalledEditMenus)
			{
				value();
			}
		}
		remove
		{
			_editMenus = (Action)Delegate.Remove(_editMenus, value);
		}
	}

	public static event Action BeforeHideDynamicMenu;

	private void Awake()
	{
		orig_Awake();
		if (!(_instance != this))
		{
			_editMenus?.Invoke();
			hasCalledEditMenus = true;
		}
	}

	public void SceneInit()
	{
		if (this == _instance)
		{
			SetupRefs();
		}
	}

	private void Start()
	{
		orig_Start();
		if (!(_instance != this))
		{
			GameObject gameObject = base.transform.Find("UICanvas/MainMenuScreen/TeamCherryLogo/Hidden_Dreams_Logo").gameObject;
			GameObject obj = UnityEngine.Object.Instantiate(gameObject, gameObject.transform.parent);
			obj.SetActive(value: true);
			Vector3 position = obj.transform.position;
			obj.transform.position = position - new Vector3(0.4f, 0.1f, 0f);
			gameObject.transform.position = position + new Vector3(0.6f, 0f, 0f);
			obj.transform.localScale *= 0.1f;
			obj.GetComponent<SpriteRenderer>().sprite = LoadImage();
		}
	}

	public void SetState(UIState newState)
	{
		if (gm == null)
		{
			gm = GameManager.instance;
		}
		if (newState != uiState)
		{
			if (uiState == UIState.PAUSED && newState == UIState.PLAYING)
			{
				UIClosePauseMenu();
			}
			else if (uiState == UIState.PLAYING && newState == UIState.PAUSED)
			{
				UIGoToPauseMenu();
			}
			else
			{
				switch (newState)
				{
				case UIState.INACTIVE:
					DisableScreens();
					break;
				case UIState.MAIN_MENU_HOME:
					if (Platform.Current.EngagementState == Platform.EngagementStates.Engaged)
					{
						didLeaveEngageMenu = true;
						UIGoToMainMenu();
					}
					else
					{
						UIGoToEngageMenu();
					}
					break;
				case UIState.LOADING:
					DisableScreens();
					break;
				case UIState.PLAYING:
					DisableScreens();
					break;
				case UIState.CUTSCENE:
					DisableScreens();
					break;
				}
			}
			uiState = newState;
		}
		else if (newState == UIState.MAIN_MENU_HOME)
		{
			UIGoToMainMenu();
		}
	}

	private void SetMenuState(MainMenuState newState)
	{
		menuState = newState;
	}

	private void SetupRefs()
	{
		gm = GameManager.instance;
		gs = gm.gameSettings;
		playerData = PlayerData.instance;
		ih = gm.inputHandler;
		if (gm.IsGameplayScene())
		{
			hero_ctrl = HeroController.instance;
		}
		if (gm.IsMenuScene() && gameTitle == null)
		{
			gameTitle = GameObject.Find("LogoTitle").GetComponent<SpriteRenderer>();
		}
		if (UICanvas.worldCamera == null)
		{
			UICanvas.worldCamera = GameCameras.instance.mainCamera;
		}
	}

	public void SetUIStartState(GameState gameState)
	{
		switch (gameState)
		{
		case GameState.MAIN_MENU:
			SetState(UIState.MAIN_MENU_HOME);
			break;
		case GameState.LOADING:
			SetState(UIState.LOADING);
			break;
		case GameState.ENTERING_LEVEL:
			SetState(UIState.PLAYING);
			break;
		case GameState.PLAYING:
			SetState(UIState.PLAYING);
			break;
		case GameState.CUTSCENE:
			SetState(UIState.CUTSCENE);
			break;
		}
	}

	public IEnumerator ShowMainMenuHome()
	{
		ih.StopUIInput();
		SetMenuState(MainMenuState.MAIN_MENU);
		mainMenuScreen.alpha = 0f;
		ShowCanvas(UICanvas);
		mainMenuScreen.gameObject.SetActive(value: true);
		while (mainMenuScreen.alpha < 1f)
		{
			mainMenuScreen.alpha += Time.unscaledDeltaTime * MENU_FADE_SPEED;
			yield return null;
		}
		mainMenuScreen.alpha = 1f;
		mainMenuScreen.interactable = true;
		ih.StartUIInput();
		yield return null;
		mainMenuButtons.HighlightDefault();
	}

	private Coroutine StartMenuAnimationCoroutine(IEnumerator routine)
	{
		return StartCoroutine(StartMenuAnimationCoroutineWorker(routine));
	}

	private IEnumerator StartMenuAnimationCoroutineWorker(IEnumerator routine)
	{
		menuAnimationCounter++;
		yield return StartCoroutine(routine);
		menuAnimationCounter--;
	}

	public void UIGoToOptionsMenu()
	{
		StartMenuAnimationCoroutine(GoToOptionsMenu());
	}

	public void UILeaveOptionsMenu()
	{
		StartMenuAnimationCoroutine(LeaveOptionsMenu());
	}

	public void UIExplicitSwitchUser()
	{
		slotOne.ClearCache();
		slotTwo.ClearCache();
		slotThree.ClearCache();
		slotFour.ClearCache();
		UIGoToEngageMenu();
	}

	public void UIGoToEngageMenu()
	{
		StartMenuAnimationCoroutine(GoToEngageMenu());
	}

	public void UIGoToNoSaveMenu()
	{
		StartMenuAnimationCoroutine(GoToNoSaveMenu());
	}

	public void UIGoToMainMenu()
	{
		StartMenuAnimationCoroutine(GoToMainMenu());
	}

	public void UIGoToProfileMenu()
	{
		StartMenuAnimationCoroutine(GoToProfileMenu());
	}

	public void UIReturnToProfileMenu()
	{
		if (Platform.Current.IsSavingAllowedByEngagement)
		{
			UIGoToProfileMenu();
		}
		else
		{
			UIGoToMainMenu();
		}
	}

	public void UIMainStartGame()
	{
		if (Platform.Current.IsSavingAllowedByEngagement)
		{
			UIGoToProfileMenu();
			return;
		}
		gm.profileID = -1;
		if (gm.GetStatusRecordInt("RecPermadeathMode") == 1 || gm.GetStatusRecordInt("RecBossRushMode") == 1)
		{
			UIGoToPlayModeMenu();
		}
		else
		{
			StartNewGame();
		}
	}

	public void UIGoToControllerMenu()
	{
		StartMenuAnimationCoroutine(GoToControllerMenu());
	}

	public void UIGoToRemapControllerMenu()
	{
		StartMenuAnimationCoroutine(GoToRemapControllerMenu());
	}

	public void UIGoToKeyboardMenu()
	{
		StartMenuAnimationCoroutine(GoToKeyboardMenu());
	}

	public void UIGoToAudioMenu()
	{
		StartMenuAnimationCoroutine(GoToAudioMenu());
	}

	public void UIGoToVideoMenu(bool rollbackRes = false)
	{
		StartMenuAnimationCoroutine(GoToVideoMenu(rollbackRes));
	}

	public void UIGoToPauseMenu()
	{
		goToPauseMenuCo = StartMenuAnimationCoroutine(GoToPauseMenu());
	}

	public void UIClosePauseMenu()
	{
		ih.StopUIInput();
		StartCoroutine(HideCurrentMenu());
		StartMenuAnimationCoroutine(FadeOutCanvasGroup(modalDimmer));
	}

	public void UIClearPauseMenu()
	{
		pauseMenuAnimator.SetBool("clear", value: true);
	}

	public void UnClearPauseMenu()
	{
		pauseMenuAnimator.SetBool("clear", value: false);
	}

	public void UIGoToOverscanMenu()
	{
		StartMenuAnimationCoroutine(GoToOverscanMenu());
	}

	public void UIGoToBrightnessMenu()
	{
		StartMenuAnimationCoroutine(GoToBrightnessMenu());
	}

	public void UIGoToGameOptionsMenu()
	{
		StartMenuAnimationCoroutine(GoToGameOptionsMenu());
	}

	public void UIGoToAchievementsMenu()
	{
		StartMenuAnimationCoroutine(GoToAchievementsMenu());
	}

	public void UIGoToExtrasMenu()
	{
		StartMenuAnimationCoroutine(GoToExtrasMenu());
	}

	public void UIGoToExtrasContentMenu()
	{
		StartMenuAnimationCoroutine(GoToExtrasContentMenu());
	}

	public void UIShowQuitGamePrompt()
	{
		StartMenuAnimationCoroutine(GoToQuitGamePrompt());
	}

	public void UIShowReturnMenuPrompt()
	{
		StartMenuAnimationCoroutine(GoToReturnMenuPrompt());
	}

	public void UIShowResolutionPrompt(bool startTimer = false)
	{
		StartMenuAnimationCoroutine(GoToResolutionPrompt(startTimer));
	}

	public void UILeaveExitToMenuPrompt()
	{
		StartMenuAnimationCoroutine(LeaveExitToMenuPrompt());
	}

	public void UIGoToPlayModeMenu()
	{
		StartMenuAnimationCoroutine(GoToPlayModeMenu());
	}

	public void UIReturnToMainMenu()
	{
		StartMenuAnimationCoroutine(ReturnToMainMenu());
	}

	public void UIGoToMenuCredits()
	{
		StartMenuAnimationCoroutine(GoToMenuCredits());
	}

	public void UIStartNewGame()
	{
		StartNewGame();
	}

	public void UIStartNewGameContinue()
	{
		StartNewGame(permaDeath, bossRush);
	}

	public void StartNewGame(bool permaDeath = false, bool bossRush = false)
	{
		this.permaDeath = permaDeath;
		this.bossRush = bossRush;
		ih.StopUIInput();
		if (gs.overscanAdjusted == 1 && gs.brightnessAdjusted == 1)
		{
			uiAudioPlayer.PlayStartGame();
			gm.EnsureSaveSlotSpace(delegate(bool hasSpace)
			{
				if (hasSpace)
				{
					if (menuState == MainMenuState.SAVE_PROFILES)
					{
						StartCoroutine(HideSaveProfileMenu());
					}
					else
					{
						StartCoroutine(HideCurrentMenu());
					}
					uiAudioPlayer.PlayStartGame();
					gm.StartNewGame(permaDeath, bossRush);
				}
				else
				{
					ih.StartUIInput();
					(gm.profileID switch
					{
						2 => slotTwo, 
						3 => slotThree, 
						4 => slotFour, 
						_ => slotOne, 
					}).Select();
				}
			});
		}
		else if (gs.overscanAdjusted == 0)
		{
			UIGoToOverscanMenu();
		}
		else if (gs.overscanAdjusted == 1 && gs.brightnessAdjusted == 0)
		{
			UIGoToBrightnessMenu();
		}
	}

	public void ContinueGame()
	{
		ih.StopUIInput();
		uiAudioPlayer.PlayStartGame();
		if ((bool)MenuStyles.Instance)
		{
			MenuStyles.Instance.StopAudio();
		}
		if (menuState == MainMenuState.SAVE_PROFILES)
		{
			StartCoroutine(HideSaveProfileMenu());
		}
	}

	public IEnumerator GoToEngageMenu()
	{
		if (ih == null)
		{
			ih = gm.inputHandler;
		}
		ih.StopUIInput();
		didLeaveEngageMenu = false;
		Platform.Current.ClearEngagement();
		if (menuState == MainMenuState.MAIN_MENU)
		{
			mainMenuScreen.interactable = false;
			yield return StartCoroutine(FadeOutCanvasGroup(mainMenuScreen));
		}
		else if (menuState == MainMenuState.SAVE_PROFILES)
		{
			yield return StartCoroutine(HideSaveProfileMenu());
		}
		else
		{
			yield return StartCoroutine(HideCurrentMenu());
		}
		ih.StopUIInput();
		gameTitle.gameObject.SetActive(value: true);
		if ((bool)MenuStyles.Instance)
		{
			MenuStyles.Instance.UpdateTitle();
		}
		engageMenuScreen.gameObject.SetActive(value: true);
		StartCoroutine(FadeInSprite(gameTitle));
		subtitleFSM.SendEvent("FADE IN");
		engageMenuScreen.topFleur.ResetTrigger("hide");
		engageMenuScreen.topFleur.SetTrigger("show");
		engageMenuScreen.bottomFleur.ResetTrigger("hide");
		engageMenuScreen.bottomFleur.SetTrigger("show");
		StartCoroutine(FadeInCanvasGroup(engageMenuScreen.title));
		yield return StartCoroutine(FadeInCanvasGroup(engageMenuScreen.GetComponent<CanvasGroup>()));
		yield return null;
		SetMenuState(MainMenuState.ENGAGE_MENU);
	}

	public IEnumerator GoToNoSaveMenu()
	{
		if (ih == null)
		{
			ih = gm.inputHandler;
		}
		ih.StopUIInput();
		yield return StartCoroutine(HideCurrentMenu());
		ih.StopUIInput();
		noSaveMenuScreen.gameObject.SetActive(value: true);
		yield return StartCoroutine(ShowMenu(noSaveMenuScreen));
		SetMenuState(MainMenuState.NO_SAVE_MENU);
		ih.StartUIInput();
	}

	public IEnumerator GoToMainMenu()
	{
		if (ih == null)
		{
			ih = gm.inputHandler;
		}
		ih.StopUIInput();
		if (menuState == MainMenuState.OPTIONS_MENU || menuState == MainMenuState.ACHIEVEMENTS_MENU || menuState == MainMenuState.QUIT_GAME_PROMPT || menuState == MainMenuState.EXTRAS_MENU || menuState == MainMenuState.ENGAGE_MENU || menuState == MainMenuState.NO_SAVE_MENU || menuState == MainMenuState.PLAY_MODE_MENU)
		{
			yield return StartCoroutine(HideCurrentMenu());
		}
		else if (menuState == MainMenuState.SAVE_PROFILES)
		{
			yield return StartCoroutine(HideSaveProfileMenu());
		}
		ih.StopUIInput();
		gameTitle.gameObject.SetActive(value: true);
		mainMenuScreen.gameObject.SetActive(value: true);
		if ((bool)MenuStyles.Instance)
		{
			MenuStyles.Instance.UpdateTitle();
		}
		StartCoroutine(FadeInSprite(gameTitle));
		subtitleFSM.SendEvent("FADE IN");
		yield return StartCoroutine(FadeInCanvasGroup(mainMenuScreen));
		mainMenuScreen.interactable = true;
		ih.StartUIInput();
		yield return null;
		mainMenuButtons.HighlightDefault();
		SetMenuState(MainMenuState.MAIN_MENU);
	}

	public IEnumerator GoToProfileMenu()
	{
		ih.StopUIInput();
		if (menuState == MainMenuState.MAIN_MENU)
		{
			StartCoroutine(FadeOutSprite(gameTitle));
			subtitleFSM.SendEvent("FADE OUT");
			yield return StartCoroutine(FadeOutCanvasGroup(mainMenuScreen));
		}
		else if (menuState == MainMenuState.PLAY_MODE_MENU)
		{
			yield return StartCoroutine(HideCurrentMenu());
			ih.StopUIInput();
		}
		StartCoroutine(FadeInCanvasGroup(saveProfileScreen));
		saveProfileTopFleur.ResetTrigger("hide");
		saveProfileTopFleur.SetTrigger("show");
		StartCoroutine(FadeInCanvasGroup(saveProfileTitle));
		StartCoroutine(FadeInCanvasGroup(saveProfileScreen));
		StartCoroutine(PrepareSaveFilesInOrder());
		yield return new WaitForSeconds(0.165f);
		SaveSlotButton[] slotButtons = new SaveSlotButton[4] { slotOne, slotTwo, slotThree, slotFour };
		int i = 0;
		while (i < slotButtons.Length)
		{
			slotButtons[i].ShowRelevantModeForSaveFileState();
			yield return new WaitForSeconds(0.165f);
			int num = i + 1;
			i = num;
		}
		yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.695f));
		StartCoroutine(FadeInCanvasGroup(saveProfileControls));
		ih.StartUIInput();
		yield return null;
		saveSlots.HighlightDefault();
		SetMenuState(MainMenuState.SAVE_PROFILES);
	}

	protected IEnumerator PrepareSaveFilesInOrder()
	{
		SaveSlotButton[] slotButtons = new SaveSlotButton[4] { slotOne, slotTwo, slotThree, slotFour };
		int i = 0;
		while (i < slotButtons.Length)
		{
			SaveSlotButton slotButton = slotButtons[i];
			if (slotButton.saveFileState == SaveSlotButton.SaveFileStates.NotStarted)
			{
				slotButton.Prepare(gm);
				while (slotButton.saveFileState == SaveSlotButton.SaveFileStates.OperationInProgress)
				{
					yield return null;
				}
			}
			int num = i + 1;
			i = num;
		}
	}

	public IEnumerator GoToOptionsMenu()
	{
		ih.StopUIInput();
		if (menuState == MainMenuState.MAIN_MENU)
		{
			StartCoroutine(FadeOutSprite(gameTitle));
			subtitleFSM.SendEvent("FADE OUT");
			yield return StartCoroutine(FadeOutCanvasGroup(mainMenuScreen));
		}
		else if (menuState == MainMenuState.AUDIO_MENU || menuState == MainMenuState.VIDEO_MENU || menuState == MainMenuState.GAMEPAD_MENU || menuState == MainMenuState.GAME_OPTIONS_MENU || menuState == MainMenuState.PAUSE_MENU)
		{
			yield return StartCoroutine(HideCurrentMenu());
		}
		else if (menuState == MainMenuState.KEYBOARD_MENU)
		{
			if (uiButtonSkins.listeningKey != null)
			{
				uiButtonSkins.listeningKey.StopActionListening();
				uiButtonSkins.listeningKey.AbortRebind();
			}
			yield return StartCoroutine(HideCurrentMenu());
		}
		yield return StartCoroutine(ShowMenu(optionsMenuScreen));
		SetMenuState(MainMenuState.OPTIONS_MENU);
		ih.StartUIInput();
	}

	public IEnumerator GoToControllerMenu()
	{
		if (menuState == MainMenuState.OPTIONS_MENU)
		{
			yield return StartCoroutine(HideCurrentMenu());
		}
		else if (menuState == MainMenuState.REMAP_GAMEPAD_MENU)
		{
			if (uiButtonSkins.listeningButton != null)
			{
				uiButtonSkins.listeningButton.StopActionListening();
				uiButtonSkins.listeningButton.AbortRebind();
			}
			yield return StartCoroutine(HideCurrentMenu());
		}
		yield return StartCoroutine(ShowMenu(gamepadMenuScreen));
		SetMenuState(MainMenuState.GAMEPAD_MENU);
	}

	public IEnumerator GoToRemapControllerMenu()
	{
		yield return StartCoroutine(HideCurrentMenu());
		StartCoroutine(ShowMenu(remapGamepadMenuScreen));
		yield return StartCoroutine(uiButtonSkins.ShowCurrentButtonMappings());
		SetMenuState(MainMenuState.REMAP_GAMEPAD_MENU);
	}

	public IEnumerator GoToKeyboardMenu()
	{
		yield return StartCoroutine(HideCurrentMenu());
		StartCoroutine(ShowMenu(keyboardMenuScreen));
		yield return StartCoroutine(uiButtonSkins.ShowCurrentKeyboardMappings());
		SetMenuState(MainMenuState.KEYBOARD_MENU);
	}

	public IEnumerator GoToAudioMenu()
	{
		yield return StartCoroutine(HideCurrentMenu());
		yield return StartCoroutine(ShowMenu(audioMenuScreen));
		SetMenuState(MainMenuState.AUDIO_MENU);
	}

	public IEnumerator GoToVideoMenu(bool rollbackRes = false)
	{
		if (menuState == MainMenuState.OPTIONS_MENU || menuState == MainMenuState.OVERSCAN_MENU || menuState == MainMenuState.BRIGHTNESS_MENU)
		{
			yield return StartCoroutine(HideCurrentMenu());
		}
		else if (menuState == MainMenuState.RESOLUTION_PROMPT)
		{
			if (rollbackRes)
			{
				HideMenuInstant(resolutionPrompt);
				videoMenuScreen.gameObject.SetActive(value: true);
				videoMenuScreen.content.gameObject.SetActive(value: true);
				eventSystem.SetSelectedGameObject(null);
				resolutionOption.RollbackResolution();
			}
			else
			{
				yield return StartCoroutine(HideCurrentMenu());
			}
		}
		yield return StartCoroutine(ShowMenu(videoMenuScreen));
		SetMenuState(MainMenuState.VIDEO_MENU);
	}

	public IEnumerator GoToOverscanMenu()
	{
		if (menuState == MainMenuState.VIDEO_MENU)
		{
			yield return StartCoroutine(HideCurrentMenu());
			overscanSetting.NormalMode();
		}
		else if (menuState == MainMenuState.SAVE_PROFILES)
		{
			yield return StartCoroutine(HideSaveProfileMenu());
			overscanSetting.DoneMode();
		}
		else if (menuState == MainMenuState.PLAY_MODE_MENU)
		{
			yield return StartCoroutine(HideCurrentMenu());
			overscanSetting.DoneMode();
		}
		yield return StartCoroutine(ShowMenu(overscanMenuScreen));
		SetMenuState(MainMenuState.OVERSCAN_MENU);
	}

	public IEnumerator GoToBrightnessMenu()
	{
		if (menuState == MainMenuState.VIDEO_MENU)
		{
			yield return StartCoroutine(HideCurrentMenu());
			brightnessSetting.NormalMode();
		}
		else if (menuState == MainMenuState.OVERSCAN_MENU)
		{
			yield return StartCoroutine(HideCurrentMenu());
			brightnessSetting.DoneMode();
		}
		else if (menuState == MainMenuState.SAVE_PROFILES)
		{
			yield return StartCoroutine(HideSaveProfileMenu());
			brightnessSetting.DoneMode();
		}
		else if (menuState == MainMenuState.PLAY_MODE_MENU)
		{
			yield return StartCoroutine(HideCurrentMenu());
			brightnessSetting.DoneMode();
		}
		yield return StartCoroutine(ShowMenu(brightnessMenuScreen));
		SetMenuState(MainMenuState.BRIGHTNESS_MENU);
	}

	public IEnumerator GoToGameOptionsMenu()
	{
		yield return StartCoroutine(HideCurrentMenu());
		yield return StartCoroutine(ShowMenu(gameOptionsMenuScreen));
		SetMenuState(MainMenuState.GAME_OPTIONS_MENU);
	}

	public IEnumerator GoToAchievementsMenu()
	{
		if (Platform.Current.HasNativeAchievementsDialog)
		{
			Platform.Current.ShowNativeAchievementsDialog();
			yield return null;
			mainMenuButtons.achievementsButton.Select();
			yield break;
		}
		ih.StopUIInput();
		if (menuState == MainMenuState.MAIN_MENU)
		{
			StartCoroutine(FadeOutSprite(gameTitle));
			subtitleFSM.SendEvent("FADE OUT");
			yield return StartCoroutine(FadeOutCanvasGroup(mainMenuScreen));
		}
		else
		{
			Debug.LogError("Entering from this menu not implemented.");
		}
		achievementListRect.anchoredPosition = new Vector2(achievementListRect.anchoredPosition.x, 0f);
		yield return StartCoroutine(ShowMenu(achievementsMenuScreen));
		SetMenuState(MainMenuState.ACHIEVEMENTS_MENU);
		ih.StartUIInput();
	}

	public IEnumerator GoToExtrasMenu()
	{
		ih.StopUIInput();
		if (menuState == MainMenuState.MAIN_MENU)
		{
			StartCoroutine(FadeOutSprite(gameTitle));
			subtitleFSM.SendEvent("FADE OUT");
			yield return StartCoroutine(FadeOutCanvasGroup(mainMenuScreen));
		}
		else if (menuState == MainMenuState.EXTRAS_CONTENT_MENU)
		{
			yield return StartCoroutine(HideMenu(extrasContentMenuScreen));
		}
		else
		{
			Debug.LogError("Entering from this menu not implemented.");
		}
		yield return StartCoroutine(ShowMenu(extrasMenuScreen));
		SetMenuState(MainMenuState.EXTRAS_MENU);
		ih.StartUIInput();
	}

	public IEnumerator GoToExtrasContentMenu()
	{
		ih.StopUIInput();
		if (menuState == MainMenuState.EXTRAS_MENU)
		{
			yield return StartCoroutine(HideMenu(extrasMenuScreen));
		}
		else
		{
			Debug.LogError("Entering from this menu not implemented.");
		}
		yield return StartCoroutine(ShowMenu(extrasContentMenuScreen));
		SetMenuState(MainMenuState.EXTRAS_CONTENT_MENU);
		ih.StartUIInput();
	}

	public IEnumerator GoToQuitGamePrompt()
	{
		ih.StopUIInput();
		if (menuState == MainMenuState.MAIN_MENU)
		{
			StartCoroutine(FadeOutSprite(gameTitle));
			subtitleFSM.SendEvent("FADE OUT");
			yield return StartCoroutine(FadeOutCanvasGroup(mainMenuScreen));
		}
		else
		{
			Debug.LogError("Switching between these menus is not implemented.");
		}
		activePrompt = quitGamePrompt;
		yield return StartCoroutine(ShowMenu(quitGamePrompt));
		SetMenuState(MainMenuState.QUIT_GAME_PROMPT);
		ih.StartUIInput();
	}

	public IEnumerator GoToReturnMenuPrompt()
	{
		ih.StopUIInput();
		if (menuState == MainMenuState.PAUSE_MENU)
		{
			yield return StartCoroutine(HideCurrentMenu());
		}
		else
		{
			Debug.LogError("Switching between these menus is not implemented.");
		}
		activePrompt = quitGamePrompt;
		yield return StartCoroutine(ShowMenu(returnMainMenuPrompt));
		SetMenuState(MainMenuState.EXIT_PROMPT);
		ih.StartUIInput();
	}

	public IEnumerator GoToResolutionPrompt(bool startTimer = false)
	{
		ih.StopUIInput();
		if (menuState == MainMenuState.VIDEO_MENU)
		{
			yield return StartCoroutine(HideMenu(videoMenuScreen));
		}
		else
		{
			Debug.LogError("Switching between these menus is not implemented.");
		}
		activePrompt = resolutionPrompt;
		yield return StartCoroutine(ShowMenu(resolutionPrompt));
		SetMenuState(MainMenuState.RESOLUTION_PROMPT);
		if (startTimer)
		{
			countdownTimer.StartTimer();
		}
		ih.StartUIInput();
	}

	public IEnumerator LeaveOptionsMenu()
	{
		yield return StartCoroutine(HideCurrentMenu());
		if (uiState == UIState.PAUSED)
		{
			UIGoToPauseMenu();
		}
		else
		{
			UIGoToMainMenu();
		}
	}

	public IEnumerator LeaveExitToMenuPrompt()
	{
		yield return StartCoroutine(HideCurrentMenu());
		if (uiState == UIState.PAUSED)
		{
			UnClearPauseMenu();
		}
	}

	public IEnumerator GoToPlayModeMenu()
	{
		ih.StopUIInput();
		if (menuState == MainMenuState.MAIN_MENU)
		{
			StartCoroutine(FadeOutSprite(gameTitle));
			subtitleFSM.SendEvent("FADE OUT");
			yield return StartCoroutine(FadeOutCanvasGroup(mainMenuScreen));
		}
		else if (menuState == MainMenuState.SAVE_PROFILES)
		{
			yield return StartCoroutine(HideSaveProfileMenu());
		}
		yield return StartCoroutine(ShowMenu(playModeMenuScreen));
		SetMenuState(MainMenuState.PLAY_MODE_MENU);
		ih.StartUIInput();
	}

	public IEnumerator ReturnToMainMenu()
	{
		ih.StopUIInput();
		bool calledBack = false;
		bool willSave = Platform.Current.IsSavingAllowedByEngagement;
		GameManager.ReturnToMainMenuSaveModes saveMode = GameManager.ReturnToMainMenuSaveModes.SaveAndCancelOnFail;
		if (!willSave)
		{
			saveMode = GameManager.ReturnToMainMenuSaveModes.SaveAndContinueOnFail;
		}
		StartCoroutine(gm.ReturnToMainMenu(saveMode, delegate(bool willComplete)
		{
			calledBack = true;
			if (!willComplete && willSave)
			{
				ih.StartUIInput();
				returnMainMenuPrompt.HighlightDefault();
			}
			else
			{
				StartCoroutine(HideCurrentMenu());
			}
		}));
		while (!calledBack)
		{
			yield return null;
		}
	}

	public IEnumerator GoToPauseMenu()
	{
		ih.StopUIInput();
		ignoreUnpause = true;
		if (uiState == UIState.PAUSED)
		{
			if (menuState == MainMenuState.OPTIONS_MENU || menuState == MainMenuState.EXIT_PROMPT)
			{
				yield return StartCoroutine(HideCurrentMenu());
			}
		}
		else
		{
			StartCoroutine(FadeInCanvasGroupAlpha(modalDimmer, 0.8f));
		}
		yield return StartCoroutine(ShowMenu(pauseMenuScreen));
		SetMenuState(MainMenuState.PAUSE_MENU);
		ih.StartUIInput();
		ignoreUnpause = false;
	}

	public IEnumerator GoToMenuCredits()
	{
		ih.StopUIInput();
		yield return StartCoroutine(HideCurrentMenu());
		GameCameras.instance.cameraController.FadeOut(CameraFadeType.START_FADE);
		yield return new WaitForSeconds(2.5f);
		gm.LoadScene("Menu_Credits");
	}

	public void ShowCutscenePrompt(CinematicSkipPopup.Texts text)
	{
		cinematicSkipPopup.Show(text);
	}

	public void HideCutscenePrompt()
	{
		cinematicSkipPopup.Hide();
	}

	public void ApplyAudioMenuSettings()
	{
		StartMenuAnimationCoroutine(GoToOptionsMenu());
	}

	public void ApplyVideoMenuSettings()
	{
		StartMenuAnimationCoroutine(GoToOptionsMenu());
	}

	public void ApplyControllerMenuSettings()
	{
		StartMenuAnimationCoroutine(GoToOptionsMenu());
	}

	public void ApplyRemapGamepadMenuSettings()
	{
		StartMenuAnimationCoroutine(GoToControllerMenu());
	}

	public void ApplyKeyboardMenuSettings()
	{
		StartMenuAnimationCoroutine(GoToOptionsMenu());
	}

	public void ApplyOverscanSettings(bool goToBrightness = false)
	{
		Debug.LogError("This function is now deprecated");
	}

	public void ApplyBrightnessSettings()
	{
		StartMenuAnimationCoroutine(GoToVideoMenu());
	}

	public void ApplyGameMenuSettings()
	{
		StartMenuAnimationCoroutine(GoToOptionsMenu());
	}

	public void SaveOverscanSettings()
	{
		gs.SaveOverscanSettings();
	}

	public void SaveBrightnessSettings()
	{
		gs.SaveBrightnessSettings();
	}

	public void DefaultAudioMenuSettings()
	{
		gs.ResetAudioSettings();
		RefreshAudioControls();
	}

	public void DefaultVideoMenuSettings()
	{
		gs.ResetVideoSettings();
		Platform.Current.AdjustGraphicsSettings(gs);
		resolutionOption.ResetToDefaultResolution();
		fullscreenOption.UpdateSetting(gs.fullScreen);
		vsyncOption.UpdateSetting(gs.vSync);
		shadersOption.UpdateSetting((int)gs.shaderQuality);
		RefreshVideoControls();
	}

	public void DefaultGamepadMenuSettings()
	{
		ih.ResetDefaultControllerButtonBindings();
		uiButtonSkins.RefreshButtonMappings();
	}

	public void DefaultKeyboardMenuSettings()
	{
		ih.ResetDefaultKeyBindings();
		uiButtonSkins.RefreshKeyMappings();
	}

	public void DefaultGameMenuSettings()
	{
		gs.ResetGameOptionsSettings();
		Platform.Current.AdjustGameSettings(gs);
		RefreshGameOptionsControls();
	}

	public void LoadStoredSettings()
	{
		gs.LoadOverscanConfigured();
		gs.LoadBrightnessConfigured();
		LoadAudioSettings();
		LoadVideoSettings();
		LoadGameOptionsSettings();
	}

	private void LoadAudioSettings()
	{
		gs.LoadAudioSettings();
		RefreshAudioControls();
	}

	private void LoadVideoSettings()
	{
		gs.LoadVideoSettings();
		gs.LoadOverscanSettings();
		gs.LoadBrightnessSettings();
		Platform.Current.AdjustGraphicsSettings(gs);
		RefreshVideoControls();
	}

	private void LoadGameOptionsSettings()
	{
		gs.LoadGameOptionsSettings();
		Platform.Current.AdjustGameSettings(gs);
		RefreshGameOptionsControls();
	}

	private void LoadControllerSettings()
	{
		Debug.LogError("Not yet implemented.");
	}

	private void RefreshAudioControls()
	{
		masterSlider.RefreshValueFromSettings();
		musicSlider.RefreshValueFromSettings();
		soundSlider.RefreshValueFromSettings();
	}

	private void RefreshVideoControls()
	{
		resolutionOption.RefreshControls();
		fullscreenOption.RefreshValueFromGameSettings();
		vsyncOption.RefreshValueFromGameSettings(alsoApplySetting: true);
		overscanSetting.RefreshValueFromSettings();
		brightnessSetting.RefreshValueFromSettings();
		displayOption.RefreshControls();
		frameCapOption.RefreshValueFromGameSettings(alsoApplySetting: true);
		particlesOption.RefreshValueFromGameSettings();
		shadersOption.RefreshValueFromGameSettings();
	}

	public void DisableFrameCapSetting()
	{
		if ((bool)frameCapOption)
		{
			frameCapOption.UpdateSetting(0);
			frameCapOption.RefreshValueFromGameSettings();
		}
	}

	public void DisableVsyncSetting()
	{
		if ((bool)vsyncOption)
		{
			vsyncOption.UpdateSetting(0);
			vsyncOption.RefreshValueFromGameSettings();
		}
	}

	private void RefreshKeyboardControls()
	{
		uiButtonSkins.RefreshKeyMappings();
	}

	private void RefreshGameOptionsControls()
	{
		languageSetting.RefreshControls();
		backerCreditsSetting.RefreshValueFromGameSettings();
		nativeAchievementsSetting.RefreshValueFromGameSettings();
		controllerRumbleSetting.RefreshValueFromGameSettings(alsoApplySetting: true);
		nativeInputSetting.RefreshValueFromGameSettings(alsoApplySetting: true);
	}

	public void RefreshAchievementsList()
	{
		AchievementsList achievementsList = gm.achievementHandler.achievementsList;
		int count = achievementsList.achievements.Count;
		if (menuAchievementsList.init)
		{
			for (int i = 0; i < count; i++)
			{
				Achievement achievement = achievementsList.achievements[i];
				MenuAchievement menuAchievement = menuAchievementsList.FindAchievement(achievement.key);
				if (menuAchievement != null)
				{
					UpdateMenuAchievementStatus(achievement, menuAchievement);
				}
				else
				{
					Debug.LogError("UI - Could not locate MenuAchievement " + achievement.key);
				}
			}
			return;
		}
		for (int j = 0; j < count; j++)
		{
			Achievement achievement2 = achievementsList.achievements[j];
			MenuAchievement menuAchievement2 = UnityEngine.Object.Instantiate(menuAchievementsList.menuAchievementPrefab);
			menuAchievement2.transform.SetParent(achievementListRect.transform, worldPositionStays: false);
			menuAchievement2.name = achievement2.key;
			menuAchievementsList.AddMenuAchievement(menuAchievement2);
			UpdateMenuAchievementStatus(achievement2, menuAchievement2);
		}
		menuAchievementsList.MarkInit();
	}

	private void UpdateMenuAchievementStatus(Achievement ach, MenuAchievement menuAch)
	{
		try
		{
			if (gm.IsAchievementAwarded(ach.key))
			{
				menuAch.icon.sprite = ach.earnedIcon;
				menuAch.icon.color = Color.white;
				menuAch.title.text = global::Language.Language.Get(ach.localizedTitle, "Achievements");
				menuAch.text.text = global::Language.Language.Get(ach.localizedText, "Achievements");
			}
			else if (ach.type == AchievementType.Normal)
			{
				menuAch.icon.sprite = ach.earnedIcon;
				menuAch.icon.color = new Color(0.57f, 0.57f, 0.57f, 0.57f);
				menuAch.title.text = global::Language.Language.Get(ach.localizedTitle, "Achievements");
				menuAch.text.text = global::Language.Language.Get(ach.localizedText, "Achievements");
			}
			else
			{
				menuAch.icon.sprite = hiddenIcon;
				menuAch.icon.color = new Color(0.57f, 0.57f, 0.57f, 0.57f);
				menuAch.title.text = global::Language.Language.Get("HIDDEN_ACHIEVEMENT_TITLE", "Achievements");
				menuAch.text.text = global::Language.Language.Get("HIDDEN_ACHIEVEMENT", "Achievements");
			}
		}
		catch (Exception)
		{
			if (ach.type == AchievementType.Normal)
			{
				menuAch.icon.sprite = ach.earnedIcon;
				menuAch.icon.color = new Color(0.57f, 0.57f, 0.57f, 0.57f);
				menuAch.title.text = global::Language.Language.Get(ach.localizedTitle, "Achievements");
				menuAch.text.text = global::Language.Language.Get(ach.localizedText, "Achievements");
			}
			else
			{
				menuAch.icon.sprite = hiddenIcon;
				menuAch.title.text = global::Language.Language.Get("HIDDEN_ACHIEVEMENT_TITLE", "Achievements");
				menuAch.text.text = global::Language.Language.Get("HIDDEN_ACHIEVEMENT", "Achievements");
			}
		}
	}

	public void TogglePauseGame()
	{
		if (!ignoreUnpause)
		{
			togglePauseCo = StartCoroutine(gm.PauseGameToggleByMenu());
		}
	}

	public void QuitGame()
	{
		ih.StopUIInput();
		StartMenuAnimationCoroutine(gm.QuitGame());
	}

	public void FadeOutMenuAudio(float duration)
	{
		menuSilenceSnapshot.TransitionTo(duration);
	}

	public void AudioGoToPauseMenu(float duration)
	{
		menuPauseSnapshot.TransitionTo(duration);
	}

	public void AudioGoToGameplay(float duration)
	{
		gameplaySnapshot.TransitionTo(duration);
	}

	public void ConfigureMenu()
	{
		if (mainMenuButtons != null)
		{
			mainMenuButtons.ConfigureNavigation();
		}
		if (gameMenuOptions != null)
		{
			gameMenuOptions.ConfigureNavigation();
		}
		if (videoMenuOptions != null)
		{
			videoMenuOptions.ConfigureNavigation();
		}
		if (uiState == UIState.MAIN_MENU_HOME)
		{
			if (slotOne != null)
			{
				slotOne.healthSlots.Awake();
			}
			if (slotTwo != null)
			{
				slotTwo.healthSlots.Awake();
			}
			if (slotThree != null)
			{
				slotThree.healthSlots.Awake();
			}
			if (slotFour != null)
			{
				slotFour.healthSlots.Awake();
			}
		}
	}

	public IEnumerator HideCurrentMenu()
	{
		if (menuState == MainMenuState.DYNAMIC_MENU)
		{
			UIManager.BeforeHideDynamicMenu?.Invoke();
			return HideMenu(currentDynamicMenu);
		}
		return orig_HideCurrentMenu();
	}

	public IEnumerator ShowMenu(MenuScreen menu)
	{
		isFadingMenu = true;
		ih.StopUIInput();
		if (menu.screenCanvasGroup != null)
		{
			StartCoroutine(FadeInCanvasGroup(menu.screenCanvasGroup));
		}
		if (menu.title != null)
		{
			StartCoroutine(FadeInCanvasGroup(menu.title));
		}
		if (menu.topFleur != null)
		{
			yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.1f));
			menu.topFleur.ResetTrigger("hide");
			menu.topFleur.SetTrigger("show");
		}
		yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.1f));
		if (menu.content != null)
		{
			StartCoroutine(FadeInCanvasGroup(menu.content));
		}
		if (menu.controls != null)
		{
			StartCoroutine(FadeInCanvasGroup(menu.controls));
		}
		if (menu.bottomFleur != null)
		{
			menu.bottomFleur.ResetTrigger("hide");
			menu.bottomFleur.SetTrigger("show");
		}
		yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.1f));
		ih.StartUIInput();
		yield return null;
		menu.HighlightDefault();
		isFadingMenu = false;
	}

	public IEnumerator HideMenu(MenuScreen menu)
	{
		isFadingMenu = true;
		ih.StopUIInput();
		if (menu.title != null)
		{
			StartCoroutine(FadeOutCanvasGroup(menu.title));
		}
		if (menu.topFleur != null)
		{
			yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.1f));
			menu.topFleur.ResetTrigger("show");
			menu.topFleur.SetTrigger("hide");
		}
		yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.1f));
		if (menu.content != null)
		{
			StartCoroutine(FadeOutCanvasGroup(menu.content));
		}
		if (menu.controls != null)
		{
			StartCoroutine(FadeOutCanvasGroup(menu.controls));
		}
		if (menu.bottomFleur != null)
		{
			yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.1f));
			menu.bottomFleur.ResetTrigger("show");
			menu.bottomFleur.SetTrigger("hide");
		}
		yield return StartCoroutine(FadeOutCanvasGroup(menu.screenCanvasGroup));
		ih.StartUIInput();
		isFadingMenu = false;
	}

	public void HideMenuInstant(MenuScreen menu)
	{
		ih.StopUIInput();
		if (menu.title != null)
		{
			HideCanvasGroup(menu.title);
		}
		if (menu.topFleur != null)
		{
			menu.topFleur.ResetTrigger("show");
			menu.topFleur.SetTrigger("hide");
		}
		if (menu.content != null)
		{
			HideCanvasGroup(menu.content);
		}
		if (menu.controls != null)
		{
			HideCanvasGroup(menu.controls);
		}
		HideCanvasGroup(menu.screenCanvasGroup);
		ih.StartUIInput();
	}

	public IEnumerator HideSaveProfileMenu()
	{
		StartCoroutine(FadeOutCanvasGroup(saveProfileTitle));
		saveProfileTopFleur.ResetTrigger("show");
		saveProfileTopFleur.SetTrigger("hide");
		yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.165f));
		slotOne.HideSaveSlot();
		yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.165f));
		slotTwo.HideSaveSlot();
		yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.165f));
		slotThree.HideSaveSlot();
		yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.165f));
		slotFour.HideSaveSlot();
		yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.33f));
		yield return StartCoroutine(FadeOutCanvasGroup(saveProfileControls));
		yield return StartCoroutine(FadeOutCanvasGroup(saveProfileScreen));
	}

	private void DisableScreens()
	{
		for (int i = 0; i < UICanvas.transform.childCount; i++)
		{
			if (!(UICanvas.transform.GetChild(i).name == "PauseMenuScreen"))
			{
				UICanvas.transform.GetChild(i).gameObject.SetActive(value: false);
			}
		}
		if ((bool)achievementsPopupPanel)
		{
			achievementsPopupPanel.SetActive(value: true);
		}
	}

	private void ShowCanvas(Canvas canvas)
	{
		canvas.gameObject.SetActive(value: true);
	}

	private void HideCanvas(Canvas canvas)
	{
		canvas.gameObject.SetActive(value: false);
	}

	public void ShowCanvasGroup(CanvasGroup cg)
	{
		cg.gameObject.SetActive(value: true);
		cg.interactable = true;
		cg.alpha = 1f;
	}

	public void HideCanvasGroup(CanvasGroup cg)
	{
		cg.interactable = false;
		cg.alpha = 0f;
		cg.gameObject.SetActive(value: false);
	}

	public IEnumerator FadeInCanvasGroup(CanvasGroup cg)
	{
		float loopFailsafe = 0f;
		cg.alpha = 0f;
		cg.gameObject.SetActive(value: true);
		while (cg.alpha < 1f)
		{
			cg.alpha += Time.unscaledDeltaTime * MENU_FADE_SPEED;
			loopFailsafe += Time.unscaledDeltaTime;
			if (cg.alpha >= 0.95f)
			{
				cg.alpha = 1f;
				break;
			}
			if (loopFailsafe >= 2f)
			{
				break;
			}
			yield return null;
		}
		cg.alpha = 1f;
		cg.interactable = true;
		cg.gameObject.SetActive(value: true);
		yield return null;
	}

	public IEnumerator FadeInCanvasGroupAlpha(CanvasGroup cg, float endAlpha)
	{
		float loopFailsafe = 0f;
		if (endAlpha > 1f)
		{
			endAlpha = 1f;
		}
		cg.alpha = 0f;
		cg.gameObject.SetActive(value: true);
		while (cg.alpha < endAlpha - 0.05f)
		{
			cg.alpha += Time.unscaledDeltaTime * MENU_FADE_SPEED;
			loopFailsafe += Time.unscaledDeltaTime;
			if (cg.alpha >= endAlpha - 0.05f)
			{
				cg.alpha = endAlpha;
				break;
			}
			if (loopFailsafe >= 2f)
			{
				break;
			}
			yield return null;
		}
		cg.alpha = endAlpha;
		cg.interactable = true;
		cg.gameObject.SetActive(value: true);
		yield return null;
	}

	public IEnumerator FadeOutCanvasGroup(CanvasGroup cg)
	{
		float loopFailsafe = 0f;
		cg.interactable = false;
		while (cg.alpha > 0.05f)
		{
			cg.alpha -= Time.unscaledDeltaTime * MENU_FADE_SPEED;
			loopFailsafe += Time.unscaledDeltaTime;
			if (cg.alpha <= 0.05f || loopFailsafe >= 2f)
			{
				break;
			}
			yield return null;
		}
		cg.alpha = 0f;
		cg.gameObject.SetActive(value: false);
		yield return null;
	}

	private IEnumerator FadeInSprite(SpriteRenderer sprite)
	{
		while (sprite.color.a < 1f)
		{
			sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + Time.unscaledDeltaTime * MENU_FADE_SPEED);
			yield return null;
		}
		sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
		yield return null;
	}

	private IEnumerator FadeOutSprite(SpriteRenderer sprite)
	{
		while (sprite.color.a > 0f)
		{
			sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - Time.unscaledDeltaTime * MENU_FADE_SPEED);
			yield return null;
		}
		sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
		yield return null;
	}

	private void EnableChildren(RectTransform parent)
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			parent.GetChild(i).gameObject.SetActive(value: true);
		}
	}

	private void EnableChildren(Canvas parent)
	{
		for (int i = 0; i < parent.transform.childCount; i++)
		{
			parent.transform.GetChild(i).gameObject.SetActive(value: true);
		}
	}

	private void DisableChildren(Canvas parent)
	{
		for (int i = 0; i < parent.transform.childCount; i++)
		{
			parent.transform.GetChild(i).gameObject.SetActive(value: false);
		}
	}

	private float GetAnimationClipLength(Animator animator, string clipName)
	{
		RuntimeAnimatorController runtimeAnimatorController = animator.runtimeAnimatorController;
		for (int i = 0; i < runtimeAnimatorController.animationClips.Length; i++)
		{
			if (runtimeAnimatorController.animationClips[i].name == clipName)
			{
				return runtimeAnimatorController.animationClips[i].length;
			}
		}
		return -1f;
	}

	public void MakeMenuLean()
	{
		Debug.Log("Making UI menu lean.");
		if ((bool)saveProfileScreen)
		{
			UnityEngine.Object.Destroy(saveProfileScreen.gameObject);
			saveProfileScreen = null;
		}
		if ((bool)achievementsMenuScreen)
		{
			UnityEngine.Object.Destroy(achievementsMenuScreen.gameObject);
			achievementsMenuScreen = null;
		}
		if (!Platform.Current.WillDisplayGraphicsSettings)
		{
			if ((bool)videoMenuScreen)
			{
				UnityEngine.Object.Destroy(videoMenuScreen.gameObject);
				videoMenuScreen = null;
			}
			if ((bool)brightnessMenuScreen)
			{
				UnityEngine.Object.Destroy(brightnessMenuScreen.gameObject);
				brightnessMenuScreen = null;
			}
			if ((bool)overscanMenuScreen)
			{
				UnityEngine.Object.Destroy(overscanMenuScreen.gameObject);
				overscanMenuScreen = null;
			}
		}
	}

	public static UIManager orig_get_instance()
	{
		if (_instance == null)
		{
			_instance = UnityEngine.Object.FindObjectOfType<UIManager>();
			if (_instance == null)
			{
				Debug.LogError("Couldn't find a UIManager, make sure one exists in the scene.");
			}
			if (Application.isPlaying)
			{
				UnityEngine.Object.DontDestroyOnLoad(_instance.gameObject);
			}
		}
		return _instance;
	}

	private Sprite LoadImage()
	{
		using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Modding.logo.png");
		byte[] array = new byte[stream.Length];
		stream.Read(array, 0, array.Length);
		Texture2D texture2D = new Texture2D(2, 2);
		texture2D.LoadImage(array, markNonReadable: true);
		return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.one * 0.5f);
	}

	private void orig_Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		else if (this != _instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		graphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
	}

	private void orig_Start()
	{
		if (!(this == _instance))
		{
			return;
		}
		SetupRefs();
		if (gm.IsMenuScene())
		{
			startMenuTime = Time.time + 0.5f;
			GameCameras.instance.cameraController.FadeSceneIn();
			LoadStoredSettings();
			if (Platform.Current.AreAchievementsFetched)
			{
				RefreshAchievementsList();
			}
			else
			{
				Platform.AchievementsFetched += delegate
				{
					RefreshAchievementsList();
				};
			}
			ConfigureMenu();
		}
		if ((bool)graphicRaycaster && (bool)InputHandler.Instance)
		{
			InputHandler.Instance.OnCursorVisibilityChange += delegate(bool isVisible)
			{
				graphicRaycaster.enabled = isVisible;
			};
		}
		if (Platform.Current.WillPreloadSaveFiles)
		{
			slotOne.Prepare(gm);
			slotTwo.Prepare(gm);
			slotThree.Prepare(gm);
			slotFour.Prepare(gm);
		}
	}

	public IEnumerator orig_HideCurrentMenu()
	{
		isFadingMenu = true;
		MenuScreen menu;
		switch (menuState)
		{
		default:
			yield break;
		case MainMenuState.OPTIONS_MENU:
			menu = optionsMenuScreen;
			break;
		case MainMenuState.AUDIO_MENU:
			menu = audioMenuScreen;
			gs.SaveAudioSettings();
			break;
		case MainMenuState.VIDEO_MENU:
			menu = videoMenuScreen;
			gs.SaveVideoSettings();
			break;
		case MainMenuState.GAMEPAD_MENU:
			menu = gamepadMenuScreen;
			gs.SaveGameOptionsSettings();
			break;
		case MainMenuState.KEYBOARD_MENU:
			menu = keyboardMenuScreen;
			ih.SendKeyBindingsToGameSettings();
			gs.SaveKeyboardSettings();
			break;
		case MainMenuState.OVERSCAN_MENU:
			menu = overscanMenuScreen;
			break;
		case MainMenuState.GAME_OPTIONS_MENU:
			menu = gameOptionsMenuScreen;
			gs.SaveGameOptionsSettings();
			break;
		case MainMenuState.ACHIEVEMENTS_MENU:
			menu = achievementsMenuScreen;
			break;
		case MainMenuState.QUIT_GAME_PROMPT:
			menu = quitGamePrompt;
			break;
		case MainMenuState.RESOLUTION_PROMPT:
			menu = resolutionPrompt;
			break;
		case MainMenuState.EXIT_PROMPT:
			menu = returnMainMenuPrompt;
			break;
		case MainMenuState.BRIGHTNESS_MENU:
			menu = brightnessMenuScreen;
			gs.SaveBrightnessSettings();
			break;
		case MainMenuState.PAUSE_MENU:
			menu = pauseMenuScreen;
			break;
		case MainMenuState.PLAY_MODE_MENU:
			menu = playModeMenuScreen;
			break;
		case MainMenuState.EXTRAS_MENU:
			menu = extrasMenuScreen;
			break;
		case MainMenuState.REMAP_GAMEPAD_MENU:
			menu = remapGamepadMenuScreen;
			if (uiButtonSkins.listeningButton != null)
			{
				uiButtonSkins.listeningButton.StopActionListening();
				uiButtonSkins.listeningButton.AbortRebind();
			}
			ih.SendButtonBindingsToGameSettings();
			gs.SaveGamepadSettings(ih.activeGamepadType);
			break;
		case MainMenuState.ENGAGE_MENU:
			menu = engageMenuScreen;
			break;
		case MainMenuState.NO_SAVE_MENU:
			menu = noSaveMenuScreen;
			break;
		}
		ih.StopUIInput();
		if (menu.title != null)
		{
			StartCoroutine(FadeOutCanvasGroup(menu.title));
			yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.1f));
		}
		if (menu.topFleur != null)
		{
			menu.topFleur.ResetTrigger("show");
			menu.topFleur.SetTrigger("hide");
			yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.1f));
		}
		if (menu.content != null)
		{
			StartCoroutine(FadeOutCanvasGroup(menu.content));
		}
		if (menu.controls != null)
		{
			StartCoroutine(FadeOutCanvasGroup(menu.controls));
		}
		if (menu.bottomFleur != null)
		{
			menu.bottomFleur.ResetTrigger("show");
			menu.bottomFleur.SetTrigger("hide");
			yield return StartCoroutine(gm.timeTool.TimeScaleIndependentWaitForSeconds(0.1f));
		}
		if (menu.screenCanvasGroup != null)
		{
			yield return StartCoroutine(FadeOutCanvasGroup(menu.screenCanvasGroup));
		}
		ih.StartUIInput();
		isFadingMenu = false;
	}

	public void UIGoToDynamicMenu(MenuScreen menu)
	{
		StartMenuAnimationCoroutine(GoToDynamicMenu(menu));
	}

	public IEnumerator GoToDynamicMenu(MenuScreen menu)
	{
		ih.StopUIInput();
		yield return HideCurrentMenu();
		yield return ShowMenu(menu);
		currentDynamicMenu = menu;
		SetMenuState(MainMenuState.DYNAMIC_MENU);
		ih.StartUIInput();
	}

	public void UILeaveDynamicMenu(MenuScreen to, MainMenuState state)
	{
		StartMenuAnimationCoroutine(LeaveDynamicMenu(to, state));
	}

	public IEnumerator LeaveDynamicMenu(MenuScreen to, MainMenuState state)
	{
		ih.StopUIInput();
		yield return HideCurrentMenu();
		yield return ShowMenu(to);
		SetMenuState(state);
		ih.StartUIInput();
	}

	public void UIPauseToDynamicMenu(MenuScreen to)
	{
		if (uiState != UIState.PAUSED)
		{
			StartMenuAnimationCoroutine(PauseToDynamicMenu(to));
			uiState = UIState.PAUSED;
		}
	}

	public IEnumerator PauseToDynamicMenu(MenuScreen to)
	{
		ih.StopUIInput();
		ignoreUnpause = true;
		if (uiState == UIState.PAUSED)
		{
			if (menuState == MainMenuState.OPTIONS_MENU || menuState == MainMenuState.EXIT_PROMPT)
			{
				yield return HideCurrentMenu();
			}
		}
		else
		{
			StartCoroutine(FadeInCanvasGroupAlpha(modalDimmer, 0.8f));
		}
		yield return ShowMenu(to);
		currentDynamicMenu = to;
		SetMenuState(MainMenuState.DYNAMIC_MENU);
		ih.StartUIInput();
		ignoreUnpause = false;
	}
}
