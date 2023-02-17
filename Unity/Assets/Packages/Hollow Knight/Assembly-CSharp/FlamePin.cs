using System.Collections.Generic;
using UnityEngine;

public class FlamePin : MonoBehaviour
{
	private PlayerData pd;

	public float level;

	private void Start()
	{
		pd = PlayerData.instance;
	}

	private void OnEnable()
	{
		base.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		if (pd == null)
		{
			pd = PlayerData.instance;
		}
		if (pd.GetVariable<List<string>>("scenesFlameCollected").Contains(base.name) || (float)pd.GetInt("grimmChildLevel") != level || pd.GetInt("flamesCollected") >= pd.GetInt("flamesRequired") || !pd.GetBool("equippedCharm_40"))
		{
			base.gameObject.SetActive(value: false);
		}
		else
		{
			base.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
	}
}
