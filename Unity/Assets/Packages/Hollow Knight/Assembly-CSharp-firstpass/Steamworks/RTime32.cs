using System;

namespace Steamworks
{
	
	[Serializable]
	public struct RTime32 : IEquatable<RTime32>, IComparable<RTime32>
	{
		public uint m_RTime32;
	
		public RTime32(uint value)
		{
			m_RTime32 = value;
		}
	
		public override string ToString()
		{
			return m_RTime32.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is RTime32)
			{
				return this == (RTime32)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_RTime32.GetHashCode();
		}
	
		public static bool operator ==(RTime32 x, RTime32 y)
		{
			return x.m_RTime32 == y.m_RTime32;
		}
	
		public static bool operator !=(RTime32 x, RTime32 y)
		{
			return !(x == y);
		}
	
		public static explicit operator RTime32(uint value)
		{
			return new RTime32(value);
		}
	
		public static explicit operator uint(RTime32 that)
		{
			return that.m_RTime32;
		}
	
		public bool Equals(RTime32 other)
		{
			return m_RTime32 == other.m_RTime32;
		}
	
		public int CompareTo(RTime32 other)
		{
			return m_RTime32.CompareTo(other.m_RTime32);
		}
	}
}