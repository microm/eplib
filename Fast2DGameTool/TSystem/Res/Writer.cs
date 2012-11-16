using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tool.TSystem.Res
{
	static class Writer
	{
		public static void Execute(string outputFileName, IList<string> contents)
		{
			//Path.GetDirectoryName(outputFileName);
			Stream stream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write);
			StreamWriter writer = new StreamWriter(stream, Encoding.Default);

			foreach (string content in contents)
			{
				writer.WriteLine(content);
			}

			writer.Flush();
			stream.Close();
		}
	}
}
