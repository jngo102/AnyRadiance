

// InputHandler
using System;
using System.Collections;
using System.Collections.Generic;
using GlobalEnums;
using InControl;
using Modding;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(GameManager))]
public class InputHandler : MonoBehaviour
{
	public readonly struct KeyOrMouseBinding
	{
		public readonly Key Key;

		public readonly Mouse Mouse;

		public KeyOrMouseBinding(Key key)
		{
			Key = key;
			Mouse = Mouse.None;
		}

		public KeyOrMouseBinding(Mouse mouse)
		{
			Key = Key.None;
			Mouse = mouse;
		}

		public static bool IsNone(KeyOrMouseBinding val)
		{
			if (val.Key == Key.None)
			{
				return val.Mouse == Mouse.None;
			}
			return false;
		}

		public override string ToString()
		{
			if (Mouse != 0)
			{
				return Mouse.ToString();
			}
			return Key.ToString();
		}
	}

	public delegate void CursorVisibilityChange(bool isVisible);

	private enum TFRMode
	{
		Off,
		MatchFrameRate,
		DoubleFrameRate
	}

	public delegate void ActiveControllerSwitch();

	public static InputHandler Instance;

	private bool verboseMode;

	private GameManager gm;

	private GameSettings gs;

	private GameConfig gc;

	public InputDevice gameController;

	public HeroActions inputActions;

	public BindingSourceType lastActiveController;

	public InputDeviceStyle lastInputDeviceStyle;

	public GamepadType activeGamepadType;

	public GamepadState gamepadState;

	private HeroController heroCtrl;

	private HeroControllerStates cState;

	private PlayerData playerData;

	private OnScreenDebugInfo debugInfo;

	private List<PlayerAction> nonMappableControllerActions;

	public float inputX;

	public float inputY;

	public bool acceptingInput;

	public bool skippingCutscene;

	private bool readyToSkipCutscene;

	private bool controllerDetected;

	private ControllerProfile currentControllerProfile;

	private TFRMode tfrMode;

	private bool isGameplayScene;

	private bool isTitleScreenScene;

	private bool isMenuScene;

	private bool isStagTravelScene;

	private float stagLockoutDuration = 1.2f;

	private bool stagLockoutActive;

	private float skipCooldownTime;

	private bool controllerPressed;

	public List<PlayerAction> mappableControllerActions { get; private set; }

	public List<PlayerAction> unmappedActions { get; private set; }

	[SerializeField]
	public bool pauseAllowed { get; private set; }

	public SkipPromptMode skipMode { get; private set; }

	public event CursorVisibilityChange OnCursorVisibilityChange;

	public event ActiveControllerSwitch RefreshActiveControllerEvent;

	public void Awake()
	{
		Instance = this;
		gm = GetComponent<GameManager>();
		debugInfo = GetComponent<OnScreenDebugInfo>();
		gs = gm.gameSettings;
		gc = gm.gameConfig;
		inputActions = new HeroActions();
		acceptingInput = true;
		pauseAllowed = true;
		skipMode = SkipPromptMode.NOT_SKIPPABLE;
		SaveDataUpgradeHandler.UpgradeSystemData(this);
	}

	public void Start()
	{
		playerData = gm.playerData;
		SetupNonMappableBindings();
		gs.LoadKeyboardSettings();
		MapKeyboardLayoutFromGameSettings();
		InputManager.OnDeviceAttached += ControllerAttached;
		InputManager.OnActiveDeviceChanged += ControllerActivated;
		InputManager.OnDeviceDetached += ControllerDetached;
		if (InputManager.ActiveDevice != null && InputManager.ActiveDevice.IsAttached)
		{
			ControllerActivated(InputManager.ActiveDevice);
		}
		else
		{
			gameController = InputDevice.Null;
		}
		Debug.LogFormat("Game controller set to {0}.", gameController.Name);
		lastActiveController = BindingSourceType.None;
	}

	private void OnDestroy()
	{
		InputManager.OnDeviceAttached -= ControllerAttached;
		InputManager.OnActiveDeviceChanged -= ControllerActivated;
		InputManager.OnDeviceDetached -= ControllerDetached;
		inputActions.Destroy();
	}

	public void SceneInit()
	{
		if (gm.IsGameplayScene())
		{
			isGameplayScene = true;
		}
		else
		{
			isGameplayScene = false;
		}
		if (gm.IsTitleScreenScene())
		{
			isTitleScreenScene = true;
		}
		else
		{
			isTitleScreenScene = false;
		}
		if (gm.IsMenuScene())
		{
			isMenuScene = true;
		}
		else
		{
			isMenuScene = false;
		}
		if (gm.IsStagTravelScene())
		{
			isStagTravelScene = true;
			stagLockoutActive = true;
			Invoke("UnlockStagInput", stagLockoutDuration);
		}
		else
		{
			isStagTravelScene = false;
		}
	}

