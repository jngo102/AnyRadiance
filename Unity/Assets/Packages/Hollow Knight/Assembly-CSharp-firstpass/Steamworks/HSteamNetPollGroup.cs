using System;

namespace Steamworks
{
	
	[Serializable]
	public struct HSteamNetPollGroup : IEquatable<HSteamNetPollGroup>, IComparable<HSteamNetPollGroup>
	{
		public static readonly HSteamNetPollGroup Invalid = new HSteamNetPollGroup(0u);
	
		public uint m_HSteamNetPollGroup;
	
		public HSteamNetPollGroup(uint value)
		{
			m_HSteamNetPollGroup = value;
		}
	
		public override string ToString()
		{
			return m_HSteamNetPollGroup.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is HSteamNetPollGroup)
			{
				return this == (HSteamNetPollGroup)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_HSteamNetPollGroup.GetHashCode();
		}
	
		public static bool operator ==(HSteamNetPollGroup x, HSteamNetPollGroup y)
		{
			return x.m_HSteamNetPollGroup == y.m_HSteamNetPollGroup;
		}
	
		public static bool operator !=(HSteamNetPollGroup x, HSteamNetPollGroup y)
		{
			return !(x == y);
		}
	
		public static explicit operator HSteamNetPollGroup(uint value)
		{
			return new HSteamNetPollGroup(value);
		}
	
		public static explicit operator uint(HSteamNetPollGroup that)
		{
			return that.m_HSteamNetPollGroup;
		}
	
		public bool Equals(HSteamNetPollGroup other)
		{
			return m_HSteamNetPollGroup == other.m_HSteamNetPollGroup;
		}
	
		public int CompareTo(HSteamNetPollGroup other)
		{
			return m_HSteamNetPollGroup.CompareTo(other.m_HSteamNetPollGroup);
		}
	}
}