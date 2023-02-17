using System;
using UnityEngine;

public class ForceCameraAspect : MonoBehaviour
{
	private tk2dCamera tk2dCam;

	private Camera hudCam;

	private int lastX;

	private int lastY;

	private float scaleAdjust;

	public static float CurrentViewportAspect { get; private set; }

	public static event Action<float> ViewportAspectChanged;

	private void Awake()
	{
		tk2dCam = GetComponent<tk2dCamera>();
		CurrentViewportAspect = 1.77777779f;
	}

	private void Start()
	{
		hudCam = GameCameras.instance.hudCamera;
		AutoScaleViewport();
	}

	private void Update()
	{
		if (lastX != Screen.width || lastY != Screen.height)
		{
			float num = AutoScaleViewport();
			lastX = Screen.width;
			lastY = Screen.height;
			if (ForceCameraAspect.ViewportAspectChanged != null)
			{
				ForceCameraAspect.ViewportAspectChanged(num);
			}
			CurrentViewportAspect = num;
		}
	}

	public void SetOverscanViewport(float adjustment)
	{
		scaleAdjust = adjustment;
		AutoScaleViewport();
	}

	private float AutoScaleViewport()
	{
		float num = (float)Screen.width / (float)Screen.height / 1.77777779f;
		float num2 = 1f + scaleAdjust;
		Rect rect = tk2dCam.CameraSettings.rect;
		if (num < 1f)
		{
			rect.width = 1f * num2;
			rect.height = num * num2;
			float num4 = (rect.x = (1f - rect.width) / 2f);
			float num6 = (rect.y = (1f - rect.height) / 2f);
		}
		else
		{
			float num7 = 1f / num;
			rect.width = num7 * num2;
			rect.height = 1f * num2;
			float num9 = (rect.x = (1f - rect.width) / 2f);
			float num11 = (rect.y = (1f - rect.height) / 2f);
		}
		tk2dCam.CameraSettings.rect = rect;
		hudCam.rect = rect;
		return 1.77777779f;
	}
}
