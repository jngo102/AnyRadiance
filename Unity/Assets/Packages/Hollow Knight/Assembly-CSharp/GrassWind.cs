using System.Collections;
using UnityEngine;

public class GrassWind : MonoBehaviour
{
	private Collider2D col;

	private void Awake()
	{
		col = GetComponent<Collider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Nail Attack")
		{
			GrassBehaviour componentInParent = GetComponentInParent<GrassBehaviour>();
			if ((bool)componentInParent)
			{
				StartCoroutine(DelayReact(componentInParent, collision));
			}
		}
	}

	private IEnumerator DelayReact(GrassBehaviour behaviour, Collider2D collision)
	{
		yield return null;
		behaviour.WindReact(collision);
	}
}
