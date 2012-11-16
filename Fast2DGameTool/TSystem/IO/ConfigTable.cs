using System;
using System.IO;
using System.Xml;
using Tool.TSystem;

namespace Tool.TSystem.IO
{
	public class ConfigTable
	{
		private string[] m_paths = new string[(int)IODataType.Max];

		public ConfigTable()
		{
			Init();
		}

		public void Init()
		{
			for (int i = 0; i < m_paths.Length; i++)
			{
				m_paths[i] = string.Empty;
			}
		}

		public string GetDataTypePath(IODataType dataType)
		{
			return m_paths[(int)dataType];
		}

		public void SetDataTypePath(IODataType dataType, string dataTypePath)
		{
			m_paths[(int)dataType] = dataTypePath;//Path.Combine(m_dataDirectory, path);
		}

		public string GetPath(IODataType type)
		{
			return m_paths[(int)type];
		}

		public void LoadFromString(string contents)
		{
			XmlDocument doc = new XmlDocument();            
			doc.LoadXml(contents);
			try
			{
				ParsePath(doc);
			}
			catch
			{
				throw new ArgumentException("Data in ConfigFile is not Verified");
			}
		}

		private void ParsePath(XmlDocument doc)
		{
            XmlNode rootNode = doc.SelectSingleNode("configlist");

            XmlNodeList pathNode = rootNode.SelectNodes("path");
            foreach (XmlNode node in pathNode)
            {
                string key = GenericXmlReader.ReadStringAttribute(node, "key");
                string name = GenericXmlReader.ReadStringAttribute(node, "name");

                AddPath(key, name);
            }
        }
    
        private void AddPath( string key, string name )
        {
            for (int i = 0; i < (int)IODataType.Max; ++i)
            {   
                string field = ((IODataType)i).ToString();
                if (field == key)
                {
                    SetDataTypePath((IODataType)i, name);
                    return;
                }
            }
		}
	}
}