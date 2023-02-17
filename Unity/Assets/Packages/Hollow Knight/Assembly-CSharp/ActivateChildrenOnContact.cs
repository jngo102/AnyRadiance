using UnityEngine;

public class ActivateChildrenOnContact : MonoBehaviour
{
	public CircleCollider2D circleCollider;

	public BoxCollider2D boxCollider;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		foreach (Transform item in base.transform)
		{
			item.gameObject.SetActive(value: true);
		}
		if (circleCollider != null)
		{
			circleCollider.enabled = false;
		}
		if (boxCollider != null)
		{
			boxCollider.enabled = false;
		}
	}
}
