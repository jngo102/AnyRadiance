using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IGameInvitationReceivedListener : GalaxyTypeAwareListenerGameInvitationReceived
	{
		public delegate void SwigDelegateIGameInvitationReceivedListener_0(IntPtr cPtr, IntPtr userID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString);
	
		private static Dictionary<IntPtr, IGameInvitationReceivedListener> listeners = new Dictionary<IntPtr, IGameInvitationReceivedListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIGameInvitationReceivedListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(string)
		};
	
		internal IGameInvitationReceivedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IGameInvitationReceivedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IGameInvitationReceivedListener()
			: this(GalaxyInstancePINVOKE.new_IGameInvitationReceivedListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IGameInvitationReceivedListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IGameInvitationReceivedListener()
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
						GalaxyInstancePINVOKE.delete_IGameInvitationReceivedListener(swigCPtr);
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
	
		public abstract void OnGameInvitationReceived(GalaxyID userID, string connectionString);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnGameInvitationReceived", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnGameInvitationReceived;
			}
			GalaxyInstancePINVOKE.IGameInvitationReceivedListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IGameInvitationReceivedListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIGameInvitationReceivedListener_0))]
		private static void SwigDirectorOnGameInvitationReceived(IntPtr cPtr, IntPtr userID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnGameInvitationReceived(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), connectionString);
			}
		}
	}
}