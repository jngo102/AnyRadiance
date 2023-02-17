using System;

namespace Steamworks
{
	
	[Serializable]
	public struct SteamNetworkingPOPID : IEquatable<SteamNetworkingPOPID>, IComparable<SteamNetworkingPOPID>
	{
		public uint m_SteamNetworkingPOPID;
	
		public SteamNetworkingPOPID(uint value)
		{
			m_SteamNetworkingPOPID = value;
		}
	
		public override string ToString()
		{
			return m_SteamNetworkingPOPID.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is SteamNetworkingPOPID)
			{
				return this == (SteamNetworkingPOPID)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_SteamNetworkingPOPID.GetHashCode();
		}
	
		public static bool operator ==(SteamNetworkingPOPID x, SteamNetworkingPOPID y)
		{
			return x.m_SteamNetworkingPOPID == y.m_SteamNetworkingPOPID;
		}
	
		public static bool operator !=(SteamNetworkingPOPID x, SteamNetworkingPOPID y)
		{
			return !(x == y);
		}
	
		public static explicit operator SteamNetworkingPOPID(uint value)
		{
			return new SteamNetworkingPOPID(value);
		}
	
		public static explicit operator uint(SteamNetworkingPOPID that)
		{
			return that.m_SteamNetworkingPOPID;
		}
	
		public bool Equals(SteamNetworkingPOPID other)
		{
			return m_SteamNetworkingPOPID == other.m_SteamNetworkingPOPID;
		}
	
		public int CompareTo(SteamNetworkingPOPID other)
		{
			return m_SteamNetworkingPOPID.CompareTo(other.m_SteamNetworkingPOPID);
		}
	}
}