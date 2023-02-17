public class MenuButtonKeyboardListCondition : MenuButtonListCondition
{
	public override bool IsFulfilled()
	{
		return Platform.Current.WillDisplayKeyboardSettings;
	}
}
