using System;
using System.Runtime.InteropServices;
using System.Security;

//
// Developer's Image Library (DevIL) is a programmer's library
// load  .bmp, .cut, .dds, .doom, .gif, .ico, .jpg, .lbm, .mdl, .mng, .pal, .pbm, .pcd, .pcx, .pgm, .pic, .png, .ppm, .psd, .psp, .raw, .sgi, .tga and .tif
// saving include .bmp, .dds, .h, .jpg, .pal, .pbm, .pcx, .pgm,.png, .ppm, .raw, .sgi, .tga and .tif
//


#region Aliases
using ILHANDLE = System.IntPtr;
using ILenum = System.Int32;
using ILboolean = System.Boolean;
using ILbitfield = System.UInt32;
using ILbyte = System.Byte;
using ILshort = System.Int16;
using ILint = System.Int32;
using ILsizei = System.Int32;
using ILubyte = System.Byte;
using ILushort = System.UInt16;
using ILuint = System.Int32;
using ILfloat = System.Single;
using ILclampf = System.Single;
using ILdouble = System.Double;
using ILclampd = System.Double;
using ILstring = System.String;
#endregion Aliases

namespace Tool.TSystem.ImageMaker
{
	public sealed class DevilAPI
	{
        private const string ILUT_LIBRARY = "ILUT.dll";
        private const string ILU_LIBRARY = "ILU.dll";
		private const string DEVIL_LIBRARY = "Devil.dll";

		#region Define Values
		public const int IL_VERSION = 174;
		#endregion

		#region Devil library
		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern void ilInit();

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern void ilShutDown();

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern void ilBindImage(Int32 Image);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilSave(IL_FileExt Type, String FileName);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilSaveImage(String FileName);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Int32 ilSaveL(IL_FileExt Type, byte[] Lump, Int32 Size);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilLoad(IL_FileExt Type, String FileName);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilLoadL(IL_FileExt Type, byte[] Lump, Int32 Size);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilLoadImage(String FileName);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern void ilGenImages(Int32 Num, out Int32 Images);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Int32 ilGenImage();

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Int32 ilTypeFromExt(String FileName);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilCompressFunc(IL_Compress Mode);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilEnable(IL_Bool Mode);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilDisable(IL_Bool Mode);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern void ilDeleteImage(Int32 Num);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern void ilDeleteImages(Int32 Num, ref Int32 Image);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilTexImage(Int32 Width, Int32 Height, Int32 Depth, Byte numChannels, IL_Format Format, IL_Type Type, byte[] Data);
        
        [DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
        public static extern ILint ilGetInteger(ILenum Mode);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr ilGetData();

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Int32 ilCopyPixels(Int32 XOff, Int32 YOff, Int32 ZOff, Int32 Width, Int32 Height, Int32 Depth, IL_Format Format, IL_Type Type, IntPtr Data);

		[DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilConvertImage(IL_Format DestFormat, IL_Type DestType);

        [DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
        public static extern Boolean ilDxtcDataToImage();

        [DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
        public static extern Boolean ilDxtcDataToSurface();

        [DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
        public static extern void ilFlipSurfaceDxtcData();

        [DllImport(DEVIL_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
        public static extern ILuint ilGetDXTCData(IntPtr Buffer, ILuint BufferSize, ILenum DXTCFormat);




		#endregion

		#region ILU library
		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern void iluInit();

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern void iluDeleteImage(Int32 Id);

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluInvertAlpha();

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Int32 iluLoadImage(String FileName);

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluMirror();

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluNegative();

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluNoisify(Single Tolerance);

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluPixelize(Int32 PixSize);

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluReplaceColour(Byte Red, Byte Green, Byte Blue, Single Tolerance);

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluRotate(Single Angle);

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluScale(Int32 Width, Int32 Height, Int32 Depth);

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluSharpen(Single Factor, Int32 Iter);

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluWave(Single Angle);

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluBlurGaussian(Int32 Iter);

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluFlipImage();

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean iluEmboss();

		[DllImport(ILU_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Int32 iluGetInteger(Int32 Mode);
		#endregion

		#region ilut library
		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern void ilutInit();

		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilutRenderer(IL_Render Renderer);

		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr ilutWinLoadImage(IntPtr file, long hDC);

		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr ilutConvertToBBitmap();

		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr ilutConvertToHBitmap(IntPtr hDC);

		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilutWinLoadUrl(String Url);

		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilutWinSaveImage(String FileName, IntPtr Bitmap);

		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Boolean ilutWinPrint(Int32 XPos, Int32 YPos, Int32 Width, Int32 Height, IntPtr hDC);

		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern Int32 ilutGetInteger(Int32 Mode);

		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr ilutD3D9Texture(IntPtr Device);

		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr ilutD3D9VolumeTexture(IntPtr Device);

		[DllImport(ILUT_LIBRARY, CallingConvention = CallingConvention.Winapi), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr ilutD3D9CubeTexture(IntPtr Device);

		#endregion
	}
}