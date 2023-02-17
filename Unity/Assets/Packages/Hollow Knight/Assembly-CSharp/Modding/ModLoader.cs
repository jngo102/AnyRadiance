// Modding.ModLoader
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     Handles loading of mods.
/// </summary>
internal static class ModLoader
{
	[Flags]
	public enum ModLoadState
	{
		NotStarted = 0,
		Started = 1,
		Preloaded = 2,
		Loaded = 4
	}

	public enum ModErrorState
	{
		Construct,
		Duplicate,
		Initialize,
		Unload
	}

	public static ModLoadState LoadState = ModLoadState.NotStarted;
}