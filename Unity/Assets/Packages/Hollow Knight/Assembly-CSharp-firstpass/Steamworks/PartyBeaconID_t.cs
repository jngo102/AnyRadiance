using System;

namespace Steamworks
{
	
	[Serializable]
	public struct PartyBeaconID_t : IEquatable<PartyBeaconID_t>, IComparable<PartyBeaconID_t>
	{
		public static readonly PartyBeaconID_t Invalid = new PartyBeaconID_t(0uL);
	
		public ulong m_PartyBeaconID;
	
		public PartyBeaconID_t(ulong value)
		{
			m_PartyBeaconID = value;
		}
	
		public override string ToString()
		{
			return m_PartyBeaconID.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is PartyBeaconID_t)
			{
				return this == (PartyBeaconID_t)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_PartyBeaconID.GetHashCode();
		}
	
		public static bool operator ==(PartyBeaconID_t x, PartyBeaconID_t y)
		{
			return x.m_PartyBeaconID == y.m_PartyBeaconID;
		}
	
		public static bool operator !=(PartyBeaconID_t x, PartyBeaconID_t y)
		{
			return !(x == y);
		}
	
		public static explicit operator PartyBeaconID_t(ulong value)
		{
			return new PartyBeaconID_t(value);
		}
	
		public static explicit operator ulong(PartyBeaconID_t that)
		{
			return that.m_PartyBeaconID;
		}
	
		public bool Equals(PartyBeaconID_t other)
		{
			return m_PartyBeaconID == other.m_PartyBeaconID;
		}
	
		public int CompareTo(PartyBeaconID_t other)
		{
			return m_PartyBeaconID.CompareTo(other.m_PartyBeaconID);
		}
	}
}