using UnityEngine;

public class BrummFlamePin : MonoBehaviour
{
	private GameManager gm;

	private PlayerData pd;

	private void Start()
	{
		gm = GameManager.instance;
		pd = gm.playerData;
	}

	private void OnEnable()
	{
		base.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		if (gm == null)
		{
			gm = GameManager.instance;
		}
		if (pd == null)
		{
			pd = gm.playerData;
		}
		if (pd.GetInt("flamesCollected") >= pd.GetInt("flamesRequired") || pd.GetBool("gotBrummsFlame") || !pd.GetBool("equippedCharm_40") || pd.GetInt("grimmChildLevel") != 3)
		{
			base.gameObject.SetActive(value: false);
		}
		base.gameObject.GetComponent<SpriteRenderer>().enabled = true;
	}
}
