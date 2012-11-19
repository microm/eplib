using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using Tool.TSystem;
using Point = Tool.TSystem.Primitive.TPoint;

namespace Tool.TSystem.Assist
{
	public class Register
	{
		private RegistryKey toolKey;

		public RegistryKey ToolKey
		{
			get { return toolKey; }
		}

		public void Close()
		{
            toolKey.Close();
		}

		public Register( string editerName )
		{
			RegistryKey sw = Registry.CurrentUser.OpenSubKey("software", true);
            RegistryKey tool = GetSubKey(sw, "FastGameMaker");
			toolKey = GetSubKey(tool, editerName);
		}

		private RegistryKey GetSubKey( RegistryKey key, string keyName )
		{
			RegistryKey subKey = key.OpenSubKey(keyName, true);

			if( subKey == null )
			{
				subKey = key.CreateSubKey(keyName);
			}

			return subKey;
		}

		public void SetPoint(string keyName, Point value)
		{
			if (toolKey == null) return;
			toolKey.SetValue(keyName, value);
		}

		public Point GetPoint( string keyName, string defaultValue )
		{
			return Point.Parse((string)toolKey.GetValue(keyName, defaultValue));
		}

		public void SetBool( string keyName, bool value )
		{
			if (toolKey == null) return;
			if (value) { toolKey.SetValue(keyName, 1); }
			else { toolKey.SetValue(keyName, 0); }
		}

		public bool GetBool(string keyName, string defaultValue )
		{
			return bool.Parse((string)toolKey.GetValue(keyName, defaultValue));
		}

		public void SetInt(string keyName, int value)
		{
			if (toolKey == null) return;
			toolKey.SetValue(keyName, value);
		}

		public int GetInt(string keyName, string defaultValue )
		{
			return int.Parse((string)toolKey.GetValue(keyName, defaultValue));
		}

		public void SetFloat(string keyName, float value)
		{
			if (toolKey == null) return;
			toolKey.SetValue(keyName, value);
		}

		public float GetFloat(string keyName, string defaultValue )
		{
			return float.Parse((string)toolKey.GetValue(keyName, defaultValue));
		}

		public void DeleteKey( string keyName )
		{
			if (toolKey == null) return;
			toolKey.DeleteSubKey(keyName);
		}

		public void DeleteTreeKey( string keyName )
		{
			if (toolKey == null) return;
			toolKey.DeleteSubKeyTree(keyName);
		}
	}
}
