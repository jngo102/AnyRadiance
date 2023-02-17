using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	
	public static class GalaxyInstance
	{
		public class Error : ApplicationException
		{
			public Error(string message)
				: base(message)
			{
			}
		}
	
		public class UnauthorizedAccessError : Error
		{
			public UnauthorizedAccessError(string message)
				: base(message)
			{
			}
		}
	
		public class InvalidArgumentError : Error
		{
			public InvalidArgumentError(string message)
				: base(message)
			{
			}
		}
	
		public class InvalidStateError : Error
		{
			public InvalidStateError(string message)
				: base(message)
			{
			}
		}
	
		public class RuntimeError : Error
		{
			public RuntimeError(string message)
				: base(message)
			{
			}
		}
	
		private class CustomExceptionHelper
		{
			public delegate void CustomExceptionDelegate(IError.Type type, string message);
	
			private static CustomExceptionDelegate customDelegate;
	
			static CustomExceptionHelper()
			{
				customDelegate = SetPendingCustomException;
				CustomExceptionRegisterCallback(customDelegate);
			}
	
			[DllImport("GalaxyCSharpGlue")]
			public static extern void CustomExceptionRegisterCallback(CustomExceptionDelegate customCallback);
	
			[MonoPInvokeCallback(typeof(CustomExceptionDelegate))]
			private static void SetPendingCustomException(IError.Type type, string message)
			{
				switch (type)
				{
				case IError.Type.UNAUTHORIZED_ACCESS:
					GalaxyInstancePINVOKE.SWIGPendingException.Set(new UnauthorizedAccessError(message));
					break;
				case IError.Type.INVALID_ARGUMENT:
					GalaxyInstancePINVOKE.SWIGPendingException.Set(new InvalidArgumentError(message));
					break;
				case IError.Type.INVALID_STATE:
					GalaxyInstancePINVOKE.SWIGPendingException.Set(new InvalidStateError(message));
					break;
				case IError.Type.RUNTIME_ERROR:
					GalaxyInstancePINVOKE.SWIGPendingException.Set(new RuntimeError(message));
					break;
				default:
					GalaxyInstancePINVOKE.SWIGPendingException.Set(new ApplicationException(message));
					break;
				}
			}
		}
	
		private static CustomExceptionHelper exceptionHelper;
	
		static GalaxyInstance()
		{
			exceptionHelper = new CustomExceptionHelper();
		}
	
		public static IError GetError()
		{
			IntPtr error = GalaxyInstancePINVOKE.GetError();
			IError result = ((!(error == IntPtr.Zero)) ? new IError(error, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static IListenerRegistrar ListenerRegistrar()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.ListenerRegistrar();
			IListenerRegistrar result = ((!(intPtr == IntPtr.Zero)) ? new IListenerRegistrar(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static IListenerRegistrar GameServerListenerRegistrar()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerListenerRegistrar();
			IListenerRegistrar result = ((!(intPtr == IntPtr.Zero)) ? new IListenerRegistrar(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static void Init(InitParams initpParams)
		{
			GalaxyInstancePINVOKE.Init(InitParams.getCPtr(initpParams));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public static void InitGameServer(InitParams initpParams)
		{
			GalaxyInstancePINVOKE.InitGameServer(InitParams.getCPtr(initpParams));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public static void Shutdown(bool unloadModule)
		{
			GalaxyInstancePINVOKE.Shutdown(unloadModule);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public static IUser User()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.User();
			IUser result = ((!(intPtr == IntPtr.Zero)) ? new IUser(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static IFriends Friends()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Friends();
			IFriends result = ((!(intPtr == IntPtr.Zero)) ? new IFriends(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static IChat Chat()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Chat();
			IChat result = ((!(intPtr == IntPtr.Zero)) ? new IChat(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static IMatchmaking Matchmaking()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Matchmaking();
			IMatchmaking result = ((!(intPtr == IntPtr.Zero)) ? new IMatchmaking(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static INetworking Networking()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Networking();
			INetworking result = ((!(intPtr == IntPtr.Zero)) ? new INetworking(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static IStats Stats()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Stats();
			IStats result = ((!(intPtr == IntPtr.Zero)) ? new IStats(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static IUtils Utils()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Utils();
			IUtils result = ((!(intPtr == IntPtr.Zero)) ? new IUtils(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static IApps Apps()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Apps();
			IApps result = ((!(intPtr == IntPtr.Zero)) ? new IApps(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static IStorage Storage()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Storage();
			IStorage result = ((!(intPtr == IntPtr.Zero)) ? new IStorage(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static ICustomNetworking CustomNetworking()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.CustomNetworking();
			ICustomNetworking result = ((!(intPtr == IntPtr.Zero)) ? new ICustomNetworking(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static ILogger Logger()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Logger();
			ILogger result = ((!(intPtr == IntPtr.Zero)) ? new ILogger(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static ITelemetry Telemetry()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.Telemetry();
			ITelemetry result = ((!(intPtr == IntPtr.Zero)) ? new ITelemetry(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static void ProcessData()
		{
			GalaxyInstancePINVOKE.ProcessData();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public static void ShutdownGameServer()
		{
			GalaxyInstancePINVOKE.ShutdownGameServer();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public static IUser GameServerUser()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerUser();
			IUser result = ((!(intPtr == IntPtr.Zero)) ? new IUser(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static IMatchmaking GameServerMatchmaking()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerMatchmaking();
			IMatchmaking result = ((!(intPtr == IntPtr.Zero)) ? new IMatchmaking(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static INetworking GameServerNetworking()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerNetworking();
			INetworking result = ((!(intPtr == IntPtr.Zero)) ? new INetworking(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static IUtils GameServerUtils()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerUtils();
			IUtils result = ((!(intPtr == IntPtr.Zero)) ? new IUtils(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static ITelemetry GameServerTelemetry()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerTelemetry();
			ITelemetry result = ((!(intPtr == IntPtr.Zero)) ? new ITelemetry(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static ILogger GameServerLogger()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.GameServerLogger();
			ILogger result = ((!(intPtr == IntPtr.Zero)) ? new ILogger(intPtr, cMemoryOwn: false) : null);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public static void ProcessGameServerData()
		{
			GalaxyInstancePINVOKE.ProcessGameServerData();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	}
}