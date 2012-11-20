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
        protected readonly StagePictureBox m_editor;
        protected TPoint m_startPosition;
        protected TPoint m_endPosition;
        protected ControlBase m_createControl;

        public BaseCreateControl(StagePictureBox editor)
        {
            m_editor = editor;
        }

        protected BaseCreateControl(StagePictureBox editor, TPoint startPosition, TPoint endPosition, ControlBase createdControl)
        {
            m_editor = editor;
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
            m_editor.LayerInfo.RemoveControl( m_createControl );
        }

        public bool Execute()
        {
            m_createControl = CreateControl();
            if (m_createControl == null) return false;
            return true;
        }

        public abstract string Name { get; }
        public abstract ICommand Clone();
        protected abstract ControlBase CreateControl();
    }
}
