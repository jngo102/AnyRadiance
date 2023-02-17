using Language;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class LocalizedTextMesh : MonoBehaviour
{
	public string keyValue;

	public void Awake()
	{
		LocalizeTextMesh(keyValue);
	}

	public void LocalizeTextMesh(string keyValue)
	{
		if (keyValue == null)
		{
			Debug.LogError("Please set the KeyValue that should be used for this TextMesh (" + base.name + ")");
		}
		else
		{
			base.gameObject.GetComponent<TextMesh>().text = global::Language.Language.Get(keyValue);
		}
	}
}
