using UnityEngine;

public class ConveyorSpeedZone : MonoBehaviour
{
	public float speed;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ((bool)collision.gameObject.GetComponent<HeroController>())
		{
			collision.gameObject.GetComponent<HeroController>().SetConveyorSpeed(speed);
		}
	}
}
