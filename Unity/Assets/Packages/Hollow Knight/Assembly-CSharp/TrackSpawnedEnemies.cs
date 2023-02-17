using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackSpawnedEnemies : MonoBehaviour
{
	private List<HealthManager> trackedEnemies = new List<HealthManager>();

	public int TotalTracked => trackedEnemies.Count;

	public int TotalAlive => Enumerable.ToList(Enumerable.Where(trackedEnemies, (HealthManager enemy) => (bool)enemy && enemy.hp > 0)).Count;

	public void Add(HealthManager enemyHealthManager)
	{
		trackedEnemies.Add(enemyHealthManager);
	}
}
