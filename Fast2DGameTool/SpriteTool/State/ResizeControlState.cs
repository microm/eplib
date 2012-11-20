using Tool.TSystem;
using Tool.TSystem.Basis;
using SpriteTool.Helper;
using Tool.TSystem.Primitive;
using Tool.TSystem.Pattern;
using SpriteTool.Data;
using SpriteTool.Command;

namespace SpriteTool.State
{
    public class ResizeControlState : AbstractState
    {
        private readonly CommandManager m_commandManager;
        private readonly Controls m_controls;
        private TPoint m_offset;
        private readonly StateManager m_stateManager;
        private readonly FlagPosition m_flagPosition;

        public ResizeControlState(Controls controls, StateManager stateManager, CommandManager commandManager, FlagPosition flagPosition)
        {
            m_flagPosition = flagPosition;
            m_controls = controls;
            m_stateManager = stateManager;
            m_commandManager = commandManager;
        }

        public override void OnMouseEvent(MouseEvent mouseEvent)
        {
            TPoint currentOffset = mouseEvent.Info.position - mouseEvent.PreviousPosition;

            switch (mouseEvent.State)
            {
                case MouseEvent.EventState.Move:
                    {
                        Resize(m_flagPosition, currentOffset, m_controls);
                        m_offset += currentOffset;
                    }
                    break;
                case MouseEvent.EventState.LUp:
                    {
                        Resize(m_flagPosition, -m_offset,m_controls);

                        m_offset += currentOffset;

                        ICommand command = new ResizeControl(m_controls, m_flagPosition, m_offset);
                        m_commandManager.CurrentCommand = command;
                        m_commandManager.Execute();

                        m_stateManager.ChangeState(StateType.Idle);
                    }
                    break;
            }
        }

        public static void Resize(FlagPosition flagPosition,  TPoint offset , Controls controls)
        {
            foreach (ControlBase control in controls)
            {
                Rect controlRect = control.Rect;

                if ((flagPosition & FlagPosition.Left) == FlagPosition.Left)
                {
                    controlRect.Left = controlRect.Left + offset.X;
                }
                else if ((flagPosition & FlagPosition.Right) == FlagPosition.Right)
                {
                    controlRect.Right = controlRect.Right + offset.X;
                }

                if ((flagPosition & FlagPosition.Top) == FlagPosition.Top)
                {
                    controlRect.Top = controlRect.Top + offset.Y;
                }
                else if ((flagPosition & FlagPosition.Bottom) == FlagPosition.Bottom)
                {
                    controlRect.Bottom = controlRect.Bottom + offset.Y;
                }
                control.Rect = controlRect;
            }
        }
    }
}