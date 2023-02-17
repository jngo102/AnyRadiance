using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IGameJoinRequestedListener : GalaxyTypeAwareListenerGameJoinRequested
	{
		public delegate void SwigDelegateIGameJoinRequestedListener_0(IntPtr cPtr, IntPtr userID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString);
	
		private static Dictionary<IntPtr, IGameJoinRequestedListener> listeners = new Dictionary<IntPtr, IGameJoinRequestedListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIGameJoinRequestedListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(string)
		};
	
		internal IGameJoinRequestedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IGameJoinRequestedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IGameJoinRequestedListener()
			: this(GalaxyInstancePINVOKE.new_IGameJoinRequestedListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IGameJoinRequestedListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IGameJoinRequestedListener()
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
						GalaxyInstancePINVOKE.delete_IGameJoinRequestedListener(swigCPtr);
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
	
		public abstract void OnGameJoinRequested(GalaxyID userID, string connectionString);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnGameJoinRequested", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnGameJoinRequested;
			}
			GalaxyInstancePINVOKE.IGameJoinRequestedListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IGameJoinRequestedListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIGameJoinRequestedListener_0))]
		private static void SwigDirectorOnGameJoinRequested(IntPtr cPtr, IntPtr userID, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string connectionString)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnGameJoinRequested(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), connectionString);
			}
		}
	}
}