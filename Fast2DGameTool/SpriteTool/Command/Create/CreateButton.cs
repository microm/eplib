using System;
using System.Collections.Generic;
using Tool.TSystem.Pattern;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Control;

namespace SpriteTool.Command.Create
{
    public class CreateButton : BaseCreateControl
    {
        public CreateButton(StageBox editor)
            : base(editor)
        {
        }

        private CreateButton(StageBox editor, TPoint startPosition, TPoint endPosition, ControlBase createdControl)
            : base(editor, startPosition, endPosition, createdControl)
        {
        }

        public override string Name
        {
            get { return "CreateButton"; }
        }

        public override ICommand Clone()
        {
            return new CreateButton(m_editPanel, m_startPosition, m_endPosition, m_createControl);
        }

        protected override ControlBase CreateControl()
        {
            if (m_editPanel.LayerInfo == null) return null;
            return m_editPanel.LayerInfo.CreateControl( ControlType.Button,  m_startPosition, m_endPosition);
        }
    }
}