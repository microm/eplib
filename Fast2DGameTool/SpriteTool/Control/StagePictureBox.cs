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
using System.Drawing.Drawing2D;

namespace SpriteTool.Control
{    
    public class StagePictureBox : PictureBox
    {
        public const int _controlID = 6;

        private Main m_main;
        private Pen m_linePen = new Pen(Brushes.Black);
        
        private bool m_bPlay = false;
        private Point m_center;        
        
        private DevImage m_dImage;

        private ControlBase m_selectControl;
        private StageForm m_form;
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

        public ControlBase SelectControl
        {
            get { return m_selectControl; }
            set { m_selectControl = value; }
        }
        
        public StagePictureBox()
        {
            SetStyle(ControlStyles.Selectable, true);
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            SizeMode = PictureBoxSizeMode.CenterImage;
            m_regionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
        }

        internal void Init( Main main,StageForm form )
        {
            m_main = main;
            m_form = form;
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
                        
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics grfx = pe.Graphics;
            DrawGrid(grfx);

            if ( m_form == null || m_form.LayerInfo == null)
            {
                base.OnPaint(pe);
                return;
            }
            Pen selectPen = new Pen(Brushes.Red);

            m_form.LayerInfo.Form.Draw(grfx);

            if (m_bGuidLine == true )
            {
                grfx.DrawRectangle(selectPen, m_selectControl.DrawRect);            
            }                  
        }       
      

        public void ResizePanel()
        {
            m_center = new Point(Width / 2, Height / 2);
            Invalidate();
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
