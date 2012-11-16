using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tool.TSystem.ImageMaker;
using Tool.TSystem.Primitive;
using System.Drawing.Imaging;

namespace SpriteTool.Control
{
    public class PiecePictureBox : PictureBox
    {
        private int m_index;
        private bool m_selected = false;
        private Main m_main;

        internal PiecePictureBox(Main main)
        {
            SetStyle(ControlStyles.Selectable, true);
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            SizeMode = PictureBoxSizeMode.AutoSize;

            m_main = main;
        }

        public int Index
        {
            get { return m_index; }
        }

        public bool Selected
        {
            get { return m_selected; }
            set { 
                m_selected = value;
                Invalidate();
            }
        }

        public void Set( Bitmap bmp , int index )
        {
            Image = bmp;
            m_index = index;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (m_main.SelectSprite == null)
            {
                return;
            }

            ListPicPanel listPanel = this.Parent as ListPicPanel;

            if (listPanel != null)
            {
                listPanel.Select(m_index);    
            }

            if (e.Button == MouseButtons.Right)
            {
                Color checkColor = ((Bitmap)Image).GetPixel(0,0);
                if (m_main.SelectSprite.HasColorKey )
                {
                    checkColor = m_main.SelectSprite.ColorKey;
                }
                Rectangle newRect = BitmapAssist.CalcImageRegion((Bitmap)Image, checkColor);
                Rect curRect = m_main.SelectSprite.ImgList[m_index].Region;
                curRect.Left += newRect.X;
                curRect.Right = curRect.Left + newRect.Width;
                curRect.Top += newRect.Y;
                curRect.Bottom = curRect.Top + newRect.Height;
                m_main.SelectSprite.ImgList[m_index].Region = curRect;

                m_main.UpdateSprite();
                
            }
        }
        
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (m_main == null) return;
            if (m_main.SelectSprite == null)
            {
                return;
            }
            Graphics grfx = pe.Graphics;

            if (m_main.SelectSprite.HasColorKey && Image != null)
            {
                ImageAttributes attr = new ImageAttributes();
                attr.SetColorKey(m_main.SelectSprite.ColorKey, m_main.SelectSprite.ColorKey);
                grfx.DrawImage(Image, new Rectangle(0, 0, Image.Width, Image.Height),
                    0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, attr);
            }
            else
            {
                base.OnPaint(pe);
            }

            
            if (m_selected)
            {
                Pen selectPen = new Pen( Brushes.Red );
                grfx.DrawRectangle(selectPen,new Rectangle(0, 0, Image.Width-1, Image.Height-1));            
            }
        }
    }
}
