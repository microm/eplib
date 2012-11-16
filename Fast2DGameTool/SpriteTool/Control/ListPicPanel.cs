using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tool.TSystem.ImageMaker;
using System.Drawing.Imaging;
using SpriteTool.Data;

namespace SpriteTool.Control
{
    public class ListPicPanel : FlowLayoutPanel
    {
        public const int _controlID = 3;

        private Main m_main;

        private int prevPos;

        public ListPicPanel()
        {
            this.AutoScroll = true;
            this.WrapContents = false;
            FlowDirection = FlowDirection.LeftToRight;
            AutoScrollMargin = new Size(0, 100);
        }
        
        internal void Init(Main main)
        {
            m_main = main;
        }

        public void RegistMouseDownEvent(System.Windows.Forms.Control control)
        {
            if ( control is Panel || control is UserControl)
            {
                control.MouseDown += ScrolledPanel_MouseDown;
                control.MouseMove += ScrolledPanel_MouseMove;
                control.MouseLeave += ScrolledPanel_MouseLeave;

                foreach (System.Windows.Forms.Control childControl in control.Controls)
                {
                    RegistMouseDownEvent(childControl);
                }
            }
        }

        private void ScrolledPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (HorizontalScroll.Visible)
            {
                this.Cursor = new Cursor(new MemoryStream(Resource3.Grabbing));
                prevPos = ((System.Windows.Forms.Control)sender).PointToScreen(e.Location).X;
            }
        }

        private void ScrolledPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (HorizontalScroll.Visible == false) return;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int curX = ((System.Windows.Forms.Control)sender).PointToScreen(e.Location).X;
                if (curX == prevPos) return;

                int newScrollX = this.HorizontalScroll.Value + (prevPos - curX);
                this.AutoScrollPosition = new Point(this.AutoScrollPosition.X, newScrollX);

                prevPos = curX;
            }
            else
            {
                this.Cursor = new Cursor(new MemoryStream(Resource3.Grab));
            }
        }
        
        private void ScrolledPanel_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }


        internal void ClearControl()
        {
            this.Controls.Clear();
        }

        internal void Add(Bitmap bmp , int index)
        {
            PiecePictureBox picBox = new PiecePictureBox(m_main);

            picBox.Set(bmp, index);
            
            this.Controls.Add(picBox);

            RegistMouseDownEvent(picBox);
        }

        internal PiecePictureBox FindControl(int selectIndex)
        {
            PiecePictureBox picBox = (Controls[selectIndex]) as PiecePictureBox;
            if (picBox != null)
            {
                if (picBox.Index == selectIndex)
                {
                    return picBox;
                }
            }
            return null;
        }

        public void Select(int index)
        {
            for (int i = 0; i < Controls.Count; ++i)
            {
                PiecePictureBox picBox = Controls[i] as PiecePictureBox;
                if (picBox != null)
                {
                    picBox.Selected = (index == i) ? true : false;
                }
            }
            m_main.SelectIndex = index;           
            
            m_main.UpdateSprite();
        }

        public void UpdateSprite()
        {
            if (m_main.SelectSprite == null) return;
            if (m_main.SelectIndex < 0) return;

            PiecePictureBox picBox = Controls[m_main.SelectIndex] as PiecePictureBox;
            if (picBox != null)
            {
                picBox.Selected = true;
            }
        }
    }
}
