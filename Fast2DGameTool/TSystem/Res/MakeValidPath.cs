using System;
using System.Collections.Generic;
using System.Text;

namespace Tool.TSystem.Res
{
	static class MakeValidPath
	{
		public static string[] Execute(string basePath, IList<string> fileNames)
		{
			string extractPath = GetExtractPath(ref basePath);

			List<string> validPathCollection = new List<string>();

			foreach (string name in fileNames)
			{
				string filePath = ConvertToStandardPath(name);

				string extractedPath = ExtractPath(filePath, extractPath);
				if (extractPath == "") continue;
				validPathCollection.Add(extractedPath);

				//if (filePath.IndexOf(extractPath) == -1) continue;
				//validPathCollection.Add(filePath.Substring(extractPath.Length));
			}

			return validPathCollection.ToArray();
		}

		private static string ExtractPath(string path, string extractPath)
		{
			if (path.IndexOf(extractPath) == -1) return "";

			return path.Substring(extractPath.Length);
		}

		private static string GetExtractPath(ref string basePath)
		{
			basePath = ConvertToStandardPath(basePath);
			basePath = RemoveLastSeprator(basePath);
			string extractPath = basePath + "/data/texture/";
			return extractPath;
		}

		private static string RemoveLastSeprator(string basePath)
		{
			if (basePath[basePath.Length - 1] == '/' ||
				basePath[basePath.Length - 1] == '\\')
			{
				basePath = basePath.Substring(0, basePath.Length - 1);
			}
			return basePath;
		}

		private static string ConvertToStandardPath(string name)
		{
			string result = name.ToLower();
			result = result.Replace('\\', '/');
			return result;
		}
	}
}
