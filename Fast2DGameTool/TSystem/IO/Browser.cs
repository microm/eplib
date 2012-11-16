using System.Collections.Generic;
using System.IO;
using System.Text;
using Tool.TSystem;
using Tool.TSystem.IO;

namespace Tool.TSystem.IO
{
	public class Browser : IBrowser
	{
		private readonly FileSystem m_fileSystem;
		private readonly ConfigTable m_configTable;

		public Browser()
		{
			m_fileSystem = new FileSystem(".");

			if (FileSystem.Exists("configure.xml"))
			{
				Stream stream = FileSystem.Get("configure.xml");
				byte[] byteContents = new byte[stream.Length];
				stream.Read(byteContents, 0, (int)stream.Length);
				string contents = Encoding.Default.GetString(byteContents);

				m_configTable = new ConfigTable();
				m_configTable.LoadFromString(contents);
				stream.Close();

                m_fileSystem.SetPath( m_configTable.GetPath(IODataType.Root) );
			}
			else
			{
				m_configTable = new ConfigTable();
			}
		}
        
	    public FileSystem FileSystem
	    {
	        get { return m_fileSystem; }
	    }

        public Stream Write(IODataType dataType, string filePathFromDataType)
		{
            string path = GetFileFullPath(dataType, filePathFromDataType);
            return FileSystem.Create(path);
		}

        public Stream Read(IODataType dataType, string filePathFromDataType)
		{
            string path = GetFileFullPath(dataType, filePathFromDataType);
            return FileSystem.Get(path);
		}

		public void CreateDirectory(string pathFromDataType)
		{
			FileSystem.CreateDirectory(pathFromDataType);
		}

        public string GetPath(IODataType dataType)
        {
            return Path.Combine(FileSystem.RootPath, m_configTable.GetDataTypePath(dataType));
        }

        public byte[] ReadBytes(IODataType dataType, string filePathFromDataType)
		{
            Stream stream = Read(dataType, filePathFromDataType);
			byte[] buffer = new byte[stream.Length];
			stream.Read(buffer, 0, buffer.Length);
			stream.Close();

			return buffer;
		}

        public string ReadString(IODataType dataType, string filePathFromDataType)
		{
            byte[] bytes = ReadBytes(dataType, filePathFromDataType);
			return Encoding.Default.GetString(bytes);
		}

        public bool Exists(IODataType dataType, string filePathFromDataType)
		{
            string path = GetFileFullPath(dataType, filePathFromDataType);
            return FileSystem.Exists(path);
		}

        public void Delete(IODataType dataType, string filePathFromDataType)
		{
            string path = GetFileFullPath(dataType, filePathFromDataType);
            if (FileSystem.Exists(path)) FileSystem.Delete(path);
		}

        public string GetFileFullPath(IODataType dataType, string fileName)
		{
            return Path.Combine(GetPath(dataType), fileName);
		}

        public List<string> GetDirectoryList(string pathFromRoot, uint depth)
		{
			List<string> subDirectoryList = FileSystem.GetSubDirectoryList(pathFromRoot, depth);
			return subDirectoryList;
		}

        public List<string> GetFileListContainName(string datatTypePathFromRoot, string name)
        {
            List<string> subFileList = FileSystem.GetSubFileList2(datatTypePathFromRoot, name);

            return subFileList;
        }

        public long GetFileLength(string pathFromRoot)
		{
			return FileSystem.GetFileLength(pathFromRoot);
		}
	}
}