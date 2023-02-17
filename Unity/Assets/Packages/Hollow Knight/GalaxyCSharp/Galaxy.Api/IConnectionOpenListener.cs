using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IConnectionOpenListener : GalaxyTypeAwareListenerConnectionOpen
	{
		public delegate void SwigDelegateIConnectionOpenListener_0(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString, ulong connectionID);
	
		public delegate void SwigDelegateIConnectionOpenListener_1(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE,
			FAILURE_REASON_UNAUTHORIZED
		}
	
		private static Dictionary<IntPtr, IConnectionOpenListener> listeners = new Dictionary<IntPtr, IConnectionOpenListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIConnectionOpenListener_0 swigDelegate0;
	
		private SwigDelegateIConnectionOpenListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(string),
			typeof(ulong)
		};
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(string),
			typeof(FailureReason)
		};
	
		internal IConnectionOpenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IConnectionOpenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IConnectionOpenListener()
			: this(GalaxyInstancePINVOKE.new_IConnectionOpenListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IConnectionOpenListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IConnectionOpenListener()
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
						GalaxyInstancePINVOKE.delete_IConnectionOpenListener(swigCPtr);
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
	
		public abstract void OnConnectionOpenSuccess(string connectionString, ulong connectionID);
	
		public abstract void OnConnectionOpenFailure(string connectionString, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnConnectionOpenSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnConnectionOpenSuccess;
			}
			if (SwigDerivedClassHasMethod("OnConnectionOpenFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnConnectionOpenFailure;
			}
			GalaxyInstancePINVOKE.IConnectionOpenListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IConnectionOpenListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIConnectionOpenListener_0))]
		private static void SwigDirectorOnConnectionOpenSuccess(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString, ulong connectionID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnConnectionOpenSuccess(connectionString, connectionID);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIConnectionOpenListener_1))]
		private static void SwigDirectorOnConnectionOpenFailure(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnConnectionOpenFailure(connectionString, (FailureReason)failureReason);
			}
		}
	}
}