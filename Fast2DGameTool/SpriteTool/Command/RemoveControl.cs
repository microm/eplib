using System;
using System.Collections.Generic;
using Tool.TSystem.Pattern;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Control;
using SpriteTool.Helper;
using SpriteTool.Data.Control;

namespace SpriteTool.Command
{
    public class RemoveControl : ICommand
    {
        private readonly Controls m_selectedControls = new Controls();
        private readonly Controls m_controls = new Controls();
        private readonly ControlContainer m_parentsControl;
        
        public RemoveControl( Controls selectedControls, ControlContainer container )
        {
            m_selectedControls = selectedControls;
            m_controls.Set(selectedControls.Get());
            m_parentsControl = container;
        }
        
        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            for (int i = 0; i < m_controls.Count; i++)
            {
                m_selectedControls.Add(m_controls[i]);
            }
        }

        public bool Execute()
        {
            for (int i = 0; i < m_controls.Count; i++ )
            {                
                m_parentsControl.Remove(m_controls[i]);
            }
            m_selectedControls.Clear();
            return true;
        }

        public string Name
        {
            get { return "RemoveControl"; }
        }


        public ICommand Clone()
        {
            return new RemoveControl(m_controls, m_parentsControl);
        }
    }
}