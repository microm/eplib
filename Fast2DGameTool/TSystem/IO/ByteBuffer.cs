using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Tool.TSystem.IO
{
	public sealed class ByteBuffer
	{
		public static byte[] ToByteArray<T>(T data) where T :  new()
		{
			int size = Marshal.SizeOf(typeof(T));

			byte[] buff = new byte[size];
			GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
			Marshal.StructureToPtr( data , handle.AddrOfPinnedObject(), false);
			handle.Free();

			return buff;
		}

		public static int SizeOf<T>()
		{
			return Marshal.SizeOf(typeof(T));
		}

		public static void WriteBuffer(byte[] buffer, byte[] src,ref int offset)
		{
			Array.Copy(src, 0, buffer, offset, src.Length);
			offset += src.Length;
		}

		public static void WriteUInt16(byte[] buffer, int value, ref int offset)
		{
			for (int i = 0; i < 2; i++)
			{
				buffer[offset++] = (byte)(value >> (i * 8));
			}
		}

		public static void WriteInt32(byte[] buffer, int value,ref int offset)
		{
			for (int i = 0; i < 4; i++)
			{
				buffer[offset++] = (byte)(value >> (i * 8));
			}
		}

		public static void WriteInt64(byte[] buffer, int value, ref int offset)
		{
			for (int i = 0; i < 8; i++ )
			{
				buffer[offset++] = (byte) (value >> (i*8));
			}
		}

		public static void WriteString(byte[] buffer, string value, int length , ref int offset)
		{
			byte[] src = Encoding.Default.GetBytes(value);
			Array.Resize(ref src,length );

			WriteUInt16( buffer,length,ref offset );
			WriteBuffer( buffer,src,ref offset );
		}

	}
}