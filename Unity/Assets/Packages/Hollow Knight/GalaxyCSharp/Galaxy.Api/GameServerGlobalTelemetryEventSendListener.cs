using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GameServerGlobalTelemetryEventSendListener : ITelemetryEventSendListener
	{
		private HandleRef swigCPtr;
	
		internal GameServerGlobalTelemetryEventSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalTelemetryEventSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GameServerGlobalTelemetryEventSendListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerTelemetryEventSend.GetListenerType(), this);
			}
		}
	
		internal static HandleRef getCPtr(GameServerGlobalTelemetryEventSendListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GameServerGlobalTelemetryEventSendListener()
		{
			Dispose();
		}
	
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerTelemetryEventSend.GetListenerType(), this);
			}
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalTelemetryEventSendListener(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	}
}