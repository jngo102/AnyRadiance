using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IdleBuzzing : MonoBehaviour
{
	private Rigidbody2D body;

	[SerializeField]
	private float waitMin;

	[SerializeField]
	private float waitMax;

	[SerializeField]
	private float speedMax;

	[SerializeField]
	private float accelerationMax;

	[SerializeField]
	private float roamingRange;

	[SerializeField]
	private float dampener;

	private Vector2 start2D;

	private Vector2 acceleration2D;

	private float waitTimer;

	private const float InspectorAccelerationConstant = 2000f;

	protected void Reset()
	{
		waitMin = 0.75f;
		waitMax = 1f;
		speedMax = 1.75f;
		accelerationMax = 15f;
		roamingRange = 1f;
		dampener = 1.125f;
	}

	protected void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}

	protected void Start()
	{
		start2D = body.position;
		acceleration2D = Vector2.zero;
		Buzz(0f);
	}

	protected void FixedUpdate()
	{
		float deltaTime = Time.deltaTime;
		Buzz(deltaTime);
	}

	private void Buzz(float deltaTime)
	{
		Vector2 position = body.position;
		Vector2 velocity = body.velocity;
		bool flag;
		if (waitTimer <= 0f)
		{
			flag = true;
			waitTimer = Random.Range(waitMin, waitMax);
		}
		else
		{
			waitTimer -= deltaTime;
			flag = false;
		}
		for (int i = 0; i < 2; i++)
		{
			float num = velocity[i];
			float num2 = start2D[i];
			float num3 = position[i] - num2;
			float num4 = acceleration2D[i];
			if (flag)
			{
				num4 = ((!(Mathf.Abs(num3) > roamingRange)) ? Random.Range(0f - accelerationMax, accelerationMax) : ((0f - Mathf.Sign(num3)) * accelerationMax));
				num4 /= 2000f;
			}
			else if (Mathf.Abs(num3) > roamingRange && num3 > 0f == num > 0f)
			{
				num4 = accelerationMax * (0f - Mathf.Sign(num3)) / 2000f;
				num /= dampener;
				waitTimer = Random.Range(waitMin, waitMax);
			}
			num += num4;
			num = (velocity[i] = Mathf.Clamp(num, 0f - speedMax, speedMax));
			acceleration2D[i] = num4;
		}
		body.velocity = velocity;
	}
}
