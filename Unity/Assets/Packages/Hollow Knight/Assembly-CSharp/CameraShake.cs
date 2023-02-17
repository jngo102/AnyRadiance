using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private static List<CameraShake> cameraShakes;

	private PlayMakerFSM cameraShakeFSM;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private void Init()
	{
		cameraShakes = new List<CameraShake>();
	}

	protected void Awake()
	{
		cameraShakeFSM = PlayMakerFSM.FindFsmOnGameObject(base.gameObject, "CameraShake");
	}

	protected void OnEnable()
	{
		cameraShakes.Add(this);
	}

	protected void OnDisable()
	{
		cameraShakes.Remove(this);
	}

	public void ShakeSingle(CameraShakeCues cue)
	{
		if (cameraShakeFSM != null)
		{
			cameraShakeFSM.SendEvent(cue.ToString());
		}
	}

	public static void Shake(CameraShakeCues cue)
	{
		for (int i = 0; i < cameraShakes.Count; i++)
		{
			CameraShake cameraShake = cameraShakes[i];
			if (cameraShake != null)
			{
				cameraShake.ShakeSingle(cue);
			}
		}
	}
}
