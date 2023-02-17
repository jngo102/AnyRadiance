using UnityEngine;

public class ConveyorZone : MonoBehaviour
{
	public float speed;

	public bool vertical;

	private bool activated = true;

	private void Start()
	{
		if ((bool)HeroController.instance)
		{
			activated = false;
			HeroController.HeroInPosition temp = null;
			temp = delegate
			{
				activated = true;
				HeroController.instance.heroInPosition -= temp;
			};
			HeroController.instance.heroInPosition += temp;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!activated)
		{
			return;
		}
		if ((bool)collision.gameObject.GetComponent<ConveyorMovement>())
		{
			collision.gameObject.GetComponent<ConveyorMovement>().StartConveyorMove(speed, 0f);
		}
		if ((bool)collision.gameObject.GetComponent<HeroController>())
		{
			if (vertical)
			{
				collision.gameObject.GetComponent<ConveyorMovementHero>().StartConveyorMove(0f, speed);
				collision.gameObject.GetComponent<HeroController>().cState.onConveyorV = true;
			}
			else
			{
				collision.gameObject.GetComponent<HeroController>().SetConveyorSpeed(speed);
				collision.gameObject.GetComponent<HeroController>().cState.inConveyorZone = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (activated)
		{
			if ((bool)collision.gameObject.GetComponent<ConveyorMovement>())
			{
				collision.gameObject.GetComponent<ConveyorMovement>().StopConveyorMove();
			}
			if ((bool)collision.gameObject.GetComponent<HeroController>())
			{
				collision.gameObject.GetComponent<ConveyorMovementHero>().StopConveyorMove();
				collision.gameObject.GetComponent<HeroController>().cState.inConveyorZone = false;
				collision.gameObject.GetComponent<HeroController>().cState.onConveyorV = false;
			}
		}
	}
}
