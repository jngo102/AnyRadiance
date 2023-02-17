using TMPro;
using UnityEngine;

public class DisplayItemAmount : MonoBehaviour
{
	public string playerDataInt;

	public TextMeshPro textObject;

	private PlayerData playerData;

	private void OnEnable()
	{
		if (playerData == null)
		{
			playerData = PlayerData.instance;
		}
		string text = playerData.GetInt(playerDataInt).ToString();
		textObject.text = text;
	}
}
