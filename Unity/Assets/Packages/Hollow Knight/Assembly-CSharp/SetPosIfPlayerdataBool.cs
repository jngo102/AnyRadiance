using UnityEngine;

public class SetPosIfPlayerdataBool : MonoBehaviour
{
	public string playerDataBool;

	public bool setX;

	public float XPos;

	public bool setY;

	public float YPos;

	public bool onceOnly;

	private bool hasSet;

	private bool hasChecked;

	private void OnEnable()
	{
		SetPosIfPlayerdataBool[] components = GetComponents<SetPosIfPlayerdataBool>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].DoCheck();
		}
	}

	private void OnDisable()
	{
		hasChecked = false;
	}

	private void DoCheck()
	{
		if (hasChecked)
		{
			return;
		}
		hasChecked = true;
		if ((!hasSet || !onceOnly) && PlayerData.instance.GetBool(playerDataBool))
		{
			if (setX)
			{
				base.transform.localPosition = new Vector3(XPos, base.transform.localPosition.y, base.transform.localPosition.z);
			}
			if (setY)
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, YPos, base.transform.localPosition.z);
			}
			hasSet = true;
		}
	}
}
