using UnityEngine;

public class GrassCut : MonoBehaviour
{
	public SpriteRenderer[] disable;

	public SpriteRenderer[] enable;

	[Space]
	public Collider2D[] disableColliders;

	public Collider2D[] enableColliders;

	[Space]
	public GameObject particles;

	public GameObject cutEffectPrefab;

	private Collider2D col;

	private void Awake()
	{
		col = GetComponent<Collider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!ShouldCut(collision))
		{
			return;
		}
		GrassBehaviour componentInParent = GetComponentInParent<GrassBehaviour>();
		SpriteRenderer[] array = disable;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
		array = enable;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
		Collider2D[] array2 = disableColliders;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].enabled = false;
		}
		array2 = enableColliders;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].enabled = true;
		}
		if ((bool)particles)
		{
			Renderer component = particles.GetComponent<Renderer>();
			if ((bool)component && (bool)componentInParent)
			{
				component.material.color = componentInParent.SharedMaterial.color;
			}
			particles.SetActive(value: true);
			particles.transform.position = new Vector3(particles.transform.position.x, particles.transform.position.y, particles.transform.position.z);
		}
		if ((bool)componentInParent)
		{
			componentInParent.CutReact(collision);
		}
		if ((bool)cutEffectPrefab)
		{
			Vector3 position = (collision.bounds.center + col.bounds.center) / 2f;
			float num = Mathf.Sign(base.transform.position.x - collision.transform.position.x);
			cutEffectPrefab.Spawn(position);
			cutEffectPrefab.transform.SetScaleX((0f - num) * 0.6f);
			cutEffectPrefab.transform.SetScaleY(1f);
		}
		Object.Destroy(this);
	}

	public static bool ShouldCut(Collider2D collision)
	{
		if (!(collision.tag == "Nail Attack") && (!(collision.tag == "HeroBox") || !HeroController.instance.cState.superDashing))
		{
			return collision.tag == "Sharp Shadow";
		}
		return true;
	}
}
