using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace Tool.TSystem.Primitive
{
    [TypeConverter(typeof(TRectConverter))]
    public struct Rect
    {
        private int left;
        private int top;
        private int right;
        private int bottom;

        public static Rect None = new Rect(0, 0, 0, 0);
        public static Rect Load(XmlNode child)
        {
            int x = int.Parse(child.Attributes["left"].Value);
            int y = int.Parse(child.Attributes["top"].Value);
            int width = int.Parse(child.Attributes["width"].Value);
            int height = int.Parse(child.Attributes["height"].Value);
            return new Rect(x, y, width, height);
        }

        public Rect( Rectangle rect )
        {
            this.left = rect.Left;
            this.top = rect.Top;
            this.right = left + rect.Width;
            this.bottom = right + rect.Height;
        }

        public Rect(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public Rect(Point position, int width, int height)
        {
            left = position.X;
            top = position.Y;
            this.right = left + width;
            this.bottom = top + height;
        }

        public Rect(Point pointA, Point pointB)
        {
            left = Math.Min(pointA.X, pointB.X);
            top = Math.Min(pointA.Y, pointB.Y);
            right = Math.Max(pointA.X , pointB.X);
            bottom = Math.Max(pointA.Y , pointB.Y);
        }

        public static bool operator ==(Rect lhs, Rect rhs)
        {
            if ((lhs.left == rhs.left) &&
                (lhs.top == rhs.top) &&
                (lhs.right == rhs.right) &&
                (lhs.bottom == rhs.bottom))
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(Rect lhs, Rect rhs)
        {
            return !(lhs == rhs);
        }

        public static Rect operator *(Rect lhs, int rhs)
        {
            return new Rect(lhs.Position, lhs.Width * rhs, lhs.Height * rhs);
        }

        public bool Has(Point point)
        {
            return (left <= point.X && top <= point.Y) && (left + Width > point.X && top + Height > point.Y);
        }

        public bool Include(Rect rect)
        {
            if ((rect.left >= Right) || (rect.Right <= left)) return false;

            if ((rect.Bottom <= top) || (rect.top >= Bottom)) return false;
            
            return true;
        }

        [Browsable(false)]
        public Point Position
        {
            get { return new Point(left, top); }
        }

        [Browsable(false)]
        public Point Size
        {
            get { return new Point(Width, Height); }
        }

        [Browsable(false)]
        public Point RightBottom
        {
            get { return new Point(right, bottom); }
        }

        public int Left
        {
            get { return left;  }
            set { left = value; }
        }

        public void SetLeft(int nleft)
        {
            left = nleft;
        }

        public int Top
        {
            get { return top; }
            set { top = value; }
        }

        public int Right
        {
            get { return right; }
            set { right = value; }
        }

        public int Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        [Browsable(false)]
        public int Width
        {
            get { return right - left; }
            set { right = left + value; }
        }

        [Browsable(false)]
        public int Height
        {
            get { return bottom - top; }
            set { bottom = top + value; }
        }


        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", left, top, Width, Height );
        }

        public static Rect Join(Rect lhs, Rect rhs)
        {
            Point start = new Point(Math.Min(lhs.left, rhs.left), Math.Min(lhs.top, rhs.top));
            Point end = new Point(Math.Max(lhs.Right, rhs.Right), Math.Max(lhs.Bottom, rhs.Bottom));
            return new Rect(start, end);
        }

        #region For Disable Warning

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        public Rectangle Convert()
        {
            return new Rectangle(left, top, Width, Height);
        }

        public static Rect Parse(string str)
        {
            string[] values = str.Split( new string[] { "," }, StringSplitOptions.RemoveEmptyEntries );

            if (values.Length != 4)
            {
                throw new FileLoadException("Point 정보가 잘못됨");
            }

            int iLeft   = int.Parse(values[0]);
            int iTop    = int.Parse(values[1]);
            int iWidth = int.Parse(values[2]);
            int iHeight = int.Parse(values[3]);

            return new Rect( new Point(iLeft, iTop), iWidth, iHeight);
        }

        /// <summary>
        /// 상하좌우로 size만큼 커진다
        /// </summary>
        public void Grow(int size)
        {
            left -= size;
            top -= size;
            Width += size * 2;
            Height += size * 2;
        }

        public void Offset(Point offset)
        {
            left += offset.X;
            top += offset.Y;
        }

        /// <summary>
        /// 상하좌우로 size만큼 작아진다
        /// </summary>
  

        public void Resize(Point size)
        {
            Width += size.X;
            Height += size.Y;
        }

        public static Rect Intersect(Rect lhs, Rect rhs)
        {
            int left = Math.Max(lhs.left, rhs.left);
            int right = Math.Min(lhs.Right, rhs.Right);
            if (left >= right) return new Rect(0, 0, 0, 0);

            int top = Math.Max(lhs.top, rhs.top);
            int bottom = Math.Min(lhs.Bottom, rhs.Bottom);
            if (top >= bottom) return new Rect(0, 0, 0, 0);

            return new Rect(new Point(left, top), new Point(right, bottom));
        }
    }

    class TRectConverter : ExpandableObjectConverter
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
                Rect rt = (Rect)value;
                return rt.Left + "," + rt.Top + rt.Right + "," + rt.Bottom;
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
                    return Rect.Parse(s);
                }
                catch { }
                throw new ArgumentException("Can not convert '" + (string)value + "' to type Person");
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}