using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IConnectionCloseListener : GalaxyTypeAwareListenerConnectionClose
	{
		public delegate void SwigDelegateIConnectionCloseListener_0(IntPtr cPtr, ulong connectionID, int closeReason);
	
		public enum CloseReason
		{
			CLOSE_REASON_UNDEFINED
		}
	
		private static Dictionary<IntPtr, IConnectionCloseListener> listeners = new Dictionary<IntPtr, IConnectionCloseListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIConnectionCloseListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(ulong),
			typeof(CloseReason)
		};
	
		internal IConnectionCloseListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IConnectionCloseListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IConnectionCloseListener()
			: this(GalaxyInstancePINVOKE.new_IConnectionCloseListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IConnectionCloseListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IConnectionCloseListener()
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
						GalaxyInstancePINVOKE.delete_IConnectionCloseListener(swigCPtr);
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
	
		public abstract void OnConnectionClosed(ulong connectionID, CloseReason closeReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnConnectionClosed", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnConnectionClosed;
			}
			GalaxyInstancePINVOKE.IConnectionCloseListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IConnectionCloseListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIConnectionCloseListener_0))]
		private static void SwigDirectorOnConnectionClosed(IntPtr cPtr, ulong connectionID, int closeReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnConnectionClosed(connectionID, (CloseReason)closeReason);
			}
		}
	}
}