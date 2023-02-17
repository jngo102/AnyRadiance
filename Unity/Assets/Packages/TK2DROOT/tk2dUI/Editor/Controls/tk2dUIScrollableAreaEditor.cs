using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(tk2dUIScrollableArea))]
public class tk2dUIScrollableAreaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        tk2dGuiUtility.LookLikeInspector();
        base.OnInspectorGUI();

		tk2dUIScrollableArea scrollableArea = (tk2dUIScrollableArea)target;

		scrollableArea.BackgroundLayoutItem = EditorGUILayout.ObjectField("Background LayoutItem", scrollableArea.BackgroundLayoutItem, typeof(tk2dUILayout), true) as tk2dUILayout;
		scrollableArea.ContentLayoutContainer = EditorGUILayout.ObjectField("Content LayoutContainer", scrollableArea.ContentLayoutContainer, typeof(tk2dUILayoutContainer), true) as tk2dUILayoutContainer;

        GUILayout.Label("Tools", EditorStyles.boldLabel);
        if (GUILayout.Button("Calculate content length")) {
            tk2dUndo.RecordObject(scrollableArea, "Content length changed");
            Bounds b = tk2dUIItemBoundsHelper.GetRendererBoundsInChildren( scrollableArea.contentContainer.transform, scrollableArea.contentContainer.transform );
            b.Encapsulate(Vector3.zero);
            float contentSize = (scrollableArea.scrollAxes == tk2dUIScrollableArea.Axes.XAxis) ? b.size.x : b.size.y;
            scrollableArea.ContentLength = contentSize * 1.02f; // 5% more
            tk2dUtil.SetDirty(scrollableArea);
        }

        tk2dUIMethodBindingHelper methodBindingUtil = new tk2dUIMethodBindingHelper();
        methodBindingUtil.BeginMessageGUI(scrollableArea, "sendMessageTarget", scrollableArea.backgroundUIItem);
        methodBindingUtil.MethodBinding( "On Scroll", typeof(tk2dUIScrollableArea), scrollableArea.SendMessageTarget, "SendMessageOnScrollMethodName" );
        methodBindingUtil.EndMessageGUI();

        if (GUI.changed)
        {
            tk2dUtil.SetDirty(scrollableArea);
        }
    }

    public void OnSceneGUI()
    {
        bool wasChange=false;
        tk2dUIScrollableArea scrollableArea = (tk2dUIScrollableArea)target;
        bool isYAxis = scrollableArea.scrollAxes== tk2dUIScrollableArea.Axes.YAxis;

        // Get rescaled transforms
        Matrix4x4 m = scrollableArea.transform.localToWorldMatrix;
        Vector3 up = m.MultiplyVector(Vector3.up);
        Vector3 right = m.MultiplyVector(Vector3.right);

        float newVisibleAreaLength = tk2dUIControlsHelperEditor.DrawLengthHandles("Visible Area Length", scrollableArea.VisibleAreaLength,scrollableArea.contentContainer.transform.position, isYAxis? -up:right, Color.red,isYAxis ? 0.2f : -0.2f, 0, 0.05f);
        if (newVisibleAreaLength != scrollableArea.VisibleAreaLength)
        {
            tk2dUndo.RecordObject(scrollableArea, "Visible area changed");
            scrollableArea.VisibleAreaLength = newVisibleAreaLength;
            wasChange = true;
        }

        float newContentLength = tk2dUIControlsHelperEditor.DrawLengthHandles("Content Length", scrollableArea.ContentLength, scrollableArea.contentContainer.transform.position, isYAxis ? -up : right, Color.blue, isYAxis ? 0.2f : -0.2f, isYAxis ? 0.4f : -0.4f, 0.1f);
        if (newContentLength != scrollableArea.ContentLength)
        {
            tk2dUndo.RecordObject(scrollableArea, "Content length changed");
            scrollableArea.ContentLength = newContentLength;
            wasChange = true;
        }

        if (wasChange)
        {
            tk2dUtil.SetDirty(scrollableArea);
        }
    }

}
