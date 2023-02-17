using System;

namespace Steamworks
{
	
	[Serializable]
	public struct InputAnalogActionHandle_t : IEquatable<InputAnalogActionHandle_t>, IComparable<InputAnalogActionHandle_t>
	{
		public ulong m_InputAnalogActionHandle;
	
		public InputAnalogActionHandle_t(ulong value)
		{
			m_InputAnalogActionHandle = value;
		}
	
		public override string ToString()
		{
			return m_InputAnalogActionHandle.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is InputAnalogActionHandle_t)
			{
				return this == (InputAnalogActionHandle_t)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_InputAnalogActionHandle.GetHashCode();
		}
	
		public static bool operator ==(InputAnalogActionHandle_t x, InputAnalogActionHandle_t y)
		{
			return x.m_InputAnalogActionHandle == y.m_InputAnalogActionHandle;
		}
	
		public static bool operator !=(InputAnalogActionHandle_t x, InputAnalogActionHandle_t y)
		{
			return !(x == y);
		}
	
		public static explicit operator InputAnalogActionHandle_t(ulong value)
		{
			return new InputAnalogActionHandle_t(value);
		}
	
		public static explicit operator ulong(InputAnalogActionHandle_t that)
		{
			return that.m_InputAnalogActionHandle;
		}
	
		public bool Equals(InputAnalogActionHandle_t other)
		{
			return m_InputAnalogActionHandle == other.m_InputAnalogActionHandle;
		}
	
		public int CompareTo(InputAnalogActionHandle_t other)
		{
			return m_InputAnalogActionHandle.CompareTo(other.m_InputAnalogActionHandle);
		}
	}
}