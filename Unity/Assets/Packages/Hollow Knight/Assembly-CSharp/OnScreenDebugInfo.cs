using System;
using System.Collections;
using System.Threading;
using InControl;
using Language;
using Modding;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnScreenDebugInfo : MonoBehaviour
{
	private GameManager gm;

	private InputHandler ih;

	private float unloadTime;

	private float loadTime;

	private float frameRate;

	private string fps;

	private string infoString;

	private string versionNumber;

	private const float textWidth = 100f;

	private Rect loadProfilerRect;

	private Rect fpsRect;

	private Rect infoRect;

	private Rect inputRect;

	private Rect tfrRect;

	private bool showFPS;

	private bool showInfo;

	private bool showInput;

	private bool showLoadingTime;

	private bool showTFR;

	private void Awake()
	{
		if (ModLoader.LoadState == ModLoader.ModLoadState.NotStarted)
		{
			ModLoader.LoadState = ModLoader.ModLoadState.Started;
			GameObject gameObject = new GameObject();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
		else
		{
			}
		orig_Awake();
	}

	private IEnumerator Start()
	{
		gm = GameManager.instance;
		gm.UnloadingLevel += OnLevelUnload;
		ih = gm.inputHandler;
		RetrieveInfo();
		GUI.depth = 2;
		while (showFPS)
		{
			if (Time.timeScale == 1f)
			{
				yield return new WaitForSeconds(0.1f);
				frameRate = 1f / Time.deltaTime;
				fps = "FPS :" + Mathf.Round(frameRate);
			}
			else
			{
				fps = "Pause";
			}
			yield return new WaitForSeconds(0.5f);
		}
	}

	private void LevelActivated(Scene sceneFrom, Scene sceneTo)
	{
		RetrieveInfo();
		if (showLoadingTime)
		{
			loadTime = (float)Math.Round(Time.realtimeSinceStartup - unloadTime, 2);
		}
	}

	private void OnEnable()
	{
		UnityEngine.SceneManagement.SceneManager.activeSceneChanged += LevelActivated;
	}

	private void OnDisable()
	{
		UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= LevelActivated;
		if (gm != null)
		{
			gm.UnloadingLevel -= OnLevelUnload;
		}
	}

	private void OnGUI()
	{
		if (showInfo)
		{
			if (showFPS)
			{
				GUI.Label(fpsRect, fps);
			}
			if (showInfo)
			{
				GUI.Label(infoRect, infoString);
			}
			if (showInput)
			{
				GUI.Label(inputRect, ReadInput());
			}
			if (showLoadingTime)
			{
				GUI.Label(loadProfilerRect, loadTime + "s");
			}
			if (showTFR)
			{
				GUI.Label(tfrRect, "TFR: " + Application.targetFrameRate);
			}
		}
	}

	public void ShowFPS()
	{
		showFPS = !showFPS;
	}

	public void ShowGameInfo()
	{
		showInfo = !showInfo;
	}

	public void ShowInput()
	{
		showInput = !showInput;
	}

	public void ShowLoadingTime()
	{
		showLoadingTime = !showLoadingTime;
	}

	public void ShowTargetFrameRate()
	{
		showTFR = !showTFR;
	}

	private void OnLevelUnload()
	{
		unloadTime = Time.realtimeSinceStartup;
	}

	private void RetrieveInfo()
	{
		if (gm == null)
		{
			gm = GameManager.instance;
		}
		versionNumber = "1.5.78.11833";
		infoString = global::Language.Language.Get("GAME_TITLE") + "\r\n" + versionNumber + " " + global::Language.Language.CurrentLanguage().ToString() + "\r\n" + gm.GetSceneNameString();
	}

	private string ReadInput()
	{
		return string.Concat(string.Concat(string.Concat(string.Concat("" + $"Move Vector: {ih.inputActions.moveVector.Vector.x.ToString()}, {ih.inputActions.moveVector.Vector.y.ToString()}", $"\nMove Pressed: {ih.inputActions.left.IsPressed || ih.inputActions.right.IsPressed}"), $"\nMove Raw L: {ih.inputActions.left.RawValue} R: {ih.inputActions.right.RawValue}"), string.Format("\nInputX: " + ih.inputX)), $"\nAny Key Down: {InputManager.AnyKeyIsPressed}");
	}

	private void orig_Awake()
	{
		fpsRect = new Rect(7f, 5f, 100f, 25f);
		infoRect = new Rect(Screen.width - 105, 5f, 100f, 70f);
		inputRect = new Rect(7f, 65f, 300f, 120f);
		loadProfilerRect = new Rect((float)(Screen.width / 2) - 50f, 5f, 100f, 25f);
		tfrRect = new Rect(7f, 20f, 100f, 25f);
	}
}
