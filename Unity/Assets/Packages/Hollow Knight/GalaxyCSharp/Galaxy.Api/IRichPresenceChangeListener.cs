using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IRichPresenceChangeListener : GalaxyTypeAwareListenerRichPresenceChange
	{
		public delegate void SwigDelegateIRichPresenceChangeListener_0(IntPtr cPtr);
	
		public delegate void SwigDelegateIRichPresenceChangeListener_1(IntPtr cPtr, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IRichPresenceChangeListener> listeners = new Dictionary<IntPtr, IRichPresenceChangeListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIRichPresenceChangeListener_0 swigDelegate0;
	
		private SwigDelegateIRichPresenceChangeListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[0];
	
		private static Type[] swigMethodTypes1 = new Type[1] { typeof(FailureReason) };
	
		internal IRichPresenceChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRichPresenceChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IRichPresenceChangeListener()
			: this(GalaxyInstancePINVOKE.new_IRichPresenceChangeListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IRichPresenceChangeListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IRichPresenceChangeListener()
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
						GalaxyInstancePINVOKE.delete_IRichPresenceChangeListener(swigCPtr);
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
	
		public abstract void OnRichPresenceChangeSuccess();
	
		public abstract void OnRichPresenceChangeFailure(FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnRichPresenceChangeSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnRichPresenceChangeSuccess;
			}
			if (SwigDerivedClassHasMethod("OnRichPresenceChangeFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnRichPresenceChangeFailure;
			}
			GalaxyInstancePINVOKE.IRichPresenceChangeListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IRichPresenceChangeListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIRichPresenceChangeListener_0))]
		private static void SwigDirectorOnRichPresenceChangeSuccess(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnRichPresenceChangeSuccess();
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIRichPresenceChangeListener_1))]
		private static void SwigDirectorOnRichPresenceChangeFailure(IntPtr cPtr, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnRichPresenceChangeFailure((FailureReason)failureReason);
			}
		}
	}
}