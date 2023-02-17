using UnityEngine;

public class PlayAudioAndRecycle : MonoBehaviour
{
	public AudioSource audioSource;

	private void OnEnable()
	{
		audioSource.Play();
	}

	private void Update()
	{
		if (!audioSource.isPlaying)
		{
			base.gameObject.Recycle();
		}
	}
}
