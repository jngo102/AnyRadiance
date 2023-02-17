using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IOverlayInitializationStateChangeListener : GalaxyTypeAwareListenerOverlayInitializationStateChange
	{
		public delegate void SwigDelegateIOverlayInitializationStateChangeListener_0(IntPtr cPtr, int overlayState);
	
		private static Dictionary<IntPtr, IOverlayInitializationStateChangeListener> listeners = new Dictionary<IntPtr, IOverlayInitializationStateChangeListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIOverlayInitializationStateChangeListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(OverlayState) };
	
		internal IOverlayInitializationStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOverlayInitializationStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IOverlayInitializationStateChangeListener()
			: this(GalaxyInstancePINVOKE.new_IOverlayInitializationStateChangeListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IOverlayInitializationStateChangeListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IOverlayInitializationStateChangeListener()
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
						GalaxyInstancePINVOKE.delete_IOverlayInitializationStateChangeListener(swigCPtr);
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
	
		public abstract void OnOverlayStateChanged(OverlayState overlayState);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnOverlayStateChanged", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnOverlayStateChanged;
			}
			GalaxyInstancePINVOKE.IOverlayInitializationStateChangeListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOverlayInitializationStateChangeListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIOverlayInitializationStateChangeListener_0))]
		private static void SwigDirectorOnOverlayStateChanged(IntPtr cPtr, int overlayState)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnOverlayStateChanged((OverlayState)overlayState);
			}
		}
	}
}