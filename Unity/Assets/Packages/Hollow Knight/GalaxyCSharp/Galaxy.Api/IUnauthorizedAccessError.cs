using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public class IUnauthorizedAccessError : IError
	{
		private HandleRef swigCPtr;
	
		internal IUnauthorizedAccessError(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUnauthorizedAccessError_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(IUnauthorizedAccessError obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IUnauthorizedAccessError()
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
						GalaxyInstancePINVOKE.delete_IUnauthorizedAccessError(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	}
}