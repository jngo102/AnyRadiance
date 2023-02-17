using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class SaveDataUpgradeHandler
{
	private class SceneSplit
	{
		public string SceneName;

		public string Version;

		public string[] NewSceneNames;

		public SceneSplit(string sceneName, string version, params string[] newSceneNames)
		{
			SceneName = sceneName;
			Version = version;
			NewSceneNames = newSceneNames;
		}

		public bool ShouldHandleSplit(string otherVersion)
		{
			otherVersion = CleanupVersionText(otherVersion);
			Version version = new Version(Version);
			Version value = new Version(otherVersion);
			return version.CompareTo(value) > 0;
		}
	}

	private struct SystemDataUpgrade
	{
		public Version TargetVersion;

		public Action<object> UpgradeAction;
	}

	private static readonly SceneSplit[] splitScenes = new SceneSplit[9]
	{
		new SceneSplit("Deepnest_26", "1.3.0.0", "Deepnest_26b"),
		new SceneSplit("Deepnest_East_14", "1.3.0.0", "Deepnest_East_14b"),
		new SceneSplit("Ruins1_31", "1.3.0.1", "Ruins1_31b"),
		new SceneSplit("Hive_03", "1.3.0.2", "Hive_03_c"),
		new SceneSplit("Ruins2_01", "1.3.0.2", "Ruins2_01_b"),
		new SceneSplit("Ruins2_11", "1.3.0.2", "Ruins2_11_b"),
		new SceneSplit("Ruins2_03", "1.3.0.3", "Ruins2_03b"),
		new SceneSplit("Ruins1_05", "1.3.0.3", "Ruins1_05c"),
		new SceneSplit("Hive_01", "1.3.0.4", "Hive_01_b")
	};

	private static readonly Dictionary<Type, SystemDataUpgrade> systemDataUpgrades = new Dictionary<Type, SystemDataUpgrade> { 
	{
		typeof(InputHandler),
		new SystemDataUpgrade
		{
			TargetVersion = new Version("1.4.0.0"),
			UpgradeAction = delegate(object obj)
			{
				InputHandler obj2 = (InputHandler)obj;
				obj2.ResetDefaultKeyBindings();
				obj2.ResetDefaultControllerButtonBindings();
				obj2.ResetAllControllerButtonBindings();
			}
		}
	} };

	private static string CleanupVersionText(string versionText)
	{
		return Regex.Replace(versionText, "[A-Za-z ]", "");
	}

	private static void ClearDreamGate(SceneSplit sceneSplit, ref string dreamGateScene)
	{
		if (sceneSplit.SceneName == dreamGateScene)
		{
			dreamGateScene = "";
		}
	}

	private static void UpdateMap(SceneSplit sceneSplit, ref List<string> scenesMapped)
	{
		if (!scenesMapped.Contains(sceneSplit.SceneName))
		{
			return;
		}
		string[] newSceneNames = sceneSplit.NewSceneNames;
		foreach (string item in newSceneNames)
		{
			if (!scenesMapped.Contains(item))
			{
				scenesMapped.Add(item);
			}
		}
	}

	public static void UpgradeSaveData(ref PlayerData playerData)
	{
		if (playerData.GetString("dreamGateScene") == "Hive_05")
		{
			playerData.SetStringSwappedArgs("", "dreamGateScene");
		}
		SceneSplit[] array = splitScenes;
		foreach (SceneSplit sceneSplit in array)
		{
			if (sceneSplit.ShouldHandleSplit(playerData.GetString("version")))
			{
				ClearDreamGate(sceneSplit, ref playerData.dreamGateScene);
				UpdateMap(sceneSplit, ref playerData.scenesMapped);
			}
		}
		PersistentBoolData persistentBoolData = new PersistentBoolData
		{
			id = "Mawlek Body",
			sceneName = "Crossroads_09"
		};
		PersistentBoolData persistentBoolData2 = new PersistentBoolData
		{
			id = "Battle Scene",
			sceneName = "Crossroads_09"
		};
		PersistentBoolData persistentBoolData3 = SceneData.instance.FindMyState(persistentBoolData);
		if (persistentBoolData3 != null && persistentBoolData3.activated)
		{
			persistentBoolData2.activated = true;
			SceneData.instance.SaveMyState(persistentBoolData2);
		}
		if (playerData.GetBool("gotShadeCharm"))
		{
			playerData.SetIntSwappedArgs(4, "royalCharmState");
		}
		if (playerData.GetBool("colosseumGoldCompleted") && !playerData.GetVariable<List<string>>("unlockedBossScenes").Contains("God Tamer Boss Scene"))
		{
			playerData.GetVariable<List<string>>("unlockedBossScenes").Add("God Tamer Boss Scene");
		}
		bool flag = false;
		for (int num = playerData.GetVariable<List<string>>("unlockedBossScenes").Count - 1; num >= 0; num--)
		{
			if (!(playerData.GetVariable<List<string>>("unlockedBossScenes")[num] != "God Tamer Boss Scene"))
			{
				if (flag)
				{
					playerData.GetVariable<List<string>>("unlockedBossScenes").RemoveAt(num);
				}
				else
				{
					flag = true;
				}
			}
		}
	}

	public static void UpgradeSystemData<T>(T system)
	{
		Type typeFromHandle = typeof(T);
		if (systemDataUpgrades.ContainsKey(typeFromHandle))
		{
			string key = $"lastSystemVersion_{typeFromHandle.ToString()}";
			Version version = new Version(Platform.Current.SharedData.GetString(key, "0.0.0.0"));
			SystemDataUpgrade systemDataUpgrade = systemDataUpgrades[typeFromHandle];
			if (version < systemDataUpgrade.TargetVersion)
			{
				systemDataUpgrade.UpgradeAction(system);
				Platform.Current.SharedData.SetString(key, "1.5.78.11833");
			}
		}
	}
}
