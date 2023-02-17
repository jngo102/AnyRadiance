using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GlobalSentFriendInvitationListRetrieveListener : ISentFriendInvitationListRetrieveListener
	{
		private static Dictionary<IntPtr, GlobalSentFriendInvitationListRetrieveListener> listeners = new Dictionary<IntPtr, GlobalSentFriendInvitationListRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		internal GlobalSentFriendInvitationListRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalSentFriendInvitationListRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GlobalSentFriendInvitationListRetrieveListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerSentFriendInvitationListRetrieve.GetListenerType(), this);
			}
		}
	
		internal static HandleRef getCPtr(GlobalSentFriendInvitationListRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GlobalSentFriendInvitationListRetrieveListener()
		{
			Dispose();
		}
	
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerSentFriendInvitationListRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalSentFriendInvitationListRetrieveListener(swigCPtr);
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