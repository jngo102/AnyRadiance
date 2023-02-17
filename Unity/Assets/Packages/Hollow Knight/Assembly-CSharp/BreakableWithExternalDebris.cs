using System;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWithExternalDebris : Breakable
{
	[Serializable]
	public struct ExternalDebris
	{
		public GameObject Prefab;

		public int Count;
	}

	[Serializable]
	public class WeightedExternalDebrisItem : WeightedItem
	{
		public ExternalDebris Value;
	}

	[SerializeField]
	private float debrisPrefabPositionVariance;

	[SerializeField]
	private ExternalDebris[] externalDebris;

	[SerializeField]
	private WeightedExternalDebrisItem[] externalDebrisVariants;

	private static List<IExternalDebris> externalDebrisResponders = new List<IExternalDebris>();

	protected override void CreateAdditionalDebrisParts(List<GameObject> debrisParts)
	{
		base.CreateAdditionalDebrisParts(debrisParts);
		for (int i = 0; i < externalDebris.Length; i++)
		{
			Spawn(externalDebris[i], debrisParts);
		}
		WeightedExternalDebrisItem weightedExternalDebrisItem = externalDebrisVariants.SelectValue();
		if (weightedExternalDebrisItem != null)
		{
			Spawn(weightedExternalDebrisItem.Value, debrisParts);
		}
	}

	private void Spawn(ExternalDebris externalDebris, List<GameObject> debrisParts)
	{
		for (int i = 0; i < externalDebris.Count; i++)
		{
			if (!(externalDebris.Prefab == null))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(externalDebris.Prefab);
				gameObject.GetComponents(externalDebrisResponders);
				for (int j = 0; j < externalDebrisResponders.Count; j++)
				{
					externalDebrisResponders[j].InitExternalDebris();
				}
				externalDebrisResponders.Clear();
				gameObject.transform.position = base.transform.position + new Vector3(UnityEngine.Random.Range(0f - debrisPrefabPositionVariance, debrisPrefabPositionVariance), UnityEngine.Random.Range(0f - debrisPrefabPositionVariance, debrisPrefabPositionVariance), 0f);
				gameObject.SetActive(value: false);
				debrisParts.Add(gameObject);
			}
		}
	}
}
