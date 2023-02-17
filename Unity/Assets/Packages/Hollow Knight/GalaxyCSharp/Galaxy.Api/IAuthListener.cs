using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IAuthListener : GalaxyTypeAwareListenerAuth
	{
		public delegate void SwigDelegateIAuthListener_0(IntPtr cPtr);
	
		public delegate void SwigDelegateIAuthListener_1(IntPtr cPtr, int failureReason);
	
		public delegate void SwigDelegateIAuthListener_2(IntPtr cPtr);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_GALAXY_SERVICE_NOT_AVAILABLE,
			FAILURE_REASON_GALAXY_SERVICE_NOT_SIGNED_IN,
			FAILURE_REASON_CONNECTION_FAILURE,
			FAILURE_REASON_NO_LICENSE,
			FAILURE_REASON_INVALID_CREDENTIALS,
			FAILURE_REASON_GALAXY_NOT_INITIALIZED,
			FAILURE_REASON_EXTERNAL_SERVICE_FAILURE
		}
	
		private static Dictionary<IntPtr, IAuthListener> listeners = new Dictionary<IntPtr, IAuthListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIAuthListener_0 swigDelegate0;
	
		private SwigDelegateIAuthListener_1 swigDelegate1;
	
		private SwigDelegateIAuthListener_2 swigDelegate2;
	
		private static Type[] swigMethodTypes0 = new Type[0];
	
		private static Type[] swigMethodTypes1 = new Type[1] { typeof(FailureReason) };
	
		private static Type[] swigMethodTypes2 = new Type[0];
	
		internal IAuthListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IAuthListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IAuthListener()
			: this(GalaxyInstancePINVOKE.new_IAuthListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IAuthListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IAuthListener()
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
						GalaxyInstancePINVOKE.delete_IAuthListener(swigCPtr);
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
	
		public abstract void OnAuthSuccess();
	
		public abstract void OnAuthFailure(FailureReason failureReason);
	
		public abstract void OnAuthLost();
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnAuthSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnAuthSuccess;
			}
			if (SwigDerivedClassHasMethod("OnAuthFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnAuthFailure;
			}
			if (SwigDerivedClassHasMethod("OnAuthLost", swigMethodTypes2))
			{
				swigDelegate2 = SwigDirectorOnAuthLost;
			}
			GalaxyInstancePINVOKE.IAuthListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IAuthListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIAuthListener_0))]
		private static void SwigDirectorOnAuthSuccess(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnAuthSuccess();
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIAuthListener_1))]
		private static void SwigDirectorOnAuthFailure(IntPtr cPtr, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnAuthFailure((FailureReason)failureReason);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIAuthListener_2))]
		private static void SwigDirectorOnAuthLost(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnAuthLost();
			}
		}
	}
}