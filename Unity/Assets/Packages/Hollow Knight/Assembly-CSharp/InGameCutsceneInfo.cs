using UnityEngine;

public class InGameCutsceneInfo : MonoBehaviour
{
	private static InGameCutsceneInfo instance;

	[SerializeField]
	private Vector2 cameraPosition;

	public static bool IsInCutscene => instance != null;

	public static Vector2 CameraPosition
	{
		get
		{
			if (!(instance != null))
			{
				return Vector2.zero;
			}
			return instance.cameraPosition;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void OnDestroy()
	{
		if (instance == this)
		{
			instance = null;
		}
	}
}
