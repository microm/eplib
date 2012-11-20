using Tool.TSystem;
using Tool.TSystem.Basis;
using Tool.TSystem.Pattern;
using SpriteTool.Control;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Data.Control;
using SpriteTool.Helper;
using System.Collections.Generic;
using SpriteTool.Command;

namespace SpriteTool.State
{
    public class IdleState : AbstractState
    {
        private readonly StageBox m_editPanel;
        private readonly StateManager m_stateManager;
        private readonly CommandManager m_commandManager;

        private readonly Clipboard m_clipboard = new Clipboard();
        private TPoint m_mouseDragStartPoint = new TPoint(0, 0);

        public IdleState(StageBox editor, StateManager stateManager, CommandManager commandManager)
        {
            m_editPanel = editor;
            m_stateManager = stateManager;
            m_commandManager = commandManager;
        }

        public override void OnMouseEvent(MouseEvent mouseEvent)
        {
            if (m_editPanel.LayerInfo == null) return;

            TPoint curPoint = mouseEvent.Info.position;
            switch (mouseEvent.State)
            {
                case MouseEvent.EventState.LDown:
                    {
                        ControlBase control = m_editPanel.LayerInfo.FindControl(curPoint);

                        if (m_editPanel.ModifyController.ExistsAnchorUnder(curPoint ) == false)
                        {
                            SelectEditor(control);
                        }
                        m_mouseDragStartPoint = curPoint;
                        LDownProcess(curPoint,control);
                    }
                    break;
                case MouseEvent.EventState.Move:
                    if (mouseEvent.Info.leftButton == false) return;

                    m_editPanel.MouseDragRect = new Rect(m_mouseDragStartPoint, curPoint);

                    break;
                case MouseEvent.EventState.LUp:
                    if (m_mouseDragStartPoint != curPoint)
                    {
                        SelectDragEditor();
                    }
                    m_editPanel.MouseDragRect = new Rect(0, 0, 0, 0);
                    break;
            }
        }

        private void LDownProcess(TPoint position,ControlBase control)
        {
            if (m_editPanel.SelectedControls.Count == 0) return;
            if (IsControlPressed()) return;

            if (m_editPanel.ModifyController.ExistsAnchorUnder(position))
            {
                m_stateManager.FlagPosition = m_editPanel.ModifyController.GetFlag(position);
                m_stateManager.ChangeState(StateType.Resize);
            }
            if (m_editPanel.ModifyController.IsInSelectedRect(position))
            {
                m_stateManager.ChangeState(StateType.Move);
            }
        }

        private bool IsControlPressed()
        {
            return (m_lockKey & LockKey.Ctrl) > 0;
        }

        private void SelectEditor(ControlBase control)
        {
            if (control == null) return;
            if ( m_editPanel.SelectedControls.Find( control ) > -1 ) return;
            
            if (IsControlPressed())
            {
                m_editPanel.SelectedControlAdd(control);
                return;
            }
            m_editPanel.SelectedControls.Clear();
            m_editPanel.SelectedControlAdd(control);
        }

        private void SelectDragEditor()
        {
            List<ControlBase> controls = m_editPanel.GetDragControls();
            if ( controls.Count == 0 ) return;

            if (IsControlPressed() == false )
            {
                m_editPanel.SelectedControls.Clear();
            }
            foreach (ControlBase control in controls)
            {
                m_editPanel.SelectedControlAdd(control);    
            }
        }

        public override void OnKeyboardEvent(KeyboardEvent keyboardEvent)
        {
            base.OnKeyboardEvent(keyboardEvent);

            if (keyboardEvent.State != KeyboardEvent.EventState.Down) return;
            
            if (keyboardEvent.Key == TKey.ESCAPE)
            {
                m_editPanel.SelectedControls.Clear();
            }
            if (keyboardEvent.LockKey == LockKey.None)
            {
                if (keyboardEvent.Key == TKey.DELETE)
                {
                    OnDelete();
                }
            }
            else if (keyboardEvent.LockKey == LockKey.Ctrl)
            {
                switch (keyboardEvent.Key)
                {
                    case TKey.C:
                        CopyToClipBoard();
                        break;
                    case TKey.X:
                        CutFromClipBoard();
                        break;
                    case TKey.V:
                        PasteFromClipBoard();
                        break;
                }
            }
        }

        public void OnDelete()
        {
            if (m_editPanel.SelectedControls.Count == 0) return;

            m_commandManager.CurrentCommand = new RemoveControl(m_editPanel.SelectedControls, m_editPanel.ContainerControl);
            m_commandManager.Execute();
        }

        public void CopyToClipBoard()
        {
            if (m_editPanel.SelectedControls.Count == 0) return;

            m_commandManager.CurrentCommand = new CopyControl(m_clipboard, m_editPanel.SelectedControls);
            m_commandManager.Execute();
        }

        public void PasteFromClipBoard()
        {
            if (m_clipboard.Count == 0) return;

            m_commandManager.CurrentCommand = new PasteControl(m_editPanel.ContainerControl, m_clipboard);
            m_commandManager.Execute();
        }

        public void CutFromClipBoard()
        {
            if (m_editPanel.SelectedControls.Count == 0) return;

            m_commandManager.CurrentCommand = new CutControl(m_clipboard, m_editPanel.SelectedControls, m_editPanel.ContainerControl);
            m_commandManager.Execute();
        }
       
    }
}