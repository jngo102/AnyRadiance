using UnityEngine;

public static class HitTaker
{
	private const int DefaultRecursionDepth = 3;

	public static void Hit(GameObject targetGameObject, HitInstance damageInstance, int recursionDepth = 3)
	{
		if (!(targetGameObject != null))
		{
			return;
		}
		Transform transform = targetGameObject.transform;
		for (int i = 0; i < recursionDepth; i++)
		{
			transform.GetComponent<IHitResponder>()?.Hit(damageInstance);
			transform = transform.parent;
			if (transform == null)
			{
				break;
			}
		}
	}
}
