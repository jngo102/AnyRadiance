using System;

namespace Steamworks
{
	
	[Serializable]
	public struct RemotePlaySessionID_t : IEquatable<RemotePlaySessionID_t>, IComparable<RemotePlaySessionID_t>
	{
		public uint m_RemotePlaySessionID;
	
		public RemotePlaySessionID_t(uint value)
		{
			m_RemotePlaySessionID = value;
		}
	
		public override string ToString()
		{
			return m_RemotePlaySessionID.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is RemotePlaySessionID_t)
			{
				return this == (RemotePlaySessionID_t)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_RemotePlaySessionID.GetHashCode();
		}
	
		public static bool operator ==(RemotePlaySessionID_t x, RemotePlaySessionID_t y)
		{
			return x.m_RemotePlaySessionID == y.m_RemotePlaySessionID;
		}
	
		public static bool operator !=(RemotePlaySessionID_t x, RemotePlaySessionID_t y)
		{
			return !(x == y);
		}
	
		public static explicit operator RemotePlaySessionID_t(uint value)
		{
			return new RemotePlaySessionID_t(value);
		}
	
		public static explicit operator uint(RemotePlaySessionID_t that)
		{
			return that.m_RemotePlaySessionID;
		}
	
		public bool Equals(RemotePlaySessionID_t other)
		{
			return m_RemotePlaySessionID == other.m_RemotePlaySessionID;
		}
	
		public int CompareTo(RemotePlaySessionID_t other)
		{
			return m_RemotePlaySessionID.CompareTo(other.m_RemotePlaySessionID);
		}
	}
}