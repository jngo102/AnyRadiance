using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class SendExtraDamage : FsmStateAction
{
	[UIHint(UIHint.Variable)]
	public FsmOwnerDefault target;

	public FsmEnum extraDamageType;

	public override void Reset()
	{
		target = new FsmOwnerDefault();
		extraDamageType = new FsmEnum("Extra Damage Type", typeof(ExtraDamageTypes), 0);
	}

	public override void OnEnter()
	{
		GameObject gameObject = ((target.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : target.GameObject.Value);
		if (gameObject != null)
		{
			ExtraDamageable component = gameObject.GetComponent<ExtraDamageable>();
			if (component != null)
			{
				component.RecieveExtraDamage((ExtraDamageTypes)(object)extraDamageType.Value);
			}
		}
		Finish();
	}
}
