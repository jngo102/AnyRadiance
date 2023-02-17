using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class SetCorpseSpawnPoint : FsmStateAction
{
	[UIHint(UIHint.Variable)]
	public FsmOwnerDefault target;

	public FsmVector3 spawnPoint;

	public override void Reset()
	{
		target = new FsmOwnerDefault();
		spawnPoint = new FsmVector3();
	}

	public override void OnEnter()
	{
		GameObject gameObject = ((target.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : target.GameObject.Value);
		if (gameObject != null)
		{
			EnemyDeathEffects component = gameObject.GetComponent<EnemyDeathEffects>();
			if (component != null)
			{
				component.corpseSpawnPoint = spawnPoint.Value;
			}
		}
		Finish();
	}
}
