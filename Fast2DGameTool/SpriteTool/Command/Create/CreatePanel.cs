using System;
using System.Collections.Generic;
using Tool.TSystem.Pattern;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Control;

namespace SpriteTool.Command.Create
{
    public class CreatePanel : BaseCreateControl
    {
        public CreatePanel(StageBox editor)
            :base(editor)
        {
        }

        private CreatePanel(StageBox editor, TPoint startPosition, TPoint endPosition, ControlBase control)
            :base(editor,startPosition,endPosition,control)
        {
        }

        public override string Name
        {
            get { return "CreatePanel"; }
        }

        public override ICommand Clone()
        {
            return new CreatePanel(m_editPanel, m_startPosition, m_endPosition, m_createControl);
        }

        protected override ControlBase CreateControl()
        {
            if (m_editPanel.LayerInfo == null) return null;
            return m_editPanel.LayerInfo.CreateControl(ControlType.Panel, m_startPosition, m_endPosition);
        }
    }
}