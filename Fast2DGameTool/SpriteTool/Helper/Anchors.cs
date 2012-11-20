using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Data.Control;
using Tool.TSystem;

namespace SpriteTool.Helper
{
    public class Anchors
    {
        public readonly static int NumberOfMoveAnchor = 8;

        private readonly List<AnchorInfo> m_infos = new List<AnchorInfo>();
        private readonly ControlBase m_control;
        private Rect m_rect;

        public struct AnchorInfo
        {
            public Rect rect;
            public FlagPosition flag;

            public AnchorInfo(int x, int y, FlagPosition flag)
            {
                rect = new Rect(x, y, Define.AnchorSize, Define.AnchorSize);
                this.flag = flag;
            }

            public AnchorInfo(Rect rect, FlagPosition flag)
            {
                this.rect = rect;
                this.flag = flag;
            }
        }

        public Anchors()
        {
            m_rect = new Rect(0, 0, 10, 10);
        }

        public Anchors(ControlBase control)
        {
            m_control = control;

            TPoint point = control.Rect.Position - new TPoint(Define.AnchorOffset / 2, Define.AnchorOffset / 2);
            TPoint size = control.Rect.Size + new TPoint(Define.AnchorSize, Define.AnchorSize);

            Rect = new Rect(point, size.X, size.Y);

            Generate(control.Rect.Position, control.Rect.RightBottom);

            if (m_control is ControlContainer)
            {
                m_infos.Add(new AnchorInfo(new Rect(point + new TPoint(15, -Define.AnchorOffset / 2), 10, 10), FlagPosition.None));
            }
        }

        public void Generate(TPoint min, TPoint max)
        {
            m_infos.Clear();

            m_infos.Add(new AnchorInfo(min.X - Define.AnchorOffset, min.Y - Define.AnchorOffset, FlagPosition.Left | FlagPosition.Top));

            m_infos.Add(new AnchorInfo((min.X + max.X) / 2 - Define.AnchorSize / 2, min.Y - Define.AnchorOffset, FlagPosition.Top));

            m_infos.Add(new AnchorInfo(max.X + Define.Interval, min.Y - Define.AnchorOffset, FlagPosition.Right | FlagPosition.Top));

            m_infos.Add(new AnchorInfo(min.X - Define.AnchorOffset, (min.Y + max.Y) / 2 - Define.AnchorSize / 2, FlagPosition.Left));

            m_infos.Add(new AnchorInfo(max.X + Define.Interval, (min.Y + max.Y) / 2 - Define.AnchorSize / 2, FlagPosition.Right));

            m_infos.Add(new AnchorInfo(min.X - Define.AnchorOffset, max.Y + Define.Interval, FlagPosition.Left | FlagPosition.Bottom));

            m_infos.Add(new AnchorInfo((min.X + max.X) / 2 - Define.AnchorSize / 2, max.Y + Define.Interval, FlagPosition.Bottom));

            m_infos.Add(new AnchorInfo(max.X + Define.Interval, max.Y + Define.Interval, FlagPosition.Right | FlagPosition.Bottom));
        }

        public int Count
        {
            get { return m_infos.Count; }
        }

        public List<AnchorInfo> Infos
        {
            get { return m_infos; }
        }

        public ControlBase Control
        {
            get { return m_control; }
        }

        public Rect Rect
        {
            get { return m_rect; }
            set { m_rect = value; }
        }

        public void Clear()
        {
            m_infos.Clear();
        }

        public void Draw(Graphics g)
        {
            DrawLine(g, Rect.Position, Rect.RightBottom);

            SolidBrush brush = new SolidBrush(Define.LineColor);
            for (int i = 0; i < m_infos.Count; i++)
            {
                Rectangle drawRect = new Rectangle(
                    m_infos[i].rect.Left,
                    m_infos[i].rect.Top,
                    m_infos[i].rect.Width,
                    m_infos[i].rect.Height);

                if (i == NumberOfMoveAnchor)
                {
                    g.FillRectangle(brush, drawRect);
                }
                else
                {
                    brush.Color = Define.ControllerColor;
                    g.FillRectangle(brush, drawRect);
                }
            }
        }

        private void DrawLine(Graphics g, TPoint min, TPoint max)
        {
            Pen penThin = new Pen(Define.LineColor);
            penThin.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            g.DrawLine(penThin, new Point(min.X, min.Y), new Point(max.X, min.Y)); //up
            g.DrawLine(penThin, new Point(max.X, min.Y), new Point(max.X, max.Y)); //right
            g.DrawLine(penThin, new Point(max.X, max.Y), new Point(min.X, max.Y)); //down
            g.DrawLine(penThin, new Point(min.X, max.Y), new Point(min.X, min.Y)); //left
        }

        public bool ExistsUnder(TPoint position)
        {
            foreach (AnchorInfo info in m_infos)
            {
                if (info.rect.Has(position))
                {
                    return true;
                }
            }
            return false;
        }

        public FlagPosition GetFlag(TPoint position)
        {
            foreach (AnchorInfo info in m_infos)
            {
                if (info.rect.Has(position))
                {
                    return info.flag;
                }
            }
            return FlagPosition.None;
        }
    }
}
