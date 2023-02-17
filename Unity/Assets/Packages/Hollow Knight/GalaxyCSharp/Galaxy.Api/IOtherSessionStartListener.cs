using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IOtherSessionStartListener : GalaxyTypeAwareListenerOtherSessionStart
	{
		public delegate void SwigDelegateIOtherSessionStartListener_0(IntPtr cPtr);
	
		private static Dictionary<IntPtr, IOtherSessionStartListener> listeners = new Dictionary<IntPtr, IOtherSessionStartListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIOtherSessionStartListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[0];
	
		internal IOtherSessionStartListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOtherSessionStartListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IOtherSessionStartListener()
			: this(GalaxyInstancePINVOKE.new_IOtherSessionStartListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IOtherSessionStartListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IOtherSessionStartListener()
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
						GalaxyInstancePINVOKE.delete_IOtherSessionStartListener(swigCPtr);
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
	
		public abstract void OnOtherSessionStarted();
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnOtherSessionStarted", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnOtherSessionStarted;
			}
			GalaxyInstancePINVOKE.IOtherSessionStartListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOtherSessionStartListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIOtherSessionStartListener_0))]
		private static void SwigDirectorOnOtherSessionStarted(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnOtherSessionStarted();
			}
		}
	}
}