using Language;
using TMPro;
using UnityEngine;

public class AchievementListener : MonoBehaviour
{
	private GameManager gm;

	public SpriteRenderer icon;

	public TextMeshPro title;

	public TextMeshPro text;

	public PlayMakerFSM fsmToSendEvent;

	public string eventName;

	private void OnEnable()
	{
		if (!gm)
		{
			gm = GameManager.instance;
		}
		Debug.Log(base.name + " enabled, subscribing to AwardAchievement.");
		gm.achievementHandler.AwardAchievementEvent += CaptureAchievementEvent;
	}

	private void OnDisable()
	{
		gm.achievementHandler.AwardAchievementEvent -= CaptureAchievementEvent;
	}

	private void CaptureAchievementEvent(string achievementKey)
	{
		Debug.Log("*** Achievement Awarded! *** " + achievementKey);
		Achievement achievement = gm.achievementHandler.achievementsList.FindAchievement(achievementKey);
		icon.sprite = achievement.earnedIcon;
		title.text = global::Language.Language.Get(achievement.localizedTitle, "Achievements");
		text.text = global::Language.Language.Get(achievement.localizedText, "Achievements");
		if ((bool)fsmToSendEvent && !string.IsNullOrEmpty(eventName))
		{
			fsmToSendEvent.SendEvent(eventName);
		}
	}
}
