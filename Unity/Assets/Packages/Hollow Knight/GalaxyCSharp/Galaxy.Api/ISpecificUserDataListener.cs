using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ISpecificUserDataListener : GalaxyTypeAwareListenerSpecificUserData
	{
		public delegate void SwigDelegateISpecificUserDataListener_0(IntPtr cPtr, IntPtr userID);
	
		private static Dictionary<IntPtr, ISpecificUserDataListener> listeners = new Dictionary<IntPtr, ISpecificUserDataListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateISpecificUserDataListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(GalaxyID) };
	
		internal ISpecificUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISpecificUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ISpecificUserDataListener()
			: this(GalaxyInstancePINVOKE.new_ISpecificUserDataListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ISpecificUserDataListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ISpecificUserDataListener()
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
						GalaxyInstancePINVOKE.delete_ISpecificUserDataListener(swigCPtr);
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
	
		public abstract void OnSpecificUserDataUpdated(GalaxyID userID);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnSpecificUserDataUpdated", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnSpecificUserDataUpdated;
			}
			GalaxyInstancePINVOKE.ISpecificUserDataListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISpecificUserDataListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateISpecificUserDataListener_0))]
		private static void SwigDirectorOnSpecificUserDataUpdated(IntPtr cPtr, IntPtr userID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnSpecificUserDataUpdated(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()));
			}
		}
	}
}