using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	
	public class IMatchmaking : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		internal IMatchmaking(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(IMatchmaking obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IMatchmaking()
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
						GalaxyInstancePINVOKE.delete_IMatchmaking(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public bool SendLobbyMessage(GalaxyID lobbyID, string msg)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(msg);
			return SendLobbyMessage(lobbyID, bytes, (uint)bytes.Length);
		}
	
		public uint GetLobbyMessage(GalaxyID lobbyID, uint messageID, ref GalaxyID _senderID, out string msg, uint internalBufferLen = 1024u)
		{
			byte[] msg2 = new byte[internalBufferLen];
			GalaxyID senderID = new GalaxyID();
			uint lobbyMessage = GetLobbyMessage(lobbyID, messageID, ref senderID, ref msg2, (uint)msg2.Length);
			msg = Encoding.UTF8.GetString(msg2, 0, (int)lobbyMessage);
			return lobbyMessage;
		}
	
		public virtual void CreateLobby(LobbyType lobbyType, uint maxMembers, bool joinable, LobbyTopologyType lobbyTopologyType, ILobbyCreatedListener lobbyCreatedListener, ILobbyEnteredListener lobbyEnteredListener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_CreateLobby__SWIG_0(swigCPtr, (int)lobbyType, maxMembers, joinable, (int)lobbyTopologyType, ILobbyCreatedListener.getCPtr(lobbyCreatedListener), ILobbyEnteredListener.getCPtr(lobbyEnteredListener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void CreateLobby(LobbyType lobbyType, uint maxMembers, bool joinable, LobbyTopologyType lobbyTopologyType, ILobbyCreatedListener lobbyCreatedListener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_CreateLobby__SWIG_1(swigCPtr, (int)lobbyType, maxMembers, joinable, (int)lobbyTopologyType, ILobbyCreatedListener.getCPtr(lobbyCreatedListener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void CreateLobby(LobbyType lobbyType, uint maxMembers, bool joinable, LobbyTopologyType lobbyTopologyType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_CreateLobby__SWIG_2(swigCPtr, (int)lobbyType, maxMembers, joinable, (int)lobbyTopologyType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestLobbyList(bool allowFullLobbies, ILobbyListListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyList__SWIG_0(swigCPtr, allowFullLobbies, ILobbyListListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestLobbyList(bool allowFullLobbies)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyList__SWIG_1(swigCPtr, allowFullLobbies);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestLobbyList()
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyList__SWIG_2(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void AddRequestLobbyListResultCountFilter(uint limit)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListResultCountFilter(swigCPtr, limit);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void AddRequestLobbyListStringFilter(string keyToMatch, string valueToMatch, LobbyComparisonType comparisonType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListStringFilter(swigCPtr, keyToMatch, valueToMatch, (int)comparisonType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void AddRequestLobbyListNumericalFilter(string keyToMatch, int valueToMatch, LobbyComparisonType comparisonType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListNumericalFilter(swigCPtr, keyToMatch, valueToMatch, (int)comparisonType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void AddRequestLobbyListNearValueFilter(string keyToMatch, int valueToBeCloseTo)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListNearValueFilter(swigCPtr, keyToMatch, valueToBeCloseTo);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual GalaxyID GetLobbyByIndex(uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyByIndex(swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID result = null;
			if (intPtr != IntPtr.Zero)
			{
				result = new GalaxyID(intPtr, cMemoryOwn: true);
			}
			return result;
		}
	
		public virtual void JoinLobby(GalaxyID lobbyID, ILobbyEnteredListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_JoinLobby__SWIG_0(swigCPtr, GalaxyID.getCPtr(lobbyID), ILobbyEnteredListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void JoinLobby(GalaxyID lobbyID)
		{
			GalaxyInstancePINVOKE.IMatchmaking_JoinLobby__SWIG_1(swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void LeaveLobby(GalaxyID lobbyID, ILobbyLeftListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_LeaveLobby__SWIG_0(swigCPtr, GalaxyID.getCPtr(lobbyID), ILobbyLeftListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void LeaveLobby(GalaxyID lobbyID)
		{
			GalaxyInstancePINVOKE.IMatchmaking_LeaveLobby__SWIG_1(swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetMaxNumLobbyMembers(GalaxyID lobbyID, uint maxNumLobbyMembers, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetMaxNumLobbyMembers__SWIG_0(swigCPtr, GalaxyID.getCPtr(lobbyID), maxNumLobbyMembers, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetMaxNumLobbyMembers(GalaxyID lobbyID, uint maxNumLobbyMembers)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetMaxNumLobbyMembers__SWIG_1(swigCPtr, GalaxyID.getCPtr(lobbyID), maxNumLobbyMembers);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint GetMaxNumLobbyMembers(GalaxyID lobbyID)
		{
			uint result = GalaxyInstancePINVOKE.IMatchmaking_GetMaxNumLobbyMembers(swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint GetNumLobbyMembers(GalaxyID lobbyID)
		{
			uint result = GalaxyInstancePINVOKE.IMatchmaking_GetNumLobbyMembers(swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual GalaxyID GetLobbyMemberByIndex(GalaxyID lobbyID, uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberByIndex(swigCPtr, GalaxyID.getCPtr(lobbyID), index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID result = null;
			if (intPtr != IntPtr.Zero)
			{
				result = new GalaxyID(intPtr, cMemoryOwn: true);
			}
			return result;
		}
	
		public virtual void SetLobbyType(GalaxyID lobbyID, LobbyType lobbyType, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyType__SWIG_0(swigCPtr, GalaxyID.getCPtr(lobbyID), (int)lobbyType, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetLobbyType(GalaxyID lobbyID, LobbyType lobbyType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyType__SWIG_1(swigCPtr, GalaxyID.getCPtr(lobbyID), (int)lobbyType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual LobbyType GetLobbyType(GalaxyID lobbyID)
		{
			LobbyType result = (LobbyType)GalaxyInstancePINVOKE.IMatchmaking_GetLobbyType(swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void SetLobbyJoinable(GalaxyID lobbyID, bool joinable, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyJoinable__SWIG_0(swigCPtr, GalaxyID.getCPtr(lobbyID), joinable, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetLobbyJoinable(GalaxyID lobbyID, bool joinable)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyJoinable__SWIG_1(swigCPtr, GalaxyID.getCPtr(lobbyID), joinable);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual bool IsLobbyJoinable(GalaxyID lobbyID)
		{
			bool result = GalaxyInstancePINVOKE.IMatchmaking_IsLobbyJoinable(swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void RequestLobbyData(GalaxyID lobbyID, ILobbyDataRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyData__SWIG_0(swigCPtr, GalaxyID.getCPtr(lobbyID), ILobbyDataRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestLobbyData(GalaxyID lobbyID)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyData__SWIG_1(swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual string GetLobbyData(GalaxyID lobbyID, string key)
		{
			string result = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyData(swigCPtr, GalaxyID.getCPtr(lobbyID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetLobbyDataCopy(GalaxyID lobbyID, string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IMatchmaking_GetLobbyDataCopy(swigCPtr, GalaxyID.getCPtr(lobbyID), key, array, bufferLength);
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
	
		public virtual void SetLobbyData(GalaxyID lobbyID, string key, string value, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyData__SWIG_0(swigCPtr, GalaxyID.getCPtr(lobbyID), key, value, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetLobbyData(GalaxyID lobbyID, string key, string value)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyData__SWIG_1(swigCPtr, GalaxyID.getCPtr(lobbyID), key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint GetLobbyDataCount(GalaxyID lobbyID)
		{
			uint result = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyDataCount(swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool GetLobbyDataByIndex(GalaxyID lobbyID, uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			bool result = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyDataByIndex(swigCPtr, GalaxyID.getCPtr(lobbyID), index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void DeleteLobbyData(GalaxyID lobbyID, string key, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyData__SWIG_0(swigCPtr, GalaxyID.getCPtr(lobbyID), key, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void DeleteLobbyData(GalaxyID lobbyID, string key)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyData__SWIG_1(swigCPtr, GalaxyID.getCPtr(lobbyID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual string GetLobbyMemberData(GalaxyID lobbyID, GalaxyID memberID, string key)
		{
			string result = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberData(swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetLobbyMemberDataCopy(GalaxyID lobbyID, GalaxyID memberID, string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberDataCopy(swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID), key, array, bufferLength);
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
	
		public virtual void SetLobbyMemberData(GalaxyID lobbyID, string key, string value, ILobbyMemberDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyMemberData__SWIG_0(swigCPtr, GalaxyID.getCPtr(lobbyID), key, value, ILobbyMemberDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetLobbyMemberData(GalaxyID lobbyID, string key, string value)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyMemberData__SWIG_1(swigCPtr, GalaxyID.getCPtr(lobbyID), key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint GetLobbyMemberDataCount(GalaxyID lobbyID, GalaxyID memberID)
		{
			uint result = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberDataCount(swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool GetLobbyMemberDataByIndex(GalaxyID lobbyID, GalaxyID memberID, uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			bool result = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberDataByIndex(swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID), index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void DeleteLobbyMemberData(GalaxyID lobbyID, string key, ILobbyMemberDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyMemberData__SWIG_0(swigCPtr, GalaxyID.getCPtr(lobbyID), key, ILobbyMemberDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void DeleteLobbyMemberData(GalaxyID lobbyID, string key)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyMemberData__SWIG_1(swigCPtr, GalaxyID.getCPtr(lobbyID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual GalaxyID GetLobbyOwner(GalaxyID lobbyID)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyOwner(swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID result = null;
			if (intPtr != IntPtr.Zero)
			{
				result = new GalaxyID(intPtr, cMemoryOwn: true);
			}
			return result;
		}
	
		public virtual bool SendLobbyMessage(GalaxyID lobbyID, byte[] data, uint dataSize)
		{
			bool result = GalaxyInstancePINVOKE.IMatchmaking_SendLobbyMessage(swigCPtr, GalaxyID.getCPtr(lobbyID), data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint GetLobbyMessage(GalaxyID lobbyID, uint messageID, ref GalaxyID senderID, ref byte[] msg, uint msgLength)
		{
			uint result = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMessage(swigCPtr, GalaxyID.getCPtr(lobbyID), messageID, GalaxyID.getCPtr(senderID), msg, msgLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}