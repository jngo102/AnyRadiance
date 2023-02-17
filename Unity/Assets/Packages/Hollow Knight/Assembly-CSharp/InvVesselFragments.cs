using UnityEngine;

public class InvVesselFragments : MonoBehaviour
{
	public SpriteRenderer self;

	public SpriteRenderer piece1;

	public SpriteRenderer piece2;

	public SpriteRenderer full;

	public Sprite backboardSprite;

	public Sprite singlePieceSprite;

	public Sprite doublePieceSprite;

	public Sprite fullSprite;

	public Sprite emptySprite;

	private PlayerData playerData;

	private void OnEnable()
	{
		if (playerData == null)
		{
			playerData = PlayerData.instance;
		}
		if (playerData.GetInt("MPReserveMax") == playerData.GetInt("MPReserveCap"))
		{
			full.sprite = fullSprite;
			self.sprite = emptySprite;
			piece1.sprite = emptySprite;
			piece2.sprite = emptySprite;
		}
		else if (playerData.GetInt("vesselFragments") == 2)
		{
			full.sprite = emptySprite;
			self.sprite = backboardSprite;
			piece1.sprite = emptySprite;
			piece2.sprite = doublePieceSprite;
		}
		else if (playerData.GetInt("vesselFragments") == 1)
		{
			full.sprite = emptySprite;
			self.sprite = backboardSprite;
			piece1.sprite = singlePieceSprite;
			piece2.sprite = emptySprite;
		}
		else
		{
			full.sprite = emptySprite;
			self.sprite = backboardSprite;
			piece1.sprite = emptySprite;
			piece2.sprite = emptySprite;
		}
	}
}
