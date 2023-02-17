using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IFriendInvitationListRetrieveListener : GalaxyTypeAwareListenerFriendInvitationListRetrieve
	{
		public delegate void SwigDelegateIFriendInvitationListRetrieveListener_0(IntPtr cPtr);
	
		public delegate void SwigDelegateIFriendInvitationListRetrieveListener_1(IntPtr cPtr, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IFriendInvitationListRetrieveListener> listeners = new Dictionary<IntPtr, IFriendInvitationListRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIFriendInvitationListRetrieveListener_0 swigDelegate0;
	
		private SwigDelegateIFriendInvitationListRetrieveListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[0];
	
		private static Type[] swigMethodTypes1 = new Type[1] { typeof(FailureReason) };
	
		internal IFriendInvitationListRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationListRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IFriendInvitationListRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationListRetrieveListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IFriendInvitationListRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IFriendInvitationListRetrieveListener()
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
						GalaxyInstancePINVOKE.delete_IFriendInvitationListRetrieveListener(swigCPtr);
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
	
		public abstract void OnFriendInvitationListRetrieveSuccess();
	
		public abstract void OnFriendInvitationListRetrieveFailure(FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnFriendInvitationListRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnFriendInvitationListRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnFriendInvitationListRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnFriendInvitationListRetrieveFailure;
			}
			GalaxyInstancePINVOKE.IFriendInvitationListRetrieveListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationListRetrieveListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFriendInvitationListRetrieveListener_0))]
		private static void SwigDirectorOnFriendInvitationListRetrieveSuccess(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFriendInvitationListRetrieveSuccess();
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFriendInvitationListRetrieveListener_1))]
		private static void SwigDirectorOnFriendInvitationListRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFriendInvitationListRetrieveFailure((FailureReason)failureReason);
			}
		}
	}
}