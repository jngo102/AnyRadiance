using System.Collections.Generic;
using UnityEngine;

public class AchievementHandler : MonoBehaviour
{
	public delegate void AchievementAwarded(string key);

	private GameManager gm;

	public AchievementsList achievementsListPrefab;

	public Sprite hiddenAchievementIcon;

	private List<string> queuedAchievements = new List<string>();

	private Dictionary<string, List<string>> achievementWhiteLists = new Dictionary<string, List<string>> { 
	{
		"GODS_GLORY",
		new List<string> { "PANTHEON1", "PANTHEON2", "PANTHEON3", "PANTHEON4", "ENDINGD", "COMPLETIONGG" }
	} };

	public AchievementsList achievementsList { get; private set; }

	public event AchievementAwarded AwardAchievementEvent;

	private void Awake()
	{
		achievementsList = Object.Instantiate(achievementsListPrefab, base.transform);
	}

	private void Start()
	{
		gm = GameManager.instance;
	}

	public void AwardAchievementToPlayer(string key)
	{
		if (achievementsList.FindAchievement(key) != null)
		{
			if (!CanAwardAchievement(key) || Platform.Current.IsAchievementUnlocked(key).GetValueOrDefault())
			{
				return;
			}
			Platform.Current.PushAchievementUnlock(key);
			if (gm.gameSettings.showNativeAchievementPopups == 1)
			{
				if (this.AwardAchievementEvent != null)
				{
					this.AwardAchievementEvent(key);
				}
				else
				{
					Debug.LogError("AwardAchievement has no subscribers.");
				}
			}
		}
		else
		{
			Debug.LogError("No such achievement exists in the AchievementsList: " + key);
		}
	}

	public bool AchievementWasAwarded(string key)
	{
		if (achievementsList.FindAchievement(key) != null)
		{
			return Platform.Current.IsAchievementUnlocked(key).GetValueOrDefault();
		}
		Debug.LogError("No such achievement exists in AchievementsList: " + key);
		return false;
	}

	public void ResetAllAchievements()
	{
		Platform.Current.ResetAchievements();
	}

	public void FlushRecordsToDisk()
	{
		Platform.Current.SharedData.Save();
	}

	public void QueueAchievement(string key)
	{
		if (!queuedAchievements.Contains(key))
		{
			queuedAchievements.Add(key);
		}
	}

	public void AwardQueuedAchievements()
	{
		foreach (string queuedAchievement in queuedAchievements)
		{
			AwardAchievementToPlayer(queuedAchievement);
		}
		queuedAchievements.Clear();
	}

	public void AwardAllAchievements()
	{
		foreach (Achievement achievement in achievementsList.achievements)
		{
			AwardAchievementToPlayer(achievement.key);
		}
	}

	private bool CanAwardAchievement(string key)
	{
		if ((bool)GameManager.instance)
		{
			string currentMapZone = GameManager.instance.GetCurrentMapZone();
			if (achievementWhiteLists.ContainsKey(currentMapZone))
			{
				if (achievementWhiteLists[currentMapZone].Contains(key))
				{
					return true;
				}
				Debug.LogWarning($"Achievement <b>{key}</b> can not be awarded in map zone <b>{currentMapZone}</b>");
				return false;
			}
		}
		return true;
	}
}
