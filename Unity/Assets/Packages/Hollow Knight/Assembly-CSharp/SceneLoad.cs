using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad
{
	public enum Phases
	{
		FetchBlocked,
		Fetch,
		ActivationBlocked,
		Activation,
		UnloadUnusedAssets,
		GarbageCollect,
		StartCall,
		LoadBoss
	}

	private class PhaseInfo
	{
		public float? BeginTime;

		public float? EndTime;
	}

	public delegate void FetchCompleteDelegate();

	public delegate void WillActivateDelegate();

	public delegate void ActivationCompleteDelegate();

	public delegate void CompleteDelegate();

	public delegate void StartCalledDelegate();

	public delegate void BossLoadCompleteDelegate();

	public delegate void FinishDelegate();

	private readonly MonoBehaviour runner;

	private readonly string targetSceneName;

	public const int PhaseCount = 8;

	private readonly PhaseInfo[] phaseInfos;

	public string TargetSceneName => targetSceneName;

	public bool IsFetchAllowed { get; set; }

	public bool IsActivationAllowed { get; set; }

	public bool IsUnloadAssetsRequired { get; set; }

	public bool IsGarbageCollectRequired { get; set; }

	public bool IsFinished { get; private set; }

	public float? BeginTime => phaseInfos[0].BeginTime;

	public event FetchCompleteDelegate FetchComplete;

	public event WillActivateDelegate WillActivate;

	public event ActivationCompleteDelegate ActivationComplete;

	public event CompleteDelegate Complete;

	public event StartCalledDelegate StartCalled;

	public event BossLoadCompleteDelegate BossLoaded;

	public event FinishDelegate Finish;

	public SceneLoad(MonoBehaviour runner, string targetSceneName)
	{
		this.runner = runner;
		this.targetSceneName = targetSceneName;
		phaseInfos = new PhaseInfo[8];
		for (int i = 0; i < 8; i++)
		{
			phaseInfos[i] = new PhaseInfo
			{
				BeginTime = null
			};
		}
	}

	private void RecordBeginTime(Phases phase)
	{
		phaseInfos[(int)phase].BeginTime = Time.realtimeSinceStartup;
	}

	private void RecordEndTime(Phases phase)
	{
		phaseInfos[(int)phase].EndTime = Time.realtimeSinceStartup;
	}

	public float? GetDuration(Phases phase)
	{
		PhaseInfo phaseInfo = phaseInfos[(int)phase];
		if (phaseInfo.BeginTime.HasValue && phaseInfo.EndTime.HasValue)
		{
			return phaseInfo.EndTime.Value - phaseInfo.BeginTime.Value;
		}
		return null;
	}

	public void Begin()
	{
		runner.StartCoroutine(BeginRoutine());
	}

	private IEnumerator BeginRoutine()
	{
		SceneAdditiveLoadConditional.loadInSequence = true;
		yield return runner.StartCoroutine(ScenePreloader.FinishPendingOperations());
		RecordBeginTime(Phases.FetchBlocked);
		while (!IsFetchAllowed)
		{
			yield return null;
		}
		RecordEndTime(Phases.FetchBlocked);
		RecordBeginTime(Phases.Fetch);
		AsyncOperation loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);
		loadOperation.allowSceneActivation = false;
		while (loadOperation.progress < 0.9f)
		{
			yield return null;
		}
		RecordEndTime(Phases.Fetch);
		if (this.FetchComplete != null)
		{
			try
			{
				this.FetchComplete();
			}
			catch (Exception exception)
			{
				Debug.LogError("Exception in responders to SceneLoad.FetchComplete. Attempting to continue load regardless.");
				Debug.LogException(exception);
			}
		}
		RecordBeginTime(Phases.ActivationBlocked);
		while (!IsActivationAllowed)
		{
			yield return null;
		}
		RecordEndTime(Phases.ActivationBlocked);
		RecordBeginTime(Phases.Activation);
		if (this.WillActivate != null)
		{
			try
			{
				this.WillActivate();
			}
			catch (Exception exception2)
			{
				Debug.LogError("Exception in responders to SceneLoad.WillActivate. Attempting to continue load regardless.");
				Debug.LogException(exception2);
			}
		}
		loadOperation.allowSceneActivation = true;
		yield return loadOperation;
		RecordEndTime(Phases.Activation);
		if (this.ActivationComplete != null)
		{
			try
			{
				this.ActivationComplete();
			}
			catch (Exception exception3)
			{
				Debug.LogError("Exception in responders to SceneLoad.ActivationComplete. Attempting to continue load regardless.");
				Debug.LogException(exception3);
			}
		}
		RecordBeginTime(Phases.UnloadUnusedAssets);
		if (IsUnloadAssetsRequired)
		{
			yield return Resources.UnloadUnusedAssets();
		}
		RecordEndTime(Phases.UnloadUnusedAssets);
		RecordBeginTime(Phases.GarbageCollect);
		if (IsGarbageCollectRequired)
		{
			GCManager.Collect();
		}
		RecordEndTime(Phases.GarbageCollect);
		if (this.Complete != null)
		{
			try
			{
				this.Complete();
			}
			catch (Exception exception4)
			{
				Debug.LogError("Exception in responders to SceneLoad.Complete. Attempting to continue load regardless.");
				Debug.LogException(exception4);
			}
		}
		RecordBeginTime(Phases.StartCall);
		yield return null;
		RecordEndTime(Phases.StartCall);
		if (this.StartCalled != null)
		{
			try
			{
				this.StartCalled();
			}
			catch (Exception exception5)
			{
				Debug.LogError("Exception in responders to SceneLoad.StartCalled. Attempting to continue load regardless.");
				Debug.LogException(exception5);
			}
		}
		if (SceneAdditiveLoadConditional.ShouldLoadBoss)
		{
			RecordBeginTime(Phases.LoadBoss);
			yield return runner.StartCoroutine(SceneAdditiveLoadConditional.LoadAll());
			RecordEndTime(Phases.LoadBoss);
			try
			{
				if (this.BossLoaded != null)
				{
					this.BossLoaded();
				}
				if ((bool)GameManager.instance)
				{
					GameManager.instance.LoadedBoss();
				}
			}
			catch (Exception exception6)
			{
				Debug.LogError("Exception in responders to SceneLoad.BossLoaded. Attempting to continue load regardless.");
				Debug.LogException(exception6);
			}
		}
		try
		{
			ScenePreloader.Cleanup();
		}
		catch (Exception exception7)
		{
			Debug.LogError("Exception in responders to ScenePreloader.Cleanup. Attempting to continue load regardless.");
			Debug.LogException(exception7);
		}
		IsFinished = true;
		if (this.Finish != null)
		{
			try
			{
				this.Finish();
			}
			catch (Exception exception8)
			{
				Debug.LogError("Exception in responders to SceneLoad.Finish. Attempting to continue load regardless.");
				Debug.LogException(exception8);
			}
		}
	}
}
