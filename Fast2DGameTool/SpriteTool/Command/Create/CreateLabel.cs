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
        public CreateLabel(StageBox editor)
            : base(editor)
        {
        }

        private CreateLabel(StageBox editor, TPoint startPosition, TPoint endPosition, ControlBase control)
            : base( editor,startPosition,endPosition,control)
        {
        }

        public override bool CheckImage // 이미지를 필요로 하나 체크
        {
            get { return false; }
        }

        public override string Name
        {
            get { return "CreateLabel"; }
        }

        public override ICommand Clone()
        {
            return new CreateLabel(m_editPanel, StartPosition, EndPosition, m_createControl);
        }

        protected override ControlBase CreateControl()
        {
            if (m_editPanel.LayerInfo == null) return null;
            return m_editPanel.LayerInfo.CreateControl(ControlType.Label, StartPosition, EndPosition);
        }
    }
}