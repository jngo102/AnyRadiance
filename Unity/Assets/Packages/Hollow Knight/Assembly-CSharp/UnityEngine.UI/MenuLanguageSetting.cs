using System;
using GlobalEnums;
using HKMenu;
using Language;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	
	public class MenuLanguageSetting : MenuOptionHorizontal, IMoveHandler, IEventSystemHandler, IMenuOptionListSetting, IPointerClickHandler
	{
		private SupportedLanguages[] langs;
	
		private GameSettings gs;
	
		public FixVerticalAlign textAligner;
	
		private new void OnEnable()
		{
			RefreshControls();
			UpdateAlpha();
		}
	
		public void UpdateAlpha()
		{
			CanvasGroup component = GetComponent<CanvasGroup>();
			if ((bool)component)
			{
				if (!base.interactable)
				{
					component.alpha = 0.5f;
				}
				else
				{
					component.alpha = 1f;
				}
			}
		}
	
		public new void OnMove(AxisEventData move)
		{
			if (base.interactable)
			{
				if (MoveOption(move.moveDir))
				{
					UpdateLanguageSetting();
				}
				else
				{
					base.OnMove(move);
				}
			}
		}
	
		public new void OnPointerClick(PointerEventData eventData)
		{
			if (base.interactable)
			{
				PointerClickCheckArrows(eventData);
				UpdateLanguageSetting();
			}
		}
	
		public static Rect RectTransformToScreenSpace(RectTransform transform)
		{
			Vector2 vector = Vector2.Scale(transform.rect.size, transform.lossyScale);
			return new Rect(transform.position.x, (float)Screen.height - transform.position.y, vector.x, vector.y);
		}
	
		public void RefreshControls()
		{
			RefreshAvailableLanguages();
			RefreshCurrentIndex();
			PushUpdateOptionList();
			UpdateText();
		}
	
		private void UpdateLanguageSetting()
		{
			GameManager.instance.gameSettings.gameLanguage = langs[selectedOptionIndex];
			global::Language.Language.SwitchLanguage((LanguageCode)langs[selectedOptionIndex]);
			gm.RefreshLocalization();
			UIManager.instance.RefreshAchievementsList();
			UpdateText();
		}
	
		private void RefreshAvailableLanguages()
		{
			if (GameManager.instance.gameConfig.hideLanguageOption)
			{
				langs = Enum.GetValues(typeof(TestingLanguages)) as SupportedLanguages[];
			}
			else
			{
				langs = Enum.GetValues(typeof(SupportedLanguages)) as SupportedLanguages[];
			}
		}
	
		public void RefreshCurrentIndex()
		{
			bool flag = false;
			string text = global::Language.Language.CurrentLanguage().ToString();
			for (int i = 0; i < langs.Length; i++)
			{
				if (text == langs[i].ToString())
				{
					selectedOptionIndex = i;
					flag = true;
				}
			}
			if (!flag)
			{
				Debug.LogError("Couldn't find currently active language");
			}
		}
	
		public void PushUpdateOptionList()
		{
			string[] array = new string[langs.Length];
			for (int i = 0; i < langs.Length; i++)
			{
				array[i] = langs[i].ToString();
			}
			SetOptionList(array);
		}
	
		protected override void UpdateText()
		{
			if (optionList != null && optionText != null)
			{
				try
				{
					optionText.text = global::Language.Language.Get("LANG_" + optionList[selectedOptionIndex].ToString(), sheetTitle);
				}
				catch (Exception ex)
				{
					Debug.LogError(optionText.text + " : " + optionList?.ToString() + " : " + selectedOptionIndex + " " + ex);
				}
				optionText.GetComponent<FixVerticalAlign>().AlignText();
			}
		}
	}
}