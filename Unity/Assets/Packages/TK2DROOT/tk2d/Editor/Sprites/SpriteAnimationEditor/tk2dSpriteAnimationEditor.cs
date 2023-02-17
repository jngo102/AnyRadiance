using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(tk2dSpriteAnimation))]
class tk2dSpriteAnimationEditor : Editor
{
    public static bool viewData = false;

    void OnEnable() {
        viewData = false;
    }

    public override void OnInspectorGUI()
    {
        tk2dSpriteAnimation anim = (tk2dSpriteAnimation)target;
        
        GUILayout.Space(8);
        if (anim != null)
        {
            string message = @"Due to changes in the prefab system in Unity 2018.3, the edit functionality has been moved." +
                              "Exit prefab edit mode, select your sprite animation and click 2D Toolikt / Edit... in the main menu";
            tk2dGuiUtility.InfoBox(message, tk2dGuiUtility.WarningLevel.Warning);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Open Editor...", GUILayout.MinWidth(120)))
            {
                UnityEditor.EditorUtility.DisplayDialog("Open Editor has moved",
                    message,
                    "Ok");
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        if (viewData) {
            GUILayout.BeginVertical("box");
            DrawDefaultInspector();
            GUILayout.EndVertical();
        }

        GUILayout.Space(64);
	}
	
    [MenuItem("CONTEXT/tk2dSpriteAnimation/View data")]
    static void ToggleViewData() {
        tk2dSpriteAnimationEditor.viewData = true;
    }

	[MenuItem("Assets/Create/tk2d/Sprite Animation", false, 10001)]
    static void DoAnimationCreate()
    {
		string path = tk2dEditorUtility.CreateNewPrefab("SpriteAnimation");
        if (path.Length != 0)
        {
            GameObject go = new GameObject();
            go.AddComponent<tk2dSpriteAnimation>();
	        tk2dEditorUtility.SetGameObjectActive(go, false);

            PrefabUtility.SaveAsPrefabAsset(go, path);
            GameObject.DestroyImmediate(go);

            tk2dEditorUtility.GetOrCreateIndex().AddSpriteAnimation(AssetDatabase.LoadAssetAtPath(path, typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation);
			tk2dEditorUtility.CommitIndex();

			// Select object
			Selection.activeObject = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
        }
    }	
}

