using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IGogServicesConnectionStateListener : GalaxyTypeAwareListenerGogServicesConnectionState
	{
		public delegate void SwigDelegateIGogServicesConnectionStateListener_0(IntPtr cPtr, int connectionState);
	
		private static Dictionary<IntPtr, IGogServicesConnectionStateListener> listeners = new Dictionary<IntPtr, IGogServicesConnectionStateListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIGogServicesConnectionStateListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(GogServicesConnectionState) };
	
		internal IGogServicesConnectionStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IGogServicesConnectionStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IGogServicesConnectionStateListener()
			: this(GalaxyInstancePINVOKE.new_IGogServicesConnectionStateListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IGogServicesConnectionStateListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IGogServicesConnectionStateListener()
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
						GalaxyInstancePINVOKE.delete_IGogServicesConnectionStateListener(swigCPtr);
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
	
		public abstract void OnConnectionStateChange(GogServicesConnectionState connectionState);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnConnectionStateChange", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnConnectionStateChange;
			}
			GalaxyInstancePINVOKE.IGogServicesConnectionStateListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IGogServicesConnectionStateListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIGogServicesConnectionStateListener_0))]
		private static void SwigDirectorOnConnectionStateChange(IntPtr cPtr, int connectionState)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnConnectionStateChange((GogServicesConnectionState)connectionState);
			}
		}
	}
}