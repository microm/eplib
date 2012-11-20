using Tool.TSystem;
using Tool.TSystem.Basis;

namespace SpriteTool.State
{
    public abstract class AbstractState 
    {
        protected LockKey m_lockKey = LockKey.None;

        public abstract void OnMouseEvent(MouseEvent mouseEvent);
        public virtual void OnKeyboardEvent(KeyboardEvent keyEvent)
        {
            m_lockKey = keyEvent.LockKey;
        }
    }
}