using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	
	public class IChat : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		internal IChat(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(IChat obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IChat()
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
						GalaxyInstancePINVOKE.delete_IChat(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public virtual void RequestChatRoomWithUser(GalaxyID userID, IChatRoomWithUserRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomWithUser__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID), IChatRoomWithUserRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestChatRoomWithUser(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomWithUser__SWIG_1(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestChatRoomMessages(ulong chatRoomID, uint limit, ulong referenceMessageID, IChatRoomMessagesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomMessages__SWIG_0(swigCPtr, chatRoomID, limit, referenceMessageID, IChatRoomMessagesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestChatRoomMessages(ulong chatRoomID, uint limit, ulong referenceMessageID)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomMessages__SWIG_1(swigCPtr, chatRoomID, limit, referenceMessageID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestChatRoomMessages(ulong chatRoomID, uint limit)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomMessages__SWIG_2(swigCPtr, chatRoomID, limit);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint SendChatRoomMessage(ulong chatRoomID, string msg, IChatRoomMessageSendListener listener)
		{
			uint result = GalaxyInstancePINVOKE.IChat_SendChatRoomMessage__SWIG_0(swigCPtr, chatRoomID, msg, IChatRoomMessageSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint SendChatRoomMessage(ulong chatRoomID, string msg)
		{
			uint result = GalaxyInstancePINVOKE.IChat_SendChatRoomMessage__SWIG_1(swigCPtr, chatRoomID, msg);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint GetChatRoomMessageByIndex(uint index, ref ulong messageID, ref ChatMessageType messageType, ref GalaxyID senderID, ref uint sendTime, out string buffer, uint bufferLength)
		{
			int jarg = 0;
			byte[] array = new byte[bufferLength];
			try
			{
				uint result = GalaxyInstancePINVOKE.IChat_GetChatRoomMessageByIndex(swigCPtr, index, ref messageID, ref jarg, GalaxyID.getCPtr(senderID), ref sendTime, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return result;
			}
			finally
			{
				messageType = (ChatMessageType)jarg;
				buffer = Encoding.UTF8.GetString(array);
			}
		}
	
		public virtual uint GetChatRoomMemberCount(ulong chatRoomID)
		{
			uint result = GalaxyInstancePINVOKE.IChat_GetChatRoomMemberCount(swigCPtr, chatRoomID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual GalaxyID GetChatRoomMemberUserIDByIndex(ulong chatRoomID, uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IChat_GetChatRoomMemberUserIDByIndex(swigCPtr, chatRoomID, index);
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
	
		public virtual uint GetChatRoomUnreadMessageCount(ulong chatRoomID)
		{
			uint result = GalaxyInstancePINVOKE.IChat_GetChatRoomUnreadMessageCount(swigCPtr, chatRoomID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void MarkChatRoomAsRead(ulong chatRoomID)
		{
			GalaxyInstancePINVOKE.IChat_MarkChatRoomAsRead(swigCPtr, chatRoomID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	}
}