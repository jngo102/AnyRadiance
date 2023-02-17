using UnityEngine;

public class BetaGateChanger : MonoBehaviour
{
	public TransitionPoint[] gates;

	public void SwitchToBetaExit()
	{
		for (int i = 0; i < gates.Length; i++)
		{
			gates[i].targetScene = "BetaEnd";
		}
	}
}
