using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tool.TSystem.Res
{
    public class FileInfos
    {
        public List<string> lstFiles;
        public List<string> lstDirs;

        public FileInfos()
        {
            lstDirs = new List<string>();
            lstFiles = new List<string>();
        }

        public void Clear()
        {
            lstFiles.Clear();
            lstDirs.Clear();
        }
    }

	public class FileExplorer
	{
        public static string ValidPath(string path)
        {
            return path.ToLower().Replace('\\', '/');
        }

		public static void GetFilesInDirectory(string rootDirectory, string option , FileInfos fileinfo )
		{
			string[] fileList = Directory.GetFiles(rootDirectory, option, SearchOption.AllDirectories);
			for (int i = 0; i < fileList.Length; i++)
			{
                fileinfo.lstFiles.Add( ValidPath(fileList[i]) );
			}
		}

        public static string Strip_Folder(string i_strPath)
        {
            int nStart = i_strPath.LastIndexOf('/');
            if (nStart < 0)
            {
                return i_strPath;
            }
            nStart++;
            
            return i_strPath.Substring(nStart, i_strPath.Length - nStart);            
        }

        public static int Find_Folder(string i_strPath, int i_nCount, FileInfos fileinfo)
        {
            string[] dirs = Directory.GetDirectories(i_strPath, "*.*");
            foreach (string dir in dirs)
            {
                if (dir.IndexOf(".svn") != 0) //svn 폴더 제외
                {
                    continue;
                }
                ++i_nCount;
                fileinfo.lstDirs.Add(dir);
                i_nCount = Find_Folder( dir, i_nCount, fileinfo);
            }
            return i_nCount;
        }  
	}
}
