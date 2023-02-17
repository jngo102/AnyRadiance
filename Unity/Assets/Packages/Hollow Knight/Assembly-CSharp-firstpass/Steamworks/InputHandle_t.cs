using System;

namespace Steamworks
{
	
	[Serializable]
	public struct InputHandle_t : IEquatable<InputHandle_t>, IComparable<InputHandle_t>
	{
		public ulong m_InputHandle;
	
		public InputHandle_t(ulong value)
		{
			m_InputHandle = value;
		}
	
		public override string ToString()
		{
			return m_InputHandle.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is InputHandle_t)
			{
				return this == (InputHandle_t)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_InputHandle.GetHashCode();
		}
	
		public static bool operator ==(InputHandle_t x, InputHandle_t y)
		{
			return x.m_InputHandle == y.m_InputHandle;
		}
	
		public static bool operator !=(InputHandle_t x, InputHandle_t y)
		{
			return !(x == y);
		}
	
		public static explicit operator InputHandle_t(ulong value)
		{
			return new InputHandle_t(value);
		}
	
		public static explicit operator ulong(InputHandle_t that)
		{
			return that.m_InputHandle;
		}
	
		public bool Equals(InputHandle_t other)
		{
			return m_InputHandle == other.m_InputHandle;
		}
	
		public int CompareTo(InputHandle_t other)
		{
			return m_InputHandle.CompareTo(other.m_InputHandle);
		}
	}
}