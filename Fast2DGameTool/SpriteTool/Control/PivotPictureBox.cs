using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tool.TSystem.ImageMaker;
using System.Drawing.Imaging;
using SpriteTool.Data;
using System.Collections.Generic;
using TPoint = Tool.TSystem.Primitive.Point;

namespace SpriteTool.Control
{
    public class PivotPictureBox : PictureBox
    {
        public const int _controlID = 4;

        private Main m_main;
        
        
        private Point m_startPos = new Point(0,0);
        private bool m_bPlay = false;
        private int m_index = 0;

        public PivotPictureBox()
        {
            SetStyle(ControlStyles.Selectable, true);
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            SizeMode = PictureBoxSizeMode.CenterImage;
        }

        internal void Init(Main main)
        {
            m_main = main;
        }

        public int Index
        {
            get { return m_index; }
            set {
                m_index = value;

                UpdateSprite();
            }
        }

        public bool Play
        {
            get { return m_bPlay; }
            set { m_bPlay = value; }
        }

        public Bitmap CurrentImage
        {
            get
            {
                if (m_main == null || m_index < 0 ) return null;

                if (m_main.SelectSprite == null)
                    return null;
                if (m_index >= m_main.ImageList.Count)
                    return null;

                return m_main.ImageList[m_index];
            }
        }
        
        public void UpdateImages()
        {
            m_main.ImageList.Clear();
            foreach (System.Windows.Forms.Control pic in m_main.Form.ListPicturePanel.Controls )
            {
                PiecePictureBox picBox = pic as PiecePictureBox;
                if (picBox != null)
                {
                    m_main.ImageList.Add((Bitmap)picBox.Image);
                }
            }
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (m_bPlay)
                return;

            if (e.Button != System.Windows.Forms.MouseButtons.Right)
                return;

            if (CurrentImage == null)
                return;

            TPoint newPivot = new TPoint( e.X - m_startPos.X , e.Y - m_startPos.Y );
            m_main.SelectSprite.ImgList[m_index].Pivot = newPivot;

            UpdateSprite();
        }

        public void SetCenterPivot()
        {
            if (CurrentImage == null)
                return;

            TPoint center = new TPoint(m_main.ImageList[m_index].Width / 2, m_main.ImageList[m_index].Height / 2);
            m_main.SelectSprite.ImgList[m_index].Pivot = center;

            UpdateSprite();
        }

        public void UpdateSprite()
        {
            if (CurrentImage == null)
                return;
            Point center = new Point(Width / 2, Height / 2);

            m_startPos = new Point( center.X - m_main.SelectSprite.ImgList[m_index].Pivot.X ,
                center.Y - m_main.SelectSprite.ImgList[m_index].Pivot.Y );

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (CurrentImage == null)
                return;

            string msg = string.Format("xpos = {0}, ypos = {1}", e.X - m_startPos.X, e.Y - m_startPos.Y);
            m_main.Form.StripLabel.Text = msg;

        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.Parent.Cursor = Cursors.Arrow;
        }
        
        protected override void OnPaint(PaintEventArgs pe)
        {  
            if (CurrentImage == null)
                return;

            Graphics grfx = pe.Graphics;
            ImageAttributes attr = new ImageAttributes();

            if (m_main.SelectSprite.HasColorKey)
            {
                attr.SetColorKey(m_main.SelectSprite.ColorKey, m_main.SelectSprite.ColorKey);
            }
            Point center = new Point(Width / 2, Height / 2);

            Bitmap curImage = m_main.ImageList[m_index];

            grfx.DrawImage(curImage, new Rectangle(m_startPos.X, m_startPos.Y, curImage.Width, curImage.Height),
                    0, 0, curImage.Width, curImage.Height, GraphicsUnit.Pixel, attr);

            //Cross Pivot
            grfx.DrawLine(m_main.LinePen, new Point(center.X - 5, center.Y), new Point(center.X + 5, center.Y));
            grfx.DrawLine(m_main.LinePen, new Point(center.X, center.Y - 5), new Point(center.X, center.Y + 5));

        }

    }
}
