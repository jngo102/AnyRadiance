using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILobbyLeftListener : GalaxyTypeAwareListenerLobbyLeft
	{
		public delegate void SwigDelegateILobbyLeftListener_0(IntPtr cPtr, IntPtr lobbyID, int leaveReason);
	
		public enum LobbyLeaveReason
		{
			LOBBY_LEAVE_REASON_UNDEFINED,
			LOBBY_LEAVE_REASON_USER_LEFT,
			LOBBY_LEAVE_REASON_LOBBY_CLOSED,
			LOBBY_LEAVE_REASON_CONNECTION_LOST
		}
	
		private static Dictionary<IntPtr, ILobbyLeftListener> listeners = new Dictionary<IntPtr, ILobbyLeftListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILobbyLeftListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(LobbyLeaveReason)
		};
	
		internal ILobbyLeftListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyLeftListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILobbyLeftListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyLeftListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILobbyLeftListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILobbyLeftListener()
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
						GalaxyInstancePINVOKE.delete_ILobbyLeftListener(swigCPtr);
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
	
		public abstract void OnLobbyLeft(GalaxyID lobbyID, LobbyLeaveReason leaveReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLobbyLeft", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLobbyLeft;
			}
			GalaxyInstancePINVOKE.ILobbyLeftListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyLeftListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyLeftListener_0))]
		private static void SwigDirectorOnLobbyLeft(IntPtr cPtr, IntPtr lobbyID, int leaveReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyLeft(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()), (LobbyLeaveReason)leaveReason);
			}
		}
	}
}