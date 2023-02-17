using UnityEngine;

public class InvNailArtBackboard : MonoBehaviour
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
			if (!playerData.GetBool("hasNailArt") || playerData.GetBool("hasAllNailArts"))
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
