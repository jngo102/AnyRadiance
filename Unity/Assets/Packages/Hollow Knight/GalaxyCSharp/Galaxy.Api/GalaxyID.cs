using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public class GalaxyID : IDisposable
	{
		public enum IDType
		{
			ID_TYPE_UNASSIGNED,
			ID_TYPE_LOBBY,
			ID_TYPE_USER
		}
	
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		public static readonly ulong UNASSIGNED_VALUE = GalaxyInstancePINVOKE.GalaxyID_UNASSIGNED_VALUE_get();
	
		internal GalaxyID(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GalaxyID()
			: this(GalaxyInstancePINVOKE.new_GalaxyID__SWIG_0(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public GalaxyID(ulong _value)
			: this(GalaxyInstancePINVOKE.new_GalaxyID__SWIG_1(_value), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public GalaxyID(GalaxyID galaxyID)
			: this(GalaxyInstancePINVOKE.new_GalaxyID__SWIG_2(getCPtr(galaxyID)), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		internal static HandleRef getCPtr(GalaxyID obj)
		{
			return (!(obj == null)) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}
	
		~GalaxyID()
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
						GalaxyInstancePINVOKE.delete_GalaxyID(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public static bool operator ==(GalaxyID other1, GalaxyID other2)
		{
			if ((object)other1 == null && (object)other2 == null)
			{
				return true;
			}
			if ((object)other1 == null || (object)other2 == null)
			{
				return false;
			}
			return other1.operator_equals(other2);
		}
	
		public static bool operator !=(GalaxyID other1, GalaxyID other2)
		{
			if ((object)other1 == null && (object)other2 == null)
			{
				return false;
			}
			if ((object)other1 == null || (object)other2 == null)
			{
				return true;
			}
			return other1.operator_not_equals(other2);
		}
	
		public static bool operator <(GalaxyID other1, GalaxyID other2)
		{
			return other1.operator_less(other2);
		}
	
		public static bool operator >(GalaxyID other1, GalaxyID other2)
		{
			return !other1.operator_less(other2) && !other1.operator_equals(other2);
		}
	
		public override int GetHashCode()
		{
			return (int)ToUint64();
		}
	
		public override bool Equals(object obj)
		{
			GalaxyID galaxyID = obj as GalaxyID;
			if (galaxyID == null)
			{
				return false;
			}
			return operator_equals(galaxyID);
		}
	
		public override string ToString()
		{
			return ToUint64().ToString();
		}
	
		public static GalaxyID FromRealID(IDType type, ulong value)
		{
			GalaxyID result = new GalaxyID(GalaxyInstancePINVOKE.GalaxyID_FromRealID((int)type, value), cMemoryOwn: true);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		private GalaxyID operator_assign(GalaxyID other)
		{
			GalaxyID result = new GalaxyID(GalaxyInstancePINVOKE.GalaxyID_operator_assign(swigCPtr, getCPtr(other)), cMemoryOwn: false);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		private bool operator_less(GalaxyID other)
		{
			bool result = GalaxyInstancePINVOKE.GalaxyID_operator_less(swigCPtr, getCPtr(other));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		private bool operator_equals(GalaxyID other)
		{
			bool result = GalaxyInstancePINVOKE.GalaxyID_operator_equals(swigCPtr, getCPtr(other));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		private bool operator_not_equals(GalaxyID other)
		{
			bool result = GalaxyInstancePINVOKE.GalaxyID_operator_not_equals(swigCPtr, getCPtr(other));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public bool IsValid()
		{
			bool result = GalaxyInstancePINVOKE.GalaxyID_IsValid(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public ulong ToUint64()
		{
			ulong result = GalaxyInstancePINVOKE.GalaxyID_ToUint64(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public ulong GetRealID()
		{
			ulong result = GalaxyInstancePINVOKE.GalaxyID_GetRealID(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public IDType GetIDType()
		{
			IDType result = (IDType)GalaxyInstancePINVOKE.GalaxyID_GetIDType(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}