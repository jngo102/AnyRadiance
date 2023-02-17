using System;

namespace Steamworks
{
	
	[Serializable]
	public struct servernetadr_t
	{
		private ushort m_usConnectionPort;
	
		private ushort m_usQueryPort;
	
		private uint m_unIP;
	
		public void Init(uint ip, ushort usQueryPort, ushort usConnectionPort)
		{
			m_unIP = ip;
			m_usQueryPort = usQueryPort;
			m_usConnectionPort = usConnectionPort;
		}
	
		public ushort GetQueryPort()
		{
			return m_usQueryPort;
		}
	
		public void SetQueryPort(ushort usPort)
		{
			m_usQueryPort = usPort;
		}
	
		public ushort GetConnectionPort()
		{
			return m_usConnectionPort;
		}
	
		public void SetConnectionPort(ushort usPort)
		{
			m_usConnectionPort = usPort;
		}
	
		public uint GetIP()
		{
			return m_unIP;
		}
	
		public void SetIP(uint unIP)
		{
			m_unIP = unIP;
		}
	
		public string GetConnectionAddressString()
		{
			return ToString(m_unIP, m_usConnectionPort);
		}
	
		public string GetQueryAddressString()
		{
			return ToString(m_unIP, m_usQueryPort);
		}
	
		public static string ToString(uint unIP, ushort usPort)
		{
			return $"{(ulong)(unIP >> 24) & 0xFFuL}.{(ulong)(unIP >> 16) & 0xFFuL}.{(ulong)(unIP >> 8) & 0xFFuL}.{(ulong)unIP & 0xFFuL}:{usPort}";
		}
	
		public static bool operator <(servernetadr_t x, servernetadr_t y)
		{
			if (x.m_unIP >= y.m_unIP)
			{
				if (x.m_unIP == y.m_unIP)
				{
					return x.m_usQueryPort < y.m_usQueryPort;
				}
				return false;
			}
			return true;
		}
	
		public static bool operator >(servernetadr_t x, servernetadr_t y)
		{
			if (x.m_unIP <= y.m_unIP)
			{
				if (x.m_unIP == y.m_unIP)
				{
					return x.m_usQueryPort > y.m_usQueryPort;
				}
				return false;
			}
			return true;
		}
	
		public override bool Equals(object other)
		{
			if (other is servernetadr_t)
			{
				return this == (servernetadr_t)other;
			}
			return false;
		}
	
		public override int GetHashCode()
		{
			return m_unIP.GetHashCode() + m_usQueryPort.GetHashCode() + m_usConnectionPort.GetHashCode();
		}
	
		public static bool operator ==(servernetadr_t x, servernetadr_t y)
		{
			if (x.m_unIP == y.m_unIP && x.m_usQueryPort == y.m_usQueryPort)
			{
				return x.m_usConnectionPort == y.m_usConnectionPort;
			}
			return false;
		}
	
		public static bool operator !=(servernetadr_t x, servernetadr_t y)
		{
			return !(x == y);
		}
	
		public bool Equals(servernetadr_t other)
		{
			if (m_unIP == other.m_unIP && m_usQueryPort == other.m_usQueryPort)
			{
				return m_usConnectionPort == other.m_usConnectionPort;
			}
			return false;
		}
	
		public int CompareTo(servernetadr_t other)
		{
			return m_unIP.CompareTo(other.m_unIP) + m_usQueryPort.CompareTo(other.m_usQueryPort) + m_usConnectionPort.CompareTo(other.m_usConnectionPort);
		}
	}
}