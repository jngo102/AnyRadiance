using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public class INetworking : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		internal INetworking(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(INetworking obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~INetworking()
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
						GalaxyInstancePINVOKE.delete_INetworking(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public virtual bool SendP2PPacket(GalaxyID galaxyID, byte[] data, uint dataSize, P2PSendType sendType, byte channel)
		{
			bool result = GalaxyInstancePINVOKE.INetworking_SendP2PPacket__SWIG_0(swigCPtr, GalaxyID.getCPtr(galaxyID), data, dataSize, (int)sendType, channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool SendP2PPacket(GalaxyID galaxyID, byte[] data, uint dataSize, P2PSendType sendType)
		{
			bool result = GalaxyInstancePINVOKE.INetworking_SendP2PPacket__SWIG_1(swigCPtr, GalaxyID.getCPtr(galaxyID), data, dataSize, (int)sendType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool PeekP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID, byte channel)
		{
			bool result = GalaxyInstancePINVOKE.INetworking_PeekP2PPacket__SWIG_0(swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID), channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool PeekP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID)
		{
			bool result = GalaxyInstancePINVOKE.INetworking_PeekP2PPacket__SWIG_1(swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool IsP2PPacketAvailable(ref uint outMsgSize, byte channel)
		{
			bool result = GalaxyInstancePINVOKE.INetworking_IsP2PPacketAvailable__SWIG_0(swigCPtr, ref outMsgSize, channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool IsP2PPacketAvailable(ref uint outMsgSize)
		{
			bool result = GalaxyInstancePINVOKE.INetworking_IsP2PPacketAvailable__SWIG_1(swigCPtr, ref outMsgSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool ReadP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID, byte channel)
		{
			bool result = GalaxyInstancePINVOKE.INetworking_ReadP2PPacket__SWIG_0(swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID), channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool ReadP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID)
		{
			bool result = GalaxyInstancePINVOKE.INetworking_ReadP2PPacket__SWIG_1(swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void PopP2PPacket(byte channel)
		{
			GalaxyInstancePINVOKE.INetworking_PopP2PPacket__SWIG_0(swigCPtr, channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void PopP2PPacket()
		{
			GalaxyInstancePINVOKE.INetworking_PopP2PPacket__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual int GetPingWith(GalaxyID galaxyID)
		{
			int result = GalaxyInstancePINVOKE.INetworking_GetPingWith(swigCPtr, GalaxyID.getCPtr(galaxyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void RequestNatTypeDetection()
		{
			GalaxyInstancePINVOKE.INetworking_RequestNatTypeDetection(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual NatType GetNatType()
		{
			NatType result = (NatType)GalaxyInstancePINVOKE.INetworking_GetNatType(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual ConnectionType GetConnectionType(GalaxyID userID)
		{
			ConnectionType result = (ConnectionType)GalaxyInstancePINVOKE.INetworking_GetConnectionType(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}