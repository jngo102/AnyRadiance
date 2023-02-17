using System;
using UnityEngine;

[Serializable]
public class GameVersionData : ScriptableObject
{
	[SerializeField]
	public GameVersion gameVersion;

	public string version;

	public string GetGameVersionString()
	{
		return gameVersion.major + "." + gameVersion.minor + "." + gameVersion.revision + "." + gameVersion.package;
	}
}
