using HutongGames.PlayMaker;

[ActionCategory("Hollow Knight/GG")]
public class GGCheckBoundHeart : FSMUtility.CheckFsmStateAction
{
	public enum CheckSource
	{
		Regular,
		Joni
	}

	public FsmInt healthNumber;

	public CheckSource checkSource;

	public override bool IsTrue
	{
		get
		{
			int num = -1;
			switch (checkSource)
			{
			case CheckSource.Regular:
				num = healthNumber.Value;
				break;
			case CheckSource.Joni:
				num = (int)((float)healthNumber.Value * 0.714285731f) + 1;
				break;
			}
			if (BossSequenceController.BoundShell)
			{
				return num > BossSequenceController.BoundMaxHealth;
			}
			return false;
		}
	}

	public override void Reset()
	{
		healthNumber = null;
		checkSource = CheckSource.Regular;
		base.Reset();
	}
}
