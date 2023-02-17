using UnityEngine;

namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when an enemy is enabled. Check this isDead flag to see if they're already dead. If you return true, this
	///     will mark the enemy as already dead on load. Default behavior is to return the value inside "isAlreadyDead".
	/// </summary>
	public delegate bool OnEnableEnemyHandler(GameObject enemy, bool isAlreadyDead);
}