using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public abstract class GlobalAchievementChangeListener : IAchievementChangeListener
	{
		private HandleRef swigCPtr;
	
		internal GlobalAchievementChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GlobalAchievementChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public GlobalAchievementChangeListener()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Register(GalaxyTypeAwareListenerAchievementChange.GetListenerType(), this);
			}
		}
	
		internal static HandleRef getCPtr(GlobalAchievementChangeListener obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~GlobalAchievementChangeListener()
		{
			Dispose();
		}
	
		public override void Dispose()
		{
			if (GalaxyInstance.ListenerRegistrar() != null)
			{
				GalaxyInstance.ListenerRegistrar().Unregister(GalaxyTypeAwareListenerAchievementChange.GetListenerType(), this);
			}
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GlobalAchievementChangeListener(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	}
}