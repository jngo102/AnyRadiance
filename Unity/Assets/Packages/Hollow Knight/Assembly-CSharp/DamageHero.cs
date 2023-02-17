using UnityEngine;

public class DamageHero : MonoBehaviour
{
	public int damageDealt = 1;

	public int hazardType = 1;

	public bool shadowDashHazard;

	public bool resetOnEnable;

	private int? initialValue;

	private void OnEnable()
	{
		if (resetOnEnable)
		{
			if (!initialValue.HasValue)
			{
				initialValue = damageDealt;
			}
			else
			{
				damageDealt = initialValue.Value;
			}
		}
	}
}
