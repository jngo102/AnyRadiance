using UnityEngine;

public class DisplayOnWorldMapOnly : MonoBehaviour
{
	public GameMap gameMap;

	private MeshRenderer meshRenderer;

	private void OnEnable()
	{
		if (meshRenderer == null)
		{
			meshRenderer = GetComponent<MeshRenderer>();
		}
		if (gameMap.displayNextArea)
		{
			meshRenderer.enabled = false;
		}
		else
		{
			meshRenderer.enabled = true;
		}
	}
}
