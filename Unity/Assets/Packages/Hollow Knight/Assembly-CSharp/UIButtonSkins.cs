// UIButtonSkins
using System.Collections;
using GlobalEnums;
using InControl;
using UnityEngine;

public class UIButtonSkins : MonoBehaviour
{
	[Header("Empty Button")]
	public Sprite blankKey;

	[Header("Keyboard Button Skins")]
	public Sprite squareKey;

	public Sprite rectangleKey;

	public Sprite upArrowKey;

	public Sprite downArrowKey;

	public Sprite leftArrowKey;

	public Sprite rightArrowKey;

	[Space]
	public Sprite leftMouseButton;

	public Sprite rightMouseButton;

	public Sprite middleMouseButton;

	[Header("Default Font Settings")]
	public int labelFontSize;

	public TextAnchor labelAlignment;

	[Space(10f)]
	public int buttonFontSize;

	public TextAnchor buttonAlignment;

	[Space(10f)]
	public int wideButtonFontSize;

	public TextAnchor wideButtonAlignment;

	[Header("Universal Controller Buttons")]
	public Sprite a;

	public Sprite b;

	public Sprite x;

	public Sprite y;

	public Sprite lb;

	public Sprite lt;

	public Sprite rb;

	public Sprite rt;

	public Sprite start;

	public Sprite select;

	public Sprite dpadLeft;

	public Sprite dpadRight;

	public Sprite dpadUp;

	public Sprite dpadDown;

	[Header("XBone Controller Buttons")]
	public Sprite view;

	public Sprite menu;

	[Header("PS4 Controller Buttons")]
	public Sprite options;

	public Sprite share;

	public Sprite touchpadButton;

	public Sprite ps4x;

	public Sprite ps4square;

	public Sprite ps4triangle;

	public Sprite ps4circle;

	public Sprite ps4lb;

	public Sprite ps4lt;

	public Sprite ps4rb;

	public Sprite ps4rt;

	[Header("Switch HID Buttons")]
	[SerializeField]
	private Sprite switchHidB;

	[SerializeField]
	private Sprite switchHidA;

	[SerializeField]
	private Sprite switchHidY;

	[SerializeField]
	private Sprite switchHidX;

	[SerializeField]
	private Sprite switchHidLeftBumper;

	[SerializeField]
	private Sprite switchHidLeftTrigger;

	[SerializeField]
	private Sprite switchHidRightBumper;

	[SerializeField]
	private Sprite switchHidRightTrigger;

	[SerializeField]
	private Sprite switchHidMinus;

	[SerializeField]
	private Sprite switchHidPlus;

	[SerializeField]
	private Sprite switchHidDPadUp;

	[SerializeField]
	private Sprite switchHidDPadDown;

	[SerializeField]
	private Sprite switchHidDPadLeft;

	[SerializeField]
	private Sprite switchHidDPadRight;

	private bool active;

	private GameManager gm;

	private InputHandler ih;

	[Header("Keyboard Menu")]
	public RectTransform mappableKeyboardButtons;

	[Header("Controller Menu")]
	public RectTransform mappableControllerButtons;

	public MappableKey listeningKey { get; private set; }

	public MappableControllerButton listeningButton { get; private set; }

	private void Start()
	{
		SetupRefs();
	}

	private void OnEnable()
	{
		if (!active)
		{
			SetupRefs();
		}
	}

	public ButtonSkin GetButtonSkinFor(PlayerAction action)
	{
		switch (ih.lastActiveController)
		{
			case BindingSourceType.None:
			case BindingSourceType.KeyBindingSource:
			case BindingSourceType.MouseBindingSource:
				return GetButtonSkinFor(ih.GetKeyBindingForAction(action).ToString());
			case BindingSourceType.DeviceBindingSource:
				{
					InputControlType buttonBindingForAction = ih.GetButtonBindingForAction(action);
					return GetButtonSkinFor(buttonBindingForAction);
				}
			default:
				return null;
		}
	}

	public ButtonSkin GetKeyboardSkinFor(PlayerAction action)
	{
		return GetButtonSkinFor(ih.GetKeyBindingForAction(action).ToString());
	}

