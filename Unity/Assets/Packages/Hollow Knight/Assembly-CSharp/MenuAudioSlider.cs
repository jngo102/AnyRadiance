using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuAudioSlider : MonoBehaviour
{
	public enum AudioSettingType
	{
		MasterVolume,
		MusicVolume,
		SoundVolume
	}

	public Slider slider;

	public Text textUI;

	public AudioMixer masterMixer;

	public AudioSettingType audioSetting;

	private GameSettings gs;

	[SerializeField]
	private float value;

	private void Start()
	{
		gs = GameManager.instance.gameSettings;
		UpdateValue();
	}

	public void UpdateValue()
	{
		textUI.text = slider.value.ToString();
	}

	public void RefreshValueFromSettings()
	{
		if (gs == null)
		{
			gs = GameManager.instance.gameSettings;
		}
		if (audioSetting == AudioSettingType.MasterVolume)
		{
			slider.value = gs.masterVolume;
			UpdateValue();
		}
		else if (audioSetting == AudioSettingType.MusicVolume)
		{
			slider.value = gs.musicVolume;
			UpdateValue();
		}
		else if (audioSetting == AudioSettingType.SoundVolume)
		{
			slider.value = gs.soundVolume;
			UpdateValue();
		}
	}

	public void UpdateTextValue(float value)
	{
		textUI.text = value.ToString();
	}

	public void SetMasterLevel(float masterLevel)
	{
		float num = ((!(masterLevel > 9f)) ? GetVolumeLevel(masterLevel) : 0f);
		masterMixer.SetFloat("MasterVolume", num);
		gs.masterVolume = masterLevel;
	}

	public void SetMusicLevel(float musicLevel)
	{
		float num = ((!(musicLevel > 9f)) ? GetVolumeLevel(musicLevel) : 0f);
		masterMixer.SetFloat("MusicVolume", num);
		gs.musicVolume = musicLevel;
	}

	public void SetSoundLevel(float soundLevel)
	{
		float num = ((!(soundLevel > 9f)) ? GetVolumeLevel(soundLevel) : 0f);
		masterMixer.SetFloat("SFXVolume", num);
		gs.soundVolume = soundLevel;
	}

	private float GetVolumeLevel(float x)
	{
		return -1.02f * (x * x) + 17.5f * x - 76.6f;
	}
}
