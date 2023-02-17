using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IOverlayVisibilityChangeListener : GalaxyTypeAwareListenerOverlayVisibilityChange
	{
		public delegate void SwigDelegateIOverlayVisibilityChangeListener_0(IntPtr cPtr, bool overlayVisible);
	
		private static Dictionary<IntPtr, IOverlayVisibilityChangeListener> listeners = new Dictionary<IntPtr, IOverlayVisibilityChangeListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIOverlayVisibilityChangeListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(bool) };
	
		internal IOverlayVisibilityChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOverlayVisibilityChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IOverlayVisibilityChangeListener()
			: this(GalaxyInstancePINVOKE.new_IOverlayVisibilityChangeListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IOverlayVisibilityChangeListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IOverlayVisibilityChangeListener()
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
						GalaxyInstancePINVOKE.delete_IOverlayVisibilityChangeListener(swigCPtr);
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
	
		public virtual void OnOverlayVisibilityChanged(bool overlayVisible)
		{
			GalaxyInstancePINVOKE.IOverlayVisibilityChangeListener_OnOverlayVisibilityChanged(swigCPtr, overlayVisible);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnOverlayVisibilityChanged", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnOverlayVisibilityChanged;
			}
			GalaxyInstancePINVOKE.IOverlayVisibilityChangeListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOverlayVisibilityChangeListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIOverlayVisibilityChangeListener_0))]
		private static void SwigDirectorOnOverlayVisibilityChanged(IntPtr cPtr, bool overlayVisible)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnOverlayVisibilityChanged(overlayVisible);
			}
		}
	}
}