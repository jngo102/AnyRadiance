using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILeaderboardScoreUpdateListener : GalaxyTypeAwareListenerLeaderboardScoreUpdate
	{
		public delegate void SwigDelegateILeaderboardScoreUpdateListener_0(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name, int score, uint oldRank, uint newRank);
	
		public delegate void SwigDelegateILeaderboardScoreUpdateListener_1(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name, int score, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_NO_IMPROVEMENT,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, ILeaderboardScoreUpdateListener> listeners = new Dictionary<IntPtr, ILeaderboardScoreUpdateListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILeaderboardScoreUpdateListener_0 swigDelegate0;
	
		private SwigDelegateILeaderboardScoreUpdateListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[4]
		{
			typeof(string),
			typeof(int),
			typeof(uint),
			typeof(uint)
		};
	
		private static Type[] swigMethodTypes1 = new Type[3]
		{
			typeof(string),
			typeof(int),
			typeof(FailureReason)
		};
	
		internal ILeaderboardScoreUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILeaderboardScoreUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILeaderboardScoreUpdateListener()
			: this(GalaxyInstancePINVOKE.new_ILeaderboardScoreUpdateListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILeaderboardScoreUpdateListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILeaderboardScoreUpdateListener()
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
						GalaxyInstancePINVOKE.delete_ILeaderboardScoreUpdateListener(swigCPtr);
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
	
		public abstract void OnLeaderboardScoreUpdateSuccess(string name, int score, uint oldRank, uint newRank);
	
		public abstract void OnLeaderboardScoreUpdateFailure(string name, int score, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLeaderboardScoreUpdateSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLeaderboardScoreUpdateSuccess;
			}
			if (SwigDerivedClassHasMethod("OnLeaderboardScoreUpdateFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnLeaderboardScoreUpdateFailure;
			}
			GalaxyInstancePINVOKE.ILeaderboardScoreUpdateListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILeaderboardScoreUpdateListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILeaderboardScoreUpdateListener_0))]
		private static void SwigDirectorOnLeaderboardScoreUpdateSuccess(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name, int score, uint oldRank, uint newRank)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLeaderboardScoreUpdateSuccess(name, score, oldRank, newRank);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILeaderboardScoreUpdateListener_1))]
		private static void SwigDirectorOnLeaderboardScoreUpdateFailure(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name, int score, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLeaderboardScoreUpdateFailure(name, score, (FailureReason)failureReason);
			}
		}
	}
}