	public ButtonSkin GetControllerButtonSkinFor(PlayerAction action)
	{
		InputControlType buttonBindingForAction = ih.GetButtonBindingForAction(action);
		return GetButtonSkinFor(buttonBindingForAction);
	}

	public ButtonSkin GetButtonSkinFor(HeroActionButton actionButton)
	{
		if (ih == null)
		{
			Debug.LogWarning("Attempting to get button skins before the Input Handler is ready.", this);
			return GetButtonSkinFor(Key.None.ToString());
		}
		return GetButtonSkinFor(ih.ActionButtonToPlayerAction(actionButton));
	}

	public IEnumerator ShowCurrentKeyboardMappings()
	{
		MappableKey[] componentsInChildren = mappableKeyboardButtons.GetComponentsInChildren<MappableKey>();
		MappableKey[] array = componentsInChildren;
		foreach (MappableKey obj in array)
		{
			obj.GetBinding();
			obj.ShowCurrentBinding();
			yield return null;
		}
		yield return null;
	}

	public IEnumerator ShowCurrentButtonMappings()
	{
		MappableControllerButton[] actionButtons = mappableControllerButtons.GetComponentsInChildren<MappableControllerButton>();
		for (int i = 0; i < actionButtons.Length; i++)
		{
			actionButtons[i].ShowCurrentBinding();
			yield return null;
		}
		yield return null;
	}

	public void RefreshKeyMappings()
	{
		MappableKey[] componentsInChildren = mappableKeyboardButtons.GetComponentsInChildren<MappableKey>();
		foreach (MappableKey obj in componentsInChildren)
		{
			obj.GetBinding();
			obj.ShowCurrentBinding();
		}
	}

