using System;
using Language;
using UnityEngine;
using UnityEngine.UI;

public class AutoLocalizeTextUI : MonoBehaviour
{
	[Tooltip("UI Text component to place text.")]
	public Text textField;

	[Tooltip("The page name to reference the text from.")]
	public string sheetTitle;

	[Tooltip("The key to look up.")]
	public string textKey;

	private GameManager gm;

	private FixVerticalAlign textAligner;

	private bool hasTextAligner;

	private void Awake()
	{
		textAligner = GetComponent<FixVerticalAlign>();
		if ((bool)textAligner)
		{
			hasTextAligner = true;
		}
	}

	private void OnEnable()
	{
		gm = GameManager.instance;
		if ((bool)gm)
		{
			gm.RefreshLanguageText += RefreshTextFromLocalization;
		}
		RefreshTextFromLocalization();
	}

	private void OnDisable()
	{
		if (gm != null)
		{
			gm.RefreshLanguageText -= RefreshTextFromLocalization;
		}
	}

	public void RefreshTextFromLocalization()
	{
		string text = global::Language.Language.Get(textKey, sheetTitle);
		text = text.Replace("\\n", Environment.NewLine);
		text = text.Replace("<br>", Environment.NewLine);
		textField.text = text;
		if (hasTextAligner)
		{
			textAligner.AlignText();
		}
	}
}
