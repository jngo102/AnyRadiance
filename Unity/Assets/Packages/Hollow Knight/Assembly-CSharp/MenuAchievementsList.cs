using System.Collections.Generic;
using UnityEngine;

public class MenuAchievementsList : MonoBehaviour
{
	public MenuAchievement menuAchievementPrefab;

	[SerializeField]
	private List<MenuAchievement> menuAchievementsList;

	[SerializeField]
	public bool init { get; private set; }

	public void AddMenuAchievement(MenuAchievement achievement)
	{
		menuAchievementsList.Add(achievement);
	}

	public MenuAchievement FindAchievement(string key)
	{
		for (int i = 0; i < menuAchievementsList.Count; i++)
		{
			if (menuAchievementsList[i].name == key)
			{
				return menuAchievementsList[i];
			}
		}
		return null;
	}

	public void MarkInit()
	{
		init = true;
	}
}
