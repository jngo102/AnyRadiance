using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class PlayAudioEvent : FsmStateAction
{
	[ObjectType(typeof(AudioClip))]
	public FsmObject audioClip;

	public FsmFloat pitchMin;

	public FsmFloat pitchMax;

	public FsmFloat volume;

	[ObjectType(typeof(AudioSource))]
	public FsmObject audioPlayerPrefab;

	public FsmOwnerDefault spawnPoint;

	public FsmVector3 spawnPosition;

	public override void Reset()
	{
		audioClip = null;
		pitchMin = 1f;
		pitchMax = 1f;
		volume = 1f;
	}

	public override void OnEnter()
	{
		AudioEvent audioEvent = default(AudioEvent);
		audioEvent.Clip = audioClip.Value as AudioClip;
		audioEvent.PitchMin = pitchMin.Value;
		audioEvent.PitchMax = pitchMax.Value;
		audioEvent.Volume = volume.Value;
		AudioEvent audioEvent2 = audioEvent;
		if ((bool)audioPlayerPrefab.Value)
		{
			Vector3 value = spawnPosition.Value;
			GameObject safe = spawnPoint.GetSafe(this);
			if ((bool)safe)
			{
				value += safe.transform.position;
			}
			audioEvent2.SpawnAndPlayOneShot(audioPlayerPrefab.Value as AudioSource, value);
		}
	}
}
