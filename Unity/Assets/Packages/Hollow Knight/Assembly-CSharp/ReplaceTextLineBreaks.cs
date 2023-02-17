using TMPro;
using UnityEngine;

public class ReplaceTextLineBreaks : MonoBehaviour
{
	private TextMeshPro textMesh;

	private void Start()
	{
		textMesh = GetComponent<TextMeshPro>();
		Debug.Log(textMesh.text);
		Debug.Break();
		string text = textMesh.text;
		text = text.Replace("<br>", "\n");
		textMesh.text = text;
		Debug.Log(text);
		Debug.Log(textMesh.text);
	}
}
