using UnityEngine;

public class InvRelicBackboard : MonoBehaviour
{
	public Sprite activeSprite;

	public Sprite inactiveSprite;

	private PlayerData playerData;

	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		if (spriteRenderer != null)
		{
			if (playerData == null)
			{
				playerData = PlayerData.instance;
			}
			if (!playerData.GetBool("foundTrinket1") && !playerData.GetBool("foundTrinket2") && !playerData.GetBool("foundTrinket3") && !playerData.GetBool("foundTrinket4"))
			{
				spriteRenderer.sprite = inactiveSprite;
			}
			else
			{
				spriteRenderer.sprite = activeSprite;
			}
		}
	}
}
