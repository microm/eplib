using System;
using System.Drawing;
using System.Drawing.Imaging;
using Tool.TSystem.IO;

namespace Tool.TSystem.ImageMaker
{
	public abstract unsafe class Image : IDisposable
	{
		protected int m_width;
		protected int m_height;
		protected int m_depth;
		protected int m_bpp;

		private byte[] m_imageData;
		private Bitmap m_bmpImage;
		private BitmapData m_bmData;
		private byte* m_pPixel;

		protected bool m_modify = true;

		#region IDisposable Members
		private bool isDisposed = false;

		public void Dispose()
		{
			GC.SuppressFinalize(this);

			if (isDisposed) return;

			if (m_bmpImage != null)
				m_bmpImage.Dispose();

			isDisposed = true;
		}

		~Image()
		{
			Dispose();
		}
		#endregion

		public int Width
		{
			get { return m_width; }
		}

		public int Height
		{
			get { return m_height; }
		}

		public int Depth
		{
			get { return m_depth; }
		}
        
		public Bitmap BmpImage
		{
			get
			{
				if (m_bmpImage == null || m_modify )
				{
					m_bmpImage = GetBitmap();
					m_modify = false;
				}
				return m_bmpImage;
			}
		}

		public byte[] ImageData
		{
			get { return m_imageData; }
			set { m_imageData = value; }
		}

		public abstract bool Load(string path);
		public abstract bool Save(string path);
		protected  abstract Bitmap GetBitmap();

		public byte[] ToBuffer()
		{
			byte[] buffer = new byte[2 * 3 + m_imageData.Length];

			int offset = 0;
			WriteUInt16(buffer, m_width, ref offset);
			WriteUInt16(buffer, m_height, ref offset);
			WriteUInt16(buffer, m_bpp, ref offset);
			WriteBuffer(buffer, m_imageData, ref offset);

			return buffer;
		}

		public static DevImage ReadImage(BinaryLoader reader)
		{
			int width = reader.ReadInt16();
			int height = reader.ReadInt16();
			int bpp = reader.ReadInt16();

			byte[] buffer = reader.ReadBytes(width * height * bpp);

			return new DevImage(width, height, bpp, buffer);
		}

		public static void WriteBuffer(byte[] buffer, byte[] src, ref int offset)
		{
			Array.Copy(src, 0, buffer, offset, src.Length);
			offset += src.Length;
		}

		public static void WriteUInt16(byte[] buffer, int value, ref int offset)
		{
			buffer[offset++] = (byte)value;
			buffer[offset++] = (byte)(value >> 8);
		}

		#region Assist Pixels
		public void LockBitmap()
		{
			m_bmpImage = GetBitmap();
            
			m_bmData = m_bmpImage.LockBits(new Rectangle(0, 0, m_bmpImage.Width, m_bmpImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			m_pPixel = (byte*)m_bmData.Scan0;
		}

		public int[,] Get3x3(int x, int y)
		{
			int[,] vals = new int[3, 3];

			vals[0, 0] = *(m_pPixel + (x - 1) * 4 + m_bmData.Stride * (y - 1));
			vals[0, 1] = *(m_pPixel + (x) * 4 + m_bmData.Stride * (y - 1));
			vals[0, 2] = *(m_pPixel + (x + 1) * 4 + m_bmData.Stride * (y - 1));
			vals[1, 0] = *(m_pPixel + (x - 1) * 4 + m_bmData.Stride * y);
			vals[1, 1] = *(m_pPixel + (x) * 4 + m_bmData.Stride * y);
			vals[1, 2] = *(m_pPixel + (x + 1) * 4 + m_bmData.Stride * (y));
			vals[2, 0] = *(m_pPixel + (x - 1) * 4 + m_bmData.Stride * (y + 1));
			vals[2, 1] = *(m_pPixel + (x) * 4 + m_bmData.Stride * (y + 1));
			vals[2, 2] = *(m_pPixel + (x + 1) * 4 + m_bmData.Stride * (y + 1));
			return vals;
		}

		public byte GetPixelValue(int x, int y)
		{
			return *(m_pPixel + x * 4 + m_bmData.Stride * y);
		}

		public Color GetPixel(int x, int y)
		{
			byte* pixel = ( m_pPixel + x * 4 + m_bmData.Stride * y );
			return Color.FromArgb(*(pixel + 3), *(pixel+2), *(pixel + 1), *(pixel )); //RGBA
		}

		public int GetGreyPixel(int x, int y)
		{
			byte* pixel = (m_pPixel + x * 4 + m_bmData.Stride * y);
			return (int)(*(pixel) * 0.299 + *(pixel + 1) * 0.587 + *(pixel+2 ) * 0.114);
		}

		public void SetPixel(int x, int y, byte[] val)
		{
			int index = m_width * m_bpp * y + x * m_bpp; //BGRA

			m_imageData[index] = val[3];
			m_imageData[index + 1] = val[2];
			m_imageData[index + 2] = val[1];
			m_imageData[index + 3] = val[0]; //alpha

			m_modify = true;
		}

        public void SetPixel(int x, int y, PixelData4 val)
        {
            int index = m_width * m_bpp * y + x * m_bpp; //BGRA

            m_imageData[index] = val.B;
            m_imageData[index + 1] = val.G;
            m_imageData[index + 2] = val.R;
            m_imageData[index + 3] = val.A; //alpha

            m_modify = true;
        }

		public void UnlockBitmap()
		{
			m_bmpImage.UnlockBits(m_bmData);
			m_bmData = null;
			m_pPixel = null;
		}

		#endregion


	}
}