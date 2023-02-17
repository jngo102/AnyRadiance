using System;

namespace Steamworks
{
	
	[Serializable]
	public struct InputActionSetHandle_t : IEquatable<InputActionSetHandle_t>, IComparable<InputActionSetHandle_t>
	{
		public ulong m_InputActionSetHandle;
	
		public InputActionSetHandle_t(ulong value)
		{
			m_InputActionSetHandle = value;
		}
	
		public override string ToString()
		{
			return m_InputActionSetHandle.ToString();
		}
	
		public override bool Equals(object other)
		{
			if (other is InputActionSetHandle_t)
			{
				return this == (InputActionSetHandle_t)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_InputActionSetHandle.GetHashCode();
		}
	
		public static bool operator ==(InputActionSetHandle_t x, InputActionSetHandle_t y)
		{
			return x.m_InputActionSetHandle == y.m_InputActionSetHandle;
		}
	
		public static bool operator !=(InputActionSetHandle_t x, InputActionSetHandle_t y)
		{
			return !(x == y);
		}
	
		public static explicit operator InputActionSetHandle_t(ulong value)
		{
			return new InputActionSetHandle_t(value);
		}
	
		public static explicit operator ulong(InputActionSetHandle_t that)
		{
			return that.m_InputActionSetHandle;
		}
	
		public bool Equals(InputActionSetHandle_t other)
		{
			return m_InputActionSetHandle == other.m_InputActionSetHandle;
		}
	
		public int CompareTo(InputActionSetHandle_t other)
		{
			return m_InputActionSetHandle.CompareTo(other.m_InputActionSetHandle);
		}
	}
}