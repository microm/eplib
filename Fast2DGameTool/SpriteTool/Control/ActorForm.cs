using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpriteTool.Data;
using System.IO;
using System.Xml;
using Tool.TSystem;
using Tool.TSystem.IO;

namespace SpriteTool.Control
{
    public partial class ActorForm : Form
    {
        private Main m_main;
        private ActorInfo m_selectActor;
        private bool bModify = false;
        
        public ActorForm()
        {
            InitializeComponent();
            
        }

        internal void Init(Main main)
        {
            m_main = main;
            prevPictrue.Init(this,m_main);
        }

        public bool Modify
        {
            get { return bModify; }
            set { bModify = value; }
        }

        public string SelectName
        {
            get
            {
                if (m_selectActor == null)
                    return "";
                return m_selectActor.Name;
            }
        }

        private void UpdateList()
        {
            listActor.Items.Clear();

            int selectIndex = 0;
            int index = 0;
            foreach ( ActorInfo info in m_main.ActorList.Actors )
            {
                if (SelectName == info.Name)
                {
                    selectIndex = index;
                }
                listActor.Items.Add( info.Name );
                ++index;
            }
            listActor.SelectedIndex = selectIndex;
        }

        private void UpdateRegion()
        {
            if ( m_selectActor == null )
                return;

            listRegion.Items.Clear();
            int index = 0;
            foreach (ImgData img in m_selectActor.SpriteInfo.ImgList)
            {
                listRegion.Items.Add( string.Format( "{0}.[{1}]",index,  img.Region.ToString()) );
                ++index;
            }

            prevPictrue.Invalidate();
        }

        public void CreateActor()
        {
            if (m_main.SpritesMap.SelectCate != (int)SpriteMap.E_Entity.Actor)
            {
                MessageBox.Show("Actor 카테고리에서 만 생성할수 있습니다.");
                return;
            }

            if (m_main.SelectSprite == null)
                return;

           
            if (m_main.ActorList.IsExist(m_main.SelectSprite.Name))
            {
                SelectActor(m_main.SelectSprite.Name);               
                return;
            }
            m_main.SelectSprite.IsParts = true;
            m_main.ActorList.Add(m_main.SelectSprite);

            SelectActor(m_main.SelectSprite.Name);

            bModify = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CreateActor(); 
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listActor.SelectedIndex < 0)
                return;

            if (m_selectActor == null)
                return;

            m_main.ActorList.Delete(m_selectActor);
            m_selectActor = null;

            UpdateList();
            UpdateRegion();
            bModify = true;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void listActor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( listActor.SelectedIndex < 0 )
            {
                m_selectActor = null;
                return;
            }
            string selectName = listActor.Items[listActor.SelectedIndex].ToString();
            SelectActor(selectName);
        }

        public void SelectActor( string name )
        {
            if (m_selectActor != null)
            {
                if (m_selectActor.Name == name)
                    return;
            }
            ActorInfo selectActor;
            m_main.ActorList.FindActor(name, out selectActor);

            if (selectActor != null)
            {
                prevPictrue.SetActor(selectActor);
                m_selectActor = selectActor;
            }

            UpdateList();
            UpdateRegion();
        }

        private void prevPictrue_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = new Point(e.X - prevPictrue.Center.X, e.Y - prevPictrue.Center.Y);

            string msg = string.Format("xpos = {0}, ypos = {1}", pos.X, pos.Y);
            toolStripStatusLabel1.Text = msg;

            
        }

        private void listRegion_SelectedIndexChanged(object sender, EventArgs e)
        {            
        }

        private void btnAddImg_Click(object sender, EventArgs e)
        {
            if (listRegion.SelectedIndex < 0)
                return;

            ActorInfo.Anchor newAnchor = new ActorInfo.Anchor( listRegion.SelectedIndex );
            m_selectActor.Anchors.Add(newAnchor);

            prevPictrue.UpdateAnchor();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            m_main.SaveActor();
            bModify = false;
        }        

        private void ActorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bModify)
            {
                if (DialogResult.Yes == MessageBox.Show("저장하시겠습니까?", "액터정보", MessageBoxButtons.YesNo))
                {
                    m_main.SaveActor();            
                }
            }
        }

        private void chkGuide_CheckedChanged(object sender, EventArgs e)
        {
            prevPictrue.GuidLine = chkGuide.Checked;
            prevPictrue.Invalidate();
        }

        private void anchorPropertyGrid_Click(object sender, EventArgs e)
        {

        }
    }
}
