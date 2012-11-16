using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using File=Tool.TSystem.IO.File;

namespace Tool.TSystem.IO
{
	public class BinaryLoader
	{
		//int m_position = 0;

		private BinaryReader m_binaryReader = null;
		private Stream m_stream = null;

		private bool m_isOpen = false;

		public long Position
		{
			get { return m_stream.Position; }
			set { m_stream.Position = value; }
		}

		public bool IsOpen
		{
			get { return m_isOpen; }
		}

		public void Open(string path)
		{
			m_stream = File.LoadToStream(path);
			m_binaryReader = new BinaryReader(m_stream);

			m_isOpen = true;
		}

		public void Open(Stream stream)
		{
			m_stream = stream;
			m_binaryReader = new BinaryReader(m_stream);

			m_isOpen = true;
		}

		public T Read<T>() where T : new()
		{
			int size = Marshal.SizeOf(typeof (T));
			byte[] buff = m_binaryReader.ReadBytes(size);
			if (buff.Length != size)
			{
				Debug.Print("Error writing the data.");
			}

			GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
			T s = (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof (T));
			handle.Free();

			//m_position += size;
			return s;
		}

		public T ReadStream<T>() where T : new()
		{
			int size = Marshal.SizeOf(typeof (T));
			byte[] buff = new byte[size];

			int amt = 0;
			while (amt < buff.Length)
				amt += m_stream.Read(buff, amt, buff.Length - amt);

			GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
			T s = (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof (T));
			handle.Free();

			//m_position += size;
			return s;
		}

		public T[] ReadArray<T>(int num) where T : new()
		{
			///
			/// Slow.
			/// 
			int size = Marshal.SizeOf(typeof (T));
			T[] array = new T[num];
			byte[] buff = m_binaryReader.ReadBytes(size*num);

			GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
			for (int i = 0; i < num; i++)
			{
				IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement((Array) handle.Target, i*size);
				array[i] = (T) Marshal.PtrToStructure(ptr, typeof (T));
			}

			handle.Free();
			return array;
		}

		public Int16 ReadInt16()
		{
			//m_position += 2;
			return m_binaryReader.ReadInt16();
		}

		public Int32 ReadInt32()
		{
			//m_position += 4;
			return m_binaryReader.ReadInt32();
		}

		public Int64 ReadInt64()
		{
			//m_position += 8;
			return m_binaryReader.ReadInt64();
		}

        public bool ReadBoolean()
        {
            return m_binaryReader.ReadBoolean();
        }

		public float ReadSingle()
		{
			return m_binaryReader.ReadSingle();
		}

		public byte ReadByte()
		{
			//m_position += 1;

			return m_binaryReader.ReadByte();
		}

		public byte[] ReadBytes(int count)
		{
			//m_position += count;
			return m_binaryReader.ReadBytes(count);
		}

		public string ReadString()
		{
			int size = m_binaryReader.ReadUInt16();
			char[] res = m_binaryReader.ReadChars(size);
			//m_position += 2 + res.HalfLength;
			string result = new string(res);

			return result.TrimEnd('\0');
		}

		public void Close()
		{
			m_isOpen = false;
			if (m_binaryReader != null)
			{
				m_binaryReader.Close();
				m_binaryReader = null;
			}
			if (m_stream != null)
			{
				m_stream.Close();
				m_stream = null;
			}
		}
	}
}