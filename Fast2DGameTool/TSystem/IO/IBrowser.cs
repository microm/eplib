using System.Collections.Generic;
using System.IO;
using Tool.TSystem;

namespace Tool.TSystem.IO
{
	public delegate void AsyncReadCallback(byte[] buffer);

	public interface IBrowser
    {
        FileSystem FileSystem { get; }

        Stream Write(IODataType dataType, string filePathFromDataType);
        Stream Read(IODataType dataType, string filePathFromDataType);
        byte[] ReadBytes(IODataType dataType, string filePathFromDataType);
        string ReadString(IODataType dataType, string filePathFromDataType);
        bool Exists(IODataType dataType, string filePathFromDataType);
        void Delete(IODataType dataType, string filePathFromDataType);

        string GetPath(IODataType dataType);
        string GetFileFullPath(IODataType dataType, string fileName);
		List<string> GetDirectoryList( string pathFromDataType, uint depth);
	    List<string> GetFileListContainName( string pathFromDataType, string name);
		long GetFileLength( string pathFromDataType);
		void CreateDirectory( string pathFromDataType);
	}
}