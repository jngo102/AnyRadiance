using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class TriggerEnterEventSubscribe : FsmStateAction
{
	[ObjectType(typeof(TriggerEnterEvent))]
	public FsmObject trigger;

	public FsmEvent triggerEnteredEvent;

	public FsmEvent triggerExitedEvent;

	public FsmEvent triggerStayedEvent;

	public override void Reset()
	{
		triggerEnteredEvent = null;
		triggerExitedEvent = null;
		triggerStayedEvent = null;
	}

	public override void OnEnter()
	{
		if (!trigger.IsNone)
		{
			TriggerEnterEvent obj = (TriggerEnterEvent)trigger.Value;
			obj.OnTriggerEntered += SendEnteredEvent;
			obj.OnTriggerExited += SendExitedEvent;
			obj.OnTriggerStayed += SendStayedEvent;
		}
		Finish();
	}

	public override void OnExit()
	{
		if (!trigger.IsNone)
		{
			TriggerEnterEvent obj = (TriggerEnterEvent)trigger.Value;
			obj.OnTriggerEntered -= SendEnteredEvent;
			obj.OnTriggerExited -= SendExitedEvent;
			obj.OnTriggerStayed -= SendStayedEvent;
		}
	}

	private void SendEnteredEvent(Collider2D collider, GameObject sender)
	{
		base.Fsm.Event(triggerEnteredEvent);
	}

	private void SendExitedEvent(Collider2D collider, GameObject sender)
	{
		base.Fsm.Event(triggerExitedEvent);
	}

	private void SendStayedEvent(Collider2D collider, GameObject sender)
	{
		base.Fsm.Event(triggerStayedEvent);
	}
}
