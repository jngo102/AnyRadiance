using System;

namespace Steamworks
{
	
	[Serializable]
	public struct HSteamNetConnection : IEquatable<HSteamNetConnection>, IComparable<HSteamNetConnection>
	{
		public static readonly HSteamNetConnection Invalid = new HSteamNetConnection(0u);
	
		public uint m_HSteamNetConnection;
	
		public HSteamNetConnection(uint value)
		{
			m_HSteamNetConnection = value;
		}
	
		public override string ToString()
		{
			return m_HSteamNetConnection.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is HSteamNetConnection)
			{
				return this == (HSteamNetConnection)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_HSteamNetConnection.GetHashCode();
		}
	
		public static bool operator ==(HSteamNetConnection x, HSteamNetConnection y)
		{
			return x.m_HSteamNetConnection == y.m_HSteamNetConnection;
		}
	
		public static bool operator !=(HSteamNetConnection x, HSteamNetConnection y)
		{
			return !(x == y);
		}
	
		public static explicit operator HSteamNetConnection(uint value)
		{
			return new HSteamNetConnection(value);
		}
	
		public static explicit operator uint(HSteamNetConnection that)
		{
			return that.m_HSteamNetConnection;
		}
	
		public bool Equals(HSteamNetConnection other)
		{
			return m_HSteamNetConnection == other.m_HSteamNetConnection;
		}
	
		public int CompareTo(HSteamNetConnection other)
		{
			return m_HSteamNetConnection.CompareTo(other.m_HSteamNetConnection);
		}
	}
}