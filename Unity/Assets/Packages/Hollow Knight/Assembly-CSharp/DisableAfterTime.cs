using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
	public float waitTime = 5f;

	private float disableTime;

	public string sendEvent;

	private void OnEnable()
	{
		disableTime = Time.time + waitTime;
	}

	private void Update()
	{
		if (Time.time >= disableTime)
		{
			if (sendEvent != "")
			{
				FSMUtility.SendEventToGameObject(base.gameObject, sendEvent);
			}
			else
			{
				base.gameObject.SetActive(value: false);
			}
		}
	}
}
