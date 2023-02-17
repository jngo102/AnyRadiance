using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ISentFriendInvitationListRetrieveListener : GalaxyTypeAwareListenerSentFriendInvitationListRetrieve
	{
		public delegate void SwigDelegateISentFriendInvitationListRetrieveListener_0(IntPtr cPtr);
	
		public delegate void SwigDelegateISentFriendInvitationListRetrieveListener_1(IntPtr cPtr, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, ISentFriendInvitationListRetrieveListener> listeners = new Dictionary<IntPtr, ISentFriendInvitationListRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateISentFriendInvitationListRetrieveListener_0 swigDelegate0;
	
		private SwigDelegateISentFriendInvitationListRetrieveListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[0];
	
		private static Type[] swigMethodTypes1 = new Type[1] { typeof(FailureReason) };
	
		internal ISentFriendInvitationListRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISentFriendInvitationListRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ISentFriendInvitationListRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ISentFriendInvitationListRetrieveListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ISentFriendInvitationListRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ISentFriendInvitationListRetrieveListener()
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
						GalaxyInstancePINVOKE.delete_ISentFriendInvitationListRetrieveListener(swigCPtr);
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
	
		public abstract void OnSentFriendInvitationListRetrieveSuccess();
	
		public abstract void OnSentFriendInvitationListRetrieveFailure(FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnSentFriendInvitationListRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnSentFriendInvitationListRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnSentFriendInvitationListRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnSentFriendInvitationListRetrieveFailure;
			}
			GalaxyInstancePINVOKE.ISentFriendInvitationListRetrieveListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISentFriendInvitationListRetrieveListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateISentFriendInvitationListRetrieveListener_0))]
		private static void SwigDirectorOnSentFriendInvitationListRetrieveSuccess(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnSentFriendInvitationListRetrieveSuccess();
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateISentFriendInvitationListRetrieveListener_1))]
		private static void SwigDirectorOnSentFriendInvitationListRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnSentFriendInvitationListRetrieveFailure((FailureReason)failureReason);
			}
		}
	}
}