using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	
	public class MenuStyleSetting : MenuOptionHorizontal, IMoveHandler, IEventSystemHandler, IPointerClickHandler
	{
		private MenuStyles styles;
	
		private List<int> indexList = new List<int>();
	
		private new void OnEnable()
		{
			styles = MenuStyles.Instance;
			if (!styles || styles.styles.Length == 0)
			{
				return;
			}
			List<string> list = new List<string>();
			for (int i = 0; i < styles.styles.Length; i++)
			{
				MenuStyles.MenuStyle menuStyle = styles.styles[i];
				if (menuStyle.IsAvailable)
				{
					list.Add(menuStyle.displayName);
					indexList.Add(i);
				}
			}
			optionList = list.ToArray();
			selectedOptionIndex = indexList.IndexOf(styles.CurrentStyle);
			UpdateText();
		}
	
		public new void OnMove(AxisEventData move)
		{
			if (base.interactable)
			{
				if (MoveOption(move.moveDir))
				{
					UpdateStyle();
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
				UpdateStyle();
			}
		}
	
		private void UpdateStyle()
		{
			if ((bool)styles)
			{
				styles.SetStyle(indexList[selectedOptionIndex], fade: true);
			}
		}
	}
}