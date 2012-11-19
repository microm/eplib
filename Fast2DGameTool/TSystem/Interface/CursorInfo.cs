using System;
using Tool.TSystem.Primitive;

namespace Tool.TSystem.Interface
{
    public class CursorInfo
    {
        private UInt32 m_ownerID;
        private TPoint m_prevCursorPos;
        private TPoint m_cursorPos;

        public uint OwnerID
        {
            get { return m_ownerID; }
            set { m_ownerID = value; }
        }

        public TPoint PrevCursorPos
        {
            get { return m_prevCursorPos; }
            set { m_prevCursorPos = value; }
        }

        public TPoint CursorPos
        {
            get { return m_cursorPos; }
            set { m_cursorPos = value; }
        }
    }
}