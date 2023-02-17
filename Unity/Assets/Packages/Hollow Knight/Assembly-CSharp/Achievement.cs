using System;
using GlobalEnums;
using UnityEngine;

[Serializable]
public class Achievement
{
	public string key;

	public AchievementType type;

	public Sprite earnedIcon;

	public Sprite unearnedIcon;

	public string localizedText;

	public string localizedTitle;
}
