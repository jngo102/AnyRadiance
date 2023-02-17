using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILobbyListListener : GalaxyTypeAwareListenerLobbyList
	{
		public delegate void SwigDelegateILobbyListListener_0(IntPtr cPtr, uint lobbyCount, int _result);
	
		private static Dictionary<IntPtr, ILobbyListListener> listeners = new Dictionary<IntPtr, ILobbyListListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILobbyListListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(uint),
			typeof(LobbyListResult)
		};
	
		internal ILobbyListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILobbyListListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyListListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILobbyListListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILobbyListListener()
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
						GalaxyInstancePINVOKE.delete_ILobbyListListener(swigCPtr);
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
	
		public abstract void OnLobbyList(uint lobbyCount, LobbyListResult _result);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLobbyList", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLobbyList;
			}
			GalaxyInstancePINVOKE.ILobbyListListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyListListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyListListener_0))]
		private static void SwigDirectorOnLobbyList(IntPtr cPtr, uint lobbyCount, int _result)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyList(lobbyCount, (LobbyListResult)_result);
			}
		}
	}
}