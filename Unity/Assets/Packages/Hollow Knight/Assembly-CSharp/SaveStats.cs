using System;
using System.Collections.Generic;
using GlobalEnums;

[Serializable]
public class SaveStats
{
	private PlayTime playTimeStruct;

	public int maxHealth { get; private set; }

	public int geo { get; private set; }

	public MapZone mapZone { get; private set; }

	public float playTime { get; private set; }

	public int maxMPReserve { get; private set; }

	public int permadeathMode { get; private set; }

	public bool bossRushMode { get; private set; }

	public float completionPercentage { get; private set; }

	public bool unlockedCompletionRate { get; private set; }

	public Dictionary<string, string> LoadedMods { get; set; }

	public string Name { get; set; }

	public SaveStats(int maxHealth, int geo, MapZone mapZone, float playTime, int maxMPReserve, int permadeathMode, bool bossRushMode, float completionPercentage, bool unlockedCompletionRate)
	{
		this.maxHealth = maxHealth;
		this.geo = geo;
		this.mapZone = mapZone;
		this.playTime = playTime;
		this.maxMPReserve = maxMPReserve;
		this.permadeathMode = permadeathMode;
		this.bossRushMode = bossRushMode;
		this.completionPercentage = completionPercentage;
		playTimeStruct.RawTime = playTime;
		this.unlockedCompletionRate = unlockedCompletionRate;
	}

	public string GetPlaytimeHHMM()
	{
		if (playTimeStruct.HasHours)
		{
			return $"{(int)playTimeStruct.Hours:0}h {(int)playTimeStruct.Minutes:00}m";
		}
		return $"{(int)playTimeStruct.Minutes:0}m";
	}

	public string GetPlaytimeHHMMSS()
	{
		if (!playTimeStruct.HasHours)
		{
			return $"{(int)playTimeStruct.Minutes:0}m {(int)playTimeStruct.Seconds:00}s";
		}
		if (!playTimeStruct.HasMinutes)
		{
			return $"{(int)playTimeStruct.Seconds:0}s";
		}
		return $"{(int)playTimeStruct.Hours:0}h {(int)playTimeStruct.Minutes:00}m {(int)playTimeStruct.Seconds:00}s";
	}

	public string GetCompletionPercentage()
	{
		return completionPercentage + "%";
	}

	public int GetMPSlotsVisible()
	{
		return (int)((float)maxMPReserve / 33f);
	}
}
