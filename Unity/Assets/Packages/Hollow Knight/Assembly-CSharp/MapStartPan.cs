using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Inventory")]
public class MapStartPan : FsmStateAction
{
	[UIHint(UIHint.Variable)]
	public FsmOwnerDefault target;

	public override void Reset()
	{
		target = new FsmOwnerDefault();
	}

	public override void OnEnter()
	{
		GameObject gameObject = ((target.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : target.GameObject.Value);
		if (gameObject != null)
		{
			GameMap component = gameObject.GetComponent<GameMap>();
			if (component != null)
			{
				component.StartPan();
			}
		}
		Finish();
	}
}
