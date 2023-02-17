using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	
	public class IFriends : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		internal IFriends(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(IFriends obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IFriends()
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
						GalaxyInstancePINVOKE.delete_IFriends(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public virtual uint GetDefaultAvatarCriteria()
		{
			uint result = GalaxyInstancePINVOKE.IFriends_GetDefaultAvatarCriteria(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void SetDefaultAvatarCriteria(uint defaultAvatarCriteria)
		{
			GalaxyInstancePINVOKE.IFriends_SetDefaultAvatarCriteria(swigCPtr, defaultAvatarCriteria);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestUserInformation(GalaxyID userID, uint avatarCriteria, IUserInformationRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestUserInformation__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID), avatarCriteria, IUserInformationRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestUserInformation(GalaxyID userID, uint avatarCriteria)
		{
			GalaxyInstancePINVOKE.IFriends_RequestUserInformation__SWIG_1(swigCPtr, GalaxyID.getCPtr(userID), avatarCriteria);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestUserInformation(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_RequestUserInformation__SWIG_2(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual bool IsUserInformationAvailable(GalaxyID userID)
		{
			bool result = GalaxyInstancePINVOKE.IFriends_IsUserInformationAvailable(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual string GetPersonaName()
		{
			string result = GalaxyInstancePINVOKE.IFriends_GetPersonaName(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetPersonaNameCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetPersonaNameCopy(swigCPtr, array, bufferLength);
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
	
		public virtual PersonaState GetPersonaState()
		{
			PersonaState result = (PersonaState)GalaxyInstancePINVOKE.IFriends_GetPersonaState(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual string GetFriendPersonaName(GalaxyID userID)
		{
			string result = GalaxyInstancePINVOKE.IFriends_GetFriendPersonaName(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetFriendPersonaNameCopy(GalaxyID userID, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetFriendPersonaNameCopy(swigCPtr, GalaxyID.getCPtr(userID), array, bufferLength);
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
	
		public virtual PersonaState GetFriendPersonaState(GalaxyID userID)
		{
			PersonaState result = (PersonaState)GalaxyInstancePINVOKE.IFriends_GetFriendPersonaState(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual string GetFriendAvatarUrl(GalaxyID userID, AvatarType avatarType)
		{
			string result = GalaxyInstancePINVOKE.IFriends_GetFriendAvatarUrl(swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetFriendAvatarUrlCopy(GalaxyID userID, AvatarType avatarType, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetFriendAvatarUrlCopy(swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType, array, bufferLength);
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
	
		public virtual uint GetFriendAvatarImageID(GalaxyID userID, AvatarType avatarType)
		{
			uint result = GalaxyInstancePINVOKE.IFriends_GetFriendAvatarImageID(swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetFriendAvatarImageRGBA(GalaxyID userID, AvatarType avatarType, byte[] buffer, uint bufferLength)
		{
			GalaxyInstancePINVOKE.IFriends_GetFriendAvatarImageRGBA(swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType, buffer, bufferLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual bool IsFriendAvatarImageRGBAAvailable(GalaxyID userID, AvatarType avatarType)
		{
			bool result = GalaxyInstancePINVOKE.IFriends_IsFriendAvatarImageRGBAAvailable(swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void RequestFriendList(IFriendListListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendList__SWIG_0(swigCPtr, IFriendListListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestFriendList()
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendList__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual bool IsFriend(GalaxyID userID)
		{
			bool result = GalaxyInstancePINVOKE.IFriends_IsFriend(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint GetFriendCount()
		{
			uint result = GalaxyInstancePINVOKE.IFriends_GetFriendCount(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual GalaxyID GetFriendByIndex(uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IFriends_GetFriendByIndex(swigCPtr, index);
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
	
		public virtual void SendFriendInvitation(GalaxyID userID, IFriendInvitationSendListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_SendFriendInvitation__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID), IFriendInvitationSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SendFriendInvitation(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_SendFriendInvitation__SWIG_1(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestFriendInvitationList(IFriendInvitationListRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendInvitationList__SWIG_0(swigCPtr, IFriendInvitationListRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestFriendInvitationList()
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendInvitationList__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestSentFriendInvitationList(ISentFriendInvitationListRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestSentFriendInvitationList__SWIG_0(swigCPtr, ISentFriendInvitationListRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestSentFriendInvitationList()
		{
			GalaxyInstancePINVOKE.IFriends_RequestSentFriendInvitationList__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint GetFriendInvitationCount()
		{
			uint result = GalaxyInstancePINVOKE.IFriends_GetFriendInvitationCount(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetFriendInvitationByIndex(uint index, ref GalaxyID userID, ref uint sendTime)
		{
			GalaxyInstancePINVOKE.IFriends_GetFriendInvitationByIndex(swigCPtr, index, GalaxyID.getCPtr(userID), ref sendTime);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RespondToFriendInvitation(GalaxyID userID, bool accept, IFriendInvitationRespondToListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RespondToFriendInvitation__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID), accept, IFriendInvitationRespondToListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RespondToFriendInvitation(GalaxyID userID, bool accept)
		{
			GalaxyInstancePINVOKE.IFriends_RespondToFriendInvitation__SWIG_1(swigCPtr, GalaxyID.getCPtr(userID), accept);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void DeleteFriend(GalaxyID userID, IFriendDeleteListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteFriend__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID), IFriendDeleteListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void DeleteFriend(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteFriend__SWIG_1(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetRichPresence(string key, string value, IRichPresenceChangeListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_SetRichPresence__SWIG_0(swigCPtr, key, value, IRichPresenceChangeListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetRichPresence(string key, string value)
		{
			GalaxyInstancePINVOKE.IFriends_SetRichPresence__SWIG_1(swigCPtr, key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void DeleteRichPresence(string key, IRichPresenceChangeListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteRichPresence__SWIG_0(swigCPtr, key, IRichPresenceChangeListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void DeleteRichPresence(string key)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteRichPresence__SWIG_1(swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void ClearRichPresence(IRichPresenceChangeListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_ClearRichPresence__SWIG_0(swigCPtr, IRichPresenceChangeListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void ClearRichPresence()
		{
			GalaxyInstancePINVOKE.IFriends_ClearRichPresence__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestRichPresence(GalaxyID userID, IRichPresenceRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestRichPresence__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID), IRichPresenceRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestRichPresence(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_RequestRichPresence__SWIG_1(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestRichPresence()
		{
			GalaxyInstancePINVOKE.IFriends_RequestRichPresence__SWIG_2(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual string GetRichPresence(string key, GalaxyID userID)
		{
			string result = GalaxyInstancePINVOKE.IFriends_GetRichPresence__SWIG_0(swigCPtr, key, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual string GetRichPresence(string key)
		{
			string result = GalaxyInstancePINVOKE.IFriends_GetRichPresence__SWIG_1(swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetRichPresenceCopy(string key, out string buffer, uint bufferLength, GalaxyID userID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceCopy__SWIG_0(swigCPtr, key, array, bufferLength, GalaxyID.getCPtr(userID));
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
	
		public virtual void GetRichPresenceCopy(string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceCopy__SWIG_1(swigCPtr, key, array, bufferLength);
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
	
		public virtual uint GetRichPresenceCount(GalaxyID userID)
		{
			uint result = GalaxyInstancePINVOKE.IFriends_GetRichPresenceCount__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint GetRichPresenceCount()
		{
			uint result = GalaxyInstancePINVOKE.IFriends_GetRichPresenceCount__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetRichPresenceByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength, GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_GetRichPresenceByIndex__SWIG_0(swigCPtr, index, key, keyLength, value, valueLength, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void GetRichPresenceByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			GalaxyInstancePINVOKE.IFriends_GetRichPresenceByIndex__SWIG_1(swigCPtr, index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual string GetRichPresenceKeyByIndex(uint index, GalaxyID userID)
		{
			string result = GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndex__SWIG_0(swigCPtr, index, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual string GetRichPresenceKeyByIndex(uint index)
		{
			string result = GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndex__SWIG_1(swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetRichPresenceKeyByIndexCopy(uint index, out string buffer, uint bufferLength, GalaxyID userID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndexCopy__SWIG_0(swigCPtr, index, array, bufferLength, GalaxyID.getCPtr(userID));
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
	
		public virtual void GetRichPresenceKeyByIndexCopy(uint index, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndexCopy__SWIG_1(swigCPtr, index, array, bufferLength);
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
	
		public virtual void ShowOverlayInviteDialog(string connectionString)
		{
			GalaxyInstancePINVOKE.IFriends_ShowOverlayInviteDialog(swigCPtr, connectionString);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SendInvitation(GalaxyID userID, string connectionString, ISendInvitationListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_SendInvitation__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID), connectionString, ISendInvitationListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SendInvitation(GalaxyID userID, string connectionString)
		{
			GalaxyInstancePINVOKE.IFriends_SendInvitation__SWIG_1(swigCPtr, GalaxyID.getCPtr(userID), connectionString);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void FindUser(string userSpecifier, IUserFindListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_FindUser__SWIG_0(swigCPtr, userSpecifier, IUserFindListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void FindUser(string userSpecifier)
		{
			GalaxyInstancePINVOKE.IFriends_FindUser__SWIG_1(swigCPtr, userSpecifier);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual bool IsUserInTheSameGame(GalaxyID userID)
		{
			bool result = GalaxyInstancePINVOKE.IFriends_IsUserInTheSameGame(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}