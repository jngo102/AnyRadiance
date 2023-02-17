using UnityEngine;

public struct Sweep
{
	public int CardinalDirection;

	public Vector2 Direction;

	public Vector2 ColliderOffset;

	public Vector2 ColliderExtents;

	public float SkinThickness;

	public int RayCount;

	private const float DefaultSkinThickness = 0.1f;

	public const int DefaultRayCount = 3;

	public Sweep(Collider2D collider, int cardinalDirection, int rayCount, float skinThickness = 0.1f)
	{
		CardinalDirection = cardinalDirection;
		Direction = new Vector2(DirectionUtils.GetX(cardinalDirection), DirectionUtils.GetY(cardinalDirection));
		ColliderOffset = collider.offset.MultiplyElements((Vector2)collider.transform.localScale);
		ColliderExtents = collider.bounds.extents;
		RayCount = rayCount;
		SkinThickness = skinThickness;
	}

	public bool Check(Vector2 offset, float distance, int layerMask)
	{
		float clippedDistance;
		return Check(offset, distance, layerMask, out clippedDistance);
	}

	public bool Check(Vector2 offset, float distance, int layerMask, out float clippedDistance)
	{
		if (distance <= 0f)
		{
			clippedDistance = 0f;
			return false;
		}
		Vector2 vector = ColliderOffset + Vector2.Scale(ColliderExtents, Direction);
		Vector2 vector2 = Vector2.Scale(ColliderExtents, new Vector2(Mathf.Abs(Direction.y), Mathf.Abs(Direction.x)));
		float num = distance;
		for (int i = 0; i < RayCount; i++)
		{
			float num2 = 2f * ((float)i / (float)(RayCount - 1)) - 1f;
			Vector2 vector3 = vector + vector2 * num2 + Direction * (0f - SkinThickness);
			Vector2 vector4 = offset + vector3;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(vector4, Direction, num + SkinThickness, layerMask);
			float num3 = raycastHit2D.distance - SkinThickness;
			if ((bool)raycastHit2D && num3 < num)
			{
				num = num3;
				Debug.DrawLine(vector4, vector4 + Direction * raycastHit2D.distance, Color.red);
			}
			else
			{
				Debug.DrawLine(vector4, vector4 + Direction * (distance + SkinThickness), Color.green);
			}
		}
		clippedDistance = num;
		return distance - num > Mathf.Epsilon;
	}
}
