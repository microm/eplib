using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Tool.TSystem.IO;

namespace SpriteTool.Data.Control
{
    public class ButtonControl : ControlBase
    {
        public static int CountId = 1;

        private int m_downImage;
        private string m_clickEvent;
        private string m_text;

        public string Text
        {
            get { return m_text; }
            set { m_text = value; }
        }

        public int DownImage
        {
            get { return m_downImage; }
            set { m_downImage = value; }
        }       

        public string ClickEvent
        {
            get { return m_clickEvent; }
            set { m_clickEvent = value; }
        }

        public ButtonControl()
        {
            m_name = string.Format("Button_{0}", CountId++);
        }

        public override ControlType Type
        {
            get { return ControlType.Button; }
        }

        public override void ReadProperties(Main main, XmlNode node)
        {
            XmlNode propNode = node.SelectSingleNode("properties");

            m_downImage = GenericXmlReader.ReadIntAttribute(propNode, "downImage");
            m_text = GenericXmlReader.ReadStringAttribute(propNode, "text");
            m_clickEvent = GenericXmlReader.ReadStringAttribute(propNode, "clickevent");            
        }

        public override void WriteProperties(XmlWriter writer)
        {
            writer.WriteStartElement("properties");

            GenericXmlWriter.WriteAttribute(writer, "downImage", m_downImage);
            GenericXmlWriter.WriteAttribute(writer, "text", m_text);
            GenericXmlWriter.WriteAttribute(writer, "clickevent", m_clickEvent);
            
            writer.WriteEndElement();  
        }
    }
}
