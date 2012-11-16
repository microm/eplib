using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Tool.TSystem.ImageMaker
{
	public class DevImage : Image
	{
		private PixelFormat m_pixelFormat = PixelFormat.Format32bppArgb;
        
		static DevImage()
		{
            if (DevilAPI.ilGetInteger((int)IL_Values.VERSION_NUM) < DevilAPI.IL_VERSION)
            {
                Console.WriteLine("*** Your DevIL native libraries are older than what Tao.DevIl supports, get the latest DevIL native libraries. ***");
                Console.WriteLine("Your DevIL native IL version: {0}.  version: {1}.",
                    DevilAPI.ilGetInteger((int)IL_Values.VERSION_NUM), DevilAPI.IL_VERSION);
            }


			DevilAPI.ilInit();
			//DevilAPI.ilutInit();
			//DevilAPI.ilutRenderer(IL_Render.WIN32);
		}

		public DevImage(int width, int height)
		{
			m_height = height;
			m_width = width;
			m_bpp = 4;
			ImageData = new byte[m_bpp * Width * Height];
			for (int i = 0; i < ImageData.Length; i++)
			{
				ImageData[i] = 0xFF;
			}
		}

		public DevImage(int width, int height, int bpp, byte[] buffer)
		{
			m_height = height;
			m_width = width;
			m_bpp = bpp;
			ImageData = buffer;
		}

		public DevImage(Bitmap bmp)
		{
			m_width = bmp.Width;
			m_height = bmp.Height;
		    m_pixelFormat = bmp.PixelFormat;
            if ( bmp.PixelFormat == PixelFormat.Format32bppArgb )
            {
                m_bpp = 4;
            }
            else if ( bmp.PixelFormat == PixelFormat.Format24bppRgb )
            {
                m_bpp = 3;
            }
			BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, Width, Height),
			                                     ImageLockMode.ReadOnly, m_pixelFormat);
			ImageData = new byte[m_bpp * Width * Height];
			Marshal.Copy(bitmapData.Scan0, ImageData, 0, m_bpp * Width * Height);

			bmp.UnlockBits(bitmapData);
		}

		public override bool Load(string path)
		{
			byte[] bytes = File.ReadAllBytes(path);
			return Load(bytes);
		}

		public bool Load(byte[] bytes)
		{
			int ilImage;

			DevilAPI.ilGenImages(1, out ilImage);
			DevilAPI.ilBindImage(ilImage);
			try
			{
				if (!DevilAPI.ilLoadL(IL_FileExt.TYPE_UNKNOWN, bytes, bytes.Length))
					throw new Exception("Failed to load image.");

				if (!DevilAPI.ilConvertImage(IL_Format.BGRA, IL_Type.UNSIGNED_BYTE))
					throw new Exception("Failed to convert image.");

                m_bpp = DevilAPI.ilGetInteger((int)IL_Values.IMAGE_BPP);
                m_width = DevilAPI.ilGetInteger((int)IL_Values.IMAGE_WIDTH);
                m_height = DevilAPI.ilGetInteger((int)IL_Values.IMAGE_HEIGHT);
                m_depth = DevilAPI.ilGetInteger((int)IL_Values.IMAGE_DEPTH);

				ReloadImageData();

				DevilAPI.ilDeleteImages(1, ref ilImage);
			}
			catch (Exception e)
			{
				DevilAPI.ilDeleteImages(1, ref ilImage);

				Debug.Print(e.ToString());
				return false;
			}
			return true;
		}
        
		public override bool Save(string path)
		{
            int ilImage = ImageBinding();
            DevilAPI.iluFlipImage();
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		    
            bool result = DevilAPI.ilSaveImage(path);
			DevilAPI.ilDeleteImages(1, ref ilImage);
			return result;
		}
        
		protected override Bitmap GetBitmap()
		{
			int ilImage = ImageBinding();
			return MakeBitmap(ilImage);
		}

		public void SaveBmp( string path )
		{
		    Bitmap bmp = GetBitmap();
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			bmp.Save( path , ImageFormat.Bmp );
		}

		public Bitmap ReSize(int width, int height)
		{
			int ilImage = ImageBinding();
			if (width == m_width || height == m_height)
			{
				return MakeBitmap(ilImage);
			}

			DevilAPI.iluScale(width, height, 1);

			m_width = width;
			m_height = height;

			ReloadImageData();
			return BmpImage;
		}

		public void BlurFilter(int Iter)
		{
			int ilImage = ImageBinding();
			DevilAPI.iluBlurGaussian(Iter);
			ReloadBitmap(ilImage);
		}

        public void FlipFilter()
        {
            int ilImage = ImageBinding();
            DevilAPI.iluFlipImage();
            ReloadBitmap(ilImage);
        }

        public void MirrorFilter()
        {
            int ilImage = ImageBinding();
            DevilAPI.iluMirror();
            ReloadBitmap(ilImage);
        }

        public void EmbossFilter()
        {
            int ilImage = ImageBinding();
            DevilAPI.iluEmboss();
            ReloadBitmap(ilImage);
        }

        public void NoisifyFilter()
        {
            int ilImage = ImageBinding();
            DevilAPI.iluNoisify(0.3f);
            ReloadBitmap(ilImage);
        }

        public void PixelizeFilter()
        {
            int ilImage = ImageBinding();
            DevilAPI.iluPixelize(5);
            ReloadBitmap(ilImage);
        }

		public Bitmap ImageFilter(IL_Filter filter)
		{
			int ilImage = ImageBinding();
			switch (filter)
			{
				case IL_Filter.Flip:
					DevilAPI.iluFlipImage();
					break;
				case IL_Filter.Mirror:
					DevilAPI.iluMirror();
					break;
				case IL_Filter.Pixelize:
					DevilAPI.iluPixelize(5);
					break;
				case IL_Filter.Blur:
					DevilAPI.iluBlurGaussian(4);
					break;
				case IL_Filter.Emboss:
					DevilAPI.iluEmboss();
					break;
				case IL_Filter.Noisify:
					DevilAPI.iluNoisify(0.3f);
					break;
			}
			return MakeBitmap(ilImage);
		}

		private void ReloadImageData()
		{
			ImageData = new byte[m_bpp * Width * Height];
			IntPtr dataptr = DevilAPI.ilGetData();
			Marshal.Copy(dataptr, ImageData, 0, m_bpp * Width * Height);

			m_modify = true;
		}

		private void ReloadBitmap(int ilImage)
		{
			Bitmap bitmap = new Bitmap(Width, Height, m_pixelFormat);

			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
			                                        ImageLockMode.WriteOnly, m_pixelFormat);
            if (m_bpp == 4)
            {
                DevilAPI.ilCopyPixels(0, 0, 0, Width, Height, 1, IL_Format.BGRA, IL_Type.UNSIGNED_BYTE, bitmapData.Scan0);
            }
            else
            {
                DevilAPI.ilCopyPixels(0, 0, 0, Width, Height, 1, IL_Format.BGR, IL_Type.UNSIGNED_BYTE, bitmapData.Scan0);
            }
		    bitmap.UnlockBits(bitmapData);

			Marshal.Copy(bitmapData.Scan0, ImageData, 0, m_bpp * Width * Height);

			DevilAPI.ilDeleteImages(1, ref ilImage);
			m_modify = true;
		}

        public Bitmap Crop(int x, int y, int width, int height)
        {
            int ilImage = ImageBinding();

            if (width <= 0 || height <= 0)
                return null;

            if (width == m_width || height == m_height)
            {
                return MakeBitmap(ilImage);
            }
            Bitmap bitmap = new Bitmap(width, height, m_pixelFormat);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height),
                                                    ImageLockMode.WriteOnly, m_pixelFormat);

            IL_Format pixelformat = IL_Format.BGR;
            if (m_pixelFormat == PixelFormat.Format32bppArgb)
            {
                pixelformat = IL_Format.BGRA;
            }

            DevilAPI.ilCopyPixels(x, y, 0, width, height, 1, pixelformat, IL_Type.UNSIGNED_BYTE, bitmapData.Scan0);
            bitmap.UnlockBits(bitmapData);
            DevilAPI.ilDeleteImages(1, ref ilImage);

            return bitmap;
        }

        public Bitmap ConvertRGB()
        {
            int ilImage = ImageBinding();

            //DevilAPI.ilConvertImage(IL_Format.RGB, IL_Type.UNSIGNED_BYTE);

            Bitmap bitmap = new Bitmap(Width, Height, m_pixelFormat);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                                    ImageLockMode.WriteOnly, m_pixelFormat);

            IL_Format pixelformat =  IL_Format.BGR;
            if (m_pixelFormat == PixelFormat.Format32bppArgb)
            {
                pixelformat = IL_Format.BGRA;
            }

            DevilAPI.ilCopyPixels(0, 0, 0, Width, Height, 1,pixelformat, IL_Type.UNSIGNED_BYTE, bitmapData.Scan0);
            bitmap.UnlockBits(bitmapData);

            DevilAPI.ilDeleteImages(1, ref ilImage);
            return bitmap;
        }

		private Bitmap MakeBitmap(int ilImage)
		{
			Bitmap bitmap = new Bitmap(Width, Height, m_pixelFormat);

			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
			                                        ImageLockMode.WriteOnly, m_pixelFormat);

            if (m_bpp == 4)
            {
                DevilAPI.ilCopyPixels(0, 0, 0, Width, Height, 1, IL_Format.BGRA, IL_Type.UNSIGNED_BYTE, bitmapData.Scan0);
            }
            else if (m_bpp == 3)
            {
                DevilAPI.ilCopyPixels(0, 0, 0, Width, Height, 1, IL_Format.BGR, IL_Type.UNSIGNED_BYTE, bitmapData.Scan0);
            }			
            bitmap.UnlockBits(bitmapData);

			DevilAPI.ilDeleteImages(1, ref ilImage);
			return bitmap;
		}

		private int ImageBinding()
		{
			int ilImage;
			DevilAPI.ilGenImages(1, out ilImage);
			DevilAPI.ilBindImage(ilImage);

            if (m_bpp == 4)
            {
                DevilAPI.ilTexImage(Width, Height, 1, 4, IL_Format.BGRA, IL_Type.UNSIGNED_BYTE, ImageData);
            }
            else if (m_bpp == 3)
            {
                DevilAPI.ilTexImage(Width, Height, 1, 3, IL_Format.BGR, IL_Type.UNSIGNED_BYTE, ImageData);
            }
		    return ilImage;
		}
        
		public static DevImage FromFile(string path)
		{
			DevImage newImage = new DevImage(1, 1);
			if (newImage.Load(path))
			{
				return newImage;
			}
			return null;
		}

		public static DevImage FromBytes(byte[] bytes)
		{
			DevImage newImage = new DevImage(1, 1);
			if (newImage.Load(bytes))
			{
				return newImage;
			}
			return null;
		}

		public float[,] GetHeight()
		{
			float[,] heights = new float[Width + 1, Width + 1];
			LockBitmap();
			for (int y = 1; y < Width - 1; y++)
			{
				for (int x = 1; x < Height - 1; x++)
				{
					int[,] vals = Get3x3(x, y);

					float height = vals[0, 0] + vals[0, 1] + vals[0, 2] +
					               vals[1, 0] + vals[1, 1] + vals[1, 2] +
					               vals[2, 0] + vals[2, 1] + vals[2, 2];

					heights[x, y] = height / 9.0f;
				}
			}
			UnlockBitmap();

			return heights;
		}

        public bool CopyImg (Int32 offX, Int32 offY, DevImage image)
        {
           //*
            if(image == null) return false;
            if((image.Width  > m_width - offX) && (image.Height > m_height - offY)) return false;

            LockBitmap();
            image.LockBitmap();

            byte[] val = new byte[4];
            
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.m_width; j++)
                {
                    val[0] = image.GetPixel(j, i).A;
                    val[1] = image.GetPixel(j, i).R;
                    val[2] = image.GetPixel(j, i).G;
                    val[3] = image.GetPixel(j, i).B;
                    SetPixel(offX + j, offY + i, val);
                }
            }

            image.UnlockBitmap();
            UnlockBitmap();
            return true;
            /**/

            //Bitmap bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            //Graphics g = Graphics.FromImage(bmp);

            //g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height,
            //                    GraphicsUnit.Pixel);
            //g.Dispose();
            //return true;
        }
	}
}