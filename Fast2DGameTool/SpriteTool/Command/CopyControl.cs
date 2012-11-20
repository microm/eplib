using System;
using System.Collections.Generic;
using Tool.TSystem.Pattern;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Control;
using SpriteTool.Helper;

namespace SpriteTool.Command
{
    public class CopyControl : ICommand
    {
        private readonly Clipboard m_clipboard;
        private readonly Controls m_controls;

        public CopyControl(Clipboard clipboard, Controls controls)
        {
            m_clipboard = clipboard;
            m_controls = controls;
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            m_clipboard.Clear();
        }

        public bool Execute()
        {
            m_clipboard.Set(m_controls.Get());

            return true;
        }

        public string Name
        {
            get { return "CopyControl"; }
        }

        public ICommand Clone()
        {
            return new CopyControl(m_clipboard,m_controls);
        }
    }
}