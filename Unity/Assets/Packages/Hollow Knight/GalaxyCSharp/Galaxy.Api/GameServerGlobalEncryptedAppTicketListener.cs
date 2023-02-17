using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GameServerGlobalEncryptedAppTicketListener : IEncryptedAppTicketListener
	{
		private HandleRef swigCPtr;
	
		internal GameServerGlobalEncryptedAppTicketListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalEncryptedAppTicketListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GameServerGlobalEncryptedAppTicketListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerEncryptedAppTicket.GetListenerType(), this);
			}
		}
	
		internal static HandleRef getCPtr(GameServerGlobalEncryptedAppTicketListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GameServerGlobalEncryptedAppTicketListener()
		{
			Dispose();
		}
	
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerEncryptedAppTicket.GetListenerType(), this);
			}
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalEncryptedAppTicketListener(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	}
}