using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IRichPresenceListener : GalaxyTypeAwareListenerRichPresence
	{
		public delegate void SwigDelegateIRichPresenceListener_0(IntPtr cPtr, IntPtr userID);
	
		private static Dictionary<IntPtr, IRichPresenceListener> listeners = new Dictionary<IntPtr, IRichPresenceListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIRichPresenceListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(GalaxyID) };
	
		internal IRichPresenceListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRichPresenceListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IRichPresenceListener()
			: this(GalaxyInstancePINVOKE.new_IRichPresenceListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IRichPresenceListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IRichPresenceListener()
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
						GalaxyInstancePINVOKE.delete_IRichPresenceListener(swigCPtr);
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
	
		public abstract void OnRichPresenceUpdated(GalaxyID userID);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnRichPresenceUpdated", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnRichPresenceUpdated;
			}
			GalaxyInstancePINVOKE.IRichPresenceListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IRichPresenceListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIRichPresenceListener_0))]
		private static void SwigDirectorOnRichPresenceUpdated(IntPtr cPtr, IntPtr userID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnRichPresenceUpdated(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()));
			}
		}
	}
}