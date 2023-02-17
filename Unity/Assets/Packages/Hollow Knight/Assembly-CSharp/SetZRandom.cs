using UnityEngine;

public class SetZRandom : MonoBehaviour
{
	public float zMin;

	public float zMax;

	private void OnEnable()
	{
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y, Random.Range(zMin, zMax));
		base.transform.position = position;
	}
}
