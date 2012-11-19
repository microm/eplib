using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Tool.TSystem.ImageMaker;
using System.Windows.Forms;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using Tool.TSystem;

namespace SpriteTool.Control
{
    public partial class StageForm : Form
    {
        private Main m_main;
        
        public List<string> m_resNames = new List<string>();
        public List<TPoint> m_resolutions = new List<TPoint>();
        private StageLayer m_layerInfo;       
        private TPoint m_prevPos;

        private bool m_bModify = false;
        private bool m_drag = false;

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

            picDraw.GuidTabSize = 50;
        }

        public StageLayer LayerInfo
        {
            get { return m_layerInfo; }
            set { m_layerInfo = value; }
        }

        internal void Init(Main main)
        {       

            m_main = main;
            picDraw.Init( m_main,this );

            InitStageList();
        }

        public bool Modify
        {
            get { return m_bModify; }
            set { m_bModify = value; }
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

        private void StageForm_Resize(object sender, EventArgs e)
        {
            picDraw.ResizePanel();
        }

        private void cmbResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbResolution.SelectedIndex < 0)
                return;

            this.Size = new Size(m_resolutions[cmbResolution.SelectedIndex].X + 200 , m_resolutions[cmbResolution.SelectedIndex].Y + 45 );
        }
      
        private void picDraw_MouseDown(object sender, MouseEventArgs e)
        {
            if (LayerInfo == null)
                return;

            TPoint mousePos = new TPoint(e.X, e.Y);

            ControlBase control = LayerInfo.Form.GetControlByPoint(mousePos);
            if (control != null)
            {
                picDraw.SelectControl = control;
                objPropertyGrid.SelectedObject = control;
                m_drag = true;
            }           

            m_prevPos = mousePos;
            picDraw.Focus();
        }

        private void picDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (LayerInfo == null)
                return;

            TPoint mousePos = new TPoint(e.X, e.Y);

            if (m_prevPos == mousePos)
                return;

            TPoint pos = new TPoint(e.X - picDraw.Center.X, e.Y - picDraw.Center.Y);

            if (picDraw.SelectControl != null && m_drag)
            {
                //TPoint startPos = picDraw.SelectControl.GetStartPos(picDraw);         
                picDraw.SelectControl.Anchor.Position = pos;
                m_bModify = true;
            }
            picDraw.Invalidate();
            m_prevPos = mousePos;
        }

        private void picDraw_MouseUp(object sender, MouseEventArgs e)
        {
            m_drag = false;
        }

        private void picDraw_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (LayerInfo == null)
                return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (LayerInfo == null)
                return;

            if (m_bModify)
            {
                LayerInfo.Save(m_main);

                InitStageList();
                m_bModify = false;
            }
        }

        private void btnStageCreate_Click(object sender, EventArgs e)
        {
            LayerInfo = new StageLayer( txtStage.Text );
        }

        private void cmbStageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStageList.SelectedIndex < 0)
                return;

            // Load Stage
            LayerInfo = new StageLayer("");
            if (LayerInfo.Load(m_main, cmbStageList.Items[cmbStageList.SelectedIndex].ToString()))
            {
                List<ControlBase> allControl = new List<ControlBase>();
                LayerInfo.Form.CollectControl(allControl);
                foreach (ControlBase control in allControl)
                {
                    control.Anchor.LoadBmp(m_main, control.Sprite);                       
                }
                picDraw.Invalidate();
            }
        }

        private void InitStageList()
        {
            cmbStageList.Items.Clear();

            List<string> files = m_main.Browser.GetFileListContainName("script/stage/", "stg");
            foreach (string file in files)
            {
                cmbStageList.Items.Add(file);
            }
        }

        private bool CanControlAdd( bool isLabel = false )
        {
            if (LayerInfo == null)
            {
                MessageBox.Show("선택된 StageLayer 가 없습니다.");
                return false;
            }
            if ( picDraw.SelectControl == null)
            {
                MessageBox.Show("선택된 Control 이 존재하지 않습니다.");
                return false;
            }
            if (isLabel != false)
            {
                if (m_main.SelectSprite == null || m_main.SelectIndex < 0)
                {
                    MessageBox.Show("선택된 이미지가 존재하지 않습니다.");
                    return false;
                }
            }
            return true;
        }

        private void panelCreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanControlAdd() == false)
                return;
            ControlBase control = ControlBase.CreateControl(ControlType.Panel);

            control.Anchor.LoadBmp(m_main, control.Sprite);

            m_bModify = true;
            picDraw.Invalidate();
        }

        private void labelCreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanControlAdd( true ) == false)
                return;

            ControlBase control = ControlBase.CreateControl(ControlType.Label);

            control.Anchor.LoadBmp(m_main,control.Sprite);

            m_bModify = true;
            picDraw.Invalidate();
        }

        private void buttonCreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanControlAdd() == false)
                return;
            ControlBase control = ControlBase.CreateControl(ControlType.Button);

            control.Anchor.LoadBmp(m_main, control.Sprite);

            m_bModify = true;
            picDraw.Invalidate();
        }


        private void btnCreateForm_Click(object sender, EventArgs e)
        {
            if (LayerInfo == null)
            {
                MessageBox.Show("선택된 StageLayer 가 없습니다.");
                return;
            }
            if (m_main.SelectSprite == null || m_main.SelectIndex < 0)
            {
                MessageBox.Show("선택된 이미지가 존재하지 않습니다.");
                return;
            }

            ControlBase control = ControlBase.CreateControl(ControlType.Form);

            control.Anchor.LoadBmp(m_main, control.Sprite);

            m_bModify = true;
            picDraw.Invalidate();
        }

    }
}
