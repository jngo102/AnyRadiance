using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IChatRoomMessagesRetrieveListener : GalaxyTypeAwareListenerChatRoomMessagesRetrieve
	{
		public delegate void SwigDelegateIChatRoomMessagesRetrieveListener_0(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght);
	
		public delegate void SwigDelegateIChatRoomMessagesRetrieveListener_1(IntPtr cPtr, ulong chatRoomID, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_FORBIDDEN,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IChatRoomMessagesRetrieveListener> listeners = new Dictionary<IntPtr, IChatRoomMessagesRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIChatRoomMessagesRetrieveListener_0 swigDelegate0;
	
		private SwigDelegateIChatRoomMessagesRetrieveListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[3]
		{
			typeof(ulong),
			typeof(uint),
			typeof(uint)
		};
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(ulong),
			typeof(FailureReason)
		};
	
		internal IChatRoomMessagesRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomMessagesRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IChatRoomMessagesRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomMessagesRetrieveListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IChatRoomMessagesRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IChatRoomMessagesRetrieveListener()
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
						GalaxyInstancePINVOKE.delete_IChatRoomMessagesRetrieveListener(swigCPtr);
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
	
		public abstract void OnChatRoomMessagesRetrieveSuccess(ulong chatRoomID, uint messageCount, uint longestMessageLenght);
	
		public abstract void OnChatRoomMessagesRetrieveFailure(ulong chatRoomID, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnChatRoomMessagesRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnChatRoomMessagesRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnChatRoomMessagesRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnChatRoomMessagesRetrieveFailure;
			}
			GalaxyInstancePINVOKE.IChatRoomMessagesRetrieveListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomMessagesRetrieveListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIChatRoomMessagesRetrieveListener_0))]
		private static void SwigDirectorOnChatRoomMessagesRetrieveSuccess(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnChatRoomMessagesRetrieveSuccess(chatRoomID, messageCount, longestMessageLenght);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIChatRoomMessagesRetrieveListener_1))]
		private static void SwigDirectorOnChatRoomMessagesRetrieveFailure(IntPtr cPtr, ulong chatRoomID, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnChatRoomMessagesRetrieveFailure(chatRoomID, (FailureReason)failureReason);
			}
		}
	}
}