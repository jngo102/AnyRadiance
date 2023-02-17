using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IAchievementChangeListener : GalaxyTypeAwareListenerAchievementChange
	{
		public delegate void SwigDelegateIAchievementChangeListener_0(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name);
	
		private static Dictionary<IntPtr, IAchievementChangeListener> listeners = new Dictionary<IntPtr, IAchievementChangeListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIAchievementChangeListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[1] { typeof(string) };
	
		internal IAchievementChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IAchievementChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IAchievementChangeListener()
			: this(GalaxyInstancePINVOKE.new_IAchievementChangeListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IAchievementChangeListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IAchievementChangeListener()
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
						GalaxyInstancePINVOKE.delete_IAchievementChangeListener(swigCPtr);
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
	
		public abstract void OnAchievementUnlocked(string name);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnAchievementUnlocked", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnAchievementUnlocked;
			}
			GalaxyInstancePINVOKE.IAchievementChangeListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IAchievementChangeListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIAchievementChangeListener_0))]
		private static void SwigDirectorOnAchievementUnlocked(IntPtr cPtr, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string name)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnAchievementUnlocked(name);
			}
		}
	}
}