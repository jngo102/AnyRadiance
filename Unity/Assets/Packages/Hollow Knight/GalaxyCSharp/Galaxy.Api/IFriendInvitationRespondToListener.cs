using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IFriendInvitationRespondToListener : GalaxyTypeAwareListenerFriendInvitationRespondTo
	{
		public delegate void SwigDelegateIFriendInvitationRespondToListener_0(IntPtr cPtr, IntPtr userID, bool accept);
	
		public delegate void SwigDelegateIFriendInvitationRespondToListener_1(IntPtr cPtr, IntPtr userID, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_USER_DOES_NOT_EXIST,
			FAILURE_REASON_FRIEND_INVITATION_DOES_NOT_EXIST,
			FAILURE_REASON_USER_ALREADY_FRIEND,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IFriendInvitationRespondToListener> listeners = new Dictionary<IntPtr, IFriendInvitationRespondToListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIFriendInvitationRespondToListener_0 swigDelegate0;
	
		private SwigDelegateIFriendInvitationRespondToListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(bool)
		};
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(FailureReason)
		};
	
		internal IFriendInvitationRespondToListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationRespondToListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IFriendInvitationRespondToListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationRespondToListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IFriendInvitationRespondToListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IFriendInvitationRespondToListener()
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
						GalaxyInstancePINVOKE.delete_IFriendInvitationRespondToListener(swigCPtr);
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
	
		public abstract void OnFriendInvitationRespondToSuccess(GalaxyID userID, bool accept);
	
		public abstract void OnFriendInvitationRespondToFailure(GalaxyID userID, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnFriendInvitationRespondToSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnFriendInvitationRespondToSuccess;
			}
			if (SwigDerivedClassHasMethod("OnFriendInvitationRespondToFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnFriendInvitationRespondToFailure;
			}
			GalaxyInstancePINVOKE.IFriendInvitationRespondToListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationRespondToListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFriendInvitationRespondToListener_0))]
		private static void SwigDirectorOnFriendInvitationRespondToSuccess(IntPtr cPtr, IntPtr userID, bool accept)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFriendInvitationRespondToSuccess(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), accept);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFriendInvitationRespondToListener_1))]
		private static void SwigDirectorOnFriendInvitationRespondToFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFriendInvitationRespondToFailure(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), (FailureReason)failureReason);
			}
		}
	}
}