using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	
	public class IStats : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		internal IStats(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(IStats obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IStats()
		{
			Dispose();
		}
	
		public virtual void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IStats(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public virtual void RequestUserStatsAndAchievements(GalaxyID userID, IUserStatsAndAchievementsRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserStatsAndAchievements__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID), IUserStatsAndAchievementsRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestUserStatsAndAchievements(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserStatsAndAchievements__SWIG_1(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestUserStatsAndAchievements()
		{
			GalaxyInstancePINVOKE.IStats_RequestUserStatsAndAchievements__SWIG_2(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual int GetStatInt(string name, GalaxyID userID)
		{
			int result = GalaxyInstancePINVOKE.IStats_GetStatInt__SWIG_0(swigCPtr, name, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual int GetStatInt(string name)
		{
			int result = GalaxyInstancePINVOKE.IStats_GetStatInt__SWIG_1(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual float GetStatFloat(string name, GalaxyID userID)
		{
			float result = GalaxyInstancePINVOKE.IStats_GetStatFloat__SWIG_0(swigCPtr, name, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual float GetStatFloat(string name)
		{
			float result = GalaxyInstancePINVOKE.IStats_GetStatFloat__SWIG_1(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void SetStatInt(string name, int value)
		{
			GalaxyInstancePINVOKE.IStats_SetStatInt(swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetStatFloat(string name, float value)
		{
			GalaxyInstancePINVOKE.IStats_SetStatFloat(swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void UpdateAvgRateStat(string name, float countThisSession, double sessionLength)
		{
			GalaxyInstancePINVOKE.IStats_UpdateAvgRateStat(swigCPtr, name, countThisSession, sessionLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void GetAchievement(string name, ref bool unlocked, ref uint unlockTime, GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_GetAchievement__SWIG_0(swigCPtr, name, ref unlocked, ref unlockTime, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void GetAchievement(string name, ref bool unlocked, ref uint unlockTime)
		{
			GalaxyInstancePINVOKE.IStats_GetAchievement__SWIG_1(swigCPtr, name, ref unlocked, ref unlockTime);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetAchievement(string name)
		{
			GalaxyInstancePINVOKE.IStats_SetAchievement(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void ClearAchievement(string name)
		{
			GalaxyInstancePINVOKE.IStats_ClearAchievement(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void StoreStatsAndAchievements(IStatsAndAchievementsStoreListener listener)
		{
			GalaxyInstancePINVOKE.IStats_StoreStatsAndAchievements__SWIG_0(swigCPtr, IStatsAndAchievementsStoreListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void StoreStatsAndAchievements()
		{
			GalaxyInstancePINVOKE.IStats_StoreStatsAndAchievements__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void ResetStatsAndAchievements(IStatsAndAchievementsStoreListener listener)
		{
			GalaxyInstancePINVOKE.IStats_ResetStatsAndAchievements__SWIG_0(swigCPtr, IStatsAndAchievementsStoreListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void ResetStatsAndAchievements()
		{
			GalaxyInstancePINVOKE.IStats_ResetStatsAndAchievements__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual string GetAchievementDisplayName(string name)
		{
			string result = GalaxyInstancePINVOKE.IStats_GetAchievementDisplayName(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetAchievementDisplayNameCopy(string name, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStats_GetAchievementDisplayNameCopy(swigCPtr, name, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}
	
		public virtual string GetAchievementDescription(string name)
		{
			string result = GalaxyInstancePINVOKE.IStats_GetAchievementDescription(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetAchievementDescriptionCopy(string name, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStats_GetAchievementDescriptionCopy(swigCPtr, name, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}
	
		public virtual bool IsAchievementVisible(string name)
		{
			bool result = GalaxyInstancePINVOKE.IStats_IsAchievementVisible(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool IsAchievementVisibleWhileLocked(string name)
		{
			bool result = GalaxyInstancePINVOKE.IStats_IsAchievementVisibleWhileLocked(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void RequestLeaderboards(ILeaderboardsRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboards__SWIG_0(swigCPtr, ILeaderboardsRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestLeaderboards()
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboards__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual string GetLeaderboardDisplayName(string name)
		{
			string result = GalaxyInstancePINVOKE.IStats_GetLeaderboardDisplayName(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetLeaderboardDisplayNameCopy(string name, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStats_GetLeaderboardDisplayNameCopy(swigCPtr, name, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}
	
		public virtual LeaderboardSortMethod GetLeaderboardSortMethod(string name)
		{
			LeaderboardSortMethod result = (LeaderboardSortMethod)GalaxyInstancePINVOKE.IStats_GetLeaderboardSortMethod(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual LeaderboardDisplayType GetLeaderboardDisplayType(string name)
		{
			LeaderboardDisplayType result = (LeaderboardDisplayType)GalaxyInstancePINVOKE.IStats_GetLeaderboardDisplayType(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void RequestLeaderboardEntriesGlobal(string name, uint rangeStart, uint rangeEnd, ILeaderboardEntriesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesGlobal__SWIG_0(swigCPtr, name, rangeStart, rangeEnd, ILeaderboardEntriesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestLeaderboardEntriesGlobal(string name, uint rangeStart, uint rangeEnd)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesGlobal__SWIG_1(swigCPtr, name, rangeStart, rangeEnd);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestLeaderboardEntriesAroundUser(string name, uint countBefore, uint countAfter, GalaxyID userID, ILeaderboardEntriesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesAroundUser__SWIG_0(swigCPtr, name, countBefore, countAfter, GalaxyID.getCPtr(userID), ILeaderboardEntriesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestLeaderboardEntriesAroundUser(string name, uint countBefore, uint countAfter, GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesAroundUser__SWIG_1(swigCPtr, name, countBefore, countAfter, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestLeaderboardEntriesAroundUser(string name, uint countBefore, uint countAfter)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesAroundUser__SWIG_2(swigCPtr, name, countBefore, countAfter);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestLeaderboardEntriesForUsers(string name, ref GalaxyID[] userArray, ILeaderboardEntriesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesForUsers__SWIG_0(swigCPtr, name, Array.ConvertAll(userArray, (GalaxyID id) => id.ToUint64()), (uint)userArray.LongLength, ILeaderboardEntriesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestLeaderboardEntriesForUsers(string name, ref GalaxyID[] userArray)
		{
			GalaxyInstancePINVOKE.IStats_RequestLeaderboardEntriesForUsers__SWIG_1(swigCPtr, name, Array.ConvertAll(userArray, (GalaxyID id) => id.ToUint64()), (uint)userArray.LongLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void GetRequestedLeaderboardEntry(uint index, ref uint rank, ref int score, ref GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_GetRequestedLeaderboardEntry(swigCPtr, index, ref rank, ref score, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void GetRequestedLeaderboardEntryWithDetails(uint index, ref uint rank, ref int score, byte[] details, uint detailsSize, ref uint outDetailsSize, ref GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_GetRequestedLeaderboardEntryWithDetails(swigCPtr, index, ref rank, ref score, details, detailsSize, ref outDetailsSize, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetLeaderboardScore(string name, int score, bool forceUpdate, ILeaderboardScoreUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScore__SWIG_0(swigCPtr, name, score, forceUpdate, ILeaderboardScoreUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetLeaderboardScore(string name, int score, bool forceUpdate)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScore__SWIG_1(swigCPtr, name, score, forceUpdate);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetLeaderboardScore(string name, int score)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScore__SWIG_2(swigCPtr, name, score);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetLeaderboardScoreWithDetails(string name, int score, byte[] details, uint detailsSize, bool forceUpdate, ILeaderboardScoreUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScoreWithDetails__SWIG_0(swigCPtr, name, score, details, detailsSize, forceUpdate, ILeaderboardScoreUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetLeaderboardScoreWithDetails(string name, int score, byte[] details, uint detailsSize, bool forceUpdate)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScoreWithDetails__SWIG_1(swigCPtr, name, score, details, detailsSize, forceUpdate);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetLeaderboardScoreWithDetails(string name, int score, byte[] details, uint detailsSize)
		{
			GalaxyInstancePINVOKE.IStats_SetLeaderboardScoreWithDetails__SWIG_2(swigCPtr, name, score, details, detailsSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint GetLeaderboardEntryCount(string name)
		{
			uint result = GalaxyInstancePINVOKE.IStats_GetLeaderboardEntryCount(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void FindLeaderboard(string name, ILeaderboardRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_FindLeaderboard__SWIG_0(swigCPtr, name, ILeaderboardRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void FindLeaderboard(string name)
		{
			GalaxyInstancePINVOKE.IStats_FindLeaderboard__SWIG_1(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void FindOrCreateLeaderboard(string name, string displayName, LeaderboardSortMethod sortMethod, LeaderboardDisplayType displayType, ILeaderboardRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_FindOrCreateLeaderboard__SWIG_0(swigCPtr, name, displayName, (int)sortMethod, (int)displayType, ILeaderboardRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void FindOrCreateLeaderboard(string name, string displayName, LeaderboardSortMethod sortMethod, LeaderboardDisplayType displayType)
		{
			GalaxyInstancePINVOKE.IStats_FindOrCreateLeaderboard__SWIG_1(swigCPtr, name, displayName, (int)sortMethod, (int)displayType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestUserTimePlayed(GalaxyID userID, IUserTimePlayedRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserTimePlayed__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID), IUserTimePlayedRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestUserTimePlayed(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IStats_RequestUserTimePlayed__SWIG_1(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestUserTimePlayed()
		{
			GalaxyInstancePINVOKE.IStats_RequestUserTimePlayed__SWIG_2(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint GetUserTimePlayed(GalaxyID userID)
		{
			uint result = GalaxyInstancePINVOKE.IStats_GetUserTimePlayed__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint GetUserTimePlayed()
		{
			uint result = GalaxyInstancePINVOKE.IStats_GetUserTimePlayed__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}