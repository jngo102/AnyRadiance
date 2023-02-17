using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILeaderboardsRetrieveListener : GalaxyTypeAwareListenerLeaderboardsRetrieve
	{
		public delegate void SwigDelegateILeaderboardsRetrieveListener_0(IntPtr cPtr);
	
		public delegate void SwigDelegateILeaderboardsRetrieveListener_1(IntPtr cPtr, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, ILeaderboardsRetrieveListener> listeners = new Dictionary<IntPtr, ILeaderboardsRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILeaderboardsRetrieveListener_0 swigDelegate0;
	
		private SwigDelegateILeaderboardsRetrieveListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[0];
	
		private static Type[] swigMethodTypes1 = new Type[1] { typeof(FailureReason) };
	
		internal ILeaderboardsRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardsRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILeaderboardsRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardsRetrieveListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILeaderboardsRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILeaderboardsRetrieveListener()
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
						GalaxyInstancePINVOKE.delete_ILeaderboardsRetrieveListener(swigCPtr);
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
	
		public abstract void OnLeaderboardsRetrieveSuccess();
	
		public abstract void OnLeaderboardsRetrieveFailure(FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLeaderboardsRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLeaderboardsRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnLeaderboardsRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnLeaderboardsRetrieveFailure;
			}
			GalaxyInstancePINVOKE.ILeaderboardsRetrieveListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardsRetrieveListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILeaderboardsRetrieveListener_0))]
		private static void SwigDirectorOnLeaderboardsRetrieveSuccess(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLeaderboardsRetrieveSuccess();
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILeaderboardsRetrieveListener_1))]
		private static void SwigDirectorOnLeaderboardsRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLeaderboardsRetrieveFailure((FailureReason)failureReason);
			}
		}
	}
}