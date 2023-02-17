using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GlobalFileShareListener : IFileShareListener
	{
		private HandleRef swigCPtr;
	
		internal GlobalFileShareListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalFileShareListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GlobalFileShareListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerFileShare.GetListenerType(), this);
			}
		}
	
		internal static HandleRef getCPtr(GlobalFileShareListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GlobalFileShareListener()
		{
			Dispose();
		}
	
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerFileShare.GetListenerType(), this);
			}
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalFileShareListener(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	}
}