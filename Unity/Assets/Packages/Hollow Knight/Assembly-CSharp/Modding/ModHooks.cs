using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using GlobalEnums;
using HutongGames.PlayMaker;
using JetBrains.Annotations;
using Language;
using UnityEngine;

/// <summary>
///     Class to hook into various events for the game.
/// </summary>
[PublicAPI]
public class ModHooks
{
	public static event Action<List<GameObject>> DrawBlackBordersHook;

	public delegate bool OnEnableEnemyHandler(GameObject enemy, bool isAlreadyDead);

	/// <summary>
	///     Called when an enemy is enabled. Check this isDead flag to see if they're already dead. If you return true, this
	///     will mark the enemy as already dead on load. Default behavior is to return the value inside "isAlreadyDead".
	/// </summary>
	/// <see cref="T:Modding.Delegates.OnEnableEnemyHandler" />
	/// <remarks>HealthManager.CheckPersistence</remarks>
	public static event OnEnableEnemyHandler OnEnableEnemyHook;

	/// <summary>
	///     Called when an enemy recieves a death event. It looks like this event may be called multiple times on an enemy, so
	///     check "eventAlreadyRecieved" to see if the event has been fired more than once.
	/// </summary>
	/// <see cref="T:Modding.Delegates.OnReceiveDeathEventHandler" />
	/// <remarks>EnemyDeathEffects.RecieveDeathEvent</remarks>
	public static event OnReceiveDeathEventHandler OnReceiveDeathEventHook;

	public delegate void OnReceiveDeathEventHandler(EnemyDeathEffects enemyDeathEffects, bool eventAlreadyReceived, ref float? attackDirection, ref bool resetDeathEvent, ref bool spellBurn, ref bool isWatery);

	/// <summary>
	///     Called when an enemy dies and a journal kill is recorded. You may use the "playerDataName" string or one of the
	///     additional pre-formatted player data strings to look up values in playerData.
	/// </summary>
	/// <see cref="T:Modding.Delegates.RecordKillForJournalHandler" />
	/// <remarks>EnemyDeathEffects.OnRecordKillForJournal</remarks>
	public static event RecordKillForJournalHandler RecordKillForJournalHook;

	public delegate void RecordKillForJournalHandler(EnemyDeathEffects enemyDeathEffects, string playerDataName, string killedBoolPlayerDataLookupKey, string killCountIntPlayerDataLookupKey, string newDataBoolPlayerDataLookupKey);

	internal static void OnDrawBlackBorders(List<GameObject> borders)
	{
		if (ModHooks.DrawBlackBordersHook == null)
		{
			return;
		}
		Delegate[] invocationList = ModHooks.DrawBlackBordersHook.GetInvocationList();
		for (int i = 0; i < invocationList.Length; i++)
		{
			Action<List<GameObject>> action = (Action<List<GameObject>>)invocationList[i];
			try
			{
				action(borders);
			}
			catch (Exception message)
			{

			}
		}
	}

	internal static bool OnEnableEnemy(GameObject enemy, bool isAlreadyDead)
	{
		if (ModHooks.OnEnableEnemyHook == null)
		{
			return isAlreadyDead;
		}
		Delegate[] invocationList = ModHooks.OnEnableEnemyHook.GetInvocationList();
		for (int i = 0; i < invocationList.Length; i++)
		{
			OnEnableEnemyHandler onEnableEnemyHandler = (OnEnableEnemyHandler)invocationList[i];
			try
			{
				isAlreadyDead = onEnableEnemyHandler(enemy, isAlreadyDead);
			}
			catch (Exception message)
			{

			}
		}
		return isAlreadyDead;
	}

	internal static void OnRecordKillForJournal(EnemyDeathEffects enemyDeathEffects, string playerDataName, string killedBoolPlayerDataLookupKey, string killCountIntPlayerDataLookupKey, string newDataBoolPlayerDataLookupKey)
	{
		if (ModHooks.RecordKillForJournalHook == null)
		{
			return;
		}
		Delegate[] invocationList = ModHooks.RecordKillForJournalHook.GetInvocationList();
		for (int i = 0; i < invocationList.Length; i++)
		{
			RecordKillForJournalHandler recordKillForJournalHandler = (RecordKillForJournalHandler)invocationList[i];
			try
			{
				recordKillForJournalHandler(enemyDeathEffects, playerDataName, killedBoolPlayerDataLookupKey, killCountIntPlayerDataLookupKey, newDataBoolPlayerDataLookupKey);
			}
			catch (Exception message)
			{

			}
		}
	}

	internal static void OnRecieveDeathEvent(EnemyDeathEffects enemyDeathEffects, bool eventAlreadyRecieved, ref float? attackDirection, ref bool resetDeathEvent, ref bool spellBurn, ref bool isWatery)
	{
		if (ModHooks.OnReceiveDeathEventHook == null)
		{
			return;
		}
		Delegate[] invocationList = ModHooks.OnReceiveDeathEventHook.GetInvocationList();
		for (int i = 0; i < invocationList.Length; i++)
		{
			OnReceiveDeathEventHandler onReceiveDeathEventHandler = (OnReceiveDeathEventHandler)invocationList[i];
			try
			{
				onReceiveDeathEventHandler(enemyDeathEffects, eventAlreadyRecieved, ref attackDirection, ref resetDeathEvent, ref spellBurn, ref isWatery);
			}
			catch (Exception message)
			{

			}
		}
	}
}