using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class GetPersistentBool : FsmStateAction
{
	[UIHint(UIHint.Variable)]
	public FsmOwnerDefault target;

	public FsmBool storeValue;

	public override void Reset()
	{
		target = new FsmOwnerDefault();
	}

	public override void OnEnter()
	{
		GameObject gameObject = ((target.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : target.GameObject.Value);
		if (gameObject != null)
		{
			PersistentBoolItem component = gameObject.GetComponent<PersistentBoolItem>();
			storeValue.Value = component.persistentBoolData.activated;
		}
		Finish();
	}
}
