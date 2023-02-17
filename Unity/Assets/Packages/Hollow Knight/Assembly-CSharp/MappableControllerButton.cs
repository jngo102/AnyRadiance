// MappableControllerButton
using System.Collections.Generic;
using GlobalEnums;
using InControl;
using Language;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MappableControllerButton : MenuButton, ISubmitHandler, IEventSystemHandler, IPointerClickHandler, ICancelHandler
{
	private bool verboseMode;

	private GameManager gm;

	private InputHandler ih;

	private UIManager ui;

	private UIButtonSkins uibs;

	private GameSettings gs;

	private PlayerAction playerAction;

	private bool active;

	private bool isListening;

	private bool changePending;

	private InputControlType currentBinding;

	private List<DeviceBindingSource> unmappableButtons;

	[Space(6f)]
	[Header("Button Mapping")]
	public HeroActionButton actionButtonType;

	public Text buttonmapText;

	public Image buttonmapSprite;

	public Throbber listeningThrobber;

	private new void Start()
	{
		if (Application.isPlaying)
		{
			active = true;
			SetupRefs();
		}
	}

	private new void OnEnable()
	{
		if (Application.isPlaying)
		{
			if (!active)
			{
				Start();
			}
			GetBinding();
		}
	}

	private void GetBinding()
	{
		currentBinding = ih.GetButtonBindingForAction(playerAction);
	}

	public void ListenForNewButton()
	{
		buttonmapSprite.sprite = uibs.blankKey;
		buttonmapText.text = "";
		listeningThrobber.gameObject.SetActive(value: true);
		base.interactable = false;
		SetupBindingListenOptions();
		isListening = true;
		uibs.ListeningForButtonRebind(this);
		playerAction.ListenForBinding();
	}

	public void ShowCurrentBinding()
	{
		if (!active)
		{
			Start();
		}
		GetBinding();
		if (currentBinding == InputControlType.None)
		{
			buttonmapSprite.sprite = uibs.blankKey;
			buttonmapText.text = global::Language.Language.Get("CTRL_UNMAPPED", "MainMenu");
			listeningThrobber.gameObject.SetActive(value: false);
		}
		else
		{
			ButtonSkin controllerButtonSkinFor = uibs.GetControllerButtonSkinFor(playerAction);
			buttonmapSprite.sprite = controllerButtonSkinFor.sprite;
			buttonmapText.text = "";
			listeningThrobber.gameObject.SetActive(value: false);
			if (buttonmapSprite.sprite == null)
			{
				Debug.LogError("Could not find a suitable skin for the new button map: " + currentBinding);
				buttonmapSprite.sprite = uibs.blankKey;
			}
		}
		base.interactable = true;
	}

	public void AbortRebind()
	{
		if (isListening)
		{
			base.interactable = true;
			isListening = false;
		}
	}

	public void StopActionListening()
	{
		playerAction.StopListeningForBinding();
	}

	public bool OnBindingFound(PlayerAction action, BindingSource binding)
	{
		DeviceBindingSource deviceBindingSource = (DeviceBindingSource)binding;
		if (unmappableButtons.Contains(binding as DeviceBindingSource))
		{
			uibs.FinishedListeningForButton();
			action.StopListeningForBinding();
			AbortRebind();
			if (verboseMode)
			{
				Debug.LogFormat("Cancelled new {0} button binding (Not allowed to bind {1})", action.Name, deviceBindingSource.Control);
			}
			return false;
		}
		return true;
	}

	public void OnBindingAdded(PlayerAction action, BindingSource binding)
	{
		DeviceBindingSource deviceBindingSource = (DeviceBindingSource)binding;
		if (verboseMode)
		{
			Debug.Log("New binding added for " + action.Name + ": " + deviceBindingSource.Control);
		}
		uibs.FinishedListeningForButton();
		isListening = false;
		base.interactable = true;
		changePending = true;
		ih.RemapUIButtons();
	}

	public void OnBindingRejected(PlayerAction action, BindingSource binding, BindingSourceRejectionType rejection)
	{
		DeviceBindingSource deviceBindingSource = (DeviceBindingSource)binding;
		switch (rejection)
		{
			case BindingSourceRejectionType.DuplicateBindingOnAction:
				if (verboseMode)
				{
					Debug.LogFormat("{0}->{1} is already bound to {2}, cancelling rebind", deviceBindingSource.DeviceName, deviceBindingSource.Control, action.Name);
				}
				uibs.FinishedListeningForButton();
				AbortRebind();
				action.StopListeningForBinding();
				isListening = false;
				break;
			case BindingSourceRejectionType.DuplicateBindingOnActionSet:
				if (verboseMode)
				{
					string text = " |";
					for (int i = 0; i < action.Bindings.Count; i++)
					{
						text = text + action.Bindings[i].Name + "|";
					}
					text += "|";
					Debug.LogErrorFormat("{0}->{1} is already bound to another button: {2}", deviceBindingSource.DeviceName, deviceBindingSource.Control, text);
				}
				break;
			default:
				if (verboseMode)
				{
					Debug.Log("Binding rejected for " + action.Name + ": " + rejection);
				}
				uibs.FinishedListeningForButton();
				AbortRebind();
				action.StopListeningForBinding();
				isListening = false;
				break;
		}
	}

	public new void OnSubmit(BaseEventData eventData)
	{
		if (!isListening)
		{
			ListenForNewButton();
		}
	}

	public new void OnPointerClick(PointerEventData eventData)
	{
		OnSubmit(eventData);
	}

	public new void OnCancel(BaseEventData eventData)
	{
		if (isListening)
		{
			if (ih.lastActiveController == BindingSourceType.KeyBindingSource)
			{
				StopListeningForNewButton();
			}
		}
		else
		{
			base.OnCancel(eventData);
		}
	}

	private void StopListeningForNewButton()
	{
		uibs.FinishedListeningForButton();
		StopActionListening();
		AbortRebind();
	}

	private void SetupUnmappableButtons()
	{
		unmappableButtons = new List<DeviceBindingSource>();
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.DPadUp));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.DPadDown));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.DPadLeft));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.DPadRight));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.LeftStickUp));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.LeftStickDown));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.LeftStickLeft));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.LeftStickRight));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.RightStickUp));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.RightStickDown));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.RightStickLeft));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.RightStickRight));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.LeftStickButton));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.RightStickButton));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.Start));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.Select));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.Command));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.Back));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.Menu));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.Options));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.TouchPadButton));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.Options));
		unmappableButtons.Add(new DeviceBindingSource(InputControlType.Share));
	}

	private void SetupBindingListenOptions()
	{
		BindingListenOptions bindingListenOptions = new BindingListenOptions();
		bindingListenOptions.IncludeControllers = true;
		bindingListenOptions.IncludeNonStandardControls = false;
		bindingListenOptions.IncludeMouseButtons = false;
		bindingListenOptions.IncludeKeys = false;
		bindingListenOptions.IncludeModifiersAsFirstClassKeys = false;
		bindingListenOptions.IncludeUnknownControllers = false;
		bindingListenOptions.MaxAllowedBindingsPerType = 1u;
		bindingListenOptions.OnBindingFound = OnBindingFound;
		bindingListenOptions.OnBindingAdded = OnBindingAdded;
		bindingListenOptions.OnBindingRejected = OnBindingRejected;
		bindingListenOptions.UnsetDuplicateBindingsOnSet = true;
		ih.inputActions.ListenOptions = bindingListenOptions;
	}

	private void SetupRefs()
	{
		gm = GameManager.instance;
		ui = gm.ui;
		uibs = ui.uiButtonSkins;
		ih = gm.inputHandler;
		gs = gm.gameSettings;
		playerAction = ih.ActionButtonToPlayerAction(actionButtonType);
		HookUpAudioPlayer();
		SetupUnmappableButtons();
	}
}
