using System;
using System.Collections.Generic;
using Tool.TSystem.Pattern;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Control;
using SpriteTool.Helper;
using SpriteTool.State;

namespace SpriteTool.Command
{
    public class MoveControl : ICommand
    {
        private readonly Controls m_controls = new Controls();
        private readonly TPoint m_offset;
		
        public MoveControl(Controls controls, TPoint offset)
        {
            m_controls.Set(controls.Get());
            m_offset = offset;
        }

        public void Redo()
        {
            MoveState.Move(m_offset, m_controls);
        }

        public void Undo()
        {
            MoveState.Move(-m_offset, m_controls);	
        }

        public bool Execute()
        {
            MoveState.Move(m_offset, m_controls);
            return true;
        }

        public string Name
        {
            get { return "MoveControl"; }
        }

        public ICommand Clone()
        {
            ICommand command = new MoveControl(m_controls, m_offset);
            return command;
        }
    }
}