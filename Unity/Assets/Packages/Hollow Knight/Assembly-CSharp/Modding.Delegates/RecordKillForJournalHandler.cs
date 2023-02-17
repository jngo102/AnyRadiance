namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when an enemy dies and a journal kill is recorded. You may use the "playerDataName" string or one of the
	///     additional pre-formatted player data strings to look up values in playerData.
	/// </summary>
	public delegate void RecordKillForJournalHandler(EnemyDeathEffects enemyDeathEffects, string playerDataName, string killedBoolPlayerDataLookupKey, string killCountIntPlayerDataLookupKey, string newDataBoolPlayerDataLookupKey);
}