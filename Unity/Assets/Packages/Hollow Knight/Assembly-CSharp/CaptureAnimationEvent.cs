using UnityEngine;

public class CaptureAnimationEvent : MonoBehaviour
{
	private PlayerData playerData;

	private void Start()
	{
		playerData = PlayerData.instance;
	}

	public void SetPlayerDataBoolTrue(string boolName)
	{
		playerData.SetBool(boolName, value: true);
	}

	public void SetPlayerDataBoolFalse(string boolName)
	{
		playerData.SetBool(boolName, value: false);
	}

	public void IncrementPlayerDataInt(string intName)
	{
		playerData.IncrementInt(intName);
	}

	public void DecrementPlayerDataInt(string intName)
	{
		playerData.DecrementInt(intName);
	}

	public bool GetPlayerDataBool(string boolName)
	{
		return playerData.GetBool(boolName);
	}

	public int GetPlayerDataInt(string intName)
	{
		return playerData.GetInt(intName);
	}

	public float GetPlayerDataFloat(string floatName)
	{
		return playerData.GetFloat(floatName);
	}

	public string GetPlayerDataString(string stringName)
	{
		return playerData.GetString(stringName);
	}

	public void EquipCharm(int charmNum)
	{
		playerData.EquipCharm(charmNum);
	}

	public void UnequipCharm(int charmNum)
	{
		playerData.UnequipCharm(charmNum);
	}

	public void UpdateBlueHealth()
	{
		playerData.UpdateBlueHealth();
	}
}
