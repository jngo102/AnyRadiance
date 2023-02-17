using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILobbyMemberStateListener : GalaxyTypeAwareListenerLobbyMemberState
	{
		public delegate void SwigDelegateILobbyMemberStateListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int memberStateChange);
	
		private static Dictionary<IntPtr, ILobbyMemberStateListener> listeners = new Dictionary<IntPtr, ILobbyMemberStateListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILobbyMemberStateListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[3]
		{
			typeof(GalaxyID),
			typeof(GalaxyID),
			typeof(LobbyMemberStateChange)
		};
	
		internal ILobbyMemberStateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyMemberStateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILobbyMemberStateListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyMemberStateListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILobbyMemberStateListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILobbyMemberStateListener()
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
						GalaxyInstancePINVOKE.delete_ILobbyMemberStateListener(swigCPtr);
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
	
		public abstract void OnLobbyMemberStateChanged(GalaxyID lobbyID, GalaxyID memberID, LobbyMemberStateChange memberStateChange);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLobbyMemberStateChanged", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLobbyMemberStateChanged;
			}
			GalaxyInstancePINVOKE.ILobbyMemberStateListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyMemberStateListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyMemberStateListener_0))]
		private static void SwigDirectorOnLobbyMemberStateChanged(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int memberStateChange)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyMemberStateChanged(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()), new GalaxyID(new GalaxyID(memberID, cMemoryOwn: false).ToUint64()), (LobbyMemberStateChange)memberStateChange);
			}
		}
	}
}