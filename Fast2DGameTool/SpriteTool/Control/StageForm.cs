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
using SpriteTool.Data.Control;
using Tool.TSystem.Pattern;
using SpriteTool.State;
using Tool.TSystem.Basis;

namespace SpriteTool.Control
{
    public partial class StageForm : Form
    {
        private Main m_main;
        
        public List<string> m_resNames = new List<string>();
        public List<TPoint> m_resolutions = new List<TPoint>();
           
        private bool m_bModify = false;

        private readonly CommandManager m_commandManager = new CommandManager();
        private readonly StateManager m_stateManager;

        private readonly MouseEventTranslator m_mouseTranslator = new MouseEventTranslator();
        private readonly KeyboardEvent m_keyEvent = new KeyboardEvent();

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

            stagePanel.GuidTabSize = 50;

            stagePanel.SelectControlEventHandler += OnSelectControl;
            m_stateManager = new StateManager(m_commandManager, stagePanel);
        }

        internal void Init(Main main)
        {       
            m_main = main;
            stagePanel.Init( m_main,this );

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

        private void OnSelectControl()
        {
            if ( stagePanel.SelectedControls.Count == 1 )
            {
                objPropertyGrid.SelectedObject = stagePanel.SelectedControls[0];
            }
            else
            {
                objPropertyGrid.SelectedObject = null;
            }            
        }

        private void chkGuide_CheckedChanged(object sender, EventArgs e)
        {
            stagePanel.GuidLine = chkGuide.Checked;
            stagePanel.Invalidate();
        }

        private void StageForm_Resize(object sender, EventArgs e)
        {
            stagePanel.ResizePanel();
        }

        private void cmbResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbResolution.SelectedIndex < 0)
                return;

            this.Size = new Size(m_resolutions[cmbResolution.SelectedIndex].X + 200 , m_resolutions[cmbResolution.SelectedIndex].Y + 45 );
        }
      
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (stagePanel.LayerInfo == null)
                return;

            if (m_bModify)
            {
                stagePanel.LayerInfo.Save(m_main);

                InitStageList();
                m_bModify = false;
            }
        }

        private void btnStageCreate_Click(object sender, EventArgs e)
        {
            stagePanel.LayerInfo = new StageLayer(txtStage.Text , m_main );
        }

        private void cmbStageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStageList.SelectedIndex < 0)
                return;

            // Load Stage
            stagePanel.LayerInfo = new StageLayer("",m_main);
            if (stagePanel.LayerInfo.Load(m_main, cmbStageList.Items[cmbStageList.SelectedIndex].ToString()))
            {
                List<ControlBase> allControl = new List<ControlBase>();
                stagePanel.LayerInfo.Form.CollectControl(allControl);
                foreach (ControlBase control in allControl)
                {
                    control.Anchor.LoadBmp(m_main, control.Sprite);                       
                }
                stagePanel.Invalidate();
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
            if (stagePanel.LayerInfo == null)
            {
                MessageBox.Show("선택된 StageLayer 가 없습니다.");
                return false;
            }
            if ( stagePanel.SelectedControls == null)
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


        // Create Control
        private void panelCreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanControlAdd() == false)
                return;

            TPoint createPos = new TPoint(createMenuStrip.Left, createMenuStrip.Top);
            
            ControlBase control = ControlBase.CreateControl(ControlType.Panel);
            control.Init(m_main.SelectSprite, m_main.SelectIndex);
            control.Anchor.LoadBmp(m_main, control.Sprite);

            ControlContainer container = stagePanel.LayerInfo.FindContainer(createPos);
            
            control.Anchor.Position = new TPoint(createMenuStrip.Left, createMenuStrip.Top) - container.AbsolutePosition;
            container.Add(control);

            m_bModify = true;
            stagePanel.Invalidate();
        }

        private void labelCreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanControlAdd( true ) == false)
                return;

            ControlBase control = ControlBase.CreateControl(ControlType.Label);

            control.Anchor.LoadBmp(m_main,control.Sprite);

            m_bModify = true;
            stagePanel.Invalidate();
        }

        private void buttonCreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanControlAdd() == false)
                return;
            ControlBase control = ControlBase.CreateControl(ControlType.Button);
            control.Init(m_main.SelectSprite, m_main.SelectIndex);
            control.Anchor.LoadBmp(m_main, control.Sprite);

            m_bModify = true;
            stagePanel.Invalidate();
        }


        private void btnCreateForm_Click(object sender, EventArgs e)
        {
            if (stagePanel.LayerInfo == null)
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
            control.Init(m_main.SelectSprite, m_main.SelectIndex);
            control.Anchor.LoadBmp(m_main, control.Sprite);

            stagePanel.LayerInfo.AddForm(control);

            m_bModify = true;
            stagePanel.Invalidate();
        }

        private void stagePanel_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEvent mouseEvent = m_mouseTranslator.MouseDown(e);
            OnMouseEvent(mouseEvent);
        }

        private void stagePanel_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEvent mouseEvent = m_mouseTranslator.MouseMove(e);
            OnMouseEvent(mouseEvent);
        }

        private void stagePanel_MouseUp(object sender, MouseEventArgs e)
        {
            MouseEvent mouseEvent = m_mouseTranslator.MouseUp(e);
            OnMouseEvent(mouseEvent);
        }

        public void OnMouseEvent(MouseEvent mouseEvent)
        {
            if (mouseEvent == null) return;
            m_stateManager.CurrentState.OnMouseEvent(mouseEvent);
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (KeyEventTranslator.MessageProc(m.Msg, m.WParam.ToInt32(), m.LParam.ToInt32(), m_keyEvent))
            {
                m_stateManager.CurrentState.OnKeyboardEvent(m_keyEvent);
            }
            return base.ProcessKeyPreview(ref m);
        }

    }
}
