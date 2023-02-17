using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IFileShareListener : GalaxyTypeAwareListenerFileShare
	{
		public delegate void SwigDelegateIFileShareListener_0(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string fileName, ulong sharedFileID);
	
		public delegate void SwigDelegateIFileShareListener_1(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string fileName, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, IFileShareListener> listeners = new Dictionary<IntPtr, IFileShareListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIFileShareListener_0 swigDelegate0;
	
		private SwigDelegateIFileShareListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(string),
			typeof(ulong)
		};
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(string),
			typeof(FailureReason)
		};
	
		internal IFileShareListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFileShareListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IFileShareListener()
			: this(GalaxyInstancePINVOKE.new_IFileShareListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IFileShareListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IFileShareListener()
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
						GalaxyInstancePINVOKE.delete_IFileShareListener(swigCPtr);
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
	
		public abstract void OnFileShareSuccess(string fileName, ulong sharedFileID);
	
		public abstract void OnFileShareFailure(string fileName, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnFileShareSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnFileShareSuccess;
			}
			if (SwigDerivedClassHasMethod("OnFileShareFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnFileShareFailure;
			}
			GalaxyInstancePINVOKE.IFileShareListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFileShareListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFileShareListener_0))]
		private static void SwigDirectorOnFileShareSuccess(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string fileName, ulong sharedFileID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFileShareSuccess(fileName, sharedFileID);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFileShareListener_1))]
		private static void SwigDirectorOnFileShareFailure(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string fileName, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFileShareFailure(fileName, (FailureReason)failureReason);
			}
		}
	}
}