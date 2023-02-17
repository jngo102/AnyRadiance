using UnityEngine;

public class MapMarkerButton : MonoBehaviour
{
	public enum DisableType
	{
		GameObject,
		SpriteRenderer
	}

	public int neededMarkerTypes = 2;

	public DisableType disable;

	public bool keepDisabled;

	private bool shouldDisable;

	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		PlayerData playerData = GameManager.instance.playerData;
		if (playerData != null)
		{
			if ((playerData.GetBool("hasMarker_b") ? 1 : 0) + (playerData.GetBool("hasMarker_r") ? 1 : 0) + (playerData.GetBool("hasMarker_w") ? 1 : 0) + (playerData.GetBool("hasMarker_y") ? 1 : 0) < neededMarkerTypes)
			{
				DoDisable();
				shouldDisable = true;
			}
			else
			{
				shouldDisable = false;
			}
		}
	}

	private void Update()
	{
		if (keepDisabled && shouldDisable)
		{
			DoDisable();
		}
	}

	private void DoDisable()
	{
		switch (disable)
		{
		case DisableType.GameObject:
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(value: false);
			}
			break;
		case DisableType.SpriteRenderer:
			if ((bool)spriteRenderer && spriteRenderer.enabled)
			{
				spriteRenderer.enabled = false;
			}
			break;
		}
	}
}
