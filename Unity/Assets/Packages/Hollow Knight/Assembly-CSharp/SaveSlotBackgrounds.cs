using GlobalEnums;
using UnityEngine;

public class SaveSlotBackgrounds : MonoBehaviour
{
	[SerializeField]
	public AreaBackground[] areaBackgrounds;

	public AreaBackground GetBackground(string areaName)
	{
		if (areaBackgrounds != null && areaBackgrounds.Length != 0)
		{
			for (int i = 0; i < areaBackgrounds.Length; i++)
			{
				if (areaBackgrounds[i].areaName.ToString() == areaName)
				{
					return areaBackgrounds[i];
				}
			}
			return null;
		}
		Debug.LogError("No background images have been created in this prefab.");
		return null;
	}

	public AreaBackground GetBackground(MapZone mapZone)
	{
		if (areaBackgrounds != null && areaBackgrounds.Length != 0)
		{
			for (int i = 0; i < areaBackgrounds.Length; i++)
			{
				if (areaBackgrounds[i].areaName == mapZone)
				{
					return areaBackgrounds[i];
				}
			}
			return null;
		}
		Debug.LogError("No background images have been created in this prefab.");
		return null;
	}
}
