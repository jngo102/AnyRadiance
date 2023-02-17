using UnityEngine;

public class BreakablePoleTopLand : MonoBehaviour
{
	public float angleMin = 165f;

	public float angleMax = 195f;

	[Space]
	public GameObject[] effects;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Collider2D otherCollider = collision.otherCollider;
		if (collision.gameObject.layer != 8)
		{
			return;
		}
		Vector2 point = collision.GetSafeContact().Point;
		if (point.y > otherCollider.bounds.center.y)
		{
			return;
		}
		float z = base.transform.eulerAngles.z;
		if (z >= angleMin && z <= angleMax)
		{
			GameObject[] array = effects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Spawn(point);
			}
			Rigidbody2D component = GetComponent<Rigidbody2D>();
			if ((bool)component)
			{
				component.isKinematic = true;
				component.simulated = false;
				component.velocity = Vector2.zero;
				component.angularVelocity = 0f;
			}
		}
	}
}
