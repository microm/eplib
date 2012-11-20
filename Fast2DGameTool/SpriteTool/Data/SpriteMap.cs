using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tool.TSystem.IO;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace SpriteTool.Data
{
    public  class SpriteMap
    {
        public enum E_Entity : int
        {
            Item = 0,
            Actor = 1,
            Effect = 2,
            BackGround = 3,
            UI = 4, 
            Max = 5,
        }

        private List<SpriteInfo>[] m_spriteCate;
        private int m_selectCate = -1;
        public int _version;

        public List<SpriteInfo>[] SpriteCate
        {
            get { return m_spriteCate; }
        }

        public int SelectCate
        {
            get { return m_selectCate; }
            set { m_selectCate = value; }
        }

        public List<SpriteInfo>[] SpriteUnits
        {
            get { return m_spriteCate; }
        }

        public bool FindSpriteUnit(string name,out SpriteInfo sprite ,int cate = -1 )
        {
            sprite = null;

            if (cate == -1)
                cate = m_selectCate;
            
            if (cate < 0)
                return false;

            foreach (SpriteInfo unit in m_spriteCate[cate])
            {
                if (unit.Name == name)
                {
                    sprite = unit;
                    return true;
                }
            }
            
            return false;
        }

        public bool IsExist(int cate, string name )
        {
            foreach (SpriteInfo unit in m_spriteCate[cate])
            {
                if (unit.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public SpriteMap()
        {
            m_spriteCate = new List<SpriteInfo>[(int)E_Entity.Max];

            for (int i = 0; i < (int)E_Entity.Max; ++i)
            {
                m_spriteCate[i] = new List<SpriteInfo>();
            }
        }

        public SpriteInfo Add( int cate, string name, string path)
        {
            if (cate < 0) return null;
            
            if ( IsExist( cate, name ) )
            {
                MessageBox.Show("동일한 이름이 이미 존재 합니다.");
                return null;
            }
            SpriteInfo newUnit = new SpriteInfo();

            newUnit.Name = name;
            newUnit.Path = ((E_Entity)cate).ToString() +"/"+ path;         

            m_spriteCate[cate].Add(newUnit);

            return newUnit;
        }
        
        public void Delete(SpriteInfo unit)
        {
            if (m_selectCate < 0) return;

            m_spriteCate[m_selectCate].Remove(unit);
        }

        public void Read(Stream stream)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            XmlNode rootNode = doc.SelectSingleNode("SpriteList");
            _version = GenericXmlReader.ReadIntAttribute(rootNode, "version");

            XmlNodeList cateNode = rootNode.SelectNodes("Cate");
            foreach (XmlNode node in cateNode)
            {
                string name = GenericXmlReader.ReadStringAttribute(node, "name");
                int cate = GenericXmlReader.ReadIntAttribute(node, "index");

                XmlNodeList spriteNode = node.SelectNodes("sprite");

                foreach (XmlNode snode in spriteNode)
                {
                    SpriteInfo sprite = new SpriteInfo();
                    sprite.Read(snode);
                    sprite.Cate = (E_Entity)cate;
                    //sprite.Path = ((E_Entity)cate).ToString() + "/" + sprite.Path;
                    m_spriteCate[cate].Add(sprite);
                }
            }
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartDocument();

            writer.WriteStartElement("SpriteList");
            GenericXmlWriter.WriteAttribute(writer, "version", _version);

            for(int i = 0; i < m_spriteCate.Length; ++i)
            {
                writer.WriteStartElement("Cate");
                GenericXmlWriter.WriteAttribute(writer, "index", i);
                GenericXmlWriter.WriteAttribute(writer, "name", ((E_Entity)i).ToString() );

                foreach (SpriteInfo sprite in m_spriteCate[i])
                {
                    sprite.Write(writer);
                }                

                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndDocument();
        }
    }

    
}
