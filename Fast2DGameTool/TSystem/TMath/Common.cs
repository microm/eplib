using System;
using Tool.TSystem.Primitive;
using DxPlane = Microsoft.DirectX.Plane;

namespace Tool.TSystem.TMath
{
    public static class Common
    {
        public const float PI = (float)Math.PI;
        public const float DoublePI = PI * 2.0f;
        public const float HalfPI = PI * 0.5f;
        public const float RadianToDegree = 180.0f / PI;
        public const float DegreeToRadian = PI / 180.0f;

    	public const float DirectionEpsilon = 1/1024.0f;
    	public const float PositionEpsilon = 1/128.0f;
		public const float HalfPositionEpsilon = PositionEpsilon / 2.0f;

        public static readonly float Epsilon = 0.0000001f;

        public static readonly float FloatMin = -1000000000000;
        public static readonly float FloatMax = 1000000000000;

        public static float DegreesToRadians(float degree)
        {
            return degree * DegreeToRadian;
        }

        public static float RadiansToDegrees(float radian)
        {
            return radian * RadianToDegree;
        }
        
        public static float RadianIn2PI(float i_fRadian)
        {
            int n2PIMulti = (int)(i_fRadian / DoublePI);
            float radian = i_fRadian - n2PIMulti * DoublePI;

            if (radian < 0.0f)
            {
                return radian + DoublePI;
            }
            return radian;
        }

        public static float Arctangent_yx(double y, double x)
        {
            if (Math.Abs(x) > 1.0E-6)
            {
                return (float)Math.Atan2(y, x);
            }
            else if (y > 0)
            {
                return HalfPI;
            }
            else
            {
                return -HalfPI;
            }
        }

        public static float Lerp(float lhs, float rhs, float w)
        {
            return lhs + (rhs - lhs)*w;
        }

        // Gaussian Filter Weight		
        public static float[] GetWeightTable(float dispersion )
        {
            float[] tblWeight = new float[8];
            float total = 0;
            for (int i = 0; i < tblWeight.Length; i++)
            {
                float pos = 1.0f + 2.0f * (float)i;

                tblWeight[i] = (float)Math.Exp(-0.5f * (pos * pos) / dispersion);
                total += 2.0f * tblWeight[i];
            }
            for (int i = 0; i < tblWeight.Length; i++)
            {
                tblWeight[i] /= total;
            }

            return tblWeight;
        }

		public static float MultipleBytes(params byte[] values)
		{
			float result = 1;
			foreach (byte b in values)
				result *= b / 255.0f;
			return result;
		}

        public static uint MakeHex(uint high, uint low)
        {
            uint hex = 0;
            hex = (high & 0xFFFF) << 16 | (low & 0xFFFF);
            return hex;
        }

        public static uint GetHiWord(uint hex)
        {
            return ((hex >> 16) & 0xFFFF);
        }

        public static uint GetLowWord(uint hex)
        {
            return (hex & 0xFFFF);
        }

    }
}