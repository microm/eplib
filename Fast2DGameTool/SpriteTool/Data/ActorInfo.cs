using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPoint = Tool.TSystem.Primitive.Point;
using Tool.TSystem.IO;
using System.Xml;
using System.Drawing;


namespace SpriteTool.Data
{
    public class ActorInfo
    {
        public class Anchor
        {            
            public int Index;
            public TPoint Offset;
            public bool bXFlip = false;
            public bool bYFlip = false;
            public int ZOrder = 3;

            private Bitmap m_bmp;

	        public System.Drawing.Bitmap Bmp
	        {
		        get { return m_bmp; }
		        set { m_bmp = value; }
	        }

            public Anchor(int _index) 
            {
                Index = _index;
                Offset = new TPoint(0, 0);
            }
        }

        private string m_name;        
        private List<Anchor> m_anchors;

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

        public List<Anchor> Anchors
        {
            get { return m_anchors; }
            set { m_anchors = value; }
        }

        public ActorInfo()
        {
            m_anchors = new List<Anchor>();
        }

        public void Read(XmlNode spriteNode)
        {
            m_name = GenericXmlReader.ReadStringAttribute(spriteNode, "name");
            
            XmlNodeList imgNode = spriteNode.SelectNodes("anchor");
            foreach (XmlNode node in imgNode)
            {
                int index = GenericXmlReader.ReadIntAttribute(node, "index");
                Anchor anchor = new Anchor(index);

                anchor.Offset = GenericXmlReader.ReadPointAttribute(node, "offset");
                anchor.bXFlip = GenericXmlReader.ReadBoolAttribute(node, "xflip");
                anchor.bYFlip = GenericXmlReader.ReadBoolAttribute(node, "yflip");
                anchor.ZOrder = GenericXmlReader.ReadIntAttribute(node, "zorder");
                m_anchors.Add(anchor);
            }
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("actor");

            GenericXmlWriter.WriteAttribute(writer, "name", m_name);

            foreach (Anchor anchor in m_anchors)
            {
                writer.WriteStartElement("anchor");

                GenericXmlWriter.WriteAttribute(writer, "index", anchor.Index );
                GenericXmlWriter.WriteAttribute(writer, "offset", anchor.Offset.ToString());
                GenericXmlWriter.WriteAttribute(writer, "xflip", anchor.bXFlip);
                GenericXmlWriter.WriteAttribute(writer, "yflip", anchor.bYFlip);
                GenericXmlWriter.WriteAttribute(writer, "zorder", anchor.ZOrder);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }
    }
}
