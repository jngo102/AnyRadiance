using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public class IUtils : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		internal IUtils(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(IUtils obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IUtils()
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
						GalaxyInstancePINVOKE.delete_IUtils(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public virtual void GetImageSize(uint imageID, ref int width, ref int height)
		{
			GalaxyInstancePINVOKE.IUtils_GetImageSize(swigCPtr, imageID, ref width, ref height);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void GetImageRGBA(uint imageID, byte[] buffer, uint bufferLength)
		{
			GalaxyInstancePINVOKE.IUtils_GetImageRGBA(swigCPtr, imageID, buffer, bufferLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RegisterForNotification(string type)
		{
			GalaxyInstancePINVOKE.IUtils_RegisterForNotification(swigCPtr, type);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint GetNotification(ulong notificationID, ref bool consumable, ref byte[] type, uint typeLength, byte[] content, uint contentSize)
		{
			uint result = GalaxyInstancePINVOKE.IUtils_GetNotification(swigCPtr, notificationID, ref consumable, type, typeLength, content, contentSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void ShowOverlayWithWebPage(string url)
		{
			GalaxyInstancePINVOKE.IUtils_ShowOverlayWithWebPage(swigCPtr, url);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual bool IsOverlayVisible()
		{
			bool result = GalaxyInstancePINVOKE.IUtils_IsOverlayVisible(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual OverlayState GetOverlayState()
		{
			OverlayState result = (OverlayState)GalaxyInstancePINVOKE.IUtils_GetOverlayState(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void DisableOverlayPopups(string popupGroup)
		{
			GalaxyInstancePINVOKE.IUtils_DisableOverlayPopups(swigCPtr, popupGroup);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual GogServicesConnectionState GetGogServicesConnectionState()
		{
			GogServicesConnectionState result = (GogServicesConnectionState)GalaxyInstancePINVOKE.IUtils_GetGogServicesConnectionState(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}