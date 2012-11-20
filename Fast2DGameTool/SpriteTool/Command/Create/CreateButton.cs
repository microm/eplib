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
        public CreateButton(StagePictureBox editor)
            : base(editor)
        {
        }

        private CreateButton(StagePictureBox editor, TPoint startPosition, TPoint endPosition, ControlBase createdControl)
            : base(editor, startPosition, endPosition, createdControl)
        {
        }

        public override string Name
        {
            get { return "CreateButton"; }
        }

        public override ICommand Clone()
        {
            return new CreateButton(m_editor, m_startPosition, m_endPosition, m_createControl);
        }

        protected override ControlBase CreateControl()
        {
            if (m_editor.LayerInfo == null) return null;
            return m_editor.LayerInfo.CreateControl( ControlType.Button,  m_startPosition, m_endPosition);
        }
    }
}