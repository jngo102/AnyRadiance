using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IChatRoomMessageSendListener : GalaxyTypeAwareListenerChatRoomMessageSend
	{
		public delegate void SwigDelegateIChatRoomMessageSendListener_0(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, ulong messageID, uint sendTime);
	
		public delegate void SwigDelegateIChatRoomMessageSendListener_1(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_FORBIDDEN,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IChatRoomMessageSendListener> listeners = new Dictionary<IntPtr, IChatRoomMessageSendListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIChatRoomMessageSendListener_0 swigDelegate0;
	
		private SwigDelegateIChatRoomMessageSendListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[4]
		{
			typeof(ulong),
			typeof(uint),
			typeof(ulong),
			typeof(uint)
		};
	
		private static Type[] swigMethodTypes1 = new Type[3]
		{
			typeof(ulong),
			typeof(uint),
			typeof(FailureReason)
		};
	
		internal IChatRoomMessageSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomMessageSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IChatRoomMessageSendListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomMessageSendListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IChatRoomMessageSendListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IChatRoomMessageSendListener()
		{
			Dispose();
		}
	
		public override void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IChatRoomMessageSendListener(swigCPtr);
					}
					IntPtr handle = swigCPtr.Handle;
					if (listeners.ContainsKey(handle))
					{
						listeners.Remove(handle);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	
		public abstract void OnChatRoomMessageSendSuccess(ulong chatRoomID, uint sentMessageIndex, ulong messageID, uint sendTime);
	
		public abstract void OnChatRoomMessageSendFailure(ulong chatRoomID, uint sentMessageIndex, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnChatRoomMessageSendSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnChatRoomMessageSendSuccess;
			}
			if (SwigDerivedClassHasMethod("OnChatRoomMessageSendFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnChatRoomMessageSendFailure;
			}
			GalaxyInstancePINVOKE.IChatRoomMessageSendListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomMessageSendListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIChatRoomMessageSendListener_0))]
		private static void SwigDirectorOnChatRoomMessageSendSuccess(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, ulong messageID, uint sendTime)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnChatRoomMessageSendSuccess(chatRoomID, sentMessageIndex, messageID, sendTime);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIChatRoomMessageSendListener_1))]
		private static void SwigDirectorOnChatRoomMessageSendFailure(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnChatRoomMessageSendFailure(chatRoomID, sentMessageIndex, (FailureReason)failureReason);
			}
		}
	}
}