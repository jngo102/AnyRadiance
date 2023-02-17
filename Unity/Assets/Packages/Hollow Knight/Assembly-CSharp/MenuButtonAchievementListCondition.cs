public class MenuButtonAchievementListCondition : MenuButtonListCondition
{
	public override bool IsFulfilled()
	{
		return !Platform.Current.HasNativeAchievementsDialog;
	}
}
