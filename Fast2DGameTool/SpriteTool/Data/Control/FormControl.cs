using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Tool.TSystem.Primitive;
using Tool.TSystem.IO;

namespace SpriteTool.Data.Control
{
    public class FormControl : ControlContainer
    {
        public FormControl()            
        {
            m_name = string.Format("Form");
        }

        public override TPoint AbsolutePosition
        {
            get
            {
                return m_anchor.Position + m_root.StartPos;
            }
        }

        public override ControlType Type
        {
            get { return ControlType.Form; }
        }

    }
}
