using UnityEngine;

public class ConstrainPosition : MonoBehaviour
{
	public bool constrainX;

	public float xMin;

	public float xMax;

	public bool constrainY;

	public float yMin;

	public float yMax;

	private void Update()
	{
		Vector3 position = base.transform.position;
		bool flag = false;
		if (constrainX)
		{
			if (position.x < xMin)
			{
				position.x = xMin;
				flag = true;
			}
			else if (position.x > xMax)
			{
				position.x = xMax;
				flag = true;
			}
		}
		if (constrainY)
		{
			if (position.y < yMin)
			{
				position.y = yMin;
				flag = true;
			}
			else if (position.y > yMax)
			{
				position.y = yMax;
				flag = true;
			}
		}
		if (flag)
		{
			base.transform.position = position;
		}
	}
}
