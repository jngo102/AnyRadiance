using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILobbyMemberDataUpdateListener : GalaxyTypeAwareListenerLobbyMemberDataUpdate
	{
		public delegate void SwigDelegateILobbyMemberDataUpdateListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID);
	
		public delegate void SwigDelegateILobbyMemberDataUpdateListener_1(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_LOBBY_DOES_NOT_EXIST,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, ILobbyMemberDataUpdateListener> listeners = new Dictionary<IntPtr, ILobbyMemberDataUpdateListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILobbyMemberDataUpdateListener_0 swigDelegate0;
	
		private SwigDelegateILobbyMemberDataUpdateListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(GalaxyID)
		};
	
		private static Type[] swigMethodTypes1 = new Type[3]
		{
			typeof(GalaxyID),
			typeof(GalaxyID),
			typeof(FailureReason)
		};
	
		internal ILobbyMemberDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyMemberDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILobbyMemberDataUpdateListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyMemberDataUpdateListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILobbyMemberDataUpdateListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILobbyMemberDataUpdateListener()
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
						GalaxyInstancePINVOKE.delete_ILobbyMemberDataUpdateListener(swigCPtr);
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
	
		public abstract void OnLobbyMemberDataUpdateSuccess(GalaxyID lobbyID, GalaxyID memberID);
	
		public abstract void OnLobbyMemberDataUpdateFailure(GalaxyID lobbyID, GalaxyID memberID, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLobbyMemberDataUpdateSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLobbyMemberDataUpdateSuccess;
			}
			if (SwigDerivedClassHasMethod("OnLobbyMemberDataUpdateFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnLobbyMemberDataUpdateFailure;
			}
			GalaxyInstancePINVOKE.ILobbyMemberDataUpdateListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyMemberDataUpdateListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyMemberDataUpdateListener_0))]
		private static void SwigDirectorOnLobbyMemberDataUpdateSuccess(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyMemberDataUpdateSuccess(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()), new GalaxyID(new GalaxyID(memberID, cMemoryOwn: false).ToUint64()));
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyMemberDataUpdateListener_1))]
		private static void SwigDirectorOnLobbyMemberDataUpdateFailure(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyMemberDataUpdateFailure(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()), new GalaxyID(new GalaxyID(memberID, cMemoryOwn: false).ToUint64()), (FailureReason)failureReason);
			}
		}
	}
}