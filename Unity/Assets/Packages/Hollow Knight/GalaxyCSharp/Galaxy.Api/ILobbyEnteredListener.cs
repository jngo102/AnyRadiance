using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILobbyEnteredListener : GalaxyTypeAwareListenerLobbyEntered
	{
		public delegate void SwigDelegateILobbyEnteredListener_0(IntPtr cPtr, IntPtr lobbyID, int _result);
	
		private static Dictionary<IntPtr, ILobbyEnteredListener> listeners = new Dictionary<IntPtr, ILobbyEnteredListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILobbyEnteredListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(LobbyEnterResult)
		};
	
		internal ILobbyEnteredListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyEnteredListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILobbyEnteredListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyEnteredListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILobbyEnteredListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILobbyEnteredListener()
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
						GalaxyInstancePINVOKE.delete_ILobbyEnteredListener(swigCPtr);
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
	
		public abstract void OnLobbyEntered(GalaxyID lobbyID, LobbyEnterResult _result);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLobbyEntered", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLobbyEntered;
			}
			GalaxyInstancePINVOKE.ILobbyEnteredListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyEnteredListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyEnteredListener_0))]
		private static void SwigDirectorOnLobbyEntered(IntPtr cPtr, IntPtr lobbyID, int _result)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyEntered(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()), (LobbyEnterResult)_result);
			}
		}
	}
}