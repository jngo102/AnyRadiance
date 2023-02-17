using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
[HutongGames.PlayMaker.Tooltip("Check and respond to the amount of objects in a Trigger that has TrackTriggerObjects attached to the same object.")]
public class CheckTrackTriggerCount : FsmStateAction
{
	public enum IntTest
	{
		Equal,
		LessThan,
		MoreThan,
		LessThanOrEqual,
		MoreThanOrEqual
	}

	public FsmOwnerDefault target;

	public FsmInt count;

	[ObjectType(typeof(IntTest))]
	public FsmEnum test;

	public bool everyFrame;

	[Space]
	public FsmEvent successEvent;

	private TrackTriggerObjects track;

	public override void Reset()
	{
		target = null;
		count = null;
		test = null;
		everyFrame = true;
		successEvent = null;
	}

	public override void OnPreprocess()
	{
		base.Fsm.HandleFixedUpdate = true;
	}

	public override void OnEnter()
	{
		GameObject safe = target.GetSafe(this);
		if ((bool)safe)
		{
			track = safe.GetComponent<TrackTriggerObjects>();
			if ((bool)track)
			{
				if (!CheckCount())
				{
					if (everyFrame)
					{
						return;
					}
				}
				else
				{
					base.Fsm.Event(successEvent);
				}
			}
			else
			{
				Debug.LogError("Target GameObject does not have a TrackTriggerObjects component attached!", base.Owner);
			}
		}
		Finish();
	}

	public override void OnFixedUpdate()
	{
		if (everyFrame && CheckCount())
		{
			base.Fsm.Event(successEvent);
		}
	}

	public bool CheckCount()
	{
		if ((bool)track)
		{
			switch ((IntTest)(object)test.Value)
			{
			case IntTest.Equal:
				return track.InsideCount == count.Value;
			case IntTest.LessThan:
				return track.InsideCount < count.Value;
			case IntTest.LessThanOrEqual:
				return track.InsideCount <= count.Value;
			case IntTest.MoreThan:
				return track.InsideCount > count.Value;
			case IntTest.MoreThanOrEqual:
				return track.InsideCount >= count.Value;
			}
			Debug.LogError($"IntTest type {((IntTest)(object)test.Value).ToString()} not implemented!", base.Owner);
		}
		return false;
	}
}
