using UnityEngine;

public class ConveyorMovementHero : MonoBehaviour
{
	private float xSpeed;

	private float ySpeed;

	private bool onConveyor;

	public bool gravityOff;

	private HeroController heroCon;

	private void Start()
	{
		heroCon = GetComponent<HeroController>();
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
		if (gravityOff)
		{
			if (!heroCon.cState.superDashing)
			{
				heroCon.AffectedByGravity(gravityApplies: true);
			}
			gravityOff = false;
		}
	}

	private void LateUpdate()
	{
		if (!onConveyor)
		{
			return;
		}
		_ = xSpeed;
		_ = 0f;
		if (ySpeed != 0f && (heroCon.cState.wallSliding || heroCon.cState.superDashOnWall))
		{
			if (heroCon.cState.superDashOnWall)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f);
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, ySpeed);
			if (!gravityOff)
			{
				heroCon.AffectedByGravity(gravityApplies: false);
				gravityOff = true;
			}
		}
		else if (gravityOff && !heroCon.cState.superDashing)
		{
			heroCon.AffectedByGravity(gravityApplies: true);
			gravityOff = false;
		}
	}
}
