using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IUserDataListener : GalaxyTypeAwareListenerUserData
	{
		public delegate void SwigDelegateIUserDataListener_0(IntPtr cPtr);
	
		private static Dictionary<IntPtr, IUserDataListener> listeners = new Dictionary<IntPtr, IUserDataListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIUserDataListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[0];
	
		internal IUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IUserDataListener()
			: this(GalaxyInstancePINVOKE.new_IUserDataListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IUserDataListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IUserDataListener()
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
						GalaxyInstancePINVOKE.delete_IUserDataListener(swigCPtr);
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
	
		public abstract void OnUserDataUpdated();
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnUserDataUpdated", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnUserDataUpdated;
			}
			GalaxyInstancePINVOKE.IUserDataListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserDataListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIUserDataListener_0))]
		private static void SwigDirectorOnUserDataUpdated(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnUserDataUpdated();
			}
		}
	}
}