	public void RefreshButtonMappings()
	{
		MappableControllerButton[] componentsInChildren = mappableControllerButtons.GetComponentsInChildren<MappableControllerButton>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].ShowCurrentBinding();
		}
	}

	public void ListeningForKeyRebind(MappableKey mappableKey)
	{
		if (listeningKey == null)
		{
			listeningKey = mappableKey;
			return;
		}
		listeningKey.AbortRebind();
		listeningKey = mappableKey;
	}

	public void ListeningForButtonRebind(MappableControllerButton mappableButton)
	{
		if (listeningButton != null)
		{
			listeningButton.AbortRebind();
		}
		listeningButton = mappableButton;
	}

	public void FinishedListeningForKey()
	{
		listeningKey = null;
		RefreshKeyMappings();
	}

	public void FinishedListeningForButton()
	{
		listeningButton = null;
		StartCoroutine(ShowCurrentButtonMappings());
	}

	private ButtonSkin GetButtonSkinFor(InputControlType inputControlType)
	{
		ButtonSkin buttonSkin = new ButtonSkin(blankKey, "", ButtonSkinType.CONTROLLER);
		if (ih.activeGamepadType == GamepadType.PS4)
		{
			switch (inputControlType)
			{
				case InputControlType.Action1:
					buttonSkin.sprite = ps4x;
					break;
				case InputControlType.Action2:
					buttonSkin.sprite = ps4circle;
					break;
				case InputControlType.Action3:
					buttonSkin.sprite = ps4square;
					break;
				case InputControlType.Action4:
					buttonSkin.sprite = ps4triangle;
					break;
				case InputControlType.LeftBumper:
					buttonSkin.sprite = ps4lb;
					break;
				case InputControlType.LeftTrigger:
					buttonSkin.sprite = ps4lt;
					break;
				case InputControlType.RightBumper:
					buttonSkin.sprite = ps4rb;
					break;
				case InputControlType.RightTrigger:
					buttonSkin.sprite = ps4rt;
					break;
				case InputControlType.Start:
					buttonSkin.sprite = start;
					break;
				case InputControlType.Select:
					buttonSkin.sprite = select;
					break;
				case InputControlType.Options:
					buttonSkin.sprite = options;
					break;
				case InputControlType.Share:
					buttonSkin.sprite = share;
					break;
				case InputControlType.TouchPadButton:
					buttonSkin.sprite = touchpadButton;
					break;
				case InputControlType.DPadUp:
					buttonSkin.sprite = dpadUp;
					break;
				case InputControlType.DPadDown:
					buttonSkin.sprite = dpadDown;
					break;
				case InputControlType.DPadLeft:
					buttonSkin.sprite = dpadLeft;
					break;
				case InputControlType.DPadRight:
					buttonSkin.sprite = dpadRight;
					break;
			}
		}
		else if (ih.activeGamepadType == GamepadType.SWITCH_JOYCON_DUAL || ih.activeGamepadType == GamepadType.SWITCH_PRO_CONTROLLER)
		{
			switch (inputControlType)
			{
				case InputControlType.Action1:
					buttonSkin.sprite = switchHidB;
					break;
				case InputControlType.Action2:
					buttonSkin.sprite = switchHidA;
					break;
				case InputControlType.Action3:
					buttonSkin.sprite = switchHidY;
					break;
				case InputControlType.Action4:
					buttonSkin.sprite = switchHidX;
					break;
				case InputControlType.LeftBumper:
					buttonSkin.sprite = switchHidLeftBumper;
					break;
				case InputControlType.LeftTrigger:
					buttonSkin.sprite = switchHidLeftTrigger;
					break;
				case InputControlType.RightBumper:
					buttonSkin.sprite = switchHidRightBumper;
					break;
				case InputControlType.RightTrigger:
					buttonSkin.sprite = switchHidRightTrigger;
					break;
				case InputControlType.Select:
					buttonSkin.sprite = switchHidMinus;
					break;
				case InputControlType.Start:
					buttonSkin.sprite = switchHidPlus;
					break;
				case InputControlType.DPadUp:
					buttonSkin.sprite = switchHidDPadUp;
					break;
				case InputControlType.DPadDown:
					buttonSkin.sprite = switchHidDPadDown;
					break;
				case InputControlType.DPadLeft:
					buttonSkin.sprite = switchHidDPadLeft;
					break;
				case InputControlType.DPadRight:
					buttonSkin.sprite = switchHidDPadRight;
					break;
			}
		}
		else
		{
			switch (inputControlType)
			{
				case InputControlType.Action1:
					buttonSkin.sprite = a;
					break;
				case InputControlType.Action2:
					buttonSkin.sprite = b;
					break;
				case InputControlType.Action3:
					buttonSkin.sprite = x;
					break;
				case InputControlType.Action4:
					buttonSkin.sprite = y;
					break;
				case InputControlType.LeftBumper:
					buttonSkin.sprite = lb;
					break;
				case InputControlType.LeftTrigger:
					buttonSkin.sprite = lt;
					break;
				case InputControlType.RightBumper:
					buttonSkin.sprite = rb;
					break;
				case InputControlType.RightTrigger:
					buttonSkin.sprite = rt;
					break;
				case InputControlType.Start:
					buttonSkin.sprite = start;
					break;
				case InputControlType.Select:
					buttonSkin.sprite = select;
					break;
				case InputControlType.Back:
					buttonSkin.sprite = select;
					break;
				case InputControlType.View:
					buttonSkin.sprite = view;
					break;
				case InputControlType.Menu:
					buttonSkin.sprite = menu;
					break;
				case InputControlType.DPadUp:
					buttonSkin.sprite = dpadUp;
					break;
				case InputControlType.DPadDown:
					buttonSkin.sprite = dpadDown;
					break;
				case InputControlType.DPadLeft:
					buttonSkin.sprite = dpadLeft;
					break;
				case InputControlType.DPadRight:
					buttonSkin.sprite = dpadRight;
					break;
			}
		}
		return buttonSkin;
	}

	private ButtonSkin GetButtonSkinFor(string buttonName)
	{
		ButtonSkin buttonSkin = new ButtonSkin(blankKey, buttonName, ButtonSkinType.BLANK);
		if (buttonName.Length == 1)
		{
			if (char.IsLetter(buttonName[0]))
			{
				buttonSkin.sprite = squareKey;
				buttonSkin.symbol = buttonName;
				buttonSkin.skinType = ButtonSkinType.SQUARE;
			}
		}
		else
		{
			switch (buttonName)
			{
				case "Return":
					buttonSkin.sprite = rectangleKey;
					buttonSkin.symbol = "Enter";
					buttonSkin.skinType = ButtonSkinType.WIDE;
					break;
				case "Escape":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "Esc";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "UpArrow":
					buttonSkin.sprite = upArrowKey;
					buttonSkin.symbol = "";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "DownArrow":
					buttonSkin.sprite = downArrowKey;
					buttonSkin.symbol = "";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "LeftArrow":
					buttonSkin.sprite = leftArrowKey;
					buttonSkin.symbol = "";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "RightArrow":
					buttonSkin.sprite = rightArrowKey;
					buttonSkin.symbol = "";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "LeftShift":
					buttonSkin.sprite = rectangleKey;
					buttonSkin.symbol = "L Shift";
					buttonSkin.skinType = ButtonSkinType.WIDE;
					break;
				case "RightShift":
					buttonSkin.sprite = rectangleKey;
					buttonSkin.symbol = "R Shift";
					buttonSkin.skinType = ButtonSkinType.WIDE;
					break;
				case "LeftControl":
					buttonSkin.sprite = rectangleKey;
					buttonSkin.symbol = "L Ctrl";
					buttonSkin.skinType = ButtonSkinType.WIDE;
					break;
				case "RightControl":
					buttonSkin.sprite = rectangleKey;
					buttonSkin.symbol = "R Ctrl";
					buttonSkin.skinType = ButtonSkinType.WIDE;
					break;
				case "LeftAlt":
					buttonSkin.sprite = rectangleKey;
					buttonSkin.symbol = "L Alt";
					buttonSkin.skinType = ButtonSkinType.WIDE;
					break;
				case "RightAlt":
					buttonSkin.sprite = rectangleKey;
					buttonSkin.symbol = "R Alt";
					buttonSkin.skinType = ButtonSkinType.WIDE;
					break;
				case "Backquote":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "~";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Minus":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "-";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Equals":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "=";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Backspace":
					buttonSkin.sprite = rectangleKey;
					buttonSkin.skinType = ButtonSkinType.WIDE;
					break;
				case "Left Bracket":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "[";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Right Bracket":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "]";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Backslash":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "\\";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Semicolon":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = ";";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Quote":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "'";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Comma":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = ",";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Period":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = ".";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Slash":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "/";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Pad1":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "1";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Pad2":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "2";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Pad3":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "3";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Pad4":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "4";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Pad5":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "5";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Pad6":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "6";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Pad7":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "7";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Pad8":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "8";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Pad9":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "9";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Pad0":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "0";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "PadMultiply":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "*";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "PadDivide":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "/";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "PadMinus":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "-";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "PadPlus":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "+";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "PadPeriod":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = ".";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Tab":
					buttonSkin.sprite = rectangleKey;
					buttonSkin.skinType = ButtonSkinType.WIDE;
					break;
				case "Space":
					buttonSkin.sprite = rectangleKey;
					buttonSkin.skinType = ButtonSkinType.WIDE;
					break;
				case "Key1":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "1";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Key2":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "2";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Key3":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "3";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Key4":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "4";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Key5":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "5";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Key6":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "6";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Key7":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "7";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Key8":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "8";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Key9":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "9";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Key0":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "0";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "LeftBracket":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "[";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "RightBracket":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "]";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F1":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F2":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F3":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F4":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F5":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F6":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F7":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F8":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F9":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F10":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F11":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "F12":
					buttonSkin.sprite = squareKey;
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Insert":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "Ins";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Delete":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "Del";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "Home":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "Home";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "End":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "End";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "PageUp":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "PgUp";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "PageDown":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "PgDn";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "PadEnter":
					buttonSkin.sprite = squareKey;
					buttonSkin.symbol = "Ent";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "LeftButton":
					buttonSkin.sprite = leftMouseButton;
					buttonSkin.symbol = "";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "RightButton":
					buttonSkin.sprite = rightMouseButton;
					buttonSkin.symbol = "";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				case "MiddleButton":
					buttonSkin.sprite = middleMouseButton;
					buttonSkin.symbol = "";
					buttonSkin.skinType = ButtonSkinType.SQUARE;
					break;
				default:
					buttonSkin.skinType = ButtonSkinType.BLANK;
					buttonSkin.symbol = buttonName;
					break;
			}
		}
		return buttonSkin;
	}

	private void SetupRefs()
	{
		ih = GameManager.instance.inputHandler;
		active = true;
	}
}
