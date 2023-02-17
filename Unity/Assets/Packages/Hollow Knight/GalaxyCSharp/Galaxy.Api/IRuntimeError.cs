using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public class IRuntimeError : IError
	{
		private HandleRef swigCPtr;
	
		internal IRuntimeError(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRuntimeError_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(IRuntimeError obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IRuntimeError()
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
						GalaxyInstancePINVOKE.delete_IRuntimeError(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	}
}