using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Tool.TSystem
{
    public class Define
    {
        public static readonly uint None = 0;
        public static readonly uint Cursor = 1;
        public static readonly uint Map = 2;
        public static readonly int Thumnail_Size = 80;

        public static readonly float CellSize = 1.0f;
        public static readonly int NodeCell = 16;
        public static readonly int CellCountPerLeaf = NodeCell*NodeCell;

        public static readonly int InDoorMapSize = NodeCell*4;

        public static readonly int PixelColumnCountPerCell = 2;
        public static readonly float PixelSize = CellSize / PixelColumnCountPerCell;
        public static readonly int PixelColumnCountPerLeaf = PixelColumnCountPerCell * NodeCell;
        public static readonly int AlphaPixelColumnCountPerLeaf = PixelColumnCountPerLeaf + 2;
    }

    public enum EParamType
    {
        Int = 0,
        Float = 1,
        Vector = 2,
        Tex = 3,
        Matrix = 4,
        Variable = 5,
        Int_ptr = 6,
        Float_ptr = 7,
        Vector_ptr = 8,
        Matrix_ptr = 9,
        Unknown = 0x1fffffff,
    }
    
    public enum TextCharType
    {
	    None	= 0,
	    Alpha,
	    Numeric,
        AlphaNumeric,
    }; 

    [StructLayout(LayoutKind.Sequential)]
    public struct PixelData4
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        public PixelData4(byte a, byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public uint GetValue()
        {
            UInt32 value =  B;
            value |= (uint)G << 8;
            value |= (uint)R << 16;
            value |= (uint)A << 24;
            return value;
        }

        public byte this[int index]
        {
            get
            {
                if (index == 0) return A;
                if (index == 1) return R;
                if (index == 2) return G;
                return B;
            }
            set
            {
                if (index == 0) A = value;
                if (index == 1) R = value;
                if (index == 2) G = value;
                if (index == 3) B = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", A,R,G,B);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PixelData3
    {
        public byte R;
        public byte G;
        public byte B;
        public PixelData3(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public byte this[int index]
        {
            get
            {
                if (index == 0) return R;
                if (index == 1) return G;
                return B;
            }
            set
            {
                if (index == 0) R = value;
                if (index == 1) G = value;
                if (index == 2) B = value;
            }
        }
    }
}
