using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILobbyDataUpdateListener : GalaxyTypeAwareListenerLobbyDataUpdate
	{
		public delegate void SwigDelegateILobbyDataUpdateListener_0(IntPtr cPtr, IntPtr lobbyID);
	
		public delegate void SwigDelegateILobbyDataUpdateListener_1(IntPtr cPtr, IntPtr lobbyID, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_LOBBY_DOES_NOT_EXIST,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, ILobbyDataUpdateListener> listeners = new Dictionary<IntPtr, ILobbyDataUpdateListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILobbyDataUpdateListener_0 swigDelegate0;
	
		private SwigDelegateILobbyDataUpdateListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(GalaxyID) };
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(FailureReason)
		};
	
		internal ILobbyDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILobbyDataUpdateListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyDataUpdateListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILobbyDataUpdateListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILobbyDataUpdateListener()
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
						GalaxyInstancePINVOKE.delete_ILobbyDataUpdateListener(swigCPtr);
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
	
		public abstract void OnLobbyDataUpdateSuccess(GalaxyID lobbyID);
	
		public abstract void OnLobbyDataUpdateFailure(GalaxyID lobbyID, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLobbyDataUpdateSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLobbyDataUpdateSuccess;
			}
			if (SwigDerivedClassHasMethod("OnLobbyDataUpdateFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnLobbyDataUpdateFailure;
			}
			GalaxyInstancePINVOKE.ILobbyDataUpdateListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyDataUpdateListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyDataUpdateListener_0))]
		private static void SwigDirectorOnLobbyDataUpdateSuccess(IntPtr cPtr, IntPtr lobbyID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyDataUpdateSuccess(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()));
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyDataUpdateListener_1))]
		private static void SwigDirectorOnLobbyDataUpdateFailure(IntPtr cPtr, IntPtr lobbyID, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyDataUpdateFailure(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()), (FailureReason)failureReason);
			}
		}
	}
}