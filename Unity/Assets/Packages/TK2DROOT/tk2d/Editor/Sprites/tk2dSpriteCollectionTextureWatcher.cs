using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class tk2dSpriteCollectionTextureWatcher : AssetPostprocessor
{
	void OnPreprocessTexture()
	{
		if (tk2dPreferences.inst.autoRebuild)
		{
			// Make sure sprite textures always have the correct format set up
			if (tk2dSpriteCollectionBuilder.IsSpriteSourceTexture(assetPath))
			{
				tk2dSpriteCollectionBuilder.ConfigureSpriteTextureImporter(assetPath);
			}
		}
	}
	
	static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		if (tk2dPreferences.inst.autoRebuild && importedAssets != null && importedAssets.Length	!= 0)
		{
			// Unity 2017 includes assets from outside Assets as well, which we don't care about
			string basePath = Application.dataPath;

			List<string> changedRelativePaths = new List<string>();
			foreach (var v in importedAssets)
			{
				string p = System.IO.Path.GetFullPath(v).Replace('\\', '/');
				if (p.IndexOf(basePath) == 0)
				{
					changedRelativePaths.Add(v);
				}
			}

			tk2dSpriteCollectionBuilder.RebuildOutOfDate(changedRelativePaths.ToArray());
		}
	}
}



