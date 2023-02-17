using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SetVersionNumber : MonoBehaviour
{
	private Text textUi;

	private void Awake()
	{
		textUi = GetComponent<Text>();
	}

	private void Start()
	{
		if (textUi != null)
		{
			StringBuilder stringBuilder = new StringBuilder("1.5.78.11833");
			if (CheatManager.IsCheatsEnabled)
			{
				stringBuilder.Append("\n(CHEATS ENABLED)");
			}
			textUi.text = stringBuilder.ToString();
		}
	}
}
