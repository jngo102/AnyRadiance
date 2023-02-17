using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class MakeEnemyDreamnailReactionReady : FsmStateAction
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
			EnemyDreamnailReaction component = gameObject.GetComponent<EnemyDreamnailReaction>();
			if (component != null)
			{
				component.MakeReady();
			}
		}
		Finish();
	}
}
