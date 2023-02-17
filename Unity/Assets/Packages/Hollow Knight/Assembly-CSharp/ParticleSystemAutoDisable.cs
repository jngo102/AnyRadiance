using UnityEngine;

public class ParticleSystemAutoDisable : MonoBehaviour
{
	private ParticleSystem ps;

	private bool activated;

	public void Start()
	{
		ps = GetComponent<ParticleSystem>();
		if (!ps)
		{
			ps = GetComponentInChildren<ParticleSystem>();
		}
	}

	public void Update()
	{
		if ((bool)ps)
		{
			if (ps.IsAlive())
			{
				activated = true;
			}
			if (!ps.IsAlive() && activated)
			{
				base.gameObject.SetActive(value: false);
			}
		}
	}
}
