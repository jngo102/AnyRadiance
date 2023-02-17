using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildAssetBundles : EditorWindow
{
    string outPath;

    [MenuItem("Assets/Build Asset Bundles")]
    static void Init()
    {
        var window = (BuildAssetBundles)GetWindow(typeof(BuildAssetBundles));
        window.Show();
    }

    void OnGUI()
    {
        outPath = GUILayout.TextField(outPath);
        if (GUILayout.Button("Build"))
        {
            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
            BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
            Debug.Log("Successfully built asset bundles.");
        }
    }
}
