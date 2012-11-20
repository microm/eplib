using System;
using System.Collections.Generic;
using Tool.TSystem.Pattern;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Control;
using SpriteTool.Helper;
using SpriteTool.State;
using Tool.TSystem;

namespace SpriteTool.Command
{
    public class ResizeControl : ICommand
    {
        protected readonly Controls m_controls = new Controls();
        protected readonly TPoint m_offset;
        protected readonly FlagPosition m_flagPosition;

        public ResizeControl(Controls controls, FlagPosition flagPosition, TPoint offset)
        {
            m_controls.Set(controls.Get());
            m_flagPosition = flagPosition;
            m_offset = offset;
        }

        public void Redo()
        {
            ResizeControlState.Resize(m_flagPosition, m_offset, m_controls);
        }

        public void Undo()
        {
            ResizeControlState.Resize(m_flagPosition, -m_offset, m_controls);
        }

        public bool Execute()
        {
            ResizeControlState.Resize(m_flagPosition, m_offset,m_controls);
            return true;
        }

        public string Name
        {
            get { return "ResizeControl"; }
        }

        public ICommand Clone()
        {
            return new ResizeControl(m_controls, m_flagPosition, m_offset);
        }
    }
}