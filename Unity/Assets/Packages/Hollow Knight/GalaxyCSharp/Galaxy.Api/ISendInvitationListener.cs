using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ISendInvitationListener : GalaxyTypeAwareListenerSendInvitation
	{
		public delegate void SwigDelegateISendInvitationListener_0(IntPtr cPtr, IntPtr userID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString);
	
		public delegate void SwigDelegateISendInvitationListener_1(IntPtr cPtr, IntPtr userID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_USER_DOES_NOT_EXIST,
			FAILURE_REASON_RECEIVER_DOES_NOT_ALLOW_INVITING,
			FAILURE_REASON_SENDER_DOES_NOT_ALLOW_INVITING,
			FAILURE_REASON_RECEIVER_BLOCKED,
			FAILURE_REASON_SENDER_BLOCKED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, ISendInvitationListener> listeners = new Dictionary<IntPtr, ISendInvitationListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateISendInvitationListener_0 swigDelegate0;
	
		private SwigDelegateISendInvitationListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(string)
		};
	
		private static Type[] swigMethodTypes1 = new Type[3]
		{
			typeof(GalaxyID),
			typeof(string),
			typeof(FailureReason)
		};
	
		internal ISendInvitationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISendInvitationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ISendInvitationListener()
			: this(GalaxyInstancePINVOKE.new_ISendInvitationListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ISendInvitationListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ISendInvitationListener()
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
						GalaxyInstancePINVOKE.delete_ISendInvitationListener(swigCPtr);
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
	
		public abstract void OnInvitationSendSuccess(GalaxyID userID, string connectionString);
	
		public abstract void OnInvitationSendFailure(GalaxyID userID, string connectionString, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnInvitationSendSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnInvitationSendSuccess;
			}
			if (SwigDerivedClassHasMethod("OnInvitationSendFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnInvitationSendFailure;
			}
			GalaxyInstancePINVOKE.ISendInvitationListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISendInvitationListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateISendInvitationListener_0))]
		private static void SwigDirectorOnInvitationSendSuccess(IntPtr cPtr, IntPtr userID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnInvitationSendSuccess(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), connectionString);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateISendInvitationListener_1))]
		private static void SwigDirectorOnInvitationSendFailure(IntPtr cPtr, IntPtr userID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnInvitationSendFailure(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), connectionString, (FailureReason)failureReason);
			}
		}
	}
}