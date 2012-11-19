using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Point = Tool.TSystem.Primitive.TPoint;


namespace Tool.TSystem.Assist
{
	public class IniReadWriter
	{
		private string fileName;
		private int charSize;
		
		public string FileName
		{
			get { return fileName; }
			set { fileName = value; }
		}
		
		// ---- ini 파일 의 읽고 쓰기를 위한 API 함수 선언 ----
		[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW",
					SetLastError = true,
					CharSet = CharSet.Unicode, ExactSpelling = true,
					CallingConvention = CallingConvention.StdCall)]
		private static extern int GetPrivateProfileString(
					string lpAppName,
					string lpKeyName,
					string lpDefault,
					string lpReturnString,
					int nSize,
					string lpFilename);

		[DllImport("KERNEL32.DLL", EntryPoint = "WritePrivateProfileStringW",
					SetLastError = true,
					CharSet = CharSet.Unicode, ExactSpelling = true,
					CallingConvention = CallingConvention.StdCall)]
		private static extern int WritePrivateProfileString(
					string lpAppName,
					string lpKeyName,
					string lpString,
					string lpFilename);

		public IniReadWriter()
		{
			FileName = "";
			charSize = 1024;
		}

		public IniReadWriter(string value)
		{
			string path = Directory.GetCurrentDirectory() + "\\";
			FileName = path + value;
			charSize = 1024;
		}

		public string IniReadString(string Section, string Key)
		{
			return IniReadString(Section, Key, "");
		}

		private string ConvertString( string value )
		{
			char[] charsToTrim = { ' ', '\0' };
			
			return value.Trim(charsToTrim);
		}

		public string IniReadString(string Section, string Key, string defaultValue)
		{
			string temp = new string(' ', charSize);
			GetPrivateProfileString(Section, Key, defaultValue, temp, charSize, FileName);

			return ConvertString(temp);
		}

		public void IniWriterString(string Section, string Key, string Value)
		{
			WritePrivateProfileString(Section, Key, Value, FileName);
		}

		public int IniReadInt(string Section, string Key)
		{
			return IniReadInt( Section, Key, 0 );
		}

		public int IniReadInt( string Section, string Key, int defaultValue )
		{
			string temp = new string(' ', charSize);
			GetPrivateProfileString(Section, Key, defaultValue.ToString(), temp, charSize, FileName);
			return int.Parse(ConvertString(temp));
		}

		public void IniWriterInt(string Section, string Key, int Value)
		{
			WritePrivateProfileString(Section, Key, Value.ToString(), FileName);
		}

		public Point IniReadPoint(string Section, string Key)
		{
			return IniReadPoint(Section, Key, new Point(0,0));
		}

		public Point IniReadPoint(string Section, string Key, Point defaultValue)
		{
			string temp = new string(' ', charSize);
			GetPrivateProfileString(Section, Key, defaultValue.ToString(), temp, charSize, FileName);
			return Point.Parse(ConvertString(temp));
		}

		public void IniWriterPoint(string Section, string Key, Point Value)
		{
			WritePrivateProfileString(Section, Key, Value.ToString(), FileName);
		}

		public bool IniReadBool(string Section, string Key)
		{
			return IniReadBool(Section, Key, false);
		}

		public bool IniReadBool(string Section, string Key, bool defaultValue)
		{
			string temp = new string(' ', charSize);
			GetPrivateProfileString(Section, Key, defaultValue.ToString(), temp, charSize, FileName);
			string strValue = ConvertString(temp);

			if (strValue == true.ToString() )
			{
				return true;
			}
			else
			{
				return false;
			}		
		}

		public void IniWriterBool(string Section, string Key, bool bValue)
		{
			WritePrivateProfileString(Section, Key, bValue.ToString(), FileName);
		}

		public float IniReadFloat(string Section, string Key)
		{
			return IniReadFloat(Section, Key, 0.0f);
		}

		public float IniReadFloat(string Section, string Key, float defaultValue)
		{
			string temp = new string(' ', charSize);
			GetPrivateProfileString(Section, Key, defaultValue.ToString(), temp, charSize, FileName);
			return float.Parse(ConvertString(temp));
		}

		public void IniWriterFloat(string Section, string Key, float Value)
		{
			WritePrivateProfileString(Section, Key, Value.ToString(), FileName);
		}








		/// ini파일에 쓰기
		public void G_IniWriteValue(String Section, String Key, String Value, string avsPath)
		{
			WritePrivateProfileString(Section, Key, Value, avsPath);
		}

		/// ini파일에서 읽어 오기
		public String G_IniReadValue(string Section, string Key, string avsPath)
		{
			StringBuilder temp = new StringBuilder(2000);
			int i = GetPrivateProfileString(Section, Key, "", temp.ToString(), 2000, avsPath);
			return temp.ToString();
		}

	}
}
