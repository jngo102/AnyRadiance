using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IUserStatsAndAchievementsRetrieveListener : GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve
	{
		public delegate void SwigDelegateIUserStatsAndAchievementsRetrieveListener_0(IntPtr cPtr, IntPtr userID);
	
		public delegate void SwigDelegateIUserStatsAndAchievementsRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IUserStatsAndAchievementsRetrieveListener> listeners = new Dictionary<IntPtr, IUserStatsAndAchievementsRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIUserStatsAndAchievementsRetrieveListener_0 swigDelegate0;
	
		private SwigDelegateIUserStatsAndAchievementsRetrieveListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(GalaxyID) };
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(FailureReason)
		};
	
		internal IUserStatsAndAchievementsRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserStatsAndAchievementsRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IUserStatsAndAchievementsRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IUserStatsAndAchievementsRetrieveListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IUserStatsAndAchievementsRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IUserStatsAndAchievementsRetrieveListener()
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
						GalaxyInstancePINVOKE.delete_IUserStatsAndAchievementsRetrieveListener(swigCPtr);
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
	
		public abstract void OnUserStatsAndAchievementsRetrieveSuccess(GalaxyID userID);
	
		public abstract void OnUserStatsAndAchievementsRetrieveFailure(GalaxyID userID, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnUserStatsAndAchievementsRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnUserStatsAndAchievementsRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnUserStatsAndAchievementsRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnUserStatsAndAchievementsRetrieveFailure;
			}
			GalaxyInstancePINVOKE.IUserStatsAndAchievementsRetrieveListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserStatsAndAchievementsRetrieveListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIUserStatsAndAchievementsRetrieveListener_0))]
		private static void SwigDirectorOnUserStatsAndAchievementsRetrieveSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnUserStatsAndAchievementsRetrieveSuccess(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()));
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIUserStatsAndAchievementsRetrieveListener_1))]
		private static void SwigDirectorOnUserStatsAndAchievementsRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnUserStatsAndAchievementsRetrieveFailure(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), (FailureReason)failureReason);
			}
		}
	}
}