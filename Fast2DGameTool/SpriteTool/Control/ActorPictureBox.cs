using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tool.TSystem.ImageMaker;
using System.Drawing.Imaging;
using SpriteTool.Data;
using System.Collections.Generic;
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
        private AnchorInfo m_selectAnchor;
   
        private Pen m_regionPen = new Pen(Brushes.Black);        
        
        private bool m_bGuidLine = true;
        private int m_guidTabSize = 20;

        public int GuidTabSize
        {
            get { return m_guidTabSize; }
            set { 
                m_guidTabSize = value;                
            }
        }

        public SpriteTool.Data.AnchorInfo SelectAnchor
        {
            get { return m_selectAnchor; }
            set { m_selectAnchor = value; }
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
            
            if (info.SpriteInfo == null)
            {
                SpriteInfo spriteInfo;
                m_main.SpritesMap.FindSpriteUnit(info.Name, out spriteInfo,(int)SpriteMap.E_Entity.Actor);
                info.SpriteInfo = spriteInfo;
            }

            UpdateAnchor();
        }

        public void UpdateAnchor()
        {            
            foreach (AnchorInfo anchor in m_actorInfo.Anchors)
            {
                anchor.LoadBmp(m_main, m_actorInfo.SpriteInfo);
            }
            Invalidate();
        }

        public TPoint GetSpritePos( SpriteInfo info, AnchorInfo anchor)
        {
            if (anchor.Bmp == null)
                return new TPoint(0, 0);

            int index = anchor.Index;
            ImgData imgData = info.ImgList[index];
            TPoint offset = imgData.Pivot;

            if (anchor.XFlip)
            {
                offset.X = anchor.Bmp.Width - imgData.Pivot.X;
            }
            if (anchor.YFlip)
            {
                offset.Y = anchor.Bmp.Height - imgData.Pivot.Y;
            }
            return new TPoint(m_center.X - offset.X, m_center.Y - offset.Y);
        }      
                        
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics grfx = pe.Graphics;
            DrawGrid(grfx);
            if (m_actorInfo == null)
            {
                base.OnPaint(pe);
                return;
            }            
            ImageAttributes attr = new ImageAttributes();
            if (m_actorInfo.SpriteInfo.HasColorKey)
            {
                attr.SetColorKey(m_actorInfo.SpriteInfo.ColorKey, m_actorInfo.SpriteInfo.ColorKey);
            }
            Pen selectPen = new Pen(Brushes.Red);

            List<AnchorInfo> SortInfo = new List<AnchorInfo>(m_actorInfo.Anchors);
            SortInfo.Sort(CompareAnchor);
            foreach (AnchorInfo anchor in SortInfo)
            {
                Bitmap curImage = anchor.Bmp;
                TPoint startPos = GetSpritePos(m_actorInfo.SpriteInfo, anchor) + anchor.Position;
                
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

        private static int CompareAnchor(AnchorInfo lh, AnchorInfo rh)
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
