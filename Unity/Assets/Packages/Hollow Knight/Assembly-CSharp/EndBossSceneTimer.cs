using System.Collections;
using UnityEngine;

public class EndBossSceneTimer : MonoBehaviour
{
	[SerializeField]
	private float delay = 10f;

	private void OnEnable()
	{
		StartCoroutine(Delayed());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	private IEnumerator Delayed()
	{
		yield return new WaitForSeconds(delay);
		if ((bool)BossSceneController.Instance)
		{
			BossSceneController.Instance.EndBossScene();
		}
	}
}
