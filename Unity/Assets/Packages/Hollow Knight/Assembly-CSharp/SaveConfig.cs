using System;
using UnityEngine;

[Serializable]
public class SaveConfig : ScriptableObject
{
	public enum SaveSet
	{
		Default,
		Test,
		Full
	}

	[SerializeField]
	public SaveSet saveToUse;

	[SerializeField]
	public PlayerData defaultPlayerData;

	[SerializeField]
	public PlayerData testPlayerData;

	[SerializeField]
	public PlayerData fullPlayerData;
}
