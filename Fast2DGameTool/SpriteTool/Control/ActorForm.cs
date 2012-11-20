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
using Point = System.Drawing.Point;
using Tool.TSystem.Primitive;

namespace SpriteTool.Control
{
    public partial class ActorForm : Form
    {
        private Main m_main;
        private ActorInfo m_selectActor;
       
        private bool m_drag = false;
        private TPoint m_prevPos;

        public ActorForm()
        {
            InitializeComponent();            
        }

        internal void Init(Main main)
        {
            m_main = main;
            prevPictrue.Init(m_main);
            anchorPropertyGrid.Init(m_main);

            anchorPropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(anchorPropertyGrid_PropertyValueChanged);
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
            foreach ( ActorInfo info in m_main.Actors.Actors )
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

           
            if (m_main.Actors.IsExist(m_main.SelectSprite.Name))
            {
                SelectActor(m_main.SelectSprite.Name);               
                return;
            }
            m_main.SelectSprite.IsParts = true;
            m_main.Actors.Add(m_main.SelectSprite);

            SelectActor(m_main.SelectSprite.Name);
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

            m_main.Actors.Delete(m_selectActor);
            m_selectActor = null;

            UpdateList();
            UpdateRegion();
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
            m_main.Actors.FindActor(name, out selectActor);

            if (selectActor != null)
            {
                prevPictrue.SetActor(selectActor);
                m_selectActor = selectActor;
            }

            UpdateList();
            UpdateRegion();
        }

      

        private void listRegion_SelectedIndexChanged(object sender, EventArgs e)
        {            
        }

        private void btnAddImg_Click(object sender, EventArgs e)
        {
            if (listRegion.SelectedIndex < 0)
                return;

            AnchorInfo newAnchor = new AnchorInfo( listRegion.SelectedIndex );
            m_selectActor.Anchors.Add(newAnchor);
            m_main.Actors.Modify = true;

            prevPictrue.UpdateAnchor();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            m_main.SaveActor();
        }        

        private void ActorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_main.Actors.Modify)
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

        private void anchorPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            prevPictrue.Invalidate();
        }

        private void prevPictrue_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (prevPictrue.SelectAnchor != null)
            {
                if (e.Control)
                {
                    prevPictrue.SelectAnchor.Bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    prevPictrue.SelectAnchor.XFlip = !prevPictrue.SelectAnchor.XFlip;
                }
                else if (e.Alt)
                {
                    prevPictrue.SelectAnchor.YFlip = !prevPictrue.SelectAnchor.YFlip;
                    prevPictrue.SelectAnchor.Bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }
                else if (e.KeyCode == Keys.OemMinus)
                {
                    prevPictrue.SelectAnchor.ZOrder -= 1;
                }
                else if (e.KeyCode == Keys.Oemplus)
                {
                    prevPictrue.SelectAnchor.ZOrder += 1;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    m_selectActor.Anchors.Remove(prevPictrue.SelectAnchor);
                    prevPictrue.SelectAnchor = null;
                    //anchorPropertyGrid.SelectedObject = null;

                    m_main.Actors.Modify = true;
                    prevPictrue.UpdateAnchor();
                }
            }
            anchorPropertyGrid.SelectedObject = prevPictrue.SelectAnchor;
            prevPictrue.Invalidate();
        }        

        private void prevPictrue_MouseDown(object sender, MouseEventArgs e)
        {
            TPoint mousePos = new TPoint(e.X, e.Y);
            foreach (AnchorInfo anchor in m_selectActor.Anchors)
            {
                Bitmap curImage = anchor.Bmp;

                TPoint startPos = prevPictrue.GetSpritePos(m_selectActor.SpriteInfo, anchor) + anchor.Position;
                Rect selectRect = new Rect(startPos.X, startPos.Y, startPos.X + curImage.Width, startPos.Y + curImage.Height);

                if (selectRect.Has(mousePos))
                {
                    prevPictrue.SelectAnchor = anchor;
                    anchorPropertyGrid.SelectedObject = anchor;
                    m_drag = true;
                }
            }
            m_prevPos = mousePos;
            prevPictrue.Focus();
        }

        private void prevPictrue_MouseMove(object sender, MouseEventArgs e)
        {
            TPoint mousePos = new TPoint(e.X, e.Y);

            if (m_prevPos == mousePos)
                return;

            TPoint pos = new TPoint(e.X - prevPictrue.Center.X, e.Y - prevPictrue.Center.Y);

            string msg = string.Format("xpos = {0}, ypos = {1}", pos.X, pos.Y);
            toolStripStatusLabel1.Text = msg;           

            if (prevPictrue.SelectAnchor != null && m_drag)
            {
                TPoint startPos = prevPictrue.GetSpritePos(m_selectActor.SpriteInfo, prevPictrue.SelectAnchor);
                TPoint newOffset = pos;

                prevPictrue.SelectAnchor.Position = pos;
                m_main.Actors.Modify = true;
            }
            anchorPropertyGrid.SelectedObject = prevPictrue.SelectAnchor;
            prevPictrue.Invalidate();
            m_prevPos = mousePos;
        }

        private void prevPictrue_MouseUp(object sender, MouseEventArgs e)
        {
            m_drag = false;
        }

    }
}
