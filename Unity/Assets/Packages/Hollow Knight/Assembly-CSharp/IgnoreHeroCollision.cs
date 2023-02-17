using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class IgnoreHeroCollision : MonoBehaviour
{
	private void Start()
	{
		if (!HeroController.instance)
		{
			return;
		}
		if (HeroController.instance.isHeroInPosition)
		{
			Ignore();
			return;
		}
		HeroController.HeroInPosition temp = null;
		temp = delegate
		{
			Ignore();
			HeroController.instance.heroInPosition -= temp;
			temp = null;
		};
		HeroController.instance.heroInPosition += temp;
	}

	private void Ignore()
	{
		Collider2D component = GetComponent<Collider2D>();
		Collider2D[] components = HeroController.instance.GetComponents<Collider2D>();
		foreach (Collider2D collider in components)
		{
			Physics2D.IgnoreCollision(component, collider);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
		}
	}
}
