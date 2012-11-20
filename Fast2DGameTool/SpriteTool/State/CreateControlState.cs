using Tool.TSystem;
using Tool.TSystem.Basis;
using SpriteTool.Helper;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using Tool.TSystem.Pattern;
using SpriteTool.Command;
using System.Windows.Forms;

namespace SpriteTool.State
{
    public class CreateControlState : AbstractState
    {
        private readonly BaseCreateControl m_createCommand;
        private readonly StateManager m_stateManager;
        private readonly CommandManager m_commandManager;
		
        private TPoint m_startPosition;

        public CreateControlState( BaseCreateControl createControlCommand, StateManager stateManager, CommandManager commandManager)
        {
            m_createCommand = createControlCommand;
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
                        if (CanControlAdd(m_createCommand.CheckImage) == false)
                        {
                            return;
                        }
                        m_createCommand.StartPosition = m_startPosition;
                        m_createCommand.EndPosition = mouseEvent.Info.position;
                        m_commandManager.CurrentCommand = m_createCommand;
                        m_commandManager.Execute();
                        m_stateManager.ChangeState( StateType.Idle );
                    }
                    break;
            }
        }


        private bool CanControlAdd(bool checkImage = false)
        {
            if (m_stateManager.EditPanel.SelectedControls == null)
            {
                MessageBox.Show("선택된 Control 이 존재하지 않습니다.");
                return false;
            }
            if ( checkImage )
            {
                if (m_stateManager.EditPanel.Main.SelectSprite == null || m_stateManager.EditPanel.Main.SelectIndex < 0)
                {
                    MessageBox.Show("선택된 이미지가 존재하지 않습니다.");
                    return false;
                }
            }
            return true;
        }
    }
}