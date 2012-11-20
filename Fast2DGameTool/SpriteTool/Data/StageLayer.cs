using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tool.TSystem.Primitive;
using Tool.TSystem.IO;
using System.Xml;
using SpriteTool.Control;
using System.IO;
using Tool.TSystem;
using System.Windows.Forms;
using SpriteTool.Data.Control;

namespace SpriteTool.Data
{  
    public enum ScaleType
    {
        None,
        WidthRatio,
        HeightRatio,
        FullScale,
    }


    public class StageLayer
    {
        private string m_name;
        private FormControl m_form;
        private TPoint m_startPos = new TPoint(0, 0);        
        private TPoint m_size = new TPoint(300,300);
        private ScaleType m_scaleType = ScaleType.None;
        private float m_scale = 1.0f;
        private Main m_main;

        public FormControl Form
        {
            get { return m_form; }
            set { m_form = value; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public TPoint StartPos
        {
            get { return m_startPos; }
            set { m_startPos = value; }
        }

        public StageLayer(string name,Main main)
        {
            m_name = name;
            m_main = main;
        }

        public bool Load(Main main, string fileName)
        {
            string path = "stage/" + fileName;
            Stream stream = main.Browser.Read(IODataType.Script, path);

            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            XmlNode rootNode = doc.SelectSingleNode("Stage");
            string name = GenericXmlReader.ReadStringAttribute(rootNode, "name");

            XmlNode formNode = rootNode.SelectSingleNode("Form");
            ControlBase form = ControlBase.Read(main, formNode);

            if (form.Type == ControlType.Form)
            {
                m_form = (FormControl)form;
                return true;
            }            
            return false;
        }

        public void Save(Main main)
        {
            string path = "stage/" + m_name + ".stg";
            Stream stream = main.Browser.Write(IODataType.Script, path);
            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Stage");
            GenericXmlWriter.WriteAttribute(writer, "name", m_name);

            m_form.Write(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            stream.Close();
        }

        public void Draw(Graphics grfx)
        {
            Color customColor = Color.FromArgb(50, Color.Gray);
            SolidBrush shadowBrush = new SolidBrush(customColor);

            grfx.FillRectangle(shadowBrush, new Rectangle( m_startPos.X, m_startPos.Y, m_size.X, m_size.Y ) );

            if (m_form != null)
            {
                m_form.Draw(grfx);
            }    
        }

        public ControlBase FindControl(TPoint pos)
        {
            if (m_form == null)
                return null;

            return m_form.GetControlByPoint(pos);
        }

        public ControlContainer FindContainer(TPoint pos)
        {          
            ControlBase control = FindControl(pos);
            if (control == null)
                return null;

            if ( control is ControlContainer )
            {
                return (ControlContainer)control;
            }
            return control.Parent;
        }

        internal void AddForm(ControlBase control)
        {
            m_form = (FormControl)control;
            m_form.Root = this;
        }

        internal void RemoveControl(ControlBase control)
        {
            if (m_form == null)
                return;

            m_form.Remove(control);
        }

        internal ControlBase CreateControl(ControlType controlType, TPoint startPosition, TPoint endPosition)
        {
            ControlBase control = ControlBase.CreateControl(controlType);
                        
            control.Init(m_main.SelectSprite, m_main.SelectIndex);
            control.Anchor.Position = startPosition;
            if (m_main.SelectSprite != null)
            {
                control.Anchor.LoadBmp(m_main, control.Sprite);                
            }
            control.Size = endPosition - startPosition;
            control.Root = this;            
            return control;
        }

    }
}

