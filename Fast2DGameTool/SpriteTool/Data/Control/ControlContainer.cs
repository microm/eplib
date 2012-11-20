using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tool.TSystem.Primitive;
using System.Drawing;
using Tool.TSystem.IO;
using System.Xml;

namespace SpriteTool.Data.Control
{
    public abstract class ControlContainer : ControlBase
    {
        protected LayeredCollection m_layeredCollection = new LayeredCollection();

        public ControlContainer()
        {

        }

        public override StageLayer Root
        {
            get { return m_root; }
            set
            {
                m_root = value;
                foreach (ControlBase control in m_layeredCollection)
                {
                    control.Root = value;
                }
            }
        }
        
        public void Add(ControlBase control)
        {
            control.Parent = this;
            control.Root = Root;
            m_layeredCollection.Add(control);
        }

        public void Clear()
        {
            m_layeredCollection.Clear();
        }

        public bool Remove(ControlBase control)
        {
            if (m_layeredCollection.Remove(control)) return true;

            foreach (ControlBase child in m_layeredCollection)
            {
                if (child is ControlContainer)
                {
                    ControlContainer container = child as ControlContainer;
                    if (container.Remove(control)) return true;
                }
            }

            return false;
        }

        public List<ControlBase> ControlInRect(Rect rect)
        {
            List<ControlBase> controls = new List<ControlBase>();

            foreach (ControlBase control in m_layeredCollection)
            {
                if (control.Rect.Include(rect))
                {
                    controls.Add(control);
                }
            }
            return controls;
        }


        public int ChildControlCount
        {
            get { return m_layeredCollection.Count; }
        }

        public override ControlBase GetControlByPoint(TPoint position)
        {
            if (position.IsIn(Rect))
            {
                foreach (ControlBase child in m_layeredCollection)
                {
                    ControlBase controlUnderPos = child.GetControlByPoint(position);
                    if (controlUnderPos != null) return controlUnderPos;
                }
                return this;
            }
            return null;
        }

        public ControlBase FindControl(string name)
        {
            ControlBase control = m_layeredCollection.Find(name);
            if (control != null) return control;

            foreach (ControlBase child in m_layeredCollection)
            {
                if (child is ControlContainer)
                {
                    ControlContainer container = child as ControlContainer;
                    control = container.FindControl(name);
                    if (control != null) return control;
                }
            }
            return null;
        }

        public virtual void CollectControl(List<ControlBase> collects)
        {
            collects.Add(this);
            foreach (ControlBase child in m_layeredCollection)
            {
                child.CollectControl(collects);
            }
        }

        public virtual void DrawSelf(Graphics grfx)
        {
            grfx.DrawImage(Anchor.Bmp, DrawRect,
                0, 0, Anchor.Bmp.Width, Anchor.Bmp.Height, GraphicsUnit.Pixel);
        }

        public override void Draw(Graphics grfx)
        {
            DrawSelf(grfx);
            foreach (ControlBase child in m_layeredCollection)
            {
                child.Draw(grfx);
            }
        }

        public override void Write(XmlWriter writer)
        {
            foreach (ControlBase child in m_layeredCollection)
            {
                child.Write(writer);
            }
            //GenericXmlWriter.WriteAttribute(writer, "cate", m_sprite.Cate);
            
        }

        public override void ReadProperties(Main main, XmlNode node)
        {
            XmlNodeList controlNodes = node.SelectNodes("control");
            foreach (XmlNode child in controlNodes)
            {
                ControlBase.Read(main, child);
            }
        }

        public override void WriteProperties(XmlWriter writer)
        {
            foreach (ControlBase child in m_layeredCollection)
            {
                child.Write(writer);
            }
        }
    }
}
