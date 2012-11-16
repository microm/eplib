using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using System.Runtime.InteropServices;

namespace Tool.TSystem.Res
{
	public class Reader
	{
		int m_position = 0;

		private BinaryReader m_br = null;
		private FileStream m_fs = null;

		private long m_Length = -1;
		private bool m_bOpen = false;

		public Reader( )
		{
		}

		public bool open
		{
			get { return m_bOpen; }
		}

		public void Open( string path )
		{
			m_fs = new FileStream(path, FileMode.Open, FileAccess.Read);
			m_br = new BinaryReader(m_fs);
			m_Length = m_fs.Length;

			m_bOpen = true;
		}

		public T Read<T>() where T : new() 
		{
			int size = Marshal.SizeOf(typeof(T));
			byte[] buff = m_br.ReadBytes(size);
			if (buff.Length != size)
			{
				Console.WriteLine("Error writing the data.");
			}

			GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
			T s = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
			handle.Free();

			m_position += size;
			return s;
		}

		public T ReadStream<T>() where T : new()
		{
			int size = Marshal.SizeOf(typeof(T));
			byte[] buff = new byte[size];

			int amt = 0;
			while (amt < buff.Length)
				amt += m_fs.Read(buff, amt, buff.Length - amt);

			GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
			T s = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
			handle.Free();

			m_position += size;
			return s;
		}

		public T[] ReadArray<T>( int num ) where T : new()  
		{
			int size = Marshal.SizeOf(typeof(T));

			T[] array = new T[num];

			for ( int i=0;i<num ; i++ )
			{
				byte[] buff = m_br.ReadBytes(size);
				if (buff.Length != size)
				{
					Console.WriteLine("Error writing the data.");					
				}

				GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
				T s = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
				
				array[i]= s;
				
				handle.Free();
			}
			m_position += size * num;

			return array;
		}

		public void ReadInt(ref int value)
		{
			value = m_br.ReadInt32();
			m_position += 4;
		}

		public void ReadUInt(ref uint value)
		{
			value = m_br.ReadUInt32();
			m_position += 4;
		}		

		public string ReadString()
		{
			string res = m_br.ReadString();
			m_position += res.Length;
			return res;
		}

		public void Close()
		{
			m_bOpen = false;

			if (m_br != null)
			{
				m_br.Close();
				m_br = null;
			}
			if (m_fs != null)
			{
				m_fs.Close();
				m_fs = null;
			}
		}


		public static byte[] ToByteArray<T>(T data) where T : new() 
		{
			int size = Marshal.SizeOf(typeof(T));

			byte[] buff = new byte[size];
			GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
			Marshal.StructureToPtr( data , handle.AddrOfPinnedObject(), false);
			handle.Free();

			return buff;
		}

	}
}
