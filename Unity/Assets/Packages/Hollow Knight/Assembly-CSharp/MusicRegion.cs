using System.Collections;
using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.Audio;

public class MusicRegion : MonoBehaviour
{
	public bool dirtmouth;

	public bool minesDelay;

	[Space]
	public MusicCue enterMusicCue;

	public AudioMixerSnapshot enterMusicSnapshot;

	public string enterTrackEvent;

	public float enterTransitionTime;

	[Space]
	public MusicCue exitMusicCue;

	public AudioMixerSnapshot exitMusicSnapshot;

	public string exitTrackEvent;

	public float exitTransitionTime;

	private Coroutine fadeInRoutine;

	private void Reset()
	{
		PlayMakerFSM component = GetComponent<PlayMakerFSM>();
		if ((bool)component && component.FsmName == "Music Region")
		{
			FsmVariables fsmVariables = component.FsmVariables;
			dirtmouth = fsmVariables.GetFsmBool("Dirtmouth").Value;
			minesDelay = fsmVariables.GetFsmBool("Mines Delay").Value;
			enterMusicCue = fsmVariables.GetFsmObject("Enter Music Cue").Value as MusicCue;
			enterMusicSnapshot = fsmVariables.GetFsmObject("Enter Music Snapshot").Value as AudioMixerSnapshot;
			enterTrackEvent = fsmVariables.GetFsmString("Enter Track Event").Value;
			enterTransitionTime = fsmVariables.GetFsmFloat("Enter Transition Time").Value;
			exitMusicCue = fsmVariables.GetFsmObject("Exit Music Cue").Value as MusicCue;
			exitMusicSnapshot = fsmVariables.GetFsmObject("Exit Music Snapshot").Value as AudioMixerSnapshot;
			exitTrackEvent = fsmVariables.GetFsmString("Exit Track Event").Value;
			exitTransitionTime = fsmVariables.GetFsmFloat("Exit Transition Time").Value;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 9)
		{
			if (fadeInRoutine != null)
			{
				StopCoroutine(fadeInRoutine);
			}
			fadeInRoutine = StartCoroutine(FadeIn());
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 9)
		{
			if (fadeInRoutine != null)
			{
				StopCoroutine(fadeInRoutine);
			}
			FadeOut();
		}
	}

	private IEnumerator FadeIn()
	{
		if (minesDelay)
		{
			yield return new WaitForSeconds(2.35f);
		}
		float timeToReach = enterTransitionTime;
		if (dirtmouth)
		{
			MusicCue currentMusicCue = GameManager.instance.AudioManager.CurrentMusicCue;
			if ((currentMusicCue ? currentMusicCue.name : "") != "Dirtmouth")
			{
				timeToReach = 1f;
			}
		}
		if (enterMusicSnapshot != null)
		{
			enterMusicSnapshot.TransitionTo(timeToReach);
		}
		if ((bool)enterMusicCue)
		{
			GameManager.instance.AudioManager.ApplyMusicCue(enterMusicCue, 0f, 0f, applySnapshot: false);
		}
		fadeInRoutine = null;
	}

	private void FadeOut()
	{
		if (exitMusicSnapshot != null)
		{
			exitMusicSnapshot.TransitionTo(exitTransitionTime);
		}
		if ((bool)exitMusicCue)
		{
			GameManager.instance.AudioManager.ApplyMusicCue(exitMusicCue, 0f, 0f, applySnapshot: false);
		}
	}
}
