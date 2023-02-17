using UnityEngine;

public class InvMarkerCollide : MonoBehaviour
{
	public MapMarkerMenu markerMenu;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject gameObject = collision.gameObject;
		InvMarker component = gameObject.GetComponent<InvMarker>();
		if ((bool)component)
		{
			component.markerMenu = markerMenu;
			markerMenu.AddToCollidingList(gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		GameObject gameObject = collision.gameObject;
		if ((bool)gameObject.GetComponent<InvMarker>())
		{
			markerMenu.RemoveFromCollidingList(gameObject);
		}
	}
}
