using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ISharedFileDownloadListener : GalaxyTypeAwareListenerSharedFileDownload
	{
		public delegate void SwigDelegateISharedFileDownloadListener_0(IntPtr cPtr, ulong sharedFileID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string fileName);
	
		public delegate void SwigDelegateISharedFileDownloadListener_1(IntPtr cPtr, ulong sharedFileID, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, ISharedFileDownloadListener> listeners = new Dictionary<IntPtr, ISharedFileDownloadListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateISharedFileDownloadListener_0 swigDelegate0;
	
		private SwigDelegateISharedFileDownloadListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(ulong),
			typeof(string)
		};
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(ulong),
			typeof(FailureReason)
		};
	
		internal ISharedFileDownloadListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISharedFileDownloadListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ISharedFileDownloadListener()
			: this(GalaxyInstancePINVOKE.new_ISharedFileDownloadListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ISharedFileDownloadListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ISharedFileDownloadListener()
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
						GalaxyInstancePINVOKE.delete_ISharedFileDownloadListener(swigCPtr);
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
	
		public abstract void OnSharedFileDownloadSuccess(ulong sharedFileID, string fileName);
	
		public abstract void OnSharedFileDownloadFailure(ulong sharedFileID, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnSharedFileDownloadSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnSharedFileDownloadSuccess;
			}
			if (SwigDerivedClassHasMethod("OnSharedFileDownloadFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnSharedFileDownloadFailure;
			}
			GalaxyInstancePINVOKE.ISharedFileDownloadListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISharedFileDownloadListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateISharedFileDownloadListener_0))]
		private static void SwigDirectorOnSharedFileDownloadSuccess(IntPtr cPtr, ulong sharedFileID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string fileName)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnSharedFileDownloadSuccess(sharedFileID, fileName);
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateISharedFileDownloadListener_1))]
		private static void SwigDirectorOnSharedFileDownloadFailure(IntPtr cPtr, ulong sharedFileID, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnSharedFileDownloadFailure(sharedFileID, (FailureReason)failureReason);
			}
		}
	}
}