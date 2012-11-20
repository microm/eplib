using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tool.TSystem.Primitive;
using Tool.TSystem.IO;
using System.Xml;
using SpriteTool.Control;
using System.IO;
using Tool.TSystem;
using System.Drawing;
using System.Drawing.Drawing2D;
using SpriteTool.Data.Control;

namespace SpriteTool.Data
{
    public enum ControlType : int
    {
        Form = 0,
        Panel,
        Label,
        Button,
        CheckBox,
        Max,
    }

    public abstract class ControlBase
    {
        protected StageLayer m_root;
        protected ControlContainer m_parent;

        protected AnchorInfo m_anchor;       
        protected string m_name = "noname";

        private SpriteInfo m_sprite;
        private TPoint m_size = new TPoint(50,50);
              
        public abstract ControlType Type { get; }

        public virtual StageLayer Root
        {
            get { return m_root; }
            set { m_root = value; }
        }

        public ControlContainer Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }

        public SpriteInfo Sprite
        {
            get { return m_sprite; }
        }

        public TPoint Size
        {
            get { return m_size; }
            set { m_size = value; }
        }
        
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        
        public AnchorInfo Anchor
        {
            get { return m_anchor; }
            set { m_anchor = value; }
        }

        public virtual TPoint AbsolutePosition
		{
			get
			{
                if (m_parent == null) return m_anchor.Position;
				
                return m_anchor.Position + m_parent.AbsolutePosition;				
			}
		}

        public Rect Rect
        {
            get
            {
                if (Sprite == null)
                {
                    return new Rect(AbsolutePosition, m_size.X, m_size.Y);
                }
                return new Rect(AbsolutePosition, m_anchor.Bmp.Width, m_anchor.Bmp.Height);
            }
            set
            {
                m_anchor.Position = value.Position;
                m_size = value.Size;
            }
        }

        public virtual Rectangle DrawRect
        {
            get
            {
                if (Sprite == null)
                {
                    return new Rectangle(AbsolutePosition.X, AbsolutePosition.Y, m_size.X, m_size.Y);
                }
                return new Rectangle(AbsolutePosition.X, AbsolutePosition.Y, m_anchor.Bmp.Width, m_anchor.Bmp.Height);
            }
        }

        public ControlBase()
        {
        }

        public void Init(SpriteInfo info, int _index)
        {
            m_sprite = info;          
            m_anchor = new AnchorInfo(_index);
        }

        private bool HasParent()
        {
            return Parent != null;
        }

        public bool IsIn(TPoint pos)
        {
            if ( pos.IsIn( new Rect(AbsolutePosition, m_anchor.Bmp.Width, m_anchor.Bmp.Height) ) )
            {
                return true;
            }
            return false;
        }

        public virtual ControlBase GetControlByPoint(TPoint position)
        {
            if (position.IsIn(Rect)) return this;
            return null;
        }

        public virtual void CollectControl( List<ControlBase> collects )
        {
            collects.Add(this);
        }

        public virtual ControlBase Clone()
        {
            return null;
        }

        public virtual void Draw(Graphics grfx)
        {
            //Matrix transform = new Matrix();
            //transform.RotateAt(control.Rotate, new PointF(m_center.X, m_center.Y));
            //grfx.Transform = transform;

            grfx.DrawImage(m_anchor.Bmp, DrawRect,
                0, 0, m_anchor.Bmp.Width, m_anchor.Bmp.Height, GraphicsUnit.Pixel);
        }

        public virtual void Write(XmlWriter writer)
        {
            writer.WriteStartElement("control");

            if (m_sprite != null)
            {
                GenericXmlWriter.WriteAttribute(writer, "cate", m_sprite.Cate);
                GenericXmlWriter.WriteAttribute(writer, "name", m_sprite.Name);
            }
            GenericXmlWriter.WriteAttribute(writer, "type", Type.ToString() );
            Anchor.Write(writer);

            WriteProperties(writer);
            
            writer.WriteEndElement();             
        }

        public abstract void WriteProperties(XmlWriter writer);
        public abstract void ReadProperties(Main main, XmlNode node);

        static public ControlBase Read(Main main, XmlNode node)
        {
            ControlType type = ConvertContorlType(GenericXmlReader.ReadStringAttribute(node, "type"));
            ControlBase newControl = CreateControl(type);

            XmlNode anchorNode = node.SelectSingleNode("anchor");
            AnchorInfo anchor = AnchorInfo.Read(anchorNode);

            SpriteInfo info = null;
            if (GenericXmlReader.IsExistAttribute(node, "cate"))
            {
                int cate = GenericXmlReader.ReadIntAttribute(node, "cate");
                string spriteName = GenericXmlReader.ReadStringAttribute(node, "name");

                if (main.SpritesMap.FindSpriteUnit(spriteName, out info, cate))
                {
                    return null;
                }
                newControl.Init(info, anchor.Index);
            }
            newControl.ReadProperties(main,node);

            return newControl;
        }

        static public ControlBase CreateControl(ControlType type)
        {
            ControlBase newControl = null;
            switch (type)
            {
                case ControlType.Form:
                    newControl = new FormControl();
                    break;
                case ControlType.Panel:
                    newControl = new PanelControl();
                    break;
                case ControlType.Label:
                    newControl = new LabelControl();
                    break;
                case ControlType.Button:
                    newControl = new ButtonControl();
                    break;
            }

            return newControl;
        }

        static public ControlType ConvertContorlType(string type)
        {
            for (int i = 0; i < (int)ControlType.Max; ++i)
            {
                if (((ControlType)i).ToString() == type)
                    return (ControlType)i;
            }
            return ControlType.Max;
        }
    }
}
