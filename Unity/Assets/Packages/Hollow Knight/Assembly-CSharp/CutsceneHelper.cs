using System.Collections;
using GlobalEnums;
using UnityEngine;

public class CutsceneHelper : MonoBehaviour
{
	public enum NextScene
	{
		SpecifyScene,
		MainMenu,
		PermaDeathUnlock,
		GameCompletionScreen,
		EndCredits,
		MrMushroomUnlock,
		GGReturn,
		MainMenuNoSave
	}

	public float waitBeforeFadeIn;

	public CameraFadeInType fadeInSpeed;

	public SkipPromptMode skipMode;

	[Tooltip("Prevents the skip action from taking place until the lock is released. Useful for animators delaying skip feature.")]
	public bool startSkipLocked;

	[Tooltip("Reset any flags that may have been previously set.")]
	public bool resetOnStart;

	public NextScene nextSceneType;

	public string nextScene;

	private GameManager gm;

	private IEnumerator Start()
	{
		gm = GameManager.instance;
		if (resetOnStart)
		{
			gm.inputHandler.skippingCutscene = false;
		}
		if (startSkipLocked)
		{
			gm.inputHandler.SetSkipMode(SkipPromptMode.NOT_SKIPPABLE);
		}
		else
		{
			gm.inputHandler.SetSkipMode(skipMode);
		}
		GameCameras.instance.DisableHUDCamIfAllowed();
		yield return new WaitForSeconds(waitBeforeFadeIn);
		if (fadeInSpeed == CameraFadeInType.SLOW)
		{
			GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE SCENE IN SLOWLY");
		}
		else if (fadeInSpeed == CameraFadeInType.NORMAL)
		{
			GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE SCENE IN");
		}
		else if (fadeInSpeed == CameraFadeInType.INSTANT)
		{
			GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE SCENE IN INSTANT");
		}
	}

	public void LoadNextScene()
	{
		GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE INSTANT");
		DoSceneLoad();
	}

	public IEnumerator SkipCutscene()
	{
		PlayMakerFSM.BroadcastEvent("JUST FADE");
		yield return new WaitForSeconds(0.5f);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		DoSceneLoad();
	}

	public void UnlockSkip()
	{
		gm.inputHandler.SetSkipMode(skipMode);
	}

	private void DoSceneLoad()
	{
		switch (nextSceneType)
		{
		case NextScene.SpecifyScene:
			GameManager.instance.LoadScene(nextScene);
			break;
		case NextScene.MainMenu:
			GameManager.instance.StartCoroutine(GameManager.instance.ReturnToMainMenu(GameManager.ReturnToMainMenuSaveModes.SaveAndContinueOnFail));
			break;
		case NextScene.PermaDeathUnlock:
			GameManager.instance.LoadPermadeathUnlockScene();
			break;
		case NextScene.GameCompletionScreen:
			GameManager.instance.LoadScene("End_Game_Completion");
			break;
		case NextScene.EndCredits:
			GameManager.instance.LoadScene("End_Credits");
			break;
		case NextScene.MrMushroomUnlock:
			GameManager.instance.LoadMrMushromScene();
			break;
		case NextScene.GGReturn:
			GameManager.instance.BeginSceneTransition(new GameManager.SceneLoadInfo
			{
				SceneName = nextScene,
				EntryGateName = GameManager.instance.playerData.GetString("bossReturnEntryGate"),
				EntryDelay = 0f,
				Visualization = GameManager.SceneLoadVisualizations.Dream,
				PreventCameraFadeOut = true,
				WaitForSceneTransitionCameraFade = false
			});
			break;
		case NextScene.MainMenuNoSave:
			GameManager.instance.StartCoroutine(GameManager.instance.ReturnToMainMenu(GameManager.ReturnToMainMenuSaveModes.DontSave));
			break;
		}
	}
}
