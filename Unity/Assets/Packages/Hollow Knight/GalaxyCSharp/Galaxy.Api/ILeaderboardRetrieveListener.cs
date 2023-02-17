using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILeaderboardRetrieveListener : GalaxyTypeAwareListenerLeaderboardRetrieve
	{
		public delegate void SwigDelegateILeaderboardRetrieveListener_0(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name);
	
		public delegate void SwigDelegateILeaderboardRetrieveListener_1(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, ILeaderboardRetrieveListener> listeners = new Dictionary<IntPtr, ILeaderboardRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILeaderboardRetrieveListener_0 swigDelegate0;
	
		private SwigDelegateILeaderboardRetrieveListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(string) };
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(string),
			typeof(FailureReason)
		};
	
		internal ILeaderboardRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILeaderboardRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardRetrieveListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILeaderboardRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILeaderboardRetrieveListener()
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
						GalaxyInstancePINVOKE.delete_ILeaderboardRetrieveListener(swigCPtr);
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
	
		public abstract void OnLeaderboardRetrieveSuccess(string name);
	
		public abstract void OnLeaderboardRetrieveFailure(string name, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLeaderboardRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLeaderboardRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnLeaderboardRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnLeaderboardRetrieveFailure;
			}
			GalaxyInstancePINVOKE.ILeaderboardRetrieveListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardRetrieveListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILeaderboardRetrieveListener_0))]
		private static void SwigDirectorOnLeaderboardRetrieveSuccess(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLeaderboardRetrieveSuccess(name);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILeaderboardRetrieveListener_1))]
		private static void SwigDirectorOnLeaderboardRetrieveFailure(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLeaderboardRetrieveFailure(name, (FailureReason)failureReason);
			}
		}
	}
}