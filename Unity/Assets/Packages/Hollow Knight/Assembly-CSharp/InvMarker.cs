using UnityEngine;

public class InvMarker : MonoBehaviour
{
	public MapMarkerMenu markerMenu;

	public int colour;

	public int id;

	private void OnDisable()
	{
		if ((bool)markerMenu)
		{
			markerMenu.RemoveFromCollidingList(base.gameObject);
		}
	}
}
