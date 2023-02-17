using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IFriendInvitationSendListener : GalaxyTypeAwareListenerFriendInvitationSend
	{
		public delegate void SwigDelegateIFriendInvitationSendListener_0(IntPtr cPtr, IntPtr userID);
	
		public delegate void SwigDelegateIFriendInvitationSendListener_1(IntPtr cPtr, IntPtr userID, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_USER_DOES_NOT_EXIST,
			FAILURE_REASON_USER_ALREADY_INVITED,
			FAILURE_REASON_USER_ALREADY_FRIEND,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IFriendInvitationSendListener> listeners = new Dictionary<IntPtr, IFriendInvitationSendListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIFriendInvitationSendListener_0 swigDelegate0;
	
		private SwigDelegateIFriendInvitationSendListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(GalaxyID) };
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(FailureReason)
		};
	
		internal IFriendInvitationSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IFriendInvitationSendListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationSendListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IFriendInvitationSendListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IFriendInvitationSendListener()
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
						GalaxyInstancePINVOKE.delete_IFriendInvitationSendListener(swigCPtr);
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
	
		public abstract void OnFriendInvitationSendSuccess(GalaxyID userID);
	
		public abstract void OnFriendInvitationSendFailure(GalaxyID userID, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnFriendInvitationSendSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnFriendInvitationSendSuccess;
			}
			if (SwigDerivedClassHasMethod("OnFriendInvitationSendFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnFriendInvitationSendFailure;
			}
			GalaxyInstancePINVOKE.IFriendInvitationSendListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationSendListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFriendInvitationSendListener_0))]
		private static void SwigDirectorOnFriendInvitationSendSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFriendInvitationSendSuccess(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()));
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFriendInvitationSendListener_1))]
		private static void SwigDirectorOnFriendInvitationSendFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFriendInvitationSendFailure(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), (FailureReason)failureReason);
			}
		}
	}
}