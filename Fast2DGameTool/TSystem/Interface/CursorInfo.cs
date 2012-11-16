using System;
using Tool.TSystem.Primitive;

namespace Tool.TSystem.Interface
{
    public class CursorInfo
    {
        private UInt32 m_ownerID;
        private Point m_prevCursorPos;
        private Point m_cursorPos;

        public uint OwnerID
        {
            get { return m_ownerID; }
            set { m_ownerID = value; }
        }

        public Point PrevCursorPos
        {
            get { return m_prevCursorPos; }
            set { m_prevCursorPos = value; }
        }

        public Point CursorPos
        {
            get { return m_cursorPos; }
            set { m_cursorPos = value; }
        }
    }
}