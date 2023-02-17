using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GalaxyTypeAwareListenerUserFind : IGalaxyListener
	{
		private HandleRef swigCPtr;
	
		internal GalaxyTypeAwareListenerUserFind(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserFind_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GalaxyTypeAwareListenerUserFind()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerUserFind(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerUserFind obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GalaxyTypeAwareListenerUserFind()
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
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerUserFind(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	
		public static ListenerType GetListenerType()
		{
			ListenerType result = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerUserFind_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}