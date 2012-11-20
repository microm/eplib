using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace SpriteTool.Data.Control
{
    public class PanelControl : ControlContainer
    {
        public static int CountId = 1;

        public PanelControl()
        {
            m_name = string.Format("Panel_{0}", CountId++);
        }

        public override ControlType Type
        {
            get { return ControlType.Panel; }
        }

       
    }
}
