using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IAccessTokenListener : GalaxyTypeAwareListenerAccessToken
	{
		public delegate void SwigDelegateIAccessTokenListener_0(IntPtr cPtr);
	
		private static Dictionary<IntPtr, IAccessTokenListener> listeners = new Dictionary<IntPtr, IAccessTokenListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIAccessTokenListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[0];
	
		internal IAccessTokenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IAccessTokenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IAccessTokenListener()
			: this(GalaxyInstancePINVOKE.new_IAccessTokenListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IAccessTokenListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IAccessTokenListener()
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
						GalaxyInstancePINVOKE.delete_IAccessTokenListener(swigCPtr);
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
	
		public abstract void OnAccessTokenChanged();
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnAccessTokenChanged", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnAccessTokenChanged;
			}
			GalaxyInstancePINVOKE.IAccessTokenListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IAccessTokenListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIAccessTokenListener_0))]
		private static void SwigDirectorOnAccessTokenChanged(IntPtr cPtr)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnAccessTokenChanged();
			}
		}
	}
}