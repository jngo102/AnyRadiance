using GlobalEnums;
using UnityEngine;

public class DeactivateGameObjectPerBuildType : MonoBehaviour
{
	[SerializeField]
	private BuildTypes[] buildTypes;

	private void OnEnable()
	{
		BuildTypes[] array = buildTypes;
		for (int i = 0; i < array.Length; i++)
		{
			_ = array[i];
		}
	}
}
