using System;
using Tool.TSystem.Primitive;

namespace Tool.TSystem.Interface
{
    public class DragInfo
    {
        private UInt32 m_ownerID;
        private TPoint m_startPos;
        private bool m_isStart = false;
        private E_MOUSEDRAG m_type = E_MOUSEDRAG.NONE;
        private E_MOUSEBUTTON m_button = E_MOUSEBUTTON.NO;

        public uint OwnerID
        {
            get { return m_ownerID; }
            set { m_ownerID = value; }
        }

        public TPoint StartPos
        {
            get { return m_startPos; }
            set { m_startPos = value; }
        }

        public bool IsStart
        {
            get { return m_isStart; }
            set { m_isStart = value; }
        }

        public E_MOUSEDRAG Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        public E_MOUSEBUTTON Button
        {
            get { return m_button; }
            set { m_button = value; }
        }
    }
}