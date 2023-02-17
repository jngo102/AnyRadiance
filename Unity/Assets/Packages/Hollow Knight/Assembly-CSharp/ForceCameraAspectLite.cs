using UnityEngine;

public class ForceCameraAspectLite : MonoBehaviour
{
	public Camera sceneCamera;

	private bool viewportChanged;

	private int lastX;

	private int lastY;

	private float scaleAdjust;

	private void Start()
	{
		AutoScaleViewport();
	}

	private void Update()
	{
		viewportChanged = false;
		if (lastX != Screen.width)
		{
			viewportChanged = true;
		}
		if (lastY != Screen.height)
		{
			viewportChanged = true;
		}
		if (viewportChanged)
		{
			AutoScaleViewport();
		}
		lastX = Screen.width;
		lastY = Screen.height;
	}

	private void AutoScaleViewport()
	{
		float num = (float)Screen.width / (float)Screen.height / 1.77777779f;
		float num2 = 1f + scaleAdjust;
		Rect rect = sceneCamera.rect;
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
		sceneCamera.rect = rect;
	}
}
