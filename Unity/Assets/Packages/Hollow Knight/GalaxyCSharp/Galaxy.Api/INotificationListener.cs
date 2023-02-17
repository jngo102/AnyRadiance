using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class INotificationListener : GalaxyTypeAwareListenerNotification
	{
		public delegate void SwigDelegateINotificationListener_0(IntPtr cPtr, ulong notificationID, uint typeLength, uint contentSize);
	
		private static Dictionary<IntPtr, INotificationListener> listeners = new Dictionary<IntPtr, INotificationListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateINotificationListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[3]
		{
			typeof(ulong),
			typeof(uint),
			typeof(uint)
		};
	
		internal INotificationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.INotificationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public INotificationListener()
			: this(GalaxyInstancePINVOKE.new_INotificationListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(INotificationListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~INotificationListener()
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
						GalaxyInstancePINVOKE.delete_INotificationListener(swigCPtr);
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
	
		public abstract void OnNotificationReceived(ulong notificationID, uint typeLength, uint contentSize);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnNotificationReceived", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnNotificationReceived;
			}
			GalaxyInstancePINVOKE.INotificationListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(INotificationListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateINotificationListener_0))]
		private static void SwigDirectorOnNotificationReceived(IntPtr cPtr, ulong notificationID, uint typeLength, uint contentSize)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnNotificationReceived(notificationID, typeLength, contentSize);
			}
		}
	}
}