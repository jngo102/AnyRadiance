using UnityEngine;

public class DeactivateAfterDelay : MonoBehaviour
{
	public float time;

	public bool stayInPlace;

	private float timer;

	private Vector3 worldPos;

	private Vector3 startPos;

	private void Awake()
	{
		if (stayInPlace)
		{
			startPos = base.transform.localPosition;
		}
	}

	private void OnEnable()
	{
		timer = time;
		if (stayInPlace)
		{
			base.transform.localPosition = startPos;
			worldPos = base.transform.position;
		}
	}

	private void Update()
	{
		if (timer > 0f)
		{
			timer -= Time.deltaTime;
			if (stayInPlace)
			{
				base.transform.position = worldPos;
			}
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
