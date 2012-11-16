using System.Drawing;
using System.Runtime.InteropServices;

namespace Tool.TSystem.ImageMaker
{
	public class ConvMatrix
	{
		public int TopLeft = 0, TopMid = 0, TopRight = 0;
		public int MidLeft = 0, Pixel = 1, MidRight = 0;
		public int BottomLeft = 0, BottomMid = 0, BottomRight = 0;
		public int Factor = 1;
		public int Offset = 0;
        
		public void SetAll(int nVal)
		{
			TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BottomLeft = BottomMid = BottomRight = nVal;
		}
	}

	public static class Pixel
	{
		public static Color[,] Get3x3(int row, int column , Image image )
		{
			Color[,] c = new Color[3, 3];
			c[0, 0] = image.GetPixel(column - 1, row - 1);
			c[0, 1] = image.GetPixel(column - 1, row);
			c[0, 2] = image.GetPixel(column - 1, row + 1);
			c[1, 0] = image.GetPixel(column, row - 1);
			c[1, 1] = image.GetPixel(column, row);
			c[1, 2] = image.GetPixel(column, row + 1);
			c[2, 0] = image.GetPixel(column + 1, row - 1);
			c[2, 1] = image.GetPixel(column + 1, row);
			c[2, 2] = image.GetPixel(column + 1, row + 1);
			return c;
		}

		public static int[,] GetGrey3x3(int row, int column, Image image)
		{
			int[,] c = new int[3, 3];
			c[0, 0] = image.GetGreyPixel(column - 1, row - 1);
			c[0, 1] = image.GetGreyPixel(column - 1, row);
			c[0, 2] = image.GetGreyPixel(column - 1, row + 1);
			c[1, 0] = image.GetGreyPixel(column, row - 1);
			c[1, 1] = image.GetGreyPixel(column, row);
			c[1, 2] = image.GetGreyPixel(column, row + 1);
			c[2, 0] = image.GetGreyPixel(column + 1, row - 1);
			c[2, 1] = image.GetGreyPixel(column + 1, row);
			c[2, 2] = image.GetGreyPixel(column + 1, row + 1);
			return c;
		}

		public static int GetRGBHistogramValue(int column, int row, Image image)
		{
			Color c = image.GetPixel(column, row);
			int val = 0;
			int tmp = 0;
			tmp = c.R;
			if (tmp - 128 > 0)
			{
				tmp -= 128;
				val += 32;
			}
			if (tmp - 64 > 0)
			{
				val += 16;
			}
			tmp = c.G;
			if (tmp - 128 > 0)
			{
				tmp -= 128;
				val += 8;
			}
			if (tmp - 64 > 0)
			{
				val += 4;
			}
			tmp = c.B;
			if (tmp - 128 > 0)
			{
				tmp -= 128;
				val += 2;
			}
			if (tmp - 64 > 0)
			{
				val += 1;
			}
			return val;
		} 
	}
}