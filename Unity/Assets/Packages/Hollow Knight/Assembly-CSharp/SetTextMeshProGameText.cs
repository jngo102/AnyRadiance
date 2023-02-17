using Language;
using TMPro;
using UnityEngine;

public class SetTextMeshProGameText : MonoBehaviour
{
	private TextMeshPro textMesh;

	public string sheetName;

	public string convName;

	private void Awake()
	{
		textMesh = GetComponent<TextMeshPro>();
	}

	private void Start()
	{
		UpdateText();
	}

	public void UpdateText()
	{
		if ((bool)textMesh)
		{
			textMesh.text = global::Language.Language.Get(convName, sheetName);
			string text = textMesh.text;
			text = text.Replace("<br>", "\n");
			textMesh.text = text;
		}
	}

	private void ChangedLanguage(LanguageCode code)
	{
		UpdateText();
	}
}
