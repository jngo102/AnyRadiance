using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILobbyOwnerChangeListener : GalaxyTypeAwareListenerLobbyOwnerChange
	{
		public delegate void SwigDelegateILobbyOwnerChangeListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr newOwnerID);
	
		private static Dictionary<IntPtr, ILobbyOwnerChangeListener> listeners = new Dictionary<IntPtr, ILobbyOwnerChangeListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILobbyOwnerChangeListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(GalaxyID)
		};
	
		internal ILobbyOwnerChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyOwnerChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILobbyOwnerChangeListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyOwnerChangeListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILobbyOwnerChangeListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILobbyOwnerChangeListener()
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
						GalaxyInstancePINVOKE.delete_ILobbyOwnerChangeListener(swigCPtr);
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
	
		public abstract void OnLobbyOwnerChanged(GalaxyID lobbyID, GalaxyID newOwnerID);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLobbyOwnerChanged", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLobbyOwnerChanged;
			}
			GalaxyInstancePINVOKE.ILobbyOwnerChangeListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyOwnerChangeListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyOwnerChangeListener_0))]
		private static void SwigDirectorOnLobbyOwnerChanged(IntPtr cPtr, IntPtr lobbyID, IntPtr newOwnerID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyOwnerChanged(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()), new GalaxyID(new GalaxyID(newOwnerID, cMemoryOwn: false).ToUint64()));
			}
		}
	}
}