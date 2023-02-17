using System.Collections;
using GlobalEnums;
using UnityEngine;

public class StagTravel : MonoBehaviour
{
	private class StagTravelAsyncLoadInfo : GameManager.SceneLoadInfo
	{
		private StagTravel stagTravel;

		public StagTravelAsyncLoadInfo(StagTravel stagTravel)
		{
			this.stagTravel = stagTravel;
		}

		public override void NotifyFetchComplete()
		{
			base.NotifyFetchComplete();
			stagTravel.NotifyFetchComplete();
		}

		public override bool IsReadyToActivate()
		{
			if (base.IsReadyToActivate())
			{
				return stagTravel.IsReadyToActivate;
			}
			return false;
		}

		public override void NotifyFinished()
		{
			base.NotifyFinished();
			GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE SCENE OUT INSTANT");
			GameCameras.instance.EnableImageEffects(isGameplayLevel: true, isBloomForced: false);
		}
	}

	[SerializeField]
	private CinematicSequence cinematicSequence;

	[SerializeField]
	private float minimumDuration;

	[SerializeField]
	private float fadeRate;

	private bool isAsync;

	private float currentDuration;

	private bool isFetchComplete;

	private bool isReadyToActivate;

	private bool isSkipping;

	private bool isSkipFadeComplete;

	protected bool IsReadyToActivate => isReadyToActivate;

	protected IEnumerator Start()
	{
		isAsync = Platform.Current.FetchScenesBeforeFade;
		GameCameras.instance.DisableImageEffects();
		if (!isAsync)
		{
			GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE SCENE IN");
			GameManager.instance.inputHandler.SetSkipMode(SkipPromptMode.SKIP_INSTANT);
			cinematicSequence.IsLooping = false;
			cinematicSequence.Begin();
			while (cinematicSequence.IsPlaying && !isSkipFadeComplete && cinematicSequence.VideoPlayer != null && cinematicSequence.VideoPlayer.CurrentTime < 3.9f)
			{
				yield return null;
			}
			GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE OUT INSTANT");
			yield return null;
			GameManager.instance.BeginSceneTransition(new GameManager.SceneLoadInfo
			{
				EntryGateName = "door_stagExit",
				SceneName = GameManager.instance.playerData.GetString("nextScene"),
				PreventCameraFadeOut = true,
				Visualization = GameManager.SceneLoadVisualizations.Custom
			});
			isReadyToActivate = true;
		}
		else
		{
			StartCoroutine("FadeInRoutine");
			cinematicSequence.IsLooping = true;
			cinematicSequence.Begin();
			GameManager.instance.BeginSceneTransition(new StagTravelAsyncLoadInfo(this)
			{
				EntryGateName = "door_stagExit",
				SceneName = GameManager.instance.playerData.GetString("nextScene"),
				PreventCameraFadeOut = true,
				Visualization = GameManager.SceneLoadVisualizations.Custom
			});
		}
	}

	private IEnumerator FadeInRoutine()
	{
		GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE OUT INSTANT");
		yield return new WaitForSeconds(1.5f);
		GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE SCENE IN");
	}

	protected void Update()
	{
		currentDuration += Time.unscaledDeltaTime;
		if (isAsync && !isSkipping && isFetchComplete && currentDuration > minimumDuration)
		{
			StartCoroutine(Skip());
		}
	}

	protected void NotifyFetchComplete()
	{
		isFetchComplete = true;
	}

	public IEnumerator Skip()
	{
		if (!isSkipping)
		{
			StopCoroutine("FadeInRoutine");
			isSkipping = true;
			GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE OUT");
			yield return new WaitForSecondsRealtime(0.5f);
			isSkipFadeComplete = true;
			isReadyToActivate = true;
		}
	}
}
