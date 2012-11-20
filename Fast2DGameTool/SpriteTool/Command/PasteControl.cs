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
    public class PasteControl : ICommand
    {
        private readonly Clipboard m_clipboard;
        private readonly ControlContainer m_parentsControl;

        private Controls m_undoList;

        public PasteControl( ControlContainer container, Clipboard clipboard)
        {
            m_clipboard = clipboard;
            m_undoList = new Controls();
            m_parentsControl = container;           
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            Controls controls = m_undoList;

            foreach (ControlBase control in controls)
            {
                m_parentsControl.Remove(control);               
            }
            m_undoList.Clear();
        }

        public bool Execute()
        {
            for (int i = 0; i < m_clipboard.Count; i++)
            {
                ControlBase control = m_clipboard.Controls[i];

                if (!(control is ControlBase)) continue;

                ControlBase copyControl = (ControlBase)((ControlBase)control).Clone();

                if (copyControl != null)
                {
                    Rect rect = copyControl.Rect;

                    int pasteNumber = m_clipboard.GetNumber(control.Name);

                    rect.Left = (m_parentsControl.Rect.Width - rect.Width) / 2 + pasteNumber * Define.ControlOffset;
                    rect.Top = (m_parentsControl.Rect.Height - rect.Height) / 2 + pasteNumber * Define.ControlOffset;

                    copyControl.Name = string.Format("{0}{1}", copyControl.Name, pasteNumber);
                    copyControl.Rect = rect;

                    m_parentsControl.Add(copyControl);

                    m_undoList.Add(copyControl);
                }
            }
            return true;
        }

        public string Name
        {
            get { return "PasteControl"; }
        }

        public ICommand Clone()
        {
            PasteControl command = new PasteControl( m_parentsControl, m_clipboard);
            command.m_undoList = m_undoList;

            return command;
        }

    }
}