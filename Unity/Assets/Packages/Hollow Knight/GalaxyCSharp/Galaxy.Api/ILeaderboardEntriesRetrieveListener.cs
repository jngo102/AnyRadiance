using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILeaderboardEntriesRetrieveListener : GalaxyTypeAwareListenerLeaderboardEntriesRetrieve
	{
		public delegate void SwigDelegateILeaderboardEntriesRetrieveListener_0(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name, uint entryCount);
	
		public delegate void SwigDelegateILeaderboardEntriesRetrieveListener_1(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_NOT_FOUND,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, ILeaderboardEntriesRetrieveListener> listeners = new Dictionary<IntPtr, ILeaderboardEntriesRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILeaderboardEntriesRetrieveListener_0 swigDelegate0;
	
		private SwigDelegateILeaderboardEntriesRetrieveListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(string),
			typeof(uint)
		};
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(string),
			typeof(FailureReason)
		};
	
		internal ILeaderboardEntriesRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardEntriesRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILeaderboardEntriesRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardEntriesRetrieveListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILeaderboardEntriesRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILeaderboardEntriesRetrieveListener()
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
						GalaxyInstancePINVOKE.delete_ILeaderboardEntriesRetrieveListener(swigCPtr);
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
	
		public abstract void OnLeaderboardEntriesRetrieveSuccess(string name, uint entryCount);
	
		public abstract void OnLeaderboardEntriesRetrieveFailure(string name, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLeaderboardEntriesRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLeaderboardEntriesRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnLeaderboardEntriesRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnLeaderboardEntriesRetrieveFailure;
			}
			GalaxyInstancePINVOKE.ILeaderboardEntriesRetrieveListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardEntriesRetrieveListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILeaderboardEntriesRetrieveListener_0))]
		private static void SwigDirectorOnLeaderboardEntriesRetrieveSuccess(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name, uint entryCount)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLeaderboardEntriesRetrieveSuccess(name, entryCount);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILeaderboardEntriesRetrieveListener_1))]
		private static void SwigDirectorOnLeaderboardEntriesRetrieveFailure(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLeaderboardEntriesRetrieveFailure(name, (FailureReason)failureReason);
			}
		}
	}
}