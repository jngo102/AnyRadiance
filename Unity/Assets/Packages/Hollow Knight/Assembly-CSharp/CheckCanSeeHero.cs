using HutongGames.PlayMaker;

[ActionCategory("Hollow Knight")]
public class CheckCanSeeHero : FsmStateAction
{
	[UIHint(UIHint.Variable)]
	public FsmBool storeResult;

	public bool everyFrame;

	private LineOfSightDetector source;

	public override void Reset()
	{
		storeResult = new FsmBool();
	}

	public override void OnEnter()
	{
		source = base.Owner.GetComponent<LineOfSightDetector>();
		Apply();
		if (!everyFrame)
		{
			Finish();
		}
	}

	public override void OnUpdate()
	{
		Apply();
	}

	private void Apply()
	{
		if (source != null)
		{
			storeResult.Value = source.CanSeeHero;
		}
		else
		{
			storeResult.Value = false;
		}
	}
}
