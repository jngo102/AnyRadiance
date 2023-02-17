using UnityEngine;

public static class TimeController
{
	private static float slowMotionTimeScale;

	private static float pauseTimeScale;

	private static float platformBackgroundTimeScale;

	private static float genericTimeScale;

	public static float SlowMotionTimeScale
	{
		get
		{
			return slowMotionTimeScale;
		}
		set
		{
			SetTimeScaleFactor(ref slowMotionTimeScale, value);
		}
	}

	public static float PauseTimeScale
	{
		get
		{
			return pauseTimeScale;
		}
		set
		{
			SetTimeScaleFactor(ref pauseTimeScale, value);
		}
	}

	public static float PlatformBackgroundTimeScale
	{
		get
		{
			return platformBackgroundTimeScale;
		}
		set
		{
			SetTimeScaleFactor(ref platformBackgroundTimeScale, value);
		}
	}

	public static float GenericTimeScale
	{
		get
		{
			return genericTimeScale;
		}
		set
		{
			SetTimeScaleFactor(ref genericTimeScale, value);
		}
	}

	static TimeController()
	{
		slowMotionTimeScale = 1f;
		pauseTimeScale = 1f;
		platformBackgroundTimeScale = 1f;
		genericTimeScale = 1f;
	}

	private static void SetTimeScaleFactor(ref float field, float val)
	{
		if (field != val)
		{
			field = val;
			float num = slowMotionTimeScale * pauseTimeScale * platformBackgroundTimeScale * genericTimeScale;
			if (num < 0.01f)
			{
				num = 0f;
			}
			Time.timeScale = num;
		}
	}
}
