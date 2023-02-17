using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILobbyCreatedListener : GalaxyTypeAwareListenerLobbyCreated
	{
		public delegate void SwigDelegateILobbyCreatedListener_0(IntPtr cPtr, IntPtr lobbyID, int _result);
	
		private static Dictionary<IntPtr, ILobbyCreatedListener> listeners = new Dictionary<IntPtr, ILobbyCreatedListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILobbyCreatedListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(LobbyCreateResult)
		};
	
		internal ILobbyCreatedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyCreatedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILobbyCreatedListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyCreatedListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILobbyCreatedListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILobbyCreatedListener()
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
						GalaxyInstancePINVOKE.delete_ILobbyCreatedListener(swigCPtr);
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
	
		public abstract void OnLobbyCreated(GalaxyID lobbyID, LobbyCreateResult _result);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLobbyCreated", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLobbyCreated;
			}
			GalaxyInstancePINVOKE.ILobbyCreatedListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyCreatedListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyCreatedListener_0))]
		private static void SwigDirectorOnLobbyCreated(IntPtr cPtr, IntPtr lobbyID, int _result)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyCreated(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()), (LobbyCreateResult)_result);
			}
		}
	}
}