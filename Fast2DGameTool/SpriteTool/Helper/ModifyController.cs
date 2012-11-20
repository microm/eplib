using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SpriteTool.Data.Control;
using SpriteTool.Data;
using Tool.TSystem.Primitive;
using Tool.TSystem;

namespace SpriteTool.Helper
{
    public class ModifyController
    {
        private readonly List<Anchors> m_anchors = new List<Anchors>();
        private readonly Controls m_selectedControls;

        public ModifyController(Controls selectedControls)
        {
            m_selectedControls = selectedControls;
        }

        public int AnchorsCount
        {
            get { return m_anchors.Count; }
        }

        public bool ExistsAnchorUnder(TPoint position)
        {
            foreach (Anchors anchor in m_anchors)
            {
                if (anchor.ExistsUnder(position))
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw(Graphics renderer)
        {
            if (m_anchors.Count == 0) return;

            foreach (Anchors anchor in m_anchors)
            {
                anchor.Draw(renderer);
            }

            Refresh();
        }

        public void Remove(StageLayer Stage )
        {
            foreach (ControlBase control in m_selectedControls)
            {
                Stage.RemoveControl( control);
            }
        }

        public bool IsInSelectedRect(TPoint position)
        {
            foreach (Anchors anchor in m_anchors)
            {
                if (anchor.Control is ControlContainer)
                {
                    if (anchor.Infos[Anchors.NumberOfMoveAnchor].rect.Has(position))
                        return true;

                    continue;
                }

                if (anchor.Rect.Has(position))
                {
                    return true;
                }
            }
            return false;
        }

        public ControlBase ControlInRect(TPoint position)
        {
            foreach (Anchors anchor in m_anchors)
            {
                if (anchor.Rect.Has(position))
                {
                    return anchor.Control;
                }
            }
            return null;
        }

        public void Refresh()
        {
            m_anchors.Clear();
            foreach (ControlBase control in m_selectedControls)
            {
                m_anchors.Add(new Anchors(control));
            }
        }

        public FlagPosition GetFlag(TPoint position)
        {
            foreach (Anchors anchor in m_anchors)
            {
                FlagPosition flag = anchor.GetFlag(position);
                if (flag != FlagPosition.None)
                {
                    return flag;
                }
            }

            return FlagPosition.None;
        }
    }
}
