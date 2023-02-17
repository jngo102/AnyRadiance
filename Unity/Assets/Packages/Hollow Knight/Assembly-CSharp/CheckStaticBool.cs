using HutongGames.PlayMaker;

public class CheckStaticBool : FsmStateAction
{
	public FsmString variableName;

	public FsmEvent trueEvent;

	public FsmEvent falseEvent;

	public override void Reset()
	{
		variableName = null;
		trueEvent = null;
		falseEvent = null;
	}

	public override void OnEnter()
	{
		if (!variableName.IsNone && StaticVariableList.Exists(variableName.Value) && StaticVariableList.GetValue<bool>(variableName.Value))
		{
			base.Fsm.Event(trueEvent);
		}
		base.Fsm.Event(falseEvent);
		Finish();
	}
}
