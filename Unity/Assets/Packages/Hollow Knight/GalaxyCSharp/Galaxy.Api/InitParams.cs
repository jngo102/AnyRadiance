using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public class InitParams : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		public string clientID
		{
			get
			{
				string result = GalaxyInstancePINVOKE.InitParams_clientID_get(swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return result;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_clientID_set(swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}
	
		public string clientSecret
		{
			get
			{
				string result = GalaxyInstancePINVOKE.InitParams_clientSecret_get(swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return result;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_clientSecret_set(swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}
	
		public string configFilePath
		{
			get
			{
				string result = GalaxyInstancePINVOKE.InitParams_configFilePath_get(swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return result;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_configFilePath_set(swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}
	
		public string storagePath
		{
			get
			{
				string result = GalaxyInstancePINVOKE.InitParams_storagePath_get(swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return result;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_storagePath_set(swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}
	
		public string galaxyModulePath
		{
			get
			{
				string result = GalaxyInstancePINVOKE.InitParams_galaxyModulePath_get(swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return result;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_galaxyModulePath_set(swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}
	
		public bool loadModule
		{
			get
			{
				bool result = GalaxyInstancePINVOKE.InitParams_loadModule_get(swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return result;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_loadModule_set(swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}
	
		internal InitParams(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		public InitParams(string _clientID, string _clientSecret, string _configFilePath, string _galaxyModulePath, bool _loadModule, string _storagePath)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_0(_clientID, _clientSecret, _configFilePath, _galaxyModulePath, _loadModule, _storagePath), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public InitParams(string _clientID, string _clientSecret, string _configFilePath, string _galaxyModulePath, bool _loadModule)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_1(_clientID, _clientSecret, _configFilePath, _galaxyModulePath, _loadModule), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public InitParams(string _clientID, string _clientSecret, string _configFilePath, string _galaxyModulePath)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_2(_clientID, _clientSecret, _configFilePath, _galaxyModulePath), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public InitParams(string _clientID, string _clientSecret, string _configFilePath)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_3(_clientID, _clientSecret, _configFilePath), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public InitParams(string _clientID, string _clientSecret)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_4(_clientID, _clientSecret), cMemoryOwn: true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		internal static HandleRef getCPtr(InitParams obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~InitParams()
		{
			Dispose();
		}
	
		public virtual void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_InitParams(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	}
}