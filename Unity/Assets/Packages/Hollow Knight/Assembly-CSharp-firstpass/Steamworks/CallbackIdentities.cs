using System;

namespace Steamworks
{
	
	internal class CallbackIdentities
	{
		public static int GetCallbackIdentity(Type callbackStruct)
		{
			object[] customAttributes = callbackStruct.GetCustomAttributes(typeof(CallbackIdentityAttribute), inherit: false);
			int num = 0;
			if (num < customAttributes.Length)
			{
				return ((CallbackIdentityAttribute)customAttributes[num]).Identity;
			}
			throw new Exception("Callback number not found for struct " + callbackStruct);
		}
	}
}