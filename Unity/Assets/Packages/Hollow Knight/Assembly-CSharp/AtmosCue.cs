using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "NewAtmosCue", menuName = "Hollow Knight/Atmos Cue", order = 1000)]
public class AtmosCue : ScriptableObject
{
	[SerializeField]
	private AudioMixerSnapshot snapshot;

	[SerializeField]
	[ArrayForEnum(typeof(AtmosChannels))]
	private bool[] isChannelEnabled;

	public AudioMixerSnapshot Snapshot => snapshot;

	public bool IsChannelEnabled(AtmosChannels channel)
	{
		if (channel < AtmosChannels.CaveWind || (int)channel >= isChannelEnabled.Length)
		{
			return false;
		}
		return isChannelEnabled[(int)channel];
	}
}
