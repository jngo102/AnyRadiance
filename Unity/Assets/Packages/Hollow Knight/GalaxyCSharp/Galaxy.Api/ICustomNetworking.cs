using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public class ICustomNetworking : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		internal ICustomNetworking(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(ICustomNetworking obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ICustomNetworking()
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
						GalaxyInstancePINVOKE.delete_ICustomNetworking(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public virtual void OpenConnection(string connectionString, IConnectionOpenListener listener)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_OpenConnection__SWIG_0(swigCPtr, connectionString, IConnectionOpenListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void OpenConnection(string connectionString)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_OpenConnection__SWIG_1(swigCPtr, connectionString);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void CloseConnection(ulong connectionID, IConnectionCloseListener listener)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_CloseConnection__SWIG_0(swigCPtr, connectionID, IConnectionCloseListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void CloseConnection(ulong connectionID)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_CloseConnection__SWIG_1(swigCPtr, connectionID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SendData(ulong connectionID, byte[] data, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_SendData(swigCPtr, connectionID, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint GetAvailableDataSize(ulong connectionID)
		{
			uint result = GalaxyInstancePINVOKE.ICustomNetworking_GetAvailableDataSize(swigCPtr, connectionID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void PeekData(ulong connectionID, byte[] dest, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_PeekData(swigCPtr, connectionID, dest, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void ReadData(ulong connectionID, byte[] dest, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_ReadData(swigCPtr, connectionID, dest, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void PopData(ulong connectionID, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_PopData(swigCPtr, connectionID, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	}
}