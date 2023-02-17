using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	
	public class IUser : IDisposable
	{
		private HandleRef swigCPtr;
	
		protected bool swigCMemOwn;
	
		internal IUser(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}
	
		internal static HandleRef getCPtr(IUser obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}
	
		~IUser()
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
						GalaxyInstancePINVOKE.delete_IUser(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}
	
		public virtual bool SignedIn()
		{
			bool result = GalaxyInstancePINVOKE.IUser_SignedIn(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual GalaxyID GetGalaxyID()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IUser_GetGalaxyID(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID result = null;
			if (intPtr != IntPtr.Zero)
			{
				result = new GalaxyID(intPtr, cMemoryOwn: true);
			}
			return result;
		}
	
		public virtual void SignInCredentials(string login, string password, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInCredentials__SWIG_0(swigCPtr, login, password, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInCredentials(string login, string password)
		{
			GalaxyInstancePINVOKE.IUser_SignInCredentials__SWIG_1(swigCPtr, login, password);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInToken(string refreshToken, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInToken__SWIG_0(swigCPtr, refreshToken, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInToken(string refreshToken)
		{
			GalaxyInstancePINVOKE.IUser_SignInToken__SWIG_1(swigCPtr, refreshToken);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInLauncher(IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInLauncher__SWIG_0(swigCPtr, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInLauncher()
		{
			GalaxyInstancePINVOKE.IUser_SignInLauncher__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInSteam(byte[] steamAppTicket, uint steamAppTicketSize, string personaName, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInSteam__SWIG_0(swigCPtr, steamAppTicket, steamAppTicketSize, personaName, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInSteam(byte[] steamAppTicket, uint steamAppTicketSize, string personaName)
		{
			GalaxyInstancePINVOKE.IUser_SignInSteam__SWIG_1(swigCPtr, steamAppTicket, steamAppTicketSize, personaName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInGalaxy(bool requireOnline, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInGalaxy__SWIG_0(swigCPtr, requireOnline, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInGalaxy(bool requireOnline)
		{
			GalaxyInstancePINVOKE.IUser_SignInGalaxy__SWIG_1(swigCPtr, requireOnline);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInGalaxy()
		{
			GalaxyInstancePINVOKE.IUser_SignInGalaxy__SWIG_2(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInUWP(IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInUWP__SWIG_0(swigCPtr, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInUWP()
		{
			GalaxyInstancePINVOKE.IUser_SignInUWP__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInPS4(string ps4ClientID, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInPS4__SWIG_0(swigCPtr, ps4ClientID, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInPS4(string ps4ClientID)
		{
			GalaxyInstancePINVOKE.IUser_SignInPS4__SWIG_1(swigCPtr, ps4ClientID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInXB1(string xboxOneUserID, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInXB1__SWIG_0(swigCPtr, xboxOneUserID, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInXB1(string xboxOneUserID)
		{
			GalaxyInstancePINVOKE.IUser_SignInXB1__SWIG_1(swigCPtr, xboxOneUserID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInXBLive(string token, string signature, string marketplaceID, string locale, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInXBLive__SWIG_0(swigCPtr, token, signature, marketplaceID, locale, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInXBLive(string token, string signature, string marketplaceID, string locale)
		{
			GalaxyInstancePINVOKE.IUser_SignInXBLive__SWIG_1(swigCPtr, token, signature, marketplaceID, locale);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInAnonymous(IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymous__SWIG_0(swigCPtr, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInAnonymous()
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymous__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInAnonymousTelemetry(IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymousTelemetry__SWIG_0(swigCPtr, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInAnonymousTelemetry()
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymousTelemetry__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInServerKey(string serverKey, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInServerKey__SWIG_0(swigCPtr, serverKey, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignInServerKey(string serverKey)
		{
			GalaxyInstancePINVOKE.IUser_SignInServerKey__SWIG_1(swigCPtr, serverKey);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SignOut()
		{
			GalaxyInstancePINVOKE.IUser_SignOut(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestUserData(GalaxyID userID, ISpecificUserDataListener listener)
		{
			GalaxyInstancePINVOKE.IUser_RequestUserData__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID), ISpecificUserDataListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestUserData(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IUser_RequestUserData__SWIG_1(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestUserData()
		{
			GalaxyInstancePINVOKE.IUser_RequestUserData__SWIG_2(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual bool IsUserDataAvailable(GalaxyID userID)
		{
			bool result = GalaxyInstancePINVOKE.IUser_IsUserDataAvailable__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool IsUserDataAvailable()
		{
			bool result = GalaxyInstancePINVOKE.IUser_IsUserDataAvailable__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual string GetUserData(string key, GalaxyID userID)
		{
			string result = GalaxyInstancePINVOKE.IUser_GetUserData__SWIG_0(swigCPtr, key, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual string GetUserData(string key)
		{
			string result = GalaxyInstancePINVOKE.IUser_GetUserData__SWIG_1(swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetUserDataCopy(string key, out string buffer, uint bufferLength, GalaxyID userID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetUserDataCopy__SWIG_0(swigCPtr, key, array, bufferLength, GalaxyID.getCPtr(userID));
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}
	
		public virtual void GetUserDataCopy(string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetUserDataCopy__SWIG_1(swigCPtr, key, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}
	
		public virtual void SetUserData(string key, string value, ISpecificUserDataListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SetUserData__SWIG_0(swigCPtr, key, value, ISpecificUserDataListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void SetUserData(string key, string value)
		{
			GalaxyInstancePINVOKE.IUser_SetUserData__SWIG_1(swigCPtr, key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual uint GetUserDataCount(GalaxyID userID)
		{
			uint result = GalaxyInstancePINVOKE.IUser_GetUserDataCount__SWIG_0(swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual uint GetUserDataCount()
		{
			uint result = GalaxyInstancePINVOKE.IUser_GetUserDataCount__SWIG_1(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool GetUserDataByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength, GalaxyID userID)
		{
			bool result = GalaxyInstancePINVOKE.IUser_GetUserDataByIndex__SWIG_0(swigCPtr, index, key, keyLength, value, valueLength, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool GetUserDataByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			bool result = GalaxyInstancePINVOKE.IUser_GetUserDataByIndex__SWIG_1(swigCPtr, index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void DeleteUserData(string key, ISpecificUserDataListener listener)
		{
			GalaxyInstancePINVOKE.IUser_DeleteUserData__SWIG_0(swigCPtr, key, ISpecificUserDataListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void DeleteUserData(string key)
		{
			GalaxyInstancePINVOKE.IUser_DeleteUserData__SWIG_1(swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual bool IsLoggedOn()
		{
			bool result = GalaxyInstancePINVOKE.IUser_IsLoggedOn(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void RequestEncryptedAppTicket(byte[] data, uint dataSize, IEncryptedAppTicketListener listener)
		{
			GalaxyInstancePINVOKE.IUser_RequestEncryptedAppTicket__SWIG_0(swigCPtr, data, dataSize, IEncryptedAppTicketListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void RequestEncryptedAppTicket(byte[] data, uint dataSize)
		{
			GalaxyInstancePINVOKE.IUser_RequestEncryptedAppTicket__SWIG_1(swigCPtr, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual void GetEncryptedAppTicket(byte[] encryptedAppTicket, uint maxEncryptedAppTicketSize, ref uint currentEncryptedAppTicketSize)
		{
			GalaxyInstancePINVOKE.IUser_GetEncryptedAppTicket(swigCPtr, encryptedAppTicket, maxEncryptedAppTicketSize, ref currentEncryptedAppTicketSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}
	
		public virtual ulong GetSessionID()
		{
			ulong result = GalaxyInstancePINVOKE.IUser_GetSessionID(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual string GetAccessToken()
		{
			string result = GalaxyInstancePINVOKE.IUser_GetAccessToken(swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual void GetAccessTokenCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetAccessTokenCopy(swigCPtr, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}
	
		public virtual bool ReportInvalidAccessToken(string accessToken, string info)
		{
			bool result = GalaxyInstancePINVOKE.IUser_ReportInvalidAccessToken__SWIG_0(swigCPtr, accessToken, info);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	
		public virtual bool ReportInvalidAccessToken(string accessToken)
		{
			bool result = GalaxyInstancePINVOKE.IUser_ReportInvalidAccessToken__SWIG_1(swigCPtr, accessToken);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}