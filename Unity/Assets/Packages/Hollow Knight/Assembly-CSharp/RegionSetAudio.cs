using UnityEngine;
using UnityEngine.Audio;

public class RegionSetAudio : MonoBehaviour
{
	public AudioMixerSnapshot atmosSnapshotEnter;

	public AudioMixerSnapshot enviroSnapshotEnter;

	public AudioMixerSnapshot atmosSnapshotExit;

	public AudioMixerSnapshot enviroSnapshotExit;

	public float transitionTime;

	private bool entered;

	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if (!entered)
		{
			if (atmosSnapshotEnter != null)
			{
				atmosSnapshotEnter.TransitionTo(transitionTime);
			}
			if (enviroSnapshotEnter != null)
			{
				enviroSnapshotEnter.TransitionTo(transitionTime);
			}
			entered = true;
		}
	}

	private void OnTriggerStay2D(Collider2D otherCollider)
	{
		if (!entered)
		{
			if (atmosSnapshotEnter != null)
			{
				atmosSnapshotEnter.TransitionTo(transitionTime);
			}
			if (enviroSnapshotEnter != null)
			{
				enviroSnapshotEnter.TransitionTo(transitionTime);
			}
			entered = true;
		}
	}

	private void OnTriggerExit2D(Collider2D otherCollider)
	{
		if (entered)
		{
			if (atmosSnapshotExit != null)
			{
				atmosSnapshotExit.TransitionTo(transitionTime);
			}
			if (enviroSnapshotExit != null)
			{
				enviroSnapshotExit.TransitionTo(transitionTime);
			}
			entered = false;
		}
	}
}
