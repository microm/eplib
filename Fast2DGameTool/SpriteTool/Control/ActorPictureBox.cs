using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tool.TSystem.ImageMaker;
using System.Drawing.Imaging;
using SpriteTool.Data;
using System.Collections.Generic;
using TPoint = Tool.TSystem.Primitive.Point;
using Point = System.Drawing.Point;
using Tool.TSystem.Primitive;
using Tool.TSystem;

namespace SpriteTool.Control
{
    public class EditPictureBox : PictureBox
    {
        public const int _controlID = 6;

        private Main m_main;
        private Pen m_linePen = new Pen(Brushes.Black);
        
        private bool m_bPlay = false;
        private ActorInfo m_actorInfo;

        private Point m_center;
        
        private TPoint m_prePos;
        private ActorInfo.Anchor m_selectAnchor;

        private DevImage m_dImage;
        private List<Bitmap> m_srcImageList = new List<Bitmap>();

        private Pen m_regionPen = new Pen(Brushes.Black);
        private bool m_drag = false;
        private bool m_bModify = false;
        
        private bool m_bGuidLine = true;
        private int m_guidTabSize = 20;

        public int GuidTabSize
        {
            get { return m_guidTabSize; }
            set { 
                m_guidTabSize = value;                
            }
        }

        public bool Modify
        {
            get { return m_bModify; }
        }

        public EditPictureBox()
        {
            SetStyle(ControlStyles.Selectable, true);
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            SizeMode = PictureBoxSizeMode.CenterImage;
            m_regionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
        }

        internal void Init( Main main )
        {
            m_main = main;
            if (m_dImage == null)
            {
                m_dImage = new DevImage(100, 100);
            }
            m_center = new Point(Width / 2, Height / 2);
        }

        public Point Center
        {
            get { return m_center; }
        }

        public bool GuidLine
        {
            get { return m_bGuidLine; }
            set { m_bGuidLine = value; }
        }

        public bool Play
        {
            get { return m_bPlay; }
            set { m_bPlay = value; }
        }
        
        public void SetActor( ActorInfo info )
        {
            m_actorInfo = info;

            m_srcImageList.Clear();

            if (info.SpriteInfo == null)
            {
                SpriteInfo spriteInfo;
                m_main.SpritesMap.FindSpriteUnit(info.Name, out spriteInfo,(int)SpriteMap.E_Entity.Actor);
                info.SpriteInfo = spriteInfo;
            }

            string path = "actor/" + info.SpriteInfo.Path;
            path = m_main.Browser.GetFileFullPath(IODataType.Image, path);

            if (m_dImage.Load(path))
            {
                foreach (ImgData img in info.SpriteInfo.ImgList)
                {
                    m_srcImageList.Add(m_dImage.Crop(img.Region.Left, img.Region.Top, img.Region.Width, img.Region.Height));
                }                
            }
            UpdateAnchor();
        }

