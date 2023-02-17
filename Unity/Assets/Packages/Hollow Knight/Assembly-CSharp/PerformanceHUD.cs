using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PerformanceHUD : MonoBehaviour
{
	public class LoadReport
	{
		public Color Color;

		public GUIContent Content;

		public bool DidPost;
	}

	private static PerformanceHUD shared;

	private int frameCounter;

	private int lastSecond;

	private int framesLastSecond;

	private GUIContent framesLastSecondContent;

	private Color framesColor;

	private bool fpsFrames;

	private float? instantaneousFrames;

	private int lastScreenWidth;

	private int lastScreenHeight;

	private GUIContent versionContent;

	private GUIContent memoryContent;

	private List<LoadReport> loadReports;

	public static bool ShowVibrations;

	private GUIContent vibrationsContent;

	public static PerformanceHUD Shared => shared;

	public bool FpsFrames
	{
		get
		{
			return fpsFrames;
		}
		set
		{
			fpsFrames = value;
		}
	}

	public static void Init()
	{
		if (shared == null)
		{
			GameObject obj = new GameObject(typeof(PerformanceHUD).Name);
			shared = obj.AddComponent<PerformanceHUD>();
			shared.enabled = false;
			UnityEngine.Object.DontDestroyOnLoad(obj);
		}
	}

	protected void Awake()
	{
		frameCounter = 0;
		lastSecond = (int)Time.realtimeSinceStartup;
		framesLastSecondContent = new GUIContent("N/A");
		framesColor = Color.gray;
		BuildMetadata embedded = BuildMetadata.Embedded;
		if (embedded != null)
		{
			versionContent = new GUIContent(embedded.BranchName + " r" + embedded.Revision + " (" + embedded.CommitTime.ToString() + ")");
		}
		else
		{
			versionContent = new GUIContent("No Build Metadata");
		}
		memoryContent = new GUIContent("N/A");
		loadReports = new List<LoadReport>();
		vibrationsContent = new GUIContent("");
	}

	protected void OnEnable()
	{
		GameManager.SceneTransitionBegan += GameManager_SceneTransitionBegan;
	}

	protected void OnDisable()
	{
		GameManager.SceneTransitionBegan -= GameManager_SceneTransitionBegan;
	}

	protected void Update()
	{
		frameCounter++;
		int num = (int)Time.realtimeSinceStartup;
		if (num != lastSecond)
		{
			framesLastSecond = frameCounter;
			if (framesLastSecond >= 58)
			{
				framesColor = Color.green;
			}
			else if (framesLastSecond >= 50)
			{
				framesColor = Color.yellow;
			}
			else
			{
				framesColor = Color.red;
			}
			framesLastSecondContent.text = framesLastSecond.ToString();
			lastSecond = num;
			frameCounter = 0;
			UpdateMemory();
		}
		if (fpsFrames)
		{
			instantaneousFrames = 1f / Time.unscaledDeltaTime;
		}
		else
		{
			instantaneousFrames = null;
		}
		if (!ShowVibrations)
		{
			return;
		}
		VibrationMixer mixer = VibrationManager.GetMixer();
		if (mixer != null)
		{
			string text = "";
			for (int i = 0; i < mixer.PlayingEmissionCount; i++)
			{
				if (text.Length > 0)
				{
					text += ", ";
				}
				text += mixer.GetPlayingEmission(i).ToString();
			}
			vibrationsContent.text = text;
		}
		else
		{
			vibrationsContent.text = "";
		}
	}

	private void GameManager_SceneTransitionBegan(SceneLoad sceneLoad)
	{
		LoadReport loadReport = new LoadReport
		{
			Color = Color.white,
			Content = new GUIContent()
		};
		loadReports.Add(loadReport);
		while (loadReports.Count > 2)
		{
			loadReports.RemoveAt(0);
		}
		sceneLoad.FetchComplete += delegate
		{
			UpdateSceneLoadRecordContent(sceneLoad, loadReport);
		};
		sceneLoad.ActivationComplete += delegate
		{
			UpdateSceneLoadRecordContent(sceneLoad, loadReport);
		};
		sceneLoad.Complete += delegate
		{
			UpdateSceneLoadRecordContent(sceneLoad, loadReport);
		};
		sceneLoad.StartCalled += delegate
		{
			UpdateSceneLoadRecordContent(sceneLoad, loadReport);
		};
		sceneLoad.BossLoaded += delegate
		{
			UpdateSceneLoadRecordContent(sceneLoad, loadReport);
		};
		sceneLoad.Finish += delegate
		{
			UpdateSceneLoadRecordContent(sceneLoad, loadReport);
		};
		UpdateSceneLoadRecordContent(sceneLoad, loadReport);
	}

	private void UpdateSceneLoadRecordContent(SceneLoad sceneLoad, LoadReport report)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(sceneLoad.TargetSceneName);
		stringBuilder.Append(":    ");
		float num = 0f;
		for (int i = 0; i < 8; i++)
		{
			SceneLoad.Phases phase = (SceneLoad.Phases)i;
			float? duration = sceneLoad.GetDuration(phase);
			if (duration.HasValue && duration.Value > Mathf.Epsilon)
			{
				stringBuilder.Append(phase.ToString());
				stringBuilder.Append(": ");
				stringBuilder.Append(duration.Value.ToString("0.00s"));
				stringBuilder.Append("    ");
				num += duration.Value;
			}
		}
		if (num > Mathf.Epsilon)
		{
			stringBuilder.Append("Total: ");
			stringBuilder.Append(num.ToString("0.00s"));
		}
		if (num > 3.5f)
		{
			report.Color = Color.red;
		}
		else if (num > 3f)
		{
			report.Color = Color.yellow;
		}
		else
		{
			report.Color = Color.white;
		}
		report.Content.text = stringBuilder.ToString();
		if (sceneLoad.IsFinished && !report.DidPost)
		{
			report.DidPost = true;
		}
	}

	private IEnumerator ReportUpload(WWW www)
	{
		yield return www;
		Debug.LogFormat("Finished upload (isDone={0}, error={1}).", www.isDone, www.error);
	}

	private static string GetTimeStr(float? time)
	{
		if (time.HasValue)
		{
			return time.Value.ToString("0.00") + "s";
		}
		return "N/A";
	}

	private void UpdateMemory()
	{
		double num = (double)GCManager.GetMemoryUsage() / 1024.0 / 1024.0;
		double num2 = SystemInfo.systemMemorySize;
		memoryContent.text = "Memory (CPU): " + num.ToString("0.0") + "/" + num2.ToString("0.0") + " - " + (GCManager.IsAutomaticCollectionEnabled ? "GC On" : "GC Off");
	}

	protected void OnGUI()
	{
		GUI.color = framesColor;
		LabelWithShadow(new Rect(0f, Screen.height - 24, Screen.width, 24f), framesLastSecondContent);
		GUI.color = Color.white;
		LabelWithShadow(new Rect(0f, Screen.height - 48, Screen.width, 24f), versionContent);
		GUI.color = Color.white;
		LabelWithShadow(new Rect(0f, Screen.height - 72, Screen.width, 24f), memoryContent);
		for (int i = 0; i < loadReports.Count; i++)
		{
			LoadReport loadReport = loadReports[i];
			GUI.color = loadReport.Color;
			LabelWithShadow(new Rect(0f, Screen.height - 24 * (i + 4), Screen.width, 24f), loadReport.Content);
		}
		if (fpsFrames && instantaneousFrames.HasValue)
		{
			float value = instantaneousFrames.Value;
			if (value < 57.5f)
			{
				Color color = Color.yellow;
				if (value < 50.5f)
				{
					color = Color.red;
				}
				GUI.color = color;
				GUI.DrawTexture(new Rect(0f, 0f, 2f, Screen.height), Texture2D.whiteTexture);
				GUI.DrawTexture(new Rect(Screen.width - 2, 0f, 2f, Screen.height), Texture2D.whiteTexture);
				GUI.DrawTexture(new Rect(0f, 0f, Screen.width, 2f), Texture2D.whiteTexture);
				GUI.DrawTexture(new Rect(0f, Screen.height - 2, Screen.width, 2f), Texture2D.whiteTexture);
				GUI.color = Color.white;
			}
		}
		if (ShowVibrations)
		{
			GUI.color = Color.white;
			LabelWithShadow(new Rect(0f, Screen.height - 144, Screen.width, 24f), vibrationsContent);
		}
		if ((bool)GameManager.instance && (bool)GameManager.instance.sm)
		{
			SceneManager sm = GameManager.instance.sm;
			string text2 = $"Saturation: {sm.saturation}, Adjusted: {sm.AdjustSaturation(sm.saturation)}";
			GUI.color = Color.white;
			LabelWithShadow(new Rect(0f, Screen.height - 168, Screen.width, 24f), new GUIContent(text2));
		}
		StringBuilder ggInfo = new StringBuilder();
		int lineCount = 0;
		Action<string> action = delegate(string line)
		{
			if (lineCount > 0)
			{
				ggInfo.Append('\n');
			}
			ggInfo.Append(line);
			lineCount++;
		};
		Action<string> action2 = delegate(string text)
		{
			ggInfo.Append(text);
		};
		action("Challenge Type: ");
		if (BossSceneController.IsBossScene)
		{
			if (BossSequenceController.IsInSequence)
			{
				action2("Boss Sequence");
			}
			else
			{
				action2("Boss Statue");
			}
			action("Boss Level: ");
			action2(BossSceneController.Instance.BossLevel.ToString());
			action("Boss Health: ");
			if (BossSceneController.Instance.BossHealthLookup.Count > 0)
			{
				foreach (KeyValuePair<HealthManager, BossSceneController.BossHealthDetails> item in BossSceneController.Instance.BossHealthLookup)
				{
					action(string.Format(" - {0} - Base: {1}, Adjusted: {2}, Current: {3}", item.Key ? item.Key.gameObject.name : "(missing)", item.Value.baseHP, item.Value.adjustedHP, item.Key ? item.Key.hp.ToString() : "(missing)"));
				}
			}
			else
			{
				action(" - (none)");
			}
		}
		else
		{
			action2("Regular");
		}
		float num = 24 + (lineCount - 1) * 16;
		LabelWithShadow(new Rect(0f, (float)(Screen.height - 168) - num, Screen.width, num), new GUIContent(ggInfo.ToString()));
	}

	private void LabelWithShadow(Rect rect, GUIContent content)
	{
		Vector2 vector = GUI.skin.label.CalcSize(content);
		Color color = GUI.color;
		try
		{
			GUI.color = new Color(0f, 0f, 0f, 0.5f);
			GUI.DrawTexture(new Rect(rect.x, rect.y, vector.x, rect.height), Texture2D.whiteTexture);
			GUI.color = Color.black;
			GUI.Label(new Rect(rect.x + 2f, rect.y + 2f, rect.width, rect.height), content);
			GUI.color = color;
			GUI.Label(new Rect(rect.x + 0f, rect.y + 0f, rect.width, rect.height), content);
		}
		finally
		{
			GUI.color = color;
		}
	}
}
