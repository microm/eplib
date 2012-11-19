using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Tool.TSystem.Primitive;
using System.Drawing;

namespace Tool.TSystem.IO
{
	public static class GenericXmlReader
	{
		public static string ReadString(XmlNode node, string name)
		{
			XmlNode nameNode = node.SelectSingleNode(name);
			return nameNode.InnerText;
		}

		public static int ReadInt(XmlNode node, string name)
		{
			XmlNode nameNode = node.SelectSingleNode(name);
			return int.Parse(nameNode.InnerText);
		}
        
		public static bool IsExistAttribute(XmlNode node, string name)
		{
			return node.Attributes[name] != null;
		}

		public static int ReadIntAttribute(XmlNode node, string name)
		{
			return int.Parse(ReadStringAttribute(node, name));
		}

        public static int ReadHexAttribute(XmlNode node, string name)
        {
            string str = ReadStringAttribute(node, name);
            return int.Parse(str,NumberStyles.HexNumber);
        }

		public static string ReadStringAttribute(XmlNode node, string name)
		{
			return node.Attributes[name].InnerText;
		}

        public static Color ReadColorAttribute(XmlNode node, string name)
        {
            string strline = node.Attributes[name].InnerText;

            string[] array = strline.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int[] values = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                values[i] = int.Parse(array[i]);
            }
            return Color.FromArgb(values[0], values[1], values[2]);
        }

        public static TPoint ReadPointAttribute(XmlNode node, string name)
        {
            string strline = node.Attributes[name].InnerText;
            return TPoint.Parse( strline );
        }

        public static Rect ReadRectAttribute(XmlNode node, string name)
        {
            string strline = node.Attributes[name].InnerText;
                        
            return Rect.Parse(strline);
        }

		public static float[] ReadFloatArrayInAttribute(XmlNode node, int count, string name)
		{
			float[] values = new float[count];

			string strline = node.Attributes[ name ].InnerText;

			string[] array = strline.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != count) throw new FileLoadException("Float array 정보가 잘못됨");

			for (int i = 0; i < count; i++)
			{
				values[i] = float.Parse(array[i]);
			}
			return values;
		}

		public static int[] ReadIntArrayInAttribute(XmlNode node, string name)
		{
			string strline = node.Attributes[name].InnerText;

			string[] array = strline.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
			int[] values = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				values[i] = int.Parse(array[i]);
			}
			return values;
		}

        public static int[] ReadIntToHexArrayInAttribute(XmlNode node, string name)
        {
            string strline = node.Attributes[name].InnerText;

            string[] array = strline.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int[] values = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                values[i] = int.Parse(array[i], NumberStyles.HexNumber);
            }
            return values;
        }

		public static float ReadFloat(XmlNode node, string name)
		{
			XmlNode nameNode = node.SelectSingleNode(name);
			return float.Parse(nameNode.InnerText);
		}
        
		public static float ReadFloatAttribute(XmlNode node, string name)
		{
			return float.Parse(ReadStringAttribute(node, name));
		}

	    public static bool ReadBoolAttribute(XmlNode node, string name)
	    {
            if (node.Attributes[name] == null) return false;

	        return (node.Attributes[name].InnerText == "true") ? true : false;
	    }
        public static bool ReadBool(XmlNode node, string name)
        {
            XmlNode nameNode = node.SelectSingleNode(name);
            return bool.Parse(nameNode.InnerText);
        }

	    public static int ReadHexInt(XmlNode node, string name)
	    {
            XmlNode nameNode = node.SelectSingleNode(name);
            return int.Parse(nameNode.InnerText, NumberStyles.HexNumber);
	    }
	}
}