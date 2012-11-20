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
using SpriteTool.Helper;
using SpriteTool.Data.Control;

namespace SpriteTool.Control
{    
    public class StageBox : PictureBox
    {
        public const int _controlID = 6;

        private Main m_main;        
        private TPoint m_center;        
        
        private StageLayer m_layerInfo;
        private ControlContainer m_containerControl;
        
        private readonly ModifyController m_modifyController;
        private readonly Controls m_selectedControls;
        
        private StageForm m_form;   
        
        private bool m_bGuidLine = true;
        private int m_guidTabSize = 20;

        private Rect m_mouseDragRect = new Rect(0, 0, 0, 0);
        private bool m_drag = false;

        public delegate void SelectControlEvent();

        public int GuidTabSize
        {
            get { return m_guidTabSize; }
            set { 
                m_guidTabSize = value;                
            }
        }

        public ControlContainer ContainerControl
        {
            get { return m_containerControl; }
            set { m_containerControl = value; }
        }

        public StageLayer LayerInfo
        {
            get { return m_layerInfo; }
            set { m_layerInfo = value; }
        }

        public Rect MouseDragRect
        {
            get { return m_mouseDragRect; }
            set { m_mouseDragRect = value; }
        }

        public ModifyController ModifyController
        {
            get { return m_modifyController; }
        }

        public Controls SelectedControls
        {
            get { return m_selectedControls; }
        }
        
        public StageBox()
        {
            SetStyle(ControlStyles.Selectable, true);
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            m_selectedControls = new Controls();
            m_modifyController = new ModifyController(m_selectedControls);
        }

        internal void Init( Main main,StageForm form )
        {
            m_main = main;
            m_form = form;           
            m_center = new TPoint(Width / 2, Height / 2);
        }

        public TPoint Center
        {
            get { return m_center; }
        }

        public bool GuidLine
        {
            get { return m_bGuidLine; }
            set { m_bGuidLine = value; }
        }

        public event SelectControlEvent SelectControlEventHandler;

        public void SelectedControlAdd(ControlBase control)
        {
            if (control.Parent != m_containerControl) 
                return;

            m_selectedControls.Add(control);
            m_modifyController.Refresh();

            if (SelectControlEventHandler != null) SelectControlEventHandler();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            TPoint mousePos = new TPoint(e.X, e.Y);
            
            m_mouseDragRect.Position = mousePos;

            m_drag = true;

            if (LayerInfo != null)
            {
                m_containerControl = LayerInfo.FindContainer(mousePos);
            }            
            Focus();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_drag = false;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            TPoint mousePos = new TPoint(e.X, e.Y);

            if (m_drag)
            {
                m_mouseDragRect.Right = e.X;
                m_mouseDragRect.Bottom = e.Y;
                Invalidate();
            }             
        }

        public List<ControlBase> GetDragControls()
        {
            if (MouseDragRect.Width > 0 && MouseDragRect.Height > 0)
            {
                ControlContainer container = m_layerInfo.FindContainer( m_mouseDragRect.Position );
                return container.ControlInRect(m_mouseDragRect);
            }
            return new List<ControlBase>();
        }
                
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics grfx = pe.Graphics;
            DrawGrid(grfx);

            if ( m_drag && MouseDragRect.Width > 0 && MouseDragRect.Height > 0 )
            {
                Pen penDrag = new Pen(Define.DragLineColor);
                penDrag.DashStyle = DashStyle.Dot;
                grfx.DrawRectangle(penDrag, new Rectangle(MouseDragRect.Left, MouseDragRect.Top, MouseDragRect.Width, MouseDragRect.Height));
            }

            if ( m_form == null || LayerInfo == null)
            {
                base.OnPaint(pe);
                return;
            }
            LayerInfo.Draw(grfx);                                
        }             

        public void ResizePanel()
        {
            m_center = new TPoint(Width / 2, Height / 2);
            Invalidate();
        }

        private void DrawGrid(Graphics grfx)
        {
            if (m_bGuidLine == false)
                return;
    
            Pen penStroke = new Pen(Brushes.Black);
            penStroke.DashStyle = DashStyle.DashDot;
            Pen penThin = new Pen(Brushes.Silver);
            penThin.DashStyle = DashStyle.Dot;

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
