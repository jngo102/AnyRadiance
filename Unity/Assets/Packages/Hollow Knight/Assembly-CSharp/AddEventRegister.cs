using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class AddEventRegister : FsmStateAction
{
	public FsmOwnerDefault target;

	public FsmString eventName;

	public override void Reset()
	{
		eventName = new FsmString();
	}

	public override void OnEnter()
	{
		if (eventName.Value != "")
		{
			GameObject safe = target.GetSafe(this);
			if ((bool)safe)
			{
				safe.AddComponent<EventRegister>().SwitchEvent(eventName.Value);
			}
		}
		Finish();
	}
}
