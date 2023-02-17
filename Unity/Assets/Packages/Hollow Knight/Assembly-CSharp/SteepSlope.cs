using UnityEngine;

public class SteepSlope : MonoBehaviour
{
	public bool slideLeft;

	public bool slideRight;

	private HeroController hc;

	private void Start()
	{
		hc = HeroController.instance;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		Rigidbody2D component = obj.GetComponent<Rigidbody2D>();
		component.velocity = new Vector2(component.velocity.x, -20f);
		if (obj.CompareTag("Player"))
		{
			hc.ResetHardLandingTimer();
			hc.CancelSuperDash();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (slideLeft)
			{
				hc.cState.slidingLeft = true;
			}
			if (slideRight)
			{
				hc.cState.slidingRight = true;
			}
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (slideLeft)
			{
				hc.cState.slidingLeft = false;
			}
			if (slideRight)
			{
				hc.cState.slidingRight = false;
			}
		}
	}
}
