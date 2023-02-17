using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GlobalOverlayInitializationStateChangeListener : IOverlayInitializationStateChangeListener
	{
		private static Dictionary<IntPtr, GlobalOverlayInitializationStateChangeListener> listeners = new Dictionary<IntPtr, GlobalOverlayInitializationStateChangeListener>();
	
		private HandleRef swigCPtr;
	
		internal GlobalOverlayInitializationStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalOverlayInitializationStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GlobalOverlayInitializationStateChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerOverlayInitializationStateChange.GetListenerType(), this);
			}
		}
	
		internal static HandleRef getCPtr(GlobalOverlayInitializationStateChangeListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GlobalOverlayInitializationStateChangeListener()
		{
			Dispose();
		}
	
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerOverlayInitializationStateChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalOverlayInitializationStateChangeListener(swigCPtr);
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
	}
}