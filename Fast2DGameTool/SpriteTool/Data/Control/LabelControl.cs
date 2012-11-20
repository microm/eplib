using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Tool.TSystem.IO;


namespace SpriteTool.Data.Control
{
    public class LabelControl : ControlBase
    {
        public static int CountId = 1;
        private string m_text;

        public string Text
        {
            get { return m_text; }
            set { m_text = value; }
        }

        public LabelControl()
        {
            m_name = string.Format("Label_{0}", CountId++);
        }

        public override ControlType Type
        {
            get { return ControlType.Label; }
        }

        public override void ReadProperties(Main main, XmlNode node)
        {
            XmlNode propNode = node.SelectSingleNode("properties");

            m_text = GenericXmlReader.ReadStringAttribute(propNode, "text");
        }


        public override void WriteProperties(XmlWriter writer)
        {
            writer.WriteStartElement("properties");
            GenericXmlWriter.WriteAttribute(writer, "text", m_text);
            writer.WriteEndElement();
        }
    }
}
