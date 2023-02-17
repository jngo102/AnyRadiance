using UnityEngine;

public class UnlockGGMode : MonoBehaviour
{
	private void Start()
	{
		SetUnlocked();
	}

	public void SetUnlocked()
	{
		GameManager.instance.SetStatusRecordInt("RecBossRushMode", 1);
		GameManager.instance.SaveStatusRecords();
	}
}