        public void UpdateAnchor()
        {            
            foreach (ActorInfo.Anchor anchor in m_actorInfo.Anchors)
            {
                Bitmap curImage = m_srcImageList[anchor.Index].Clone() as Bitmap;

                if (anchor.bXFlip)
                {
                    curImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
                if (anchor.bYFlip)
                {
                    curImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }

                anchor.Bmp = curImage;
            }
            Invalidate();
        }

        public TPoint GetSpritePos( SpriteInfo info, ActorInfo.Anchor anchor)
        {
            int index = anchor.Index;

            ImgData imgData =  m_actorInfo.SpriteInfo.ImgList[index];
            TPoint offset = imgData.Pivot;

            if (anchor.bXFlip)
            {
                offset.X = imgData.Region.Width - imgData.Pivot.X;
            }
            if (anchor.bYFlip)
            {
                offset.Y = imgData.Region.Height - imgData.Pivot.Y;
            }

            return new TPoint(m_center.X - offset.X, m_center.Y - offset.Y);
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            m_prePos = new TPoint(e.X, e.Y);

            foreach (ActorInfo.Anchor anchor in m_actorInfo.Anchors)
            {
                Bitmap curImage = m_srcImageList[anchor.Index];

                TPoint startPos = GetSpritePos(m_actorInfo.SpriteInfo, anchor) + anchor.Offset;
                Rect selectRect = new Rect(startPos.X, startPos.Y,startPos.X+ curImage.Width,startPos.Y+ curImage.Height);

                if ( selectRect.Has( m_prePos ) )
                {
                    m_selectAnchor = anchor;
                    m_drag = true;
                }
            }
            Focus();
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (m_selectAnchor != null)
            {
                if (e.Control)
                {
                    m_selectAnchor.Bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    m_selectAnchor.bXFlip = !m_selectAnchor.bXFlip;
                }
                else if (e.Alt)
                {
                    m_selectAnchor.bYFlip = !m_selectAnchor.bYFlip;
                    m_selectAnchor.Bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }
                else if (e.KeyCode== Keys.OemMinus)
                {
                    m_selectAnchor.ZOrder -= 1;
                }
                else if (e.KeyCode == Keys.Oemplus)
                {
                    m_selectAnchor.ZOrder += 1;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    m_actorInfo.Anchors.Remove(m_selectAnchor);
                    m_selectAnchor = null;

                    m_bModify = true;
                    UpdateAnchor();
                }
            }
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            
            TPoint mousePos = new TPoint(e.X, e.Y);

            if (m_selectAnchor != null && m_drag)
            {
                TPoint startPos = GetSpritePos(m_actorInfo.SpriteInfo, m_selectAnchor );
                TPoint newOffset = new TPoint(mousePos.X - m_center.X, mousePos.Y - m_center.Y);

                m_selectAnchor.Offset = newOffset;

                m_bModify = true;
            }
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            m_drag = false;
            //m_selectAnchor = 0;
            //Invalidate();
        }
        
        
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (m_actorInfo == null)
            {
                base.OnPaint(pe);
                return;
            }

            Graphics grfx = pe.Graphics;
            ImageAttributes attr = new ImageAttributes();

            DrawGrid(grfx);

            if (m_actorInfo.SpriteInfo.HasColorKey)
            {
                attr.SetColorKey(m_actorInfo.SpriteInfo.ColorKey, m_actorInfo.SpriteInfo.ColorKey);
            }

            Pen selectPen = new Pen(Brushes.Red);

           
            List<ActorInfo.Anchor> SortInfo = new List<ActorInfo.Anchor>(m_actorInfo.Anchors);
            SortInfo.Sort(CompareAnchor);
            foreach (ActorInfo.Anchor anchor in SortInfo)
            {
                Bitmap curImage = anchor.Bmp;
                TPoint startPos = GetSpritePos(m_actorInfo.SpriteInfo, anchor) + anchor.Offset;
                
                Rectangle drawRect = new Rectangle(startPos.X, startPos.Y, curImage.Width, curImage.Height);
                grfx.DrawImage(curImage, drawRect,
                    0, 0, curImage.Width, curImage.Height, GraphicsUnit.Pixel, attr);

                if (m_bGuidLine == true)
                {
                    if (m_selectAnchor != null && m_selectAnchor == anchor)
                    {
                        grfx.DrawRectangle(selectPen, drawRect);
                    }
                    else
                    {
                        grfx.DrawRectangle(m_regionPen, drawRect);
                    }
                }
            }
        }

        private static int CompareAnchor(ActorInfo.Anchor lh, ActorInfo.Anchor rh)
        {
            if (lh.ZOrder > rh.ZOrder) return 1;
            if (lh.ZOrder < rh.ZOrder) return -1;
            return 0;
        }
        
        private void DrawGrid(Graphics grfx)
        {
            if (m_bGuidLine == false)
                return;
    
            Pen penStroke = new Pen(Brushes.Black);
            penStroke.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            Pen penThin = new Pen(Brushes.Silver);
            penThin.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            grfx.DrawLine(penStroke, new Point(0, m_center.Y), new Point(Width, m_center.Y));
            grfx.DrawLine(penStroke, new Point(m_center.X, 0), new Point(m_center.X, Height));

            for (int x = m_guidTabSize; x < m_center.X; x += m_guidTabSize)
            {
                grfx.DrawLine(penThin, new Point(m_center.X - x, 0), new Point(m_center.X - x, Height));
                grfx.DrawLine(penThin, new Point(m_center.X + x, 0), new Point(m_center.X + x, Height));
            }

            for (int y = m_guidTabSize; y < m_center.Y; y += m_guidTabSize)
            {
                grfx.DrawLine(penThin, new Point(0, m_center.Y -y), new Point(Width, m_center.Y-y));
                grfx.DrawLine(penThin, new Point(0, m_center.Y+y), new Point(Width, m_center.Y+y ));
            }
        }

    }
}
