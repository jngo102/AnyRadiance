using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AchievementsList : MonoBehaviour
{
	public List<Achievement> achievements = new List<Achievement>();

	public Achievement FindAchievement(string key)
	{
		for (int i = 0; i < achievements.Count; i++)
		{
			if (string.Equals(achievements[i].key, key))
			{
				return achievements[i];
			}
		}
		return null;
	}

	public bool AchievementExists(string key)
	{
		for (int i = 0; i < achievements.Count; i++)
		{
			if (string.Equals(achievements[i].key, key))
			{
				return true;
			}
		}
		return false;
	}
}
