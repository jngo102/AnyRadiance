using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
	public float speed;

	public bool vertical;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if ((bool)collision.gameObject.GetComponent<ConveyorMovement>())
		{
			collision.gameObject.GetComponent<ConveyorMovement>().StartConveyorMove(speed, 0f);
		}
		if ((bool)collision.gameObject.GetComponent<DropCrystal>())
		{
			collision.gameObject.GetComponent<DropCrystal>().StartConveyorMove(speed, 0f);
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
				collision.gameObject.GetComponent<HeroController>().cState.onConveyor = true;
			}
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if ((bool)collision.gameObject.GetComponent<ConveyorMovement>())
		{
			collision.gameObject.GetComponent<ConveyorMovement>().StopConveyorMove();
		}
		if ((bool)collision.gameObject.GetComponent<DropCrystal>())
		{
			collision.gameObject.GetComponent<DropCrystal>().StopConveyorMove();
		}
		if ((bool)collision.gameObject.GetComponent<HeroController>())
		{
			collision.gameObject.GetComponent<ConveyorMovementHero>().StopConveyorMove();
			collision.gameObject.GetComponent<HeroController>().cState.onConveyor = false;
			collision.gameObject.GetComponent<HeroController>().cState.onConveyorV = false;
		}
	}
}
