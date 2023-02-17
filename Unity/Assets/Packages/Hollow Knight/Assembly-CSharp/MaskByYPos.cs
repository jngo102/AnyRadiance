using UnityEngine;

public class MaskByYPos : MonoBehaviour
{
	public SpriteRenderer spriteRenderer;

	public float yPos;

	public bool maskIfAboveY;

	public bool maskIfBelowY;

	private void OnEnable()
	{
		if (spriteRenderer == null)
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}
	}

	private void Update()
	{
		float y = base.transform.position.y;
		if (maskIfAboveY)
		{
			if (y < yPos)
			{
				if (!spriteRenderer.enabled)
				{
					spriteRenderer.enabled = true;
				}
			}
			else if (spriteRenderer.enabled)
			{
				spriteRenderer.enabled = false;
			}
		}
		if (!maskIfBelowY)
		{
			return;
		}
		if (y > yPos)
		{
			if (!spriteRenderer.enabled)
			{
				spriteRenderer.enabled = true;
			}
		}
		else if (spriteRenderer.enabled)
		{
			spriteRenderer.enabled = false;
		}
	}
}
