using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tool.TSystem.ImageMaker;
using System.Drawing.Imaging;
using SpriteTool.Data;
using System.Collections.Generic;
using Tool.TSystem;

namespace SpriteTool.Control
{
    public class BasePictureBox : PictureBox
    {
        public const int _controlID = 1;

        private Main m_main;
        private Size m_imageSize;
        private DevImage m_dImage;
        
        private Point m_start = new Point(0, 0);
        private Point m_end = new Point(0,0);
        private bool m_drag = false;

        private string m_spritePath = "none";

        private Pen m_regionPen = new Pen(Brushes.Black);

        private List<Rectangle> m_regions = new List<Rectangle>();

        public BasePictureBox()
        {
            SetStyle(ControlStyles.Selectable, true);
            BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            m_regionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
        }

        internal void Init(Main main)
        {
            m_main = main;
            if (m_dImage == null)
            {
                m_dImage = new DevImage(100, 100);
            }
            
            m_regionPen.Color = m_main.Form.LineColor;
        }

        public DevImage DImage
        {
            get { return m_dImage; }
        }

        public List<Rectangle> Regions
        {
            get { return m_regions; }
        }
        
        public Size ImageSize
        {
            get { return m_imageSize; }
            set { m_imageSize = value; }
        }
        
        private void Init()
        {
            m_start = m_end;
            m_regions.Clear();           

            m_main.Form.ListPicturePanel.ClearControl();         
        }
                
        public bool LoadPath(string fileName)
        {
            string path = fileName;// ( (SpriteMap.E_Entity) m_main.SpritesMap.SelectCate ).ToString() + "/"
            path = m_main.Browser.GetFileFullPath(IODataType.Image, path);

            Init();

            if (m_dImage.Load(path))
            {
                m_imageSize.Width = m_dImage.Width;
                m_imageSize.Height = m_dImage.Height;

                Image = m_dImage.ConvertRGB();
                Text = fileName;

                m_spritePath = fileName;

                return true;
            }
            return false;
        }

        public void LoadSpriteUnit(SpriteInfo unit)
        {
            if (unit == null) return;
            if (m_spritePath != unit.Path)
            {
                LoadPath(unit.Path);
            }

            UpdateSprite();          
        }

        public bool Save(string path)
        {
            if(m_dImage != null)
            {
                m_dImage.SaveBmp(path);
                return true;
            }
            return false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();

            if (m_main.SelectSprite == null)
                return;

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                m_main.SelectSprite.ColorKey = ((Bitmap)Image).GetPixel(e.X, e.Y);
                m_main.UpdateSprite(_controlID);
                Invalidate();
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                m_start = new Point(e.X, e.Y);
                m_drag = true;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                m_drag = false;
                m_end = new Point(e.X, e.Y);

                if (m_main.SelectSprite == null)
                    return;

                if ( m_start.X < m_end.X && m_start.Y < m_end.Y )
                {
                    if (m_main.SelectIndex < 0)
                    {
                        ImgData img = m_main.SelectSprite.AddRegion(m_start, m_end);
                        m_main.Form.SpriteCtrl.UpdateData();

                        m_regions.Add(new Rectangle(img.Region.Left, img.Region.Top, img.Region.Width, img.Region.Height));
                        
                        // 미리보기 추가
                        m_main.Form.ListPicturePanel.Add(m_dImage.Crop(img.Region.Left, img.Region.Top, img.Region.Width, img.Region.Height), m_main.SelectSprite.ImgList.Count - 1);
                    }
                    else
                    {
                        ImgData img = m_main.SelectSprite.ImgList[m_main.SelectIndex];

                        img.Region = new Tool.TSystem.Primitive.Rect(m_start.X, m_start.Y, m_end.X, m_end.Y);
                        m_regions[m_main.SelectIndex] = new Rectangle(img.Region.Left, img.Region.Top, img.Region.Width, img.Region.Height);

                        PiecePictureBox picBox = m_main.Form.ListPicturePanel.FindControl(m_main.SelectIndex);
                        picBox.Image = m_dImage.Crop(img.Region.Left, img.Region.Top, img.Region.Width, img.Region.Height);
                    }
                    m_main.Form.SpriteCtrl.PivotPicture.UpdateImages();

                    m_main.Form.SpriteCtrl.UpdateTreeView();

                    Invalidate();
                }
            }
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_drag)
            {
                m_end = new Point(e.X, e.Y);
                Invalidate();
            }            
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            Focus();
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            if (m_main.SelectSprite == null)
            {
                return;
            }
            if (m_main.SelectSprite.ImgList.Count == 0)
            {
                ImgData img = m_main.SelectSprite.AddRegion( new Point(0,0) , new Point( Width,Height) );
                m_main.Form.SpriteCtrl.UpdateData();
                m_main.Form.SpriteCtrl.PivotPicture.UpdateImages();
                m_main.Form.SpriteCtrl.UpdateTreeView();
                Invalidate();
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.Parent.Cursor = Cursors.Arrow;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics grfx = pe.Graphics;

            if (m_main == null) return;
            if (m_main.SelectSprite == null)
            {                
                return;
            }

            if (m_main.SelectSprite.HasColorKey && Image != null)
            {
                ImageAttributes attr = new ImageAttributes();
                attr.SetColorKey(m_main.SelectSprite.ColorKey, m_main.SelectSprite.ColorKey);
                grfx.DrawImage(Image, new Rectangle(0, 0, m_imageSize.Width, m_imageSize.Height),
                    0, 0, m_imageSize.Width, m_imageSize.Height, GraphicsUnit.Pixel, attr);
            }
            else
            {
                base.OnPaint(pe);
            }

            if (m_start != m_end)
            {
                grfx.DrawRectangle(m_main.LinePen, new Rectangle(m_start.X, m_start.Y, m_end.X - m_start.X, m_end.Y-m_start.Y));
            }

            int index = 0;
            Pen selectPen = new Pen(Brushes.Red);
            foreach (Rectangle rect in m_regions)
            {
                if (m_main.SelectIndex == index)
                {
                    grfx.DrawRectangle(selectPen, rect);
                }
                else
                {
                    grfx.DrawRectangle(m_regionPen, rect);
                }
                ++index;
            }
        }

        internal void SetGuideColor(Color color)
        {
            m_regionPen.Color = color;
            Invalidate();
        }

        internal void UpdateSprite()
        {
            if ( m_main.SelectSprite == null)
                return;

            Init();
            int index = 0;
            foreach (ImgData img in m_main.SelectSprite.ImgList)
            {
                Rectangle region = new Rectangle(img.Region.Left, img.Region.Top, img.Region.Width, img.Region.Height);
                m_regions.Add(region);

                m_main.Form.ListPicturePanel.Add(m_dImage.Crop(region.X, region.Y, region.Width, region.Height), index);
                ++index;
            }
            Invalidate();
        }
    }
}
