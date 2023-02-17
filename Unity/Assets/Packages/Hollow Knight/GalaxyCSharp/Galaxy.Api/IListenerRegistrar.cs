using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public class IListenerRegistrar : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		internal IListenerRegistrar(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(IListenerRegistrar obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IListenerRegistrar()
		{
			Dispose();
		}
	
		public virtual void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IListenerRegistrar(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public virtual void Register(ListenerType listenerType, IGalaxyListener listener)
		{
			GalaxyInstancePINVOKE.IListenerRegistrar_Register(swigCPtr, (int)listenerType, IGalaxyListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void Unregister(ListenerType listenerType, IGalaxyListener listener)
		{
			GalaxyInstancePINVOKE.IListenerRegistrar_Unregister(swigCPtr, (int)listenerType, IGalaxyListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	}
}