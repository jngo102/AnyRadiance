using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IChatRoomMessagesListener : GalaxyTypeAwareListenerChatRoomMessages
	{
		public delegate void SwigDelegateIChatRoomMessagesListener_0(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght);
	
		private static Dictionary<IntPtr, IChatRoomMessagesListener> listeners = new Dictionary<IntPtr, IChatRoomMessagesListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIChatRoomMessagesListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[3]
		{
			typeof(ulong),
			typeof(uint),
			typeof(uint)
		};
	
		internal IChatRoomMessagesListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomMessagesListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IChatRoomMessagesListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomMessagesListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IChatRoomMessagesListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IChatRoomMessagesListener()
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
						GalaxyInstancePINVOKE.delete_IChatRoomMessagesListener(swigCPtr);
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
	
		public abstract void OnChatRoomMessagesReceived(ulong chatRoomID, uint messageCount, uint longestMessageLenght);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnChatRoomMessagesReceived", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnChatRoomMessagesReceived;
			}
			GalaxyInstancePINVOKE.IChatRoomMessagesListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomMessagesListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIChatRoomMessagesListener_0))]
		private static void SwigDirectorOnChatRoomMessagesReceived(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnChatRoomMessagesReceived(chatRoomID, messageCount, longestMessageLenght);
			}
		}
	}
}