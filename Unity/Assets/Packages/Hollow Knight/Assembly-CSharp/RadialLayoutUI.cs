using System;
using UnityEngine;

[ExecuteInEditMode]
public class RadialLayoutUI : MonoBehaviour
{
	public float scale = 1f;

	private float oldScale;

	public float radius = 1f;

	private float oldRadius;

	public bool elementOffset = true;

	private bool oldElementOffset;

	public float splitX;

	private float oldSplitX;

	public float splitY;

	private float oldSplitY;

	public bool counterClockwise;

	private bool oldCounterClockwise;

	private bool hasValueChanged;

	private void Update()
	{
		if (HasValueChanged())
		{
			UpdateUI();
		}
	}

	private void OnTransformChildrenChanged()
	{
		hasValueChanged = true;
	}

	public bool HasValueChanged()
	{
		if (hasValueChanged)
		{
			hasValueChanged = false;
			return true;
		}
		if (scale != oldScale || radius != oldRadius || elementOffset != oldElementOffset || splitX != oldSplitX || splitY != oldSplitY || counterClockwise != oldCounterClockwise)
		{
			oldScale = scale;
			oldRadius = radius;
			oldElementOffset = elementOffset;
			oldSplitX = splitX;
			oldSplitY = splitY;
			oldCounterClockwise = counterClockwise;
			return true;
		}
		return false;
	}

	public void UpdateUI()
	{
		float num = radius * scale;
		float num2 = splitX * scale;
		float num3 = splitY * scale;
		int childCount = base.transform.childCount;
		float num4 = (elementOffset ? (360f / (float)childCount / 2f) : 0f);
		for (int i = 0; i < childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			float num5 = (float)(i + 1) / (float)childCount * 360f - num4;
			if (counterClockwise)
			{
				num5 = 360f - num5;
			}
			num5 *= (float)Math.PI / 180f;
			Vector3 localPosition = new Vector3(num * Mathf.Sin(num5), num * Mathf.Cos(num5), 0f);
			localPosition.x += ((localPosition.x > 0f) ? num2 : (0f - num2));
			localPosition.y += ((localPosition.y > 0f) ? num3 : (0f - num3));
			child.localPosition = localPosition;
		}
	}
}
