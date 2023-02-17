using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IFriendAddListener : GalaxyTypeAwareListenerFriendAdd
	{
		public delegate void SwigDelegateIFriendAddListener_0(IntPtr cPtr, IntPtr userID, int invitationDirection);
	
		public enum InvitationDirection
		{
			INVITATION_DIRECTION_INCOMING,
			INVITATION_DIRECTION_OUTGOING
		}
	
		private static Dictionary<IntPtr, IFriendAddListener> listeners = new Dictionary<IntPtr, IFriendAddListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIFriendAddListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(InvitationDirection)
		};
	
		internal IFriendAddListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFriendAddListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IFriendAddListener()
			: this(GalaxyInstancePINVOKE.new_IFriendAddListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IFriendAddListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IFriendAddListener()
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
						GalaxyInstancePINVOKE.delete_IFriendAddListener(swigCPtr);
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
	
		public abstract void OnFriendAdded(GalaxyID userID, InvitationDirection invitationDirection);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnFriendAdded", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnFriendAdded;
			}
			GalaxyInstancePINVOKE.IFriendAddListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFriendAddListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIFriendAddListener_0))]
		private static void SwigDirectorOnFriendAdded(IntPtr cPtr, IntPtr userID, int invitationDirection)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnFriendAdded(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), (InvitationDirection)invitationDirection);
			}
		}
	}
}