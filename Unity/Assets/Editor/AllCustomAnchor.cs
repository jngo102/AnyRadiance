using UnityEditor;
using UnityEngine;

public class AllCustomAnchor : EditorWindow
{
    private tk2dSpriteCollection cln;

    [MenuItem("Tk2d Extensions/Make all custom anchors")]
    static void Init()
    {
        var window = (AllCustomAnchor)GetWindow(typeof(AllCustomAnchor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Path to Collection");
        cln = (tk2dSpriteCollection)EditorGUI.ObjectField(new Rect(0, 0, position.width - 6, 16), "Collection", cln, typeof(tk2dSpriteCollection), false);
        if (GUILayout.Button("Convert"))
        {
            foreach (var def in cln.textureParams)
            {
                def.anchor = tk2dSpriteCollectionDefinition.Anchor.Custom;
            }
        }
    }
}
