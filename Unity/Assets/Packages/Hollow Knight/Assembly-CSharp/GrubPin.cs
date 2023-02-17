using System.Collections.Generic;
using UnityEngine;

public class GrubPin : MonoBehaviour
{
	private PlayerData pd;

	private void Start()
	{
		pd = PlayerData.instance;
	}

	private void OnEnable()
	{
		if (pd == null)
		{
			pd = PlayerData.instance;
		}
		if (pd.GetVariable<List<string>>("scenesGrubRescued").Contains(base.name))
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
