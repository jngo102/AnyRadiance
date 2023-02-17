using UnityEngine;
using UnityEngine.UI;

public class SaveProfileMPSlots : MonoBehaviour
{
	private int slotsToShow;

	private Image[] mpUnits;

	public Sprite normalSoulOrb;

	public Sprite steelSoulOrb;

	private void Awake()
	{
		mpUnits = GetComponentsInChildren<Image>(includeInactive: true);
	}

	public void showMPSlots(int slotsToShow, bool steelsoulMode)
	{
		if (mpUnits == null)
		{
			Awake();
		}
		if (slotsToShow < 0)
		{
			return;
		}
		this.slotsToShow = slotsToShow;
		for (int i = 0; i < mpUnits.Length; i++)
		{
			if (i < slotsToShow)
			{
				mpUnits[i].gameObject.SetActive(value: true);
			}
			else
			{
				mpUnits[i].gameObject.SetActive(value: false);
			}
			if (steelsoulMode)
			{
				mpUnits[i].sprite = steelSoulOrb;
			}
			else
			{
				mpUnits[i].sprite = normalSoulOrb;
			}
		}
	}
}
