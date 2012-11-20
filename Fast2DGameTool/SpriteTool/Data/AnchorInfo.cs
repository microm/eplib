using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tool.TSystem.IO;
using System.Xml;
using System.Drawing;
using Tool.TSystem.Primitive;
using Tool.TSystem;


namespace SpriteTool.Data
{
    public class AnchorInfo
    {
        private int m_index;
        private TPoint m_position;
        private bool m_XFlip = false;
        private bool m_YFlip = false;
        private int m_rotate = 0;
        private float m_scale = 1;                

        private int m_ZOrder = 3;
        private Bitmap m_bmp;

        public int Index
        {
            get { return m_index; }
        }

        public TPoint Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public bool XFlip
        {
            get { return m_XFlip; }
            set { m_XFlip = value; }
        }

        public bool YFlip
        {
            get { return m_YFlip; }
            set { m_YFlip = value; }
        }

        public int ZOrder
        {
            get { return m_ZOrder; }
            set { m_ZOrder = value; }
        }

        public int Rotate
        {
            get { return m_rotate; }
            set { m_rotate = value; }
        }

        public float Scale
        {
            get { return m_scale; }
            set { m_scale = value; }
        }

        public System.Drawing.Bitmap Bmp
        {
            get { return m_bmp; }
            set { m_bmp = value; }
        }

        public AnchorInfo(int _index)
        {
            m_index = _index;
            m_position = new TPoint(0, 0);
        }

        public void Write(XmlWriter writer)
        {
            writer.WriteStartElement("anchor");
            GenericXmlWriter.WriteAttribute(writer, "index", Index);
            GenericXmlWriter.WriteAttribute(writer, "offset", Position.ToString());
            GenericXmlWriter.WriteAttribute(writer, "xflip", XFlip);
            GenericXmlWriter.WriteAttribute(writer, "yflip", YFlip);
            GenericXmlWriter.WriteAttribute(writer, "zorder", ZOrder);
            writer.WriteEndElement();
        }

        static public AnchorInfo Read(XmlNode node)
        {
            int index = GenericXmlReader.ReadIntAttribute(node, "index");
            AnchorInfo anchor = new AnchorInfo(index);
            anchor.Position = GenericXmlReader.ReadPointAttribute(node, "offset");
            anchor.XFlip = GenericXmlReader.ReadBoolAttribute(node, "xflip");
            anchor.YFlip = GenericXmlReader.ReadBoolAttribute(node, "yflip");
            anchor.ZOrder = GenericXmlReader.ReadIntAttribute(node, "zorder");
            return anchor;
        }

        public void LoadBmp( Main main , SpriteInfo sprite )
        {
            string path = main.Browser.GetFileFullPath(IODataType.Image, sprite.Path);
            
            if ( main.DevImage.Load(path))
            {
                ImgData img = sprite.ImgList[m_index];
                Bmp = main.DevImage.Crop(img.Region.Left, img.Region.Top, img.Region.Width, img.Region.Height);
                
                if (m_XFlip)
                {
                    Bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
                if (m_YFlip)
                {
                    Bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }
            }
        }
    }
}
