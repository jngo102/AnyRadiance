using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IUserFindListener : GalaxyTypeAwareListenerUserFind
	{
		public delegate void SwigDelegateIUserFindListener_0(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string userSpecifier, IntPtr userID);
	
		public delegate void SwigDelegateIUserFindListener_1(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string userSpecifier, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_USER_NOT_FOUND,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IUserFindListener> listeners = new Dictionary<IntPtr, IUserFindListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIUserFindListener_0 swigDelegate0;
	
		private SwigDelegateIUserFindListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(string),
			typeof(GalaxyID)
		};
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(string),
			typeof(FailureReason)
		};
	
		internal IUserFindListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserFindListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IUserFindListener()
			: this(GalaxyInstancePINVOKE.new_IUserFindListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IUserFindListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IUserFindListener()
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
						GalaxyInstancePINVOKE.delete_IUserFindListener(swigCPtr);
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
	
		public abstract void OnUserFindSuccess(string userSpecifier, GalaxyID userID);
	
		public abstract void OnUserFindFailure(string userSpecifier, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnUserFindSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnUserFindSuccess;
			}
			if (SwigDerivedClassHasMethod("OnUserFindFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnUserFindFailure;
			}
			GalaxyInstancePINVOKE.IUserFindListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserFindListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIUserFindListener_0))]
		private static void SwigDirectorOnUserFindSuccess(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string userSpecifier, IntPtr userID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnUserFindSuccess(userSpecifier, new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()));
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIUserFindListener_1))]
		private static void SwigDirectorOnUserFindFailure(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string userSpecifier, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnUserFindFailure(userSpecifier, (FailureReason)failureReason);
			}
		}
	}
}