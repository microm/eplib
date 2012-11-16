using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Tool.TSystem.IO
{
	public class BinarySaver
	{
		private Stream m_stream;
		private BinaryWriter m_writer;

		public BinarySaver(Stream stream)
		{
			m_stream = stream;
			m_writer = new BinaryWriter(m_stream);
		}

		public Stream Stream
		{
			get { return m_stream; }
		}

		public bool IsOpen
		{
			get { return m_stream.CanRead; }
		}

		public long Position
		{
			get { return m_stream.Position; }
			set { m_stream.Position = value; }
		}

		public void Write(Int32 value)
		{
			m_writer.Write(value);
		}

		public void Write(byte[] value)
		{
			m_writer.Write(value);
		}

		public void Write(Int16 value)
		{
			m_writer.Write(value);
		}

		public void Write(Int64 value)
		{
			m_writer.Write(value);
		}

        public void Write(bool value)
        {
            m_writer.Write(value);
        }

		public void Write(string value)
		{
			byte[] src = Encoding.Default.GetBytes(value);
                
			Write((Int16) src.Length);
			Write(src);
		}

		public void Write<T>(T data) where T : new()
		{
			int size = Marshal.SizeOf(typeof (T));

			byte[] buff = new byte[size];
			GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
			Marshal.StructureToPtr(data, handle.AddrOfPinnedObject(), false);
			handle.Free();

			//buff를 stream에 쓴다
			m_writer.Write(buff);
		}

		public void WriteArray<T>(T[] data) where T : new()
		{
			int count = data.Length;
			for (int i = 0; i < count; i++)
			{
				Write<T>(data[i]);
			}
		}

		public void Flush()
		{
			m_writer.Flush();
		}

		public void Close()
		{
			m_writer.Close();
		}
	}
}