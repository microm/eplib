using Tool.TSystem;
using Tool.TSystem.Basis;
using Tool.TSystem.Pattern;
using SpriteTool.Control;
using SpriteTool.Command.Create;

namespace SpriteTool.State
{
    public enum StateType
    {
        None = 0,
        Idle,
        CreateButton,
        CreatePanel,
        CreateLabel,
        Move,
        Resize,
    }

    public class StateManager
    {
        private AbstractState m_currentState;
        private readonly CommandManager m_commandManager;
        private readonly StageBox m_editPanel;       

        private FlagPosition m_flagPosition;

        public StateManager(CommandManager commandManager, StageBox editor)
        {
            m_commandManager = commandManager;
            m_editPanel = editor;

            m_currentState = new IdleState(m_editPanel, this, m_commandManager);
        }

        public StageBox EditPanel
        {
            get { return m_editPanel; }
        } 

        public AbstractState CurrentState
        {
            get { return m_currentState; }
        }

        public FlagPosition FlagPosition
        {
            get { return m_flagPosition; }
            set { m_flagPosition = value; }
        }

        public void ChangeState(StateType type)
        {
            if (EditPanel.LayerInfo == null)
                return;

            switch (type)
            {
                case StateType.CreateButton:
                    m_currentState = new CreateControlState(new CreateButton(m_editPanel), this, m_commandManager);
                    break;
                case StateType.CreateLabel:
                    m_currentState = new CreateControlState(new CreateLabel(m_editPanel), this, m_commandManager);
                    break;
                case StateType.CreatePanel:
                    m_currentState = new CreateControlState(new CreatePanel(m_editPanel), this, m_commandManager);
                    break;
                case StateType.Move:
                    m_currentState = new MoveState(m_editPanel.SelectedControls, this, m_commandManager);
                    break;
                case StateType.Resize:
                    m_currentState = new ResizeControlState(m_editPanel.SelectedControls, this, m_commandManager, m_flagPosition);
                    break;
                case StateType.Idle:
                    m_currentState = new IdleState(m_editPanel, this, m_commandManager);
                    break;
            }
        }
    }
}