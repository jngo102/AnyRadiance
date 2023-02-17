using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IUserInformationRetrieveListener : GalaxyTypeAwareListenerUserInformationRetrieve
	{
		public delegate void SwigDelegateIUserInformationRetrieveListener_0(IntPtr cPtr, IntPtr userID);
	
		public delegate void SwigDelegateIUserInformationRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IUserInformationRetrieveListener> listeners = new Dictionary<IntPtr, IUserInformationRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIUserInformationRetrieveListener_0 swigDelegate0;
	
		private SwigDelegateIUserInformationRetrieveListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(GalaxyID) };
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(FailureReason)
		};
	
		internal IUserInformationRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserInformationRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IUserInformationRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IUserInformationRetrieveListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IUserInformationRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IUserInformationRetrieveListener()
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
						GalaxyInstancePINVOKE.delete_IUserInformationRetrieveListener(swigCPtr);
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
	
		public abstract void OnUserInformationRetrieveSuccess(GalaxyID userID);
	
		public abstract void OnUserInformationRetrieveFailure(GalaxyID userID, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnUserInformationRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnUserInformationRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnUserInformationRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnUserInformationRetrieveFailure;
			}
			GalaxyInstancePINVOKE.IUserInformationRetrieveListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserInformationRetrieveListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIUserInformationRetrieveListener_0))]
		private static void SwigDirectorOnUserInformationRetrieveSuccess(IntPtr cPtr, IntPtr userID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnUserInformationRetrieveSuccess(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()));
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIUserInformationRetrieveListener_1))]
		private static void SwigDirectorOnUserInformationRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnUserInformationRetrieveFailure(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), (FailureReason)failureReason);
			}
		}
	}
}