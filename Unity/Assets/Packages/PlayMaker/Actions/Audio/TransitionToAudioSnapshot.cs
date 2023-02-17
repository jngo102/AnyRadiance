// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// HutongGames.PlayMaker.Actions.TransitionToAudioSnapshot
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.Audio;

[ActionCategory(ActionCategory.Audio)]
[HutongGames.PlayMaker.Tooltip("Transition to an audio snapshot. Easy and fun.")]
public class TransitionToAudioSnapshot : ComponentAction<AudioSource>
{
	[ObjectType(typeof(AudioMixerSnapshot))]
	public FsmObject snapshot;

	public FsmFloat transitionTime;

	public override void Reset()
	{
		snapshot = null;
		transitionTime = 1f;
	}

	public override void OnEnter()
	{
		AudioMixerSnapshot audioMixerSnapshot = snapshot.Value as AudioMixerSnapshot;
		if (audioMixerSnapshot != null)
		{
			audioMixerSnapshot.TransitionTo(transitionTime.Value);
		}
		Finish();
	}
}