	private void OnGUI()
	{
		Cursor.lockState = CursorLockMode.None;
		if (isTitleScreenScene)
		{
			Cursor.visible = false;
		}
		else if (controllerPressed)
		{
			Cursor.visible = false;
		}
		else
		{
			Cursor.visible = true;
		}
	}

	private void SetCursorVisible(bool value)
	{
		SetCursorEnabled(value);
		if (this.OnCursorVisibilityChange != null)
		{
			this.OnCursorVisibilityChange(value);
		}
	}

	private static void SetCursorEnabled(bool isEnabled)
	{
		if (isEnabled && Platform.Current.IsMouseSupported)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		else
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	private void Update()
	{
		UpdateActiveController();
		if (acceptingInput)
		{
			if (gm.gameState == GameState.PLAYING)
			{
				PlayingInput();
			}
			else if (gm.gameState == GameState.CUTSCENE)
			{
				if (isStagTravelScene)
				{
					if (!stagLockoutActive)
					{
						StagCutsceneInput();
					}
				}
				else
				{
					CutsceneInput();
				}
			}
			if (inputActions.pause.WasPressed && pauseAllowed && !playerData.GetBool("disablePause") && (gm.gameState == GameState.PLAYING || gm.gameState == GameState.PAUSED))
			{
				StartCoroutine(gm.PauseGameToggle());
			}
		}
		if (gc.enableDebugButtons)
		{
			if (Input.GetKeyDown(KeyCode.End) || Input.GetKeyDown(KeyCode.Quote))
			{
				debugInfo.ShowGameInfo();
			}
			if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Semicolon))
			{
				debugInfo.ShowFPS();
				debugInfo.ShowLoadingTime();
				debugInfo.ShowTargetFrameRate();
			}
		}
		if (controllerPressed)
		{
			if (Mathf.Abs(Input.GetAxisRaw("mouse x")) > 0.1f)
			{
				controllerPressed = false;
			}
		}
		else if (inputActions.ActiveDevice.AnyButtonIsPressed || inputActions.moveVector.WasPressed)
		{
			controllerPressed = true;
		}
	}

	private void ControllerAttached(InputDevice inputDevice)
	{
		gamepadState = GamepadState.ATTACHED;
		gameController = inputDevice;
		Debug.LogFormat("Game controller {0} attached", inputDevice.Name);
		SetActiveGamepadType(inputDevice);
	}

	private void ControllerActivated(InputDevice inputDevice)
	{
		gamepadState = GamepadState.ACTIVATED;
		gameController = inputDevice;
		Debug.LogFormat("Game controller set to {0}.", inputDevice.Name);
		SetActiveGamepadType(inputDevice);
	}

	private void ControllerDetached(InputDevice inputDevice)
	{
		gamepadState = GamepadState.DETACHED;
		activeGamepadType = GamepadType.NONE;
		gameController = InputDevice.Null;
		Debug.LogFormat("Game controller {0} detached.", inputDevice.Name);
		UIManager instance = UIManager.instance;
		if (instance.uiButtonSkins.listeningButton != null)
		{
			instance.uiButtonSkins.listeningButton.StopActionListening();
			instance.uiButtonSkins.listeningButton.AbortRebind();
			instance.uiButtonSkins.RefreshButtonMappings();
		}
	}

	private void PlayingInput()
	{
	}

	private void CutsceneInput()
	{
		if ((!Input.anyKeyDown && !gameController.AnyButton.WasPressed) || skippingCutscene)
		{
			return;
		}
		switch (skipMode)
		{
			case SkipPromptMode.SKIP_INSTANT:
				skippingCutscene = true;
				gm.SkipCutscene();
				break;
			case SkipPromptMode.SKIP_PROMPT:
				if (!readyToSkipCutscene)
				{
					gm.ui.ShowCutscenePrompt(CinematicSkipPopup.Texts.Skip);
					readyToSkipCutscene = true;
					CancelInvoke("StopCutsceneInput");
					Invoke("StopCutsceneInput", 5f * Time.timeScale);
					skipCooldownTime = Time.time + 0.3f;
				}
				else if (!(Time.time < skipCooldownTime))
				{
					CancelInvoke("StopCutsceneInput");
					readyToSkipCutscene = false;
					skippingCutscene = true;
					gm.SkipCutscene();
				}
				break;
			case SkipPromptMode.NOT_SKIPPABLE_DUE_TO_LOADING:
				gm.ui.ShowCutscenePrompt(CinematicSkipPopup.Texts.Loading);
				CancelInvoke("StopCutsceneInput");
				Invoke("StopCutsceneInput", 5f * Time.timeScale);
				break;
			case SkipPromptMode.NOT_SKIPPABLE:
				break;
		}
	}

	private void StagCutsceneInput()
	{
		if (Input.anyKeyDown || gameController.AnyButton.WasPressed)
		{
			gm.SkipCutscene();
		}
	}

	private void BetaEndInput()
	{
		if (Input.anyKeyDown || gameController.AnyButton.WasPressed)
		{
			StartCoroutine(gm.ReturnToMainMenu(GameManager.ReturnToMainMenuSaveModes.SaveAndContinueOnFail));
		}
	}

	public void AttachHeroController(HeroController heroController)
	{
		heroCtrl = heroController;
		cState = heroCtrl.cState;
	}

	public void StopAcceptingInput()
	{
		acceptingInput = false;
	}

	public void StartAcceptingInput()
	{
		acceptingInput = true;
	}

	public void PreventPause()
	{
		pauseAllowed = false;
	}

	public void AllowPause()
	{
		pauseAllowed = true;
	}

	public void UpdateActiveController()
	{
		if (lastActiveController != inputActions.LastInputType || lastInputDeviceStyle != inputActions.LastDeviceStyle)
		{
			lastActiveController = inputActions.LastInputType;
			lastInputDeviceStyle = inputActions.LastDeviceStyle;
			if (this.RefreshActiveControllerEvent != null)
			{
				this.RefreshActiveControllerEvent();
			}
		}
	}

	public void StopUIInput()
	{
		acceptingInput = false;
		EventSystem.current.sendNavigationEvents = false;
		UIManager.instance.inputModule.allowMouseInput = false;
	}

	public void StartUIInput()
	{
		acceptingInput = true;
		EventSystem.current.sendNavigationEvents = true;
		UIManager.instance.inputModule.allowMouseInput = true;
	}

	public void StopMouseInput()
	{
		UIManager.instance.inputModule.allowMouseInput = false;
	}

	public void EnableMouseInput()
	{
		UIManager.instance.inputModule.allowMouseInput = true;
	}

	public void SetSkipMode(SkipPromptMode newMode)
	{
		Debug.Log("Setting skip mode: " + newMode);
		switch (newMode)
		{
			case SkipPromptMode.NOT_SKIPPABLE:
				StopAcceptingInput();
				break;
			case SkipPromptMode.SKIP_PROMPT:
				readyToSkipCutscene = false;
				StartAcceptingInput();
				break;
			case SkipPromptMode.SKIP_INSTANT:
				StartAcceptingInput();
				break;
			case SkipPromptMode.NOT_SKIPPABLE_DUE_TO_LOADING:
				readyToSkipCutscene = false;
				StartAcceptingInput();
				break;
		}
		skipMode = newMode;
	}

	public void RefreshPlayerData()
	{
		playerData = PlayerData.instance;
	}

	public void ResetDefaultKeyBindings()
	{
		if (verboseMode)
		{
			Debug.LogFormat("Active Device: {0} GamePadState: {1} GamePadType: {2}", InputManager.ActiveDevice.Name, gamepadState, activeGamepadType);
		}
		inputActions.jump.ClearBindings();
		inputActions.attack.ClearBindings();
		inputActions.dash.ClearBindings();
		inputActions.cast.ClearBindings();
		inputActions.superDash.ClearBindings();
		inputActions.dreamNail.ClearBindings();
		inputActions.quickMap.ClearBindings();
		inputActions.openInventory.ClearBindings();
		inputActions.quickCast.ClearBindings();
		inputActions.up.ClearBindings();
		inputActions.down.ClearBindings();
		inputActions.left.ClearBindings();
		inputActions.right.ClearBindings();
		MapDefaultKeyboardLayout();
		gs.jumpKey = Key.Z.ToString();
		gs.attackKey = Key.X.ToString();
		gs.dashKey = Key.C.ToString();
		gs.castKey = Key.A.ToString();
		gs.superDashKey = Key.S.ToString();
		gs.dreamNailKey = Key.D.ToString();
		gs.quickMapKey = Key.Tab.ToString();
		gs.inventoryKey = Key.I.ToString();
		gs.quickCastKey = Key.F.ToString();
		gs.upKey = Key.UpArrow.ToString();
		gs.downKey = Key.DownArrow.ToString();
		gs.leftKey = Key.LeftArrow.ToString();
		gs.rightKey = Key.RightArrow.ToString();
		gs.SaveKeyboardSettings();
		if (gameController != InputDevice.Null)
		{
			SetActiveGamepadType(gameController);
		}
	}

	public void ResetDefaultControllerButtonBindings()
	{
		if (verboseMode)
		{
			Debug.LogFormat("Active Device: {0} GamePadState: {1} GamePadType: {2}", InputManager.ActiveDevice.Name, gamepadState, activeGamepadType);
		}
		inputActions.jump.ClearBindings();
		inputActions.attack.ClearBindings();
		inputActions.dash.ClearBindings();
		inputActions.cast.ClearBindings();
		inputActions.superDash.ClearBindings();
		inputActions.dreamNail.ClearBindings();
		inputActions.quickMap.ClearBindings();
		inputActions.quickCast.ClearBindings();
		MapKeyboardLayoutFromGameSettings();
		gs.ResetGamepadSettings(activeGamepadType);
		gs.SaveGamepadSettings(activeGamepadType);
		MapControllerButtons(activeGamepadType);
	}

	public void ResetAllControllerButtonBindings()
	{
		int num = Enum.GetNames(typeof(GamepadType)).Length;
		for (int i = 0; i < num; i++)
		{
			GamepadType gamepadType = (GamepadType)i;
			if (gs.LoadGamepadSettings(gamepadType))
			{
				gs.ResetGamepadSettings(gamepadType);
				gs.SaveGamepadSettings(gamepadType);
			}
		}
	}

	public void SendKeyBindingsToGameSettings()
	{
		gs.jumpKey = GetKeyBindingForAction(inputActions.jump).ToString();
		gs.attackKey = GetKeyBindingForAction(inputActions.attack).ToString();
		gs.dashKey = GetKeyBindingForAction(inputActions.dash).ToString();
		gs.castKey = GetKeyBindingForAction(inputActions.cast).ToString();
		gs.superDashKey = GetKeyBindingForAction(inputActions.superDash).ToString();
		gs.dreamNailKey = GetKeyBindingForAction(inputActions.dreamNail).ToString();
		gs.quickMapKey = GetKeyBindingForAction(inputActions.quickMap).ToString();
		gs.inventoryKey = GetKeyBindingForAction(inputActions.openInventory).ToString();
		gs.upKey = GetKeyBindingForAction(inputActions.up).ToString();
		gs.downKey = GetKeyBindingForAction(inputActions.down).ToString();
		gs.leftKey = GetKeyBindingForAction(inputActions.left).ToString();
		gs.rightKey = GetKeyBindingForAction(inputActions.right).ToString();
		gs.quickCastKey = GetKeyBindingForAction(inputActions.quickCast).ToString();
	}

	public void SendButtonBindingsToGameSettings()
	{
		gs.controllerMapping.jump = GetButtonBindingForAction(inputActions.jump);
		gs.controllerMapping.attack = GetButtonBindingForAction(inputActions.attack);
		gs.controllerMapping.dash = GetButtonBindingForAction(inputActions.dash);
		gs.controllerMapping.cast = GetButtonBindingForAction(inputActions.cast);
		gs.controllerMapping.superDash = GetButtonBindingForAction(inputActions.superDash);
		gs.controllerMapping.dreamNail = GetButtonBindingForAction(inputActions.dreamNail);
		gs.controllerMapping.quickMap = GetButtonBindingForAction(inputActions.quickMap);
		gs.controllerMapping.quickCast = GetButtonBindingForAction(inputActions.quickCast);
	}

	public void MapControllerButtons(GamepadType gamePadType)
	{
		inputActions.Reset();
		MapKeyboardLayoutFromGameSettings();
		if (!gs.LoadGamepadSettings(gamePadType))
		{
			gs.ResetGamepadSettings(gamePadType);
		}
		inputActions.jump.AddBinding(new DeviceBindingSource(gs.controllerMapping.jump));
		inputActions.attack.AddBinding(new DeviceBindingSource(gs.controllerMapping.attack));
		inputActions.dash.AddBinding(new DeviceBindingSource(gs.controllerMapping.dash));
		inputActions.cast.AddBinding(new DeviceBindingSource(gs.controllerMapping.cast));
		inputActions.superDash.AddBinding(new DeviceBindingSource(gs.controllerMapping.superDash));
		inputActions.dreamNail.AddBinding(new DeviceBindingSource(gs.controllerMapping.dreamNail));
		inputActions.quickMap.AddBinding(new DeviceBindingSource(gs.controllerMapping.quickMap));
		inputActions.quickCast.AddBinding(new DeviceBindingSource(gs.controllerMapping.quickCast));
		switch (gamePadType)
		{
			case GamepadType.XBOX_360:
				inputActions.openInventory.AddBinding(new DeviceBindingSource(InputControlType.Back));
				return;
			case GamepadType.PS4:
				inputActions.openInventory.AddBinding(new DeviceBindingSource(InputControlType.TouchPadButton));
				inputActions.pause.AddDefaultBinding(new DeviceBindingSource(InputControlType.Options));
				return;
		}
		if (activeGamepadType == GamepadType.XBOX_ONE)
		{
			inputActions.openInventory.AddBinding(new DeviceBindingSource(InputControlType.View));
			inputActions.openInventory.AddBinding(new DeviceBindingSource(InputControlType.Back));
			inputActions.pause.AddDefaultBinding(new DeviceBindingSource(InputControlType.Menu));
		}
		else if (gamePadType == GamepadType.PS3_WIN)
		{
			inputActions.openInventory.AddBinding(new DeviceBindingSource(InputControlType.Select));
		}
		else if (activeGamepadType == GamepadType.SWITCH_JOYCON_DUAL || activeGamepadType == GamepadType.SWITCH_PRO_CONTROLLER)
		{
			inputActions.openInventory.AddBinding(new DeviceBindingSource(InputControlType.Select));
			inputActions.pause.AddDefaultBinding(new DeviceBindingSource(InputControlType.Start));
		}
		else if (gamePadType == GamepadType.UNKNOWN)
		{
			inputActions.openInventory.AddBinding(new DeviceBindingSource(InputControlType.Select));
		}
	}

	public void RemapUIButtons()
	{
		inputActions.menuSubmit.ResetBindings();
		inputActions.menuCancel.ResetBindings();
		inputActions.paneLeft.ResetBindings();
		inputActions.paneRight.ResetBindings();
	}

	public PlayerAction ActionButtonToPlayerAction(HeroActionButton actionButtonType)
	{
		switch (actionButtonType)
		{
			case HeroActionButton.JUMP:
				return inputActions.jump;
			case HeroActionButton.ATTACK:
				return inputActions.attack;
			case HeroActionButton.DASH:
				return inputActions.dash;
			case HeroActionButton.CAST:
				return inputActions.cast;
			case HeroActionButton.SUPER_DASH:
				return inputActions.superDash;
			case HeroActionButton.QUICK_MAP:
				return inputActions.quickMap;
			case HeroActionButton.QUICK_CAST:
				return inputActions.quickCast;
			case HeroActionButton.INVENTORY:
				return inputActions.openInventory;
			case HeroActionButton.DREAM_NAIL:
				return inputActions.dreamNail;
			case HeroActionButton.UP:
				return inputActions.up;
			case HeroActionButton.DOWN:
				return inputActions.down;
			case HeroActionButton.LEFT:
				return inputActions.left;
			case HeroActionButton.RIGHT:
				return inputActions.right;
			case HeroActionButton.MENU_SUBMIT:
				return inputActions.menuSubmit;
			case HeroActionButton.MENU_CANCEL:
				return inputActions.menuCancel;
			case HeroActionButton.MENU_PANE_LEFT:
				return inputActions.paneLeft;
			case HeroActionButton.MENU_PANE_RIGHT:
				return inputActions.paneRight;
			default:
				Debug.Log("No PlayerAction could be matched to HeroActionButton: " + actionButtonType);
				return null;
		}
	}

	public KeyOrMouseBinding GetKeyBindingForAction(PlayerAction action)
	{
		if (inputActions.Actions.Contains(action))
		{
			int count = action.Bindings.Count;
			if (count == 0)
			{
				if (verboseMode)
				{
					Debug.LogFormat("{0} has no key or button bindings.", action.Name);
				}
				return new KeyOrMouseBinding(Key.None);
			}
			if (count == 1)
			{
				BindingSource bindingSource = action.Bindings[0];
				if (bindingSource.BindingSourceType == BindingSourceType.KeyBindingSource || bindingSource.BindingSourceType == BindingSourceType.MouseBindingSource)
				{
					return GetKeyBindingForActionBinding(action, action.Bindings[0]);
				}
				if (verboseMode)
				{
					Debug.LogFormat("{0} has no key bindings, only a single other binding ({1}: {2}).", action.Name, action.Bindings[0].BindingSourceType, action.Bindings[0].DeviceName);
				}
				return new KeyOrMouseBinding(Key.None);
			}
			if (count > 1)
			{
				foreach (BindingSource binding in action.Bindings)
				{
					if (binding.BindingSourceType == BindingSourceType.KeyBindingSource || binding.BindingSourceType == BindingSourceType.MouseBindingSource)
					{
						KeyOrMouseBinding keyBindingForActionBinding = GetKeyBindingForActionBinding(action, binding);
						if (!KeyOrMouseBinding.IsNone(keyBindingForActionBinding))
						{
							return keyBindingForActionBinding;
						}
					}
				}
				if (verboseMode)
				{
					Debug.LogFormat("This action has bindings but none are keyboard keys ({0})", action.Name);
				}
				return new KeyOrMouseBinding(Key.None);
			}
		}
		if (verboseMode)
		{
			Debug.LogFormat("This action is not in inputActions set. ({0})", action.Name);
		}
		return new KeyOrMouseBinding(Key.None);
	}

	private KeyOrMouseBinding GetKeyBindingForActionBinding(PlayerAction action, BindingSource bindingSource)
	{
		KeyBindingSource keyBindingSource = bindingSource as KeyBindingSource;
		if (keyBindingSource != null)
		{
			if (keyBindingSource.Control.Count == 0)
			{
				Debug.LogErrorFormat("This action has no key mapped but registered a key binding. ({0})", action.Name);
				return new KeyOrMouseBinding(Key.None);
			}
			if (keyBindingSource.Control.Count == 1)
			{
				return new KeyOrMouseBinding(keyBindingSource.Control.Get(0));
			}
			if (keyBindingSource.Control.Count > 1)
			{
				if (verboseMode)
				{
					Debug.LogFormat("This action has a KeyCombo mapped, this is unsupported ({0})", action.Name);
				}
				return new KeyOrMouseBinding(Key.None);
			}
			return new KeyOrMouseBinding(Key.None);
		}
		MouseBindingSource mouseBindingSource = bindingSource as MouseBindingSource;
		if (mouseBindingSource != null)
		{
			return new KeyOrMouseBinding(mouseBindingSource.Control);
		}
		Debug.LogErrorFormat("Keybinding Error - Action: {0} returned a null binding.", action.Name);
		return new KeyOrMouseBinding(Key.None);
	}

	public InputControlType GetButtonBindingForAction(PlayerAction action)
	{
		if (inputActions.Actions.Contains(action))
		{
			if (action.Bindings.Count > 0)
			{
				for (int i = 0; i < action.Bindings.Count; i++)
				{
					if (action.Bindings[i].BindingSourceType == BindingSourceType.DeviceBindingSource)
					{
						DeviceBindingSource deviceBindingSource = action.Bindings[i] as DeviceBindingSource;
						if (deviceBindingSource != null)
						{
							return deviceBindingSource.Control;
						}
					}
				}
				if (verboseMode)
				{
					string text = "|";
					for (int j = 0; j < action.Bindings.Count; j++)
					{
						text = text + action.Bindings[j].Name + "|";
					}
				}
				return InputControlType.None;
			}
			if (verboseMode)
			{
				Debug.Log("No bindings found for this action: " + action.Name);
			}
			return InputControlType.None;
		}
		if (verboseMode)
		{
			Debug.LogFormat("InputActions does not contain {0} as an action.", action.Name);
		}
		return InputControlType.None;
	}

	public PlayerAction GetActionForMappableControllerButton(InputControlType button)
	{
		for (int i = 0; i < mappableControllerActions.Count; i++)
		{
			PlayerAction playerAction = mappableControllerActions[i];
			if (GetButtonBindingForAction(playerAction) == button)
			{
				return playerAction;
			}
		}
		return null;
	}

	public PlayerAction GetActionForDefaultControllerButton(InputControlType button)
	{
		InputControl control = InputManager.ActiveDevice.GetControl(button);
		if (control != null)
		{
			if (verboseMode)
			{
				Debug.LogFormat("{0} button is mapped to {1}", button, control);
			}
		}
		else if (verboseMode)
		{
			Debug.LogFormat("{0} button is not mapped to anything", button);
		}
		return null;
	}

	public void PrintMappings(PlayerAction action)
	{
		if (inputActions.Actions.Contains(action))
		{
			foreach (BindingSource binding in action.Bindings)
			{
				if (binding.BindingSourceType == BindingSourceType.DeviceBindingSource)
				{
					DeviceBindingSource deviceBindingSource = (DeviceBindingSource)binding;
					Debug.LogFormat("{0} : {1} of type {2}", action.Name, deviceBindingSource.Control, binding.BindingSourceType);
				}
				else
				{
					Debug.LogFormat("{0} : {1} of type {2}", action.Name, binding.Name, binding.BindingSourceType);
				}
			}
			return;
		}
		Debug.Log("Action Not Found");
	}

	public string ActionButtonLocalizedKey(PlayerAction action)
	{
		return ActionButtonLocalizedKey(action.Name);
	}

	public string ActionButtonLocalizedKey(string actionName)
	{
		switch (actionName)
		{
			case "Jump":
				return "BUTTON_JUMP";
			case "Attack":
				return "BUTTON_ATTACK";
			case "Dash":
				return "BUTTON_DASH";
			case "Cast":
				return "BUTTON_CAST";
			case "Super Dash":
				return "BUTTON_SUPER_DASH";
			case "Quick Map":
				return "BUTTON_MAP";
			case "Quick Cast":
				return "BUTTON_QCAST";
			case "Inventory":
				return "BUTTON_INVENTORY";
			case "Move":
				return "BUTTON_MOVE";
			case "Look":
				return "BUTTON_LOOK";
			case "Pause":
				return "BUTTON_PAUSE";
			case "Dream Nail":
				return "BUTTON_DREAM_NAIL";
			default:
				Debug.Log("IH Unknown Key for action: " + actionName);
				return "unknownkey";
		}
	}

	private void StopCutsceneInput()
	{
		readyToSkipCutscene = false;
		gm.ui.HideCutscenePrompt();
	}

	private void UnlockStagInput()
	{
		stagLockoutActive = false;
	}

	private IEnumerator SetupGamepadUIInputActions()
	{
		if (gm.ui.menuState == MainMenuState.GAMEPAD_MENU)
		{
			yield return new WaitForSeconds(0.5f);
		}
		else
		{
			yield return new WaitForEndOfFrame();
		}
		switch (Platform.Current.AcceptRejectInputStyle)
		{
			case Platform.AcceptRejectInputStyles.NonJapaneseStyle:
				inputActions.menuSubmit.AddDefaultBinding(InputControlType.Action1);
				inputActions.menuCancel.AddDefaultBinding(InputControlType.Action2);
				break;
			case Platform.AcceptRejectInputStyles.JapaneseStyle:
				inputActions.menuSubmit.AddDefaultBinding(InputControlType.Action2);
				inputActions.menuCancel.AddDefaultBinding(InputControlType.Action1);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void RemoveGamepadUIInputActions()
	{
		inputActions.menuSubmit.RemoveBinding(new DeviceBindingSource(InputControlType.Action1));
		inputActions.menuSubmit.RemoveBinding(new DeviceBindingSource(InputControlType.Action2));
		inputActions.menuCancel.RemoveBinding(new DeviceBindingSource(InputControlType.Action1));
		inputActions.menuCancel.RemoveBinding(new DeviceBindingSource(InputControlType.Action2));
	}

	private void DestroyCurrentActionSet()
	{
		inputActions.Destroy();
	}

	public void SetActiveGamepadType(InputDevice inputDevice)
	{
		if (verboseMode)
		{
			Debug.LogFormat("Setting Active Input Device: {0} Meta: {1}", inputDevice.Name, inputDevice.Meta);
		}
		if (gamepadState == GamepadState.DETACHED)
		{
			return;
		}
		Debug.Log("Controller Type: " + inputDevice.DeviceStyle);
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.LinuxEditor)
		{
			switch (inputDevice.DeviceStyle)
			{
				case InputDeviceStyle.Xbox360:
					activeGamepadType = GamepadType.XBOX_360;
					break;
				case InputDeviceStyle.XboxOne:
					activeGamepadType = GamepadType.XBOX_ONE;
					break;
				case InputDeviceStyle.PlayStation3:
					activeGamepadType = GamepadType.PS3_WIN;
					break;
				case InputDeviceStyle.PlayStation4:
					activeGamepadType = GamepadType.PS4;
					break;
				case InputDeviceStyle.NintendoSwitch:
					activeGamepadType = GamepadType.SWITCH_PRO_CONTROLLER;
					break;
				default:
					Debug.LogError("Unable to match controller of name (" + inputDevice.Name + "), will attempt default mapping set.");
					activeGamepadType = GamepadType.XBOX_360;
					break;
			}
		}
		else
		{
			Debug.LogError("Unsupported platform for InputHander " + Application.platform);
			activeGamepadType = GamepadType.XBOX_360;
		}
		Debug.Log("Active Gamepad Type: " + activeGamepadType);
		MapControllerButtons(activeGamepadType);
		UpdateActiveController();
		SetupMappableControllerBindingsList();
		StartCoroutine(SetupGamepadUIInputActions());
	}

	private void MapDefaultKeyboardLayout()
	{
		inputActions.jump.AddBinding(new KeyBindingSource(Key.Z));
		inputActions.attack.AddBinding(new KeyBindingSource(Key.X));
		inputActions.dash.AddBinding(new KeyBindingSource(Key.C));
		inputActions.cast.AddBinding(new KeyBindingSource(Key.A));
		inputActions.superDash.AddBinding(new KeyBindingSource(Key.S));
		inputActions.dreamNail.AddBinding(new KeyBindingSource(Key.D));
		inputActions.quickMap.AddBinding(new KeyBindingSource(Key.Tab));
		inputActions.openInventory.AddBinding(new KeyBindingSource(Key.I));
		inputActions.quickCast.AddBinding(new KeyBindingSource(Key.F));
		inputActions.up.AddBinding(new KeyBindingSource(Key.UpArrow));
		inputActions.down.AddBinding(new KeyBindingSource(Key.DownArrow));
		inputActions.left.AddBinding(new KeyBindingSource(Key.LeftArrow));
		inputActions.right.AddBinding(new KeyBindingSource(Key.RightArrow));
	}

	private void MapKeyboardLayoutFromGameSettings()
	{
		AddKeyBinding(inputActions.jump, gs.jumpKey);
		AddKeyBinding(inputActions.attack, gs.attackKey);
		AddKeyBinding(inputActions.dash, gs.dashKey);
		AddKeyBinding(inputActions.cast, gs.castKey);
		AddKeyBinding(inputActions.superDash, gs.superDashKey);
		AddKeyBinding(inputActions.dreamNail, gs.dreamNailKey);
		AddKeyBinding(inputActions.quickMap, gs.quickMapKey);
		AddKeyBinding(inputActions.openInventory, gs.inventoryKey);
		AddKeyBinding(inputActions.quickCast, gs.quickCastKey);
		AddKeyBinding(inputActions.up, gs.upKey);
		AddKeyBinding(inputActions.down, gs.downKey);
		AddKeyBinding(inputActions.left, gs.leftKey);
		AddKeyBinding(inputActions.right, gs.rightKey);
	}

	private static void AddKeyBinding(PlayerAction action, string savedBinding)
	{
		Mouse result = Mouse.None;
		if (Enum.TryParse<Key>(savedBinding, out var result2) || Enum.TryParse<Mouse>(savedBinding, out result))
		{
			if (result != 0)
			{
				action.AddBinding(new MouseBindingSource(result));
				return;
			}
			action.AddBinding(new KeyBindingSource(result2));
		}
	}

	private void SetupNonMappableBindings()
	{
		inputActions = new HeroActions();
		inputActions.menuSubmit.AddDefaultBinding(Key.Return);
		inputActions.menuCancel.AddDefaultBinding(Key.Escape);
		inputActions.left.AddDefaultBinding(InputControlType.DPadLeft);
		inputActions.left.AddDefaultBinding(InputControlType.LeftStickLeft);
		inputActions.right.AddDefaultBinding(InputControlType.DPadRight);
		inputActions.right.AddDefaultBinding(InputControlType.LeftStickRight);
		inputActions.up.AddDefaultBinding(InputControlType.DPadUp);
		inputActions.up.AddDefaultBinding(InputControlType.LeftStickUp);
		inputActions.down.AddDefaultBinding(InputControlType.DPadDown);
		inputActions.down.AddDefaultBinding(InputControlType.LeftStickDown);
		inputActions.rs_up.AddDefaultBinding(InputControlType.RightStickUp);
		inputActions.rs_down.AddDefaultBinding(InputControlType.RightStickDown);
		inputActions.rs_left.AddDefaultBinding(InputControlType.RightStickLeft);
		inputActions.rs_right.AddDefaultBinding(InputControlType.RightStickRight);
		inputActions.textSpeedup.AddDefaultBinding(Key.Z);
		inputActions.textSpeedup.AddDefaultBinding(Key.Return);
		inputActions.textSpeedup.AddDefaultBinding(Key.Space);
		inputActions.textSpeedup.AddDefaultBinding(InputControlType.Action1);
		inputActions.skipCutscene.AddDefaultBinding(Key.Return);
		inputActions.skipCutscene.AddDefaultBinding(Key.Space);
		inputActions.skipCutscene.AddDefaultBinding(InputControlType.Action2);
		inputActions.pause.AddDefaultBinding(Key.Escape);
		inputActions.pause.AddDefaultBinding(InputControlType.Start);
		inputActions.paneRight.AddDefaultBinding(Key.RightBracket);
		inputActions.paneRight.AddDefaultBinding(InputControlType.RightTrigger);
		inputActions.paneRight.AddDefaultBinding(InputControlType.RightBumper);
		inputActions.paneLeft.AddDefaultBinding(Key.LeftBracket);
		inputActions.paneLeft.AddDefaultBinding(InputControlType.LeftTrigger);
		inputActions.paneLeft.AddDefaultBinding(InputControlType.LeftBumper);
		nonMappableControllerActions = new List<PlayerAction>();
		nonMappableControllerActions.Add(inputActions.paneLeft);
		nonMappableControllerActions.Add(inputActions.paneRight);
		nonMappableControllerActions.Add(inputActions.pause);
		nonMappableControllerActions.Add(inputActions.textSpeedup);
	}

	private void SetupMappableControllerBindingsList()
	{
		mappableControllerActions = new List<PlayerAction>();
		mappableControllerActions.Add(inputActions.jump);
		mappableControllerActions.Add(inputActions.attack);
		mappableControllerActions.Add(inputActions.dash);
		mappableControllerActions.Add(inputActions.cast);
		mappableControllerActions.Add(inputActions.superDash);
		mappableControllerActions.Add(inputActions.dreamNail);
		mappableControllerActions.Add(inputActions.quickMap);
		mappableControllerActions.Add(inputActions.quickCast);
		mappableControllerActions.Add(inputActions.openInventory);
		mappableControllerActions.Add(inputActions.up);
		mappableControllerActions.Add(inputActions.down);
		mappableControllerActions.Add(inputActions.left);
		mappableControllerActions.Add(inputActions.right);
	}
}
