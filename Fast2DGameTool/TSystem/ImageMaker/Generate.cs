using System.Drawing;
using System.Drawing.Imaging;
using Tool.TSystem.Primitive;
using Point=System.Drawing.Point;

namespace Tool.TSystem.ImageMaker
{
	public unsafe static class Generate
	{
		public static Bitmap AddAlpha(Bitmap srcBmp, Bitmap srcAlpha)
		{
			DevImage newImage = new DevImage(srcBmp);
			Bitmap destBmp = newImage.BmpImage.Clone() as Bitmap;

			BitmapData srcData = srcAlpha.LockBits(new Rectangle(0, 0, srcAlpha.Width, srcAlpha.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			BitmapData destData = destBmp.LockBits(new Rectangle(0, 0, destBmp.Width, destBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
           
			byte* dest = (byte*)destData.Scan0;
			byte* src = (byte*)srcData.Scan0;

			int srcOffset = srcData.Stride - srcAlpha.Width * 4;
			int destOffset = destData.Stride - destBmp.Width * 4;

			for (int y = 0; y < destBmp.Height; ++y)
			{
				for (int x = 0; x < destBmp.Width; ++x)
				{
					*(dest + 3) = *(src); //alpha = red
					src += 4;
					dest += 4;
				}
				src += srcOffset;
				dest += destOffset;
			}
			destBmp.UnlockBits(destData);
			srcAlpha.UnlockBits(srcData);

			return destBmp;
		}

		public static Bitmap SetAlpha(Bitmap srcBmp, byte alpha)
		{
			BitmapData srcData = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            
			byte* src = (byte*)srcData.Scan0;
			int srcOffset = srcData.Stride - srcBmp.Width * 4;

			for (int y = 0; y < srcBmp.Height; ++y)
			{
				for (int x = 0; x < srcBmp.Width; ++x)
				{
					*(src + 3) = alpha; //alpha = red
					src += 4;
				}
				src += srcOffset;
			}
			srcBmp.UnlockBits(srcData);
			return srcBmp;
		}

		public static double[] GreyscaleHistogram(DevImage img)
		{
			double[] histogram = new double[256];
			img.LockBitmap();
			for (int y = 0; y < img.Width; y++)
			{
				for (int x = 0; x < img.Height; x++)
				{
					histogram[img.GetGreyPixel(y, x)]++;
				}
			}
			img.UnlockBitmap();
			return histogram;
		}

		public static double[] Histogram(DevImage img)
		{
			double[] histogram = new double[64];
			img.LockBitmap();
			for (int y = 0; y < img.Width; y++)
			{
				for (int x = 0; x < img.Height; x++)
				{
					histogram[Pixel.GetRGBHistogramValue(y, x, img)]++;
				}
			}
			img.UnlockBitmap();
			return histogram;
		}

		public static Bitmap ScaleBitmap(int width, int height, Bitmap srcImage)
		{
			Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
			Graphics g = Graphics.FromImage(bmp);

			Rectangle destRect = new Rectangle(0, 0, width, height);
			Rectangle srcRect = new Rectangle(0, 0, srcImage.Width, srcImage.Height);
			g.DrawImage(srcImage, destRect, srcRect, GraphicsUnit.Pixel);

			g.Dispose();
			return bmp;
		}


		public static Bitmap SplitImage(DevImage image, Rectangle rect)
		{
			Bitmap destBmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);

			image.LockBitmap();
			BitmapData destData = destBmp.LockBits(new Rectangle(0, 0, destBmp.Width, destBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            
			byte* pDest = (byte*)destData.Scan0;
			int offset = destData.Stride - destBmp.Width * 4;
			for (int y = 0; y < destBmp.Height; ++y)
			{
				for (int x = 0; x < destBmp.Width; ++x)
				{
					Color color = image.GetPixel(x + rect.Left, y + rect.Top);
					*(pDest++) = color.R;
					*(pDest++) = color.G;
					*(pDest++) = color.B;
					*(pDest++) = color.A;
				}
				pDest += offset;
			}
            
			image.UnlockBitmap();
			destBmp.UnlockBits(destData);
			return destBmp;
		}

		public static void MergeImageRect(DevImage image, Bitmap srcImage, Point point)
		{
			BitmapData srcData = srcImage.LockBits(new Rectangle(0, 0, srcImage.Width, srcImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            
			byte* pSrc = (byte*)srcData.Scan0;
			int offset = srcData.Stride - srcImage.Width * 4;
			for (int y = 0; y < srcImage.Height; ++y)
			{
				for (int x = 0; x < srcImage.Width; ++x)
				{
					int index = image.Width * 4 * (y + point.Y) + (x + point.X) * 4;
					image.ImageData[index++] = *(pSrc+2);
					image.ImageData[index++] = *(pSrc+1);
					image.ImageData[index++] = *(pSrc);
					image.ImageData[index] = *(pSrc+3);

					pSrc += 4;
				}
				pSrc += offset;
			}
			srcImage.UnlockBits(srcData);
		}

		public static Bitmap MixImage(Bitmap srcBmp, Bitmap srBmp2) //modulate
		{
			Bitmap destBmp = srcBmp.Clone() as Bitmap;

			BitmapData srcData = srBmp2.LockBits(new Rectangle(0, 0, srBmp2.Width, srBmp2.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			BitmapData destData = destBmp.LockBits(new Rectangle(0, 0, destBmp.Width, destBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            
			PixelData4* dest = (PixelData4*)destData.Scan0;
			PixelData4* src = (PixelData4*)srcData.Scan0;

			int srcOffset = srcData.Stride / 4 - srBmp2.Width;
			int destOffset = destData.Stride / 4 - destBmp.Width;

			for (int y = 0; y < destBmp.Height; ++y)
			{
				for (int x = 0; x < destBmp.Width; ++x)
				{
					PixelData4 pixel = new PixelData4(
						(byte) ((*dest).R*((*src).R/255f)),
						(byte) ((*dest).G*((*src).G/255f)),
						(byte) ((*dest).B*((*src).B/255f)), (*dest).A);

					*(dest++) = pixel;
					src++;
				}
				src += srcOffset;
				dest += destOffset;
			}
			destBmp.UnlockBits(destData);
			srBmp2.UnlockBits(srcData);

			return destBmp;
		}

		public static Bitmap RedMixImage(Bitmap srcBmp, Bitmap srBmp2)
		{
			Bitmap destBmp = srcBmp.Clone() as Bitmap;

			BitmapData srcData = srBmp2.LockBits(new Rectangle(0, 0, srBmp2.Width, srBmp2.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
			BitmapData destData = destBmp.LockBits(new Rectangle(0, 0, destBmp.Width, destBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            
			PixelData4* dest = (PixelData4*)destData.Scan0;
			PixelData3* src = (PixelData3*)srcData.Scan0;

			int srcOffset = srcData.Stride / 3 - srBmp2.Width;
			int destOffset = destData.Stride / 4 - destBmp.Width;

			for (int y = 0; y < destBmp.Height; ++y)
			{
				for (int x = 0; x < destBmp.Width; ++x)
				{
					PixelData4 pixel =
						new PixelData4(
							(byte)((*dest).R * ((*src).R / 255f)),
							(byte)((*dest).G * ((*src).G / 255f)),
							(*src).B,
							(*dest).A);

					*(dest++) = pixel;
					src++;
				}
				src += srcOffset;
				dest += destOffset;
			}
			destBmp.UnlockBits(destData);
			srBmp2.UnlockBits(srcData);

			return destBmp;
		}

		public static Bitmap MakeBitmapFromData(int width, byte[,] vals)
		{
			Bitmap destBmp = new Bitmap(width, width, PixelFormat.Format32bppArgb);

			BitmapData destData = destBmp.LockBits(new Rectangle(0, 0, destBmp.Width, destBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			byte* pDest = (byte*)destData.Scan0;
			int destOffset = destData.Stride - destBmp.Width * 4;

			for (int y = 0; y < width; ++y)
			{
				for (int x = 0; x < width; ++x)
				{
					int index = y*width + x;
					*(pDest++) = vals[index, 1];
					*(pDest++) = vals[index, 2];
					*(pDest++) = vals[index, 3];
					*(pDest++) = vals[index, 0];
				}
				pDest += destOffset;
			}
			destBmp.UnlockBits(destData);
			return destBmp;
		}
	}
}