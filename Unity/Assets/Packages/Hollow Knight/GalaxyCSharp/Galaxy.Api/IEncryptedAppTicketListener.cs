using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IEncryptedAppTicketListener : GalaxyTypeAwareListenerEncryptedAppTicket
	{
		public delegate void SwigDelegateIEncryptedAppTicketListener_0(IntPtr cPtr);
	
		public delegate void SwigDelegateIEncryptedAppTicketListener_1(IntPtr cPtr, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IEncryptedAppTicketListener> listeners = new Dictionary<IntPtr, IEncryptedAppTicketListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIEncryptedAppTicketListener_0 swigDelegate0;
	
		private SwigDelegateIEncryptedAppTicketListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[0];
	
		private static Type[] swigMethodTypes1 = new Type[1] { typeof(FailureReason) };
	
		internal IEncryptedAppTicketListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IEncryptedAppTicketListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IEncryptedAppTicketListener()
			: this(GalaxyInstancePINVOKE.new_IEncryptedAppTicketListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IEncryptedAppTicketListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IEncryptedAppTicketListener()
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
						GalaxyInstancePINVOKE.delete_IEncryptedAppTicketListener(swigCPtr);
					}
					IntPtr handle = swigCPtr.Handle;
					if (listeners.ContainsKey(handle))
					{
						listeners.Remove(handle);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	
		public abstract void OnEncryptedAppTicketRetrieveSuccess();
	
		public virtual void OnEncryptedAppTicketRetrieveFailure(FailureReason failureReason)
		{
			GalaxyInstancePINVOKE.IEncryptedAppTicketListener_OnEncryptedAppTicketRetrieveFailure(swigCPtr, (int)failureReason);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnEncryptedAppTicketRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnEncryptedAppTicketRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnEncryptedAppTicketRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnEncryptedAppTicketRetrieveFailure;
			}
			GalaxyInstancePINVOKE.IEncryptedAppTicketListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IEncryptedAppTicketListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIEncryptedAppTicketListener_0))]
		private static void SwigDirectorOnEncryptedAppTicketRetrieveSuccess(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnEncryptedAppTicketRetrieveSuccess();
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIEncryptedAppTicketListener_1))]
		private static void SwigDirectorOnEncryptedAppTicketRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnEncryptedAppTicketRetrieveFailure((FailureReason)failureReason);
			}
		}
	}
}