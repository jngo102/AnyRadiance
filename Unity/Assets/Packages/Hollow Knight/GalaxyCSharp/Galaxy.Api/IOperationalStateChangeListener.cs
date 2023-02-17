using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IOperationalStateChangeListener : GalaxyTypeAwareListenerOperationalStateChange
	{
		public delegate void SwigDelegateIOperationalStateChangeListener_0(IntPtr cPtr, uint operationalState);
	
		public enum OperationalState
		{
			OPERATIONAL_STATE_SIGNED_IN = 1,
			OPERATIONAL_STATE_LOGGED_ON
		}
	
		private static Dictionary<IntPtr, IOperationalStateChangeListener> listeners = new Dictionary<IntPtr, IOperationalStateChangeListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIOperationalStateChangeListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(uint) };
	
		internal IOperationalStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOperationalStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IOperationalStateChangeListener()
			: this(GalaxyInstancePINVOKE.new_IOperationalStateChangeListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IOperationalStateChangeListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IOperationalStateChangeListener()
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
						GalaxyInstancePINVOKE.delete_IOperationalStateChangeListener(swigCPtr);
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
	
		public abstract void OnOperationalStateChanged(uint operationalState);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnOperationalStateChanged", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnOperationalStateChanged;
			}
			GalaxyInstancePINVOKE.IOperationalStateChangeListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOperationalStateChangeListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIOperationalStateChangeListener_0))]
		private static void SwigDirectorOnOperationalStateChanged(IntPtr cPtr, uint operationalState)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnOperationalStateChanged(operationalState);
			}
		}
	}
}