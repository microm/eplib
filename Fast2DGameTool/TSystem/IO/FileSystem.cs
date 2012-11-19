using System;
using System.Collections.Generic;
using System.IO;
using File=Tool.TSystem.IO.File;

namespace Tool.TSystem.IO
{
	public class FileSystem
	{
		private string m_rootPath;

		public FileSystem(string rootPath)
		{
			if (Path.IsPathRooted(rootPath)) m_rootPath = rootPath;
			else m_rootPath = Path.GetFullPath(rootPath);
		}

        public FileSystem()
        {
            // TODO: Complete member initialization
        }

        public void SetPath(string dataPath)
        {
            m_rootPath = Path.Combine(m_rootPath, dataPath);
        }

		public bool Exists(string filePathFromRoot)
		{
			string fullPath = Path.Combine(m_rootPath, filePathFromRoot);
			return File.Exists(fullPath);
		}

        public bool DirectoryExists(string filePathFromRoot)
        {
            string fullPath = Path.Combine(m_rootPath, filePathFromRoot);
            return Directory.Exists(fullPath);
        }

		public void Delete(string filePathFromRoot)
		{
			string fullPath = Path.Combine(m_rootPath, filePathFromRoot);
			if (File.Exists(fullPath)) File.Delete(fullPath);
		}

		public Stream Create(string filePathFromRoot)
		{
			string fullPath = Path.Combine(m_rootPath, filePathFromRoot);
			if (File.Exists(fullPath)) File.Delete(fullPath);

			return new FileStream(fullPath, FileMode.Create, FileAccess.Write);
		}

		public Stream Get(string filePathFromRoot)
		{
			string fullPath = Path.Combine(m_rootPath, filePathFromRoot);
            if (!File.Exists(fullPath)) return null;
			
            return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
		}

		public string RootPath
		{
			get { return m_rootPath; }
		}

		public List<string> GetSubFileList(string pathFromRoot, string searchPattern)
		{
			string fullPath = Path.Combine(m_rootPath, pathFromRoot);
			
			int skipLength = fullPath.Length;
			if(fullPath.EndsWith("\\")==false) skipLength++;
			
			List<string> subFiles = new List<string>();
			CollectSubFilesFullPath(subFiles, fullPath, searchPattern);

			for (int i = 0; i < subFiles.Count; ++i)
			{
				subFiles[i] = subFiles[i].Substring(skipLength);
			}
				
			
			return subFiles;
		}

        public List<string> GetSubFileList2(string pathFromRoot, string searchPattern)
        {
            string fullPath = Path.Combine(m_rootPath, pathFromRoot);

            int skipLength = fullPath.Length;
            if (fullPath.EndsWith("\\") == false) skipLength++;

            List<string> subFiles = new List<string>();
            CollectSubFilesSearchPattenInDirectory(subFiles, fullPath, searchPattern);

            for (int i = 0; i < subFiles.Count; ++i)
            {
                subFiles[i] = subFiles[i].Substring(skipLength-1);
            }


            return subFiles;
        }

		public List<string> GetSubDirectoryList(string pathFromRoot, uint depth)
		{
			string fullPath = Path.Combine(m_rootPath, pathFromRoot);

			int skipLength = fullPath.Length;
			if (fullPath.EndsWith("\\") == false) skipLength++;
			
			List<string> subDirs = new List<string>();
			if (Directory.Exists(fullPath)) CollectSubDirectoriesFullPath(subDirs, fullPath, depth);

			for (int i = 0; i < subDirs.Count; ++i)
			{
				subDirs[i] = subDirs[i].Substring(skipLength);
			}
			
			return subDirs;
		}

		public long GetFileLength(string filePathFromRoot)
		{
			string fullPath = Path.Combine(m_rootPath, filePathFromRoot);
			FileInfo fileInfo = new FileInfo(fullPath);
			return fileInfo.Length;
		}

		public void CreateDirectory(string pathFromRoot)
		{
			string fullPath = Path.Combine(m_rootPath, pathFromRoot);
			Directory.CreateDirectory(fullPath);
		}

        

	    private static void CollectSubFilesFullPath(List<string> subFilesFullPath,
		                                string currentDirFullPath,
		                                string searchPattern)
		{
			subFilesFullPath.AddRange(Directory.GetFiles(currentDirFullPath, searchPattern));
			List<string> directories = new List<string>(Directory.GetDirectories(currentDirFullPath));
			foreach (string s in directories)
				CollectSubFilesFullPath(subFilesFullPath, s, searchPattern);
		}


        private static void CollectSubFilesSearchPattenInDirectory(List<string> subFilesFullPath,
                                        string currentDirFullPath,
                                        string searchPattern)
        {
            subFilesFullPath.AddRange(Directory.GetFiles(currentDirFullPath));
            for (int i = 0; i < subFilesFullPath.Count;)
            {
                string s = subFilesFullPath[i];
                if (s.Contains(searchPattern))
                {
                    ++i;
                }
                else
                {
                    subFilesFullPath.RemoveAt(i);
                }
            }
        }

        private static void CollectSubDirectoriesFullPath(List<string> subDirsFullPath,
		                                      string currentDirFullPath,
		                                      uint recursiveDepth)
		{
			if(recursiveDepth==0) return;
			--recursiveDepth;
			
			List<string> subDirs = new List<string>(Directory.GetDirectories(currentDirFullPath));
			foreach (string s in subDirs)
			{
				if (Path.GetFileName(s)== ".svn") continue;
				
				subDirsFullPath.Add(s);
				if(recursiveDepth>0) CollectSubDirectoriesFullPath(subDirsFullPath, s, recursiveDepth);
			}
		}
	}
}