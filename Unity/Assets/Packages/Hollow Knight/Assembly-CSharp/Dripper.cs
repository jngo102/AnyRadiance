using System.Collections;
using UnityEngine;

public class Dripper : MonoBehaviour
{
	public GameObject spatterPrefab;

	private Coroutine routine;

	private void OnEnable()
	{
		if ((bool)HeroController.instance && (bool)spatterPrefab)
		{
			routine = StartCoroutine(Behaviour());
		}
	}

	private void OnDisable()
	{
		if (routine != null)
		{
			StopCoroutine(routine);
		}
	}

	private IEnumerator Behaviour()
	{
		transform.SetParent(HeroController.instance.transform);
		transform.localPosition = new Vector3(0f, -0.5f, 0.01f);
		yield return new WaitForSeconds(0.04f);
		WaitForSeconds frequency = new WaitForSeconds(0.025f);
		float elapsed = 0f;
		while (elapsed <= 0.4f)
		{
			yield return frequency;
			elapsed += 0.025f;
			FlingUtils.Config config = default(FlingUtils.Config);
			config.Prefab = spatterPrefab;
			config.SpeedMin = 0f;
			config.SpeedMax = 1f;
			config.AmountMin = 1;
			config.AmountMax = 1;
			config.AngleMin = 90f;
			config.AngleMax = 90f;
			config.OriginVariationX = 0.5f;
			config.OriginVariationY = 0.8f;
			FlingUtils.SpawnAndFling(config, transform, Vector3.zero);
		}
		routine = null;
		gameObject.Recycle();
	}
}
