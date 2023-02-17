using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerActivateGameObject : MonoBehaviour
{
	public bool deactivateObjectOnLoad;

	private bool active;

	public GameObject gameObjectToSet;

	private void Start()
	{
		if (deactivateObjectOnLoad && gameObjectToSet != null)
		{
			gameObjectToSet.SetActive(value: false);
		}
	}

	public void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if (otherCollider.gameObject.layer == 9)
		{
			gameObjectToSet.SetActive(value: true);
			active = true;
		}
	}

	public void OnTriggerStay2D(Collider2D otherCollider)
	{
		if (!active && otherCollider.gameObject.layer == 9)
		{
			OnTriggerEnter2D(otherCollider);
		}
	}

	public void OnTriggerExit2D(Collider2D otherCollider)
	{
		if (otherCollider.gameObject.layer == 9)
		{
			gameObjectToSet.SetActive(value: false);
			active = false;
		}
	}
}
