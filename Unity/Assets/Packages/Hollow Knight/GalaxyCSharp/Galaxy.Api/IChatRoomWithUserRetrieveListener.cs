using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IChatRoomWithUserRetrieveListener : GalaxyTypeAwareListenerChatRoomWithUserRetrieve
	{
		public delegate void SwigDelegateIChatRoomWithUserRetrieveListener_0(IntPtr cPtr, IntPtr userID, ulong chatRoomID);
	
		public delegate void SwigDelegateIChatRoomWithUserRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_FORBIDDEN,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IChatRoomWithUserRetrieveListener> listeners = new Dictionary<IntPtr, IChatRoomWithUserRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIChatRoomWithUserRetrieveListener_0 swigDelegate0;
	
		private SwigDelegateIChatRoomWithUserRetrieveListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(ulong)
		};
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(FailureReason)
		};
	
		internal IChatRoomWithUserRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomWithUserRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IChatRoomWithUserRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomWithUserRetrieveListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IChatRoomWithUserRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IChatRoomWithUserRetrieveListener()
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
						GalaxyInstancePINVOKE.delete_IChatRoomWithUserRetrieveListener(swigCPtr);
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
	
		public abstract void OnChatRoomWithUserRetrieveSuccess(GalaxyID userID, ulong chatRoomID);
	
		public abstract void OnChatRoomWithUserRetrieveFailure(GalaxyID userID, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnChatRoomWithUserRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnChatRoomWithUserRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnChatRoomWithUserRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnChatRoomWithUserRetrieveFailure;
			}
			GalaxyInstancePINVOKE.IChatRoomWithUserRetrieveListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomWithUserRetrieveListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIChatRoomWithUserRetrieveListener_0))]
		private static void SwigDirectorOnChatRoomWithUserRetrieveSuccess(IntPtr cPtr, IntPtr userID, ulong chatRoomID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnChatRoomWithUserRetrieveSuccess(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), chatRoomID);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIChatRoomWithUserRetrieveListener_1))]
		private static void SwigDirectorOnChatRoomWithUserRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnChatRoomWithUserRetrieveFailure(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), (FailureReason)failureReason);
			}
		}
	}
}