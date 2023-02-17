using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class ILobbyMessageListener : GalaxyTypeAwareListenerLobbyMessage
	{
		public delegate void SwigDelegateILobbyMessageListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr senderID, uint messageID, uint messageLength);
	
		private static Dictionary<IntPtr, ILobbyMessageListener> listeners = new Dictionary<IntPtr, ILobbyMessageListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateILobbyMessageListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[4]
		{
			typeof(GalaxyID),
			typeof(GalaxyID),
			typeof(uint),
			typeof(uint)
		};
	
		internal ILobbyMessageListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyMessageListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public ILobbyMessageListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyMessageListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(ILobbyMessageListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~ILobbyMessageListener()
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
						GalaxyInstancePINVOKE.delete_ILobbyMessageListener(swigCPtr);
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
	
		public abstract void OnLobbyMessageReceived(GalaxyID lobbyID, GalaxyID senderID, uint messageID, uint messageLength);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnLobbyMessageReceived", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnLobbyMessageReceived;
			}
			GalaxyInstancePINVOKE.ILobbyMessageListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyMessageListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateILobbyMessageListener_0))]
		private static void SwigDirectorOnLobbyMessageReceived(IntPtr cPtr, IntPtr lobbyID, IntPtr senderID, uint messageID, uint messageLength)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnLobbyMessageReceived(new GalaxyID(new GalaxyID(lobbyID, cMemoryOwn: false).ToUint64()), new GalaxyID(new GalaxyID(senderID, cMemoryOwn: false).ToUint64()), messageID, messageLength);
			}
		}
	}
}