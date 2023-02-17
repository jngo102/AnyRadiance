using GlobalEnums;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MenuButtonIcon : ActionButtonIconBase
{
	public Platform.MenuActions menuAction;

	public override HeroActionButton Action
	{
		get
		{
			switch (Platform.Current.AcceptRejectInputStyle)
			{
			case Platform.AcceptRejectInputStyles.NonJapaneseStyle:
				switch (menuAction)
				{
				case Platform.MenuActions.Submit:
					return HeroActionButton.JUMP;
				case Platform.MenuActions.Cancel:
					return HeroActionButton.CAST;
				}
				break;
			case Platform.AcceptRejectInputStyles.JapaneseStyle:
				switch (menuAction)
				{
				case Platform.MenuActions.Submit:
					return HeroActionButton.CAST;
				case Platform.MenuActions.Cancel:
					return HeroActionButton.JUMP;
				}
				break;
			}
			return HeroActionButton.MENU_CANCEL;
		}
	}
}
