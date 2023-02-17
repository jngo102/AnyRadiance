using System.Collections;
using GlobalEnums;
using UnityEngine;

public class AutoRecycleSelf : MonoBehaviour
{
	[Header("Trigger Event Type")]
	public AfterEvent afterEvent;

	[Header("Time Event Settings")]
	public float timeToWait;

	private AudioSource audioSource;

	private bool validAudioSource;

	private bool ApplicationIsQuitting;

	private void OnEnable()
	{
		if (afterEvent == AfterEvent.TIME)
		{
			if (timeToWait > 0f)
			{
				StartCoroutine(StartTimer(timeToWait));
			}
		}
		else if (afterEvent == AfterEvent.LEVEL_UNLOAD)
		{
			GameManager.instance.DestroyPersonalPools += RecycleSelf;
		}
		else if (afterEvent == AfterEvent.AUDIO_CLIP_END)
		{
			audioSource = GetComponent<AudioSource>();
			if (audioSource == null)
			{
				Debug.LogError(base.name + " requires an AudioSource to auto-recycle itself.");
				validAudioSource = false;
			}
			else
			{
				validAudioSource = true;
			}
		}
	}

	private void Update()
	{
		if (Time.frameCount % 20 == 0)
		{
			Update20();
		}
	}

	private void Update20()
	{
		if (validAudioSource && !audioSource.isPlaying)
		{
			RecycleSelf();
		}
	}

	private void OnDisable()
	{
		if (afterEvent == AfterEvent.LEVEL_UNLOAD && !ApplicationIsQuitting)
		{
			GameManager.instance.DestroyPersonalPools -= RecycleSelf;
		}
	}

	private void OnApplicationQuit()
	{
		ApplicationIsQuitting = true;
	}

	private IEnumerator StartTimer(float wait)
	{
		yield return new WaitForSeconds(wait);
		gameObject.Recycle();
	}

	private void RecycleSelf()
	{
		base.gameObject.Recycle();
	}
}
