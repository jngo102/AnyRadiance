using UnityEngine;

public class CameraControlAnimationEvents : MonoBehaviour
{
	public void BigShake()
	{
		SendCameraEvent("BigShake");
	}

	public void SmallShake()
	{
		SendCameraEvent("SmallShake");
	}

	public void AverageShake()
	{
		SendCameraEvent("AverageShake");
	}

	public void EnemyKillShake()
	{
		SendCameraEvent("EnemyKillShake");
	}

	public void SmallRumble()
	{
		SetCameraBool("RumblingSmall", value: true);
	}

	public void MedRumble()
	{
		SetCameraBool("RumblingMed", value: true);
	}

	public void BigRumble()
	{
		SetCameraBool("RumblingBig", value: true);
	}

	public void StopRumble()
	{
		SetCameraBool("RumblingSmall", value: false);
		SetCameraBool("RumblingMed", value: false);
		SetCameraBool("RumblingBig", value: false);
	}

	private void SendCameraEvent(string eventName)
	{
		if (base.enabled)
		{
			GameCameras.instance.cameraShakeFSM.SendEvent(eventName);
		}
	}

	private void SetCameraBool(string boolName, bool value)
	{
		if (base.enabled)
		{
			GameCameras.instance.cameraShakeFSM.FsmVariables.FindFsmBool(boolName).Value = value;
		}
	}
}
