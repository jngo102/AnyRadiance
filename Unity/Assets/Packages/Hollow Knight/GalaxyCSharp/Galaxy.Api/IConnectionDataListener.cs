using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IConnectionDataListener : GalaxyTypeAwareListenerConnectionData
	{
		public delegate void SwigDelegateIConnectionDataListener_0(IntPtr cPtr, ulong connectionID, uint dataSize);
	
		private static Dictionary<IntPtr, IConnectionDataListener> listeners = new Dictionary<IntPtr, IConnectionDataListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIConnectionDataListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(ulong),
			typeof(uint)
		};
	
		internal IConnectionDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IConnectionDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IConnectionDataListener()
			: this(GalaxyInstancePINVOKE.new_IConnectionDataListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IConnectionDataListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IConnectionDataListener()
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
						GalaxyInstancePINVOKE.delete_IConnectionDataListener(swigCPtr);
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
	
		public abstract void OnConnectionDataReceived(ulong connectionID, uint dataSize);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnConnectionDataReceived", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnConnectionDataReceived;
			}
			GalaxyInstancePINVOKE.IConnectionDataListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IConnectionDataListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIConnectionDataListener_0))]
		private static void SwigDirectorOnConnectionDataReceived(IntPtr cPtr, ulong connectionID, uint dataSize)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnConnectionDataReceived(connectionID, dataSize);
			}
		}
	}
}