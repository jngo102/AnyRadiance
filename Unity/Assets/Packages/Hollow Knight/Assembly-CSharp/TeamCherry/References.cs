using System;
using UnityEngine;

namespace TeamCherry
{
	
	[Serializable]
	public class References : ScriptableObject
	{
		public SceneDefaultSettings sceneDefaultSettings;
	
		public CleanScenePrefabs cleanScenePrefabs;
	
		public GameConfig gameConfig;
	
		public SaveConfig saveConfig;
	
		public GameVersionData gameVersionData;
	}
}