using UnityEngine;

public class PreInstantiateGameObject : MonoBehaviour
{
	public GameObject prefab;

	private GameObject instantiatedGameObject;

	public GameObject InstantiatedGameObject => instantiatedGameObject;

	private void Awake()
	{
		if ((bool)prefab)
		{
			instantiatedGameObject = Object.Instantiate(prefab);
			instantiatedGameObject.SetActive(value: false);
		}
	}
}
