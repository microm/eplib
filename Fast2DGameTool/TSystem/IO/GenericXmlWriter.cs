using System.Xml;
using Tool.TSystem.Primitive;

namespace Tool.TSystem.IO
{
	public class GenericXmlWriter
	{
		public static void WriteLeafElement<T>(XmlWriter writer, string name, T value)
		{
			writer.WriteStartElement(name);
			writer.WriteValue(value.ToString());
			writer.WriteEndElement();
		}

		public static void WriteAttribute<T>(XmlWriter writer, string name, T value)
		{
			writer.WriteStartAttribute(name);
			writer.WriteValue(value);
			writer.WriteEndAttribute();
		}
	}
}