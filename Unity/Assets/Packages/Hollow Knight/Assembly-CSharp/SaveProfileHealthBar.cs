using UnityEngine;
using UnityEngine.UI;

public class SaveProfileHealthBar : MonoBehaviour
{
	private int buttonsToShow;

	private Image[] healthUnits;

	public Sprite normalHealth;

	public Sprite steelHealth;

	public void Awake()
	{
		healthUnits = GetComponentsInChildren<Image>(includeInactive: true);
	}

	public void showHealth(int numberToShow, bool steelsoulMode)
	{
		if (healthUnits == null)
		{
			Awake();
		}
		if (numberToShow <= 0)
		{
			return;
		}
		buttonsToShow = numberToShow;
		for (int i = 0; i < healthUnits.Length; i++)
		{
			if (i < buttonsToShow)
			{
				healthUnits[i].gameObject.SetActive(value: true);
			}
			else
			{
				healthUnits[i].gameObject.SetActive(value: false);
			}
			if (steelsoulMode)
			{
				healthUnits[i].sprite = steelHealth;
			}
			else
			{
				healthUnits[i].sprite = normalHealth;
			}
		}
	}
}
