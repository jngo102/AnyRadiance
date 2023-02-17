using HKMenu;
using Language;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	
	public class MenuFrameCapSetting : MenuOptionHorizontal, IMoveHandler, IEventSystemHandler, IMenuOptionListSetting, IPointerClickHandler
	{
		private int[] frameCapValues = new int[8] { -1, 30, 50, 60, 72, 100, 120, 144 };
	
		private int tfrOff = -1;
	
		private GameSettings gs;
	
		public FixVerticalAlign textAligner;
	
		public new void OnEnable()
		{
			RefreshControls();
		}
	
		public new void OnMove(AxisEventData move)
		{
			if (MoveOption(move.moveDir))
			{
				UpdateFrameCapSetting();
			}
			else
			{
				base.OnMove(move);
			}
		}
	
		public new void OnPointerClick(PointerEventData eventData)
		{
			PointerClickCheckArrows(eventData);
			UpdateFrameCapSetting();
		}
	
		public void RefreshControls()
		{
			RefreshCurrentIndex();
			PushUpdateOptionList();
			if (selectedOptionIndex == 0)
			{
				optionText.text = global::Language.Language.Get("MOH_OFF", "MainMenu");
				textAligner.AlignText();
			}
			else
			{
				UpdateText();
			}
		}
	
		public void DisableFrameCapSetting()
		{
			SetOptionTo(0);
			UpdateFrameCapSetting();
		}
	
		public void ApplyValueFromGameSettings()
		{
			Application.targetFrameRate = GameManager.instance.gameSettings.targetFrameRate;
			RefreshControls();
		}
	
		private void UpdateFrameCapSetting()
		{
			if (selectedOptionIndex == 0)
			{
				optionText.text = global::Language.Language.Get("MOH_OFF", "MainMenu");
				textAligner.AlignText();
			}
			else
			{
				UIManager.instance.DisableVsyncSetting();
			}
			GameManager.instance.gameSettings.targetFrameRate = frameCapValues[selectedOptionIndex];
			Application.targetFrameRate = frameCapValues[selectedOptionIndex];
		}
	
		public void RefreshCurrentIndex()
		{
			bool flag = false;
			for (int i = 0; i < frameCapValues.Length; i++)
			{
				if (Application.targetFrameRate == frameCapValues[i])
				{
					selectedOptionIndex = i;
					flag = true;
				}
			}
			if (!flag)
			{
				Debug.LogError("Couldn't match current Target Frame Rate setting - " + Application.targetFrameRate);
			}
		}
	
		public void PushUpdateOptionList()
		{
			string[] array = new string[frameCapValues.Length];
			for (int i = 0; i < frameCapValues.Length; i++)
			{
				array[i] = frameCapValues[i].ToString();
			}
			SetOptionList(array);
		}
	}
}