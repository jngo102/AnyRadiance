using UnityEngine;

public class KeepRotation : MonoBehaviour
{
	public float angle;

	private Transform tf;

	private Vector3 rotation;

	private void Start()
	{
		tf = GetComponent<Transform>();
		rotation = new Vector3(0f, 0f, angle);
	}

	private void Update()
	{
		if (tf != null)
		{
			tf.localEulerAngles = rotation;
		}
	}
}
