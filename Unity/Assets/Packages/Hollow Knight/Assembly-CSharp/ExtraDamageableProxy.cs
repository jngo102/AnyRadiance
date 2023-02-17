using UnityEngine;

public class ExtraDamageableProxy : MonoBehaviour, IExtraDamageable
{
	public ExtraDamageable passTo;

	void IExtraDamageable.RecieveExtraDamage(ExtraDamageTypes extraDamageTypes)
	{
		if (passTo != null)
		{
			passTo.RecieveExtraDamage(extraDamageTypes);
		}
	}
}
