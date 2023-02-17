using InControl;
using Language;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ControllerButtonLabel : MonoBehaviour
{
	[Header("Button Text")]
	private Text buttonText;

	[Header("Button Label")]
	public string overrideLabelKey;

	[Header("Button Mapping")]
	public InputControlType controllerButton;

	private InputHandler ih;

	private UIManager ui;

	private void Awake()
	{
		ih = GameManager.instance.inputHandler;
		ui = UIManager.instance;
		buttonText = GetComponent<Text>();
	}

	private void OnEnable()
	{
		if (!string.IsNullOrEmpty(overrideLabelKey))
		{
			buttonText.text = global::Language.Language.Get(overrideLabelKey, "MainMenu");
		}
		else
		{
			ShowCurrentBinding();
		}
	}

	private void ShowCurrentBinding()
	{
		buttonText.text = "+";
		if (controllerButton != 0)
		{
			PlayerAction actionForMappableControllerButton = ih.GetActionForMappableControllerButton(controllerButton);
			if (actionForMappableControllerButton != null)
			{
				buttonText.text = global::Language.Language.Get(ih.ActionButtonLocalizedKey(actionForMappableControllerButton), "MainMenu");
				return;
			}
			actionForMappableControllerButton = ih.GetActionForDefaultControllerButton(controllerButton);
			if (actionForMappableControllerButton != null)
			{
				buttonText.text = global::Language.Language.Get(ih.ActionButtonLocalizedKey(actionForMappableControllerButton), "MainMenu");
			}
			else
			{
				buttonText.text = global::Language.Language.Get("CTRL_UNMAPPED", "MainMenu");
			}
		}
		else
		{
			buttonText.text = global::Language.Language.Get("CTRL_UNMAPPED", "MainMenu");
		}
	}
}
