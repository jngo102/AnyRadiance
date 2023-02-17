using Language;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCurrentLanguage : MonoBehaviour
{
	public Text textObject;

	public string replaceText = "({0})";

	private void Awake()
	{
		if (!textObject)
		{
			textObject = GetComponent<Text>();
		}
	}

	private void OnEnable()
	{
		if ((bool)textObject)
		{
			string text = global::Language.Language.CurrentLanguage().ToString();
			string arg = global::Language.Language.Get("LANG_" + text, "MainMenu");
			textObject.text = string.Format(replaceText, arg);
		}
	}
}
