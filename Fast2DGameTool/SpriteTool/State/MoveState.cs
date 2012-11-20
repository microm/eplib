using Tool.TSystem;
using Tool.TSystem.Basis;
using Tool.TSystem.Pattern;
using SpriteTool.Helper;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Command;

namespace SpriteTool.State
{
    public class MoveState : AbstractState
    {
        private Controls m_controls;
        private StateManager m_stateManager;
        private CommandManager m_commandManager;
        private TPoint m_offset;
        public MoveState( Controls controls, StateManager stateManager, CommandManager commandManager )
        {
            m_controls = controls;
            m_stateManager = stateManager;
            m_commandManager = commandManager;
        }

        public override void OnMouseEvent(MouseEvent mouseEvent)
        {
            TPoint currentOffset = mouseEvent.Info.position - mouseEvent.PreviousPosition;

            switch ( mouseEvent.State )
            {
                case MouseEvent.EventState.Move:
                    {
                        Move(currentOffset, m_controls);
                        m_offset += currentOffset;
                    }
                    break;
                case MouseEvent.EventState.LUp:
                    {
                        Move(-m_offset,m_controls);
                        m_offset += currentOffset;
                        ICommand command = new MoveControl(m_controls, m_offset);
                        m_commandManager.CurrentCommand = command;
                        m_commandManager.Execute();

                        m_stateManager.ChangeState( StateType.Idle );
                    }
                    break;
            }
        }

        public static void Move(TPoint offset,Controls controls)
        {
            foreach (ControlBase control in controls)
            {
                Rect currentRect = control.Rect;

                currentRect.Left += offset.X;
                currentRect.Top += offset.Y;

                control.Rect = currentRect;
            }
        }
    }
}