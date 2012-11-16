using System;
using System.Diagnostics;
using System.IO;

namespace Tool.TSystem.IO
{
	public sealed class File
	{
		public static Stream LoadToStream(string path)
		{
			byte[] contentBytes = System.IO.File.ReadAllBytes(path);

			//File Data를 Copy해서 보낸다.
			Stream stream = new MemoryStream();
			stream.Write(contentBytes, 0, contentBytes.Length);
			stream.Flush();
			stream.Position = 0;

			return stream;
		}

		public static String ReadAllText(string path)
		{
			return System.IO.File.ReadAllText(path);
		}

		public static byte[] ReadAllBytes(string path)
		{
			return System.IO.File.ReadAllBytes(path);
		}
        
	    public static void Save(string path, Stream contents)
		{
			if( contents.Position != 0) contents.Position = 0;

			BinaryReader reader = new BinaryReader(contents);
			Save(path, reader.ReadBytes((int)contents.Length));
		}

		public static void Save(string path, string contents)
		{
			try
			{
				System.IO.File.WriteAllText(path, contents);
			}
			catch (Exception e)
			{
				Debug.Print(e.ToString());
			}
		}

		public static void Save(string path, byte[] buffers)
		{
			try
			{
				System.IO.File.WriteAllBytes(path, buffers);
			}
			catch (Exception e)
			{
				Debug.Print(e.ToString());
			}
		}

		public static void Seek(Stream stream, long current, long offset)
		{
			if (offset < current)
			{
				throw new FormatException();
			}
			else if (offset == current)
			{
				return;
			}
			if (stream.CanSeek)
			{
				stream.Seek(offset, SeekOrigin.Begin);
			}
			else
			{
				byte[] buffer = new byte[1024];

				while (current < offset)
				{
					int len;
					if ((offset - current) < 1024)
					{
						len = (int)(offset - current);
					}
					else
					{
						len = 1024;
					}
					if (stream.Read(buffer, 0, len) != len)
					{
						throw new FormatException();
					}
				}
			}
		}

		public static bool Exists(string path)
		{
			return System.IO.File.Exists(path);
		}

		public static void Delete(string path)
		{
			System.IO.File.Delete(path);
		}
	}
}