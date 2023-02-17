using System;
using UnityEngine;

public class WorldInfo : ScriptableObject
{
	[Serializable]
	public class SceneInfo
	{
		public string SceneName;

		public TransitionInfo[] Transitions;

		public int ZoneTags;
	}

	[Serializable]
	public struct TransitionInfo
	{
		public string DoorName;

		public string DestinationSceneName;

		public string DestinationDoorName;
	}

	[Flags]
	public enum ZoneTags
	{
		None = 0,
		Room = 1,
		Crossroads = 2,
		Ruins = 4,
		Fungus1 = 8,
		Fungus2 = 0x10,
		Fungus3 = 0x20,
		Cliffs = 0x40,
		RestingGrounds = 0x80,
		Mines = 0x100,
		Deepnest = 0x200,
		Deepnest_East = 0x400,
		Abyss = 0x800,
		Waterways = 0x1000,
		White_Palace = 0x2000,
		Hive = 0x4000,
		Grimm = 0x8000,
		Dream = 0x10000,
		Tutorial = 0x20000,
		SpecialA = 0x40000,
		SpecialB = 0x80000
	}

	public SceneInfo[] Scenes;

	private static string[] NonGameplaySceneName = new string[20]
	{
		"Pre_Menu_Intro", "Menu_Title", "BetaEnd", "Knight_Pickup", "Opening_Sequence", "Prologue_Excerpt", "Intro_Cutscene_Prologue", "Intro_Cutscene", "Cinematic_Stag_travel", "Cinematic_Ending_A",
		"Cinematic_Ending_B", "Cinematic_Ending_C", "End_Credits", "Cinematic_MrMushroom", "Menu_Credits", "End_Game_Completion", "PermaDeath", "PermaDeath_Unlock", "Cinematic_Ending_D", "Cinematic_Ending_E"
	};

	public static bool NameLooksLikeGameplayScene(string sceneName)
	{
		if (Array.IndexOf(NonGameplaySceneName, sceneName) != -1)
		{
			return false;
		}
		if (sceneName.EndsWith("_boss_defeated", StringComparison.InvariantCultureIgnoreCase) || sceneName.EndsWith("_boss", StringComparison.InvariantCultureIgnoreCase) || sceneName.EndsWith("_preload", StringComparison.InvariantCultureIgnoreCase))
		{
			return false;
		}
		return true;
	}

	public SceneInfo GetSceneInfo(string sceneName)
	{
		for (int i = 0; i < Scenes.Length; i++)
		{
			SceneInfo sceneInfo = Scenes[i];
			if (sceneInfo.SceneName.Equals(sceneName, StringComparison.InvariantCultureIgnoreCase))
			{
				return sceneInfo;
			}
		}
		return null;
	}
}
