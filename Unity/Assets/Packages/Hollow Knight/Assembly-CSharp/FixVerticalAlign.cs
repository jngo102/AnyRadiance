using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Text))]
public class FixVerticalAlign : MonoBehaviour
{
	public enum LabelFixType
	{
		Normal,
		KeyMap
	}

	private Text text;

	public LabelFixType labelFixType;

	private void OnEnable()
	{
		if (labelFixType == LabelFixType.Normal)
		{
			AlignText();
		}
		else if (labelFixType == LabelFixType.KeyMap)
		{
			AlignTextKeymap();
		}
	}

	private void Start()
	{
		if (labelFixType == LabelFixType.Normal)
		{
			AlignText();
		}
		else if (labelFixType == LabelFixType.KeyMap)
		{
			AlignTextKeymap();
		}
	}

	public void AlignText()
	{
		text = GetComponent<Text>();
		if (!string.IsNullOrEmpty(text.text))
		{
			if (text.text[text.text.Length - 1] != '\n')
			{
				text.text += "\n";
			}
			text.lineSpacing = -0.33f;
		}
	}

	public void AlignTextKeymap()
	{
		text = GetComponent<Text>();
		if (!string.IsNullOrEmpty(text.text))
		{
			if (text.text[text.text.Length - 1] != '\n')
			{
				text.text += "\n";
			}
			text.lineSpacing = -0.05f;
		}
	}
}
