using GlobalEnums;
using InControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ControllerDetect : MonoBehaviour
{
	private bool verboseMode;

	private GameManager gm;

	private UIManager ui;

	private InputHandler ih;

	private Image controllerImage;

	[Header("Controller Menu Items")]
	public CanvasGroup controllerPrompt;

	public CanvasGroup remapDialog;

	public CanvasGroup menuControls;

	public CanvasGroup remapControls;

	[Header("Controller Menu Preselect")]
	public Selectable controllerMenuPreselect;

	public Selectable remapMenuPreselect;

	[Header("Remap Menu Controls")]
	public MenuSelectable remapApplyButton;

	public MenuSelectable defaultsButton;

	[Header("Controller Menu Controls")]
	public MenuButton applyButton;

	public MenuButton remapButton;

	[SerializeField]
	public ControllerImage[] controllerImages;

	private float profileYPos;

	private void Awake()
	{
		gm = GameManager.instance;
		ih = gm.inputHandler;
		ui = UIManager.instance;
		controllerImage = GetComponent<Image>();
		profileYPos = GetComponent<RectTransform>().anchoredPosition.y;
	}

	private void OnEnable()
	{
		LookForActiveController();
		Debug.LogFormat("Subscribing to controller detection.");
		InputManager.OnActiveDeviceChanged += ControllerActivated;
		InputManager.OnDeviceAttached += ControllerAttached;
		InputManager.OnDeviceDetached += ControllerDetached;
	}

	private void OnDisable()
	{
		Debug.LogFormat("Unsubscribing from controller detection.");
		InputManager.OnActiveDeviceChanged -= ControllerActivated;
		InputManager.OnDeviceAttached -= ControllerAttached;
		InputManager.OnDeviceDetached -= ControllerDetached;
	}

	private void ControllerActivated(InputDevice inputDevice)
	{
		Debug.LogFormat("CD - Controller Activated: {0} : {1}", ih.gamepadState, ih.activeGamepadType);
		LookForActiveController();
	}

	private void ControllerAttached(InputDevice inputDevice)
	{
		Debug.LogFormat("CD - Controller Attached: {0} : {1}", ih.gamepadState, ih.activeGamepadType);
		LookForActiveController();
	}

	private void ControllerDetached(InputDevice inputDevice)
	{
		Debug.LogFormat("CD - Controller Detached: {0} : {1}", ih.gamepadState, ih.activeGamepadType);
		LookForActiveController();
		if (EventSystem.current != applyButton)
		{
			applyButton.Select();
		}
	}

	private void ShowController(GamepadType gamepadType)
	{
		GamepadType gamepadType2 = ((gamepadType != GamepadType.PS3_WIN) ? gamepadType : GamepadType.PS4);
		for (int i = 0; i < controllerImages.Length; i++)
		{
			if (controllerImages[i].gamepadType == gamepadType2)
			{
				controllerImage.sprite = controllerImages[i].sprite;
				if (controllerImages[i].buttonPositions != null)
				{
					controllerImages[i].buttonPositions.gameObject.SetActive(value: true);
				}
				base.transform.localScale = new Vector3(controllerImages[i].displayScale, controllerImages[i].displayScale, 1f);
				RectTransform component = GetComponent<RectTransform>();
				Vector2 anchoredPosition = component.anchoredPosition;
				anchoredPosition.y = profileYPos + controllerImages[i].offsetY;
				component.anchoredPosition = anchoredPosition;
			}
			else if (controllerImages[i].buttonPositions != null)
			{
				controllerImages[i].buttonPositions.gameObject.SetActive(value: false);
			}
		}
	}

	private void HideButtonLabels()
	{
		for (int i = 0; i < controllerImages.Length; i++)
		{
			if (controllerImages[i].buttonPositions != null)
			{
				controllerImages[i].buttonPositions.gameObject.SetActive(value: false);
			}
		}
	}

	private void LookForActiveController()
	{
		if (ih.gamepadState == GamepadState.DETACHED)
		{
			HideButtonLabels();
			controllerImage.sprite = controllerImages[0].sprite;
			ui.ShowCanvasGroup(controllerPrompt);
			remapButton.gameObject.SetActive(value: false);
		}
		else if (ih.activeGamepadType != 0)
		{
			ui.HideCanvasGroup(controllerPrompt);
			remapButton.gameObject.SetActive(value: true);
			ShowController(ih.activeGamepadType);
		}
	}
}
