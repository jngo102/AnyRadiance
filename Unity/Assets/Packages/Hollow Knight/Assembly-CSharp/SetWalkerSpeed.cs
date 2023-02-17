using HutongGames.PlayMaker;

[ActionCategory("Hollow Knight")]
public class SetWalkerSpeed : WalkerAction
{
	public FsmFloat walkSpeedL;

	public FsmFloat walkSpeedR;

	public override void Reset()
	{
		base.Reset();
		walkSpeedL = new FsmFloat
		{
			UseVariable = true
		};
		walkSpeedR = new FsmFloat
		{
			UseVariable = true
		};
	}

	protected override void Apply(Walker walker)
	{
		if (!walkSpeedL.IsNone)
		{
			walker.walkSpeedL = walkSpeedL.Value;
		}
		if (!walkSpeedR.IsNone)
		{
			walker.walkSpeedR = walkSpeedR.Value;
		}
	}
}
