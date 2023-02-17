using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GalaxyTypeAwareListenerFriendInvitationListRetrieve : IGalaxyListener
	{
		private HandleRef swigCPtr;
	
		internal GalaxyTypeAwareListenerFriendInvitationListRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitationListRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GalaxyTypeAwareListenerFriendInvitationListRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerFriendInvitationListRetrieve(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerFriendInvitationListRetrieve obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GalaxyTypeAwareListenerFriendInvitationListRetrieve()
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
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerFriendInvitationListRetrieve(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	
		public static ListenerType GetListenerType()
		{
			ListenerType result = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendInvitationListRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}