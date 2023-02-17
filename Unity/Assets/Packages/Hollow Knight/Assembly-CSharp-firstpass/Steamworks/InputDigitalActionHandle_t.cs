using System;

namespace Steamworks
{
	
	[Serializable]
	public struct InputDigitalActionHandle_t : IEquatable<InputDigitalActionHandle_t>, IComparable<InputDigitalActionHandle_t>
	{
		public ulong m_InputDigitalActionHandle;
	
		public InputDigitalActionHandle_t(ulong value)
		{
			m_InputDigitalActionHandle = value;
		}
	
		public override string ToString()
		{
			return m_InputDigitalActionHandle.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is InputDigitalActionHandle_t)
			{
				return this == (InputDigitalActionHandle_t)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_InputDigitalActionHandle.GetHashCode();
		}
	
		public static bool operator ==(InputDigitalActionHandle_t x, InputDigitalActionHandle_t y)
		{
			return x.m_InputDigitalActionHandle == y.m_InputDigitalActionHandle;
		}
	
		public static bool operator !=(InputDigitalActionHandle_t x, InputDigitalActionHandle_t y)
		{
			return !(x == y);
		}
	
		public static explicit operator InputDigitalActionHandle_t(ulong value)
		{
			return new InputDigitalActionHandle_t(value);
		}
	
		public static explicit operator ulong(InputDigitalActionHandle_t that)
		{
			return that.m_InputDigitalActionHandle;
		}
	
		public bool Equals(InputDigitalActionHandle_t other)
		{
			return m_InputDigitalActionHandle == other.m_InputDigitalActionHandle;
		}
	
		public int CompareTo(InputDigitalActionHandle_t other)
		{
			return m_InputDigitalActionHandle.CompareTo(other.m_InputDigitalActionHandle);
		}
	}
}