using System;
using System.Collections.Generic;
using Tool.TSystem.Pattern;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Control;

namespace SpriteTool.Command
{
    public abstract class BaseCreateControl : ICommand
    {
        protected readonly StageBox m_editPanel;
        protected TPoint m_startPosition;
        protected TPoint m_endPosition;
        protected ControlBase m_createControl;

        public BaseCreateControl(StageBox editor)
        {
            m_editPanel = editor;
        }

        protected BaseCreateControl(StageBox editor, TPoint startPosition, TPoint endPosition, ControlBase createdControl)
        {
            m_editPanel = editor;
            m_startPosition = startPosition;
            m_endPosition = endPosition;
            m_createControl = createdControl;
        }

        public TPoint StartPosition
        {
            set { m_startPosition = value; }
            get { return m_startPosition; }
        }

        public TPoint EndPosition
        {
            set { m_endPosition = value; }
            get { return m_endPosition; }
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            m_editPanel.LayerInfo.RemoveControl( m_createControl );
            m_editPanel.Invalidate();
        }

        public bool Execute()
        {
            m_createControl = CreateControl();
            if (m_createControl == null) return false;
            m_editPanel.Invalidate();
            return true;
        }

        public abstract string Name { get; }
        public abstract ICommand Clone();
        protected abstract ControlBase CreateControl();

        public virtual bool CheckImage // 이미지를 필요로 하나 체크
        {
            get { return false; }
        }
    }
}
