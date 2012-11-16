using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpriteTool.Control;
using Tool.TSystem.Assist;
using TPoint = Tool.TSystem.Primitive.Point;
using Tool.TSystem;
using System.IO;
using SpriteTool.Data;

namespace SpriteTool
{
    public partial class MainForm : Form
    {
        private readonly Main m_main;
        private Point prevPos;

        private Color m_backColor = Color.White;
        private Color m_lineColor = Color.Black;
        
        public Color BackgroundColor
        {
            get { return m_backColor; }
            set { 
                m_backColor = value;
                splitContainer2.Panel1.BackColor = m_backColor;
                ListPanel.BackColor = m_backColor;
                RightCtrl.PivotPicture.BackColor = m_backColor;
            }
        }

        public Color LineColor
        {
            get { return m_lineColor; }
            set
            {
                m_lineColor = value;
                m_main.LinePen.Color = m_lineColor;

                SplitPic.SetGuideColor(m_lineColor);
                RightCtrl.PivotPicture.Invalidate();
            }
        }

        public MainForm()
        {
            InitializeComponent();

            ControlStyles style = ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint |
                                  ControlStyles.AllPaintingInWmPaint;
            SetStyle(style, true);

            m_main = new Main(this);

            RightCtrl.Init(m_main);
            SplitPic.Init(m_main);
            ListPanel.Init(m_main);
        }

        public AniSpriteCtrl SpriteCtrl
        {
            get { return RightCtrl; }
        }

        public BasePictureBox BasePicture            
        {
            get { return SplitPic; }
        }

        public ListPicPanel ListPicturePanel
        {
            get { return ListPanel; }
        }

        public ToolStripStatusLabel StripLabel
        {
            get { return toolStripImageInfo; }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Register reg = new Register("SpriteTool");
            TPoint winsize = reg.GetPoint("WindowSize", "1200, 800");
            reg.Close();   
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Register reg = new Register("SpriteTool");
            reg.SetPoint("WindowSize", new TPoint(Width, Height));

            reg.Close();
        }

        private void imageOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.InitialDirectory = m_main.Browser.GetPath(IODataType.Image);
            fileDlg.Filter = "All Formats|*.JPG;*.BMP;*.PNG;JPEG (*.JPG)|*.JPG|BMP |*.BMP|PNG |*.PNG|TGA |*.TGA|";
            if (fileDlg.ShowDialog() == DialogResult.OK)
            {               
                m_main.ImageLoad(fileDlg.FileName);
            }
        }
        
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDlg = new SaveFileDialog();
            fileDlg.InitialDirectory = m_main.Browser.GetPath(IODataType.Script);
            fileDlg.Filter = "Sprite File (*.xml)|*.xml";
            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                m_main.SaveFile(fileDlg.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_main.Save();
        }

        private void guidLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            menuItem.Checked = !menuItem.Checked;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutDlg = new AboutForm();
            aboutDlg.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void SplitPic_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Middle)
                return;
            if (splitContainer2.Panel1.VerticalScroll.Visible || splitContainer2.Panel1.HorizontalScroll.Visible)
            {
                splitContainer2.Panel1.Cursor = new Cursor(new MemoryStream(Resource3.Grabbing));

                prevPos.X = ((System.Windows.Forms.Control)sender).PointToScreen(e.Location).X;
                prevPos.Y = ((System.Windows.Forms.Control)sender).PointToScreen(e.Location).Y;
            }
        }

        private void SplitPic_MouseMove(object sender, MouseEventArgs e)
        {
            string msg = string.Format( "xpos = {0}, ypos = {1}", e.X, e.Y );
            toolStripImageInfo.Text = msg;

            Point curPos = new Point(0, 0);

            if (splitContainer2.Panel1.VerticalScroll.Visible == false && splitContainer2.Panel1.HorizontalScroll.Visible == false) return;
            if (e.Button == MouseButtons.Middle)
            {
                curPos.X = ((System.Windows.Forms.Control)sender).PointToScreen(e.Location).X;
                curPos.Y = ((System.Windows.Forms.Control)sender).PointToScreen(e.Location).Y;
                if (curPos == prevPos) return;

                int newScrollX = splitContainer2.Panel1.HorizontalScroll.Value + (prevPos.X - curPos.X);
                int newScrollY = splitContainer2.Panel1.VerticalScroll.Value + (prevPos.Y - curPos.Y);

                splitContainer2.Panel1.AutoScrollPosition = new Point(newScrollX, newScrollY);

                prevPos = curPos;
            }
            else
            {
                //splitContainer2.Panel1.Cursor = new Cursor(new MemoryStream(Resource3.Grab));
                this.Cursor = Cursors.Cross;
            }
        }

        private void toolStripBtnLineColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                SplitPic.SetGuideColor( colorDialog1.Color );
            }
        }

        private void SplitPic_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                BackgroundColor = colorDialog1.Color;
            }
        }

        private void lineColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                LineColor = colorDialog1.Color;
            }
        }

        private void AutoImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;

            for (int cate = 0; cate < (int)SpriteMap.E_Entity.Max; ++cate)
            {
                string path = ((SpriteMap.E_Entity)cate).ToString() + "/";
                path = m_main.Browser.GetFileFullPath(IODataType.Image, path);

                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    string ext = Path.GetExtension(file).ToLower() ;
                    if (ext == ".jpg" || ext == ".png")
                    {
                        string name = Path.GetFileNameWithoutExtension( file );

                        if (m_main.SpritesMap.IsExist(cate, name) == false)
                        {
                            string filepath = Path.GetFileName(file);
                            m_main.SpritesMap.Add(cate, name, filepath);
                            ++count;
                        }                        
                    }
                }
            }

            MessageBox.Show(String.Format("{0} 개의 Sprite를 새로 등록 하였습니다.", count));

            RightCtrl.UpdateTreeView();
            m_main.UpdateSprite();
        }

        
        private void selectRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectRegionForm selectForm = new SelectRegionForm();
            selectForm.Init(m_main);

            selectForm.Show();
            
        }

        private void MainForm_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

      

        private void imageCleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            List<SpriteInfo> removeList = new List<SpriteInfo>();
            for (int cate = 0; cate < (int)SpriteMap.E_Entity.Max; ++cate)
            {
                foreach (SpriteInfo sprite in m_main.SpritesMap.SpriteUnits[cate])
                {
                    string path = ((SpriteMap.E_Entity)cate).ToString() + "/" + sprite.Path;
                    if (m_main.Browser.Exists(IODataType.Image, path) == false )
                    {
                        removeList.Add( sprite );
                        ++count;
                    }
                }

                foreach( SpriteInfo sprite in removeList )
                {
                    m_main.SpritesMap.SpriteUnits[cate].Remove(sprite);
                }
            }

            MessageBox.Show(String.Format("{0} 개의 Sprite를 정리 하였습니다.", count));

            RightCtrl.UpdateTreeView();
            m_main.UpdateSprite();
        }

        private void actorButton_Click(object sender, EventArgs e)
        {
            ActorForm actorEditor = new ActorForm();
            actorEditor.Init(m_main);
            actorEditor.SelectActor("");
            actorEditor.ShowDialog();
        }

        private void toolStripStageButton_Click(object sender, EventArgs e)
        {
            ActorForm actorEditor = new ActorForm();
            actorEditor.Init(m_main);
            actorEditor.SelectActor("");
            actorEditor.ShowDialog();
        }


    }
}
