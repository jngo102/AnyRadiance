using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class INetworkingListener : GalaxyTypeAwareListenerNetworking
	{
		public delegate void SwigDelegateINetworkingListener_0(IntPtr cPtr, uint msgSize, byte channel);
	
		private static Dictionary<IntPtr, INetworkingListener> listeners = new Dictionary<IntPtr, INetworkingListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateINetworkingListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(uint),
			typeof(byte)
		};
	
		internal INetworkingListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.INetworkingListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public INetworkingListener()
			: this(GalaxyInstancePINVOKE.new_INetworkingListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(INetworkingListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~INetworkingListener()
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
						GalaxyInstancePINVOKE.delete_INetworkingListener(swigCPtr);
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
	
		public abstract void OnP2PPacketAvailable(uint msgSize, byte channel);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnP2PPacketAvailable", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnP2PPacketAvailable;
			}
			GalaxyInstancePINVOKE.INetworkingListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(INetworkingListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateINetworkingListener_0))]
		private static void SwigDirectorOnP2PPacketAvailable(IntPtr cPtr, uint msgSize, byte channel)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnP2PPacketAvailable(msgSize, channel);
			}
		}
	}
}