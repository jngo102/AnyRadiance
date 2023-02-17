// MappableKey
using System.Collections.Generic;
using GlobalEnums;
using InControl;
using Language;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MappableKey : MenuButton, ISubmitHandler, IEventSystemHandler, IPointerClickHandler, ICancelHandler
{
	private GameManager gm;

	private InputHandler ih;

	private UIManager ui;

	private UIButtonSkins uibs;

	private GameSettings gs;

	private PlayerAction playerAction;

	private bool active;

	private bool isListening;

	private bool changePending;

	private int oldFontSize;

	private TextAnchor oldAlignment;

	private Sprite oldSprite;

	private string oldText;

	private InputHandler.KeyOrMouseBinding currentBinding;

	private List<KeyBindingSource> unmappableKeys;

	private float sqrX = 32f;

	private float sqrWidth = 65f;

	private bool sqrBestFit = true;

	private int sqrFontSize = 46;

	private int sqrMinFont = 20;

	private int sqrMaxFont = 46;

	private HorizontalWrapMode sqrHOverflow;

	private TextAnchor sqrAlignment = TextAnchor.MiddleCenter;

	private float wideX;

	private float wideWidth = 137f;

	private bool wideBestFit;

	private int wideFontSize = 34;

	private HorizontalWrapMode wideHOverflow;

	private TextAnchor wideAlignment = TextAnchor.MiddleCenter;

	private float blankX;

	private float blankWidth = 162f;

	private bool blankBestFit;

	private int blankFontSize = 46;

	private HorizontalWrapMode blankOverflow = HorizontalWrapMode.Overflow;

	private TextAnchor blankAlignment = TextAnchor.MiddleRight;

	[Space(6f)]
	[Header("Button Mapping")]
	public HeroActionButton actionButtonType;

	public Text keymapText;

	public Image keymapSprite;

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

	public void GetBinding()
	{
		currentBinding = ih.GetKeyBindingForAction(playerAction);
	}

	public void ListenForNewButton()
	{
		playerAction.ClearBindings();
		oldFontSize = keymapText.fontSize;
		oldAlignment = keymapText.alignment;
		oldSprite = keymapSprite.sprite;
		oldText = keymapText.text;
		keymapSprite.sprite = uibs.blankKey;
		keymapText.text = global::Language.Language.Get("KEYBOARD_PRESSKEY", "MainMenu");
		keymapText.fontSize = blankFontSize;
		keymapText.alignment = blankAlignment;
		keymapText.horizontalOverflow = blankOverflow;
		base.interactable = false;
		SetupBindingListenOptions();
		isListening = true;
		uibs.ListeningForKeyRebind(this);
		playerAction.ListenForBinding();
	}

	public void ShowCurrentBinding()
	{
		if (!active)
		{
			Start();
		}
		if (InputHandler.KeyOrMouseBinding.IsNone(currentBinding))
		{
			keymapSprite.sprite = uibs.blankKey;
			keymapText.text = global::Language.Language.Get("KEYBOARD_UNMAPPED", "MainMenu");
			keymapText.fontSize = blankFontSize;
			keymapText.alignment = blankAlignment;
			keymapText.resizeTextForBestFit = blankBestFit;
			keymapText.horizontalOverflow = blankOverflow;
			keymapText.GetComponent<FixVerticalAlign>().AlignText();
		}
		else
		{
			ButtonSkin keyboardSkinFor = uibs.GetKeyboardSkinFor(playerAction);
			keymapSprite.sprite = keyboardSkinFor.sprite;
			keymapText.text = keyboardSkinFor.symbol;
			if (keyboardSkinFor.skinType == ButtonSkinType.SQUARE)
			{
				keymapText.fontSize = sqrFontSize;
				keymapText.alignment = sqrAlignment;
				keymapText.rectTransform.anchoredPosition = new Vector2(sqrX, keymapText.rectTransform.anchoredPosition.y);
				keymapText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sqrWidth);
				keymapText.resizeTextForBestFit = sqrBestFit;
				keymapText.resizeTextMinSize = sqrMinFont;
				keymapText.resizeTextMaxSize = sqrMaxFont;
				keymapText.horizontalOverflow = sqrHOverflow;
			}
			else if (keyboardSkinFor.skinType == ButtonSkinType.WIDE)
			{
				keymapText.fontSize = wideFontSize;
				keymapText.alignment = wideAlignment;
				keymapText.rectTransform.anchoredPosition = new Vector2(wideX, keymapText.rectTransform.anchoredPosition.y);
				keymapText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, wideWidth);
				keymapText.resizeTextForBestFit = wideBestFit;
				keymapText.horizontalOverflow = wideHOverflow;
			}
			else
			{
				keymapText.alignment = uibs.labelAlignment;
			}
			if (keymapSprite.sprite == null)
			{
				Debug.LogError("Could not find a suitable skin for the new button map: " + currentBinding);
				keymapSprite.sprite = uibs.blankKey;
			}
			keymapText.GetComponent<FixVerticalAlign>().AlignTextKeymap();
		}
		base.interactable = true;
	}

	public void AbortRebind()
	{
		if (isListening)
		{
			keymapText.text = oldText;
			keymapText.fontSize = oldFontSize;
			keymapText.alignment = oldAlignment;
			keymapSprite.sprite = oldSprite;
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
		if (unmappableKeys.Contains(binding as KeyBindingSource))
		{
			uibs.FinishedListeningForKey();
			action.StopListeningForBinding();
			AbortRebind();
			Debug.LogFormat("Cancelled new {0} button binding", action.Name);
			return false;
		}
		return true;
	}

	public void OnBindingAdded(PlayerAction action, BindingSource binding)
	{
		Debug.Log("New binding added for " + action.Name + ": " + binding.Name);
		isListening = false;
		base.interactable = true;
		changePending = true;
		uibs.FinishedListeningForKey();
	}

	public void OnBindingRejected(PlayerAction action, BindingSource binding, BindingSourceRejectionType rejection)
	{
		switch (rejection)
		{
			case BindingSourceRejectionType.DuplicateBindingOnAction:
				Debug.LogFormat("{0} is already bound to {1}, cancelling rebind", binding.Name, action.Name);
				uibs.FinishedListeningForKey();
				AbortRebind();
				action.StopListeningForBinding();
				isListening = false;
				break;
			case BindingSourceRejectionType.DuplicateBindingOnActionSet:
				Debug.LogErrorFormat("{0} is already bound to another key.", binding.Name);
				break;
			default:
				Debug.Log("Binding rejected for " + action.Name + ": " + rejection);
				uibs.FinishedListeningForKey();
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
			StopListeningForNewKey();
		}
		else
		{
			base.OnCancel(eventData);
		}
	}

	private void StopListeningForNewKey()
	{
		uibs.FinishedListeningForKey();
		StopActionListening();
		AbortRebind();
	}

	private void SetupUnmappableKeys()
	{
		unmappableKeys = new List<KeyBindingSource>();
		unmappableKeys.Add(new KeyBindingSource(Key.Escape));
		unmappableKeys.Add(new KeyBindingSource(Key.Return));
		unmappableKeys.Add(new KeyBindingSource(Key.Numlock));
		unmappableKeys.Add(new KeyBindingSource(Key.LeftCommand));
		unmappableKeys.Add(new KeyBindingSource(Key.RightCommand));
	}

	private void SetupBindingListenOptions()
	{
		BindingListenOptions bindingListenOptions = new BindingListenOptions();
		bindingListenOptions.IncludeControllers = false;
		bindingListenOptions.IncludeNonStandardControls = false;
		bindingListenOptions.IncludeMouseButtons = true;
		bindingListenOptions.IncludeKeys = true;
		bindingListenOptions.IncludeModifiersAsFirstClassKeys = true;
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
		SetupUnmappableKeys();
	}
}
