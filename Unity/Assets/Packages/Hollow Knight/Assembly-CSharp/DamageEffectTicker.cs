using System.Collections.Generic;
using UnityEngine;

public class DamageEffectTicker : MonoBehaviour
{
	public List<GameObject> enemyList;

	public float damageInterval;

	public string damageEvent;

	public ExtraDamageTypes extraDamageType;

	private float timer;

	private void OnEnable()
	{
		enemyList.Clear();
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (!(timer >= damageInterval))
		{
			return;
		}
		for (int i = 0; i < enemyList.Count; i++)
		{
			if (!(enemyList[i] == null))
			{
				PlayMakerFSM playMakerFSM = FSMUtility.LocateFSM(enemyList[i], "Extra Damage");
				if (playMakerFSM != null)
				{
					playMakerFSM.SendEvent(damageEvent);
				}
				enemyList[i].GetComponent<IExtraDamageable>()?.RecieveExtraDamage(extraDamageType);
			}
		}
		timer -= damageInterval;
	}

	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
		enemyList.Add(otherCollider.gameObject);
	}

	private void OnTriggerExit2D(Collider2D otherCollider)
	{
		enemyList.Remove(otherCollider.gameObject);
	}

	public void EmptyDamageList()
	{
		enemyList.Clear();
	}

	public void SetDamageEvent(string newEvent)
	{
		damageEvent = newEvent;
	}

	public void SetDamageInterval(float newInterval)
	{
		damageInterval = newInterval;
	}
}
