using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tool.TSystem.Primitive;
using Tool.TSystem.IO;
using System.Xml;

namespace SpriteTool.Data
{
    public class ImgData
    {
        public TPoint Pivot;
        public Rect Region;
    }

    public class SpriteInfo
    {
        private string m_name;
        private SpriteTool.Data.SpriteMap.E_Entity m_cate;       
        private string m_path;     

        private bool m_hasColorKey;
        private Color m_colorKey;
        private float m_speed;
        private bool m_isCircle;
        private bool m_isParts;     
        
        private List<ImgData> m_imgList;                
        public List<ImgData> ImgList
        {
            get { return m_imgList; }
            set { m_imgList = value; }
        }

        public SpriteTool.Data.SpriteMap.E_Entity Cate
        {
            get { return m_cate; }
            set { m_cate = value; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Path
        {
            get { return m_path; }
            set { m_path = value; }
        }

        public bool IsCircle
        {
            get { return m_isCircle; }
            set { m_isCircle = value; }
        }

        public bool HasColorKey
        {
            get { return m_hasColorKey; }
            set { m_hasColorKey = value; }
        }
        
        public bool IsParts
        {
            get { return m_isParts; }
            set { m_isParts = value; }
        }

        public System.Drawing.Color ColorKey
        {
            get { return m_colorKey; }
            set { m_colorKey = value;
                m_hasColorKey = true;
            }
        }

        public float Speed
        {
            get { return m_speed; }
            set { m_speed = value; }
        }

        public SpriteInfo()
        {
            m_imgList = new List<ImgData>();
        }

        public void Read(XmlNode spriteNode)
        {
            m_name = GenericXmlReader.ReadStringAttribute(spriteNode, "name");
            m_path = GenericXmlReader.ReadStringAttribute(spriteNode, "path");
            m_hasColorKey = GenericXmlReader.IsExistAttribute( spriteNode , "colorKey" );
            if ( m_hasColorKey )
            {
                m_colorKey = GenericXmlReader.ReadColorAttribute(spriteNode, "colorKey");                
            }
            if (GenericXmlReader.IsExistAttribute(spriteNode, "parts"))
            {
                m_isParts = GenericXmlReader.ReadBoolAttribute(spriteNode, "parts");
            }
            if (GenericXmlReader.IsExistAttribute(spriteNode, "speed"))
            {
                m_speed = GenericXmlReader.ReadFloatAttribute(spriteNode, "speed");
                m_isCircle = GenericXmlReader.ReadBoolAttribute(spriteNode, "circle");
            }
            else
            {
                m_speed = 0;
                m_isCircle = false;
            }

            XmlNodeList imgNode = spriteNode.SelectNodes("img");
            foreach (XmlNode node in imgNode)
            {
                ImgData img = new ImgData();

                img.Pivot = GenericXmlReader.ReadPointAttribute(node, "pivot");
                img.Region = GenericXmlReader.ReadRectAttribute(node, "region");
                m_imgList.Add(img);
            }
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("sprite");

            GenericXmlWriter.WriteAttribute(writer, "name", m_name );
            GenericXmlWriter.WriteAttribute(writer, "path", m_path);
            
            if (m_speed > 0)
            {
                GenericXmlWriter.WriteAttribute(writer, "speed", m_speed);
                GenericXmlWriter.WriteAttribute(writer, "circle", m_isCircle);
            }
            GenericXmlWriter.WriteAttribute(writer, "parts", m_isParts);
            if (m_hasColorKey)
            {
                GenericXmlWriter.WriteAttribute(writer, "colorKey", string.Format("{0},{1},{2}", m_colorKey.R, m_colorKey.G, m_colorKey.B ));                
            }

            foreach (ImgData img in m_imgList)
            {
                writer.WriteStartElement("img");

                GenericXmlWriter.WriteAttribute(writer, "pivot", img.Pivot.ToString());
                GenericXmlWriter.WriteAttribute(writer, "region", img.Region.ToString());

                writer.WriteEndElement();
            } 

            writer.WriteEndElement();
        }

        internal ImgData AddRegion(System.Drawing.Point start, System.Drawing.Point end)
        {
            ImgData img = new ImgData();

            img.Region = new Tool.TSystem.Primitive.Rect(start.X, start.Y, end.X, end.Y);
            img.Pivot = new TPoint(img.Region.Width / 2, img.Region.Height / 2);

            ImgList.Add(img);

            return img;
        }

        internal void RemoveRegion(int selectIndex)
        {
            ImgList.RemoveAt(selectIndex);
        }
    }
}
