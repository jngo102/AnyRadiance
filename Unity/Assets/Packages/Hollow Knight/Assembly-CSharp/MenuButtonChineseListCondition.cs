using UnityEngine;

public class MenuButtonChineseListCondition : MenuButtonListCondition
{
	[SerializeField]
	private bool isChineseBuildDesired;

	public override bool IsFulfilled()
	{
		return !isChineseBuildDesired;
	}
}
