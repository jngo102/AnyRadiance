using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMenu : MonoBehaviour
{
	protected IEnumerator Start()
	{
		yield return null;
		UIManager instance = UIManager.instance;
		if (instance != null)
		{
			UIManager.instance.AudioGoToGameplay(0f);
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		HeroController instance2 = HeroController.instance;
		if (instance2 != null)
		{
			UnityEngine.Object.Destroy(instance2.gameObject);
		}
		GameCameras instance3 = GameCameras.instance;
		if (instance3 != null)
		{
			UnityEngine.Object.Destroy(instance3.gameObject);
		}
		GameManager instance4 = GameManager.instance;
		if (instance4 != null)
		{
			try
			{
				ObjectPool.RecycleAll();
			}
			catch (Exception exception)
			{
				Debug.LogErrorFormat("Error while recycling all as part of quit, attempting to continue regardless.");
				Debug.LogException(exception);
			}
			instance4.playerData.Reset();
			instance4.sceneData.Reset();
			UnityEngine.Object.Destroy(instance4.gameObject);
		}
		TimeController.GenericTimeScale = 1f;
		BossSequenceController.Reset();
		BossStatueLoadManager.Clear();
		yield return null;
		GCManager.Collect();
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Menu_Title", LoadSceneMode.Single);
	}
}
