using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GameServerGlobalLobbyDataRetrieveListener : ILobbyDataRetrieveListener
	{
		private HandleRef swigCPtr;
	
		internal GameServerGlobalLobbyDataRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalLobbyDataRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GameServerGlobalLobbyDataRetrieveListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerLobbyDataRetrieve.GetListenerType(), this);
			}
		}
	
		internal static HandleRef getCPtr(GameServerGlobalLobbyDataRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GameServerGlobalLobbyDataRetrieveListener()
		{
			Dispose();
		}
	
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerLobbyDataRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalLobbyDataRetrieveListener(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	}
}