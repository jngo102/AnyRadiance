using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GalaxyTypeAwareListenerFriendList : IGalaxyListener
	{
		private HandleRef swigCPtr;
	
		internal GalaxyTypeAwareListenerFriendList(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendList_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GalaxyTypeAwareListenerFriendList()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerFriendList(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerFriendList obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GalaxyTypeAwareListenerFriendList()
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
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerFriendList(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	
		public static ListenerType GetListenerType()
		{
			ListenerType result = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerFriendList_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}