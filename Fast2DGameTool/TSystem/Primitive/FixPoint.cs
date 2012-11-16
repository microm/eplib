using System;
using System.Collections.Generic;
using System.Text;

namespace Tool.TSystem.Primitive
{
	public struct FixPoint : IEquatable<FixPoint>
	{
		private const int Shift = 15;
		private const int LeftShift = 1 << Shift;
		private int m_value;

		private FixPoint(int value)
		{
			m_value = value;
		}

		public FixPoint(double value)
		{
			m_value = (int)(value * LeftShift);
		}

		public double Value
		{
			get { return (((double)m_value) ) / LeftShift; }
			set { m_value = (int)(value * LeftShift); }
		}

		public static FixPoint operator +(FixPoint lhs, double rhs)
		{
			return lhs + new FixPoint(rhs);
		}

		public static FixPoint operator +(double lhs, FixPoint rhs)
		{
			return new FixPoint(lhs) + rhs;
		}

		public static FixPoint operator +(FixPoint lhs, FixPoint rhs)
		{
			return new FixPoint(lhs.m_value + rhs.m_value);
		}

		public static FixPoint operator -(FixPoint lhs)
		{
			return new FixPoint(-lhs.m_value);
		}

		public static FixPoint operator -(FixPoint lhs, double rhs)
		{
			return lhs - new FixPoint(rhs);
		}

		public static FixPoint operator -(double lhs, FixPoint rhs)
		{
			return new FixPoint(lhs) - rhs;
		}

		public static FixPoint operator -(FixPoint lhs, FixPoint rhs)
		{
			return new FixPoint(lhs.m_value - rhs.m_value);
		}

		public static FixPoint operator *(FixPoint lhs, double rhs)
		{
			return lhs * new FixPoint(rhs);
		}

		public static FixPoint operator *(double lhs, FixPoint rhs)
		{
			return new FixPoint(lhs) * rhs;
		}

		public static FixPoint operator *(FixPoint lhs, FixPoint rhs)
		{
			return new FixPoint((int)(((long)lhs.m_value * rhs.m_value) >> Shift));
		}

		public static FixPoint operator /(FixPoint lhs, double rhs)
		{
			return lhs / new FixPoint(rhs);
		}

		public static FixPoint operator /(double lhs, FixPoint rhs)
		{
			return new FixPoint(lhs) / rhs;
		}

		public static FixPoint operator /(FixPoint lhs, FixPoint rhs)
		{
			return new FixPoint((int)((((long)lhs.m_value) << Shift) / rhs.m_value));
		}

		public static bool operator ==(FixPoint lhs, FixPoint rhs)
		{
			return lhs.m_value == rhs.m_value;
		}

		public static bool operator !=(FixPoint lhs, FixPoint rhs)
		{
			return lhs != rhs;
		}

		public bool Equals(FixPoint fixPoint)
		{
			return m_value == fixPoint.m_value;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is FixPoint)) return false;
			return Equals((FixPoint) obj);
		}

		public override int GetHashCode()
		{
			return m_value;
		}
	}
}
