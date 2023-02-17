using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GameServerGlobalLobbyMemberDataUpdateListener : ILobbyMemberDataUpdateListener
	{
		private HandleRef swigCPtr;
	
		internal GameServerGlobalLobbyMemberDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyMemberDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GameServerGlobalLobbyMemberDataUpdateListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyMemberDataUpdate.GetListenerType(), this);
			}
		}
	
		internal static HandleRef getCPtr(GameServerGlobalLobbyMemberDataUpdateListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GameServerGlobalLobbyMemberDataUpdateListener()
		{
			Dispose();
		}
	
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyMemberDataUpdate.GetListenerType(), this);
			}
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyMemberDataUpdateListener(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	}
}