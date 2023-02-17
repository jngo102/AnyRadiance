using UnityEngine;

public class ActivateIfPlayerdataTrue : MonoBehaviour
{
	public string boolName;

	private GameManager gm;

	private PlayerData pd;

	private void Start()
	{
		gm = GameManager.instance;
		pd = gm.playerData;
		if (pd.GetBool(boolName))
		{
			base.gameObject.SetActive(value: true);
		}
	}
}
