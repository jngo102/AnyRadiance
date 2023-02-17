using System;
using System.Collections;
using GlobalEnums;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	
	[Serializable]
	public class SaveSlotButton : MenuButton, ISelectHandler, IEventSystemHandler, IDeselectHandler, ISubmitHandler, IPointerClickHandler
	{
		public enum SaveFileStates
		{
			NotStarted,
			OperationInProgress,
			Empty,
			LoadedStats,
			Corrupted
		}
	
		public enum SaveSlot
		{
			SLOT_1,
			SLOT_2,
			SLOT_3,
			SLOT_4
		}
	
		public enum SlotState
		{
			HIDDEN,
			OPERATION_IN_PROGRESS,
			EMPTY_SLOT,
			SAVE_PRESENT,
			CORRUPTED,
			CLEAR_PROMPT,
			DEFEATED
		}
	
		private bool verboseMode;
	
		[Header("Slot Number")]
		public SaveSlot saveSlot;
	
		[Header("Animation")]
		public Animator topFleur;
	
		public Animator highlight;
	
		[Header("Canvas Groups")]
		public CanvasGroup newGameText;
	
		public CanvasGroup saveCorruptedText;
	
		public CanvasGroup loadingText;
	
		public CanvasGroup activeSaveSlot;
	
		public CanvasGroup clearSaveButton;
	
		public CanvasGroup clearSavePrompt;
	
		public CanvasGroup backgroundCg;
	
		public CanvasGroup slotNumberText;
	
		public CanvasGroup myCanvasGroup;
	
		public CanvasGroup defeatedText;
	
		public CanvasGroup defeatedBackground;
	
		public CanvasGroup brokenSteelOrb;
	
		[Header("Text Elements")]
		public Text geoText;
	
		public Text locationText;
	
		public Text playTimeText;
	
		public Text completionText;
	
		[Header("Soul Orbs")]
		public CanvasGroup normalSoulOrbCg;
	
		public CanvasGroup hardcoreSoulOrbCg;
	
		public CanvasGroup ggSoulOrbCg;
	
		[Header("Visual Elements")]
		public Image background;
	
		public Image soulOrbIcon;
	
		public SaveProfileHealthBar healthSlots;
	
		public Image geoIcon;
	
		public SaveProfileMPSlots mpSlots;
	
		public SaveSlotBackgrounds saveSlots;
	
		[Header("Raycast Blocker")]
		public GameObject clearSaveBlocker;
	
		private GameManager gm;
	
		private UIManager ui;
	
		private InputHandler ih;
	
		public SaveFileStates saveFileState;
	
		private PreselectOption clearSavePromptHighlight;
	
		[SerializeField]
		private SaveStats saveStats;
	
		private Navigation noNav;
	
		private Navigation fullSlotNav;
	
		private Navigation emptySlotNav;
	
		private IEnumerator currentLoadingTextFadeIn;
	
		private bool didLoadSaveStats;
	
		private CoroutineQueue coroutineQueue;
	
		public SlotState state { get; private set; }
	
		private int SaveSlotIndex => saveSlot switch
		{
			SaveSlot.SLOT_1 => 1, 
			SaveSlot.SLOT_2 => 2, 
			SaveSlot.SLOT_3 => 3, 
			SaveSlot.SLOT_4 => 4, 
			_ => 0, 
		};
	
		private new void Awake()
		{
			gm = GameManager.instance;
			clearSavePromptHighlight = clearSavePrompt.GetComponent<PreselectOption>();
			coroutineQueue = new CoroutineQueue(gm);
			SetupNavs();
		}
	
		private new void OnEnable()
		{
			if (saveStats != null && saveFileState == SaveFileStates.LoadedStats)
			{
				PresentSaveSlot(saveStats);
			}
		}
	
		private new void Start()
		{
			if (Application.isPlaying)
			{
				ui = UIManager.instance;
				ih = gm.inputHandler;
				HookUpAudioPlayer();
			}
		}
	
		public void Prepare(GameManager gameManager, bool isReload = false)
		{
			if (saveFileState != 0 && (!isReload || saveFileState != SaveFileStates.Corrupted))
			{
				return;
			}
			ChangeSaveFileState(SaveFileStates.OperationInProgress);
			Platform.Current.IsSaveSlotInUse(SaveSlotIndex, delegate(bool fileExists)
			{
				if (!fileExists)
				{
					ChangeSaveFileState(SaveFileStates.Empty);
				}
				else
				{
					gameManager.GetSaveStatsForSlot(SaveSlotIndex, delegate(SaveStats saveStats)
					{
						if (saveStats == null)
						{
							ChangeSaveFileState(SaveFileStates.Corrupted);
						}
						else
						{
							this.saveStats = saveStats;
							ChangeSaveFileState(SaveFileStates.LoadedStats);
						}
					});
				}
			});
		}
	
		public void ClearCache()
		{
			saveFileState = SaveFileStates.NotStarted;
			saveStats = null;
		}
	
		private void ChangeSaveFileState(SaveFileStates nextSaveFileState)
		{
			saveFileState = nextSaveFileState;
			if (base.isActiveAndEnabled)
			{
				ShowRelevantModeForSaveFileState();
			}
		}
	
		public new void OnSubmit(BaseEventData eventData)
		{
			if (saveFileState == SaveFileStates.LoadedStats)
			{
				if (saveStats.permadeathMode == 2)
				{
					ForceDeselect();
					ClearSavePrompt();
				}
				else
				{
					gm.LoadGameFromUI(SaveSlotIndex);
				}
				base.OnSubmit(eventData);
			}
			else if (saveFileState == SaveFileStates.Empty)
			{
				gm.profileID = SaveSlotIndex;
				if (gm.GetStatusRecordInt("RecPermadeathMode") == 1 || gm.GetStatusRecordInt("RecBossRushMode") == 1)
				{
					ui.UIGoToPlayModeMenu();
				}
				else
				{
					ui.StartNewGame();
				}
				base.OnSubmit(eventData);
			}
			else if (saveFileState == SaveFileStates.Corrupted)
			{
				StartCoroutine(ReloadCorrupted());
			}
		}
	
		protected IEnumerator ReloadCorrupted()
		{
			ih.StopUIInput();
			Prepare(gm, isReload: true);
			while (saveFileState == SaveFileStates.OperationInProgress)
			{
				yield return null;
			}
			ih.StartUIInput();
		}
	
		public new void OnPointerClick(PointerEventData eventData)
		{
			OnSubmit(eventData);
		}
	
		public new void OnSelect(BaseEventData eventData)
		{
			highlight.ResetTrigger("hide");
			highlight.SetTrigger("show");
			if (leftCursor != null)
			{
				leftCursor.ResetTrigger("hide");
				leftCursor.SetTrigger("show");
			}
			if (rightCursor != null)
			{
				rightCursor.ResetTrigger("hide");
				rightCursor.SetTrigger("show");
			}
			base.OnSelect(eventData);
			if (!base.interactable)
			{
				try
				{
					uiAudioPlayer.PlaySelect();
				}
				catch (Exception ex)
				{
					Debug.LogError(base.name + " doesn't have a select sound specified. " + ex);
				}
			}
		}
	
		public new void OnDeselect(BaseEventData eventData)
		{
			StartCoroutine(ValidateDeselect());
		}
	
		public void ShowRelevantModeForSaveFileState()
		{
			switch (saveFileState)
			{
			case SaveFileStates.Empty:
				coroutineQueue.Enqueue(AnimateToSlotState(SlotState.EMPTY_SLOT));
				break;
			default:
				coroutineQueue.Enqueue(AnimateToSlotState(SlotState.OPERATION_IN_PROGRESS));
				break;
			case SaveFileStates.Corrupted:
				coroutineQueue.Enqueue(AnimateToSlotState(SlotState.CORRUPTED));
				break;
			case SaveFileStates.LoadedStats:
				if (saveStats.permadeathMode == 2)
				{
					coroutineQueue.Enqueue(AnimateToSlotState(SlotState.DEFEATED));
				}
				else
				{
					coroutineQueue.Enqueue(AnimateToSlotState(SlotState.SAVE_PRESENT));
				}
				break;
			}
		}
	
		public void HideSaveSlot()
		{
			coroutineQueue.Enqueue(AnimateToSlotState(SlotState.HIDDEN));
		}
	
		public void ClearSavePrompt()
		{
			coroutineQueue.Enqueue(AnimateToSlotState(SlotState.CLEAR_PROMPT));
		}
	
		public void CancelClearSave()
		{
			if (state == SlotState.CLEAR_PROMPT)
			{
				ShowRelevantModeForSaveFileState();
			}
		}
	
		public void ClearSaveFile()
		{
			gm.ClearSaveFile(SaveSlotIndex, delegate
			{
			});
			saveStats = null;
			ChangeSaveFileState(SaveFileStates.Empty);
		}
	
		private IEnumerator FadeInCanvasGroupAfterDelay(float delay, CanvasGroup cg)
		{
			for (float timer = 0f; timer < delay; timer += Time.unscaledDeltaTime)
			{
				yield return null;
			}
			yield return ui.FadeInCanvasGroup(cg);
		}
	
		private IEnumerator AnimateToSlotState(SlotState nextState)
		{
			SlotState slotState = state;
			if (slotState == nextState)
			{
				yield break;
			}
			if (currentLoadingTextFadeIn != null)
			{
				StopCoroutine(currentLoadingTextFadeIn);
				currentLoadingTextFadeIn = null;
			}
			if (verboseMode)
			{
				Debug.LogFormat("{0} SetState: {1} -> {2}", name, state, nextState);
			}
			state = nextState;
			switch (nextState)
			{
			case SlotState.HIDDEN:
			case SlotState.OPERATION_IN_PROGRESS:
				navigation = noNav;
				break;
			case SlotState.EMPTY_SLOT:
				navigation = emptySlotNav;
				break;
			case SlotState.SAVE_PRESENT:
			case SlotState.CORRUPTED:
			case SlotState.DEFEATED:
				navigation = fullSlotNav;
				break;
			}
			switch (slotState)
			{
			case SlotState.HIDDEN:
				switch (nextState)
				{
				case SlotState.OPERATION_IN_PROGRESS:
					topFleur.ResetTrigger("hide");
					topFleur.SetTrigger("show");
					yield return new WaitForSeconds(0.2f);
					StartCoroutine(currentLoadingTextFadeIn = FadeInCanvasGroupAfterDelay(5f, loadingText));
					break;
				case SlotState.EMPTY_SLOT:
					topFleur.ResetTrigger("hide");
					topFleur.SetTrigger("show");
					yield return new WaitForSeconds(0.2f);
					StartCoroutine(ui.FadeInCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeInCanvasGroup(newGameText));
					break;
				case SlotState.SAVE_PRESENT:
					topFleur.ResetTrigger("hide");
					topFleur.SetTrigger("show");
					yield return new WaitForSeconds(0.2f);
					StartCoroutine(ui.FadeInCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeInCanvasGroup(backgroundCg));
					StartCoroutine(ui.FadeInCanvasGroup(activeSaveSlot));
					StartCoroutine(ui.FadeInCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = true;
					break;
				case SlotState.DEFEATED:
					topFleur.ResetTrigger("hide");
					topFleur.SetTrigger("show");
					yield return new WaitForSeconds(0.2f);
					StartCoroutine(ui.FadeInCanvasGroup(defeatedBackground));
					StartCoroutine(ui.FadeInCanvasGroup(defeatedText));
					StartCoroutine(ui.FadeInCanvasGroup(brokenSteelOrb));
					StartCoroutine(ui.FadeInCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = true;
					myCanvasGroup.blocksRaycasts = true;
					break;
				case SlotState.CORRUPTED:
					topFleur.ResetTrigger("hide");
					topFleur.SetTrigger("show");
					yield return new WaitForSeconds(0.2f);
					StartCoroutine(ui.FadeInCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeInCanvasGroup(saveCorruptedText));
					StartCoroutine(ui.FadeInCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = true;
					myCanvasGroup.blocksRaycasts = true;
					break;
				}
				break;
			case SlotState.OPERATION_IN_PROGRESS:
				switch (nextState)
				{
				case SlotState.EMPTY_SLOT:
					yield return StartCoroutine(ui.FadeOutCanvasGroup(loadingText));
					StartCoroutine(ui.FadeInCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeInCanvasGroup(newGameText));
					break;
				case SlotState.SAVE_PRESENT:
					yield return StartCoroutine(ui.FadeOutCanvasGroup(loadingText));
					if (saveStats != null)
					{
						PresentSaveSlot(saveStats);
					}
					StartCoroutine(ui.FadeInCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeInCanvasGroup(backgroundCg));
					StartCoroutine(ui.FadeInCanvasGroup(activeSaveSlot));
					StartCoroutine(ui.FadeInCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = true;
					break;
				case SlotState.DEFEATED:
					yield return StartCoroutine(ui.FadeOutCanvasGroup(loadingText));
					StartCoroutine(ui.FadeInCanvasGroup(defeatedBackground));
					StartCoroutine(ui.FadeInCanvasGroup(defeatedText));
					StartCoroutine(ui.FadeInCanvasGroup(brokenSteelOrb));
					StartCoroutine(ui.FadeInCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = true;
					myCanvasGroup.blocksRaycasts = true;
					break;
				case SlotState.CORRUPTED:
					yield return StartCoroutine(ui.FadeOutCanvasGroup(loadingText));
					StartCoroutine(ui.FadeInCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeInCanvasGroup(saveCorruptedText));
					StartCoroutine(ui.FadeInCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = true;
					myCanvasGroup.blocksRaycasts = true;
					break;
				}
				break;
			case SlotState.SAVE_PRESENT:
				switch (nextState)
				{
				case SlotState.CLEAR_PROMPT:
					ih.StopUIInput();
					interactable = false;
					myCanvasGroup.blocksRaycasts = false;
					StartCoroutine(ui.FadeOutCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeOutCanvasGroup(activeSaveSlot));
					StartCoroutine(ui.FadeOutCanvasGroup(backgroundCg));
					yield return StartCoroutine(ui.FadeOutCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = false;
					clearSaveBlocker.SetActive(value: true);
					yield return StartCoroutine(ui.FadeInCanvasGroup(clearSavePrompt));
					clearSavePrompt.interactable = true;
					clearSavePrompt.blocksRaycasts = true;
					clearSavePromptHighlight.HighlightDefault();
					ih.StartUIInput();
					break;
				case SlotState.HIDDEN:
					topFleur.ResetTrigger("show");
					topFleur.SetTrigger("hide");
					yield return new WaitForSeconds(0.2f);
					StartCoroutine(ui.FadeOutCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeOutCanvasGroup(backgroundCg));
					StartCoroutine(ui.FadeOutCanvasGroup(activeSaveSlot));
					StartCoroutine(ui.FadeOutCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = false;
					break;
				}
				break;
			case SlotState.CLEAR_PROMPT:
				switch (nextState)
				{
				case SlotState.SAVE_PRESENT:
					ih.StopUIInput();
					yield return StartCoroutine(ui.FadeOutCanvasGroup(clearSavePrompt));
					clearSaveBlocker.SetActive(value: false);
					clearSavePrompt.interactable = false;
					clearSavePrompt.blocksRaycasts = false;
					if (saveStats != null)
					{
						PresentSaveSlot(saveStats);
					}
					StartCoroutine(ui.FadeInCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeInCanvasGroup(activeSaveSlot));
					StartCoroutine(ui.FadeInCanvasGroup(backgroundCg));
					yield return StartCoroutine(ui.FadeInCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = true;
					interactable = true;
					myCanvasGroup.blocksRaycasts = true;
					Select();
					ih.StartUIInput();
					break;
				case SlotState.EMPTY_SLOT:
					ih.StopUIInput();
					StartCoroutine(ui.FadeOutCanvasGroup(backgroundCg));
					yield return StartCoroutine(ui.FadeOutCanvasGroup(clearSavePrompt));
					clearSavePrompt.interactable = false;
					clearSavePrompt.blocksRaycasts = false;
					clearSaveBlocker.SetActive(value: false);
					StartCoroutine(ui.FadeInCanvasGroup(slotNumberText));
					yield return StartCoroutine(ui.FadeInCanvasGroup(newGameText));
					myCanvasGroup.blocksRaycasts = true;
					Select();
					ih.StartUIInput();
					break;
				case SlotState.DEFEATED:
					ih.StopUIInput();
					StartCoroutine(ui.FadeOutCanvasGroup(backgroundCg));
					yield return StartCoroutine(ui.FadeOutCanvasGroup(clearSavePrompt));
					clearSavePrompt.interactable = false;
					clearSavePrompt.blocksRaycasts = false;
					clearSaveBlocker.SetActive(value: false);
					StartCoroutine(ui.FadeInCanvasGroup(defeatedBackground));
					StartCoroutine(ui.FadeInCanvasGroup(defeatedText));
					StartCoroutine(ui.FadeInCanvasGroup(brokenSteelOrb));
					yield return StartCoroutine(ui.FadeInCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = true;
					myCanvasGroup.blocksRaycasts = true;
					Select();
					ih.StartUIInput();
					break;
				case SlotState.HIDDEN:
					yield return StartCoroutine(ui.FadeOutCanvasGroup(clearSavePrompt));
					break;
				case SlotState.CORRUPTED:
					ih.StopUIInput();
					yield return StartCoroutine(ui.FadeOutCanvasGroup(clearSavePrompt));
					clearSavePrompt.interactable = false;
					clearSavePrompt.blocksRaycasts = false;
					clearSaveBlocker.SetActive(value: false);
					StartCoroutine(ui.FadeInCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeInCanvasGroup(saveCorruptedText));
					yield return StartCoroutine(ui.FadeInCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = true;
					myCanvasGroup.blocksRaycasts = true;
					Select();
					ih.StartUIInput();
					break;
				}
				break;
			case SlotState.EMPTY_SLOT:
				if (nextState == SlotState.HIDDEN)
				{
					topFleur.ResetTrigger("show");
					topFleur.SetTrigger("hide");
					yield return new WaitForSeconds(0.2f);
					StartCoroutine(ui.FadeOutCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeOutCanvasGroup(backgroundCg));
					StartCoroutine(ui.FadeOutCanvasGroup(newGameText));
				}
				break;
			case SlotState.DEFEATED:
				switch (nextState)
				{
				case SlotState.CLEAR_PROMPT:
					ih.StopUIInput();
					interactable = false;
					myCanvasGroup.blocksRaycasts = false;
					StartCoroutine(ui.FadeOutCanvasGroup(defeatedBackground));
					StartCoroutine(ui.FadeOutCanvasGroup(defeatedText));
					StartCoroutine(ui.FadeOutCanvasGroup(brokenSteelOrb));
					yield return StartCoroutine(ui.FadeOutCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = false;
					clearSaveBlocker.SetActive(value: true);
					yield return StartCoroutine(ui.FadeInCanvasGroup(clearSavePrompt));
					clearSavePrompt.interactable = true;
					clearSavePrompt.blocksRaycasts = true;
					clearSavePromptHighlight.HighlightDefault();
					interactable = false;
					myCanvasGroup.blocksRaycasts = false;
					ih.StartUIInput();
					break;
				case SlotState.HIDDEN:
					topFleur.ResetTrigger("show");
					topFleur.SetTrigger("hide");
					yield return new WaitForSeconds(0.2f);
					StartCoroutine(ui.FadeOutCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeOutCanvasGroup(backgroundCg));
					StartCoroutine(ui.FadeOutCanvasGroup(activeSaveSlot));
					StartCoroutine(ui.FadeOutCanvasGroup(defeatedBackground));
					StartCoroutine(ui.FadeOutCanvasGroup(defeatedText));
					StartCoroutine(ui.FadeOutCanvasGroup(brokenSteelOrb));
					StartCoroutine(ui.FadeOutCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = false;
					break;
				}
				break;
			case SlotState.CORRUPTED:
				switch (nextState)
				{
				case SlotState.CLEAR_PROMPT:
					ih.StopUIInput();
					interactable = false;
					myCanvasGroup.blocksRaycasts = false;
					StartCoroutine(ui.FadeOutCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeOutCanvasGroup(saveCorruptedText));
					yield return StartCoroutine(ui.FadeOutCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = false;
					clearSaveBlocker.SetActive(value: true);
					yield return StartCoroutine(ui.FadeInCanvasGroup(clearSavePrompt));
					clearSavePrompt.interactable = true;
					clearSavePrompt.blocksRaycasts = true;
					clearSavePromptHighlight.HighlightDefault();
					interactable = false;
					myCanvasGroup.blocksRaycasts = false;
					ih.StartUIInput();
					break;
				case SlotState.HIDDEN:
					topFleur.ResetTrigger("show");
					topFleur.SetTrigger("hide");
					yield return new WaitForSeconds(0.2f);
					StartCoroutine(ui.FadeOutCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeOutCanvasGroup(saveCorruptedText));
					StartCoroutine(ui.FadeOutCanvasGroup(clearSaveButton));
					clearSaveButton.blocksRaycasts = false;
					break;
				case SlotState.OPERATION_IN_PROGRESS:
					StartCoroutine(ui.FadeOutCanvasGroup(slotNumberText));
					StartCoroutine(ui.FadeOutCanvasGroup(saveCorruptedText));
					yield return StartCoroutine(ui.FadeOutCanvasGroup(clearSaveButton));
					StartCoroutine(currentLoadingTextFadeIn = FadeInCanvasGroupAfterDelay(5f, loadingText));
					break;
				}
				break;
			default:
				if (slotState == SlotState.OPERATION_IN_PROGRESS && nextState == SlotState.HIDDEN)
				{
					topFleur.ResetTrigger("show");
					topFleur.SetTrigger("hide");
					yield return new WaitForSeconds(0.2f);
					StartCoroutine(ui.FadeOutCanvasGroup(loadingText));
				}
				break;
			}
		}
	
		private void PresentSaveSlot(SaveStats saveStats)
		{
			geoIcon.enabled = true;
			geoText.enabled = true;
			completionText.enabled = true;
			if (saveStats.bossRushMode)
			{
				normalSoulOrbCg.alpha = 0f;
				hardcoreSoulOrbCg.alpha = 0f;
				ggSoulOrbCg.alpha = 1f;
				healthSlots.showHealth(saveStats.maxHealth, steelsoulMode: false);
				playTimeText.text = saveStats.GetPlaytimeHHMM();
				mpSlots.showMPSlots(saveStats.GetMPSlotsVisible(), steelsoulMode: false);
				geoIcon.enabled = false;
				geoText.enabled = false;
				completionText.enabled = false;
				AreaBackground areaBackground = saveSlots.GetBackground(MapZone.GODS_GLORY);
				if (areaBackground != null)
				{
					background.sprite = areaBackground.backgroundImage;
				}
			}
			else if (saveStats.permadeathMode == 0)
			{
				normalSoulOrbCg.alpha = 1f;
				hardcoreSoulOrbCg.alpha = 0f;
				ggSoulOrbCg.alpha = 0f;
				healthSlots.showHealth(saveStats.maxHealth, steelsoulMode: false);
				geoText.text = saveStats.geo.ToString();
				if (saveStats.unlockedCompletionRate)
				{
					completionText.text = saveStats.completionPercentage + "%";
				}
				else
				{
					completionText.text = "";
				}
				playTimeText.text = saveStats.GetPlaytimeHHMM();
				mpSlots.showMPSlots(saveStats.GetMPSlotsVisible(), steelsoulMode: false);
				AreaBackground areaBackground2 = saveSlots.GetBackground(saveStats.mapZone);
				if (areaBackground2 != null)
				{
					background.sprite = areaBackground2.backgroundImage;
				}
			}
			else if (saveStats.permadeathMode == 1)
			{
				normalSoulOrbCg.alpha = 0f;
				hardcoreSoulOrbCg.alpha = 1f;
				ggSoulOrbCg.alpha = 0f;
				healthSlots.showHealth(saveStats.maxHealth, steelsoulMode: true);
				geoText.text = saveStats.geo.ToString();
				if (saveStats.unlockedCompletionRate)
				{
					completionText.text = saveStats.completionPercentage + "%";
				}
				else
				{
					completionText.text = "";
				}
				playTimeText.text = saveStats.GetPlaytimeHHMM();
				mpSlots.showMPSlots(saveStats.GetMPSlotsVisible(), steelsoulMode: true);
				AreaBackground areaBackground3 = saveSlots.GetBackground(saveStats.mapZone);
				if (areaBackground3 != null)
				{
					background.sprite = areaBackground3.backgroundImage;
				}
			}
			else if (saveStats.permadeathMode == 2)
			{
				normalSoulOrbCg.alpha = 0f;
				hardcoreSoulOrbCg.alpha = 0f;
				ggSoulOrbCg.alpha = 0f;
			}
			locationText.text = gm.GetFormattedMapZoneString(saveStats.mapZone).Replace("<br>", Environment.NewLine);
		}
	
		private void SetupNavs()
		{
			noNav = new Navigation
			{
				mode = Navigation.Mode.Explicit,
				selectOnLeft = null,
				selectOnRight = null,
				selectOnUp = base.navigation.selectOnUp,
				selectOnDown = base.navigation.selectOnDown
			};
			emptySlotNav = new Navigation
			{
				mode = Navigation.Mode.Explicit,
				selectOnRight = null,
				selectOnUp = base.navigation.selectOnUp,
				selectOnDown = base.navigation.selectOnDown
			};
			fullSlotNav = new Navigation
			{
				mode = Navigation.Mode.Explicit,
				selectOnRight = clearSaveButton.GetComponent<ClearSaveButton>(),
				selectOnUp = base.navigation.selectOnUp,
				selectOnDown = base.navigation.selectOnDown
			};
		}
	
		private IEnumerator ValidateDeselect()
		{
			prevSelectedObject = EventSystem.current.currentSelectedGameObject;
			yield return new WaitForEndOfFrame();
			if (EventSystem.current.currentSelectedGameObject != null)
			{
				leftCursor.ResetTrigger("show");
				rightCursor.ResetTrigger("show");
				highlight.ResetTrigger("show");
				leftCursor.SetTrigger("hide");
				rightCursor.SetTrigger("hide");
				highlight.SetTrigger("hide");
				deselectWasForced = false;
			}
			else if (deselectWasForced)
			{
				leftCursor.ResetTrigger("show");
				rightCursor.ResetTrigger("show");
				highlight.ResetTrigger("show");
				leftCursor.SetTrigger("hide");
				rightCursor.SetTrigger("hide");
				highlight.SetTrigger("hide");
				deselectWasForced = false;
			}
			else
			{
				deselectWasForced = false;
				dontPlaySelectSound = true;
				EventSystem.current.SetSelectedGameObject(prevSelectedObject);
			}
		}
	}
}