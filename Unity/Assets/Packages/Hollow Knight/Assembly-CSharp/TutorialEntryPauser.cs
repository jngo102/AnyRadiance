using UnityEngine;

public class TutorialEntryPauser : MonoBehaviour
{
	private GameManager gm;

	private PlayerData pd;

	private HeroController hc;

	private void Start()
	{
		gm = GameManager.instance;
		pd = PlayerData.instance;
		hc = HeroController.instance;
		if ((bool)hc)
		{
			if (!pd.GetBool("openingCreditsPlayed") && !pd.GetBool("visitedDirtmouth"))
			{
				hc.enterWithoutInput = true;
				hc.IgnoreInput();
				hc.FaceRight();
				if (pd != null)
				{
					pd.SetBoolSwappedArgs(value: true, "disablePause");
				}
			}
			else if (gm.entryGateName == "top1")
			{
				hc.enterWithoutInput = true;
				hc.IgnoreInput();
				hc.FaceRight();
				Invoke("EnableControl", 3f);
			}
		}
		else
		{
			Debug.LogError("Entry Pauser could not find hero");
		}
	}

	private void EnableControl()
	{
		hc.enterWithoutInput = false;
		hc.AcceptInput();
	}
}
