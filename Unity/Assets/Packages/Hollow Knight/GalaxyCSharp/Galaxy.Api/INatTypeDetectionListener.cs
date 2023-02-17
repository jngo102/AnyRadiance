using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class INatTypeDetectionListener : GalaxyTypeAwareListenerNatTypeDetection
	{
		public delegate void SwigDelegateINatTypeDetectionListener_0(IntPtr cPtr, int natType);
	
		public delegate void SwigDelegateINatTypeDetectionListener_1(IntPtr cPtr);
	
		private static Dictionary<IntPtr, INatTypeDetectionListener> listeners = new Dictionary<IntPtr, INatTypeDetectionListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateINatTypeDetectionListener_0 swigDelegate0;
	
		private SwigDelegateINatTypeDetectionListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(NatType) };
	
		private static Type[] swigMethodTypes1 = new Type[0];
	
		internal INatTypeDetectionListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.INatTypeDetectionListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public INatTypeDetectionListener()
			: this(GalaxyInstancePINVOKE.new_INatTypeDetectionListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(INatTypeDetectionListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~INatTypeDetectionListener()
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
						GalaxyInstancePINVOKE.delete_INatTypeDetectionListener(swigCPtr);
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
	
		public abstract void OnNatTypeDetectionSuccess(NatType natType);
	
		public abstract void OnNatTypeDetectionFailure();
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnNatTypeDetectionSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnNatTypeDetectionSuccess;
			}
			if (SwigDerivedClassHasMethod("OnNatTypeDetectionFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnNatTypeDetectionFailure;
			}
			GalaxyInstancePINVOKE.INatTypeDetectionListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(INatTypeDetectionListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateINatTypeDetectionListener_0))]
		private static void SwigDirectorOnNatTypeDetectionSuccess(IntPtr cPtr, int natType)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnNatTypeDetectionSuccess((NatType)natType);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateINatTypeDetectionListener_1))]
		private static void SwigDirectorOnNatTypeDetectionFailure(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnNatTypeDetectionFailure();
			}
		}
	}
}