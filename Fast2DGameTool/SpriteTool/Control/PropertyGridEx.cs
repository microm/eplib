using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpriteTool.Control
{
    public partial class PropertyGridEx : PropertyGrid
    {
        private Main m_main;        

        public PropertyGridEx()
        {
            InitializeComponent();
        }

        public void Init(Main main)
        {
            m_main = main;
        }

        public PropertyGridEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        
    }
}
