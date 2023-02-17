using System.Collections;
using InControl;
using Language;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
	private bool verboseMode;

	public Animator startManagerAnimator;

	public Slider progressIndicator;

	[SerializeField]
	private StandaloneLoadingSpinner loadSpinnerPrefab;

	[Header("Controller Notice")]
	public SpriteRenderer controllerImage;

	[Space(5f)]
	public Sprite winController;

	public Sprite osxController;

	public SetTextMeshProGameText controllerNoticeText;

	[Header("Language Select")]
	public CanvasGroup languageSelect;

	public Animator languageAnimator;

	public PreselectOption preselector;

	public CanvasGroup languageConfirm;

	private string selectedLanguage;

	private string oldLanguage;

	[Header("Input")]
	public InControlInputModule inputModule;

	[Header("Audio")]
	public MenuAudioController uiAudioPlayer;

	[Header("Debug")]
	public bool showProgessIndicator;

	private AsyncOperation loadop;

	private RuntimePlatform platform;

	private string logoTrigger = "fadeLogo";

	private string controllerTrigger = "controllerNotice";

	private string loadingIconTrigger = "loading";

	private float fadeSpeed = 1.6f;

	private bool confirmedLanguage;

	private void Awake()
	{
		platform = Application.platform;
	}

	private IEnumerator Start()
	{
		controllerImage.sprite = GetControllerSpriteForPlatform(platform);
		AsyncOperation loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Menu_Title");
		loadOperation.allowSceneActivation = false;
		if (!CheckIsLanguageSet() && Platform.Current.ShowLanguageSelect)
		{
			yield return StartCoroutine(ShowLanguageSelect());
			while (!confirmedLanguage)
			{
				yield return null;
			}
			yield return StartCoroutine(LanguageSettingDone());
		}
		startManagerAnimator.SetBool("WillShowControllerNotice", value: false);
		startManagerAnimator.SetBool("WillShowQuote", value: true);
		Object.Instantiate(loadSpinnerPrefab).Setup(null);
		loadOperation.allowSceneActivation = true;
		yield return loadOperation;
	}

	private Sprite GetControllerSpriteForPlatform(RuntimePlatform runtimePlatform)
	{
		switch (runtimePlatform)
		{
		case RuntimePlatform.WindowsPlayer:
		case RuntimePlatform.WindowsEditor:
		case RuntimePlatform.LinuxPlayer:
		case RuntimePlatform.LinuxEditor:
			return winController;
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXPlayer:
			return osxController;
		default:
			return null;
		}
	}

	public void SwitchToMenuScene()
	{
		if (verboseMode)
		{
			Debug.Log("Switching Scenes");
		}
		loadop.allowSceneActivation = true;
	}

	public void SetLanguage(string newLanguage)
	{
		oldLanguage = global::Language.Language.CurrentLanguage().ToString();
		selectedLanguage = newLanguage;
		global::Language.Language.SwitchLanguage(selectedLanguage);
		languageConfirm.gameObject.SetActive(value: true);
		StartCoroutine(FadeIn(languageConfirm, 0.25f));
		AutoLocalizeTextUI[] componentsInChildren = languageConfirm.GetComponentsInChildren<AutoLocalizeTextUI>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].RefreshTextFromLocalization();
		}
	}

	private IEnumerator FadeIn(CanvasGroup group, float duration)
	{
		group.alpha = 0f;
		for (float elapsed = 0f; elapsed < duration; elapsed += Time.deltaTime)
		{
			group.alpha = elapsed / duration;
			yield return new WaitForEndOfFrame();
		}
		group.alpha = 1f;
		PreselectOption component = group.GetComponent<PreselectOption>();
		if ((bool)component)
		{
			component.HighlightDefault(deselect: true);
		}
	}

	private IEnumerator FadeOut(CanvasGroup group, float duration)
	{
		group.alpha = 1f;
		for (float elapsed = 0f; elapsed < duration; elapsed += Time.deltaTime)
		{
			group.alpha = 1f - elapsed / duration;
			yield return new WaitForEndOfFrame();
		}
		group.alpha = 0f;
		group.gameObject.SetActive(value: false);
	}

	public bool CheckIsLanguageSet()
	{
		if (Platform.Current.IsPlayerPrefsLoaded)
		{
			return Platform.Current.SharedData.GetBool("GameLangSet", def: false);
		}
		return false;
	}

	public void ConfirmLanguage()
	{
		Platform.Current.SharedData.SetInt("GameLangSet", 1);
		Platform.Current.SharedData.Save();
		controllerNoticeText.UpdateText();
		StartCoroutine(FadeOut(languageConfirm, 0.25f));
		confirmedLanguage = true;
	}

	public void CancelLanguage()
	{
		global::Language.Language.SwitchLanguage(oldLanguage);
		StartCoroutine(FadeOut(languageConfirm, 0.25f));
		if ((bool)preselector)
		{
			preselector.HighlightDefault(deselect: true);
		}
	}

	private IEnumerator ShowLanguageSelect()
	{
		languageSelect.alpha = 0f;
		languageSelect.gameObject.SetActive(value: true);
		while ((double)languageSelect.alpha < 0.99)
		{
			languageSelect.alpha += Time.smoothDeltaTime * fadeSpeed;
			if ((double)languageSelect.alpha > 0.99)
			{
				languageSelect.alpha = 1f;
			}
			yield return null;
		}
		Cursor.lockState = CursorLockMode.None;
		preselector.HighlightDefault();
		yield return null;
	}

	private IEnumerator LanguageSettingDone()
	{
		Cursor.lockState = CursorLockMode.Locked;
		while ((double)languageSelect.alpha > 0.01)
		{
			languageSelect.alpha -= Time.smoothDeltaTime * fadeSpeed;
			if ((double)languageSelect.alpha < 0.01)
			{
				languageSelect.alpha = 0f;
			}
			yield return null;
		}
		languageSelect.gameObject.SetActive(value: false);
		ConfigManager.SaveConfig();
	}

	private IEnumerator orig_Start()
	{
		controllerImage.sprite = GetControllerSpriteForPlatform(platform);
		AsyncOperation loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Menu_Title");
		loadOperation.allowSceneActivation = false;
		if (!CheckIsLanguageSet() && Platform.Current.ShowLanguageSelect)
		{
			yield return StartCoroutine(ShowLanguageSelect());
			while (!confirmedLanguage)
			{
				yield return null;
			}
			yield return StartCoroutine(LanguageSettingDone());
		}
		startManagerAnimator.SetBool("WillShowControllerNotice", value: false);
		startManagerAnimator.SetBool("WillShowQuote", value: true);
		startManagerAnimator.SetTrigger("Start");
		int loadingIconNameHash = Animator.StringToHash("LoadingIcon");
		while (startManagerAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash != loadingIconNameHash)
		{
			yield return null;
		}
		Object.Instantiate(loadSpinnerPrefab).Setup(null);
		bool didWaitForPlayerPrefs = false;
		while (!Platform.Current.IsPlayerPrefsLoaded)
		{
			if (!didWaitForPlayerPrefs)
			{
				didWaitForPlayerPrefs = true;
				Debug.LogFormat("Waiting for PlayerPrefs load...");
			}
			yield return null;
		}
		if (!didWaitForPlayerPrefs)
		{
			Debug.LogFormat("Didn't need to wait for PlayerPrefs load.");
		}
		else
		{
			Debug.LogFormat("Finished waiting for PlayerPrefs load.");
		}
		loadOperation.allowSceneActivation = true;
		yield return loadOperation;
	}
}
