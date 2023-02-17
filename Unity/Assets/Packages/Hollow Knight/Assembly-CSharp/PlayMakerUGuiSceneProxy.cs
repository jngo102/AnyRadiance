using UnityEngine;

public class PlayMakerUGuiSceneProxy : MonoBehaviour
{
	public static PlayMakerFSM fsm;

	private void Start()
	{
		fsm = GetComponent<PlayMakerFSM>();
	}

	private void Update()
	{
	}
}
