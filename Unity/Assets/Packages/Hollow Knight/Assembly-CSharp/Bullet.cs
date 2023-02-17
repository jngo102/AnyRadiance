using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Explosion explosionPrefab;

	public float shootDistance;

	public float shootSpeed;

	private void OnEnable()
	{
		StartCoroutine(Shoot());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	private IEnumerator Shoot()
	{
		float travelledDistance = 0f;
		while (travelledDistance < shootDistance)
		{
			travelledDistance += shootSpeed * Time.deltaTime;
			transform.position += transform.forward * (shootSpeed * Time.deltaTime);
			yield return 0;
		}
		explosionPrefab.Spawn(transform.position);
		gameObject.Recycle();
	}
}
