using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossDoorChallengeCompleteUI : MonoBehaviour
{
	[Serializable]
	public class BindingIcon
	{
		public Image icon;

		public Sprite allUnlockedSprite;

		public GameObject[] flashEffects;

		private bool alreadyVisible;

		public void SetAlreadyVisible(bool value, bool allUnlocked)
		{
			GameObject[] array = flashEffects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: false);
			}
			if ((bool)icon)
			{
				icon.enabled = value;
			}
			alreadyVisible = value;
			if (allUnlocked)
			{
				SetAllUnlocked();
			}
		}

		public IEnumerator DoAppearAnim(float appearDelay)
		{
			if (!alreadyVisible && (bool)icon)
			{
				icon.enabled = false;
			}
			GameObject[] array = flashEffects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(value: true);
			}
			yield return new WaitForSeconds(appearDelay);
			if ((bool)icon)
			{
				icon.enabled = true;
			}
		}

		public IEnumerator DoAllAppearAnim(float appearDelay)
		{
			GameObject[] array = flashEffects;
			foreach (GameObject obj in array)
			{
				obj.SetActive(value: false);
				obj.SetActive(value: true);
			}
			yield return new WaitForSeconds(appearDelay);
			SetAllUnlocked();
		}

		private void SetAllUnlocked()
		{
			if ((bool)icon && (bool)allUnlockedSprite)
			{
				icon.sprite = allUnlockedSprite;
			}
		}
	}

	public float achievementShowDelay = 0.5f;

	public Animator animator;

	public float appearAnimDelay = 2f;

	public float appearEndWaitTime = 1f;

	public float bindingCapAnimDelay = 0.5f;

	public float bindingCapAppearDelay = 0.2f;

	public float completionCapAppearDelay = 0.75f;

	public float endAnimDelay = 2f;

	public AudioSource musicSource;

	public float musicDelay = 1f;

	[Space]
	public BindingIcon bindingCapNail;

	public BindingIcon bindingCapShell;

	public BindingIcon bindingCapCharm;

	public BindingIcon bindingCapSoul;

	public AudioSource audioSourcePrefab;

	public AudioEvent screenAppearSound;

	public AudioEvent bindingAppearSound;

	public float bindingAppearPitchIncrease = 0.05f;

	public AudioEvent bindingAllAppearSound;

	public AudioEvent coreAppearSound;

	[Space]
	public GameObject[] coreFlashEffects;

	public GameObject completeCore;

	public GameObject allBindingsCore;

	public GameObject noHitsCore;

	public GameObject allBindingsNoHitsCore;

	[Space]
	public CanvasGroup timerGroup;

	public float timerFadeDelay = 1f;

	public float timerFadeTime = 2f;

	public Text timerText;

	private bool waitingForInput;

	private void Start()
	{
		StartCoroutine(Sequence());
		StartCoroutine(ShowAchievements());
	}

	private void Update()
	{
		if (waitingForInput && (InputHandler.Instance.gameController.AnyButtonWasPressed || Input.anyKeyDown))
		{
			waitingForInput = false;
		}
	}

	private IEnumerator ShowAchievements()
	{
		yield return new WaitForSeconds(achievementShowDelay);
		GameManager.instance.AwardQueuedAchievements();
	}

	private IEnumerator Sequence()
	{
		GameObject[] array = coreFlashEffects;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].SetActive(value: false);
		}
		BossSequenceDoor.Completion completion = (BossSequenceController.IsInSequence ? BossSequenceController.PreviousCompletion : BossSequenceDoor.Completion.None);
		bool boundNail = !BossSequenceController.IsInSequence || BossSequenceController.BoundNail;
		bool boundShell = !BossSequenceController.IsInSequence || BossSequenceController.BoundShell;
		bool boundCharms = !BossSequenceController.IsInSequence || BossSequenceController.BoundCharms;
		bool boundSoul = !BossSequenceController.IsInSequence || BossSequenceController.BoundSoul;
		bool knightDamaged = !BossSequenceController.IsInSequence || BossSequenceController.KnightDamaged;
		if ((bool)completeCore)
		{
			completeCore.SetActive(value: false);
		}
		if ((bool)allBindingsCore)
		{
			allBindingsCore.SetActive(completion.allBindings);
		}
		if ((bool)noHitsCore)
		{
			noHitsCore.SetActive(completion.noHits && !completion.allBindings);
		}
		if ((bool)allBindingsNoHitsCore)
		{
			allBindingsNoHitsCore.SetActive(completion.noHits && completion.allBindings);
		}
		if ((bool)timerGroup)
		{
			timerGroup.alpha = 0f;
		}
		for (int k = 0; k < 4; k++)
		{
			BindingIcon bindingIcon = null;
			bool value = false;
			switch (k)
			{
			case 0:
				bindingIcon = bindingCapNail;
				value = completion.boundNail;
				break;
			case 1:
				bindingIcon = bindingCapShell;
				value = completion.boundShell;
				break;
			case 2:
				bindingIcon = bindingCapCharm;
				value = completion.boundCharms;
				break;
			case 3:
				bindingIcon = bindingCapSoul;
				value = completion.boundSoul;
				break;
			}
			bindingIcon?.SetAlreadyVisible(value, completion.allBindings);
		}
		yield return new WaitForSeconds(musicDelay);
		if ((bool)musicSource)
		{
			musicSource.Play();
		}
		yield return new WaitForSeconds(appearAnimDelay - musicDelay);
		if ((bool)animator)
		{
			animator.Play("Appear");
			yield return null;
			yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + appearEndWaitTime);
		}
		for (int i = 0; i < 4; i++)
		{
			BindingIcon bindingIcon2 = GetBindingIcon(i);
			if (bindingIcon2 != null)
			{
				StartCoroutine(bindingIcon2.DoAppearAnim(bindingCapAppearDelay));
				float num = (float)i * bindingAppearPitchIncrease;
				AudioEvent audioEvent = default(AudioEvent);
				audioEvent.Clip = bindingAppearSound.Clip;
				audioEvent.PitchMin = bindingAppearSound.PitchMin + num;
				audioEvent.PitchMax = bindingAppearSound.PitchMax + num;
				audioEvent.Volume = bindingAppearSound.Volume;
				AudioEvent audioEvent2 = audioEvent;
				audioEvent2.SpawnAndPlayOneShot(audioSourcePrefab, transform.position);
				yield return new WaitForSeconds(bindingCapAnimDelay);
			}
		}
		bool allBindings = boundNail && boundShell && boundCharms && boundSoul;
		if (allBindings)
		{
			for (int l = 0; l < 4; l++)
			{
				BindingIcon bindingIcon3 = GetBindingIcon(l);
				if (bindingIcon3 != null)
				{
					StartCoroutine(bindingIcon3.DoAllAppearAnim(bindingCapAppearDelay));
				}
			}
			bindingAllAppearSound.SpawnAndPlayOneShot(audioSourcePrefab, transform.position);
		}
		yield return new WaitForSeconds(completionCapAppearDelay);
		array = coreFlashEffects;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].SetActive(value: true);
		}
		coreAppearSound.SpawnAndPlayOneShot(audioSourcePrefab, transform.position);
		yield return new WaitForSeconds(bindingCapAppearDelay);
		if ((bool)completeCore)
		{
			completeCore.SetActive(!allBindings);
		}
		if ((bool)allBindingsCore && allBindings)
		{
			allBindingsCore.SetActive(value: true);
		}
		if ((bool)noHitsCore && !knightDamaged && !allBindings)
		{
			noHitsCore.SetActive(value: true);
		}
		if ((bool)allBindingsNoHitsCore && !knightDamaged && allBindings)
		{
			allBindingsNoHitsCore.SetActive(value: true);
		}
		if ((bool)timerText)
		{
			int num2 = Mathf.RoundToInt(BossSequenceController.Timer);
			timerText.text = $"{num2 / 60:00}:{num2 % 60:00}";
			if ((bool)timerGroup)
			{
				yield return new WaitForSeconds(timerFadeDelay);
				for (float elapsed = 0f; elapsed <= timerFadeTime; elapsed += Time.deltaTime)
				{
					timerGroup.alpha = elapsed / timerFadeTime;
					yield return null;
				}
			}
		}
		yield return new WaitForSeconds(endAnimDelay);
		waitingForInput = true;
		while (waitingForInput)
		{
			yield return null;
		}
		if ((bool)animator)
		{
			animator.Play("Disappear");
			yield return null;
			yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
		}
		HeroController.instance.EnterWithoutInput(flag: true);
		StaticVariableList.SetValue("finishedBossReturning", value: true);
		GameCameras.instance.cameraFadeFSM.SendEvent("FADE OUT INSTANT");
		yield return null;
		BossSequenceController.RestoreBindings();
		GameManager.instance.BeginSceneTransition(new GameManager.SceneLoadInfo
		{
			SceneName = (BossSequenceController.ShouldUnlockGGMode ? "GG_Unlock" : GameManager.instance.playerData.GetString("dreamReturnScene")),
			EntryGateName = GameManager.instance.playerData.GetString("bossReturnEntryGate"),
			EntryDelay = 0f,
			Visualization = GameManager.SceneLoadVisualizations.Dream,
			PreventCameraFadeOut = true,
			WaitForSceneTransitionCameraFade = false,
			AlwaysUnloadUnusedAssets = true
		});
	}

	private BindingIcon GetBindingIcon(int index)
	{
		BindingIcon result = null;
		switch (index)
		{
		case 0:
			if (BossSequenceController.BoundNail || !BossSequenceController.IsInSequence)
			{
				result = bindingCapNail;
			}
			break;
		case 1:
			if (BossSequenceController.BoundShell || !BossSequenceController.IsInSequence)
			{
				result = bindingCapShell;
			}
			break;
		case 2:
			if (BossSequenceController.BoundCharms || !BossSequenceController.IsInSequence)
			{
				result = bindingCapCharm;
			}
			break;
		case 3:
			if (BossSequenceController.BoundSoul || !BossSequenceController.IsInSequence)
			{
				result = bindingCapSoul;
			}
			break;
		}
		return result;
	}
}
