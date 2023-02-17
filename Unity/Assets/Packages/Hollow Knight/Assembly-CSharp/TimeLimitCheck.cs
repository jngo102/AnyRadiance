using HutongGames.PlayMaker;
using UnityEngine;

public class TimeLimitCheck : FsmStateAction
{
	[UIHint(UIHint.Variable)]
	public FsmFloat storedValue;

	public FsmEvent aboveEvent;

	public FsmEvent belowEvent;

	public override void Reset()
	{
		storedValue = null;
		aboveEvent = null;
		belowEvent = null;
	}

	public override void OnEnter()
	{
		base.Fsm.Event((Time.time >= storedValue.Value) ? aboveEvent : belowEvent);
		Finish();
	}
}
