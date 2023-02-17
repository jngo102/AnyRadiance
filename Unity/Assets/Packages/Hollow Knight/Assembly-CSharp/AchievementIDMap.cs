using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "AchievementIDMap", menuName = "Hollow Knight/Achievement ID Map", order = 1900)]
public class AchievementIDMap : ScriptableObject
{
	[Serializable]
	public class AchievementIDPair
	{
		[SerializeField]
		[FormerlySerializedAs("achievementId")]
		private string internalId;

		[SerializeField]
		[FormerlySerializedAs("trophyId")]
		private int serviceId;

		public string InternalId => internalId;

		public int ServiceId => serviceId;
	}

	[SerializeField]
	private AchievementIDPair[] pairs;

	private Dictionary<string, int> serviceIdsByInternalId;

	public int? GetServiceIdForInternalId(string internalId)
	{
		if (serviceIdsByInternalId == null)
		{
			serviceIdsByInternalId = new Dictionary<string, int>();
			for (int i = 0; i < pairs.Length; i++)
			{
				AchievementIDPair achievementIDPair = pairs[i];
				serviceIdsByInternalId.Add(achievementIDPair.InternalId, achievementIDPair.ServiceId);
			}
		}
		if (!serviceIdsByInternalId.TryGetValue(internalId, out var value))
		{
			return null;
		}
		return value;
	}
}
