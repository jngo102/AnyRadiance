using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	
	internal class GalaxyInstancePINVOKE
	{
		protected class SWIGExceptionHelper
		{
			public delegate void ExceptionDelegate(string message);
	
			public delegate void ExceptionArgumentDelegate(string message, string paramName);
	
			private static ExceptionDelegate applicationDelegate;
	
			private static ExceptionDelegate arithmeticDelegate;
	
			private static ExceptionDelegate divideByZeroDelegate;
	
			private static ExceptionDelegate indexOutOfRangeDelegate;
	
			private static ExceptionDelegate invalidCastDelegate;
	
			private static ExceptionDelegate invalidOperationDelegate;
	
			private static ExceptionDelegate ioDelegate;
	
			private static ExceptionDelegate nullReferenceDelegate;
	
			private static ExceptionDelegate outOfMemoryDelegate;
	
			private static ExceptionDelegate overflowDelegate;
	
			private static ExceptionDelegate systemDelegate;
	
			private static ExceptionArgumentDelegate argumentDelegate;
	
			private static ExceptionArgumentDelegate argumentNullDelegate;
	
			private static ExceptionArgumentDelegate argumentOutOfRangeDelegate;
	
			static SWIGExceptionHelper()
			{
				applicationDelegate = SetPendingApplicationException;
				arithmeticDelegate = SetPendingArithmeticException;
				divideByZeroDelegate = SetPendingDivideByZeroException;
				indexOutOfRangeDelegate = SetPendingIndexOutOfRangeException;
				invalidCastDelegate = SetPendingInvalidCastException;
				invalidOperationDelegate = SetPendingInvalidOperationException;
				ioDelegate = SetPendingIOException;
				nullReferenceDelegate = SetPendingNullReferenceException;
				outOfMemoryDelegate = SetPendingOutOfMemoryException;
				overflowDelegate = SetPendingOverflowException;
				systemDelegate = SetPendingSystemException;
				argumentDelegate = SetPendingArgumentException;
				argumentNullDelegate = SetPendingArgumentNullException;
				argumentOutOfRangeDelegate = SetPendingArgumentOutOfRangeException;
				SWIGRegisterExceptionCallbacks_GalaxyInstance(applicationDelegate, arithmeticDelegate, divideByZeroDelegate, indexOutOfRangeDelegate, invalidCastDelegate, invalidOperationDelegate, ioDelegate, nullReferenceDelegate, outOfMemoryDelegate, overflowDelegate, systemDelegate);
				SWIGRegisterExceptionCallbacksArgument_GalaxyInstance(argumentDelegate, argumentNullDelegate, argumentOutOfRangeDelegate);
			}
	
			[DllImport("GalaxyCSharpGlue")]
			public static extern void SWIGRegisterExceptionCallbacks_GalaxyInstance(ExceptionDelegate applicationDelegate, ExceptionDelegate arithmeticDelegate, ExceptionDelegate divideByZeroDelegate, ExceptionDelegate indexOutOfRangeDelegate, ExceptionDelegate invalidCastDelegate, ExceptionDelegate invalidOperationDelegate, ExceptionDelegate ioDelegate, ExceptionDelegate nullReferenceDelegate, ExceptionDelegate outOfMemoryDelegate, ExceptionDelegate overflowDelegate, ExceptionDelegate systemExceptionDelegate);
	
			[DllImport("GalaxyCSharpGlue", EntryPoint = "SWIGRegisterExceptionArgumentCallbacks_GalaxyInstance")]
			public static extern void SWIGRegisterExceptionCallbacksArgument_GalaxyInstance(ExceptionArgumentDelegate argumentDelegate, ExceptionArgumentDelegate argumentNullDelegate, ExceptionArgumentDelegate argumentOutOfRangeDelegate);
	
			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingApplicationException(string message)
			{
				SWIGPendingException.Set(new ApplicationException(message, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingArithmeticException(string message)
			{
				SWIGPendingException.Set(new ArithmeticException(message, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingDivideByZeroException(string message)
			{
				SWIGPendingException.Set(new DivideByZeroException(message, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingIndexOutOfRangeException(string message)
			{
				SWIGPendingException.Set(new IndexOutOfRangeException(message, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingInvalidCastException(string message)
			{
				SWIGPendingException.Set(new InvalidCastException(message, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingInvalidOperationException(string message)
			{
				SWIGPendingException.Set(new InvalidOperationException(message, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingIOException(string message)
			{
				SWIGPendingException.Set(new IOException(message, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingNullReferenceException(string message)
			{
				SWIGPendingException.Set(new NullReferenceException(message, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingOutOfMemoryException(string message)
			{
				SWIGPendingException.Set(new OutOfMemoryException(message, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingOverflowException(string message)
			{
				SWIGPendingException.Set(new OverflowException(message, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionDelegate))]
			private static void SetPendingSystemException(string message)
			{
				SWIGPendingException.Set(new SystemException(message, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionArgumentDelegate))]
			private static void SetPendingArgumentException(string message, string paramName)
			{
				SWIGPendingException.Set(new ArgumentException(message, paramName, SWIGPendingException.Retrieve()));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionArgumentDelegate))]
			private static void SetPendingArgumentNullException(string message, string paramName)
			{
				Exception ex = SWIGPendingException.Retrieve();
				if (ex != null)
				{
					message = message + " Inner Exception: " + ex.Message;
				}
				SWIGPendingException.Set(new ArgumentNullException(paramName, message));
			}
	
			[MonoPInvokeCallback(typeof(ExceptionArgumentDelegate))]
			private static void SetPendingArgumentOutOfRangeException(string message, string paramName)
			{
				Exception ex = SWIGPendingException.Retrieve();
				if (ex != null)
				{
					message = message + " Inner Exception: " + ex.Message;
				}
				SWIGPendingException.Set(new ArgumentOutOfRangeException(paramName, message));
			}
		}
	
		public class SWIGPendingException
		{
			[ThreadStatic]
			private static Exception pendingException;
	
			private static int numExceptionsPending;
	
			public static bool Pending
			{
				get
				{
					bool result = false;
					if (numExceptionsPending > 0 && pendingException != null)
					{
						result = true;
					}
					return result;
				}
			}
	
			public static void Set(Exception e)
			{
				if (pendingException != null)
				{
					throw new ApplicationException("FATAL: An earlier pending exception from unmanaged code was missed and thus not thrown (" + pendingException.ToString() + ")", e);
				}
				pendingException = e;
				lock (typeof(GalaxyInstancePINVOKE))
				{
					numExceptionsPending++;
				}
			}
	
			public static Exception Retrieve()
			{
				Exception result = null;
				if (numExceptionsPending > 0 && pendingException != null)
				{
					result = pendingException;
					pendingException = null;
					lock (typeof(GalaxyInstancePINVOKE))
					{
						numExceptionsPending--;
						return result;
					}
				}
				return result;
			}
		}
	
		protected class SWIGStringHelper
		{
			public delegate string SWIGStringDelegate(string message);
	
			private static SWIGStringDelegate stringDelegate;
	
			static SWIGStringHelper()
			{
				stringDelegate = CreateString;
				SWIGRegisterStringCallback_GalaxyInstance(stringDelegate);
			}
	
			[DllImport("GalaxyCSharpGlue")]
			public static extern void SWIGRegisterStringCallback_GalaxyInstance(SWIGStringDelegate stringDelegate);
	
			[MonoPInvokeCallback(typeof(SWIGStringDelegate))]
			private static string CreateString(string cString)
			{
				return cString;
			}
		}
	
		public class UTF8Marshaler : ICustomMarshaler
		{
			private static UTF8Marshaler static_instance;
	
			public IntPtr MarshalManagedToNative(object managedObj)
			{
				if (managedObj == null)
				{
					return IntPtr.Zero;
				}
				if (!(managedObj is string))
				{
					throw new MarshalDirectiveException("UTF8Marshaler must be used on a string.");
				}
				byte[] bytes = Encoding.UTF8.GetBytes((string)managedObj);
				IntPtr intPtr = Marshal.AllocHGlobal(bytes.Length + 1);
				Marshal.Copy(bytes, 0, intPtr, bytes.Length);
				Marshal.WriteByte(intPtr, bytes.Length, 0);
				return intPtr;
			}
	
			public unsafe object MarshalNativeToManaged(IntPtr pNativeData)
			{
				byte* ptr;
				for (ptr = (byte*)(void*)pNativeData; *ptr != 0; ptr++)
				{
				}
				int num = (int)(int)(ptr - (uint)(void*)pNativeData);
				byte[] array = new byte[num];
				Marshal.Copy(pNativeData, array, 0, num);
				return Encoding.UTF8.GetString(array);
			}
	
			public void CleanUpNativeData(IntPtr pNativeData)
			{
				Marshal.FreeHGlobal(pNativeData);
			}
	
			public void CleanUpManagedData(object managedObj)
			{
			}
	
			public int GetNativeDataSize()
			{
				return -1;
			}
	
			public static ICustomMarshaler GetInstance(string cookie)
			{
				if (static_instance == null)
				{
					return static_instance = new UTF8Marshaler();
				}
				return static_instance;
			}
		}
	
		protected static SWIGExceptionHelper swigExceptionHelper;
	
		protected static SWIGStringHelper swigStringHelper;
	
		static GalaxyInstancePINVOKE()
		{
			swigExceptionHelper = new SWIGExceptionHelper();
			swigStringHelper = new SWIGStringHelper();
		}
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IError___")]
		public static extern void delete_IError(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IError_GetName___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IError_GetName(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IError_GetMsg___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IError_GetMsg(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IError_GetErrorType___")]
		public static extern int IError_GetErrorType(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IUnauthorizedAccessError___")]
		public static extern void delete_IUnauthorizedAccessError(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IInvalidArgumentError___")]
		public static extern void delete_IInvalidArgumentError(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IInvalidStateError___")]
		public static extern void delete_IInvalidStateError(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IRuntimeError___")]
		public static extern void delete_IRuntimeError(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GetError___")]
		public static extern IntPtr GetError();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IGalaxyListener___")]
		public static extern void delete_IGalaxyListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IGalaxyListener___")]
		public static extern IntPtr new_IGalaxyListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IListenerRegistrar___")]
		public static extern void delete_IListenerRegistrar(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IListenerRegistrar_Register___")]
		public static extern void IListenerRegistrar_Register(HandleRef jarg1, int jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IListenerRegistrar_Unregister___")]
		public static extern void IListenerRegistrar_Unregister(HandleRef jarg1, int jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ListenerRegistrar___")]
		public static extern IntPtr ListenerRegistrar();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerListenerRegistrar___")]
		public static extern IntPtr GameServerListenerRegistrar();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerOverlayVisibilityChange_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerOverlayVisibilityChange_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerOverlayVisibilityChange___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerOverlayVisibilityChange();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerOverlayVisibilityChange___")]
		public static extern void delete_GalaxyTypeAwareListenerOverlayVisibilityChange(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerOverlayInitializationStateChange_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerOverlayInitializationStateChange_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerOverlayInitializationStateChange___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerOverlayInitializationStateChange();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerOverlayInitializationStateChange___")]
		public static extern void delete_GalaxyTypeAwareListenerOverlayInitializationStateChange(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerGogServicesConnectionState_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerGogServicesConnectionState_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerGogServicesConnectionState___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerGogServicesConnectionState();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerGogServicesConnectionState___")]
		public static extern void delete_GalaxyTypeAwareListenerGogServicesConnectionState(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerOperationalStateChange_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerOperationalStateChange_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerOperationalStateChange___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerOperationalStateChange();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerOperationalStateChange___")]
		public static extern void delete_GalaxyTypeAwareListenerOperationalStateChange(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerAuth_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerAuth_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerAuth___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerAuth();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerAuth___")]
		public static extern void delete_GalaxyTypeAwareListenerAuth(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerOtherSessionStart_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerOtherSessionStart_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerOtherSessionStart___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerOtherSessionStart();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerOtherSessionStart___")]
		public static extern void delete_GalaxyTypeAwareListenerOtherSessionStart(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerUserData_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerUserData_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerUserData___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerUserData();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerUserData___")]
		public static extern void delete_GalaxyTypeAwareListenerUserData(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerSpecificUserData_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerSpecificUserData_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerSpecificUserData___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerSpecificUserData();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerSpecificUserData___")]
		public static extern void delete_GalaxyTypeAwareListenerSpecificUserData(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerEncryptedAppTicket_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerEncryptedAppTicket_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerEncryptedAppTicket___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerEncryptedAppTicket();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerEncryptedAppTicket___")]
		public static extern void delete_GalaxyTypeAwareListenerEncryptedAppTicket(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerAccessToken_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerAccessToken_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerAccessToken___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerAccessToken();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerAccessToken___")]
		public static extern void delete_GalaxyTypeAwareListenerAccessToken(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyList_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLobbyList_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLobbyList___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLobbyList();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLobbyList___")]
		public static extern void delete_GalaxyTypeAwareListenerLobbyList(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyCreated_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLobbyCreated_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLobbyCreated___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLobbyCreated();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLobbyCreated___")]
		public static extern void delete_GalaxyTypeAwareListenerLobbyCreated(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyEntered_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLobbyEntered_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLobbyEntered___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLobbyEntered();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLobbyEntered___")]
		public static extern void delete_GalaxyTypeAwareListenerLobbyEntered(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyLeft_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLobbyLeft_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLobbyLeft___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLobbyLeft();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLobbyLeft___")]
		public static extern void delete_GalaxyTypeAwareListenerLobbyLeft(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyData_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLobbyData_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLobbyData___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLobbyData();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLobbyData___")]
		public static extern void delete_GalaxyTypeAwareListenerLobbyData(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyDataUpdate_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLobbyDataUpdate_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLobbyDataUpdate___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLobbyDataUpdate();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLobbyDataUpdate___")]
		public static extern void delete_GalaxyTypeAwareListenerLobbyDataUpdate(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyMemberDataUpdate_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLobbyMemberDataUpdate_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLobbyMemberDataUpdate___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLobbyMemberDataUpdate();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLobbyMemberDataUpdate___")]
		public static extern void delete_GalaxyTypeAwareListenerLobbyMemberDataUpdate(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyDataRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLobbyDataRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLobbyDataRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLobbyDataRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLobbyDataRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerLobbyDataRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyMemberState_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLobbyMemberState_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLobbyMemberState___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLobbyMemberState();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLobbyMemberState___")]
		public static extern void delete_GalaxyTypeAwareListenerLobbyMemberState(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyOwnerChange_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLobbyOwnerChange_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLobbyOwnerChange___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLobbyOwnerChange();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLobbyOwnerChange___")]
		public static extern void delete_GalaxyTypeAwareListenerLobbyOwnerChange(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyMessage_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLobbyMessage_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLobbyMessage___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLobbyMessage();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLobbyMessage___")]
		public static extern void delete_GalaxyTypeAwareListenerLobbyMessage(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerStatsAndAchievementsStore_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerStatsAndAchievementsStore_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerStatsAndAchievementsStore___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerStatsAndAchievementsStore();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerStatsAndAchievementsStore___")]
		public static extern void delete_GalaxyTypeAwareListenerStatsAndAchievementsStore(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerAchievementChange_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerAchievementChange_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerAchievementChange___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerAchievementChange();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerAchievementChange___")]
		public static extern void delete_GalaxyTypeAwareListenerAchievementChange(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLeaderboardsRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLeaderboardsRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLeaderboardsRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLeaderboardsRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLeaderboardsRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerLeaderboardsRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLeaderboardEntriesRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLeaderboardEntriesRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLeaderboardEntriesRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLeaderboardEntriesRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLeaderboardEntriesRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerLeaderboardEntriesRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLeaderboardScoreUpdate_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLeaderboardScoreUpdate_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLeaderboardScoreUpdate___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLeaderboardScoreUpdate();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLeaderboardScoreUpdate___")]
		public static extern void delete_GalaxyTypeAwareListenerLeaderboardScoreUpdate(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLeaderboardRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerLeaderboardRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerLeaderboardRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerLeaderboardRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerLeaderboardRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerLeaderboardRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerUserTimePlayedRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerUserTimePlayedRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerUserTimePlayedRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerUserTimePlayedRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerUserTimePlayedRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerUserTimePlayedRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFileShare_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerFileShare_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerFileShare___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerFileShare();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerFileShare___")]
		public static extern void delete_GalaxyTypeAwareListenerFileShare(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerSharedFileDownload_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerSharedFileDownload_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerSharedFileDownload___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerSharedFileDownload();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerSharedFileDownload___")]
		public static extern void delete_GalaxyTypeAwareListenerSharedFileDownload(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerConnectionOpen_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerConnectionOpen_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerConnectionOpen___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerConnectionOpen();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerConnectionOpen___")]
		public static extern void delete_GalaxyTypeAwareListenerConnectionOpen(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerConnectionClose_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerConnectionClose_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerConnectionClose___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerConnectionClose();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerConnectionClose___")]
		public static extern void delete_GalaxyTypeAwareListenerConnectionClose(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerConnectionData_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerConnectionData_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerConnectionData___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerConnectionData();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerConnectionData___")]
		public static extern void delete_GalaxyTypeAwareListenerConnectionData(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerNetworking_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerNetworking_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerNetworking___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerNetworking();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerNetworking___")]
		public static extern void delete_GalaxyTypeAwareListenerNetworking(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerNatTypeDetection_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerNatTypeDetection_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerNatTypeDetection___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerNatTypeDetection();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerNatTypeDetection___")]
		public static extern void delete_GalaxyTypeAwareListenerNatTypeDetection(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerPersonaDataChanged_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerPersonaDataChanged_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerPersonaDataChanged___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerPersonaDataChanged();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerPersonaDataChanged___")]
		public static extern void delete_GalaxyTypeAwareListenerPersonaDataChanged(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerUserInformationRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerUserInformationRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerUserInformationRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerUserInformationRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerUserInformationRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerUserInformationRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendList_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerFriendList_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerFriendList___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerFriendList();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerFriendList___")]
		public static extern void delete_GalaxyTypeAwareListenerFriendList(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendInvitationSend_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerFriendInvitationSend_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerFriendInvitationSend___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerFriendInvitationSend();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerFriendInvitationSend___")]
		public static extern void delete_GalaxyTypeAwareListenerFriendInvitationSend(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendInvitationListRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerFriendInvitationListRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerFriendInvitationListRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerFriendInvitationListRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerFriendInvitationListRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerFriendInvitationListRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerSentFriendInvitationListRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerSentFriendInvitationListRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerSentFriendInvitationListRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerSentFriendInvitationListRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerSentFriendInvitationListRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerSentFriendInvitationListRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendInvitation_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerFriendInvitation_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerFriendInvitation___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerFriendInvitation();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerFriendInvitation___")]
		public static extern void delete_GalaxyTypeAwareListenerFriendInvitation(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendInvitationRespondTo_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerFriendInvitationRespondTo_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerFriendInvitationRespondTo___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerFriendInvitationRespondTo();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerFriendInvitationRespondTo___")]
		public static extern void delete_GalaxyTypeAwareListenerFriendInvitationRespondTo(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendAdd_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerFriendAdd_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerFriendAdd___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerFriendAdd();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerFriendAdd___")]
		public static extern void delete_GalaxyTypeAwareListenerFriendAdd(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendDelete_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerFriendDelete_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerFriendDelete___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerFriendDelete();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerFriendDelete___")]
		public static extern void delete_GalaxyTypeAwareListenerFriendDelete(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerRichPresenceChange_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerRichPresenceChange_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerRichPresenceChange___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerRichPresenceChange();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerRichPresenceChange___")]
		public static extern void delete_GalaxyTypeAwareListenerRichPresenceChange(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerRichPresence_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerRichPresence_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerRichPresence___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerRichPresence();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerRichPresence___")]
		public static extern void delete_GalaxyTypeAwareListenerRichPresence(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerRichPresenceRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerRichPresenceRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerRichPresenceRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerRichPresenceRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerRichPresenceRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerRichPresenceRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerGameJoinRequested_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerGameJoinRequested_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerGameJoinRequested___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerGameJoinRequested();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerGameJoinRequested___")]
		public static extern void delete_GalaxyTypeAwareListenerGameJoinRequested(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerGameInvitationReceived_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerGameInvitationReceived_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerGameInvitationReceived___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerGameInvitationReceived();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerGameInvitationReceived___")]
		public static extern void delete_GalaxyTypeAwareListenerGameInvitationReceived(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerSendInvitation_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerSendInvitation_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerSendInvitation___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerSendInvitation();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerSendInvitation___")]
		public static extern void delete_GalaxyTypeAwareListenerSendInvitation(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerNotification_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerNotification_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerNotification___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerNotification();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerNotification___")]
		public static extern void delete_GalaxyTypeAwareListenerNotification(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerUserFind_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerUserFind_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerUserFind___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerUserFind();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerUserFind___")]
		public static extern void delete_GalaxyTypeAwareListenerUserFind(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerChatRoomWithUserRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerChatRoomWithUserRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerChatRoomWithUserRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerChatRoomWithUserRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerChatRoomWithUserRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerChatRoomWithUserRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerChatRoomMessagesRetrieve_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerChatRoomMessagesRetrieve_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerChatRoomMessagesRetrieve___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerChatRoomMessagesRetrieve();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerChatRoomMessagesRetrieve___")]
		public static extern void delete_GalaxyTypeAwareListenerChatRoomMessagesRetrieve(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerChatRoomMessageSend_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerChatRoomMessageSend_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerChatRoomMessageSend___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerChatRoomMessageSend();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerChatRoomMessageSend___")]
		public static extern void delete_GalaxyTypeAwareListenerChatRoomMessageSend(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerChatRoomMessages_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerChatRoomMessages_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerChatRoomMessages___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerChatRoomMessages();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerChatRoomMessages___")]
		public static extern void delete_GalaxyTypeAwareListenerChatRoomMessages(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerTelemetryEventSend_GetListenerType___")]
		public static extern int GalaxyTypeAwareListenerTelemetryEventSend_GetListenerType();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyTypeAwareListenerTelemetryEventSend___")]
		public static extern IntPtr new_GalaxyTypeAwareListenerTelemetryEventSend();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyTypeAwareListenerTelemetryEventSend___")]
		public static extern void delete_GalaxyTypeAwareListenerTelemetryEventSend(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IApps___")]
		public static extern void delete_IApps(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IApps_IsDlcInstalled___")]
		public static extern bool IApps_IsDlcInstalled(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IApps_GetCurrentGameLanguage__SWIG_0___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IApps_GetCurrentGameLanguage__SWIG_0(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IApps_GetCurrentGameLanguage__SWIG_1___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IApps_GetCurrentGameLanguage__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IApps_GetCurrentGameLanguageCopy__SWIG_0___")]
		public static extern void IApps_GetCurrentGameLanguageCopy__SWIG_0(HandleRef jarg1, byte[] jarg2, uint jarg3, ulong jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IApps_GetCurrentGameLanguageCopy__SWIG_1___")]
		public static extern void IApps_GetCurrentGameLanguageCopy__SWIG_1(HandleRef jarg1, byte[] jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOverlayVisibilityChangeListener_OnOverlayVisibilityChanged___")]
		public static extern void IOverlayVisibilityChangeListener_OnOverlayVisibilityChanged(HandleRef jarg1, bool jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IOverlayVisibilityChangeListener___")]
		public static extern IntPtr new_IOverlayVisibilityChangeListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IOverlayVisibilityChangeListener___")]
		public static extern void delete_IOverlayVisibilityChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOverlayVisibilityChangeListener_director_connect___")]
		public static extern void IOverlayVisibilityChangeListener_director_connect(HandleRef jarg1, IOverlayVisibilityChangeListener.SwigDelegateIOverlayVisibilityChangeListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOverlayInitializationStateChangeListener_OnOverlayStateChanged___")]
		public static extern void IOverlayInitializationStateChangeListener_OnOverlayStateChanged(HandleRef jarg1, int jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IOverlayInitializationStateChangeListener___")]
		public static extern IntPtr new_IOverlayInitializationStateChangeListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IOverlayInitializationStateChangeListener___")]
		public static extern void delete_IOverlayInitializationStateChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOverlayInitializationStateChangeListener_director_connect___")]
		public static extern void IOverlayInitializationStateChangeListener_director_connect(HandleRef jarg1, IOverlayInitializationStateChangeListener.SwigDelegateIOverlayInitializationStateChangeListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INotificationListener_OnNotificationReceived___")]
		public static extern void INotificationListener_OnNotificationReceived(HandleRef jarg1, ulong jarg2, uint jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_INotificationListener___")]
		public static extern IntPtr new_INotificationListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_INotificationListener___")]
		public static extern void delete_INotificationListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INotificationListener_director_connect___")]
		public static extern void INotificationListener_director_connect(HandleRef jarg1, INotificationListener.SwigDelegateINotificationListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IGogServicesConnectionStateListener_OnConnectionStateChange___")]
		public static extern void IGogServicesConnectionStateListener_OnConnectionStateChange(HandleRef jarg1, int jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IGogServicesConnectionStateListener___")]
		public static extern IntPtr new_IGogServicesConnectionStateListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IGogServicesConnectionStateListener___")]
		public static extern void delete_IGogServicesConnectionStateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IGogServicesConnectionStateListener_director_connect___")]
		public static extern void IGogServicesConnectionStateListener_director_connect(HandleRef jarg1, IGogServicesConnectionStateListener.SwigDelegateIGogServicesConnectionStateListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IUtils___")]
		public static extern void delete_IUtils(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUtils_GetImageSize___")]
		public static extern void IUtils_GetImageSize(HandleRef jarg1, uint jarg2, ref int jarg3, ref int jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUtils_GetImageRGBA___")]
		public static extern void IUtils_GetImageRGBA(HandleRef jarg1, uint jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUtils_RegisterForNotification___")]
		public static extern void IUtils_RegisterForNotification(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUtils_GetNotification___")]
		public static extern uint IUtils_GetNotification(HandleRef jarg1, ulong jarg2, ref bool jarg3, byte[] jarg4, uint jarg5, byte[] jarg6, uint jarg7);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUtils_ShowOverlayWithWebPage___")]
		public static extern void IUtils_ShowOverlayWithWebPage(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUtils_IsOverlayVisible___")]
		public static extern bool IUtils_IsOverlayVisible(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUtils_GetOverlayState___")]
		public static extern int IUtils_GetOverlayState(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUtils_DisableOverlayPopups___")]
		public static extern void IUtils_DisableOverlayPopups(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUtils_GetGogServicesConnectionState___")]
		public static extern int IUtils_GetGogServicesConnectionState(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IAuthListener_OnAuthSuccess___")]
		public static extern void IAuthListener_OnAuthSuccess(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IAuthListener_OnAuthFailure___")]
		public static extern void IAuthListener_OnAuthFailure(HandleRef jarg1, int jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IAuthListener_OnAuthLost___")]
		public static extern void IAuthListener_OnAuthLost(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IAuthListener___")]
		public static extern IntPtr new_IAuthListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IAuthListener___")]
		public static extern void delete_IAuthListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IAuthListener_director_connect___")]
		public static extern void IAuthListener_director_connect(HandleRef jarg1, IAuthListener.SwigDelegateIAuthListener_0 delegate0, IAuthListener.SwigDelegateIAuthListener_1 delegate1, IAuthListener.SwigDelegateIAuthListener_2 delegate2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOtherSessionStartListener_OnOtherSessionStarted___")]
		public static extern void IOtherSessionStartListener_OnOtherSessionStarted(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IOtherSessionStartListener___")]
		public static extern IntPtr new_IOtherSessionStartListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IOtherSessionStartListener___")]
		public static extern void delete_IOtherSessionStartListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOtherSessionStartListener_director_connect___")]
		public static extern void IOtherSessionStartListener_director_connect(HandleRef jarg1, IOtherSessionStartListener.SwigDelegateIOtherSessionStartListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOperationalStateChangeListener_OnOperationalStateChanged___")]
		public static extern void IOperationalStateChangeListener_OnOperationalStateChanged(HandleRef jarg1, uint jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IOperationalStateChangeListener___")]
		public static extern IntPtr new_IOperationalStateChangeListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IOperationalStateChangeListener___")]
		public static extern void delete_IOperationalStateChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOperationalStateChangeListener_director_connect___")]
		public static extern void IOperationalStateChangeListener_director_connect(HandleRef jarg1, IOperationalStateChangeListener.SwigDelegateIOperationalStateChangeListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserDataListener_OnUserDataUpdated___")]
		public static extern void IUserDataListener_OnUserDataUpdated(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IUserDataListener___")]
		public static extern IntPtr new_IUserDataListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IUserDataListener___")]
		public static extern void delete_IUserDataListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserDataListener_director_connect___")]
		public static extern void IUserDataListener_director_connect(HandleRef jarg1, IUserDataListener.SwigDelegateIUserDataListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISpecificUserDataListener_OnSpecificUserDataUpdated___")]
		public static extern void ISpecificUserDataListener_OnSpecificUserDataUpdated(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ISpecificUserDataListener___")]
		public static extern IntPtr new_ISpecificUserDataListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ISpecificUserDataListener___")]
		public static extern void delete_ISpecificUserDataListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISpecificUserDataListener_director_connect___")]
		public static extern void ISpecificUserDataListener_director_connect(HandleRef jarg1, ISpecificUserDataListener.SwigDelegateISpecificUserDataListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IEncryptedAppTicketListener_OnEncryptedAppTicketRetrieveSuccess___")]
		public static extern void IEncryptedAppTicketListener_OnEncryptedAppTicketRetrieveSuccess(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IEncryptedAppTicketListener_OnEncryptedAppTicketRetrieveFailure___")]
		public static extern void IEncryptedAppTicketListener_OnEncryptedAppTicketRetrieveFailure(HandleRef jarg1, int jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IEncryptedAppTicketListener___")]
		public static extern IntPtr new_IEncryptedAppTicketListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IEncryptedAppTicketListener___")]
		public static extern void delete_IEncryptedAppTicketListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IEncryptedAppTicketListener_director_connect___")]
		public static extern void IEncryptedAppTicketListener_director_connect(HandleRef jarg1, IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_0 delegate0, IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IAccessTokenListener_OnAccessTokenChanged___")]
		public static extern void IAccessTokenListener_OnAccessTokenChanged(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IAccessTokenListener___")]
		public static extern IntPtr new_IAccessTokenListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IAccessTokenListener___")]
		public static extern void delete_IAccessTokenListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IAccessTokenListener_director_connect___")]
		public static extern void IAccessTokenListener_director_connect(HandleRef jarg1, IAccessTokenListener.SwigDelegateIAccessTokenListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IUser___")]
		public static extern void delete_IUser(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignedIn___")]
		public static extern bool IUser_SignedIn(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetGalaxyID___")]
		public static extern IntPtr IUser_GetGalaxyID(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInCredentials__SWIG_0___")]
		public static extern void IUser_SignInCredentials__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInCredentials__SWIG_1___")]
		public static extern void IUser_SignInCredentials__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInToken__SWIG_0___")]
		public static extern void IUser_SignInToken__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInToken__SWIG_1___")]
		public static extern void IUser_SignInToken__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInLauncher__SWIG_0___")]
		public static extern void IUser_SignInLauncher__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInLauncher__SWIG_1___")]
		public static extern void IUser_SignInLauncher__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInSteam__SWIG_0___")]
		public static extern void IUser_SignInSteam__SWIG_0(HandleRef jarg1, byte[] jarg2, uint jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInSteam__SWIG_1___")]
		public static extern void IUser_SignInSteam__SWIG_1(HandleRef jarg1, byte[] jarg2, uint jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInGalaxy__SWIG_0___")]
		public static extern void IUser_SignInGalaxy__SWIG_0(HandleRef jarg1, bool jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInGalaxy__SWIG_1___")]
		public static extern void IUser_SignInGalaxy__SWIG_1(HandleRef jarg1, bool jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInGalaxy__SWIG_2___")]
		public static extern void IUser_SignInGalaxy__SWIG_2(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInUWP__SWIG_0___")]
		public static extern void IUser_SignInUWP__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInUWP__SWIG_1___")]
		public static extern void IUser_SignInUWP__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInPS4__SWIG_0___")]
		public static extern void IUser_SignInPS4__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInPS4__SWIG_1___")]
		public static extern void IUser_SignInPS4__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInXB1__SWIG_0___")]
		public static extern void IUser_SignInXB1__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInXB1__SWIG_1___")]
		public static extern void IUser_SignInXB1__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInXBLive__SWIG_0___")]
		public static extern void IUser_SignInXBLive__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg5, HandleRef jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInXBLive__SWIG_1___")]
		public static extern void IUser_SignInXBLive__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInAnonymous__SWIG_0___")]
		public static extern void IUser_SignInAnonymous__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInAnonymous__SWIG_1___")]
		public static extern void IUser_SignInAnonymous__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInAnonymousTelemetry__SWIG_0___")]
		public static extern void IUser_SignInAnonymousTelemetry__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInAnonymousTelemetry__SWIG_1___")]
		public static extern void IUser_SignInAnonymousTelemetry__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInServerKey__SWIG_0___")]
		public static extern void IUser_SignInServerKey__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignInServerKey__SWIG_1___")]
		public static extern void IUser_SignInServerKey__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SignOut___")]
		public static extern void IUser_SignOut(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_RequestUserData__SWIG_0___")]
		public static extern void IUser_RequestUserData__SWIG_0(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_RequestUserData__SWIG_1___")]
		public static extern void IUser_RequestUserData__SWIG_1(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_RequestUserData__SWIG_2___")]
		public static extern void IUser_RequestUserData__SWIG_2(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_IsUserDataAvailable__SWIG_0___")]
		public static extern bool IUser_IsUserDataAvailable__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_IsUserDataAvailable__SWIG_1___")]
		public static extern bool IUser_IsUserDataAvailable__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetUserData__SWIG_0___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IUser_GetUserData__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetUserData__SWIG_1___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IUser_GetUserData__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetUserDataCopy__SWIG_0___")]
		public static extern void IUser_GetUserDataCopy__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, byte[] jarg3, uint jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetUserDataCopy__SWIG_1___")]
		public static extern void IUser_GetUserDataCopy__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SetUserData__SWIG_0___")]
		public static extern void IUser_SetUserData__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_SetUserData__SWIG_1___")]
		public static extern void IUser_SetUserData__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetUserDataCount__SWIG_0___")]
		public static extern uint IUser_GetUserDataCount__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetUserDataCount__SWIG_1___")]
		public static extern uint IUser_GetUserDataCount__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetUserDataByIndex__SWIG_0___")]
		public static extern bool IUser_GetUserDataByIndex__SWIG_0(HandleRef jarg1, uint jarg2, byte[] jarg3, uint jarg4, byte[] jarg5, uint jarg6, HandleRef jarg7);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetUserDataByIndex__SWIG_1___")]
		public static extern bool IUser_GetUserDataByIndex__SWIG_1(HandleRef jarg1, uint jarg2, byte[] jarg3, uint jarg4, byte[] jarg5, uint jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_DeleteUserData__SWIG_0___")]
		public static extern void IUser_DeleteUserData__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_DeleteUserData__SWIG_1___")]
		public static extern void IUser_DeleteUserData__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_IsLoggedOn___")]
		public static extern bool IUser_IsLoggedOn(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_RequestEncryptedAppTicket__SWIG_0___")]
		public static extern void IUser_RequestEncryptedAppTicket__SWIG_0(HandleRef jarg1, byte[] jarg2, uint jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_RequestEncryptedAppTicket__SWIG_1___")]
		public static extern void IUser_RequestEncryptedAppTicket__SWIG_1(HandleRef jarg1, byte[] jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetEncryptedAppTicket___")]
		public static extern void IUser_GetEncryptedAppTicket(HandleRef jarg1, byte[] jarg2, uint jarg3, ref uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetSessionID___")]
		public static extern ulong IUser_GetSessionID(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetAccessToken___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IUser_GetAccessToken(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_GetAccessTokenCopy___")]
		public static extern void IUser_GetAccessTokenCopy(HandleRef jarg1, byte[] jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_ReportInvalidAccessToken__SWIG_0___")]
		public static extern bool IUser_ReportInvalidAccessToken__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUser_ReportInvalidAccessToken__SWIG_1___")]
		public static extern bool IUser_ReportInvalidAccessToken__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILogger___")]
		public static extern void delete_ILogger(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILogger_Trace___")]
		public static extern void ILogger_Trace(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILogger_Debug___")]
		public static extern void ILogger_Debug(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILogger_Info___")]
		public static extern void ILogger_Info(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILogger_Warning___")]
		public static extern void ILogger_Warning(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILogger_Error___")]
		public static extern void ILogger_Error(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILogger_Fatal___")]
		public static extern void ILogger_Fatal(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IPersonaDataChangedListener_OnPersonaDataChanged___")]
		public static extern void IPersonaDataChangedListener_OnPersonaDataChanged(HandleRef jarg1, HandleRef jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IPersonaDataChangedListener___")]
		public static extern IntPtr new_IPersonaDataChangedListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IPersonaDataChangedListener___")]
		public static extern void delete_IPersonaDataChangedListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IPersonaDataChangedListener_director_connect___")]
		public static extern void IPersonaDataChangedListener_director_connect(HandleRef jarg1, IPersonaDataChangedListener.SwigDelegateIPersonaDataChangedListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserInformationRetrieveListener_OnUserInformationRetrieveSuccess___")]
		public static extern void IUserInformationRetrieveListener_OnUserInformationRetrieveSuccess(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserInformationRetrieveListener_OnUserInformationRetrieveFailure___")]
		public static extern void IUserInformationRetrieveListener_OnUserInformationRetrieveFailure(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IUserInformationRetrieveListener___")]
		public static extern IntPtr new_IUserInformationRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IUserInformationRetrieveListener___")]
		public static extern void delete_IUserInformationRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserInformationRetrieveListener_director_connect___")]
		public static extern void IUserInformationRetrieveListener_director_connect(HandleRef jarg1, IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_0 delegate0, IUserInformationRetrieveListener.SwigDelegateIUserInformationRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendListListener_OnFriendListRetrieveSuccess___")]
		public static extern void IFriendListListener_OnFriendListRetrieveSuccess(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendListListener_OnFriendListRetrieveFailure___")]
		public static extern void IFriendListListener_OnFriendListRetrieveFailure(HandleRef jarg1, int jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IFriendListListener___")]
		public static extern IntPtr new_IFriendListListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IFriendListListener___")]
		public static extern void delete_IFriendListListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendListListener_director_connect___")]
		public static extern void IFriendListListener_director_connect(HandleRef jarg1, IFriendListListener.SwigDelegateIFriendListListener_0 delegate0, IFriendListListener.SwigDelegateIFriendListListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationSendListener_OnFriendInvitationSendSuccess___")]
		public static extern void IFriendInvitationSendListener_OnFriendInvitationSendSuccess(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationSendListener_OnFriendInvitationSendFailure___")]
		public static extern void IFriendInvitationSendListener_OnFriendInvitationSendFailure(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IFriendInvitationSendListener___")]
		public static extern IntPtr new_IFriendInvitationSendListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IFriendInvitationSendListener___")]
		public static extern void delete_IFriendInvitationSendListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationSendListener_director_connect___")]
		public static extern void IFriendInvitationSendListener_director_connect(HandleRef jarg1, IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_0 delegate0, IFriendInvitationSendListener.SwigDelegateIFriendInvitationSendListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationListRetrieveListener_OnFriendInvitationListRetrieveSuccess___")]
		public static extern void IFriendInvitationListRetrieveListener_OnFriendInvitationListRetrieveSuccess(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationListRetrieveListener_OnFriendInvitationListRetrieveFailure___")]
		public static extern void IFriendInvitationListRetrieveListener_OnFriendInvitationListRetrieveFailure(HandleRef jarg1, int jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IFriendInvitationListRetrieveListener___")]
		public static extern IntPtr new_IFriendInvitationListRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IFriendInvitationListRetrieveListener___")]
		public static extern void delete_IFriendInvitationListRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationListRetrieveListener_director_connect___")]
		public static extern void IFriendInvitationListRetrieveListener_director_connect(HandleRef jarg1, IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_0 delegate0, IFriendInvitationListRetrieveListener.SwigDelegateIFriendInvitationListRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISentFriendInvitationListRetrieveListener_OnSentFriendInvitationListRetrieveSuccess___")]
		public static extern void ISentFriendInvitationListRetrieveListener_OnSentFriendInvitationListRetrieveSuccess(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISentFriendInvitationListRetrieveListener_OnSentFriendInvitationListRetrieveFailure___")]
		public static extern void ISentFriendInvitationListRetrieveListener_OnSentFriendInvitationListRetrieveFailure(HandleRef jarg1, int jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ISentFriendInvitationListRetrieveListener___")]
		public static extern IntPtr new_ISentFriendInvitationListRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ISentFriendInvitationListRetrieveListener___")]
		public static extern void delete_ISentFriendInvitationListRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISentFriendInvitationListRetrieveListener_director_connect___")]
		public static extern void ISentFriendInvitationListRetrieveListener_director_connect(HandleRef jarg1, ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_0 delegate0, ISentFriendInvitationListRetrieveListener.SwigDelegateISentFriendInvitationListRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationListener_OnFriendInvitationReceived___")]
		public static extern void IFriendInvitationListener_OnFriendInvitationReceived(HandleRef jarg1, HandleRef jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IFriendInvitationListener___")]
		public static extern IntPtr new_IFriendInvitationListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IFriendInvitationListener___")]
		public static extern void delete_IFriendInvitationListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationListener_director_connect___")]
		public static extern void IFriendInvitationListener_director_connect(HandleRef jarg1, IFriendInvitationListener.SwigDelegateIFriendInvitationListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationRespondToListener_OnFriendInvitationRespondToSuccess___")]
		public static extern void IFriendInvitationRespondToListener_OnFriendInvitationRespondToSuccess(HandleRef jarg1, HandleRef jarg2, bool jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationRespondToListener_OnFriendInvitationRespondToFailure___")]
		public static extern void IFriendInvitationRespondToListener_OnFriendInvitationRespondToFailure(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IFriendInvitationRespondToListener___")]
		public static extern IntPtr new_IFriendInvitationRespondToListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IFriendInvitationRespondToListener___")]
		public static extern void delete_IFriendInvitationRespondToListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationRespondToListener_director_connect___")]
		public static extern void IFriendInvitationRespondToListener_director_connect(HandleRef jarg1, IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_0 delegate0, IFriendInvitationRespondToListener.SwigDelegateIFriendInvitationRespondToListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendAddListener_OnFriendAdded___")]
		public static extern void IFriendAddListener_OnFriendAdded(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IFriendAddListener___")]
		public static extern IntPtr new_IFriendAddListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IFriendAddListener___")]
		public static extern void delete_IFriendAddListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendAddListener_director_connect___")]
		public static extern void IFriendAddListener_director_connect(HandleRef jarg1, IFriendAddListener.SwigDelegateIFriendAddListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendDeleteListener_OnFriendDeleteSuccess___")]
		public static extern void IFriendDeleteListener_OnFriendDeleteSuccess(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendDeleteListener_OnFriendDeleteFailure___")]
		public static extern void IFriendDeleteListener_OnFriendDeleteFailure(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IFriendDeleteListener___")]
		public static extern IntPtr new_IFriendDeleteListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IFriendDeleteListener___")]
		public static extern void delete_IFriendDeleteListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendDeleteListener_director_connect___")]
		public static extern void IFriendDeleteListener_director_connect(HandleRef jarg1, IFriendDeleteListener.SwigDelegateIFriendDeleteListener_0 delegate0, IFriendDeleteListener.SwigDelegateIFriendDeleteListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRichPresenceChangeListener_OnRichPresenceChangeSuccess___")]
		public static extern void IRichPresenceChangeListener_OnRichPresenceChangeSuccess(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRichPresenceChangeListener_OnRichPresenceChangeFailure___")]
		public static extern void IRichPresenceChangeListener_OnRichPresenceChangeFailure(HandleRef jarg1, int jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IRichPresenceChangeListener___")]
		public static extern IntPtr new_IRichPresenceChangeListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IRichPresenceChangeListener___")]
		public static extern void delete_IRichPresenceChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRichPresenceChangeListener_director_connect___")]
		public static extern void IRichPresenceChangeListener_director_connect(HandleRef jarg1, IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_0 delegate0, IRichPresenceChangeListener.SwigDelegateIRichPresenceChangeListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRichPresenceListener_OnRichPresenceUpdated___")]
		public static extern void IRichPresenceListener_OnRichPresenceUpdated(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IRichPresenceListener___")]
		public static extern IntPtr new_IRichPresenceListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IRichPresenceListener___")]
		public static extern void delete_IRichPresenceListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRichPresenceListener_director_connect___")]
		public static extern void IRichPresenceListener_director_connect(HandleRef jarg1, IRichPresenceListener.SwigDelegateIRichPresenceListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRichPresenceRetrieveListener_OnRichPresenceRetrieveSuccess___")]
		public static extern void IRichPresenceRetrieveListener_OnRichPresenceRetrieveSuccess(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRichPresenceRetrieveListener_OnRichPresenceRetrieveFailure___")]
		public static extern void IRichPresenceRetrieveListener_OnRichPresenceRetrieveFailure(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IRichPresenceRetrieveListener___")]
		public static extern IntPtr new_IRichPresenceRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IRichPresenceRetrieveListener___")]
		public static extern void delete_IRichPresenceRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRichPresenceRetrieveListener_director_connect___")]
		public static extern void IRichPresenceRetrieveListener_director_connect(HandleRef jarg1, IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_0 delegate0, IRichPresenceRetrieveListener.SwigDelegateIRichPresenceRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IGameJoinRequestedListener_OnGameJoinRequested___")]
		public static extern void IGameJoinRequestedListener_OnGameJoinRequested(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IGameJoinRequestedListener___")]
		public static extern IntPtr new_IGameJoinRequestedListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IGameJoinRequestedListener___")]
		public static extern void delete_IGameJoinRequestedListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IGameJoinRequestedListener_director_connect___")]
		public static extern void IGameJoinRequestedListener_director_connect(HandleRef jarg1, IGameJoinRequestedListener.SwigDelegateIGameJoinRequestedListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IGameInvitationReceivedListener_OnGameInvitationReceived___")]
		public static extern void IGameInvitationReceivedListener_OnGameInvitationReceived(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IGameInvitationReceivedListener___")]
		public static extern IntPtr new_IGameInvitationReceivedListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IGameInvitationReceivedListener___")]
		public static extern void delete_IGameInvitationReceivedListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IGameInvitationReceivedListener_director_connect___")]
		public static extern void IGameInvitationReceivedListener_director_connect(HandleRef jarg1, IGameInvitationReceivedListener.SwigDelegateIGameInvitationReceivedListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISendInvitationListener_OnInvitationSendSuccess___")]
		public static extern void ISendInvitationListener_OnInvitationSendSuccess(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISendInvitationListener_OnInvitationSendFailure___")]
		public static extern void ISendInvitationListener_OnInvitationSendFailure(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, int jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ISendInvitationListener___")]
		public static extern IntPtr new_ISendInvitationListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ISendInvitationListener___")]
		public static extern void delete_ISendInvitationListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISendInvitationListener_director_connect___")]
		public static extern void ISendInvitationListener_director_connect(HandleRef jarg1, ISendInvitationListener.SwigDelegateISendInvitationListener_0 delegate0, ISendInvitationListener.SwigDelegateISendInvitationListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserFindListener_OnUserFindSuccess___")]
		public static extern void IUserFindListener_OnUserFindSuccess(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserFindListener_OnUserFindFailure___")]
		public static extern void IUserFindListener_OnUserFindFailure(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IUserFindListener___")]
		public static extern IntPtr new_IUserFindListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IUserFindListener___")]
		public static extern void delete_IUserFindListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserFindListener_director_connect___")]
		public static extern void IUserFindListener_director_connect(HandleRef jarg1, IUserFindListener.SwigDelegateIUserFindListener_0 delegate0, IUserFindListener.SwigDelegateIUserFindListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IFriends___")]
		public static extern void delete_IFriends(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetDefaultAvatarCriteria___")]
		public static extern uint IFriends_GetDefaultAvatarCriteria(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_SetDefaultAvatarCriteria___")]
		public static extern void IFriends_SetDefaultAvatarCriteria(HandleRef jarg1, uint jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestUserInformation__SWIG_0___")]
		public static extern void IFriends_RequestUserInformation__SWIG_0(HandleRef jarg1, HandleRef jarg2, uint jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestUserInformation__SWIG_1___")]
		public static extern void IFriends_RequestUserInformation__SWIG_1(HandleRef jarg1, HandleRef jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestUserInformation__SWIG_2___")]
		public static extern void IFriends_RequestUserInformation__SWIG_2(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_IsUserInformationAvailable___")]
		public static extern bool IFriends_IsUserInformationAvailable(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetPersonaName___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IFriends_GetPersonaName(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetPersonaNameCopy___")]
		public static extern void IFriends_GetPersonaNameCopy(HandleRef jarg1, byte[] jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetPersonaState___")]
		public static extern int IFriends_GetPersonaState(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetFriendPersonaName___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IFriends_GetFriendPersonaName(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetFriendPersonaNameCopy___")]
		public static extern void IFriends_GetFriendPersonaNameCopy(HandleRef jarg1, HandleRef jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetFriendPersonaState___")]
		public static extern int IFriends_GetFriendPersonaState(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetFriendAvatarUrl___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IFriends_GetFriendAvatarUrl(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetFriendAvatarUrlCopy___")]
		public static extern void IFriends_GetFriendAvatarUrlCopy(HandleRef jarg1, HandleRef jarg2, int jarg3, byte[] jarg4, uint jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetFriendAvatarImageID___")]
		public static extern uint IFriends_GetFriendAvatarImageID(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetFriendAvatarImageRGBA___")]
		public static extern void IFriends_GetFriendAvatarImageRGBA(HandleRef jarg1, HandleRef jarg2, int jarg3, byte[] jarg4, uint jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_IsFriendAvatarImageRGBAAvailable___")]
		public static extern bool IFriends_IsFriendAvatarImageRGBAAvailable(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestFriendList__SWIG_0___")]
		public static extern void IFriends_RequestFriendList__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestFriendList__SWIG_1___")]
		public static extern void IFriends_RequestFriendList__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_IsFriend___")]
		public static extern bool IFriends_IsFriend(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetFriendCount___")]
		public static extern uint IFriends_GetFriendCount(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetFriendByIndex___")]
		public static extern IntPtr IFriends_GetFriendByIndex(HandleRef jarg1, uint jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_SendFriendInvitation__SWIG_0___")]
		public static extern void IFriends_SendFriendInvitation__SWIG_0(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_SendFriendInvitation__SWIG_1___")]
		public static extern void IFriends_SendFriendInvitation__SWIG_1(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestFriendInvitationList__SWIG_0___")]
		public static extern void IFriends_RequestFriendInvitationList__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestFriendInvitationList__SWIG_1___")]
		public static extern void IFriends_RequestFriendInvitationList__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestSentFriendInvitationList__SWIG_0___")]
		public static extern void IFriends_RequestSentFriendInvitationList__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestSentFriendInvitationList__SWIG_1___")]
		public static extern void IFriends_RequestSentFriendInvitationList__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetFriendInvitationCount___")]
		public static extern uint IFriends_GetFriendInvitationCount(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetFriendInvitationByIndex___")]
		public static extern void IFriends_GetFriendInvitationByIndex(HandleRef jarg1, uint jarg2, HandleRef jarg3, ref uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RespondToFriendInvitation__SWIG_0___")]
		public static extern void IFriends_RespondToFriendInvitation__SWIG_0(HandleRef jarg1, HandleRef jarg2, bool jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RespondToFriendInvitation__SWIG_1___")]
		public static extern void IFriends_RespondToFriendInvitation__SWIG_1(HandleRef jarg1, HandleRef jarg2, bool jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_DeleteFriend__SWIG_0___")]
		public static extern void IFriends_DeleteFriend__SWIG_0(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_DeleteFriend__SWIG_1___")]
		public static extern void IFriends_DeleteFriend__SWIG_1(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_SetRichPresence__SWIG_0___")]
		public static extern void IFriends_SetRichPresence__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_SetRichPresence__SWIG_1___")]
		public static extern void IFriends_SetRichPresence__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_DeleteRichPresence__SWIG_0___")]
		public static extern void IFriends_DeleteRichPresence__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_DeleteRichPresence__SWIG_1___")]
		public static extern void IFriends_DeleteRichPresence__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_ClearRichPresence__SWIG_0___")]
		public static extern void IFriends_ClearRichPresence__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_ClearRichPresence__SWIG_1___")]
		public static extern void IFriends_ClearRichPresence__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestRichPresence__SWIG_0___")]
		public static extern void IFriends_RequestRichPresence__SWIG_0(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestRichPresence__SWIG_1___")]
		public static extern void IFriends_RequestRichPresence__SWIG_1(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_RequestRichPresence__SWIG_2___")]
		public static extern void IFriends_RequestRichPresence__SWIG_2(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresence__SWIG_0___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IFriends_GetRichPresence__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresence__SWIG_1___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IFriends_GetRichPresence__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresenceCopy__SWIG_0___")]
		public static extern void IFriends_GetRichPresenceCopy__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, byte[] jarg3, uint jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresenceCopy__SWIG_1___")]
		public static extern void IFriends_GetRichPresenceCopy__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresenceCount__SWIG_0___")]
		public static extern uint IFriends_GetRichPresenceCount__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresenceCount__SWIG_1___")]
		public static extern uint IFriends_GetRichPresenceCount__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresenceByIndex__SWIG_0___")]
		public static extern void IFriends_GetRichPresenceByIndex__SWIG_0(HandleRef jarg1, uint jarg2, byte[] jarg3, uint jarg4, byte[] jarg5, uint jarg6, HandleRef jarg7);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresenceByIndex__SWIG_1___")]
		public static extern void IFriends_GetRichPresenceByIndex__SWIG_1(HandleRef jarg1, uint jarg2, byte[] jarg3, uint jarg4, byte[] jarg5, uint jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresenceKeyByIndex__SWIG_0___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IFriends_GetRichPresenceKeyByIndex__SWIG_0(HandleRef jarg1, uint jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresenceKeyByIndex__SWIG_1___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IFriends_GetRichPresenceKeyByIndex__SWIG_1(HandleRef jarg1, uint jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresenceKeyByIndexCopy__SWIG_0___")]
		public static extern void IFriends_GetRichPresenceKeyByIndexCopy__SWIG_0(HandleRef jarg1, uint jarg2, byte[] jarg3, uint jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_GetRichPresenceKeyByIndexCopy__SWIG_1___")]
		public static extern void IFriends_GetRichPresenceKeyByIndexCopy__SWIG_1(HandleRef jarg1, uint jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_ShowOverlayInviteDialog___")]
		public static extern void IFriends_ShowOverlayInviteDialog(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_SendInvitation__SWIG_0___")]
		public static extern void IFriends_SendInvitation__SWIG_0(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_SendInvitation__SWIG_1___")]
		public static extern void IFriends_SendInvitation__SWIG_1(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_FindUser__SWIG_0___")]
		public static extern void IFriends_FindUser__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_FindUser__SWIG_1___")]
		public static extern void IFriends_FindUser__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriends_IsUserInTheSameGame___")]
		public static extern bool IFriends_IsUserInTheSameGame(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserStatsAndAchievementsRetrieveListener_OnUserStatsAndAchievementsRetrieveSuccess___")]
		public static extern void IUserStatsAndAchievementsRetrieveListener_OnUserStatsAndAchievementsRetrieveSuccess(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserStatsAndAchievementsRetrieveListener_OnUserStatsAndAchievementsRetrieveFailure___")]
		public static extern void IUserStatsAndAchievementsRetrieveListener_OnUserStatsAndAchievementsRetrieveFailure(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IUserStatsAndAchievementsRetrieveListener___")]
		public static extern IntPtr new_IUserStatsAndAchievementsRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IUserStatsAndAchievementsRetrieveListener___")]
		public static extern void delete_IUserStatsAndAchievementsRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserStatsAndAchievementsRetrieveListener_director_connect___")]
		public static extern void IUserStatsAndAchievementsRetrieveListener_director_connect(HandleRef jarg1, IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_0 delegate0, IUserStatsAndAchievementsRetrieveListener.SwigDelegateIUserStatsAndAchievementsRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStatsAndAchievementsStoreListener_OnUserStatsAndAchievementsStoreSuccess___")]
		public static extern void IStatsAndAchievementsStoreListener_OnUserStatsAndAchievementsStoreSuccess(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStatsAndAchievementsStoreListener_OnUserStatsAndAchievementsStoreFailure___")]
		public static extern void IStatsAndAchievementsStoreListener_OnUserStatsAndAchievementsStoreFailure(HandleRef jarg1, int jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IStatsAndAchievementsStoreListener___")]
		public static extern IntPtr new_IStatsAndAchievementsStoreListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IStatsAndAchievementsStoreListener___")]
		public static extern void delete_IStatsAndAchievementsStoreListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStatsAndAchievementsStoreListener_director_connect___")]
		public static extern void IStatsAndAchievementsStoreListener_director_connect(HandleRef jarg1, IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_0 delegate0, IStatsAndAchievementsStoreListener.SwigDelegateIStatsAndAchievementsStoreListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IAchievementChangeListener_OnAchievementUnlocked___")]
		public static extern void IAchievementChangeListener_OnAchievementUnlocked(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IAchievementChangeListener___")]
		public static extern IntPtr new_IAchievementChangeListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IAchievementChangeListener___")]
		public static extern void delete_IAchievementChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IAchievementChangeListener_director_connect___")]
		public static extern void IAchievementChangeListener_director_connect(HandleRef jarg1, IAchievementChangeListener.SwigDelegateIAchievementChangeListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardsRetrieveListener_OnLeaderboardsRetrieveSuccess___")]
		public static extern void ILeaderboardsRetrieveListener_OnLeaderboardsRetrieveSuccess(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardsRetrieveListener_OnLeaderboardsRetrieveFailure___")]
		public static extern void ILeaderboardsRetrieveListener_OnLeaderboardsRetrieveFailure(HandleRef jarg1, int jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILeaderboardsRetrieveListener___")]
		public static extern IntPtr new_ILeaderboardsRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILeaderboardsRetrieveListener___")]
		public static extern void delete_ILeaderboardsRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardsRetrieveListener_director_connect___")]
		public static extern void ILeaderboardsRetrieveListener_director_connect(HandleRef jarg1, ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_0 delegate0, ILeaderboardsRetrieveListener.SwigDelegateILeaderboardsRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardEntriesRetrieveListener_OnLeaderboardEntriesRetrieveSuccess___")]
		public static extern void ILeaderboardEntriesRetrieveListener_OnLeaderboardEntriesRetrieveSuccess(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardEntriesRetrieveListener_OnLeaderboardEntriesRetrieveFailure___")]
		public static extern void ILeaderboardEntriesRetrieveListener_OnLeaderboardEntriesRetrieveFailure(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILeaderboardEntriesRetrieveListener___")]
		public static extern IntPtr new_ILeaderboardEntriesRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILeaderboardEntriesRetrieveListener___")]
		public static extern void delete_ILeaderboardEntriesRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardEntriesRetrieveListener_director_connect___")]
		public static extern void ILeaderboardEntriesRetrieveListener_director_connect(HandleRef jarg1, ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_0 delegate0, ILeaderboardEntriesRetrieveListener.SwigDelegateILeaderboardEntriesRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardScoreUpdateListener_OnLeaderboardScoreUpdateSuccess___")]
		public static extern void ILeaderboardScoreUpdateListener_OnLeaderboardScoreUpdateSuccess(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3, uint jarg4, uint jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardScoreUpdateListener_OnLeaderboardScoreUpdateFailure___")]
		public static extern void ILeaderboardScoreUpdateListener_OnLeaderboardScoreUpdateFailure(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3, int jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILeaderboardScoreUpdateListener___")]
		public static extern IntPtr new_ILeaderboardScoreUpdateListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILeaderboardScoreUpdateListener___")]
		public static extern void delete_ILeaderboardScoreUpdateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardScoreUpdateListener_director_connect___")]
		public static extern void ILeaderboardScoreUpdateListener_director_connect(HandleRef jarg1, ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_0 delegate0, ILeaderboardScoreUpdateListener.SwigDelegateILeaderboardScoreUpdateListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardRetrieveListener_OnLeaderboardRetrieveSuccess___")]
		public static extern void ILeaderboardRetrieveListener_OnLeaderboardRetrieveSuccess(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardRetrieveListener_OnLeaderboardRetrieveFailure___")]
		public static extern void ILeaderboardRetrieveListener_OnLeaderboardRetrieveFailure(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILeaderboardRetrieveListener___")]
		public static extern IntPtr new_ILeaderboardRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILeaderboardRetrieveListener___")]
		public static extern void delete_ILeaderboardRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardRetrieveListener_director_connect___")]
		public static extern void ILeaderboardRetrieveListener_director_connect(HandleRef jarg1, ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_0 delegate0, ILeaderboardRetrieveListener.SwigDelegateILeaderboardRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserTimePlayedRetrieveListener_OnUserTimePlayedRetrieveSuccess___")]
		public static extern void IUserTimePlayedRetrieveListener_OnUserTimePlayedRetrieveSuccess(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserTimePlayedRetrieveListener_OnUserTimePlayedRetrieveFailure___")]
		public static extern void IUserTimePlayedRetrieveListener_OnUserTimePlayedRetrieveFailure(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IUserTimePlayedRetrieveListener___")]
		public static extern IntPtr new_IUserTimePlayedRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IUserTimePlayedRetrieveListener___")]
		public static extern void delete_IUserTimePlayedRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserTimePlayedRetrieveListener_director_connect___")]
		public static extern void IUserTimePlayedRetrieveListener_director_connect(HandleRef jarg1, IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_0 delegate0, IUserTimePlayedRetrieveListener.SwigDelegateIUserTimePlayedRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IStats___")]
		public static extern void delete_IStats(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestUserStatsAndAchievements__SWIG_0___")]
		public static extern void IStats_RequestUserStatsAndAchievements__SWIG_0(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestUserStatsAndAchievements__SWIG_1___")]
		public static extern void IStats_RequestUserStatsAndAchievements__SWIG_1(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestUserStatsAndAchievements__SWIG_2___")]
		public static extern void IStats_RequestUserStatsAndAchievements__SWIG_2(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetStatInt__SWIG_0___")]
		public static extern int IStats_GetStatInt__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetStatInt__SWIG_1___")]
		public static extern int IStats_GetStatInt__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetStatFloat__SWIG_0___")]
		public static extern float IStats_GetStatFloat__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetStatFloat__SWIG_1___")]
		public static extern float IStats_GetStatFloat__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_SetStatInt___")]
		public static extern void IStats_SetStatInt(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_SetStatFloat___")]
		public static extern void IStats_SetStatFloat(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, float jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_UpdateAvgRateStat___")]
		public static extern void IStats_UpdateAvgRateStat(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, float jarg3, double jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetAchievement__SWIG_0___")]
		public static extern void IStats_GetAchievement__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, ref bool jarg3, ref uint jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetAchievement__SWIG_1___")]
		public static extern void IStats_GetAchievement__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, ref bool jarg3, ref uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_SetAchievement___")]
		public static extern void IStats_SetAchievement(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_ClearAchievement___")]
		public static extern void IStats_ClearAchievement(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_StoreStatsAndAchievements__SWIG_0___")]
		public static extern void IStats_StoreStatsAndAchievements__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_StoreStatsAndAchievements__SWIG_1___")]
		public static extern void IStats_StoreStatsAndAchievements__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_ResetStatsAndAchievements__SWIG_0___")]
		public static extern void IStats_ResetStatsAndAchievements__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_ResetStatsAndAchievements__SWIG_1___")]
		public static extern void IStats_ResetStatsAndAchievements__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetAchievementDisplayName___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IStats_GetAchievementDisplayName(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetAchievementDisplayNameCopy___")]
		public static extern void IStats_GetAchievementDisplayNameCopy(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetAchievementDescription___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IStats_GetAchievementDescription(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetAchievementDescriptionCopy___")]
		public static extern void IStats_GetAchievementDescriptionCopy(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_IsAchievementVisible___")]
		public static extern bool IStats_IsAchievementVisible(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_IsAchievementVisibleWhileLocked___")]
		public static extern bool IStats_IsAchievementVisibleWhileLocked(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestLeaderboards__SWIG_0___")]
		public static extern void IStats_RequestLeaderboards__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestLeaderboards__SWIG_1___")]
		public static extern void IStats_RequestLeaderboards__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetLeaderboardDisplayName___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IStats_GetLeaderboardDisplayName(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetLeaderboardDisplayNameCopy___")]
		public static extern void IStats_GetLeaderboardDisplayNameCopy(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetLeaderboardSortMethod___")]
		public static extern int IStats_GetLeaderboardSortMethod(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetLeaderboardDisplayType___")]
		public static extern int IStats_GetLeaderboardDisplayType(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestLeaderboardEntriesGlobal__SWIG_0___")]
		public static extern void IStats_RequestLeaderboardEntriesGlobal__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, uint jarg3, uint jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestLeaderboardEntriesGlobal__SWIG_1___")]
		public static extern void IStats_RequestLeaderboardEntriesGlobal__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, uint jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestLeaderboardEntriesAroundUser__SWIG_0___")]
		public static extern void IStats_RequestLeaderboardEntriesAroundUser__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, uint jarg3, uint jarg4, HandleRef jarg5, HandleRef jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestLeaderboardEntriesAroundUser__SWIG_1___")]
		public static extern void IStats_RequestLeaderboardEntriesAroundUser__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, uint jarg3, uint jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestLeaderboardEntriesAroundUser__SWIG_2___")]
		public static extern void IStats_RequestLeaderboardEntriesAroundUser__SWIG_2(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, uint jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestLeaderboardEntriesForUsers__SWIG_0___")]
		public static extern void IStats_RequestLeaderboardEntriesForUsers__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, ulong[] array, uint jarg3, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestLeaderboardEntriesForUsers__SWIG_1___")]
		public static extern void IStats_RequestLeaderboardEntriesForUsers__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, ulong[] array, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetRequestedLeaderboardEntry___")]
		public static extern void IStats_GetRequestedLeaderboardEntry(HandleRef jarg1, uint jarg2, ref uint jarg3, ref int jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetRequestedLeaderboardEntryWithDetails___")]
		public static extern void IStats_GetRequestedLeaderboardEntryWithDetails(HandleRef jarg1, uint jarg2, ref uint jarg3, ref int jarg4, byte[] jarg5, uint jarg6, ref uint jarg7, HandleRef jarg8);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_SetLeaderboardScore__SWIG_0___")]
		public static extern void IStats_SetLeaderboardScore__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3, bool jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_SetLeaderboardScore__SWIG_1___")]
		public static extern void IStats_SetLeaderboardScore__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3, bool jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_SetLeaderboardScore__SWIG_2___")]
		public static extern void IStats_SetLeaderboardScore__SWIG_2(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_SetLeaderboardScoreWithDetails__SWIG_0___")]
		public static extern void IStats_SetLeaderboardScoreWithDetails__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3, byte[] jarg4, uint jarg5, bool jarg6, HandleRef jarg7);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_SetLeaderboardScoreWithDetails__SWIG_1___")]
		public static extern void IStats_SetLeaderboardScoreWithDetails__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3, byte[] jarg4, uint jarg5, bool jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_SetLeaderboardScoreWithDetails__SWIG_2___")]
		public static extern void IStats_SetLeaderboardScoreWithDetails__SWIG_2(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3, byte[] jarg4, uint jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetLeaderboardEntryCount___")]
		public static extern uint IStats_GetLeaderboardEntryCount(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_FindLeaderboard__SWIG_0___")]
		public static extern void IStats_FindLeaderboard__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_FindLeaderboard__SWIG_1___")]
		public static extern void IStats_FindLeaderboard__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_FindOrCreateLeaderboard__SWIG_0___")]
		public static extern void IStats_FindOrCreateLeaderboard__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, int jarg4, int jarg5, HandleRef jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_FindOrCreateLeaderboard__SWIG_1___")]
		public static extern void IStats_FindOrCreateLeaderboard__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, int jarg4, int jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestUserTimePlayed__SWIG_0___")]
		public static extern void IStats_RequestUserTimePlayed__SWIG_0(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestUserTimePlayed__SWIG_1___")]
		public static extern void IStats_RequestUserTimePlayed__SWIG_1(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_RequestUserTimePlayed__SWIG_2___")]
		public static extern void IStats_RequestUserTimePlayed__SWIG_2(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetUserTimePlayed__SWIG_0___")]
		public static extern uint IStats_GetUserTimePlayed__SWIG_0(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStats_GetUserTimePlayed__SWIG_1___")]
		public static extern uint IStats_GetUserTimePlayed__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFileShareListener_OnFileShareSuccess___")]
		public static extern void IFileShareListener_OnFileShareSuccess(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, ulong jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFileShareListener_OnFileShareFailure___")]
		public static extern void IFileShareListener_OnFileShareFailure(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IFileShareListener___")]
		public static extern IntPtr new_IFileShareListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IFileShareListener___")]
		public static extern void delete_IFileShareListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFileShareListener_director_connect___")]
		public static extern void IFileShareListener_director_connect(HandleRef jarg1, IFileShareListener.SwigDelegateIFileShareListener_0 delegate0, IFileShareListener.SwigDelegateIFileShareListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISharedFileDownloadListener_OnSharedFileDownloadSuccess___")]
		public static extern void ISharedFileDownloadListener_OnSharedFileDownloadSuccess(HandleRef jarg1, ulong jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISharedFileDownloadListener_OnSharedFileDownloadFailure___")]
		public static extern void ISharedFileDownloadListener_OnSharedFileDownloadFailure(HandleRef jarg1, ulong jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ISharedFileDownloadListener___")]
		public static extern IntPtr new_ISharedFileDownloadListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ISharedFileDownloadListener___")]
		public static extern void delete_ISharedFileDownloadListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISharedFileDownloadListener_director_connect___")]
		public static extern void ISharedFileDownloadListener_director_connect(HandleRef jarg1, ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_0 delegate0, ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IStorage___")]
		public static extern void delete_IStorage(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_FileWrite___")]
		public static extern void IStorage_FileWrite(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_FileRead___")]
		public static extern uint IStorage_FileRead(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_FileDelete___")]
		public static extern void IStorage_FileDelete(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_FileExists___")]
		public static extern bool IStorage_FileExists(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_GetFileSize___")]
		public static extern uint IStorage_GetFileSize(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_GetFileTimestamp___")]
		public static extern uint IStorage_GetFileTimestamp(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_GetFileCount___")]
		public static extern uint IStorage_GetFileCount(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_GetFileNameByIndex___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IStorage_GetFileNameByIndex(HandleRef jarg1, uint jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_GetFileNameCopyByIndex___")]
		public static extern void IStorage_GetFileNameCopyByIndex(HandleRef jarg1, uint jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_FileShare__SWIG_0___")]
		public static extern void IStorage_FileShare__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_FileShare__SWIG_1___")]
		public static extern void IStorage_FileShare__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_DownloadSharedFile__SWIG_0___")]
		public static extern void IStorage_DownloadSharedFile__SWIG_0(HandleRef jarg1, ulong jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_DownloadSharedFile__SWIG_1___")]
		public static extern void IStorage_DownloadSharedFile__SWIG_1(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_GetSharedFileName___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IStorage_GetSharedFileName(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_GetSharedFileNameCopy___")]
		public static extern void IStorage_GetSharedFileNameCopy(HandleRef jarg1, ulong jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_GetSharedFileSize___")]
		public static extern uint IStorage_GetSharedFileSize(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_GetSharedFileOwner___")]
		public static extern IntPtr IStorage_GetSharedFileOwner(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_SharedFileRead__SWIG_0___")]
		public static extern uint IStorage_SharedFileRead__SWIG_0(HandleRef jarg1, ulong jarg2, byte[] jarg3, uint jarg4, uint jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_SharedFileRead__SWIG_1___")]
		public static extern uint IStorage_SharedFileRead__SWIG_1(HandleRef jarg1, ulong jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_SharedFileClose___")]
		public static extern void IStorage_SharedFileClose(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_GetDownloadedSharedFileCount___")]
		public static extern uint IStorage_GetDownloadedSharedFileCount(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStorage_GetDownloadedSharedFileByIndex___")]
		public static extern ulong IStorage_GetDownloadedSharedFileByIndex(HandleRef jarg1, uint jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IConnectionOpenListener_OnConnectionOpenSuccess___")]
		public static extern void IConnectionOpenListener_OnConnectionOpenSuccess(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, ulong jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IConnectionOpenListener_OnConnectionOpenFailure___")]
		public static extern void IConnectionOpenListener_OnConnectionOpenFailure(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IConnectionOpenListener___")]
		public static extern IntPtr new_IConnectionOpenListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IConnectionOpenListener___")]
		public static extern void delete_IConnectionOpenListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IConnectionOpenListener_director_connect___")]
		public static extern void IConnectionOpenListener_director_connect(HandleRef jarg1, IConnectionOpenListener.SwigDelegateIConnectionOpenListener_0 delegate0, IConnectionOpenListener.SwigDelegateIConnectionOpenListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IConnectionCloseListener_OnConnectionClosed___")]
		public static extern void IConnectionCloseListener_OnConnectionClosed(HandleRef jarg1, ulong jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IConnectionCloseListener___")]
		public static extern IntPtr new_IConnectionCloseListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IConnectionCloseListener___")]
		public static extern void delete_IConnectionCloseListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IConnectionCloseListener_director_connect___")]
		public static extern void IConnectionCloseListener_director_connect(HandleRef jarg1, IConnectionCloseListener.SwigDelegateIConnectionCloseListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IConnectionDataListener_OnConnectionDataReceived___")]
		public static extern void IConnectionDataListener_OnConnectionDataReceived(HandleRef jarg1, ulong jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IConnectionDataListener___")]
		public static extern IntPtr new_IConnectionDataListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IConnectionDataListener___")]
		public static extern void delete_IConnectionDataListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IConnectionDataListener_director_connect___")]
		public static extern void IConnectionDataListener_director_connect(HandleRef jarg1, IConnectionDataListener.SwigDelegateIConnectionDataListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ICustomNetworking___")]
		public static extern void delete_ICustomNetworking(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ICustomNetworking_OpenConnection__SWIG_0___")]
		public static extern void ICustomNetworking_OpenConnection__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ICustomNetworking_OpenConnection__SWIG_1___")]
		public static extern void ICustomNetworking_OpenConnection__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ICustomNetworking_CloseConnection__SWIG_0___")]
		public static extern void ICustomNetworking_CloseConnection__SWIG_0(HandleRef jarg1, ulong jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ICustomNetworking_CloseConnection__SWIG_1___")]
		public static extern void ICustomNetworking_CloseConnection__SWIG_1(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ICustomNetworking_SendData___")]
		public static extern void ICustomNetworking_SendData(HandleRef jarg1, ulong jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ICustomNetworking_GetAvailableDataSize___")]
		public static extern uint ICustomNetworking_GetAvailableDataSize(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ICustomNetworking_PeekData___")]
		public static extern void ICustomNetworking_PeekData(HandleRef jarg1, ulong jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ICustomNetworking_ReadData___")]
		public static extern void ICustomNetworking_ReadData(HandleRef jarg1, ulong jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ICustomNetworking_PopData___")]
		public static extern void ICustomNetworking_PopData(HandleRef jarg1, ulong jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworkingListener_OnP2PPacketAvailable___")]
		public static extern void INetworkingListener_OnP2PPacketAvailable(HandleRef jarg1, uint jarg2, byte jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_INetworkingListener___")]
		public static extern IntPtr new_INetworkingListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_INetworkingListener___")]
		public static extern void delete_INetworkingListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworkingListener_director_connect___")]
		public static extern void INetworkingListener_director_connect(HandleRef jarg1, INetworkingListener.SwigDelegateINetworkingListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INatTypeDetectionListener_OnNatTypeDetectionSuccess___")]
		public static extern void INatTypeDetectionListener_OnNatTypeDetectionSuccess(HandleRef jarg1, int jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INatTypeDetectionListener_OnNatTypeDetectionFailure___")]
		public static extern void INatTypeDetectionListener_OnNatTypeDetectionFailure(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_INatTypeDetectionListener___")]
		public static extern IntPtr new_INatTypeDetectionListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_INatTypeDetectionListener___")]
		public static extern void delete_INatTypeDetectionListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INatTypeDetectionListener_director_connect___")]
		public static extern void INatTypeDetectionListener_director_connect(HandleRef jarg1, INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_0 delegate0, INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_INetworking___")]
		public static extern void delete_INetworking(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_SendP2PPacket__SWIG_0___")]
		public static extern bool INetworking_SendP2PPacket__SWIG_0(HandleRef jarg1, HandleRef jarg2, byte[] jarg3, uint jarg4, int jarg5, byte jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_SendP2PPacket__SWIG_1___")]
		public static extern bool INetworking_SendP2PPacket__SWIG_1(HandleRef jarg1, HandleRef jarg2, byte[] jarg3, uint jarg4, int jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_PeekP2PPacket__SWIG_0___")]
		public static extern bool INetworking_PeekP2PPacket__SWIG_0(HandleRef jarg1, byte[] jarg2, uint jarg3, ref uint jarg4, HandleRef jarg5, byte jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_PeekP2PPacket__SWIG_1___")]
		public static extern bool INetworking_PeekP2PPacket__SWIG_1(HandleRef jarg1, byte[] jarg2, uint jarg3, ref uint jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_IsP2PPacketAvailable__SWIG_0___")]
		public static extern bool INetworking_IsP2PPacketAvailable__SWIG_0(HandleRef jarg1, ref uint jarg2, byte jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_IsP2PPacketAvailable__SWIG_1___")]
		public static extern bool INetworking_IsP2PPacketAvailable__SWIG_1(HandleRef jarg1, ref uint jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_ReadP2PPacket__SWIG_0___")]
		public static extern bool INetworking_ReadP2PPacket__SWIG_0(HandleRef jarg1, byte[] jarg2, uint jarg3, ref uint jarg4, HandleRef jarg5, byte jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_ReadP2PPacket__SWIG_1___")]
		public static extern bool INetworking_ReadP2PPacket__SWIG_1(HandleRef jarg1, byte[] jarg2, uint jarg3, ref uint jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_PopP2PPacket__SWIG_0___")]
		public static extern void INetworking_PopP2PPacket__SWIG_0(HandleRef jarg1, byte jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_PopP2PPacket__SWIG_1___")]
		public static extern void INetworking_PopP2PPacket__SWIG_1(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_GetPingWith___")]
		public static extern int INetworking_GetPingWith(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_RequestNatTypeDetection___")]
		public static extern void INetworking_RequestNatTypeDetection(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_GetNatType___")]
		public static extern int INetworking_GetNatType(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworking_GetConnectionType___")]
		public static extern int INetworking_GetConnectionType(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyListListener_OnLobbyList___")]
		public static extern void ILobbyListListener_OnLobbyList(HandleRef jarg1, uint jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILobbyListListener___")]
		public static extern IntPtr new_ILobbyListListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILobbyListListener___")]
		public static extern void delete_ILobbyListListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyListListener_director_connect___")]
		public static extern void ILobbyListListener_director_connect(HandleRef jarg1, ILobbyListListener.SwigDelegateILobbyListListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyCreatedListener_OnLobbyCreated___")]
		public static extern void ILobbyCreatedListener_OnLobbyCreated(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILobbyCreatedListener___")]
		public static extern IntPtr new_ILobbyCreatedListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILobbyCreatedListener___")]
		public static extern void delete_ILobbyCreatedListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyCreatedListener_director_connect___")]
		public static extern void ILobbyCreatedListener_director_connect(HandleRef jarg1, ILobbyCreatedListener.SwigDelegateILobbyCreatedListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyEnteredListener_OnLobbyEntered___")]
		public static extern void ILobbyEnteredListener_OnLobbyEntered(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILobbyEnteredListener___")]
		public static extern IntPtr new_ILobbyEnteredListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILobbyEnteredListener___")]
		public static extern void delete_ILobbyEnteredListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyEnteredListener_director_connect___")]
		public static extern void ILobbyEnteredListener_director_connect(HandleRef jarg1, ILobbyEnteredListener.SwigDelegateILobbyEnteredListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyLeftListener_OnLobbyLeft___")]
		public static extern void ILobbyLeftListener_OnLobbyLeft(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILobbyLeftListener___")]
		public static extern IntPtr new_ILobbyLeftListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILobbyLeftListener___")]
		public static extern void delete_ILobbyLeftListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyLeftListener_director_connect___")]
		public static extern void ILobbyLeftListener_director_connect(HandleRef jarg1, ILobbyLeftListener.SwigDelegateILobbyLeftListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyDataListener_OnLobbyDataUpdated___")]
		public static extern void ILobbyDataListener_OnLobbyDataUpdated(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILobbyDataListener___")]
		public static extern IntPtr new_ILobbyDataListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILobbyDataListener___")]
		public static extern void delete_ILobbyDataListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyDataListener_director_connect___")]
		public static extern void ILobbyDataListener_director_connect(HandleRef jarg1, ILobbyDataListener.SwigDelegateILobbyDataListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyDataUpdateListener_OnLobbyDataUpdateSuccess___")]
		public static extern void ILobbyDataUpdateListener_OnLobbyDataUpdateSuccess(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyDataUpdateListener_OnLobbyDataUpdateFailure___")]
		public static extern void ILobbyDataUpdateListener_OnLobbyDataUpdateFailure(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILobbyDataUpdateListener___")]
		public static extern IntPtr new_ILobbyDataUpdateListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILobbyDataUpdateListener___")]
		public static extern void delete_ILobbyDataUpdateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyDataUpdateListener_director_connect___")]
		public static extern void ILobbyDataUpdateListener_director_connect(HandleRef jarg1, ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_0 delegate0, ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyMemberDataUpdateListener_OnLobbyMemberDataUpdateSuccess___")]
		public static extern void ILobbyMemberDataUpdateListener_OnLobbyMemberDataUpdateSuccess(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyMemberDataUpdateListener_OnLobbyMemberDataUpdateFailure___")]
		public static extern void ILobbyMemberDataUpdateListener_OnLobbyMemberDataUpdateFailure(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3, int jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILobbyMemberDataUpdateListener___")]
		public static extern IntPtr new_ILobbyMemberDataUpdateListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILobbyMemberDataUpdateListener___")]
		public static extern void delete_ILobbyMemberDataUpdateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyMemberDataUpdateListener_director_connect___")]
		public static extern void ILobbyMemberDataUpdateListener_director_connect(HandleRef jarg1, ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_0 delegate0, ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyDataRetrieveListener_OnLobbyDataRetrieveSuccess___")]
		public static extern void ILobbyDataRetrieveListener_OnLobbyDataRetrieveSuccess(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyDataRetrieveListener_OnLobbyDataRetrieveFailure___")]
		public static extern void ILobbyDataRetrieveListener_OnLobbyDataRetrieveFailure(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILobbyDataRetrieveListener___")]
		public static extern IntPtr new_ILobbyDataRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILobbyDataRetrieveListener___")]
		public static extern void delete_ILobbyDataRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyDataRetrieveListener_director_connect___")]
		public static extern void ILobbyDataRetrieveListener_director_connect(HandleRef jarg1, ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_0 delegate0, ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyMemberStateListener_OnLobbyMemberStateChanged___")]
		public static extern void ILobbyMemberStateListener_OnLobbyMemberStateChanged(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3, int jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILobbyMemberStateListener___")]
		public static extern IntPtr new_ILobbyMemberStateListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILobbyMemberStateListener___")]
		public static extern void delete_ILobbyMemberStateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyMemberStateListener_director_connect___")]
		public static extern void ILobbyMemberStateListener_director_connect(HandleRef jarg1, ILobbyMemberStateListener.SwigDelegateILobbyMemberStateListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyOwnerChangeListener_OnLobbyOwnerChanged___")]
		public static extern void ILobbyOwnerChangeListener_OnLobbyOwnerChanged(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILobbyOwnerChangeListener___")]
		public static extern IntPtr new_ILobbyOwnerChangeListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILobbyOwnerChangeListener___")]
		public static extern void delete_ILobbyOwnerChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyOwnerChangeListener_director_connect___")]
		public static extern void ILobbyOwnerChangeListener_director_connect(HandleRef jarg1, ILobbyOwnerChangeListener.SwigDelegateILobbyOwnerChangeListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyMessageListener_OnLobbyMessageReceived___")]
		public static extern void ILobbyMessageListener_OnLobbyMessageReceived(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3, uint jarg4, uint jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ILobbyMessageListener___")]
		public static extern IntPtr new_ILobbyMessageListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ILobbyMessageListener___")]
		public static extern void delete_ILobbyMessageListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyMessageListener_director_connect___")]
		public static extern void ILobbyMessageListener_director_connect(HandleRef jarg1, ILobbyMessageListener.SwigDelegateILobbyMessageListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IMatchmaking___")]
		public static extern void delete_IMatchmaking(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_CreateLobby__SWIG_0___")]
		public static extern void IMatchmaking_CreateLobby__SWIG_0(HandleRef jarg1, int jarg2, uint jarg3, bool jarg4, int jarg5, HandleRef jarg6, HandleRef jarg7);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_CreateLobby__SWIG_1___")]
		public static extern void IMatchmaking_CreateLobby__SWIG_1(HandleRef jarg1, int jarg2, uint jarg3, bool jarg4, int jarg5, HandleRef jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_CreateLobby__SWIG_2___")]
		public static extern void IMatchmaking_CreateLobby__SWIG_2(HandleRef jarg1, int jarg2, uint jarg3, bool jarg4, int jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_RequestLobbyList__SWIG_0___")]
		public static extern void IMatchmaking_RequestLobbyList__SWIG_0(HandleRef jarg1, bool jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_RequestLobbyList__SWIG_1___")]
		public static extern void IMatchmaking_RequestLobbyList__SWIG_1(HandleRef jarg1, bool jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_RequestLobbyList__SWIG_2___")]
		public static extern void IMatchmaking_RequestLobbyList__SWIG_2(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_AddRequestLobbyListResultCountFilter___")]
		public static extern void IMatchmaking_AddRequestLobbyListResultCountFilter(HandleRef jarg1, uint jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_AddRequestLobbyListStringFilter___")]
		public static extern void IMatchmaking_AddRequestLobbyListStringFilter(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, int jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_AddRequestLobbyListNumericalFilter___")]
		public static extern void IMatchmaking_AddRequestLobbyListNumericalFilter(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3, int jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_AddRequestLobbyListNearValueFilter___")]
		public static extern void IMatchmaking_AddRequestLobbyListNearValueFilter(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyByIndex___")]
		public static extern IntPtr IMatchmaking_GetLobbyByIndex(HandleRef jarg1, uint jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_JoinLobby__SWIG_0___")]
		public static extern void IMatchmaking_JoinLobby__SWIG_0(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_JoinLobby__SWIG_1___")]
		public static extern void IMatchmaking_JoinLobby__SWIG_1(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_LeaveLobby__SWIG_0___")]
		public static extern void IMatchmaking_LeaveLobby__SWIG_0(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_LeaveLobby__SWIG_1___")]
		public static extern void IMatchmaking_LeaveLobby__SWIG_1(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_SetMaxNumLobbyMembers__SWIG_0___")]
		public static extern void IMatchmaking_SetMaxNumLobbyMembers__SWIG_0(HandleRef jarg1, HandleRef jarg2, uint jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_SetMaxNumLobbyMembers__SWIG_1___")]
		public static extern void IMatchmaking_SetMaxNumLobbyMembers__SWIG_1(HandleRef jarg1, HandleRef jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetMaxNumLobbyMembers___")]
		public static extern uint IMatchmaking_GetMaxNumLobbyMembers(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetNumLobbyMembers___")]
		public static extern uint IMatchmaking_GetNumLobbyMembers(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyMemberByIndex___")]
		public static extern IntPtr IMatchmaking_GetLobbyMemberByIndex(HandleRef jarg1, HandleRef jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_SetLobbyType__SWIG_0___")]
		public static extern void IMatchmaking_SetLobbyType__SWIG_0(HandleRef jarg1, HandleRef jarg2, int jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_SetLobbyType__SWIG_1___")]
		public static extern void IMatchmaking_SetLobbyType__SWIG_1(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyType___")]
		public static extern int IMatchmaking_GetLobbyType(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_SetLobbyJoinable__SWIG_0___")]
		public static extern void IMatchmaking_SetLobbyJoinable__SWIG_0(HandleRef jarg1, HandleRef jarg2, bool jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_SetLobbyJoinable__SWIG_1___")]
		public static extern void IMatchmaking_SetLobbyJoinable__SWIG_1(HandleRef jarg1, HandleRef jarg2, bool jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_IsLobbyJoinable___")]
		public static extern bool IMatchmaking_IsLobbyJoinable(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_RequestLobbyData__SWIG_0___")]
		public static extern void IMatchmaking_RequestLobbyData__SWIG_0(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_RequestLobbyData__SWIG_1___")]
		public static extern void IMatchmaking_RequestLobbyData__SWIG_1(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyData___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IMatchmaking_GetLobbyData(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyDataCopy___")]
		public static extern void IMatchmaking_GetLobbyDataCopy(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, byte[] jarg4, uint jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_SetLobbyData__SWIG_0___")]
		public static extern void IMatchmaking_SetLobbyData__SWIG_0(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_SetLobbyData__SWIG_1___")]
		public static extern void IMatchmaking_SetLobbyData__SWIG_1(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyDataCount___")]
		public static extern uint IMatchmaking_GetLobbyDataCount(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyDataByIndex___")]
		public static extern bool IMatchmaking_GetLobbyDataByIndex(HandleRef jarg1, HandleRef jarg2, uint jarg3, byte[] jarg4, uint jarg5, byte[] jarg6, uint jarg7);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_DeleteLobbyData__SWIG_0___")]
		public static extern void IMatchmaking_DeleteLobbyData__SWIG_0(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_DeleteLobbyData__SWIG_1___")]
		public static extern void IMatchmaking_DeleteLobbyData__SWIG_1(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyMemberData___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string IMatchmaking_GetLobbyMemberData(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyMemberDataCopy___")]
		public static extern void IMatchmaking_GetLobbyMemberDataCopy(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4, byte[] jarg5, uint jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_SetLobbyMemberData__SWIG_0___")]
		public static extern void IMatchmaking_SetLobbyMemberData__SWIG_0(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_SetLobbyMemberData__SWIG_1___")]
		public static extern void IMatchmaking_SetLobbyMemberData__SWIG_1(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyMemberDataCount___")]
		public static extern uint IMatchmaking_GetLobbyMemberDataCount(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyMemberDataByIndex___")]
		public static extern bool IMatchmaking_GetLobbyMemberDataByIndex(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3, uint jarg4, byte[] jarg5, uint jarg6, byte[] jarg7, uint jarg8);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_DeleteLobbyMemberData__SWIG_0___")]
		public static extern void IMatchmaking_DeleteLobbyMemberData__SWIG_0(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_DeleteLobbyMemberData__SWIG_1___")]
		public static extern void IMatchmaking_DeleteLobbyMemberData__SWIG_1(HandleRef jarg1, HandleRef jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyOwner___")]
		public static extern IntPtr IMatchmaking_GetLobbyOwner(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_SendLobbyMessage___")]
		public static extern bool IMatchmaking_SendLobbyMessage(HandleRef jarg1, HandleRef jarg2, byte[] jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IMatchmaking_GetLobbyMessage___")]
		public static extern uint IMatchmaking_GetLobbyMessage(HandleRef jarg1, HandleRef jarg2, uint jarg3, HandleRef jarg4, byte[] jarg5, uint jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomWithUserRetrieveListener_OnChatRoomWithUserRetrieveSuccess___")]
		public static extern void IChatRoomWithUserRetrieveListener_OnChatRoomWithUserRetrieveSuccess(HandleRef jarg1, HandleRef jarg2, ulong jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomWithUserRetrieveListener_OnChatRoomWithUserRetrieveFailure___")]
		public static extern void IChatRoomWithUserRetrieveListener_OnChatRoomWithUserRetrieveFailure(HandleRef jarg1, HandleRef jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IChatRoomWithUserRetrieveListener___")]
		public static extern IntPtr new_IChatRoomWithUserRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IChatRoomWithUserRetrieveListener___")]
		public static extern void delete_IChatRoomWithUserRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomWithUserRetrieveListener_director_connect___")]
		public static extern void IChatRoomWithUserRetrieveListener_director_connect(HandleRef jarg1, IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_0 delegate0, IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomMessageSendListener_OnChatRoomMessageSendSuccess___")]
		public static extern void IChatRoomMessageSendListener_OnChatRoomMessageSendSuccess(HandleRef jarg1, ulong jarg2, uint jarg3, ulong jarg4, uint jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomMessageSendListener_OnChatRoomMessageSendFailure___")]
		public static extern void IChatRoomMessageSendListener_OnChatRoomMessageSendFailure(HandleRef jarg1, ulong jarg2, uint jarg3, int jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IChatRoomMessageSendListener___")]
		public static extern IntPtr new_IChatRoomMessageSendListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IChatRoomMessageSendListener___")]
		public static extern void delete_IChatRoomMessageSendListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomMessageSendListener_director_connect___")]
		public static extern void IChatRoomMessageSendListener_director_connect(HandleRef jarg1, IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_0 delegate0, IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomMessagesListener_OnChatRoomMessagesReceived___")]
		public static extern void IChatRoomMessagesListener_OnChatRoomMessagesReceived(HandleRef jarg1, ulong jarg2, uint jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IChatRoomMessagesListener___")]
		public static extern IntPtr new_IChatRoomMessagesListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IChatRoomMessagesListener___")]
		public static extern void delete_IChatRoomMessagesListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomMessagesListener_director_connect___")]
		public static extern void IChatRoomMessagesListener_director_connect(HandleRef jarg1, IChatRoomMessagesListener.SwigDelegateIChatRoomMessagesListener_0 delegate0);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomMessagesRetrieveListener_OnChatRoomMessagesRetrieveSuccess___")]
		public static extern void IChatRoomMessagesRetrieveListener_OnChatRoomMessagesRetrieveSuccess(HandleRef jarg1, ulong jarg2, uint jarg3, uint jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomMessagesRetrieveListener_OnChatRoomMessagesRetrieveFailure___")]
		public static extern void IChatRoomMessagesRetrieveListener_OnChatRoomMessagesRetrieveFailure(HandleRef jarg1, ulong jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_IChatRoomMessagesRetrieveListener___")]
		public static extern IntPtr new_IChatRoomMessagesRetrieveListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IChatRoomMessagesRetrieveListener___")]
		public static extern void delete_IChatRoomMessagesRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomMessagesRetrieveListener_director_connect___")]
		public static extern void IChatRoomMessagesRetrieveListener_director_connect(HandleRef jarg1, IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_0 delegate0, IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_IChat___")]
		public static extern void delete_IChat(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_RequestChatRoomWithUser__SWIG_0___")]
		public static extern void IChat_RequestChatRoomWithUser__SWIG_0(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_RequestChatRoomWithUser__SWIG_1___")]
		public static extern void IChat_RequestChatRoomWithUser__SWIG_1(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_RequestChatRoomMessages__SWIG_0___")]
		public static extern void IChat_RequestChatRoomMessages__SWIG_0(HandleRef jarg1, ulong jarg2, uint jarg3, ulong jarg4, HandleRef jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_RequestChatRoomMessages__SWIG_1___")]
		public static extern void IChat_RequestChatRoomMessages__SWIG_1(HandleRef jarg1, ulong jarg2, uint jarg3, ulong jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_RequestChatRoomMessages__SWIG_2___")]
		public static extern void IChat_RequestChatRoomMessages__SWIG_2(HandleRef jarg1, ulong jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_SendChatRoomMessage__SWIG_0___")]
		public static extern uint IChat_SendChatRoomMessage__SWIG_0(HandleRef jarg1, ulong jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, HandleRef jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_SendChatRoomMessage__SWIG_1___")]
		public static extern uint IChat_SendChatRoomMessage__SWIG_1(HandleRef jarg1, ulong jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_GetChatRoomMessageByIndex___")]
		public static extern uint IChat_GetChatRoomMessageByIndex(HandleRef jarg1, uint jarg2, ref ulong jarg3, ref int jarg4, HandleRef jarg5, ref uint jarg6, byte[] jarg7, uint jarg8);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_GetChatRoomMemberCount___")]
		public static extern uint IChat_GetChatRoomMemberCount(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_GetChatRoomMemberUserIDByIndex___")]
		public static extern IntPtr IChat_GetChatRoomMemberUserIDByIndex(HandleRef jarg1, ulong jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_GetChatRoomUnreadMessageCount___")]
		public static extern uint IChat_GetChatRoomUnreadMessageCount(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChat_MarkChatRoomAsRead___")]
		public static extern void IChat_MarkChatRoomAsRead(HandleRef jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetryEventSendListener_OnTelemetryEventSendSuccess___")]
		public static extern void ITelemetryEventSendListener_OnTelemetryEventSendSuccess(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetryEventSendListener_OnTelemetryEventSendFailure___")]
		public static extern void ITelemetryEventSendListener_OnTelemetryEventSendFailure(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, uint jarg3, int jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_ITelemetryEventSendListener___")]
		public static extern IntPtr new_ITelemetryEventSendListener();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ITelemetryEventSendListener___")]
		public static extern void delete_ITelemetryEventSendListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetryEventSendListener_director_connect___")]
		public static extern void ITelemetryEventSendListener_director_connect(HandleRef jarg1, ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_0 delegate0, ITelemetryEventSendListener.SwigDelegateITelemetryEventSendListener_1 delegate1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_ITelemetry___")]
		public static extern void delete_ITelemetry(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_AddStringParam___")]
		public static extern void ITelemetry_AddStringParam(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_AddIntParam___")]
		public static extern void ITelemetry_AddIntParam(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, int jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_AddFloatParam___")]
		public static extern void ITelemetry_AddFloatParam(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, double jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_AddBoolParam___")]
		public static extern void ITelemetry_AddBoolParam(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, bool jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_AddObjectParam___")]
		public static extern void ITelemetry_AddObjectParam(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_AddArrayParam___")]
		public static extern void ITelemetry_AddArrayParam(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_CloseParam___")]
		public static extern void ITelemetry_CloseParam(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_ClearParams___")]
		public static extern void ITelemetry_ClearParams(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_SetSamplingClass___")]
		public static extern void ITelemetry_SetSamplingClass(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_SendTelemetryEvent__SWIG_0___")]
		public static extern uint ITelemetry_SendTelemetryEvent__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_SendTelemetryEvent__SWIG_1___")]
		public static extern uint ITelemetry_SendTelemetryEvent__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_SendAnonymousTelemetryEvent__SWIG_0___")]
		public static extern uint ITelemetry_SendAnonymousTelemetryEvent__SWIG_0(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, HandleRef jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_SendAnonymousTelemetryEvent__SWIG_1___")]
		public static extern uint ITelemetry_SendAnonymousTelemetryEvent__SWIG_1(HandleRef jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_GetVisitID___")]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")]
		public static extern string ITelemetry_GetVisitID(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_GetVisitIDCopy___")]
		public static extern void ITelemetry_GetVisitIDCopy(HandleRef jarg1, byte[] jarg2, uint jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetry_ResetVisitID___")]
		public static extern void ITelemetry_ResetVisitID(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyID_UNASSIGNED_VALUE_get___")]
		public static extern ulong GalaxyID_UNASSIGNED_VALUE_get();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyID_FromRealID___")]
		public static extern IntPtr GalaxyID_FromRealID(int jarg1, ulong jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyID__SWIG_0___")]
		public static extern IntPtr new_GalaxyID__SWIG_0();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyID__SWIG_1___")]
		public static extern IntPtr new_GalaxyID__SWIG_1(ulong jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_GalaxyID__SWIG_2___")]
		public static extern IntPtr new_GalaxyID__SWIG_2(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyID_operator_assign___")]
		public static extern IntPtr GalaxyID_operator_assign(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyID_operator_less___")]
		public static extern bool GalaxyID_operator_less(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyID_operator_equals___")]
		public static extern bool GalaxyID_operator_equals(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyID_operator_not_equals___")]
		public static extern bool GalaxyID_operator_not_equals(HandleRef jarg1, HandleRef jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyID_IsValid___")]
		public static extern bool GalaxyID_IsValid(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyID_ToUint64___")]
		public static extern ulong GalaxyID_ToUint64(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyID_GetRealID___")]
		public static extern ulong GalaxyID_GetRealID(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyID_GetIDType___")]
		public static extern int GalaxyID_GetIDType(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GalaxyID___")]
		public static extern void delete_GalaxyID(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalOverlayVisibilityChangeListener___")]
		public static extern void delete_GlobalOverlayVisibilityChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalOverlayInitializationStateChangeListener___")]
		public static extern void delete_GlobalOverlayInitializationStateChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalGogServicesConnectionStateListener___")]
		public static extern void delete_GlobalGogServicesConnectionStateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalGogServicesConnectionStateListener___")]
		public static extern void delete_GameServerGlobalGogServicesConnectionStateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalAuthListener___")]
		public static extern void delete_GlobalAuthListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalAuthListener___")]
		public static extern void delete_GameServerGlobalAuthListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalOtherSessionStartListener___")]
		public static extern void delete_GlobalOtherSessionStartListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalOperationalStateChangeListener___")]
		public static extern void delete_GlobalOperationalStateChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalOperationalStateChangeListener___")]
		public static extern void delete_GameServerGlobalOperationalStateChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalUserDataListener___")]
		public static extern void delete_GlobalUserDataListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalSpecificUserDataListener___")]
		public static extern void delete_GlobalSpecificUserDataListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalSpecificUserDataListener___")]
		public static extern void delete_GameServerGlobalSpecificUserDataListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalEncryptedAppTicketListener___")]
		public static extern void delete_GlobalEncryptedAppTicketListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalEncryptedAppTicketListener___")]
		public static extern void delete_GameServerGlobalEncryptedAppTicketListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalAccessTokenListener___")]
		public static extern void delete_GlobalAccessTokenListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalAccessTokenListener___")]
		public static extern void delete_GameServerGlobalAccessTokenListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLobbyListListener___")]
		public static extern void delete_GlobalLobbyListListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLobbyCreatedListener___")]
		public static extern void delete_GlobalLobbyCreatedListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalLobbyCreatedListener___")]
		public static extern void delete_GameServerGlobalLobbyCreatedListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLobbyEnteredListener___")]
		public static extern void delete_GlobalLobbyEnteredListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalLobbyEnteredListener___")]
		public static extern void delete_GameServerGlobalLobbyEnteredListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLobbyLeftListener___")]
		public static extern void delete_GlobalLobbyLeftListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalLobbyLeftListener___")]
		public static extern void delete_GameServerGlobalLobbyLeftListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLobbyDataListener___")]
		public static extern void delete_GlobalLobbyDataListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalLobbyDataListener___")]
		public static extern void delete_GameServerGlobalLobbyDataListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLobbyDataUpdateListener___")]
		public static extern void delete_GlobalLobbyDataUpdateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalLobbyDataUpdateListener___")]
		public static extern void delete_GameServerGlobalLobbyDataUpdateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLobbyMemberDataUpdateListener___")]
		public static extern void delete_GlobalLobbyMemberDataUpdateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalLobbyMemberDataUpdateListener___")]
		public static extern void delete_GameServerGlobalLobbyMemberDataUpdateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLobbyDataRetrieveListener___")]
		public static extern void delete_GlobalLobbyDataRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalLobbyDataRetrieveListener___")]
		public static extern void delete_GameServerGlobalLobbyDataRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLobbyMemberStateListener___")]
		public static extern void delete_GlobalLobbyMemberStateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalLobbyMemberStateListener___")]
		public static extern void delete_GameServerGlobalLobbyMemberStateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLobbyOwnerChangeListener___")]
		public static extern void delete_GlobalLobbyOwnerChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLobbyMessageListener___")]
		public static extern void delete_GlobalLobbyMessageListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalLobbyMessageListener___")]
		public static extern void delete_GameServerGlobalLobbyMessageListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalUserStatsAndAchievementsRetrieveListener___")]
		public static extern void delete_GlobalUserStatsAndAchievementsRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalStatsAndAchievementsStoreListener___")]
		public static extern void delete_GlobalStatsAndAchievementsStoreListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalAchievementChangeListener___")]
		public static extern void delete_GlobalAchievementChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLeaderboardsRetrieveListener___")]
		public static extern void delete_GlobalLeaderboardsRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLeaderboardEntriesRetrieveListener___")]
		public static extern void delete_GlobalLeaderboardEntriesRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLeaderboardScoreUpdateListener___")]
		public static extern void delete_GlobalLeaderboardScoreUpdateListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalLeaderboardRetrieveListener___")]
		public static extern void delete_GlobalLeaderboardRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalUserTimePlayedRetrieveListener___")]
		public static extern void delete_GlobalUserTimePlayedRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalFileShareListener___")]
		public static extern void delete_GlobalFileShareListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalSharedFileDownloadListener___")]
		public static extern void delete_GlobalSharedFileDownloadListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalConnectionOpenListener___")]
		public static extern void delete_GlobalConnectionOpenListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalConnectionCloseListener___")]
		public static extern void delete_GlobalConnectionCloseListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalConnectionDataListener___")]
		public static extern void delete_GlobalConnectionDataListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalNetworkingListener___")]
		public static extern void delete_GlobalNetworkingListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalNetworkingListener___")]
		public static extern void delete_GameServerGlobalNetworkingListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalNatTypeDetectionListener___")]
		public static extern void delete_GlobalNatTypeDetectionListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalPersonaDataChangedListener___")]
		public static extern void delete_GlobalPersonaDataChangedListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalFriendListListener___")]
		public static extern void delete_GlobalFriendListListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalFriendInvitationSendListener___")]
		public static extern void delete_GlobalFriendInvitationSendListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalFriendInvitationListRetrieveListener___")]
		public static extern void delete_GlobalFriendInvitationListRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalSentFriendInvitationListRetrieveListener___")]
		public static extern void delete_GlobalSentFriendInvitationListRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalFriendInvitationListener___")]
		public static extern void delete_GlobalFriendInvitationListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalFriendInvitationRespondToListener___")]
		public static extern void delete_GlobalFriendInvitationRespondToListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalFriendAddListener___")]
		public static extern void delete_GlobalFriendAddListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalFriendDeleteListener___")]
		public static extern void delete_GlobalFriendDeleteListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalRichPresenceChangeListener___")]
		public static extern void delete_GlobalRichPresenceChangeListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalRichPresenceListener___")]
		public static extern void delete_GlobalRichPresenceListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalGameJoinRequestedListener___")]
		public static extern void delete_GlobalGameJoinRequestedListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalGameInvitationReceivedListener___")]
		public static extern void delete_GlobalGameInvitationReceivedListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalSendInvitationListener___")]
		public static extern void delete_GlobalSendInvitationListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalUserFindListener___")]
		public static extern void delete_GlobalUserFindListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalNotificationListener___")]
		public static extern void delete_GlobalNotificationListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalChatRoomWithUserRetrieveListener___")]
		public static extern void delete_GlobalChatRoomWithUserRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalChatRoomMessagesRetrieveListener___")]
		public static extern void delete_GlobalChatRoomMessagesRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalChatRoomMessageSendListener___")]
		public static extern void delete_GlobalChatRoomMessageSendListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalChatRoomMessagesListener___")]
		public static extern void delete_GlobalChatRoomMessagesListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalTelemetryEventSendListener___")]
		public static extern void delete_GlobalTelemetryEventSendListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalTelemetryEventSendListener___")]
		public static extern void delete_GameServerGlobalTelemetryEventSendListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalUserInformationRetrieveListener___")]
		public static extern void delete_GlobalUserInformationRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalUserInformationRetrieveListener___")]
		public static extern void delete_GameServerGlobalUserInformationRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GlobalRichPresenceRetrieveListener___")]
		public static extern void delete_GlobalRichPresenceRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_GameServerGlobalRichPresenceRetrieveListener___")]
		public static extern void delete_GameServerGlobalRichPresenceRetrieveListener(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_InitParams__SWIG_0___")]
		public static extern IntPtr new_InitParams__SWIG_0([MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4, bool jarg5, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg6);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_InitParams__SWIG_1___")]
		public static extern IntPtr new_InitParams__SWIG_1([MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4, bool jarg5);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_InitParams__SWIG_2___")]
		public static extern IntPtr new_InitParams__SWIG_2([MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg4);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_InitParams__SWIG_3___")]
		public static extern IntPtr new_InitParams__SWIG_3([MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg3);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_new_InitParams__SWIG_4___")]
		public static extern IntPtr new_InitParams__SWIG_4([MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg1, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "Galaxy.Api.GalaxyInstancePINVOKE+UTF8Marshaler")] string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_clientID_set___")]
		public static extern void InitParams_clientID_set(HandleRef jarg1, string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_clientID_get___")]
		public static extern string InitParams_clientID_get(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_clientSecret_set___")]
		public static extern void InitParams_clientSecret_set(HandleRef jarg1, string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_clientSecret_get___")]
		public static extern string InitParams_clientSecret_get(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_configFilePath_set___")]
		public static extern void InitParams_configFilePath_set(HandleRef jarg1, string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_configFilePath_get___")]
		public static extern string InitParams_configFilePath_get(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_storagePath_set___")]
		public static extern void InitParams_storagePath_set(HandleRef jarg1, string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_storagePath_get___")]
		public static extern string InitParams_storagePath_get(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_galaxyModulePath_set___")]
		public static extern void InitParams_galaxyModulePath_set(HandleRef jarg1, string jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_galaxyModulePath_get___")]
		public static extern string InitParams_galaxyModulePath_get(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_loadModule_set___")]
		public static extern void InitParams_loadModule_set(HandleRef jarg1, bool jarg2);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitParams_loadModule_get___")]
		public static extern bool InitParams_loadModule_get(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_delete_InitParams___")]
		public static extern void delete_InitParams(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Init___")]
		public static extern void Init(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_InitGameServer___")]
		public static extern void InitGameServer(HandleRef jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Shutdown___")]
		public static extern void Shutdown(bool jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_User___")]
		public static extern IntPtr User();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Friends___")]
		public static extern IntPtr Friends();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Chat___")]
		public static extern IntPtr Chat();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Matchmaking___")]
		public static extern IntPtr Matchmaking();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Networking___")]
		public static extern IntPtr Networking();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Stats___")]
		public static extern IntPtr Stats();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Utils___")]
		public static extern IntPtr Utils();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Apps___")]
		public static extern IntPtr Apps();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Storage___")]
		public static extern IntPtr Storage();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_CustomNetworking___")]
		public static extern IntPtr CustomNetworking();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Logger___")]
		public static extern IntPtr Logger();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_Telemetry___")]
		public static extern IntPtr Telemetry();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ProcessData___")]
		public static extern void ProcessData();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ShutdownGameServer___")]
		public static extern void ShutdownGameServer();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerUser___")]
		public static extern IntPtr GameServerUser();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerMatchmaking___")]
		public static extern IntPtr GameServerMatchmaking();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerNetworking___")]
		public static extern IntPtr GameServerNetworking();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerUtils___")]
		public static extern IntPtr GameServerUtils();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerTelemetry___")]
		public static extern IntPtr GameServerTelemetry();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerLogger___")]
		public static extern IntPtr GameServerLogger();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ProcessGameServerData___")]
		public static extern void ProcessGameServerData();
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUnauthorizedAccessError_SWIGUpcast___")]
		public static extern IntPtr IUnauthorizedAccessError_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IInvalidArgumentError_SWIGUpcast___")]
		public static extern IntPtr IInvalidArgumentError_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IInvalidStateError_SWIGUpcast___")]
		public static extern IntPtr IInvalidStateError_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRuntimeError_SWIGUpcast___")]
		public static extern IntPtr IRuntimeError_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerOverlayVisibilityChange_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerOverlayVisibilityChange_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerOverlayInitializationStateChange_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerOverlayInitializationStateChange_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerGogServicesConnectionState_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerGogServicesConnectionState_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerOperationalStateChange_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerOperationalStateChange_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerAuth_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerAuth_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerOtherSessionStart_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerOtherSessionStart_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerUserData_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerUserData_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerSpecificUserData_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerSpecificUserData_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerEncryptedAppTicket_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerEncryptedAppTicket_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerAccessToken_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerAccessToken_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyList_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLobbyList_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyCreated_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLobbyCreated_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyEntered_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLobbyEntered_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyLeft_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLobbyLeft_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyData_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLobbyData_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyDataUpdate_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLobbyDataUpdate_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyMemberDataUpdate_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLobbyMemberDataUpdate_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyDataRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLobbyDataRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyMemberState_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLobbyMemberState_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyOwnerChange_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLobbyOwnerChange_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLobbyMessage_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLobbyMessage_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerUserStatsAndAchievementsRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerStatsAndAchievementsStore_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerStatsAndAchievementsStore_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerAchievementChange_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerAchievementChange_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLeaderboardsRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLeaderboardsRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLeaderboardEntriesRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLeaderboardEntriesRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLeaderboardScoreUpdate_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLeaderboardScoreUpdate_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerLeaderboardRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerLeaderboardRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerUserTimePlayedRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerUserTimePlayedRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFileShare_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerFileShare_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerSharedFileDownload_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerSharedFileDownload_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerConnectionOpen_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerConnectionOpen_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerConnectionClose_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerConnectionClose_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerConnectionData_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerConnectionData_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerNetworking_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerNetworking_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerNatTypeDetection_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerNatTypeDetection_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerPersonaDataChanged_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerPersonaDataChanged_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerUserInformationRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerUserInformationRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendList_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerFriendList_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendInvitationSend_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerFriendInvitationSend_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendInvitationListRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerFriendInvitationListRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerSentFriendInvitationListRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerSentFriendInvitationListRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendInvitation_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerFriendInvitation_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendInvitationRespondTo_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerFriendInvitationRespondTo_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendAdd_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerFriendAdd_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerFriendDelete_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerFriendDelete_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerRichPresenceChange_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerRichPresenceChange_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerRichPresence_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerRichPresence_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerRichPresenceRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerRichPresenceRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerGameJoinRequested_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerGameJoinRequested_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerGameInvitationReceived_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerGameInvitationReceived_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerSendInvitation_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerSendInvitation_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerNotification_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerNotification_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerUserFind_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerUserFind_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerChatRoomWithUserRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerChatRoomWithUserRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerChatRoomMessagesRetrieve_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerChatRoomMessagesRetrieve_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerChatRoomMessageSend_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerChatRoomMessageSend_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerChatRoomMessages_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerChatRoomMessages_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GalaxyTypeAwareListenerTelemetryEventSend_SWIGUpcast___")]
		public static extern IntPtr GalaxyTypeAwareListenerTelemetryEventSend_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOverlayVisibilityChangeListener_SWIGUpcast___")]
		public static extern IntPtr IOverlayVisibilityChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOverlayInitializationStateChangeListener_SWIGUpcast___")]
		public static extern IntPtr IOverlayInitializationStateChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INotificationListener_SWIGUpcast___")]
		public static extern IntPtr INotificationListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IGogServicesConnectionStateListener_SWIGUpcast___")]
		public static extern IntPtr IGogServicesConnectionStateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IAuthListener_SWIGUpcast___")]
		public static extern IntPtr IAuthListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOtherSessionStartListener_SWIGUpcast___")]
		public static extern IntPtr IOtherSessionStartListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IOperationalStateChangeListener_SWIGUpcast___")]
		public static extern IntPtr IOperationalStateChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserDataListener_SWIGUpcast___")]
		public static extern IntPtr IUserDataListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISpecificUserDataListener_SWIGUpcast___")]
		public static extern IntPtr ISpecificUserDataListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IEncryptedAppTicketListener_SWIGUpcast___")]
		public static extern IntPtr IEncryptedAppTicketListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IAccessTokenListener_SWIGUpcast___")]
		public static extern IntPtr IAccessTokenListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IPersonaDataChangedListener_SWIGUpcast___")]
		public static extern IntPtr IPersonaDataChangedListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserInformationRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr IUserInformationRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendListListener_SWIGUpcast___")]
		public static extern IntPtr IFriendListListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationSendListener_SWIGUpcast___")]
		public static extern IntPtr IFriendInvitationSendListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationListRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr IFriendInvitationListRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISentFriendInvitationListRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr ISentFriendInvitationListRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationListener_SWIGUpcast___")]
		public static extern IntPtr IFriendInvitationListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendInvitationRespondToListener_SWIGUpcast___")]
		public static extern IntPtr IFriendInvitationRespondToListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendAddListener_SWIGUpcast___")]
		public static extern IntPtr IFriendAddListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFriendDeleteListener_SWIGUpcast___")]
		public static extern IntPtr IFriendDeleteListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRichPresenceChangeListener_SWIGUpcast___")]
		public static extern IntPtr IRichPresenceChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRichPresenceListener_SWIGUpcast___")]
		public static extern IntPtr IRichPresenceListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IRichPresenceRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr IRichPresenceRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IGameJoinRequestedListener_SWIGUpcast___")]
		public static extern IntPtr IGameJoinRequestedListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IGameInvitationReceivedListener_SWIGUpcast___")]
		public static extern IntPtr IGameInvitationReceivedListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISendInvitationListener_SWIGUpcast___")]
		public static extern IntPtr ISendInvitationListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserFindListener_SWIGUpcast___")]
		public static extern IntPtr IUserFindListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserStatsAndAchievementsRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr IUserStatsAndAchievementsRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IStatsAndAchievementsStoreListener_SWIGUpcast___")]
		public static extern IntPtr IStatsAndAchievementsStoreListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IAchievementChangeListener_SWIGUpcast___")]
		public static extern IntPtr IAchievementChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardsRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr ILeaderboardsRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardEntriesRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr ILeaderboardEntriesRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardScoreUpdateListener_SWIGUpcast___")]
		public static extern IntPtr ILeaderboardScoreUpdateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILeaderboardRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr ILeaderboardRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IUserTimePlayedRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr IUserTimePlayedRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IFileShareListener_SWIGUpcast___")]
		public static extern IntPtr IFileShareListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ISharedFileDownloadListener_SWIGUpcast___")]
		public static extern IntPtr ISharedFileDownloadListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IConnectionOpenListener_SWIGUpcast___")]
		public static extern IntPtr IConnectionOpenListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IConnectionCloseListener_SWIGUpcast___")]
		public static extern IntPtr IConnectionCloseListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IConnectionDataListener_SWIGUpcast___")]
		public static extern IntPtr IConnectionDataListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INetworkingListener_SWIGUpcast___")]
		public static extern IntPtr INetworkingListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_INatTypeDetectionListener_SWIGUpcast___")]
		public static extern IntPtr INatTypeDetectionListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyListListener_SWIGUpcast___")]
		public static extern IntPtr ILobbyListListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyCreatedListener_SWIGUpcast___")]
		public static extern IntPtr ILobbyCreatedListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyEnteredListener_SWIGUpcast___")]
		public static extern IntPtr ILobbyEnteredListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyLeftListener_SWIGUpcast___")]
		public static extern IntPtr ILobbyLeftListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyDataListener_SWIGUpcast___")]
		public static extern IntPtr ILobbyDataListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyDataUpdateListener_SWIGUpcast___")]
		public static extern IntPtr ILobbyDataUpdateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyMemberDataUpdateListener_SWIGUpcast___")]
		public static extern IntPtr ILobbyMemberDataUpdateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyDataRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr ILobbyDataRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyMemberStateListener_SWIGUpcast___")]
		public static extern IntPtr ILobbyMemberStateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyOwnerChangeListener_SWIGUpcast___")]
		public static extern IntPtr ILobbyOwnerChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ILobbyMessageListener_SWIGUpcast___")]
		public static extern IntPtr ILobbyMessageListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomWithUserRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr IChatRoomWithUserRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomMessageSendListener_SWIGUpcast___")]
		public static extern IntPtr IChatRoomMessageSendListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomMessagesListener_SWIGUpcast___")]
		public static extern IntPtr IChatRoomMessagesListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_IChatRoomMessagesRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr IChatRoomMessagesRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_ITelemetryEventSendListener_SWIGUpcast___")]
		public static extern IntPtr ITelemetryEventSendListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalOverlayVisibilityChangeListener_SWIGUpcast___")]
		public static extern IntPtr GlobalOverlayVisibilityChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalOverlayInitializationStateChangeListener_SWIGUpcast___")]
		public static extern IntPtr GlobalOverlayInitializationStateChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalGogServicesConnectionStateListener_SWIGUpcast___")]
		public static extern IntPtr GlobalGogServicesConnectionStateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalGogServicesConnectionStateListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalGogServicesConnectionStateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalAuthListener_SWIGUpcast___")]
		public static extern IntPtr GlobalAuthListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalAuthListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalAuthListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalOtherSessionStartListener_SWIGUpcast___")]
		public static extern IntPtr GlobalOtherSessionStartListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalOperationalStateChangeListener_SWIGUpcast___")]
		public static extern IntPtr GlobalOperationalStateChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalOperationalStateChangeListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalOperationalStateChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalUserDataListener_SWIGUpcast___")]
		public static extern IntPtr GlobalUserDataListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalSpecificUserDataListener_SWIGUpcast___")]
		public static extern IntPtr GlobalSpecificUserDataListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalSpecificUserDataListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalSpecificUserDataListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalEncryptedAppTicketListener_SWIGUpcast___")]
		public static extern IntPtr GlobalEncryptedAppTicketListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalEncryptedAppTicketListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalEncryptedAppTicketListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalAccessTokenListener_SWIGUpcast___")]
		public static extern IntPtr GlobalAccessTokenListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalAccessTokenListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalAccessTokenListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLobbyListListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLobbyListListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLobbyCreatedListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLobbyCreatedListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalLobbyCreatedListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalLobbyCreatedListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLobbyEnteredListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLobbyEnteredListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalLobbyEnteredListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalLobbyEnteredListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLobbyLeftListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLobbyLeftListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalLobbyLeftListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalLobbyLeftListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLobbyDataListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLobbyDataListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalLobbyDataListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalLobbyDataListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLobbyDataUpdateListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLobbyDataUpdateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalLobbyDataUpdateListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalLobbyDataUpdateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLobbyMemberDataUpdateListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLobbyMemberDataUpdateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalLobbyMemberDataUpdateListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalLobbyMemberDataUpdateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLobbyDataRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLobbyDataRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalLobbyDataRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalLobbyDataRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLobbyMemberStateListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLobbyMemberStateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalLobbyMemberStateListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalLobbyMemberStateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLobbyOwnerChangeListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLobbyOwnerChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLobbyMessageListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLobbyMessageListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalLobbyMessageListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalLobbyMessageListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalUserStatsAndAchievementsRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalUserStatsAndAchievementsRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalStatsAndAchievementsStoreListener_SWIGUpcast___")]
		public static extern IntPtr GlobalStatsAndAchievementsStoreListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalAchievementChangeListener_SWIGUpcast___")]
		public static extern IntPtr GlobalAchievementChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLeaderboardsRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLeaderboardsRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLeaderboardEntriesRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLeaderboardEntriesRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLeaderboardScoreUpdateListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLeaderboardScoreUpdateListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalLeaderboardRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalLeaderboardRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalUserTimePlayedRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalUserTimePlayedRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalFileShareListener_SWIGUpcast___")]
		public static extern IntPtr GlobalFileShareListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalSharedFileDownloadListener_SWIGUpcast___")]
		public static extern IntPtr GlobalSharedFileDownloadListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalConnectionOpenListener_SWIGUpcast___")]
		public static extern IntPtr GlobalConnectionOpenListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalConnectionCloseListener_SWIGUpcast___")]
		public static extern IntPtr GlobalConnectionCloseListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalConnectionDataListener_SWIGUpcast___")]
		public static extern IntPtr GlobalConnectionDataListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalNetworkingListener_SWIGUpcast___")]
		public static extern IntPtr GlobalNetworkingListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalNetworkingListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalNetworkingListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalNatTypeDetectionListener_SWIGUpcast___")]
		public static extern IntPtr GlobalNatTypeDetectionListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalPersonaDataChangedListener_SWIGUpcast___")]
		public static extern IntPtr GlobalPersonaDataChangedListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalFriendListListener_SWIGUpcast___")]
		public static extern IntPtr GlobalFriendListListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalFriendInvitationSendListener_SWIGUpcast___")]
		public static extern IntPtr GlobalFriendInvitationSendListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalFriendInvitationListRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalFriendInvitationListRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalSentFriendInvitationListRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalSentFriendInvitationListRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalFriendInvitationListener_SWIGUpcast___")]
		public static extern IntPtr GlobalFriendInvitationListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalFriendInvitationRespondToListener_SWIGUpcast___")]
		public static extern IntPtr GlobalFriendInvitationRespondToListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalFriendAddListener_SWIGUpcast___")]
		public static extern IntPtr GlobalFriendAddListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalFriendDeleteListener_SWIGUpcast___")]
		public static extern IntPtr GlobalFriendDeleteListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalRichPresenceChangeListener_SWIGUpcast___")]
		public static extern IntPtr GlobalRichPresenceChangeListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalRichPresenceListener_SWIGUpcast___")]
		public static extern IntPtr GlobalRichPresenceListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalGameJoinRequestedListener_SWIGUpcast___")]
		public static extern IntPtr GlobalGameJoinRequestedListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalGameInvitationReceivedListener_SWIGUpcast___")]
		public static extern IntPtr GlobalGameInvitationReceivedListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalSendInvitationListener_SWIGUpcast___")]
		public static extern IntPtr GlobalSendInvitationListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalUserFindListener_SWIGUpcast___")]
		public static extern IntPtr GlobalUserFindListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalNotificationListener_SWIGUpcast___")]
		public static extern IntPtr GlobalNotificationListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalChatRoomWithUserRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalChatRoomWithUserRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalChatRoomMessagesRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalChatRoomMessagesRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalChatRoomMessageSendListener_SWIGUpcast___")]
		public static extern IntPtr GlobalChatRoomMessageSendListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalChatRoomMessagesListener_SWIGUpcast___")]
		public static extern IntPtr GlobalChatRoomMessagesListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalTelemetryEventSendListener_SWIGUpcast___")]
		public static extern IntPtr GlobalTelemetryEventSendListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalTelemetryEventSendListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalTelemetryEventSendListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalUserInformationRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalUserInformationRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalUserInformationRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalUserInformationRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GlobalRichPresenceRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GlobalRichPresenceRetrieveListener_SWIGUpcast(IntPtr jarg1);
	
		[DllImport("GalaxyCSharpGlue", EntryPoint = "CSharp_GalaxyfApi_GameServerGlobalRichPresenceRetrieveListener_SWIGUpcast___")]
		public static extern IntPtr GameServerGlobalRichPresenceRetrieveListener_SWIGUpcast(IntPtr jarg1);
	}
}