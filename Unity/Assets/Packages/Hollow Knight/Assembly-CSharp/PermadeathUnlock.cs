using UnityEngine;

public class PermadeathUnlock : MonoBehaviour
{
	private void Start()
	{
		GameManager.instance.SetStatusRecordInt("RecPermadeathMode", 1);
		GameManager.instance.SaveStatusRecords();
	}
}
