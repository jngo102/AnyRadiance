using UnityEngine;

public class OpeningGameplayCredits : MonoBehaviour
{
	public Animator animator;

	private PlayerData pd;

	private void Start()
	{
		pd = PlayerData.instance;
		if (!pd.GetBool("openingCreditsPlayed"))
		{
			if ((bool)animator)
			{
				animator.SetBool("playCredits", value: true);
			}
			pd.SetBoolSwappedArgs(value: true, "openingCreditsPlayed");
		}
	}
}
