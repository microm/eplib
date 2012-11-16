using System.Drawing;
using System.Drawing.Imaging;

namespace Tool.TSystem.ImageMaker
{
    public static class BitmapAssist
    {
        public static void MixColor(ref PixelData4 target, PixelData4 other, float amount)
        {
            target.R = (byte)((other.R - target.R) * amount + target.R);
            target.G = (byte)((other.G - target.G) * amount + target.G);
            target.B = (byte)((other.B - target.B) * amount + target.B);
            target.A = (byte)((other.A - target.A) * amount + target.A);
        }

        public static void BlendColor(ref PixelData4 target, PixelData4 other, float amount)
        {
            target.R = (byte)((other.R - (255 - target.R)) * amount + (255 - target.R));
            target.G = (byte)((other.G - (255 - target.G)) * amount + (255 - target.G));
            target.B = (byte)((other.B - (255 - target.B)) * amount + (255 - target.B));
            target.A = (byte)((other.A - (255 - target.A)) * amount + (255 - target.A));
        }

        public static Bitmap GetNoAlphaBitmap(DevImage img)
        {
            Bitmap bmp = img.BmpImage;
            for (int column = 0; column < img.Width; column++)
            {
                for (int row = 0; row < img.Height; row++)
                {
                    Color color = Color.FromArgb(img.GetPixel(column, row).R, img.GetPixel(column, row).G, img.GetPixel(column, row).B);
                    bmp.SetPixel(column, row, color);
                }
            }
            return bmp;
        }

        public static Bitmap TileBitmap(int width, int height, int repeat, Bitmap srcImage)
        {
            Bitmap bmp = new Bitmap(width, width, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmp);

            int destWidth = (int)(0.5f + (float)width / repeat);
            int destHeight = (int)(0.5f + (float)height / repeat);

            for (int column = 0; column < repeat; column++)
            {
                for (int row = 0; row < repeat; row++)
                {
                    Rectangle destRect = new Rectangle(destWidth * column, destHeight * row, destWidth, destHeight);
                    Rectangle srcRect = new Rectangle(0, 0, srcImage.Width, srcImage.Height);
                    g.DrawImage(srcImage, destRect, srcRect, GraphicsUnit.Pixel);
                }
            }
            g.Dispose();
            return bmp;
        }

