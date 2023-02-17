using UnityEngine;

public class PlayMakerUGuiCanvasRaycastFilterEventsProxy : MonoBehaviour, ICanvasRaycastFilter
{
	public bool RayCastingEnabled = true;

	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
	{
		return RayCastingEnabled;
	}
}
