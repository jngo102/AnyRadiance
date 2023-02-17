using UnityEngine;

public class KeepWorldPosition : MonoBehaviour
{
	public bool keepX;

	public float xPosition;

	public bool keepY;

	public float yPosition;

	private void Update()
	{
		if (keepX)
		{
			base.transform.position = new Vector3(xPosition, base.transform.position.y, base.transform.position.z);
		}
		if (keepY)
		{
			base.transform.position = new Vector3(base.transform.position.x, yPosition, base.transform.position.z);
		}
	}
}
