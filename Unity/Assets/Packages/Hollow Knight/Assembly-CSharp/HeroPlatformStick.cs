using UnityEngine;

public class HeroPlatformStick : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject gameObject = collision.gameObject;
		if (gameObject.layer == 9)
		{
			HeroController component = gameObject.GetComponent<HeroController>();
			if (component != null)
			{
				component.SetHeroParent(base.transform);
			}
			else
			{
				gameObject.transform.SetParent(base.transform);
			}
			Rigidbody2D component2 = gameObject.GetComponent<Rigidbody2D>();
			if (component2 != null)
			{
				component2.interpolation = RigidbodyInterpolation2D.None;
			}
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		GameObject gameObject = collision.gameObject;
		if (gameObject.layer == 9)
		{
			HeroController component = gameObject.GetComponent<HeroController>();
			if (component != null)
			{
				component.SetHeroParent(null);
			}
			else
			{
				gameObject.transform.SetParent(null);
			}
			Rigidbody2D component2 = gameObject.GetComponent<Rigidbody2D>();
			if (component2 != null)
			{
				component2.interpolation = RigidbodyInterpolation2D.Interpolate;
			}
		}
	}
}
