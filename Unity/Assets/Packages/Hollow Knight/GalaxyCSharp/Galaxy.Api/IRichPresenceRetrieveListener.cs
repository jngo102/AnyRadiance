using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IRichPresenceRetrieveListener : GalaxyTypeAwareListenerRichPresenceRetrieve
	{
		public delegate void SwigDelegateIRichPresenceRetrieveListener_0(IntPtr cPtr, IntPtr userID);
	
		public delegate void SwigDelegateIRichPresenceRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IRichPresenceRetrieveListener> listeners = new Dictionary<IntPtr, IRichPresenceRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIRichPresenceRetrieveListener_0 swigDelegate0;
	
		private SwigDelegateIRichPresenceRetrieveListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(GalaxyID) };
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(FailureReason)
		};
	
		internal IRichPresenceRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IRichPresenceRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IRichPresenceRetrieveListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IRichPresenceRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IRichPresenceRetrieveListener()
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
						GalaxyInstancePINVOKE.delete_IRichPresenceRetrieveListener(swigCPtr);
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
	
		public virtual void OnRichPresenceRetrieveSuccess(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_OnRichPresenceRetrieveSuccess(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void OnRichPresenceRetrieveFailure(GalaxyID userID, FailureReason failureReason)
		{
			GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_OnRichPresenceRetrieveFailure(swigCPtr, GalaxyID.getCPtr(userID), (int)failureReason);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnRichPresenceRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnRichPresenceRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnRichPresenceRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnRichPresenceRetrieveFailure;
			}
			GalaxyInstancePINVOKE.IRichPresenceRetrieveListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IRichPresenceRetrieveListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIRichPresenceRetrieveListener_0))]
		private static void SwigDirectorOnRichPresenceRetrieveSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnRichPresenceRetrieveSuccess(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()));
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIRichPresenceRetrieveListener_1))]
		private static void SwigDirectorOnRichPresenceRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnRichPresenceRetrieveFailure(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), (FailureReason)failureReason);
			}
		}
	}
}