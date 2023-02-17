using UnityEngine;

public class RandomRotation : MonoBehaviour
{
	private void Start()
	{
		RandomRotate();
	}

	private void OnEnable()
	{
		RandomRotate();
	}

	private void RandomRotate()
	{
		base.transform.localEulerAngles = new Vector3(base.transform.rotation.x, base.transform.rotation.y, Random.Range(0f, 360f));
	}
}
