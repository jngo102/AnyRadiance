using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IFriendInvitationListener : GalaxyTypeAwareListenerFriendInvitation
	{
		public delegate void SwigDelegateIFriendInvitationListener_0(IntPtr cPtr, IntPtr userID, uint sendTime);
	
		private static Dictionary<IntPtr, IFriendInvitationListener> listeners = new Dictionary<IntPtr, IFriendInvitationListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIFriendInvitationListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(uint)
		};
	
		internal IFriendInvitationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendInvitationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IFriendInvitationListener()
			: this(GalaxyInstancePINVOKE.new_IFriendInvitationListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IFriendInvitationListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IFriendInvitationListener()
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
						GalaxyInstancePINVOKE.delete_IFriendInvitationListener(swigCPtr);
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
	
		public abstract void OnFriendInvitationReceived(GalaxyID userID, uint sendTime);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnFriendInvitationReceived", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnFriendInvitationReceived;
			}
			GalaxyInstancePINVOKE.IFriendInvitationListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendInvitationListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFriendInvitationListener_0))]
		private static void SwigDirectorOnFriendInvitationReceived(IntPtr cPtr, IntPtr userID, uint sendTime)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFriendInvitationReceived(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), sendTime);
			}
		}
	}
}