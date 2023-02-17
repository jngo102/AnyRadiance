using System;

namespace Steamworks
{
	
	[Serializable]
	public struct HSteamListenSocket : IEquatable<HSteamListenSocket>, IComparable<HSteamListenSocket>
	{
		public static readonly HSteamListenSocket Invalid = new HSteamListenSocket(0u);
	
		public uint m_HSteamListenSocket;
	
		public HSteamListenSocket(uint value)
		{
			m_HSteamListenSocket = value;
		}
	
		public override string ToString()
		{
			return m_HSteamListenSocket.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is HSteamListenSocket)
			{
				return this == (HSteamListenSocket)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_HSteamListenSocket.GetHashCode();
		}
	
		public static bool operator ==(HSteamListenSocket x, HSteamListenSocket y)
		{
			return x.m_HSteamListenSocket == y.m_HSteamListenSocket;
		}
	
		public static bool operator !=(HSteamListenSocket x, HSteamListenSocket y)
		{
			return !(x == y);
		}
	
		public static explicit operator HSteamListenSocket(uint value)
		{
			return new HSteamListenSocket(value);
		}
	
		public static explicit operator uint(HSteamListenSocket that)
		{
			return that.m_HSteamListenSocket;
		}
	
		public bool Equals(HSteamListenSocket other)
		{
			return m_HSteamListenSocket == other.m_HSteamListenSocket;
		}
	
		public int CompareTo(HSteamListenSocket other)
		{
			return m_HSteamListenSocket.CompareTo(other.m_HSteamListenSocket);
		}
	}
}