using System;
using System.Drawing;
using System.IO;
using System.ComponentModel;

namespace Tool.TSystem.Primitive
{
    [TypeConverter(typeof(TPointConverter))]
	public struct Point : IComparable<Point>
	{
		private int m_x;
		private int m_y;

		public Point(int x, int y)
		{
			m_x = x;
			m_y = y;
		}

        public Point(System.Drawing.Point pt)
        {
            m_x = pt.X;
            m_y = pt.Y;
        }
        
		public bool IsIn(Rect rect)
		{
			return rect.Has(this);
		}

		public static Point MaxValue
		{
			get
			{
				return new Point(int.MaxValue, int.MaxValue);				
			}
		}

		public static Point MinValue
		{
			get
			{
				return new Point(int.MinValue, int.MinValue);
			}
		}

        public int X
        {
            get { return m_x; }
            set { m_x = value; }
        }

        public int Y
        {
            get { return m_y; }
            set { m_y = value; }
        }

        public static Point operator -(Point lhs)
		{
			return new Point(-lhs.X, -lhs.Y);
		}
        
        public static Point operator /(Point lhs, int value)
        {
            return new Point((int)(lhs.X / value), (int)(lhs.Y / value));
        }

		public static bool operator ==(Point lhs, Point rhs)
		{
			return (lhs.X == rhs.X) && (lhs.Y == rhs.Y);
		}

		public static bool operator !=(Point lhs, Point rhs)
		{
			return (lhs.X != rhs.X) || (lhs.Y != rhs.Y);
		}

		public static Point operator +(Point lhs, Point rhs)
		{
			return new Point(lhs.X + rhs.X, lhs.Y + rhs.Y);
		}

        public static Point operator *(Point lhs, float value)
        {
            return new Point((int)( value *lhs.X ), (int)(value * lhs.Y ));
        }

		public static Point operator -(Point lhs, Point rhs)
		{
			return new Point(lhs.X - rhs.X, lhs.Y - rhs.Y);
		}

        
        
		public override string ToString()
		{
			return string.Format("{0},{1}", X, Y);
		}

		public static float Distance(Point curPos, Point prevPos)
		{
			return (float)Math.Sqrt(Math.Pow(curPos.X - prevPos.X, 2) + Math.Pow(curPos.Y - prevPos.Y, 2));
		}

        public System.Drawing.Point Convert()
	    {
            return new System.Drawing.Point(X, Y);
	    }

	    #region For Disable Warning

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return (X & 0x0000ffff) << 16 | (Y & 0x0000ffff);
		}

		#endregion

		public static Point Parse(string str)
		{
			string[] values = str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			if (values.Length != 2) throw new FileLoadException("Point ������ �߸���");
			return new Point(int.Parse(values[0]),
			                 int.Parse(values[1]));
		}

		public int CompareTo(Point other)
		{
			if (X > other.X) return 1;
			else if (X < other.X) return -1;

			if (Y > other.Y) return 1;
			else if (Y < other.Y) return -1;

			return 0;
		}
	}

    class TPointConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Point)
            {
                Point p = (Point)value;

                return p.X + "," + p.Y;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;
                    return Point.Parse(s);
                }
                catch { }
                throw new ArgumentException("Can not convert '" + (string)value + "' to type Person");
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}