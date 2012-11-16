namespace Tool.TSystem.ImageMaker
{


	public enum IL_FileExt : int
	{
		TYPE_UNKNOWN = 0x0000,
		BMP = 0x0420,
		CUT = 0x0421,
		DOOM = 0x0422,
		DOOM_FLAT = 0x0423,
		ICO = 0x0424,
		JPG = 0x0425,
		JFIF = 0x0425,
		LBM = 0x0426,
		PCD = 0x0427,
		PCX = 0x0428,
		PIC = 0x0429,
		PNG = 0x042A,
		PNM = 0x042B,
		SGI = 0x042C,
		TGA = 0x042D,
		TIF = 0x042E,
		CHEAD = 0x042F,
		RAW = 0x0430,
		MDL = 0x0431,
		WAL = 0x0432,
		LIF = 0x0434,
		MNG = 0x0435,
		JNG = 0x0435,
		GIF = 0x0436,
		DDS = 0x0437,
		DCX = 0x0438,
		PSD = 0x0439,
		EXIF = 0x043A,
		PSP = 0x043B,
		PIX = 0x043C,
		PXR = 0x043D,
		XPM = 0x043E,
		HDR = 0x043F,
	}

	public enum IL_Format : int
	{
		COLOUR_INDEX = 0x1900,
		COLOR_INDEX = 0x1900,
		RGB = 0x1907,
		RGBA = 0x1908,
		BGR = 0x80E0,
		BGRA = 0x80E1,
		LUMINANCE = 0x1909,
		LUMINANCE_ALPHA = 0x190A,
	}

	public enum IL_Compress : int
	{
		MODE = 0x0700,
		NONE = 0x0701,
		RLE = 0x0702,
		LZO = 0x0703,
		ZLIB = 0x0704,
	}

	public enum IL_Values : int
	{
		VERSION_NUM = 0x0DE2,
		IMAGE_WIDTH = 0x0DE4,
		IMAGE_HEIGHT = 0x0DE5,
		IMAGE_DEPTH = 0x0DE6,
		IMAGE_SIZE_OF_DATA = 0x0DE7,
		IMAGE_BPP = 0x0DE8,
		IMAGE_BYTES_PER_PIXEL = 0x0DE8,
		IMAGE_BITS_PER_PIXEL = 0x0DE9,
		IMAGE_FORMAT = 0x0DEA,
		IMAGE_TYPE = 0x0DEB,
		PALETTE_TYPE = 0x0DEC,
		PALETTE_SIZE = 0x0DED,
		PALETTE_BPP = 0x0DEE,
		PALETTE_NUM_COLS = 0x0DEF,
		PALETTE_BASE_TYPE = 0x0DF0,
		NUM_IMAGES = 0x0DF1,
		NUM_MIPMAPS = 0x0DF2,
		NUM_LAYERS = 0x0DF3,
		ACTIVE_IMAGE = 0x0DF4,
		ACTIVE_MIPMAP = 0x0DF5,
		ACTIVE_LAYER = 0x0DF6,
		CUR_IMAGE = 0x0DF7,
		IMAGE_DURATION = 0x0DF8,
		IMAGE_PLANESIZE = 0x0DF9,
		IMAGE_BPC = 0x0DFA,
		IMAGE_OFFX = 0x0DFB,
		IMAGE_OFFY = 0x0DFC,
		IMAGE_CUBEFLAGS = 0x0DFD,
		IMAGE_ORIGIN = 0x0DFE,
		IMAGE_CHANNELS = 0x0DFF,
	}

	public enum IL_Render : int
	{
		OPENGL = 0,
		ALLEGRO = 1,
		WIN32 = 2,
		DIRECT3D8 = 3,
		DIRECT3D9 = 4,
	}

	public enum IL_Bool : int
	{
		FALSE = 0,
		TRUE = 1,
	}

	public enum IL_File : int
	{
		OVERWRITE = 0x0620,
		MODE = 0x0621,
	}

	public enum IL_Type : int
	{
		BYTE = 0x1400,
		UNSIGNED_BYTE = 0x1401,
		SHORT = 0x1402,
		UNSIGNED_SHORT = 0x1403,
		INT = 0x1404,
		UNSIGNED_INT = 0x1405,
		FLOAT = 0x1406,
		DOUBLE = 0x140A,
	}

	public enum IL_Dxt : int
	{
		DXTC_FORMAT	= 0x0705,
		DXT1 = 0x0706,
		DXT2 = 0x0707,
		DXT3 = 0x0708,
		DXT4 = 0x0709,
		DXT5 = 0x070A,
		DXT_NO_COMP = 0x070B,
		KEEP_DXTC_DATA = 0x070C,
		DXTC_DATA_FORMAT = 0x070D,
	}

	public enum IL_Filter
	{
		Emboss,
		Mirror,
		Flip,
		Blur,
		Pixelize,
		Noisify
	}

	public enum IL_Channel
	{
		Alpha = 0,
		Red,
		Green,
		Blue,
		Max,
	}
}