        public static int CheckXColor(bool invert, Bitmap bitmap, Color colorKey)
        {
            if (invert == false)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        Color color = bitmap.GetPixel(x, y);
                        if (color != colorKey)
                        {
                            return x;
                        }
                    }
                }
                return 0;
            }
            else
            {
                for (int x = bitmap.Width - 1; x >= 0; x--)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        Color color = bitmap.GetPixel(x, y);
                        if (color != colorKey)
                        {
                            return x;
                        }
                    }
                }
                return bitmap.Width;
            }
        }

        public static int CheckYColor(bool invert, Bitmap bitmap, Color colorKey)
        {
            if (invert == false)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color color = bitmap.GetPixel(x, y);
                        if (color != colorKey)
                        {
                            return y;
                        }
                    }
                }
                return 0;
            }
            else
            {
                for (int y = bitmap.Height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color color = bitmap.GetPixel(x, y);
                        if (color != colorKey)
                        {
                            return y;
                        }
                    }
                }
                return bitmap.Height;
            }
        }

        public static Rectangle CalcImageRegion(Bitmap bitmap, Color colorKey)
        {
            Rectangle newRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            newRect.X = CheckXColor(false,bitmap,colorKey );
            newRect.Width = CheckXColor(true, bitmap, colorKey) - newRect.X + 1;
            newRect.Y = CheckYColor(false,bitmap,colorKey );
            newRect.Height = CheckYColor(true, bitmap, colorKey) - newRect.Y + 1;
                        
            return newRect;
        }

        public static Bitmap TileBitmap32(int width, int height, int repeat, Bitmap srcImage)
        {
            Bitmap bmp = new Bitmap(width, width, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);

            int destWidth = (int)(0.5f + (float)width / repeat);
            int destHeight = (int)(0.5f + (float)height / repeat);

            for (int column = 0; column < repeat; column++)
            {
                for (int row = 0; row < repeat; row++)
                {
                    Rectangle destRect = new Rectangle(destWidth * column, destHeight * row, destWidth, destHeight);
                    Rectangle srcRect = new Rectangle(0, 0, srcImage.Width, srcImage.Height);
                    g.DrawImage(srcImage, destRect, srcRect, GraphicsUnit.Pixel);
                }
            }
            g.Dispose();
            return bmp;
        }

       
        public static Bitmap CreateGrayScaleBitmap(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            ColorPalette paletteColors = bitmap.Palette;
            for (int i = 0; i < paletteColors.Entries.Length; ++i)
            {
                paletteColors.Entries[i] = Color.FromArgb(i, i, i);
            }
            bitmap.Palette = paletteColors;
            return bitmap;
        }

        public static Bitmap CreateAlphaBitmap(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData data =
                bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            unsafe
            {
                byte* pSrc = (byte*)data.Scan0.ToPointer();
                int offset = data.Stride - bitmap.Width * 4;
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < bitmap.Width; ++x)
                    {
                        *(pSrc + 3) = 0xFF;
                        pSrc += 4;
                    }
                    pSrc += offset;
                }
            }
            bitmap.UnlockBits(data);
            return bitmap;
        }

        public static Bitmap CreateBitmap(int width, int height,PixelData4 col )
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData data =
                bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            unsafe
            {
                byte* pSrc = (byte*)data.Scan0.ToPointer();
                int offset = data.Stride - bitmap.Width * 4;
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < bitmap.Width; ++x)
                    {
                        *(pSrc ) = col.R;
                        *(pSrc + 1) = col.G;
                        *(pSrc + 2) = col.B;
                        *(pSrc + 3) = col.A;
                        pSrc += 4;
                    }
                    pSrc += offset;
                }
            }
            bitmap.UnlockBits(data);
            return bitmap;
        }

        public static Bitmap GetChannel(Bitmap srcBmp, IL_Channel channel)
        {
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height, PixelFormat.Format24bppRgb);

            BitmapData srcData = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData destData = destBmp.LockBits(new Rectangle(0, 0, destBmp.Width, destBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* dest = (byte*)destData.Scan0.ToPointer();
                byte* src = (byte*)srcData.Scan0.ToPointer();

                int srcOffset = srcData.Stride - srcBmp.Width * 4;
                int destOffset = destData.Stride - destBmp.Width * 3;

                byte val;
                for (int y = 0; y < destBmp.Height; ++y)
                {
                    for (int x = 0; x < destBmp.Width; ++x)
                    {
                        if (IL_Channel.Red == channel)
                        {
                            val = *(src);
                            *(dest++) = 0;*(dest++) = 0;*(dest++) = val;
                        }
                        else if (IL_Channel.Green == channel)
                        {
                            val = *(src + 1);
                            *(dest++) = 0;*(dest++) = val;*(dest++) = 0;
                        }
                        else if (IL_Channel.Blue == channel)
                        {
                            val = *(src + 2);
                            *(dest++) = val;*(dest++) = 0;*(dest++) = 0;
                        }
                        else if (IL_Channel.Alpha == channel)
                        {
                            val = *(src + 3);
                            *(dest++) = val;
                            *(dest++) = val;
                            *(dest++) = val;
                        }
                        else 
                        {
                            *(dest++) = *(src+2);
                            *(dest++) = *(src+1);
                            *(dest++) = *(src);
                        }

                        src += 4;
                    }
                    src += srcOffset;
                    dest += destOffset;
                }
            }

            destBmp.UnlockBits(destData);
            srcBmp.UnlockBits(srcData);
            return destBmp;
        }

        public static void AddAlpha(Bitmap srcBmp,ImageData alpha)
        {
            if (alpha.Width != srcBmp.Width || alpha.Height != srcBmp.Height)
            {
                return;
            }
            BitmapData srcData = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* src = (byte*)srcData.Scan0;

                int srcOffset = srcData.Stride - srcBmp.Width * 4;
                for (int y = 0; y < srcBmp.Height; ++y)
                {
                    for (int x = 0; x < srcBmp.Width; ++x)
                    {
                        *(src+3) = alpha.Get( x ,y );
                        src += 4;
                    }
                    src += srcOffset;
                }
            }
            srcBmp.UnlockBits(srcData);
        }

        public static void AddChannel(ref Bitmap destBmp, Bitmap srcBmp, int channel)
        {
            if (destBmp.Width != srcBmp.Width || destBmp.Height != srcBmp.Height)
            {
                return; //error
            }
            BitmapData srcData = srcBmp.LockBits(new Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData destData = destBmp.LockBits(new Rectangle(0, 0, destBmp.Width, destBmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* dest = (byte*)destData.Scan0;
                byte* src = (byte*)srcData.Scan0;

                int srcOffset = srcData.Stride - srcBmp.Width * 4;
                int destOffset = destData.Stride - destBmp.Width * 4;

                for (int y = 0; y < destBmp.Height; ++y)
                {
                    for (int x = 0; x < destBmp.Width; ++x)
                    {
                        byte val = *(src);

                        if (val == 0xFF)
                        {
                            *(dest) = *(dest + 1) = *(dest + 2) = 0;
                        }
                        *(dest + 3) = 0xFF;
                        if ((int)IL_Channel.Red == channel)
                        {
                            *(dest) = val;
                        }
                        else if ((int)IL_Channel.Green == channel)
                        {
                            *(dest + 1) = val;
                        }
                        else if ((int)IL_Channel.Blue == channel)
                        {
                            *(dest + 2) = val;
                        }
                        else
                        {
                            *(dest + 3) = val;
                        }

                        dest += 4;
                        src += 4;
                    }
                    src += srcOffset;
                    dest += destOffset;
                }
            }

            destBmp.UnlockBits(destData);
            srcBmp.UnlockBits(srcData);
        }

        
    }
}