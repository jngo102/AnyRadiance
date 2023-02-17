using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AlertRange : MonoBehaviour
{
	private bool isHeroInRange;

	private Collider2D[] colliders;

	public bool IsHeroInRange => isHeroInRange;

	protected void Awake()
	{
		colliders = GetComponents<Collider2D>();
	}

	protected void OnTriggerEnter2D(Collider2D other)
	{
		isHeroInRange = true;
	}

	protected void OnTriggerExit2D(Collider2D other)
	{
		if (colliders.Length <= 1 || !StillInColliders())
		{
			isHeroInRange = false;
		}
	}

	private bool StillInColliders()
	{
		bool flag = false;
		Collider2D[] array = colliders;
		foreach (Collider2D collider2D in array)
		{
			if (collider2D is CircleCollider2D)
			{
				CircleCollider2D circleCollider2D = (CircleCollider2D)collider2D;
				flag = Physics2D.OverlapCircle(base.transform.TransformPoint(circleCollider2D.offset), circleCollider2D.radius * Mathf.Max(base.transform.localScale.x, base.transform.localScale.y), 512) != null;
			}
			else if (collider2D is BoxCollider2D)
			{
				BoxCollider2D boxCollider2D = (BoxCollider2D)collider2D;
				flag = Physics2D.OverlapBox(base.transform.TransformPoint(boxCollider2D.offset), new Vector2(boxCollider2D.size.x * base.transform.localScale.x, boxCollider2D.size.y * base.transform.localScale.y), base.transform.eulerAngles.z, 512) != null;
			}
			if (flag)
			{
				break;
			}
		}
		return flag;
	}

	public static AlertRange Find(GameObject root, string childName)
	{
		if (root == null)
		{
			return null;
		}
		bool flag = !string.IsNullOrEmpty(childName);
		Transform transform = root.transform;
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			AlertRange component = child.GetComponent<AlertRange>();
			if (!(component == null) && (!flag || !(child.gameObject.name != childName)))
			{
				return component;
			}
		}
		return null;
	}
}
