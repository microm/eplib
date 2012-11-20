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
    public class CutControl : ICommand
    {
        private readonly Clipboard m_clipBoard;
        private readonly Controls m_selectControls;
        private readonly ControlContainer m_parentsControl;

        public CutControl(Clipboard clipBoard, Controls selectControls, ControlContainer container)
        {
            m_clipBoard = clipBoard;
            m_selectControls = selectControls;
            m_parentsControl = container;
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            m_selectControls.Set(m_clipBoard.Controls.Get());
            
            for (int i = 0; i < m_selectControls.Count; i++)
            {
                m_parentsControl.Add(m_selectControls[i]);
            }
        }

        public bool Execute()
        {
            m_clipBoard.Set(m_selectControls.Get());

            for (int i = 0; i < m_selectControls.Count; i++)
            {
                m_parentsControl.Remove(m_selectControls[i]);
            }
            m_selectControls.Clear();
            return true;
        }

        public string Name
        {
            get { return "CutControl"; }
        }

        public ICommand Clone()
        {
            return new CutControl(m_clipBoard, m_selectControls, m_parentsControl);
        }
    }
}