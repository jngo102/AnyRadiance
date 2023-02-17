using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILobbyDataRetrieveListener : GalaxyTypeAwareListenerLobbyDataRetrieve
	{
		public delegate void SwigDelegateILobbyDataRetrieveListener_0(IntPtr cPtr, IntPtr lobbyID);
	
		public delegate void SwigDelegateILobbyDataRetrieveListener_1(IntPtr cPtr, IntPtr lobbyID, int failureReason);
	
		public enum FailureReason
		{
			FAILURE_REASON_UNDEFINED,
			FAILURE_REASON_LOBBY_DOES_NOT_EXIST,
			FAILURE_REASON_CONNECTION_FAILURE
		}
	
		private static Dictionary<IntPtr, ILobbyDataRetrieveListener> listeners = new Dictionary<IntPtr, ILobbyDataRetrieveListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILobbyDataRetrieveListener_0 swigDelegate0;
	
		private SwigDelegateILobbyDataRetrieveListener_1 swigDelegate1;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(GalaxyID) };
	
		private static Type[] swigMethodTypes1 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(FailureReason)
		};
	
		internal ILobbyDataRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyDataRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILobbyDataRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyDataRetrieveListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILobbyDataRetrieveListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILobbyDataRetrieveListener()
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
						GalaxyInstancePINVOKE.delete_ILobbyDataRetrieveListener(swigCPtr);
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
	
		public abstract void OnLobbyDataRetrieveSuccess(GalaxyID lobbyID);
	
		public abstract void OnLobbyDataRetrieveFailure(GalaxyID lobbyID, FailureReason failureReason);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLobbyDataRetrieveSuccess", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLobbyDataRetrieveSuccess;
			}
			if (SwigDerivedClassHasMethod("OnLobbyDataRetrieveFailure", swigMethodTypes1))
			{
				swigDelegate1 = SwigDirectorOnLobbyDataRetrieveFailure;
			}
			GalaxyInstancePINVOKE.ILobbyDataRetrieveListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyDataRetrieveListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyDataRetrieveListener_0))]
		private static void SwigDirectorOnLobbyDataRetrieveSuccess(IntPtr cPtr, IntPtr lobbyID)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyDataRetrieveSuccess(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()));
			}
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyDataRetrieveListener_1))]
		private static void SwigDirectorOnLobbyDataRetrieveFailure(IntPtr cPtr, IntPtr lobbyID, int failureReason)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyDataRetrieveFailure(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()), (FailureReason)failureReason);
			}
		}
	}
}