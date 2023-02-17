using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAdditiveLoadConditional : MonoBehaviour
{
	[Serializable]
	public struct BoolTest
	{
		public string playerDataBool;

		public bool value;
	}

	[Serializable]
	public struct IntTest
	{
		public enum TestType
		{
			Equal,
			Less,
			More,
			NotEqual,
			LessOrEqual,
			MoreOrEqual
		}

		public string playerDataInt;

		public string otherPlayerDataInt;

		public int value;

		public TestType testType;
	}

	public string sceneNameToLoad = "";

	public string altSceneNameToLoad = "";

	private bool loadAlt;

	[Header("Main Tests")]
	public string needsPlayerDataBool = "";

	public bool playerDataBoolValue;

	[Space]
	public string needsPlayerDataInt = "";

	public int playerDataIntValue;

	public bool isIntValue;

	[Header("Extra tests (not tested if persistent bool true)")]
	public BoolTest[] extraBoolTests;

	[Space]
	public IntTest[] extraIntTests;

	[Space]
	public bool usePersistentBoolItem;

	public PersistentBoolData persistentBoolData;

	[Space]
	public string doorTrigger = "";

	private bool sceneLoaded;

	[Header("Save State On Load (not required)")]
	public PersistentBoolData saveStateData;

	private bool skipExtraTests;

	private static List<SceneAdditiveLoadConditional> additiveSceneLoads = new List<SceneAdditiveLoadConditional>();

	public static bool loadInSequence = false;

	private string SceneNameToLoad
	{
		get
		{
			if (!loadAlt)
			{
				return sceneNameToLoad;
			}
			return altSceneNameToLoad;
		}
	}

	public static bool ShouldLoadBoss
	{
		get
		{
			if (additiveSceneLoads == null)
			{
				return false;
			}
			return additiveSceneLoads.Count > 0;
		}
	}

	private void OnEnable()
	{
		if (!(sceneNameToLoad != ""))
		{
			return;
		}
		bool flag = false;
		if (saveStateData.id != "" && saveStateData.sceneName != "")
		{
			saveStateData.semiPersistent = false;
			PersistentBoolData persistentBoolData = SceneData.instance.FindMyState(saveStateData);
			if (persistentBoolData != null && persistentBoolData.activated)
			{
				skipExtraTests = true;
			}
		}
		if (needsPlayerDataBool != "" && GameManager.instance.GetPlayerDataBool(needsPlayerDataBool) != playerDataBoolValue)
		{
			flag = true;
		}
		if (!flag && extraBoolTests != null && extraBoolTests.Length != 0 && !skipExtraTests)
		{
			BoolTest[] array = extraBoolTests;
			for (int i = 0; i < array.Length; i++)
			{
				BoolTest boolTest = array[i];
				if (GameManager.instance.GetPlayerDataBool(boolTest.playerDataBool) != boolTest.value)
				{
					flag = true;
				}
			}
		}
		if (!flag && needsPlayerDataInt != "" && GameManager.instance.GetPlayerDataInt(needsPlayerDataInt) == playerDataIntValue != isIntValue)
		{
			flag = true;
		}
		if (!flag && extraIntTests != null && extraIntTests.Length > 1 && !skipExtraTests)
		{
			IntTest[] array2 = extraIntTests;
			for (int i = 0; i < array2.Length; i++)
			{
				IntTest intTest = array2[i];
				int playerDataInt = GameManager.instance.GetPlayerDataInt(intTest.playerDataInt);
				int num = ((intTest.otherPlayerDataInt == "") ? intTest.value : GameManager.instance.GetPlayerDataInt(intTest.otherPlayerDataInt));
				bool flag2 = false;
				switch (intTest.testType)
				{
				case IntTest.TestType.Equal:
					flag2 = playerDataInt == num;
					break;
				case IntTest.TestType.NotEqual:
					flag2 = playerDataInt != num;
					break;
				case IntTest.TestType.Less:
					flag2 = playerDataInt < num;
					break;
				case IntTest.TestType.More:
					flag2 = playerDataInt > num;
					break;
				case IntTest.TestType.LessOrEqual:
					flag2 = playerDataInt <= num;
					break;
				case IntTest.TestType.MoreOrEqual:
					flag2 = playerDataInt >= num;
					break;
				}
				if (!flag2)
				{
					flag = true;
				}
			}
		}
		if (!flag && usePersistentBoolItem)
		{
			PersistentBoolData persistentBoolData2 = SceneData.instance.FindMyState(this.persistentBoolData);
			if (persistentBoolData2 != null && persistentBoolData2.activated)
			{
				flag = true;
			}
		}
		if (GameManager.instance.entryGateName != "dreamGate" && !flag && doorTrigger != "" && TransitionPoint.lastEntered != doorTrigger)
		{
			flag = true;
		}
		if (flag)
		{
			if (altSceneNameToLoad == "")
			{
				return;
			}
			loadAlt = true;
		}
		else if (saveStateData.id != "" && saveStateData.sceneName != "")
		{
			saveStateData.activated = true;
			SceneData.instance.SaveMyState(saveStateData);
		}
		if (loadInSequence)
		{
			additiveSceneLoads.Add(this);
		}
		else
		{
			StartCoroutine(LoadRoutine(callEvent: true));
		}
	}

	private void OnDisable()
	{
		if (sceneLoaded)
		{
			additiveSceneLoads.Remove(this);
			UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(SceneNameToLoad);
		}
	}

	public static IEnumerator LoadAll()
	{
		if (additiveSceneLoads != null)
		{
			foreach (SceneAdditiveLoadConditional additiveSceneLoad in additiveSceneLoads)
			{
				if ((bool)additiveSceneLoad)
				{
					yield return additiveSceneLoad.StartCoroutine(additiveSceneLoad.LoadRoutine());
				}
			}
		}
		loadInSequence = false;
	}

	private IEnumerator LoadRoutine(bool callEvent = false)
	{
		bool inSequence = loadInSequence;
		yield return null;
		yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(SceneNameToLoad, LoadSceneMode.Additive);
		Debug.Log("Additively loaded scene: " + SceneNameToLoad + (inSequence ? " in sequence" : " out of sequence"));
		sceneLoaded = true;
		if (callEvent && (bool)GameManager.instance)
		{
			GameManager.instance.LoadedBoss();
		}
	}
}
