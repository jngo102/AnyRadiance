using UnityEngine;

public class KillOnContact : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		HealthManager component = collision.gameObject.GetComponent<HealthManager>();
		if ((bool)component)
		{
			component.Die(null, AttackTypes.Generic, ignoreEvasion: true);
			return;
		}
		GeoControl component2 = collision.gameObject.GetComponent<GeoControl>();
		if ((bool)component2)
		{
			component2.Disable(0f);
			return;
		}
		HeroController component3 = collision.gameObject.GetComponent<HeroController>();
		if ((bool)component3)
		{
			StartCoroutine(component3.HazardRespawn());
		}
	}
}
