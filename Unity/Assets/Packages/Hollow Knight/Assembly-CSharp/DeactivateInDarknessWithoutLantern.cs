using UnityEngine;

public class DeactivateInDarknessWithoutLantern : MonoBehaviour
{
	private GameManager gm;

	private PlayerData pd;

	private SceneManager sm;

	private void Start()
	{
		gm = GameManager.instance;
		pd = gm.playerData;
		sm = gm.sm;
		if (sm.darknessLevel == 2 && !pd.GetBool("hasLantern"))
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
