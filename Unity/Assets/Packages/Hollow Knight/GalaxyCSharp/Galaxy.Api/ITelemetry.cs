using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	
	public class ITelemetry : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		internal ITelemetry(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(ITelemetry obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ITelemetry()
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
						GalaxyInstancePINVOKE.delete_ITelemetry(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public virtual void AddStringParam(string name, string value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddStringParam(swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void AddIntParam(string name, int value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddIntParam(swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void AddFloatParam(string name, double value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddFloatParam(swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void AddBoolParam(string name, bool value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddBoolParam(swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void AddObjectParam(string name)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddObjectParam(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void AddArrayParam(string name)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddArrayParam(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void CloseParam()
		{
			GalaxyInstancePINVOKE.ITelemetry_CloseParam(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void ClearParams()
		{
			GalaxyInstancePINVOKE.ITelemetry_ClearParams(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetSamplingClass(string name)
		{
			GalaxyInstancePINVOKE.ITelemetry_SetSamplingClass(swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint SendTelemetryEvent(string eventType, ITelemetryEventSendListener listener)
		{
			uint result = GalaxyInstancePINVOKE.ITelemetry_SendTelemetryEvent__SWIG_0(swigCPtr, eventType, ITelemetryEventSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint SendTelemetryEvent(string eventType)
		{
			uint result = GalaxyInstancePINVOKE.ITelemetry_SendTelemetryEvent__SWIG_1(swigCPtr, eventType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint SendAnonymousTelemetryEvent(string eventType, ITelemetryEventSendListener listener)
		{
			uint result = GalaxyInstancePINVOKE.ITelemetry_SendAnonymousTelemetryEvent__SWIG_0(swigCPtr, eventType, ITelemetryEventSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint SendAnonymousTelemetryEvent(string eventType)
		{
			uint result = GalaxyInstancePINVOKE.ITelemetry_SendAnonymousTelemetryEvent__SWIG_1(swigCPtr, eventType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual string GetVisitID()
		{
			string result = GalaxyInstancePINVOKE.ITelemetry_GetVisitID(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetVisitIDCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.ITelemetry_GetVisitIDCopy(swigCPtr, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}
	
		public virtual void ResetVisitID()
		{
			GalaxyInstancePINVOKE.ITelemetry_ResetVisitID(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	}
}