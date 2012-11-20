using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tool.TSystem.IO;
using System.Xml;
using System.IO;

namespace SpriteTool.Data
{
    public class ActorList
    {
        private List<ActorInfo> m_actorlist;
        public int _version;
        private bool m_bModify = false;
        
        public bool Modify
        {
            get { return m_bModify; }
            set { m_bModify = value; }
        }
        
        public List<ActorInfo> Actors
        {
            get { return m_actorlist; }
        }

        public ActorList()
        {
            m_actorlist = new List<ActorInfo>();
        }

        public ActorInfo Add(SpriteInfo info)
        {
            if (info == null)
                return null;          

            ActorInfo newActor = new ActorInfo();
            newActor.Name = info.Name;
            newActor.SpriteInfo = info;

            newActor.Anchors.Add(new AnchorInfo(0));

            m_actorlist.Add(newActor);

            m_bModify = true;

            return newActor;
        }

        public void Delete(ActorInfo unit)
        {
            m_actorlist.Remove(unit);

            m_bModify = true;
        }

        public bool IsExist(string name)
        {
            foreach (ActorInfo info in m_actorlist)
            {
                if (info.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool FindActor(string name, out ActorInfo actor)
        {
            foreach (ActorInfo info in m_actorlist)
            {
                if (info.Name == name)
                {
                    actor = info;
                    return true;
                }
            }
            actor = null;
            return false;
        }

        public void Read(Stream stream)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);

            XmlNode rootNode = doc.SelectSingleNode("ActorList");
            _version = GenericXmlReader.ReadIntAttribute(rootNode, "version");

            XmlNodeList actorNode = rootNode.SelectNodes("actor");
            foreach (XmlNode snode in actorNode)
            {
                ActorInfo actor = new ActorInfo();
                actor.Read(snode);

                m_actorlist.Add(actor);
            }
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartDocument();

            //Write the ProcessingInstruction node.
            //String PItext = "type='text/xsl' href='book.xsl'";
            //writer.WriteProcessingInstruction("xml-stylesheet", PItext);

            writer.WriteStartElement("ActorList");
            GenericXmlWriter.WriteAttribute(writer, "version", _version);
            foreach (ActorInfo actor in m_actorlist)
            {
                actor.Write(writer);
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();

            m_bModify = false;
        }
    }
}
