using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILobbyDataListener : GalaxyTypeAwareListenerLobbyData
	{
		public delegate void SwigDelegateILobbyDataListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID);
	
		private static Dictionary<IntPtr, ILobbyDataListener> listeners = new Dictionary<IntPtr, ILobbyDataListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILobbyDataListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(GalaxyID)
		};
	
		internal ILobbyDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILobbyDataListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyDataListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILobbyDataListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILobbyDataListener()
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
						GalaxyInstancePINVOKE.delete_ILobbyDataListener(swigCPtr);
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
	
		public abstract void OnLobbyDataUpdated(GalaxyID lobbyID, GalaxyID memberID);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLobbyDataUpdated", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLobbyDataUpdated;
			}
			GalaxyInstancePINVOKE.ILobbyDataListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyDataListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyDataListener_0))]
		private static void SwigDirectorOnLobbyDataUpdated(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyDataUpdated(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()), new GalaxyID(new GalaxyID(memberID, cMemoryOwn: false).ToUint64()));
			}
		}
	}
}