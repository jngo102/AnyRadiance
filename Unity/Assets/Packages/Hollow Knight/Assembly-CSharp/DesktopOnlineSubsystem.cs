using System;
using UnityEngine;

public abstract class DesktopOnlineSubsystem : IDisposable
{
	public abstract bool AreAchievementsFetched { get; }

	public virtual bool HasNativeAchievementsDialog => false;

	public virtual bool HandlesGameSaves => false;

	public virtual bool WillPreloadSaveFiles => true;

	public virtual Platform.EngagementRequirements EngagementRequirement => Platform.EngagementRequirements.Invisible;

	public virtual Platform.EngagementStates EngagementState => Platform.EngagementStates.Engaged;

	public virtual string EngagedDisplayName => null;

	public virtual Texture2D EngagedDisplayImage => null;

	public virtual void Dispose()
	{
	}

	public virtual void Update()
	{
	}

	public abstract bool? IsAchievementUnlocked(string achievementId);

	public abstract void PushAchievementUnlock(string achievementId);

	public abstract void ResetAchievements();

	public virtual void IsSaveSlotInUse(int slotIndex, Action<bool> callback)
	{
	}

	public virtual void ReadSaveSlot(int slotIndex, Action<byte[]> callback)
	{
	}

	public virtual void WriteSaveSlot(int slotIndex, byte[] bytes, Action<bool> callback)
	{
	}

	public virtual void ClearSaveSlot(int slotIndex, Action<bool> callback)
	{
	}
}
