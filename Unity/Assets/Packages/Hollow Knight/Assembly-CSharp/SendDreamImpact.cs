using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class SendDreamImpact : FsmStateAction
{
	[UIHint(UIHint.Variable)]
	public FsmOwnerDefault target;

	public override void Reset()
	{
		target = new FsmOwnerDefault();
	}

	public override void OnEnter()
	{
		GameObject safe = target.GetSafe(this);
		if (safe != null)
		{
			EnemyDreamnailReaction enemyDreamnailReaction = safe.GetComponent<EnemyDreamnailReaction>();
			if (enemyDreamnailReaction == null)
			{
				enemyDreamnailReaction = safe.GetComponentInParent<EnemyDreamnailReaction>();
				if ((bool)enemyDreamnailReaction && !enemyDreamnailReaction.allowUseChildColliders)
				{
					enemyDreamnailReaction = null;
				}
			}
			if (enemyDreamnailReaction != null)
			{
				enemyDreamnailReaction.RecieveDreamImpact();
			}
		}
		Finish();
	}
}
