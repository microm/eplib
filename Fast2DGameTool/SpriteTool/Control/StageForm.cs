using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TPoint = Tool.TSystem.Primitive.Point;

namespace SpriteTool.Control
{
    public partial class StageForm : Form
    {
        private Main m_main;
        public List<string> m_resNames;
        public List<TPoint> m_resolutions;
        private bool bModify = false;

        public StageForm()
        {
            InitializeComponent();

            m_resNames.Add("SVGA");         m_resolutions.Add(new TPoint(800, 600));
            m_resNames.Add("WVGA800");      m_resolutions.Add(new TPoint(800, 480));
            m_resNames.Add("WVGA854");      m_resolutions.Add(new TPoint(854, 480));
            m_resNames.Add("WXGA800");      m_resolutions.Add(new TPoint(1280, 800));
            m_resNames.Add("GalIII(?)");    m_resolutions.Add(new TPoint(1200, 720));
            m_resNames.Add("qHD");          m_resolutions.Add(new TPoint(960, 540));

            foreach( string res in m_resNames )
            {
                cmbResolution.Items.Add(res);
            }
        }

        internal void Init(Main main)
        {
            m_main = main;
            picDraw.Init( m_main);
        }

        public bool Modify
        {
            get { return bModify; }
            set { bModify = value; }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkGuide_CheckedChanged(object sender, EventArgs e)
        {
            picDraw.GuidLine = chkGuide.Checked;
            picDraw.Invalidate();
        }        
    }
}
