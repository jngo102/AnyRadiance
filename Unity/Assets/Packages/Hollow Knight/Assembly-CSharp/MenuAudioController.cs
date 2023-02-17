using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAudioController : MonoBehaviour
{
	private AudioSource audioSource;

	[Header("Sound Effects")]
	public AudioClip select;

	public AudioClip submit;

	public AudioClip cancel;

	public AudioClip slider;

	public AudioClip startGame;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private IEnumerator Start()
	{
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Pre_Menu_Intro")
		{
			float startVol = audioSource.volume;
			audioSource.volume = 0f;
			yield return GameManager.instance.timeTool.TimeScaleIndependentWaitForSeconds(1f);
			audioSource.volume = startVol;
		}
	}

	public void PlaySelect()
	{
		if ((bool)select)
		{
			audioSource.PlayOneShot(select);
		}
	}

	public void PlaySubmit()
	{
		if ((bool)submit)
		{
			audioSource.PlayOneShot(submit);
		}
	}

	public void PlayCancel()
	{
		if ((bool)cancel)
		{
			audioSource.PlayOneShot(cancel);
		}
	}

	public void PlaySlider()
	{
		if ((bool)slider)
		{
			audioSource.PlayOneShot(slider);
		}
	}

	public void PlayStartGame()
	{
		if ((bool)startGame)
		{
			audioSource.PlayOneShot(startGame);
		}
	}
}
