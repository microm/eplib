using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tool.TSystem.IO;
using System.Xml;
using System.Drawing;


namespace SpriteTool.Data
{ 

    public class ActorInfo
    {        

        private string m_name;
        private List<AnchorInfo> m_anchors;

        private SpriteInfo m_spriteInfo;
        
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public SpriteInfo SpriteInfo
        {
            get { return m_spriteInfo; }
            set { m_spriteInfo = value; }
        }

        public List<AnchorInfo> Anchors
        {
            get { return m_anchors; }
            set { m_anchors = value; }
        }

        public ActorInfo()
        {
            m_anchors = new List<AnchorInfo>();
        }

        public void Read(XmlNode spriteNode)
        {
            m_name = GenericXmlReader.ReadStringAttribute(spriteNode, "name");
            
            XmlNodeList imgNode = spriteNode.SelectNodes("anchor");
            foreach (XmlNode node in imgNode)
            {
                AnchorInfo anchor = AnchorInfo.Read(node);
                m_anchors.Add(anchor);
            }
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("actor");

            GenericXmlWriter.WriteAttribute(writer, "name", m_name);
            foreach (AnchorInfo anchor in m_anchors)
            {
                anchor.Write( writer );
            }
            writer.WriteEndElement();
        }
    }
}
