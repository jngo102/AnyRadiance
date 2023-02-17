using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePreloader : MonoBehaviour
{
	public class SceneLoadOp
	{
		public AsyncOperation operation;

		public string sceneName;

		public SceneLoadOp(string sceneName, AsyncOperation operation)
		{
			this.sceneName = sceneName;
			this.operation = operation;
		}
	}

	public string sceneNameToLoad = "";

	public string needsPlayerDataBool = "";

	public bool playerDataBoolValue;

	private float startLoadTime;

	private float endLoadTime;

	private float? loadTime;

	private static List<SceneLoadOp> pendingOperations = new List<SceneLoadOp>();

	private static List<SceneLoadOp> completedOperations = new List<SceneLoadOp>();

	private void Start()
	{
		loadTime = null;
		if (sceneNameToLoad != "" && (!(needsPlayerDataBool != "") || GameManager.instance.GetPlayerDataBool(needsPlayerDataBool) == playerDataBoolValue))
		{
			StartCoroutine(LoadRoutine());
		}
	}

	private void OnGUI()
	{
		if ((Debug.isDebugBuild || Application.isEditor || Application.platform == RuntimePlatform.Switch) && loadTime.HasValue)
		{
			GUI.Label(new Rect(10f, 5f, 500f, 50f), $"Preloaded Level:{sceneNameToLoad}, Time: {loadTime}");
		}
	}

	private IEnumerator LoadRoutine()
	{
		yield return null;
		AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNameToLoad, LoadSceneMode.Additive);
		async.allowSceneActivation = false;
		pendingOperations.Add(new SceneLoadOp(sceneNameToLoad, async));
		startLoadTime = Time.unscaledTime;
		while (async.progress < 0.9f)
		{
			yield return null;
		}
		endLoadTime = Time.unscaledTime;
		loadTime = endLoadTime - startLoadTime;
	}

	public static IEnumerator FinishPendingOperations()
	{
		if (pendingOperations == null)
		{
			yield break;
		}
		foreach (SceneLoadOp pendingOperation in pendingOperations)
		{
			pendingOperation.operation.allowSceneActivation = true;
			completedOperations.Add(pendingOperation);
			yield return pendingOperation.operation;
		}
		pendingOperations.Clear();
	}

	public static void Cleanup()
	{
		if (completedOperations == null)
		{
			return;
		}
		foreach (SceneLoadOp completedOperation in completedOperations)
		{
			UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(completedOperation.sceneName);
		}
		completedOperations.Clear();
	}
}
