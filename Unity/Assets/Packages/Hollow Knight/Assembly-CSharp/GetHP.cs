using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class GetHP : FsmStateAction
{
	[UIHint(UIHint.Variable)]
	public FsmOwnerDefault target;

	[UIHint(UIHint.Variable)]
	public FsmInt storeValue;

	public override void Reset()
	{
		target = new FsmOwnerDefault();
		storeValue = new FsmInt
		{
			UseVariable = true
		};
	}

	public override void OnEnter()
	{
		GameObject safe = target.GetSafe(this);
		if (safe != null)
		{
			HealthManager component = safe.GetComponent<HealthManager>();
			if (component != null && !storeValue.IsNone)
			{
				storeValue.Value = component.hp;
			}
		}
		Finish();
	}
}
