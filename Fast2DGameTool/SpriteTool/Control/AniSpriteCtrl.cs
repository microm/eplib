using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpriteTool.Data;

namespace SpriteTool.Control
{
    public partial class AniSpriteCtrl : UserControl
    {
        public const int _controlID = 2;

        private Main m_main;
                
        private bool m_invert = false;

        public AniSpriteCtrl()
        {
            InitializeComponent();
        }


        public PivotPictureBox PivotPicture
        {
            get { return pivotPic; }
        }

        internal void Init(Main main)
        {
            m_main = main;

            pivotPic.Init(m_main);
            UpdateTreeView();
        }


        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.KeyCode == Keys.Delete)
            {
                if (m_main.SelectSprite == null)
                    return;

                if (m_main.SelectIndex > 0)
                {
                    m_main.SelectSprite.ImgList.RemoveAt(m_main.SelectIndex);
                    m_main.UpdateSprite();
                }
            }
        }
        

        public void UpdateTreeView()
        {
            spriteTreeView.Nodes.Clear();

            TreeNode rootNode = new TreeNode("root");

            for (int i = 0; i < (int)SpriteMap.E_Entity.Max; ++i)
            {
                TreeNode cateNode = new TreeNode( ((SpriteMap.E_Entity)i).ToString()  );
                cateNode.Name = string.Format("{0}", i);

                foreach (SpriteInfo spriteUnit in m_main.SpriteMap.SpriteCate[i])
                {
                    TreeNode spriteNode = cateNode.Nodes.Add(spriteUnit.Name);
                    spriteNode.Name = spriteUnit.Path;
                    spriteNode.Tag = spriteUnit;
                    spriteNode.ImageIndex = 3;
                    int index = 0;
                    foreach ( ImgData img in spriteUnit.ImgList )
                    {
                        TreeNode objNode = spriteNode.Nodes.Add( string.Format( "[{0}]({1})" ,index,img.Region.ToString() ) );
                        objNode.ImageIndex = 1;
                        objNode.Name = string.Format("{0}", index );
                        ++index;
                    }

                    if (spriteUnit == m_main.SelectSprite)
                    {
                        spriteNode.Expand();
                    }
                }

                if (m_main.SpritesMap.SelectCate == i)
                {
                    cateNode.Expand();
                }
                rootNode.Nodes.Add(cateNode);
            }
            rootNode.Expand();
            spriteTreeView.Sort();
            spriteTreeView.Nodes.Add(rootNode);
        }


        private void btnSpriteCreate_Click(object sender, EventArgs e)
        {
            if (m_main.SpritesMap.SelectCate == (int)SpriteMap.E_Entity.Max)
            {
                MessageBox.Show("분류가 선택되지 않았습니다. ! ");
                return;
            }
            if ( txtSprite.Text == "" ) 
            {
                MessageBox.Show("이름을 입력해주세요 ! ");
                return;
            }
            if ( m_main.Form.BasePicture.Text == "" ) 
            {
                MessageBox.Show("로드된 이미지가 없습니다. ! ");
                return;
            }
            m_main.SelectSprite = m_main.SpritesMap.Add(m_main.SpritesMap.SelectCate,txtSprite.Text, m_main.Form.BasePicture.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (m_main.SelectSprite == null)
            {
                MessageBox.Show("선택된 Sprite 가 없습니다. ! ");
                return;
            }
            if (m_main.SelectIndex > 0)
            {
                m_main.SelectSprite.RemoveRegion(m_main.SelectIndex);
                m_main.SelectIndex = -1;
            }
            else
            {
                m_main.SpritesMap.Delete(m_main.SelectSprite);
                m_main.SelectSprite = null;
            }

            UpdateData();
            m_main.UpdateSprite();
        }


        private void txtFrame_TextChanged(object sender, EventArgs e)
        {
            if (m_main.SelectSprite == null)
                return;

            float speed = 0;
            if (float.TryParse(txtFrame.Text,out speed))
            {
                if (speed <= 0) return;

                m_main.SelectSprite.Speed = speed;
                timer1.Interval = (int)( 1.0f / m_main.SelectSprite.Speed  * 1000);
            }
        }

        public void UpdateData()
        {
            UpdateTreeView();
        }

        private void spriteTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int selectIndex = 0;
            if (e.Node.Level == 1)
            {
                m_main.SpritesMap.SelectCate = int.Parse(e.Node.Name);
                return;
            }
            if (e.Node.Level == 2)
            {
                string path = e.Node.Text;

                m_main.SpritesMap.SelectCate = int.Parse(e.Node.Parent.Name);

                m_main.SelectSprite = (SpriteInfo)(e.Node.Tag);
                m_main.SelectIndex = -1;

                selectIndex = 0;                
            }
            else if (e.Node.Level == 3)
            {
                string path = e.Node.Parent.Text;
                selectIndex = int.Parse(e.Node.Name);

                m_main.SpritesMap.SelectCate = int.Parse(e.Node.Parent.Parent.Name);

                m_main.SelectSprite = (SpriteInfo)(e.Node.Parent.Tag);
                m_main.SelectIndex = selectIndex;

                           
            }
            m_main.Form.BasePicture.LoadSpriteUnit(m_main.SelectSprite);
            UpdateSprite();

            pivotPic.Index = selectIndex;     
        }
        

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (m_main.SelectSprite == null)
                return;

            timer1.Enabled = true;
            pivotPic.Play = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (m_main.SelectSprite == null)
                return;

            timer1.Enabled = false;
            pivotPic.Play = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_main.SelectSprite == null)
                return;

            int newIndex = pivotPic.Index;

            if (m_invert) newIndex--;
            else newIndex++;

            if (newIndex >= m_main.SelectSprite.ImgList.Count)
            {
                if (m_main.SelectSprite.IsCircle)
                {
                    newIndex = m_main.SelectSprite.ImgList.Count - 2;
                    m_invert = true;
                }
                else 
                    newIndex = 0;
            }
            else if ( newIndex < 0 )
            {
                m_invert = false;
                newIndex = 1;
            }

            pivotPic.Index = newIndex;
            pivotPic.Invalidate();
        }


        private void chkColorKey_CheckedChanged(object sender, EventArgs e)
        {
            if (m_main.SelectSprite == null)
                return;
            m_main.SelectSprite.HasColorKey = chkColorKey.Checked;

            m_main.UpdateSprite();
        }

        private void chkCircle_CheckedChanged(object sender, EventArgs e)
        {
            if (m_main.SelectSprite == null)
                return;

            m_main.SelectSprite.IsCircle = chkCircle.Checked;
        }

        internal void UpdateSprite()
        {
            if (m_main.SelectSprite == null)
                return;

            txtSprite.Text = m_main.SelectSprite.Name;
            chkCircle.Checked = m_main.SelectSprite.IsCircle;
            chkColorKey.Checked = m_main.SelectSprite.HasColorKey;
            chkParts.Checked = m_main.SelectSprite.IsParts;
            if ( m_main.SelectSprite.HasColorKey )
            {
                colorKeyPanel.BackColor = m_main.SelectSprite.ColorKey;
            }
            timer1.Enabled = false;

            if (m_main.SelectSprite.ImgList.Count > 0)
            {
                m_main.SelectSprite.Speed = Math.Max(m_main.SelectSprite.Speed, 0.5f);
                timer1.Interval = (int)(1.0f / m_main.SelectSprite.Speed * 1000);
                txtFrame.Text = string.Format("{0}", m_main.SelectSprite.Speed);
            }
            PivotPicture.Index = m_main.SelectIndex;
            PivotPicture.UpdateImages();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {

        }

        private void chkParts_CheckedChanged(object sender, EventArgs e)
        {
            if (m_main.SelectSprite == null)
                return;

            m_main.SelectSprite.IsParts = chkParts.Checked;
        }

        private void btnActorCreate_Click(object sender, EventArgs e)
        {
            if (m_main.SelectSprite == null)
                return;

            ActorForm actorEditor = new ActorForm();
            actorEditor.Init(m_main);
            actorEditor.CreateActor();

            actorEditor.ShowDialog();            
        }

    }
}
