using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Tool.TSystem.ImageMaker
{
	public static class Filter
	{
		public static void Invert(Bitmap b)
		{
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			unsafe
			{
				byte* pSrc = (byte*)bmData.Scan0;

				int nOffset = bmData.Stride - b.Width * 3;
				int nWidth = b.Width * 3;
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						*pSrc = (byte)(255 - *pSrc);
						++pSrc;
					}
					pSrc += nOffset;
				}
			}
			b.UnlockBits(bmData);
		}

		public static void GrayScale(Bitmap b)
		{
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			unsafe
			{
				byte* pSrc = (byte*)bmData.Scan0;

				int nOffset = bmData.Stride - b.Width * 3;
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						byte grey = (byte)(.299 * *(pSrc) + .587 * *(pSrc + 1) + .114 * *(pSrc + 2));
						*(pSrc++) = grey;
						*(pSrc++) = grey;
						*(pSrc++) = grey;
					}
					pSrc += nOffset;
				}
			}
			b.UnlockBits(bmData);
		}

		public static void Brightness(Bitmap b, int nBrightness)
		{
			if (nBrightness < -255 || nBrightness > 255)
				return;

			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			unsafe
			{
				byte* pSrc = (byte*)bmData.Scan0;

				int nOffset = bmData.Stride - b.Width * 3;
				int nWidth = b.Width * 3;

				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						int nVal = ( *pSrc + nBrightness);
						*(pSrc++) = (byte)Math.Max(0, Math.Min(nVal, 255));
					}
					pSrc += nOffset;
				}
			}
			b.UnlockBits(bmData);
		}

		public static void Conv3x3(Bitmap b, ConvMatrix m)
		{
			if (0 == m.Factor) return;

			Bitmap bSrc = (Bitmap)b.Clone();
                        
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			int stride2 = stride * 2;
			unsafe
			{
				byte* pData = (byte*)bmData.Scan0;
				byte* pSrc = (byte*)bmSrc.Scan0;

				int nOffset = stride + 6 - b.Width * 3;
				int nWidth = b.Width - 2;
				int nHeight = b.Height - 2;
                
				for (int y = 0; y < nHeight; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						int nPixel = ((((pSrc[2] * m.TopLeft) + (pSrc[5] * m.TopMid) + (pSrc[8] * m.TopRight) +
						                (pSrc[2 + stride] * m.MidLeft) + (pSrc[5 + stride] * m.Pixel) + (pSrc[8 + stride] * m.MidRight) +
						                (pSrc[2 + stride2] * m.BottomLeft) + (pSrc[5 + stride2] * m.BottomMid) + (pSrc[8 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);
                        
						pData[5 + stride] = (byte)Math.Max(0, Math.Min(nPixel, 255));

						nPixel = ((((pSrc[1] * m.TopLeft) + (pSrc[4] * m.TopMid) + (pSrc[7] * m.TopRight) +
						            (pSrc[1 + stride] * m.MidLeft) + (pSrc[4 + stride] * m.Pixel) + (pSrc[7 + stride] * m.MidRight) +
						            (pSrc[1 + stride2] * m.BottomLeft) + (pSrc[4 + stride2] * m.BottomMid) + (pSrc[7 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

						pData[4 + stride] = (byte)Math.Max(0, Math.Min(nPixel, 255));

						nPixel = ((((pSrc[0] * m.TopLeft) + (pSrc[3] * m.TopMid) + (pSrc[6] * m.TopRight) +
						            (pSrc[0 + stride] * m.MidLeft) + (pSrc[3 + stride] * m.Pixel) + (pSrc[6 + stride] * m.MidRight) +
						            (pSrc[0 + stride2] * m.BottomLeft) + (pSrc[3 + stride2] * m.BottomMid) + (pSrc[6 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

						pData[3 + stride] = (byte)Math.Max(0, Math.Min(nPixel, 255));

						pData += 3;
						pSrc += 3;
					}

					pData += nOffset;
					pSrc += nOffset;
				}
			}
			b.UnlockBits(bmData);
			bSrc.UnlockBits(bmSrc);
		}
       
		public static void Smooth(Bitmap b, int nWeight /* default to 1 */)
		{
			ConvMatrix m = new ConvMatrix();
			m.SetAll(1);
			m.Pixel = nWeight;
			m.Factor = nWeight + 8;

			Conv3x3(b, m);
		}
       
		public static void GaussianBlur(Bitmap b, int nWeight /* default to 4*/)
		{
			ConvMatrix m = new ConvMatrix();
			m.SetAll(1);
			m.Pixel = nWeight;
			m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
			m.Factor = nWeight + 12;

			Conv3x3(b, m);
		}

		public static void Sharpen(Bitmap b, int nWeight /* default to 11*/ )
		{
			ConvMatrix m = new ConvMatrix();
			m.SetAll(0);
			m.Pixel = nWeight;
			m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = -2;
			m.Factor = nWeight - 8;

			Conv3x3(b, m);
		}

		public static void Embossing(Bitmap b, int nWeight /* default to 11*/ )
		{
			ConvMatrix m = new ConvMatrix();
			m.SetAll(0);
			m.Pixel = nWeight;
			m.TopLeft = m.TopMid = m.MidLeft = 1;
			m.MidRight = m.BottomMid = m.BottomRight = -1;
			m.Factor = nWeight - 8;

			Conv3x3(b, m);
		}

		public static void EdgeDetectQuick(Bitmap b)
		{
			ConvMatrix m = new ConvMatrix();
			m.TopLeft = m.TopMid = m.TopRight = -1;
			m.MidLeft = m.Pixel = m.MidRight = 0;
			m.BottomLeft = m.BottomMid = m.BottomRight = 1;

			m.Offset = 127;
			Conv3x3(b, m);
		}

		public static Bitmap Laplace(DevImage img)
		{
			Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
			img.LockBitmap();
			for (int y = 1; y < img.Width - 1; y++)
			{
				for (int x = 1; x < img.Height - 1; x++)
				{
					Color[,] c = Pixel.Get3x3(x, y,img);
					int red = (((c[0, 0].R + c[0, 1].R + c[0, 2].R + c[1, 0].R + c[1, 2].R + c[2, 0].R
					             + c[2, 1].R + c[2, 2].R) * -1) + (c[1, 1].R * 8)) + 128;
					int green = (((c[0, 0].G + c[0, 1].G + c[0, 2].G + c[1, 0].G + c[1, 2].G
					               + c[2, 0].G + c[2, 1].G + c[2, 2].G) * -1) + (c[1, 1].G * 8)) + 128;
					int blue = (((c[0, 0].B + c[0, 1].B + c[0, 2].B + c[1, 0].B + c[1, 2].B + c[2, 0].B
					              + c[2, 1].B + c[2, 2].B) * -1) + (c[1, 1].B * 8)) + 128;
					if (red >= 128) red = 0; else red = 255;
					if (green >= 128) green = 0; else green = 255;
					if (blue >= 128) blue = 0; else blue = 255;
					bmp.SetPixel(y, x, Color.FromArgb(red, green, blue));
				}
			}
			img.UnlockBitmap();
			return bmp;
		}

		public static Bitmap LaplaceGreyscale(DevImage img)
		{
			Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
			img.LockBitmap();
			for (int y = 1; y < img.Width - 1; y++)
			{
				for (int x = 1; x < img.Height - 1; x++)
				{
					int[,] c = Pixel.GetGrey3x3(x, y,img);
					int val = (((c[0, 0] + c[0, 1] + c[0, 2] + c[1, 0] + c[1, 2] + c[2, 0] + c[2, 1]
					             + c[2, 2]) * -1) + (c[1, 1] * 8)) + 128;
					if (val >= 128) val = 0; else val = 255;
					bmp.SetPixel(y, x, Color.FromArgb(val, val, val));
				}
			}
			img.UnlockBitmap();
			return bmp;
		}
       
		public static Bitmap Median3x3(DevImage img)
		{
			Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
			img.LockBitmap();
			for (int y = 1; y < img.Width - 1; y++)
			{
				for (int x = 1; x < img.Height - 1; x++)
				{
					Color[,] c = Pixel.Get3x3(x, y,img);
					int red = Median(c[0, 0].R, c[0, 1].R, c[0, 2].R, c[1, 0].R, c[1, 1].R, c[1, 2].R,
					                 c[2, 0].R, c[2, 1].R, c[2, 2].R);
					int green = Median(c[0, 0].G, c[0, 1].G, c[0, 2].G, c[1, 0].G, c[1, 1].G,
					                   c[1, 2].G, c[2, 0].G, c[2, 1].G, c[2, 2].G);
					int blue = Median(c[0, 0].B, c[0, 1].B, c[0, 2].B, c[1, 0].B, c[1, 1].B, c[1, 2].B,
					                  c[2, 0].B, c[2, 1].B, c[2, 2].B);
					bmp.SetPixel(y, x, Color.FromArgb(red, green, blue));
				}
			}
			img.UnlockBitmap();
			return bmp;
		}

		public static int Median(params int[] values)
		{
			Array.Sort(values);
			return values[((values.Length - 1) / 2) + 1];
		}
	}
}