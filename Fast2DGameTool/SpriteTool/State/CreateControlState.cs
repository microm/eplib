using Tool.TSystem;
using Tool.TSystem.Basis;
using SpriteTool.Helper;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using Tool.TSystem.Pattern;
using SpriteTool.Command;

namespace SpriteTool.State
{
    public class CreateControlState : AbstractState
    {
        private readonly BaseCreateControl m_createControlCommand;
        private readonly StateManager m_stateManager;
        private readonly CommandManager m_commandManager;
		
        private TPoint m_startPosition;

        public CreateControlState( BaseCreateControl createControlCommand, StateManager stateManager, CommandManager commandManager)
        {
            m_createControlCommand = createControlCommand;
            m_stateManager = stateManager;
            m_commandManager = commandManager;			
        }

        public override void OnMouseEvent(MouseEvent mouseEvent)
        {
            switch(mouseEvent.State)
            {
                case MouseEvent.EventState.LDown:
                    {
                        m_startPosition = mouseEvent.Info.position;
                    }
                    break;
                case MouseEvent.EventState.LUp:
                    {
                        m_createControlCommand.StartPosition = m_startPosition;
                        m_createControlCommand.EndPosition = mouseEvent.Info.position;
                        m_commandManager.CurrentCommand = m_createControlCommand;
                        m_commandManager.Execute();
                        m_stateManager.ChangeState( StateType.Idle );
                    }
                    break;
            }
        }
    }
}