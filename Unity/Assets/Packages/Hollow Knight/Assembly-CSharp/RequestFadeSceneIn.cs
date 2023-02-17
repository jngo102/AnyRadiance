using System.Collections;
using GlobalEnums;
using UnityEngine;

public class RequestFadeSceneIn : MonoBehaviour
{
	public float waitBeforeFade;

	public CameraFadeInType fadeInSpeed;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(waitBeforeFade);
		if (fadeInSpeed == CameraFadeInType.SLOW)
		{
			GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE SCENE IN SLOWLY");
		}
		else if (fadeInSpeed == CameraFadeInType.NORMAL)
		{
			GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE SCENE IN");
		}
		else if (fadeInSpeed == CameraFadeInType.INSTANT)
		{
			GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE SCENE IN INSTANT");
		}
	}
}
