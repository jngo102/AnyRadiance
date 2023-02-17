using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class IPersonaDataChangedListener : GalaxyTypeAwareListenerPersonaDataChanged
	{
		public delegate void SwigDelegateIPersonaDataChangedListener_0(IntPtr cPtr, IntPtr userID, uint personaStateChange);
	
		public enum PersonaStateChange
		{
			PERSONA_CHANGE_NONE = 0,
			PERSONA_CHANGE_NAME = 1,
			PERSONA_CHANGE_AVATAR = 2,
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_SMALL = 4,
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_MEDIUM = 8,
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_LARGE = 16,
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_ANY = 28
		}
	
		private static Dictionary<IntPtr, IPersonaDataChangedListener> listeners = new Dictionary<IntPtr, IPersonaDataChangedListener>();
	
		private HandleRef swigCPtr;
	
		private SwigDelegateIPersonaDataChangedListener_0 swigDelegate0;
	
		private static Type[] swigMethodTypes0 = new Type[2]
		{
			typeof(GalaxyID),
			typeof(uint)
		};
	
		internal IPersonaDataChangedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IPersonaDataChangedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			listeners.Add(cPtr, this);
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public IPersonaDataChangedListener()
			: this(GalaxyInstancePINVOKE.new_IPersonaDataChangedListener(), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			SwigDirectorConnect();
		}
	
		internal static HandleRef getCPtr(IPersonaDataChangedListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IPersonaDataChangedListener()
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
						GalaxyInstancePINVOKE.delete_IPersonaDataChangedListener(swigCPtr);
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
	
		public abstract void OnPersonaDataChanged(GalaxyID userID, uint personaStateChange);
	
		private void SwigDirectorConnect()
		{
			if (SwigDerivedClassHasMethod("OnPersonaDataChanged", swigMethodTypes0))
			{
				swigDelegate0 = SwigDirectorOnPersonaDataChanged;
			}
			GalaxyInstancePINVOKE.IPersonaDataChangedListener_director_connect(swigCPtr, swigDelegate0);
		}
	
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IPersonaDataChangedListener));
		}
	
		[MonoPInvokeCallback(typeof(SwigDelegateIPersonaDataChangedListener_0))]
		private static void SwigDirectorOnPersonaDataChanged(IntPtr cPtr, IntPtr userID, uint personaStateChange)
		{
			if (listeners.ContainsKey(cPtr))
			{
				listeners[cPtr].OnPersonaDataChanged(new GalaxyID(new GalaxyID(userID, cMemoryOwn: false).ToUint64()), personaStateChange);
			}
		}
	}
}