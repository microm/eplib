

using System.Runtime.InteropServices;
using System.Text;

namespace Tool.TSystem.IO
{
    public static class IniFile
    {
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static string Read(string section, string key, string iniPath)
        {
            StringBuilder temp = new StringBuilder(255);

            GetPrivateProfileString(section, key, "", temp, 255, iniPath);

            return temp.ToString();
        }


        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        public static void Write(string section, string key, string value, string iniPath)
        {
            WritePrivateProfileString(section, key, value, iniPath);
        }
    }
}
