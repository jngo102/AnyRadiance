using UnityEngine;

public class MenuStyleUnlock : MonoBehaviour
{
	public string unlockKey = "";

	private void Start()
	{
		Unlock(unlockKey);
		if (GameManager.instance.GetPlayerDataInt("permadeathMode") == 1)
		{
			Unlock("steelSoulMenu");
		}
	}

	public static void Unlock(string key)
	{
		if (key != "" && Platform.Current.EncryptedSharedData.GetInt(key, 0) == 0)
		{
			Platform.Current.SharedData.SetString("unlockedMenuStyle", key);
			Platform.Current.EncryptedSharedData.SetInt(key, 1);
		}
	}
}
