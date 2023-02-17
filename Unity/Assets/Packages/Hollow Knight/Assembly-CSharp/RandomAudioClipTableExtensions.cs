using UnityEngine;

public static class RandomAudioClipTableExtensions
{
	public static void PlayOneShot(this RandomAudioClipTable table, AudioSource audioSource)
	{
		if (!(table == null))
		{
			table.PlayOneShotUnsafe(audioSource);
		}
	}

	public static void SpawnAndPlayOneShot(this RandomAudioClipTable table, AudioSource prefab, Vector3 position)
	{
		if (!(table == null) && !(prefab == null))
		{
			AudioClip audioClip = table.SelectClip();
			if (!(audioClip == null))
			{
				AudioSource audioSource = prefab.Spawn();
				audioSource.transform.position = position;
				audioSource.pitch = table.SelectPitch();
				audioSource.volume = 1f;
				audioSource.PlayOneShot(audioClip);
			}
		}
	}
}
