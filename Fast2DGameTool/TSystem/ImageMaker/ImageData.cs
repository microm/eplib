using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Tool.TSystem.ImageMaker
{
    public class ImageData
    {
        private readonly IntPtr m_pBuf;
        private readonly int m_height;
        private readonly int m_width;

        private int m_x = 0;
        private int m_y = 0;

        public ImageData(int width, int height)
        {
            m_pBuf = Marshal.AllocHGlobal(width * height);
            m_width = width;
            m_height = height;
        }

        public int Width
        {
            get { return m_width; }
        }

        public int Height
        {
            get { return m_height; }
        }

        public IntPtr Buf
        {
            get { return m_pBuf; }
        }

        public unsafe void Set(byte value)
        {
            memset( (byte*)Buf, value, Width * Height);
        }

        ~ImageData()
        {
            Marshal.FreeHGlobal(Buf);
        }

        public unsafe byte Get(int x, int y)
        {
            if (y >= 0 && y < m_height &&
               x >= 0 && x < m_width)
            {
                return *((byte*)Buf + m_width * y + x);
            }
            throw new System.NotImplementedException();
        }

        public unsafe void Set(int x, int y,byte value)
        {
            if (y >= 0 && y < m_height &&
               x >= 0 && x < m_width)
            {
                *((byte*)Buf + m_width * y + x) = value;
            }
        }

        public byte Next_x()
        {
            ++m_x;
            return Get(m_x, m_y);
        }

        public byte Next_y()
        {
            ++m_y;
            return Get(m_x, m_y);
        }


        public static unsafe void memcpy(Byte* pDest, Byte* pSource, int Count)
        {
            for (uint i = 0; i < Count; i++)
            {
                *pDest++ = *pSource++;
            }
        }

        public static unsafe void memmove(Byte* pDest, Byte* pSource, int Count)
        {
            if (pSource > pDest || &pSource[Count] < pDest)
            {
                for (uint i = 0; i < Count; i++)
                {
                    *pDest++ = *pSource++;
                }
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        public static unsafe void memset(Byte* pDest, byte ByteVal, int Count)
        {
            for (uint i = 0; i < Count; i++)
            {
                *pDest++ = ByteVal;
            }
        }

        public unsafe ImageData Clone()
        {
            ImageData clone = new ImageData(Width,Height);
            memcpy((byte*)clone.Buf, (byte*)Buf, m_width * m_height);

            return clone;
        }

        public unsafe Byte[] ToArray()
        {
            int size = m_width*m_height;
            Byte[] array = new Byte[size];
            byte* bytes = (byte*) Buf;
            for (uint i = 0; i < size; i++)
            {
                array[i] = *bytes++;
            }
            return array;
        }

        public Bitmap CreateBitmap()
        {
            Bitmap bitmap = BitmapAssist.CreateGrayScaleBitmap(m_width, m_height);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, m_width, m_height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            unsafe
            {
                byte* pSrc = (byte*)data.Scan0;
                memcpy(pSrc, (byte*)Buf , m_width * m_height);
            }
            bitmap.UnlockBits(data);

            return bitmap;
        }
    }
}
