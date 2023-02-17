using UnityEngine;

[ExecuteInEditMode]
public class HazardRespawnMarker : MonoBehaviour
{
	public bool respawnFacingRight;

	private float groundSenseDistance = 3f;

	private Vector2 groundSenseRay = -Vector2.up;

	private Vector2 heroSpawnLocation;

	public bool drawDebugRays;

	private void Awake()
	{
		if (base.transform.parent != null && base.transform.parent.name.Contains("top"))
		{
			groundSenseDistance = 50f;
		}
		heroSpawnLocation = Physics2D.Raycast(base.transform.position, groundSenseRay, groundSenseDistance, 256).point;
	}

	private void Update()
	{
		if (drawDebugRays)
		{
			Debug.DrawRay(base.transform.position, groundSenseRay * groundSenseDistance, Color.green);
			Debug.DrawRay(heroSpawnLocation - Vector2.right / 2f, Vector2.right, Color.green);
		}
	}
}
