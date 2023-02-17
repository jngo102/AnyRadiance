using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DebrisPiece : MonoBehaviour
{
	[SerializeField]
	private bool resetOnDisable;

	private bool didLaunch;

	private bool didSpin;

	[Header("'set_z' Functionality")]
	[SerializeField]
	private bool forceZ;

	[SerializeField]
	private float forcedZ;

	[Header("'spin_self' Functionality")]
	[SerializeField]
	private bool randomStartRotation;

	[SerializeField]
	private float zRandomRadius;

	[SerializeField]
	private float spinFactor;

	private Rigidbody2D body;

	protected void Reset()
	{
		resetOnDisable = true;
		forceZ = true;
		forcedZ = 0.015f;
		randomStartRotation = false;
		zRandomRadius = 0.000999f;
		spinFactor = 10f;
	}

	protected void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		if (body == null)
		{
			Debug.LogErrorFormat(this, "Missing Rigidbody2D on {0}", base.name);
		}
	}

	protected void OnEnable()
	{
		if (!didLaunch)
		{
			Launch();
		}
	}

	protected void OnDisable()
	{
		if (resetOnDisable)
		{
			didLaunch = false;
			didSpin = false;
		}
	}

	private void Launch()
	{
		didLaunch = true;
		if (forceZ)
		{
			Vector3 position = base.transform.position;
			position.z = forcedZ;
			base.transform.position = position;
		}
		if (randomStartRotation)
		{
			Vector3 localEulerAngles = base.transform.localEulerAngles;
			localEulerAngles.z = Random.Range(0f, 360f);
			base.transform.localEulerAngles = localEulerAngles;
		}
	}

	protected void FixedUpdate()
	{
		if (!didSpin)
		{
			Spin();
		}
	}

	private void Spin()
	{
		didSpin = true;
		if (spinFactor != 0f)
		{
			if (zRandomRadius != 0f)
			{
				Vector3 position = base.transform.position;
				position.z += Random.Range(0f - zRandomRadius, zRandomRadius);
				base.transform.position = position;
			}
			body.AddTorque((0f - body.velocity.x) * spinFactor);
		}
	}
}
