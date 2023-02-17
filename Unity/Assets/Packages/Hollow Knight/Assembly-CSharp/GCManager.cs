using System;
using UnityEngine;
using UnityEngine.Profiling;

public class GCManager : MonoBehaviour
{
	private const long GCPanicLimit = 2936012800L;

	private const int GCPanicFrameLimit = 108000;

	private static int framesSinceCollect;

	public static bool IsSupported => false;

	public static bool IsAutomaticCollectionEnabled
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	public static void Collect()
	{
		framesSinceCollect = 0;
		GC.Collect();
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Init()
	{
		if (IsSupported)
		{
			GameObject obj = new GameObject("GCManager", typeof(GCManager));
			obj.hideFlags |= HideFlags.HideAndDontSave;
			UnityEngine.Object.DontDestroyOnLoad(obj);
		}
	}

	public static long GetMemoryUsage()
	{
		return Profiler.GetTotalReservedMemoryLong() - Profiler.GetTotalUnusedReservedMemoryLong();
	}

	protected void Update()
	{
		framesSinceCollect++;
		if (framesSinceCollect >= 108000)
		{
			Collect();
		}
		if (IsSupported)
		{
			IsAutomaticCollectionEnabled = GetMemoryUsage() > 2936012800u;
		}
	}
}
