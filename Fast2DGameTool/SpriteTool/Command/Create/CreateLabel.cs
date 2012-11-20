using System;
using System.Collections.Generic;
using Tool.TSystem.Pattern;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Control;

namespace SpriteTool.Command.Create
{
    public class CreateLabel : BaseCreateControl
    {
        public CreateLabel(StagePictureBox editor)
            : base(editor)
        {
        }

        private CreateLabel(StagePictureBox editor, TPoint startPosition, TPoint endPosition, ControlBase control)
            : base( editor,startPosition,endPosition,control)
        {
        }

        public override string Name
        {
            get { return "CreateLabel"; }
        }

        public override ICommand Clone()
        {
            return new CreateLabel(m_editor, StartPosition, EndPosition, m_createControl);
        }

        protected override ControlBase CreateControl()
        {
            if (m_editor.LayerInfo == null) return null;
            return m_editor.LayerInfo.CreateControl(ControlType.Label, StartPosition, EndPosition);
        }
    }
}