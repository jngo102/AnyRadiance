using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IFriendListListener : GalaxyTypeAwareListenerFriendList
	{
		public delegate void SwigDelegateIFriendListListener_0(IntPtr cPtr);
	
		public delegate void SwigDelegateIFriendListListener_1(IntPtr cPtr, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IFriendListListener> listeners = new Dictionary<IntPtr, IFriendListListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIFriendListListener_0 swigDelegate0;
	
		private SwigDelegateIFriendListListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[0];
	
		private static Type[] swigMethodTypes1 = new Type[1] { typeof(FailureReason) };
	
		internal IFriendListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IFriendListListener()
			: this(GalaxyInstancePINVOKE.new_IFriendListListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IFriendListListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IFriendListListener()
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
						GalaxyInstancePINVOKE.delete_IFriendListListener(swigCPtr);
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
	
		public abstract void OnFriendListRetrieveSuccess();
	
		public abstract void OnFriendListRetrieveFailure(FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnFriendListRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnFriendListRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnFriendListRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnFriendListRetrieveFailure;
			}
			GalaxyInstancePINVOKE.IFriendListListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendListListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFriendListListener_0))]
		private static void SwigDirectorOnFriendListRetrieveSuccess(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFriendListRetrieveSuccess();
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFriendListListener_1))]
		private static void SwigDirectorOnFriendListRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFriendListRetrieveFailure((FailureReason)failureReason);
			}
		}
	}
}