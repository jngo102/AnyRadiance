using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GlobalGogServicesConnectionStateListener : IGogServicesConnectionStateListener
	{
		private HandleRef swigCPtr;
	
		internal GlobalGogServicesConnectionStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalGogServicesConnectionStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GlobalGogServicesConnectionStateListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerGogServicesConnectionState.GetListenerType(), this);
			}
		}
	
		internal static HandleRef getCPtr(GlobalGogServicesConnectionStateListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GlobalGogServicesConnectionStateListener()
		{
			Dispose();
		}
	
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerGogServicesConnectionState.GetListenerType(), this);
			}
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalGogServicesConnectionStateListener(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	}
}