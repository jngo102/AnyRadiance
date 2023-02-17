using UnityEngine;

public class ConveyorMovement : MonoBehaviour
{
	private float xSpeed;

	private float ySpeed;

	public bool onConveyor;

	public void OnEnable()
	{
		onConveyor = false;
	}

	public void StartConveyorMove(float c_xSpeed, float c_ySpeed)
	{
		onConveyor = true;
		xSpeed = c_xSpeed;
		ySpeed = c_ySpeed;
	}

	public void StopConveyorMove()
	{
		onConveyor = false;
	}

	private void LateUpdate()
	{
		if (onConveyor && xSpeed != 0f)
		{
			base.transform.position = new Vector3(base.transform.position.x + xSpeed * Time.deltaTime, base.transform.position.y, base.transform.position.z);
		}
	}
}
