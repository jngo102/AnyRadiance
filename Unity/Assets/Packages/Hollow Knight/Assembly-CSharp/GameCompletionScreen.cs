using GlobalEnums;
using TMPro;
using UnityEngine;

public class GameCompletionScreen : MonoBehaviour
{
	public TextMeshPro percentageNumber;

	public TextMeshPro playTimeNumber;

	private GameManager gm;

	private void Start()
	{
		gm = GameManager.instance;
		PlayerData playerData = gm.playerData;
		playerData.CountGameCompletion();
		SaveStats saveStats = new SaveStats(playerData.GetInt("maxHealthBase"), playerData.GetInt("geo"), playerData.GetVariable<MapZone>("mapZone"), playerData.GetFloat("playTime"), playerData.GetInt("MPReserveMax"), playerData.GetInt("permadeathMode"), playerData.GetBool("bossRushMode"), playerData.GetFloat("completionPercentage"), playerData.GetBool("unlockedCompletionRate"));
		percentageNumber.text = saveStats.GetCompletionPercentage();
		playTimeNumber.text = saveStats.GetPlaytimeHHMMSS();
	}
}
