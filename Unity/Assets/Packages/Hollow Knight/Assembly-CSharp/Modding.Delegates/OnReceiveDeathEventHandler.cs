namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when an enemy recieves a death event. It looks like this event may be called multiple times on an enemy, so
	///     check "eventAlreadyReceived" to see if the event has been fired more than once.
	/// </summary>
	public delegate void OnReceiveDeathEventHandler(EnemyDeathEffects enemyDeathEffects, bool eventAlreadyReceived, ref float? attackDirection, ref bool resetDeathEvent, ref bool spellBurn, ref bool isWatery);
}