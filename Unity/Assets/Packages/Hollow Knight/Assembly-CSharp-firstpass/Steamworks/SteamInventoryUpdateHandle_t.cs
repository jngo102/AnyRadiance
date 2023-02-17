using System;

namespace Steamworks
{
	
	[Serializable]
	public struct SteamInventoryUpdateHandle_t : IEquatable<SteamInventoryUpdateHandle_t>, IComparable<SteamInventoryUpdateHandle_t>
	{
		public static readonly SteamInventoryUpdateHandle_t Invalid = new SteamInventoryUpdateHandle_t(ulong.MaxValue);
	
		public ulong m_SteamInventoryUpdateHandle;
	
		public SteamInventoryUpdateHandle_t(ulong value)
		{
			m_SteamInventoryUpdateHandle = value;
		}
	
		public override string ToString()
		{
			return m_SteamInventoryUpdateHandle.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is SteamInventoryUpdateHandle_t)
			{
				return this == (SteamInventoryUpdateHandle_t)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_SteamInventoryUpdateHandle.GetHashCode();
		}
	
		public static bool operator ==(SteamInventoryUpdateHandle_t x, SteamInventoryUpdateHandle_t y)
		{
			return x.m_SteamInventoryUpdateHandle == y.m_SteamInventoryUpdateHandle;
		}
	
		public static bool operator !=(SteamInventoryUpdateHandle_t x, SteamInventoryUpdateHandle_t y)
		{
			return !(x == y);
		}
	
		public static explicit operator SteamInventoryUpdateHandle_t(ulong value)
		{
			return new SteamInventoryUpdateHandle_t(value);
		}
	
		public static explicit operator ulong(SteamInventoryUpdateHandle_t that)
		{
			return that.m_SteamInventoryUpdateHandle;
		}
	
		public bool Equals(SteamInventoryUpdateHandle_t other)
		{
			return m_SteamInventoryUpdateHandle == other.m_SteamInventoryUpdateHandle;
		}
	
		public int CompareTo(SteamInventoryUpdateHandle_t other)
		{
			return m_SteamInventoryUpdateHandle.CompareTo(other.m_SteamInventoryUpdateHandle);
		}
	}
}