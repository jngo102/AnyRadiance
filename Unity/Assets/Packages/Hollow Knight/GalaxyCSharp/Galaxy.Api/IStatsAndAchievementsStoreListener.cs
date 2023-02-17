using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IStatsAndAchievementsStoreListener : GalaxyTypeAwareListenerStatsAndAchievementsStore
	{
		public delegate void SwigDelegateIStatsAndAchievementsStoreListener_0(IntPtr cPtr);
	
		public delegate void SwigDelegateIStatsAndAchievementsStoreListener_1(IntPtr cPtr, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IStatsAndAchievementsStoreListener> listeners = new Dictionary<IntPtr, IStatsAndAchievementsStoreListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIStatsAndAchievementsStoreListener_0 swigDelegate0;
	
		private SwigDelegateIStatsAndAchievementsStoreListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[0];
	
		private static Type[] swigMethodTypes1 = new Type[1] { typeof(FailureReason) };
	
		internal IStatsAndAchievementsStoreListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IStatsAndAchievementsStoreListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IStatsAndAchievementsStoreListener()
			: this(GalaxyInstancePINVOKE.new_IStatsAndAchievementsStoreListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IStatsAndAchievementsStoreListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IStatsAndAchievementsStoreListener()
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
						GalaxyInstancePINVOKE.delete_IStatsAndAchievementsStoreListener(swigCPtr);
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
	
		public abstract void OnUserStatsAndAchievementsStoreSuccess();
	
		public abstract void OnUserStatsAndAchievementsStoreFailure(FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnUserStatsAndAchievementsStoreSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnUserStatsAndAchievementsStoreSuccess;
			}
			if (SwigDerivedClassHasMethod("OnUserStatsAndAchievementsStoreFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnUserStatsAndAchievementsStoreFailure;
			}
			GalaxyInstancePINVOKE.IStatsAndAchievementsStoreListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IStatsAndAchievementsStoreListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIStatsAndAchievementsStoreListener_0))]
		private static void SwigDirectorOnUserStatsAndAchievementsStoreSuccess(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnUserStatsAndAchievementsStoreSuccess();
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIStatsAndAchievementsStoreListener_1))]
		private static void SwigDirectorOnUserStatsAndAchievementsStoreFailure(IntPtr cPtr, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnUserStatsAndAchievementsStoreFailure((FailureReason)failureReason);
			}
		}
	}
}